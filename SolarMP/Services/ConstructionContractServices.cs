﻿using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.ConstructionContract;
using SolarMP.Interfaces;
using SolarMP.Models;

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
                    con.Status = false;
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
                var data = await this.context.ConstructionContract.Where(x => x.Status)
                    .Include(x=>x.Package)
                    .Include(x=>x.Bracket)
                    .Include(x=>x.PaymentProcess)
                    .Include(x=>x.Staff)
                    .Include(x=>x.Customer)
                    .Include(x=>x.Process)
                    .Include(x=>x.Acceptance)
                    .Include(x=>x.Feedback)
                    .Include(x=>x.WarrantyReport)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<ConstructionContract>> GetConstructionContractByCusId(string cusId)
        {
            try
            {
                var data = await this.context.ConstructionContract.Where(x => x.Status && x.CustomerId.Equals(cusId))
                    .Include(x => x.Package)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
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
                var data = await this.context.ConstructionContract.Where(x => x.Status && x.ConstructioncontractId.Equals(constructionContractId))
                    .Include(x => x.Package)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
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
                var data = await this.context.ConstructionContract.Where(x => x.Status && x.Staffid.Equals(StaffId))
                    .Include(x => x.Package)
                    .Include(x => x.Bracket)
                    .Include(x => x.PaymentProcess)
                    .Include(x => x.Staff)
                    .Include(x => x.Customer)
                    .Include(x => x.Process)
                    .Include(x => x.Acceptance)
                    .Include(x => x.Feedback)
                    .Include(x => x.WarrantyReport)
                    .ToListAsync();
                if (data.Count > 0 && data != null)
                    return data;
                else throw new ArgumentException();
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
                _constructionContract.Status = true;
                await this.context.ConstructionContract.AddAsync(_constructionContract);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateConstructionContract(ConstructionContractUpdateDTO upConstructionContract)
        {
            try
            {
                ConstructionContract _constructionContract = await this.context.ConstructionContract.FirstAsync(x => x.ConstructioncontractId == upConstructionContract.ConstructioncontractId);
                if (_constructionContract != null)
                {
                    _constructionContract.Startdate = upConstructionContract.Startdate ?? _constructionContract.Startdate;
                    _constructionContract.Enddate = upConstructionContract.Enddate ?? _constructionContract.Enddate;
                    _constructionContract.Totalcost = upConstructionContract.Totalcost ?? _constructionContract.Totalcost;
                    _constructionContract.IsConfirmed = upConstructionContract.IsConfirmed ?? _constructionContract.IsConfirmed;
                    _constructionContract.ImageFile = upConstructionContract.ImageFile ?? _constructionContract.ImageFile;
                    _constructionContract.CustomerId = upConstructionContract.CustomerId ?? _constructionContract.CustomerId;
                    _constructionContract.Staffid = upConstructionContract.Staffid ?? _constructionContract.Staffid;
                    _constructionContract.PackageId = upConstructionContract.PackageId ?? _constructionContract.PackageId;
                    _constructionContract.BracketId = upConstructionContract.BracketId ?? _constructionContract.BracketId;
                    _constructionContract.Status = upConstructionContract.Status ?? _constructionContract.Status;
                    context.ConstructionContract.Update(_constructionContract);
                    this.context.SaveChanges();
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