using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cresce.Api.Models;
using Cresce.Core.Authentication;
using Cresce.Core.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Cresce.Api.Controllers.Customers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerServices _service;

        public CustomersController(ICustomerServices service) => _service = service;

        [HttpGet]
        public async Task<IEnumerable<CustomerModel>> GetCustomers([FromHeader] IEmployeeAuthorization authorization)
        {
            return (await _service.GetCustomers(authorization))
                .Select(entity => new CustomerModel(entity));
        }

    }
}
