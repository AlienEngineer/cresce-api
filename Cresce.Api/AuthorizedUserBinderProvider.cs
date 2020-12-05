using System.Threading.Tasks;
using Cresce.Api.Controllers;
using Cresce.Core.Authentication;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Cresce.Api
{
    public class AuthorizedUserBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(AuthorizedUser)
                ? new AuthorizedUserBinder(context.Services.GetService<ITokenFactory>())
                : null;
        }

        private class AuthorizedUserBinder : IModelBinder
        {
            private readonly ITokenFactory _tokenFactory;

            public AuthorizedUserBinder(ITokenFactory tokenFactory)
            {
                _tokenFactory = tokenFactory;
            }

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    bindingContext.HttpContext.Request.GetUser(_tokenFactory)
                );

                return Task.CompletedTask;
            }
        }
    }
}
