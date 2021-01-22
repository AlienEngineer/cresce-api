using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cresce.Core.Sql.GetEntities
{
    internal interface IGetEntitiesQuery<TEntityModel, TEntity>
        where TEntityModel : class, IUnwrap<TEntity>
    {
        Task<IEnumerable<TEntity>> GetEntities(
            Expression<Func<TEntityModel, bool>> filter = null
        );
    }
}