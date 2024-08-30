using ManageCustomers.Domain.Models;

namespace ManageCustomers.Domain.Models
{
    public interface ICustomerRepo
    {
        public List<ViewCustomers> GetAllCustomers();

        public ViewCustomers GetById(string customerId);

        public EfQueryResult AddCustomer(AddNewCustomer newCustomer);

        public EfQueryResult Update(UpdateCustomer updateCustomer);

        public EfQueryResult  Delete(string customerId);

    }
}
