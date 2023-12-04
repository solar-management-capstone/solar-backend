using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Payment;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class PaymentServices : IPayment
    {
        protected readonly solarMPContext context;
        public PaymentServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<List<PaymentProcess>> getAll()
        {
            try
            {
                var check = await this.context.PaymentProcess
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Account)
                    .ToListAsync();
                return check;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PaymentProcess>> getAllContract(string id)
        {
            try
            {
                var check = await this.context.PaymentProcess.Where(x=>x.ConstructionContractId.Equals(id))
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Account)
                    .ToListAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PaymentProcess>> getAllUser(string id)
        {
            try
            {
                var check = await this.context.PaymentProcess.Where(x => x.AccountId.Equals(id))
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Account)
                    .ToListAsync();
                return check;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaymentProcess> insert(PaymentDTO dto)
        {
            try
            {
                var check = await this.context.ConstructionContract
                    .Where(x => x.ConstructioncontractId.Equals(dto.ConstructionContractId))
                    .FirstOrDefaultAsync();
                if (check == null)
                {
                    throw new Exception("Hợp đồng không tồn tại");
                }
                if(check.Status == "2")
                {
                    var deposit = await this.context.PaymentProcess
                        .Where(x => x.ConstructionContractId.Equals(dto.ConstructionContractId) && x.IsDeposit)
                        .FirstOrDefaultAsync();
                    if(deposit != null)
                    {
                        if(deposit.Status == "Paid")
                        {
                            return deposit;
                        }
                        if(deposit.Status == "success")
                        {
                            throw new Exception("Đã thanh toán cọc");
                        }
                    }
                }
                if(check.Status == "3")
                {
                    var payment = await this.context.PaymentProcess
                        .Where(x => x.ConstructionContractId.Equals(dto.ConstructionContractId) && !x.IsDeposit)
                        .FirstOrDefaultAsync();
                    if (payment != null)
                    {
                        if (payment.Status == "Paid")
                        {
                            return payment;
                        }
                        if (payment.Status == "success")
                        {
                            throw new Exception("Đã thanh toán phần còn lại");
                        }
                    }
                }
                if(check.Status != "3" && check.Status != "2")
                {
                    throw new Exception("Hợp đồng chưa tới giai đoạn thanh toán");
                } 
                var pay = new PaymentProcess();
                pay.AccountId= dto.AccountId;
                pay.ConstructionContractId= dto.ConstructionContractId;
                pay.Amount= (decimal)(check.Totalcost /2);
                pay.Status = "Paid";
                pay.CreateAt = DateTime.Now;
                pay.PaymentId = "PAY" + Guid.NewGuid().ToString().Substring(0, 13);
                pay.IsDeposit = false;
                if (check.Status == "2")
                {
                    pay.IsDeposit = true;
                }
                

                await this.context.PaymentProcess.AddAsync(pay);
                await this.context.SaveChangesAsync();
                return pay;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
