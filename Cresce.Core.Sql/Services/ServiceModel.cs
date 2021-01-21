using Cresce.Core.Services;

namespace Cresce.Core.Sql.Services
{
    internal class ServiceModel : IUnwrap<Service>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public byte[] Image { get; set; }

        public Service Unwrap()
        {
            return new Service
            {
                Id = Id,
                Name = Name,
                Image = new Image(Image),
                Value = Value
            };
        }
    }
}
