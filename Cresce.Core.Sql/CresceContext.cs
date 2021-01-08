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
            modelBuilder.Entity<UserModel>();
            modelBuilder.Entity<OrganizationModel>();
            modelBuilder.Entity<EmployeeModel>();
        }
    }
}
