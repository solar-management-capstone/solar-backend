using SolarMP.DTOs.Process;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IProcess
    {
        Task<Process> insert(ProcessCreateDTO dto);
        Task<List<Process>> getAll();
        Task<List<Process>> getAllContract(string id);
        Task<Process> delete(string id);
        Task<Process> getById(string id);
    }
}
