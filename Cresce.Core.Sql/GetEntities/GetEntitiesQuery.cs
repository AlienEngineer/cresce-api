using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql.GetEntities
{
    internal class GetEntitiesQuery<TEntityModel, TEntity> : IGetEntitiesQuery<TEntityModel, TEntity> where TEntityModel : class, IUnwrap<TEntity>
    {
        private readonly CresceContext _context;

        public GetEntitiesQuery(CresceContext context) => _context = context;

        public async Task<IEnumerable<TEntity>> GetEntities(
            Expression<Func<TEntityModel, bool>> filter = null
        )
        {
            filter ??= model => true;
            var models = await _context
                .Set<TEntityModel>()
                .Where(filter)
                .ToListAsync();

            return models.Select(e => e.Unwrap());
        }
    }
}
