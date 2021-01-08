using Cresce.Core.Sql.Employees;
using Cresce.Core.Sql.Organizations;
using Cresce.Core.Sql.Users;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql
{
    public class CresceContext : DbContext
    {
        public CresceContext(DbContextOptions<CresceContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<OrganizationModel>().ToTable("Organizations");
            modelBuilder.Entity<EmployeeModel>().ToTable("Employees");
        }
    }
}
