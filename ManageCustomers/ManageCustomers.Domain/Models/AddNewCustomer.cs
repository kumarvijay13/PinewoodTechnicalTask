using System.Text.RegularExpressions;

namespace ManageCustomers.Domain.Models
{
    public class AddNewCustomer
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public char Gender { get; set; }

        public string Address { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

    }
}
