using Inta.ERP.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inta.ERP.Authorization.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            builder.HasData(
                 new User
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     UserId=1,
                     Email = "admin@localhost.com",
                     NormalizedEmail = "ADMIN@LOCALHOST.COM",
                     UserName = "admin@localhost.com",
                     NormalizedUserName = "ADMIN@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true,
                     SecurityStamp= Guid.NewGuid().ToString(),
                     Status=1,
                     Active=true,
                     IsApiUser=true,
                     MaximumApproveAmount = 0,
                     MaximumPettyCashApproveAmount=0,
                     CreatedUser=1,
                     CreatedDate= DateTime.Now,
                     LastModifiedUser=1,
                     LastModifiedDate= DateTime.Now

                 },
                 new User
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                     UserId=2,
                     Email = "user@localhost.com",
                     NormalizedEmail = "USER@LOCALHOST.COM",
                     UserName = "user@localhost.com",
                     NormalizedUserName = "USER@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true,
                     SecurityStamp = Guid.NewGuid().ToString(),
                     Status = 1,
                     Active = true,
                     IsApiUser = true,
                     MaximumApproveAmount = 0,
                     MaximumPettyCashApproveAmount = 0,
                     CreatedUser = 1,
                     CreatedDate = DateTime.Now,
                     LastModifiedUser = 1,
                     LastModifiedDate = DateTime.Now
                 }
            );
        }
    }
}
