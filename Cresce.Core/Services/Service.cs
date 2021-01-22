namespace Cresce.Core.Services
{
    public record Service
    {
        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public double Value { get; init; } = 0.0;
        public Image Image { get; init; } = new();
    }
}