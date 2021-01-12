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

            return context.Metadata.ModelType == typeof(AuthorizedUser)
                ? new AuthorizedUserBinder(scope.ServiceProvider.GetService<IAuthorizedUserFactory>()!)
                : null;
        }

        private class AuthorizedUserBinder : IModelBinder
        {
            private readonly IAuthorizedUserFactory _authorizedUserFactory;

            public AuthorizedUserBinder(IAuthorizedUserFactory authorizedUserFactory)
            {
                _authorizedUserFactory = authorizedUserFactory;
            }

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    bindingContext.HttpContext.Request.GetUser(_authorizedUserFactory)
                );

                return Task.CompletedTask;
            }
        }
    }
}
