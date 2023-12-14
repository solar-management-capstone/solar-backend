using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Bracket;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class BracketServices : IBracket
    {
        protected readonly solarMPContext context;
        public BracketServices(solarMPContext context)
        {
            this.context = context;
        }
        public async Task<bool> DeleteBracket(string bracketId)
        {
            try
            {
                var bracket = await this.context.Bracket
                    .Where(x => bracketId.Equals(x.BracketId))
                    .FirstOrDefaultAsync();
                if (bracket != null)
                {
                    bracket.Status = false;
                    this.context.Bracket.Update(bracket);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Bracket found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Bracket>> GetAllBrackets()
        {
            try
            {
                var data = await this.context.Bracket.Where(x => x.Status)
                    .Include(x => x.Image)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task<List<Bracket>> GetAllBracketsAdmin()
        {
            try
            {
                var data = await this.context.Bracket
                    .Include(x => x.Image)
                    .OrderByDescending(x => x.CreateAt)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Bracket>> GetBracketById(string? bracketId)
        {
            try
            {
                var data = await this.context.Bracket.Where(x => x.Status && x.BracketId.Equals(bracketId))
                    .Include(x => x.Image)
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

        public async Task<Bracket> InsertBracket(BracketDTO bracket)
        {
            try
            {
                var _bracket = new Bracket();
                _bracket.BracketId = "BKT" + Guid.NewGuid().ToString().Substring(0, 13);
                _bracket.Name= bracket.Name;
                _bracket.Price = bracket.Price;
                _bracket.Manufacturer= bracket.Manufacturer;
                _bracket.Size = bracket.Size ?? null;
                _bracket.Material = bracket.Material ?? null;
                _bracket.Status = true;
                _bracket.CreateAt = DateTime.Now;
                await this.context.Bracket.AddAsync(_bracket);
                this.context.SaveChanges();


                if (bracket.image != null && bracket.image.Count > 0)
                {
                    foreach (var image in bracket.image)
                    {
                        var img = new Image();
                        img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                        img.ImageData = image.image;
                        img.BracketId = _bracket.BracketId;
                        await this.context.Image.AddAsync(img);
                        await this.context.SaveChangesAsync();
                    }
                }


                return _bracket;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateBracket(BracketUpdateDTO upBracket)
        {
            try
            {
                Bracket _bracket = await this.context.Bracket
                    .Include(x=>x.ConstructionContract)
                    .FirstAsync(x => x.BracketId == upBracket.BracketId);
                if (_bracket.ConstructionContract.Count > 0)
                {
                    throw new Exception("Khung đỡ đã tồn tại trong hợp đồng, không thể chỉnh sửa");
                }
                if (_bracket != null)
                {
                    _bracket.Name = upBracket.Name ?? _bracket.Name;
                    _bracket.Price = upBracket.Price ?? _bracket.Price;
                    _bracket.Manufacturer = upBracket.Manufacturer ?? _bracket.Manufacturer;
                    _bracket.Status = upBracket.Status ?? _bracket.Status;
                    _bracket.Size = upBracket.Size ?? _bracket.Size;
                    _bracket.Material = upBracket.Material ?? _bracket.Material;
                    context.Bracket.Update(_bracket);
                    this.context.SaveChanges();

                    var imgg = await this.context.Image.Where(x => x.BracketId.Equals(upBracket.BracketId)).ToListAsync();
                    if(imgg != null && imgg.Count>0)
                    {
                        foreach(var img2 in imgg)
                        {
                            this.context.Image.Remove(img2);
                            await this.context.SaveChangesAsync();
                        }
                    }
                    if (upBracket.image != null && upBracket.image.Count > 0)
                    {
                        foreach (var image in upBracket.image)
                        {
                            var img = new Image();
                            img.ImageId = "IMG" + Guid.NewGuid().ToString().Substring(0, 13);
                            img.ImageData = image.image;
                            img.BracketId = _bracket.BracketId;
                            await this.context.Image.AddAsync(img);
                            await this.context.SaveChangesAsync();
                        }
                    }
                    return true;
                }
                else
                {
                    throw new ArgumentException("Bracket not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
