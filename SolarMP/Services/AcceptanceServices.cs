using Microsoft.EntityFrameworkCore;
using SolarMP.DTOs.Acceptances;
using SolarMP.Interfaces;
using SolarMP.Models;

namespace SolarMP.Services
{
    public class AcceptanceServices : IAcceptance
    {
        protected readonly solarMPContext context;
        public AcceptanceServices(solarMPContext context)
        {
            this.context = context;
        }
        public async  Task<bool> DeleteAcceptance(string acceptanceId)
        {
            try
            {
                var acceptance = await this.context.Acceptance
                    .Where(x => acceptanceId.Equals(x.AcceptanceId))
                    .FirstOrDefaultAsync();
                if (acceptance != null)
                {
                    acceptance.Status = false;
                    this.context.Acceptance.Update(acceptance);
                    await this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new ArgumentException("No Acceptance found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<List<Acceptance>> GetAcceptanceById(string? acceptanceId)
        {
            try
            {
                var data = await this.context.Acceptance
                    .Where(x => x.Status == true && x.AcceptanceId.Equals(acceptanceId))
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

        public async Task<List<Acceptance>> GetAllAcceptances()
        {
            try
            {
                var data = await this.context.Acceptance.Where(x => x.Status == true)
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<List<Acceptance>> GetAllAcceptancesAD()
        {
            try
            {
                var data = await this.context.Acceptance
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<bool> InsertAcceptance(AcceptanceDTO acceptance)
        {
            try
            {
                var _acceptance = new Acceptance();
                _acceptance.AcceptanceId = "ACPT" + Guid.NewGuid().ToString().Substring(0, 12);
                _acceptance.Rating = acceptance.Rating;
                _acceptance.Feedback = acceptance.Feedback;
                _acceptance.ConstructionContractId = acceptance.ConstructionContractId;
                _acceptance.ImageFile = acceptance.ImageFile;
                _acceptance.Status = true;
                await this.context.Acceptance.AddAsync(_acceptance);
                this.context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }

        public async Task<bool> UpdateAcceptance(AcceptanceUpdateDTO upAcceptance)
        {
            try
            {
                Acceptance _acceptance = await this.context.Acceptance.FirstAsync(x => x.AcceptanceId == upAcceptance.AcceptanceId);
                if (_acceptance != null)
                {
                    _acceptance.Rating = upAcceptance.Rating ?? _acceptance.Rating;
                    _acceptance.Feedback = upAcceptance.Feedback ?? _acceptance.Feedback;
                    _acceptance.ImageFile = upAcceptance.ImageFile ?? _acceptance.ImageFile;
                    _acceptance.Status = upAcceptance.Status ?? _acceptance.Status;
                     context.Acceptance.Update(_acceptance);
                    this.context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new ArgumentException("Acceptance not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Process went wrong");
            }
        }
    }
}
