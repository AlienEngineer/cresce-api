using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntitiesGateway<TEntityModel, TEntity>
        : IGetEntitiesGateway<TEntity> where TEntityModel : class, IUnwrap<TEntity>
    {
        private readonly CresceContext _context;

        public GetEntitiesGateway(CresceContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            var models = await _context
                .Set<TEntityModel>()
                .ToListAsync();

            return models.Select(e => e.Unwrap());
        }
    }
}
