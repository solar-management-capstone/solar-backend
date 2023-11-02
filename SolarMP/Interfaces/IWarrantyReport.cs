using SolarMP.DTOs.Package;
using SolarMP.DTOs.WarrantyReport;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IWarrantyReport
    {
        Task<WarrantyReport> insert(WarrantyReportDTO dto);
        Task<List<WarrantyReport>> getAll();
        Task<List<WarrantyReport>> getAllForAdmin();
        Task<WarrantyReport> delete(string id);
        Task<List<WarrantyReport>> getAllForContract(string id);
        Task<List<WarrantyReport>> getAllForCus(string id);
        Task<bool> insertProduct(ProductWarrantyDTO dto);
    }
}
