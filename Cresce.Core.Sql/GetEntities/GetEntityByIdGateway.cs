using System.Threading.Tasks;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntityByIdGateway<TEntityModel, TEntity> : IGetEntityByIdGateway<TEntity>
        where TEntityModel : class, IUnwrap<TEntity>, new()
    {
        private readonly CresceContext _context;

        public GetEntityByIdGateway(CresceContext context) => _context = context;

        public async Task<TEntity> GetById(params object[] keyValues)
        {
            var model = await _context.Set<TEntityModel>().FindAsync(keyValues) ?? new TEntityModel();
            return model.Unwrap();
        }
    }
}