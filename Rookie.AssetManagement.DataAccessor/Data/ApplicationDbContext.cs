using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rookie.AssetManagement.DataAccessor.Data.Seeds;
using Rookie.AssetManagement.DataAccessor.Entities;

namespace Rookie.AssetManagement.DataAccessor.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<ReturnRequest> ReturnRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SeedCategoryData();

            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            builder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<ReturnRequest>()
                .HasOne(u => u.RequestedUser)
                .WithMany(a => a.ReturnsRequest)
                .HasForeignKey(u => u.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReturnRequest>()
                .HasOne(u => u.AcceptedUser)
                .WithMany(a => a.ReturnsAccept)
                .HasForeignKey(u => u.AcceptedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Assignment>()
                 .HasOne(a => a.AssignBy)
                 .WithMany(u => u.AssignmentsBy)
                 .HasForeignKey(a => a.AssignById)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            builder.Entity<Assignment>()
                 .HasOne(a => a.AssignTo)
                 .WithMany(u => u.AssignmentsTo)
                 .HasForeignKey(a => a.AssignToId)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();
        }
    }
}
