using Cresce.Core.Users;
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
            // #region ConfigureItem
            // modelBuilder.Entity<Item>(
            //     b =>
            //     {
            //         b.Property("_id");
            //         b.HasKey("_id");
            //         b.Property(e => e.Name);
            //         b.HasMany(e => e.Tags).WithOne().IsRequired();
            //     });
            // #endregion
//
            // #region ConfigureTag
            // modelBuilder.Entity<Tag>(
            //     b =>
            //     {
            //         b.Property("_id");
            //         b.HasKey("_id");
            //         b.Property(e => e.Label);
            //     });
            // #endregion
        }
    }

    public class UserModel
    {
        public string Id { get; set; }

        public User ToUser()
        {
            if (Id == null)
            {
                return new UnknownUser();
            }
            return new AdminUser
            {
                Id = Id
            };
        }
    }
}
