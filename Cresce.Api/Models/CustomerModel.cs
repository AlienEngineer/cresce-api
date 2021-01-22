using Cresce.Core.Customers;

namespace Cresce.Api.Models
{
    public record CustomerModel
    {
        public CustomerModel(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Image = customer.Image.ToBase64();
        }

        public CustomerModel()
        {
        }

        public int Id { get; init; } = -1;
        public string Name { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
    }
}