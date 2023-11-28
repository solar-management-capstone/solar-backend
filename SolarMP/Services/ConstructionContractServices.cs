using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.ConstructionContract;
using SolarMP.Interfaces;
using SolarMP.Models;
using System;

namespace SolarMP.Services
{
    public class ConstructionContractServices : IConstructionContract
    {
        protected readonly solarMPContext context;
        public ConstructionContractServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteConstructionContract(string constructionContractId)
        {
            try
            {
                var con = await this.context.ConstructionContract
                    .Where(x => constructionContractId.Equals(x.ConstructioncontractId))
                    .FirstOrDefaultAsync();
                if (con != null)
                {
                    con.Status = "0";
                    this.context.ConstructionContract.Update(con);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Construction Contract found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<ConstructionContract>> GetAllConstructionContracts()
        {
            try
            {
                var data = await this.context.ConstructionContract
                    .Include(x=>x.Package)
                        .ThenInclude(x=>x.PackageProduct)
                            .ThenInclude(x =>x.Product)
                                .ThenInclude(x=>x.Image)
                    .Include(x => x.Package)
                        .ThenInclude(x=>x.Promotion)
                    .Include(x=>x.Bracket)
                    .Include(x=>x.PaymentProcess)
                    .Include(x=>x.Staff)
                    .Include(x=>x.Customer)
                    .Include(x=>x.Process.OrderBy(x => x.CreateAt))
                        .ThenInclude(x => x.Image)
                    .Include(x=>x.Acceptance)
                    .Include(x=>x.Feedback)
                    .Include(x=>x.WarrantyReport)
                    .Include(x=>x.Survey)
                        .ThenInclude(x=>x.Request)
                    .ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  List<ConstructionContract> GetAllConstructionContractsByCusIDv2(CusManyStatusDTO dto)
        {
            try
            {
                var result = new List<ConstructionContract>();
                foreach (var item in dto.status)
                {
                    var check = this.context.ConstructionContract
                        .Where(x => x.CustomerId.Equals(dto.customerId) && x.Status.Equals(item.status))
                    .Include(x => x.Package)
                        .ThenInclude(x => x.PackageProduct)
                            .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Image)
                    .Include(x => x.Package)
                        .ThenInclude(x => x.Promotion)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process.OrderBy(x => x.CreateAt))
                        .ThenInclude(x => x.Image)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .Include(x => x.Survey)
                        .ThenInclude(x => x.Request)
                    .OrderByDescending(x=>x.Startdate)
                    .ToList();
                    result.AddRange(check);
                }
                return result;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ConstructionContract>> GetConstructionContractByCusId(string cusId , string? status)
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.CustomerId.Equals(cusId) && x.Status.Equals(status))
                    .Include(x => x.Package)
                        .ThenInclude(x => x.PackageProduct)
                            .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Image)
                    .Include(x => x.Package)
                        .ThenInclude(x => x.Promotion)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process.OrderBy(x => x.CreateAt))
                        .ThenInclude(x => x.Image)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .Include(x => x.Survey)
                        .ThenInclude(x => x.Request)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<ConstructionContract>> GetConstructionContractById(string? constructionContractId)
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.ConstructioncontractId.Equals(constructionContractId))
                    .Include(x => x.Package)
                        .ThenInclude(x => x.PackageProduct)
                            .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Image)
                    .Include(x => x.Package)
                        .ThenInclude(x => x.Promotion)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process.OrderBy(x=>x.CreateAt))
                        .ThenInclude(x=>x.Image)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .Include(x => x.Survey)
                        .ThenInclude(x => x.Request)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<ConstructionContract>> GetConstructionContractByStaffId(string StaffId)
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.Staffid.Equals(StaffId))
                    .Include(x => x.Package)
                        .ThenInclude(x => x.PackageProduct)
                            .ThenInclude(x => x.Product)
                                .ThenInclude(x => x.Image)
                    .Include(x => x.Package)
                        .ThenInclude(x => x.Promotion)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process.OrderBy(x => x.CreateAt))
                        .ThenInclude(x => x.Image)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .Include(x => x.Survey)
                        .ThenInclude(x => x.Request)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> InsertConstructionContract(ConstructionContractDTO constructionContract)
        {
            try
            {
                var _constructionContract = new ConstructionContract();
                _constructionContract.ConstructioncontractId = "CON" + Guid.NewGuid().ToString().Substring(0, 13);
                _constructionContract.Startdate = constructionContract.Startdate;
                _constructionContract.Enddate = constructionContract.Enddate;
                _constructionContract.Totalcost = constructionContract.Totalcost;
                _constructionContract.IsConfirmed= constructionContract.IsConfirmed;
                _constructionContract.ImageFile= constructionContract.ImageFile;
                _constructionContract.CustomerId = constructionContract.CustomerId;
                _constructionContract.Staffid = constructionContract.Staffid;
                _constructionContract.PackageId = constructionContract.PackageId;
                _constructionContract.BracketId = constructionContract.BracketId;
                _constructionContract.Status = "1";
                _constructionContract.Description = constructionContract.Description ?? null;
                _constructionContract.SurveyId = constructionContract.SurveyId ?? null;

                var pck = await this.context.Package.Where(x => x.PackageId.Equals(constructionContract.PackageId))
                    .Include(x=>x.Promotion)
                    .FirstOrDefaultAsync();
                var brc = await this.context.Bracket.Where(x => x.BracketId.Equals(constructionContract.BracketId)).FirstOrDefaultAsync();

                decimal tmp = (decimal)pck.Price;
                if(pck.PromotionId != null)
                {
                    if (pck.Promotion.StartDate < DateTime.Now && pck.Promotion.EndDate > DateTime.Now)
                    {
                        tmp = (decimal)pck.PromotionPrice;
                    }
                }

                _constructionContract.Totalcost = tmp + brc.Price;
                await this.context.ConstructionContract.AddAsync(_constructionContract);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateConstructionContract(ConstructionContractUpdateDTO upConstructionContract)
        {
            try
            {
                ConstructionContract _constructionContract = await this.context.ConstructionContract
                    .Include(x=>x.Survey)
                    .FirstAsync(x => x.ConstructioncontractId == upConstructionContract.ConstructioncontractId);
                if (_constructionContract != null)
                {
                    _constructionContract.Startdate = upConstructionContract.Startdate ?? _constructionContract.Startdate;
                    _constructionContract.Enddate = upConstructionContract.Enddate ?? _constructionContract.Enddate;
                    _constructionContract.IsConfirmed = upConstructionContract.IsConfirmed ?? _constructionContract.IsConfirmed;
                    _constructionContract.ImageFile = upConstructionContract.ImageFile ?? _constructionContract.ImageFile;
                    _constructionContract.CustomerId = upConstructionContract.CustomerId ?? _constructionContract.CustomerId;
                    _constructionContract.Staffid = upConstructionContract.Staffid ?? _constructionContract.Staffid;
                    _constructionContract.PackageId = upConstructionContract.PackageId ?? _constructionContract.PackageId;
                    _constructionContract.BracketId = upConstructionContract.BracketId ?? _constructionContract.BracketId;
                    _constructionContract.Status = upConstructionContract.Status ?? _constructionContract.Status;
                    _constructionContract.Description = upConstructionContract.Description ?? _constructionContract.Description;
                    _constructionContract.SurveyId = upConstructionContract.SurveyId ?? _constructionContract.SurveyId;

                    //decimal tmp = 0;
                    //decimal tmpbck = 0;
                    //if (upConstructionContract.PackageId!= null)
                    //{
                    //    var pck = await this.context.Package.Where(x => x.PackageId.Equals(upConstructionContract.PackageId ?? _constructionContract.PackageId))
                    //        .Include(x => x.Promotion)
                    //        .FirstOrDefaultAsync();
                    //    tmp = (decimal)pck.Price;
                    //    if (pck.Promotion.StartDate < DateTime.Now && pck.Promotion.EndDate > DateTime.Now)
                    //    {
                    //        tmp = (decimal)pck.PromotionPrice;
                    //    }
                    //}
                    //if(upConstructionContract.BracketId!= null)
                    //{
                    //    var brc = await this.context.Bracket.Where(x => x.BracketId.Equals(upConstructionContract.BracketId ?? _constructionContract.BracketId)).FirstOrDefaultAsync();
                    //    tmpbck = (decimal)brc.Price;
                    //}
                    //if(tmp > 0)
                    //{

                    //}
                    //if (tmpbck > 0) { 
                    //}
                    if(upConstructionContract.BracketId!= null || upConstructionContract.PackageId != null)
                    {
                        var pck = await this.context.Package.Where(x => x.PackageId.Equals(upConstructionContract.PackageId))
                                    .Include(x => x.Promotion)
                                    .FirstOrDefaultAsync();
                        var brc = await this.context.Bracket.Where(x => x.BracketId.Equals(upConstructionContract.BracketId))
                                    .FirstOrDefaultAsync();

                        decimal tmp = (decimal)pck.Price;
                        if (pck.Promotion.StartDate < DateTime.Now && pck.Promotion.EndDate > DateTime.Now)
                        {
                            tmp = (decimal)pck.PromotionPrice;
                        }

                        _constructionContract.Totalcost = tmp + brc.Price;
                    }
                    
                    context.ConstructionContract.Update(_constructionContract);
                    this.context.SaveChanges();
                    if(upConstructionContract.SurveyId != null)
                    {
                        var disable = await this.context.Request.Where(x => x.RequestId.Equals(_constructionContract.Survey.RequestId))
                        .FirstOrDefaultAsync();
                        disable.Status = false;
                        this.context.Request.Update(disable);
                        await this.context.SaveChangesAsync();
                    }
                    
                    return true;
                }
                else
                {
                    throw new ArgumentException("Construction Contract not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
