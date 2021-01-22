using Cresce.Api.Models;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    public class CustomerRequests : ControllerRequests<CustomerModel>
    {
        public override string EntitiesUrl => "api/v1/customers";

        public override CustomerModel[] GetExpectedList()
        {
            return new[]
            {
                new CustomerModel
                {
                    Id = 1,
                    Name = "Diogo Quintas",
                    Image = Context.GetSampleImage().ToBase64()
                }
            };
        }
    }
}