using ManageCustomers.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ManageCustomers.Pages.Customers
{
    public class EditModel : PageModel
    {
        readonly ICustomerRepo _customerRepo;
        readonly ILogger<EditModel> _logger;

        [BindProperty]
        public ViewCustomers Customer { get; set; } = new ViewCustomers();

        [BindProperty]
        public UpdateCustomer UpdateCustomer { get; set; } = new UpdateCustomer();

        [BindProperty]
        public string Gender { get; set; }
        public char[] Genders = new[] { 'M', 'F' };

        public bool ErrorUpdatingCustomer { get; set; }

        public EditModel(ICustomerRepo customerRepo, ILogger<EditModel> logger)
        {
            _customerRepo = customerRepo;
            _logger = logger;
        }

        public void OnGet(string customerId)
        {
            var result = _customerRepo.GetById(customerId);
            if (result is null)
            {
                _logger.LogError("Not able to fetch customer by id: {CustomerId}", customerId);

                return;
            }

            Customer = result;
            if (Customer is not null)
            {
                UpdateCustomer.ExistingId = Customer.Id;
                UpdateCustomer.Gender = Customer.Gender;
                UpdateCustomer.Address = Customer.Address;
                UpdateCustomer.Mobile = Customer.Mobile;
            }
        }

        public IActionResult OnPost(UpdateCustomer updateCustomer)
        {
            ModelState.Clear();
            var validationState = Validate();

            if (!validationState.IsValid)
            {
                foreach (var state in validationState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        ModelState.AddModelError(state.Key, error.ErrorMessage);
                    }
                }

                return Page();
            }

            var updateResult = _customerRepo.Update(updateCustomer);
            if (!updateResult.IsSuccessfull)
            {
                ErrorUpdatingCustomer = true;
                _logger.LogError("Unable to update customer: {UpdateCustomer}", JsonSerializer.Serialize(UpdateCustomer));

                return Page();
            }

            ErrorUpdatingCustomer = false;
            return RedirectToPage("View");
        }

        public ModelStateDictionary Validate()
        {
            var result = new ModelStateDictionary();

            if (string.IsNullOrWhiteSpace(UpdateCustomer.Address))
            {
                result.AddModelError("UpdateCustomer.Address", "Address field is required");
            }

            if (string.IsNullOrWhiteSpace(UpdateCustomer.Mobile))
            {
                result.AddModelError("UpdateCustomer.Mobile", "Mobile field is required");
            }
            return result;
        }

    }
}
