using System.Threading.Tasks;

namespace Cresce.Core.Appointments
{
    public interface ICreateEntityGateway<in TEntity>
    {
        Task<int> Create(TEntity entity);
    }
}
