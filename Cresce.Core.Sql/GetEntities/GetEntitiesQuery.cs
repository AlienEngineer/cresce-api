using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntitiesQuery<TEntityDto, TEntity> : IGetEntitiesQuery<TEntityDto, TEntity>
        where TEntityDto : class, IUnwrap<TEntity>
    {
        private readonly CresceContext _context;

        public GetEntitiesQuery(CresceContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetEntities(
            Expression<Func<TEntityDto, bool>> filter = null
        )
        {
            filter ??= model => true;
            var models = await _context
                .Set<TEntityDto>()
                .Where(filter)
                .ToListAsync();

            return models.Select(e => e.Unwrap());
        }
    }
}