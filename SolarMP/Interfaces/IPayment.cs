using SolarMP.DTOs.Payment;
using SolarMP.Models;

namespace SolarMP.Interfaces
{
    public interface IPayment
    {
        Task<PaymentProcess> insert(PaymentDTO dto);
        Task<List<PaymentProcess>> getAll();
        Task<List<PaymentProcess>> getAllUser(string id);
        Task<List<PaymentProcess>> getAllContract(string id);
    }
}
