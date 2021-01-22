using System.Threading.Tasks;
using Cresce.Api.Controllers;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Api
{
    public class AuthorizationBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            var scope = context.Services.CreateScope();

            if (context.Metadata.ModelType == typeof(IAuthorization))
            {
                return new UserAuthorizationBinder(scope.ServiceProvider.GetService<IAuthorizationFactory>()!);
            }

            if (context.Metadata.ModelType == typeof(IEmployeeAuthorization))
            {
                return new EmployeeAuthorizationBinder(scope.ServiceProvider.GetService<IAuthorizationFactory>()!);
            }

            return null;
        }

        private class UserAuthorizationBinder : IModelBinder
        {
            private readonly IAuthorizationFactory _authorizationFactory;

            public UserAuthorizationBinder(IAuthorizationFactory authorizationFactory) =>
                _authorizationFactory = authorizationFactory;

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    bindingContext.HttpContext.Request.GetAuthorization(_authorizationFactory)
                );

                return Task.CompletedTask;
            }
        }

        private class EmployeeAuthorizationBinder : IModelBinder
        {
            private readonly IAuthorizationFactory _authorizationFactory;

            public EmployeeAuthorizationBinder(IAuthorizationFactory authorizationFactory) =>
                _authorizationFactory = authorizationFactory;

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    bindingContext.HttpContext.Request.GetEmployeeAuthorization(_authorizationFactory)
                );

                return Task.CompletedTask;
            }
        }
    }
}