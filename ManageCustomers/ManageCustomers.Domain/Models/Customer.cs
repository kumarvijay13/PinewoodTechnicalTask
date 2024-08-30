namespace ManageCustomers.Domain.Models
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public char Gender { get; set; }
        
        public string Address { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Mobile { get; set; } = string.Empty;
       
        public Customer()
        {
            Id = Guid.NewGuid();
        }
    }
}
