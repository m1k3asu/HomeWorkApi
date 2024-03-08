using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using HomeWorkApi.Models;
using ProjectArkansasAde.Data;

namespace TakeHomeTest.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class HomeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public HomeController(DataContext context)
        {
            _dataContext = context;
        }


        //[HttpGet]
        //[Route("GetCustomers")]

        //// Using Action Result here insead of the IActionResult Inface so I can see the results in the swagger UI

        //// Lets try not to use this call. Too many records in the database
        //public async Task<ActionResult<List<Customer>>> GetCustomers()
        //{
        //    var toReturn = await _dataContext.Customer.ToListAsync();
        //    return Ok(toReturn); ;
        //}
        [HttpGet]
        [Route("GetCustomersByFilter")]

        public async Task<ActionResult<List<CustomerDto>>> GetEmployeesByFilter(string? fname, string? lname, string? phone, string? city)
        {

            var returnDto = new List<CustomerDto>();



            // First Let Create a base query where we can append the where clause. Lets start by selecting
            // where ID is not null. Id will never be null, but we need to start somewhere.


            var customerQuery = _dataContext.Customers.Where(c => c.Id != null);

            if (!string.IsNullOrWhiteSpace(fname))
            {
                customerQuery = customerQuery.Where(w => w.FirstName.ToLower().Replace("\"", "").Contains(fname.ToLower()));

            }

            if (!string.IsNullOrWhiteSpace(lname))
            {
                customerQuery = customerQuery.Where(w => w.LastName.ToLower().Contains(lname.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                customerQuery = customerQuery.Where(w => w.Phone.ToLower() == phone.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(city))
            {
                customerQuery = customerQuery.Where(w => w.City.ToLower() == city.ToLower());
            }

            var customer = await customerQuery.ToListAsync();

            // It is never a good idea to expose our internal objects so lets return a dto
            customer.ForEach(currentCustomer =>
            {
                var customerDto = new CustomerDto()
                {
                    Id = currentCustomer.Id,
                    Phone = currentCustomer.Phone,
                    City = currentCustomer.City,
                    State = currentCustomer.State,
                    Street = currentCustomer.Street,
                    DateOfBirth = currentCustomer.DateOfBirth,
                    FirstName = currentCustomer.FirstName,
                    LastName = currentCustomer.LastName,
                    Zip = currentCustomer.Zip,
                };
                returnDto.Add(customerDto);
            });

            return Ok(returnDto);
        }


    }
}