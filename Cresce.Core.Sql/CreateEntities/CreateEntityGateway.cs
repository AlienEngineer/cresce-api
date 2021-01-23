using System.Threading.Tasks;
using Cresce.Core.Appointments;

namespace Cresce.Core.Sql.CreateEntities
{
    internal class CreateEntityGateway<TEntityDto, TEntity> : ICreateEntityGateway<TEntity>
        where TEntityDto : class, IWrap<TEntity>, IHaveAutoIdentity, new()
    {
        private readonly CresceContext _context;

        public CreateEntityGateway(CresceContext context) => _context = context;

        public async Task<int> Create(TEntity entity)
        {
            var entityDto = MakeEntityDto(entity);
            await _context.Set<TEntityDto>().AddAsync(entityDto);
            await _context.SaveChangesAsync();

            return entityDto.Id;
        }

        private static TEntityDto MakeEntityDto(TEntity entity)
        {
            var model = new TEntityDto();
            model.Wrap(entity);
            return model;
        }
    }
}