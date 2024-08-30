using ManageCustomers.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ManageCustomers.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        readonly ICustomerRepo _customerRepo;

        readonly ILogger<DeleteModel> _logger;

        string _customerId;

        [BindProperty]
        public ViewCustomers Customer { get; set; } = new ViewCustomers();
        public bool Error { get; set; }

        public DeleteModel(ICustomerRepo customerRepo, ILogger<DeleteModel> logger)
        {
            _customerRepo = customerRepo;
            _logger = logger;
        }

        public void OnGet(string customerId)
        {
            _customerId = customerId;
            var result = _customerRepo.GetById(customerId);
            if (result is null)
            {
                _logger.LogError("Unable to fetch customer by id: {CustomerId}", customerId);

                return;
            }

            Customer = result;
        }

        public IActionResult OnPostDelete()
        {
            var deleteResult = _customerRepo.Delete(Customer.Id);
            if (!deleteResult.IsSuccessfull)
            {
                Error= true;
                _logger.LogError("Unable to delete customer. Error: {Error}", deleteResult.ErrorMessage);

                return Page();
            }

            Error = false;
            return RedirectToPage("View");
        }
    }
}
