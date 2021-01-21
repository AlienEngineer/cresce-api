
namespace Cresce.Core.Customers
{
    public record Customer
    {
        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public Image Image { get; init; }= new ();
    }
}
