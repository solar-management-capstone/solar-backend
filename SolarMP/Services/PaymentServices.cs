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
                var pay = new PaymentProcess();
                pay.AccountId= dto.AccountId;
                pay.ConstructionContractId= dto.ConstructionContractId;
                pay.Amount= dto.Amount;
                pay.Status = "Paid";
                pay.CreateAt = DateTime.Now;
                pay.PaymentId = "PAY"+Guid.NewGuid().ToString().Substring(0,13);

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
