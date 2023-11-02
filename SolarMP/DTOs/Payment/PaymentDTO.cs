
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SolarMP.DTOs.Payment
{
    public class PaymentDTO
    {
        public decimal Amount { get; set; }
        public string ConstructionContractId { get; set; }
        public bool IsDeposit { get; set; }
        public string AccountId { get; set; }
    }
}
