using System;
using Cresce.Core.Sql.Appointments;
using Cresce.Core.Sql.Customers;
using Cresce.Core.Sql.Employees;
using Cresce.Core.Sql.Organizations;
using Cresce.Core.Sql.Services;
using Cresce.Core.Sql.Users;
using Microsoft.EntityFrameworkCore;

namespace Cresce.Core.Sql
{
    public class CresceContext : DbContext
    {
        public CresceContext(DbContextOptions<CresceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>().ToTable("Users");
            modelBuilder.Entity<OrganizationDto>().ToTable("Organizations");
            modelBuilder.Entity<EmployeeDto>().ToTable("Employees");
            modelBuilder.Entity<ServiceDto>().ToTable("Services");
            modelBuilder.Entity<CustomerDto>().ToTable("Patients");
            modelBuilder.Entity<AppointmentDto>().ToTable("Appointments");
        }

        public void DeleteDatabase() => Database.EnsureDeleted();

        public void Seed()
        {
            Database.EnsureCreated();

            Add(new UserDto {Id = "myUser", Password = "myPass"});
            Add(new OrganizationDto {Id = "myOrganization", UserId = "myUser"});
            Add(new EmployeeDto
            {
                Name = "Ricardo Nunes",
                Title = "Engineer",
                Image = new Image(GetSampleImage()).ToByteArray(),
                OrganizationId = "myOrganization",
                Pin = "1234"
            });
            Add(new ServiceDto
            {
                Name = "Development",
                Image = new Image(GetSampleImage()).ToByteArray(),
                Value = 30.0,
            });
            Add(new CustomerDto
            {
                Name = "Diogo Quintas",
                Image = new Image(GetSampleImage()).ToByteArray(),
            });
            Add(new AppointmentDto
            {
                Discount = 10.0,
                Hours = 3.5,
                Value = 30.0,
                CustomerId = 1,
                EmployeeId = 1,
                ServiceId = 1,
                StartedAt = new DateTime(2020, 02, 10)
            });

            SaveChanges();
        }

        public string GetSampleImage() =>
            "/9j/4AAQSkZJRgABAQEASABIAAD/4gIcSUND/Ko2JJuhuCempcX2PS6FS+fgcegih7FjXQ+tbTWulH0f6W/IlbGzaxVTo1L1SL1FIcVyp+R8N+HMz//Z";
    }
}