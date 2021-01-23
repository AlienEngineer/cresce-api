using System.Threading.Tasks;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntityByIdGateway<TEntityDto, TEntity> : IGetEntityByIdGateway<TEntity>
        where TEntityDto : class, IUnwrap<TEntity>, new()
    {
        private readonly CresceContext _context;

        public GetEntityByIdGateway(CresceContext context) => _context = context;

        public async Task<TEntity> GetById(params object[] keyValues)
        {
            var model = await _context.Set<TEntityDto>().FindAsync(keyValues) ?? new TEntityDto();
            return model.Unwrap();
        }
    }
}
