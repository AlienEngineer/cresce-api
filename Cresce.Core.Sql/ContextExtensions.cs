using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cresce.Core.Employees.GetEmployees;
using Cresce.Core.Sql.Employees;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql
{
    internal static class ContextExtensions
    {
        public static async Task<IEnumerable<TEntity>> GetEntities<TEntityModel, TEntity>(
            this CresceContext context,
            Expression<Func<TEntityModel, bool>> filter = null
        ) where TEntityModel : class, IUnwrap<TEntity>
        {
            filter ??= model => true;
            var models = await context
                .Set<TEntityModel>()
                .Where(filter)
                .ToListAsync();

            return models.Select(e => e.Unwrap());
        }
    }
}
