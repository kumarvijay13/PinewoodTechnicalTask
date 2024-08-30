using ManageCustomers.Domain.Models;
using System.Text.Json;

namespace ManageCustomers.Domain.Models
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CustomerDbContext _context;
        public CustomerRepo(CustomerDbContext context)
        {
            _context = context;
        }

        public EfQueryResult AddCustomer(AddNewCustomer customer)
        {
            try
            {
                if (customer!= null)
                {
                    Customer addNewCustomer = new Customer
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        DOB = customer.DOB,
                        Address = customer.Address,
                        Mobile = customer.Mobile,
                        Gender = customer.Gender
                    };

                    _context.Customers.Add(addNewCustomer);
                    _context.SaveChanges();

                    return new EfQueryResult
                    {
                        IsSuccessfull = true,
                        ErrorMessage = string.Empty
                    };
                }
                else
                {
                    return new EfQueryResult
                    {
                        IsSuccessfull = false,
                        ErrorMessage = "Can not add null customer"
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ViewCustomers> GetAllCustomers()
        {
            try
            {
                var customers = _context
                                .Customers
                                .Select(customer => new ViewCustomers
                                {
                                    Id = customer.Id.ToString(),
                                    FirstName = customer.FirstName,
                                    LastName = customer.LastName,
                                    DOB = customer.DOB,
                                    Address = customer.Address,
                                    Gender = customer.Gender,
                                    Mobile = customer.Mobile,
                                })
                                .ToList();

                return customers;
            }
            catch (Exception ex)
            {

                throw new Exception("Exception occured while executing GetCustomers.", ex);
            }
        }

        public EfQueryResult Update(UpdateCustomer customer)
        {
            try
            {
                if (customer is null)
                {
                    return new EfQueryResult
                    {
                        IsSuccessfull = false,
                        ErrorMessage = "UpdateCustomer is null.",
                    };
                }

                var customerFromDb = _context.Customers.FirstOrDefault(c => c.Id == Guid.Parse(customer.ExistingId));
                if (customerFromDb is null)
                {
                    return new EfQueryResult
                    {
                        IsSuccessfull = false,
                        ErrorMessage = $"Customer with Id:{customer.ExistingId} is not found in the DB.",
                    };
                }

                customerFromDb.Address = customer.Address;
                customerFromDb.Mobile = customer.Mobile;
                customerFromDb.Gender = customer.Gender;

                _context.Customers.Update(customerFromDb);
                _context.SaveChanges();

                return new EfQueryResult
                {
                    IsSuccessfull = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new EfQueryResult
                {
                    IsSuccessfull = false,
                    ErrorMessage = $"Exception encountered while executing {nameof(Update)} for existing customer: {JsonSerializer.Serialize(customer)}",
                };
            }
        }

        public EfQueryResult Delete(string customerId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerId))
                {
                    return new EfQueryResult
                    {
                        IsSuccessfull = false,
                        ErrorMessage = "Customer Id is empty/null.",
                    };
                }

                var id = Guid.Parse(customerId);
                var customerFromDb = _context.Customers.FirstOrDefault(c => c.Id == id);

                if (customerFromDb is null)
                {
                    return new EfQueryResult
                    {
                        IsSuccessfull = false,
                        ErrorMessage = $"Customer with Id:{customerId} is not found in the DB.",
                    };
                }

                _context.Customers.Remove(customerFromDb);
                _context.SaveChanges();

                return new EfQueryResult
                {
                    IsSuccessfull = true,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                return new EfQueryResult
                {
                    IsSuccessfull = false,
                    ErrorMessage = $"Exception encountered while executing {nameof(Delete)} for customer id: {customerId}",
                };
            }
        }

        public ViewCustomers GetById(string customerId)
        {
            try
            {

                var customer = _context.Customers.FirstOrDefault(c => c.Id == Guid.Parse(customerId));

                if (customer is null)
                    throw new Exception($"Customer with Id:{customerId} is not found in the DB.");

                return new ViewCustomers
                {

                    Id = customer.Id.ToString(),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DOB = customer.DOB,
                    Address = customer.Address,
                    Gender = customer.Gender,
                    Mobile = customer.Mobile,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured while executing GetCustomerById.", ex);
            }
        }

    }
}
