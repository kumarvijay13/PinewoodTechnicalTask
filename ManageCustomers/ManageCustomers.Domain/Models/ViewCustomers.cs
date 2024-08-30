using System.Reflection;

namespace ManageCustomers.Domain.Models
{
    public class ViewCustomers
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public char Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
