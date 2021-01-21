using System.Collections.Generic;
using System.Threading.Tasks;
using Cresce.Core.Authentication;

namespace Cresce.Core.GetEntities
{
    public interface IGetEntitiesService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetEntities(IEmployeeAuthorization authorization);
    }
}