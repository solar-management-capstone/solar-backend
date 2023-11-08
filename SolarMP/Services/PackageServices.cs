using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Package;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class PackageServices : IPackage
    {
        protected readonly solarMPContext context;
        public PackageServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<Package> delete(string id)
        {
            try
            {
                var check = await this.context.Package.Where(x=>x.PackageId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Package.Update(check);
                    await this.context.SaveChangesAsync();

                    return check;
                }
                return null;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getAll()
        {
            try
            {
                var check = await this.context.Package.Where(x => x.Status)
                    .Include(x=>x.PackageProduct)
                        .ThenInclude(x=>x.Product)
                            .ThenInclude(x=>x.Image)
                    .Include(x => x.Promotion)
                    .Include(x=>x.ConstructionContract)
                    .Include(x=>x.Feedback)
                    .Include(x=>x.Request)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Package>> SortPck(int? area, decimal? bill)
        {
            try
            {
                if(area != null && area > 0 && bill != null && bill >0)
                {
                    var full = await this.context.Package
                    .Where(x => (x.RoofArea <= area && x.ElectricBill <= bill))
                    .Where(x => x.Status)
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Feedback)
                    .Include(x => x.Request)
                    .ToListAsync();

                    if(full != null && full.Count>0)
                    {
                        return full;
                    }
                }
                var check = await this.context.Package
                    .Where(x => (x.RoofArea <= area || x.ElectricBill <= bill))
                    .Where(x => x.Status)
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Feedback)
                    .Include(x => x.Request)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getAllForAdmin()
        {
            try
            {
                var check = await this.context.Package
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Package> getById(string id)
        {
            try
            {
                var check = await this.context.Package.Where(x => x.PackageId.Equals(id))
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .Include(x => x.ConstructionContract)
                    .Include(x => x.Feedback)
                    .Include(x => x.Request)
                    .FirstOrDefaultAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getByName(string name)
        {
            try
            {
                var check = await this.context.Package.Where(x => x.Status && x.Name.Contains(name))
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .ToListAsync();
                if (check != null)
                {
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Package> insert(PackageCreateDTO dto)
        {
            try
            {
                var package = new Package();
                package.Status = true;
                package.PromotionId = dto.PromotionId ?? null;
                package.Name = dto.Name;
                package.Price = 0;
                package.PromotionPrice = 0;
                package.RoofArea = dto.RoofArea ?? 0;
                package.ElectricBill = dto.ElectricBill ?? 0;
                package.Description = dto.Description;
                package.PackageId = "PCK" + Guid.NewGuid().ToString().Substring(0,13);

                await this.context.Package.AddAsync(package);
                await this.context.SaveChangesAsync();

                return package;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> insertProduct(PackageProductCreateDTO dto)
        {
            try
            {
                var check = await this.context.PackageProduct.Where(x => x.PackageId.Equals(dto.PackageId)).ToListAsync();
                var pckCheck = await this.context.Package.Where(x => x.PackageId.Equals(dto.PackageId)).FirstOrDefaultAsync();
                if (check != null && check.Count>0)
                {
                    foreach(var item in check)
                    {
                        this.context.PackageProduct.Remove(item);
                        await this.context.SaveChangesAsync();
                    }
                    
                    pckCheck.Price = 0;
                    this.context.Package.Update(pckCheck);
                    await this.context.SaveChangesAsync();
                }
                foreach(var x in dto.listProduct)
                {
                    var product = new PackageProduct();
                    product.ProductId = x.productId;
                    product.PackageId = dto.PackageId;
                    product.Status = true;
                    product.Quantity = x.quantity;

                    await this.context.PackageProduct.AddAsync(product);
                    await this.context.SaveChangesAsync();
                    var pro = await this.context.Product.Where(p => p.ProductId.Equals(x.productId)).FirstOrDefaultAsync();
                    //var pck = await this.context.Package.Where(x => x.PackageId.Equals(dto.PackageId)).FirstOrDefaultAsync();
                    pckCheck.Price += pro.Price* x.quantity;

                    this.context.Package.Update(pckCheck);
                    await this.context.SaveChangesAsync();
                }
                if(pckCheck.PromotionId !=null)
                {
                    var prom = await this.context.Promotion.Where(x => x.PromotionId.Equals(pckCheck.PromotionId)).FirstOrDefaultAsync();
                    pckCheck.PromotionPrice = pckCheck.Price - (pckCheck.Price * prom.Amount)/100;
                    this.context.Package.Update(pckCheck);
                    await this.context.SaveChangesAsync();
                }
                
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Package> update(PackageUpdateDTO dto)
        {
            try
            {
                var check = await this.context.Package.Where(x => x.PackageId.Equals(dto.PackageId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = dto.Status ?? check.Status;
                    check.Name = dto.Name ?? check.Name;
                    check.Description = dto.Description ?? check.Description;
                    check.PromotionId = dto.PromotionId ?? check.PromotionId;
                    check.RoofArea = dto.RoofArea ?? check.RoofArea;
                    check.ElectricBill = dto.ElectricBill ?? check.ElectricBill;
                    if(dto.IsDisablePromotion == true)
                    {
                        check.PromotionId = null;
                        check.PromotionPrice = check.Price;
                    }

                    if(check.Price > 0)
                    {
                        var prom = await this.context.Promotion.Where(x => x.PromotionId.Equals(check.PromotionId)).FirstOrDefaultAsync();
                        check.PromotionPrice = check.Price - (check.Price * prom.Amount) / 100;
                    }
                    
                    this.context.Package.Update(check);
                    await this.context.SaveChangesAsync();

                    return check;
                }
                else
                {
                    throw new Exception("không tìm thấy");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Package>> getAllForMobile(int count)
        {
            try
            {
                var check = this.context.Package.Where(x => x.Status)
                    .Include(x => x.PackageProduct)
                        .ThenInclude(x => x.Product)
                            .ThenInclude(x => x.Image)
                    .Include(x => x.Promotion)
                    .Include(x => x.Feedback)
                    .OrderByDescending(x => x.ConstructionContract.Count)
                    .Take(count);
                if (check != null)
                {
                    return (List<Package>)check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
