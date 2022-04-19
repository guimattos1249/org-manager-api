using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrgManager.Domain;
using OrgManager.Domain.Identity;

namespace OrgManager.Repository.Contexts
{
    public class OrgManagerContext : IdentityDbContext<User, Role, int, 
                                                        IdentityUserClaim<int>, 
                                                        UserRole, IdentityUserLogin<int>, 
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public OrgManagerContext(DbContextOptions<OrgManagerContext> options) : base (options) { }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<UserDepartament> UserDepartaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(entity => {
                entity.Property(m => m.Id).HasMaxLength(110);
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityRole>(entity => {
                entity.Property(m => m.Id).HasMaxLength(110);
                entity.Property(m => m.Name).HasMaxLength(110);
                entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(m => m.UserId);
                entity.Property(m => m.LoginProvider).HasMaxLength(127);
                entity.Property(m => m.ProviderKey).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(m => m.UserId);
                entity.HasKey(m => m.RoleId);
                entity.Property(m => m.UserId).HasMaxLength(127);
                entity.Property(m => m.RoleId).HasMaxLength(127);
            });
            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.HasKey(m => m.UserId);
                entity.Property(m => m.UserId).HasMaxLength(110);
                entity.Property(m => m.LoginProvider).HasMaxLength(110);
                entity.Property(m => m.Name).HasMaxLength(110);               
                
            });

            modelBuilder.Entity<UserRole>(userRole => 
                {
                    userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                    userRole.HasOne(ur => ur.Role).
                        WithMany(r => r.UserRoles).
                        HasForeignKey(ur => ur.RoleId).
                        IsRequired();

                    userRole.HasOne(ur => ur.User).
                        WithMany(r => r.UserRoles).
                        HasForeignKey(ur => ur.UserId).
                        IsRequired();
                }
            );

            modelBuilder.Entity<UserDepartament>()
                .HasKey(UD => new {UD.UserId, UD.DepartamentId});
        }
    }
}