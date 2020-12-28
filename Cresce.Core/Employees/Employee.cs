namespace Cresce.Core.Employees
{
    public record Employee
    {
        public string Name { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public Image Image { get; init; } = new ();
    }
}

