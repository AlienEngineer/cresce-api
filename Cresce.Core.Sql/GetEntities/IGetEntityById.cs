using System.Threading.Tasks;

namespace Cresce.Core.Sql.GetEntities
{
    internal interface IGetEntityById<TEntity>
    {
        Task<TEntity> GetById(params object[] keyValues);
    }
}