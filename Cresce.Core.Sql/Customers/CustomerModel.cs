using Cresce.Core.Customers.GetCustomers;

namespace Cresce.Core.Sql.Services
{
    internal class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public Customer ToCustomer()
        {
            return new Customer
            {
                Id = Id,
                Name = Name,
                Image = new Image(Image),
            };
        }
    }
}
