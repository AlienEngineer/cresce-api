using Cresce.Core.Authentication;
using Cresce.Core.Employees;
using Cresce.Core.InMemory;
using Cresce.Core.Users;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cresce.Core.Tests
{
    public class ServicesTests<T>
    {
        private ServiceProvider _provider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            GatewaysConfiguration.RegisterServices(serviceCollection);
            ServicesConfiguration.RegisterServices(serviceCollection);
            _provider = serviceCollection.BuildServiceProvider();
        }

        protected Image GetSampleImage() => GatewaysConfiguration.GetSampleImage();

        protected T MakeService()
        {
            return GetService<T>();
        }

        protected AuthorizedUser GetAuthorizedUser()
        {
            return GetService<ITokenFactory>().MakeToken(new BasicUser
            {
                Id = "myUser"
            });
        }

        protected AuthorizedUser GetUnknownUser()
        {
            return GetService<ITokenFactory>().MakeToken(new BasicUser
            {
                Id = "some unknown user"
            });
        }

        protected AuthorizedUser GetExpiredUser()
        {
            return GetService<ITokenFactory>().MakeInvalidToken();
        }

        private TService GetService<TService>()
        {
            return _provider.GetService<TService>()!;
        }
    }
}
