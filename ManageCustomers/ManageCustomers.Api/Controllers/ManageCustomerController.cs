using ManageCustomers.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ManageCustomers.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManageCustomerController : Controller
    {
        readonly ICustomerRepo _customerRepo;
        readonly ILogger<ManageCustomerController> _logger;

        public ManageCustomerController(ICustomerRepo customerRepo, ILogger<ManageCustomerController> logger)
        {
            _customerRepo = customerRepo;
            _logger = logger;
        }

        [HttpPost("Add")]
        public IActionResult AddCustomer(AddNewCustomer customer)
        {
            
            if(customer is null)
            {
                return BadRequest(new { error = "Customer is null" });
            }

            var response = _customerRepo.AddCustomer(customer);

            if (!response.IsSuccessfull)
            {
                _logger.LogError("Unable to add customer. Error: {Error}", response.ErrorMessage);

                return new StatusCodeResult(500);
            }

            return Ok(new { message = "Customer added successfully" });
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetCustomer()
        {
           
            var getResult = _customerRepo.GetAllCustomers();

            if (getResult.Count == 0 )
            {
                _logger.LogError("No customer found");
            }

            return Ok(getResult);
        }


        [HttpPut("Edit")]
        public IActionResult Edit(UpdateCustomer updateCustomer)
        {
           
            if(updateCustomer is null)
            {
                return BadRequest(new { error = "Customer is null" });
            }

            var updateResult = _customerRepo.Update(updateCustomer);

            if (!updateResult.IsSuccessfull)
            {
                _logger.LogError("Unable to update customer");
                return new StatusCodeResult(500);
            }

            return Ok();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                return BadRequest(new { error = "customerId is null" });
            }

            if (!Guid.TryParse(customerId, out _))
            {
                return BadRequest(new { error = "Invalid customerId" });
            }

            var deleteResult = _customerRepo.Delete(customerId);

            if (!deleteResult.IsSuccessfull)
            {
                _logger.LogError("Unable to delete customer");

                return new StatusCodeResult(500);
            }

            return Ok();
        }

    }
}
