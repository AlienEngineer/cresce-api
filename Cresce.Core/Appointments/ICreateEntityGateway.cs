using System.Threading.Tasks;

namespace Cresce.Core.Appointments
{
    public interface ICreateEntityGateway<in TEntity>
    {
        Task Create(TEntity entity);
    }
}