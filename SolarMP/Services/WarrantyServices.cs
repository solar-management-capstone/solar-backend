using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.WarrantyReport;
using SolarMP.Interfaces;
using SolarMP.Models;
using System.Diagnostics;

namespace SolarMP.Services
{
    public class WarrantyServices : IWarrantyReport
    {
        protected readonly solarMPContext context;
        public WarrantyServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<WarrantyReport> delete(string id)
        {
            try
            {
                var check = await this.context.WarrantyReport.Where(x => x.WarrantyId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.WarrantyReport.Update(check);
                    await this.context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WarrantyReport>> getAll()
        {
            try
            {
                var check = await this.context.WarrantyReport.Where(x => x.Status)
                    .Include(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WarrantyReport>> getAllForAdmin()
        {
            try
            {
                var check = await this.context.WarrantyReport
                    .Include(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WarrantyReport>> getAllForContract(string id)
        {
            try
            {
                var check = await this.context.WarrantyReport.Where(x => x.ContractId.Equals(id))
                    .Include(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<WarrantyReport>> getAllForCus(string id)
        {
            try
            {
                var check = await this.context.WarrantyReport.Where(x => x.AccountId.Equals(id))
                    .Include(x => x.Image)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WarrantyReport> insert(WarrantyReportDTO dto)
        {
            try
            {
                var rpt = new WarrantyReport();
                rpt.WarrantyId = "WRT" + Guid.NewGuid().ToString().Substring(0, 13);
                rpt.Status = true;
                rpt.AccountId = dto.AccountId;
                rpt.ContractId = dto.ContractId;
                rpt.DateTime = DateTime.Now;
                rpt.Manufacturer = dto.Manufacturer;
                rpt.Description = dto.Description;
                rpt.Feature = dto.Feature;

                await this.context.WarrantyReport.AddAsync(rpt);
                await this.context.SaveChangesAsync();

                if (dto.image != null && dto.image.Count > 0)
                {
                    foreach (var image in dto.image)
                    {
                        var img = new Image();
                        img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                        img.ImageData = image.image;
                        img.WarrantyReportId = rpt.WarrantyId;
                        await this.context.Image.AddAsync(img);
                        await this.context.SaveChangesAsync();
                    }
                }

                return rpt;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> insertProduct(ProductWarrantyDTO dto)
        {
            try
            {
                foreach(var x in dto.damages)
                {
                    var prorpt = new ProductWarrantyReport();
                    prorpt.ProductId = x.ProductId;
                    prorpt.WarrantyId = dto.WarrantyId;
                    prorpt.Status = true;
                    prorpt.AmountofDamageProduct = x.AmountofDamageProduct;

                    await this.context.ProductWarrantyReport.AddAsync(prorpt);
                    await this.context.SaveChangesAsync();
                }
                return true;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
