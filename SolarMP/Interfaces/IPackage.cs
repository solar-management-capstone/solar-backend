using SolarMP.DTOs.Package;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IPackage
    {
        Task<Package> insert(PackageCreateDTO dto);
        Task<List<Package>> getAll();
        Task<List<Package>> getAllForAdmin();
        Task<Package> delete(string id);
        Task<List<Package>> getByName(string name);
        Task<Package> getById(string id);
        Task<bool> insertProduct(PackageProductCreateDTO dto);
        Task<Package> update(PackageUpdateDTO dto);
        Task<List<Package>> SortPck(int? area, decimal? bill);
    }
}
