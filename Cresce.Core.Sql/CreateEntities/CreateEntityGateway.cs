using System.Threading.Tasks;
using Cresce.Core.Appointments;

namespace Cresce.Core.Sql.CreateEntities
{
    internal class CreateEntityGateway<TEntityModel, TEntity> : ICreateEntityGateway<TEntity>
        where TEntityModel : class, IWrap<TEntity>, new()
    {
        private readonly CresceContext _context;

        public CreateEntityGateway(CresceContext context) => _context = context;

        public Task Create(TEntity entity)
        {
            _context.Set<TEntityModel>().Add(MakeEntityModel(entity));
            return _context.SaveChangesAsync();
        }

        private static TEntityModel MakeEntityModel(TEntity entity)
        {
            var model = new TEntityModel();
            model.Wrap(entity);
            return model;
        }
    }
}