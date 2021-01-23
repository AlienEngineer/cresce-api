using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cresce.Core.Sql.GetEntities
{
    internal interface IGetEntitiesQuery<TEntityDto, TEntity>
        where TEntityDto : class, IUnwrap<TEntity>
    {
        Task<IEnumerable<TEntity>> GetEntities(
            Expression<Func<TEntityDto, bool>> filter = null
        );
    }
}