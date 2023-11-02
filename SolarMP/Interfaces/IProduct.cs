using SolarMP.DTOs.Package;
using SolarMP.DTOs.Product;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IProduct
    {
        Task<Product> insert(ProductCreateDTO dto);
        Task<List<Product>> getAll();
        Task<List<Product>> getAllForAdmin();
        Task<Product> delete(string id);
        Task<List<Product>> getByName(string name);
        Task<Product> getById(string id);
        Task<Product> update(ProductUpdateDTO dto);
    }
}
