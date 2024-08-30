using ManageCustomers.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageCustomers.Pages.Customers
{
    public class ViewModel : PageModel
    {
        readonly ICustomerRepo _customerRepo;
        readonly ILogger<ViewModel> _logger;
        public List<ViewCustomers> Customers;
        public bool Error { get; set; }

        public ViewModel(ICustomerRepo customerRepo, ILogger<ViewModel> logger)
        {
            _customerRepo = customerRepo;
            _logger = logger;
        }

        public void OnGet()
        {
            var result = _customerRepo.GetAllCustomers();

            if (result.Count == 0)
            {
                Error = true;
                _logger.LogError("No customer to view");

                return;
            }

            Error = false;
            Customers = result;
        }
    }
}

