using Cresce.Api.Models;

namespace Cresce.Api.Tests.Controllers.EmployeeScope
{
    public class ServiceRequests: ControllerRequests<ServiceModel>
    {
        public override string EntitiesUrl => "api/v1/services";

        public override ServiceModel[] GetExpectedList()
        {
            return new []
            {
                new ServiceModel
                {
                    Id = 1,
                    Name = "Development",
                    Value = 30,
                    Image = Context.GetSampleImage().ToBase64()
                }
            };
        }
    }
}
