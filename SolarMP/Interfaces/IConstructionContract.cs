using SolarMP.DTOs.ConstructionContract;
using SolarMP.DTOs.Promotions;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IConstructionContract
    {
        Task<ConstructionContract> GetConstructionContractById(string? constructionContractId);
        Task<List<ConstructionContract>> GetConstructionContractByCusId(string cusId, string? status);
        Task<List<ConstructionContract>> GetConstructionContractByStaffId(string StaffId);
        Task<List<ConstructionContract>> GetAllConstructionContracts();
        List<ConstructionContract> GetAllConstructionContractsByCusIDv2(CusManyStatusDTO dto);
        Task<bool> UpdateConstructionContract(ConstructionContractUpdateDTO upConstructionContract);
        Task<bool> DeleteConstructionContract(string constructionContractId);
        Task<bool> InsertConstructionContract(ConstructionContractDTO constructionContract);
    }
}
