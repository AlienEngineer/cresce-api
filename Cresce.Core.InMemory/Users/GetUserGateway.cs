using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Users;

namespace Cresce.Core.InMemory.Users
{
    internal class GetUserGateway : IGetUserGateway
    {
        private readonly IReadOnlyDictionary<string, object> _dictionary;

        public GetUserGateway(IReadOnlyDictionary<string,object> dictionary)
        {
            _dictionary = dictionary;
        }

        public Task<User> GetUser(string user)
        {
            _dictionary.TryGetValue($"user_{user}", out var userDto);
            return Task.FromResult(userDto as User ?? new BasicUser());
        }
    }
}
