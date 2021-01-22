using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.GetEntities
{
    internal class GetEntitiesService<TEntity> : IGetEntitiesService<TEntity>
    {
        private readonly IGetEntitiesGateway<TEntity> _gateway;

        public GetEntitiesService(IGetEntitiesGateway<TEntity> gateway) => _gateway = gateway;

        public Task<IEnumerable<TEntity>> GetEntities(IEmployeeAuthorization authorization)
        {
            authorization.EnsureIsValid();
            return _gateway.GetEntities();
        }
    }
}