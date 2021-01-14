using System.Threading.Tasks;
using Cresce.Api.Controllers;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Api
{
    public class AuthorizedUserBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            var scope = context.Services.CreateScope();

            return context.Metadata.ModelType == typeof(IAuthorization)
                ? new AuthorizedUserBinder(scope.ServiceProvider.GetService<IAuthorizationFactory>()!)
                : null;
        }

        private class AuthorizedUserBinder : IModelBinder
        {
            private readonly IAuthorizationFactory _authorizationFactory;

            public AuthorizedUserBinder(IAuthorizationFactory authorizationFactory)
            {
                _authorizationFactory = authorizationFactory;
            }

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    bindingContext.HttpContext.Request.GetUser(_authorizationFactory)
                );

                return Task.CompletedTask;
            }
        }
    }
}
