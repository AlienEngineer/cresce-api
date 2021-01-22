using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cresce.Core
{
    public interface IGetEntitiesGateway<TEntity>
    {
        Task<IEnumerable<TEntity>> GetEntities();
    }
}