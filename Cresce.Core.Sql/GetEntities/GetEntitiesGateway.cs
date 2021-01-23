using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntitiesGateway<TEntityDto, TEntity>
        : IGetEntitiesGateway<TEntity> where TEntityDto : class, IUnwrap<TEntity>
    {
        private readonly CresceContext _context;

        public GetEntitiesGateway(CresceContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetEntities()
        {
            return (await MakeQuery().ToListAsync())
                .Select(e => e.Unwrap());
        }

        private IQueryable<TEntityDto> MakeQuery() => _context.Set<TEntityDto>();
    }
}