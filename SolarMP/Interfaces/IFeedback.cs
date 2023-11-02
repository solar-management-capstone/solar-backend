using SolarMP.DTOs.Feedback;
using SolarMP.DTOs.Package;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IFeedback
    {
        Task<Feedback> insert(FeedbackCreateDTO dto);
        Task<List<Feedback>> getAll();
        Task<List<Feedback>> getAllPack(string packageId);
        Task<Feedback> delete(string id);
    }
}
