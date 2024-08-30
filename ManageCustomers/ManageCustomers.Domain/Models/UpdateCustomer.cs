using System.ComponentModel.DataAnnotations;
namespace ManageCustomers.Domain.Models
{
    public class UpdateCustomer
    {
        [Required]
        public string ExistingId { get; set; } = string.Empty;

        public char Gender { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

    }
}

