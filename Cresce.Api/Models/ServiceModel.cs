using Cresce.Core.Services;

namespace Cresce.Api.Models
{
    public record ServiceModel
    {
        public ServiceModel(Service service)
        {
            Id = service.Id;
            Name = service.Name;
            Value = service.Value;
            Image = service.Image.ToBase64();
        }

        public ServiceModel()
        {
        }

        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public double Value { get; init; } = 0.0;
        public string Image { get; init; } = string.Empty;
    }
}