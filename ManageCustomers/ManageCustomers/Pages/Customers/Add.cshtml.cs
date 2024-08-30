using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ManageCustomers.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace ManageCustomers.Pages.Customers
{
    public class AddModel : PageModel
    {
        private readonly ICustomerRepo _customerRepo;

        readonly ILogger<AddModel> _logger;

        [BindProperty]
        public AddNewCustomer AddNewCustomer { get; set; } = new AddNewCustomer();

        [BindProperty]
        public string Gender { get; set; }
        public char[] Genders = new[] { 'M', 'F' };

        [BindProperty]
        public bool Errors { get; set; }

        public AddModel(ICustomerRepo customerRepo, ILogger<AddModel> logger)
        {
            _customerRepo = customerRepo;
            _logger=logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            ModelState.Clear();
            var validationState = Validate();

            if (!validationState.IsValid)
            {
                foreach (var vs in validationState)
                {
                    foreach (var error in vs.Value.Errors)
                    {
                        ModelState.AddModelError(vs.Key, error.ErrorMessage);
                    }
                }

                return Page();
            }

            var addResult = _customerRepo.AddCustomer(AddNewCustomer);
            if (!addResult.IsSuccessfull)
            {
                Errors = true;
                _logger.LogError("Unable to add customer. Error: {Error}", addResult.ErrorMessage);

                return Page();
            }
            return RedirectToPage("View");
        }

        public ModelStateDictionary Validate()
        {
            var result = new ModelStateDictionary();

            if (string.IsNullOrWhiteSpace(AddNewCustomer.FirstName))
            {
                result.AddModelError("AddNewCustomer.FirstName", "FirstName field is required");
            }

            if (string.IsNullOrWhiteSpace(AddNewCustomer.LastName))
            {
                result.AddModelError("AddNewCustomer.LastName", "LastName field is required");
            }

            if (string.IsNullOrWhiteSpace(AddNewCustomer.Address))
            {
                result.AddModelError("AddNewCustomer.Address", "Address field is required");
            }

            if (string.IsNullOrWhiteSpace(AddNewCustomer.Mobile))
            {
                result.AddModelError("AddNewCustomer.Mobile", "Mobile is required.");
            }
            return result;
        }
    }
}
