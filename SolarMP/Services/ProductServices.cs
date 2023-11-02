using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Product;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class ProductServices : IProduct
    {
        protected readonly solarMPContext context;
        public ProductServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<Product> delete(string id)
        {
            try
            {
                var check = await this.context.Product.Where(x => x.ProductId.Equals(id)).FirstOrDefaultAsync();
                if (check != null)
                {
                    check.Status = false;
                    this.context.Product.Update(check);
                    await this.context.SaveChangesAsync();

                    var proPck = await this.context.PackageProduct.Where(x=>x.ProductId.Equals(id)).ToListAsync();
                    foreach(var p in proPck)
                    {
                        p.Status = false;
                        this.context.PackageProduct.Update(p);
                        await this.context.SaveChangesAsync();
                    }
                    return check;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> getAll()
        {
            try
            {
                var check = await this.context.Product.Where(x => x.Status)
                    .Include(x => x.Image)
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

        public async Task<List<Product>> getAllForAdmin()
        {
            try
            {
                var check = await this.context.Product
                    .Include(x => x.Image)
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

        public async Task<Product> getById(string id)
        {
            try
            {
                var check = await this.context.Product.Where(x => x.ProductId.Equals(id))
                    .Include(x => x.Image)
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

        public async Task<List<Product>> getByName(string name)
        {
            try
            {
                var check = await this.context.Product.Where(x => x.Status && x.Name.Contains(name))
                    .Include(x => x.Image)
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

        public async Task<Product> insert(ProductCreateDTO dto)
        {
            try
            {
                var product = new Product();
                product.ProductId = "PRO" + Guid.NewGuid().ToString().Substring(0,13);
                product.Name = dto.Name;
                product.Price = dto.Price;
                product.Manufacturer = dto.Manufacturer;
                product.Feature = dto.Feature;
                product.WarrantyDate = dto.WarrantyDate;
                product.Status = true;

                await this.context.Product.AddAsync(product);
                await this.context.SaveChangesAsync();

                if(dto.image != null && dto.image.Count>0)
                {
                    foreach(var image in dto.image)
                    {
                        var img = new Image();
                        img.ImageId = "IMG"+Guid.NewGuid().ToString().Substring(0,13);
                        img.ImageData = image.image;
                        img.ProductId = product.ProductId;
                        await this.context.Image.AddAsync(img);
                        await this.context.SaveChangesAsync();
                    }
                }
                return product;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> update(ProductUpdateDTO dto)
        {
            try
            {
                var check = await this.context.Product.Where(x => x.ProductId.Equals(dto.ProductId)).FirstOrDefaultAsync();
                if (check != null)
                {
                    var temp = check.Price;
                    check.Name = dto.Name ?? check.Name;
                    check.Price = dto.Price ?? check.Price;
                    check.Feature = dto.Feature ?? check.Feature;
                    check.Manufacturer = dto.Manufacturer ?? check.Manufacturer;
                    check.WarrantyDate= dto.WarrantyDate ?? check.WarrantyDate;
                    check.Status = dto.Status ?? check.Status;
                    
                    this.context.Product.Update(check);
                    await this.context.SaveChangesAsync();

                    var pckPro = await this.context.PackageProduct.Where(x=>x.ProductId.Equals(dto.ProductId)).ToListAsync();
                    if (pckPro != null)
                    {
                        foreach(var p in pckPro)
                        {
                            if(temp == check.Price)
                            {
                                break;
                            }
                            var pck = await this.context.Package.Where(x=>x.PackageId.Equals(p.PackageId)).FirstOrDefaultAsync();
                            pck.Price -= temp * p.Quantity;
                            pck.Price += dto.Price * p.Quantity;
                            this.context.Package.Update(pck);
                            await this.context.SaveChangesAsync();
                        }
                    }
                    var imgg = await this.context.Image.Where(x => x.ProductId.Equals(dto.ProductId)).ToListAsync();
                    if (imgg != null && imgg.Count > 0)
                    {
                        foreach (var img2 in imgg)
                        {
                            this.context.Image.Remove(img2);
                            await this.context.SaveChangesAsync();
                        }
                    }
                    if (dto.image != null && dto.image.Count > 0)
                    {
                        foreach (var image in dto.image)
                        {
                            var img = new Image();
                            img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                            img.ImageData = image.image;
                            img.ProductId = dto.ProductId;
                            await this.context.Image.AddAsync(img);
                            await this.context.SaveChangesAsync();
                        }
                    }
                    return check;
                }
                else
                {
                    throw new Exception("Không tìm thấy product");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
