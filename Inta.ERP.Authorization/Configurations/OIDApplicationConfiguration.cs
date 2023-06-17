using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OpenIddict.EntityFrameworkCore.Models;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Inta.ERP.Authorization.Configurations
{
    public class OIDApplicationConfiguration: IEntityTypeConfiguration<OpenIddictEntityFrameworkCoreApplication>
    {
        public void Configure(EntityTypeBuilder<OpenIddictEntityFrameworkCoreApplication> builder)
        {
            builder.HasData(
                new OpenIddictEntityFrameworkCoreApplication
                {
                    //Id = "your-application-id-1",
                    ClientId="Inta_ERP_Angular_Client",
                    ConsentType = ConsentTypes.Explicit,
                    //ClientSecret=Guid.NewGuid().ToString(),
                    DisplayName = "Inta ERP Angular Client PKCE",
                    DisplayNames = "{\"fr-FR\":\"Inta ERP Angular Client PKCE\"}",
                    PostLogoutRedirectUris = "[\"https://localhost:4200\"]",
                    RedirectUris = "[\"https://localhost:4200\"]",
                    Permissions="[\"ept:authorization\",\"ept:logout\",\"ept:token\",\"ept:revocation\",\"gt:authorization_code\",\"gt:refresh_token\",\"rst:code\",\"scp:email\",\"scp:profile\",\"scp:roles\",\"scp:dataEventRecords\"]",
                    Requirements= "[\"ft:pkce\"]",
                    Type= "public"

                }
            );
        }
    }
}
