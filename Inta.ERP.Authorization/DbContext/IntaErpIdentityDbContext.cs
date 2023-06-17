using Inta.ERP.Authorization.Configurations;
using Inta.ERP.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml;

namespace Inta.ERP.Authorization.DbContext
{
    public class IntaErpIdentityDbContext: IdentityDbContext<User, Role,string, IdentityUserClaim<string>, UserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public IntaErpIdentityDbContext(DbContextOptions<IntaErpIdentityDbContext> options)
        : base(options)
        {
        }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("auth");
            //builder.ApplyConfiguration(new ApplicationERPConfiguration());
            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
                //entity.HasKey(x => x.Id).HasName("UserId");
                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
                entity.Property(e => e.MaximumApproveAmount).HasPrecision(18, 2);
                entity.Property(e => e.MaximumPettyCashApproveAmount).HasPrecision(18, 2);
            });
            builder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<UserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable(name: "UserTokens");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable(name: "UserClaims");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable(name: "RoleClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });

            builder.Entity<OpenIddictEntityFrameworkCoreApplication>(entity =>
            {
                entity.ToTable(name: "OIDApplications"); // rename the default table OpenIddictApplications
            });
            builder.Entity<OpenIddictEntityFrameworkCoreAuthorization>(entity =>
            {
                entity.ToTable(name: "OIDAuthorizations"); // rename the default table OpenIddictAuthorizations
            });
            builder.Entity<OpenIddictEntityFrameworkCoreScope>(entity =>
            {
                entity.ToTable(name: "OIDScopes"); // rename the default table OpenIddictScopes
            });
            builder.Entity<OpenIddictEntityFrameworkCoreToken>(entity =>
            {
                entity.ToTable(name: "OIDTokens"); // rename the default table OpenIddictTokens
            });

            builder.Ignore<IdentityUserLogin<string>>();
            builder.Ignore<IdentityUserToken<string>>();
        }
    }
}
