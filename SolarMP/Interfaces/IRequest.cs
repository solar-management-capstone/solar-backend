using SolarMP.DTOs.Request;
using SolarMP.Models;
using System.Net;

namespace SolarMP.Interfaces
{
    public interface IRequest
    {
        Task<Request> insert(RequestCreateDTO dto);
        Task<List<Request>> getAll();
        Task<List<Request>> getAllForUser(string id);
        Task<List<Request>> getAllForStaff(string id);
        Task<List<Request>> getAllForPackage(string id);
        Task<Request> assignStaff(RequestUpdateDTO dto);

    }
}
