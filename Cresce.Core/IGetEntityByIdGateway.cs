using System.Threading.Tasks;

namespace Cresce.Core
{
    public interface IGetEntityByIdGateway<TEntity>
    {
        Task<TEntity> GetById(params object[] keyValues);
    }
}