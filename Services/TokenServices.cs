using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.IdentityModel.Tokens;

namespace Parking_Intelligence_Api.Services;

public class TokenServices
{
    public static object GenerateToken(Models.User users)
    {
        var builder = WebApplication.CreateBuilder();
        string? hash = null;


        if (builder.Environment.IsDevelopment())
        {
            hash = builder.Configuration.GetConnectionString("secret");
        }


        if (builder.Environment.IsProduction())
        {
            var keyVaultUrl = builder.Configuration.GetSection("secret:KeyVaultURL");
            var keyVaultClientId = builder.Configuration.GetSection("secret:ClientId");
            var keyVaultClientSecret = builder.Configuration.GetSection("secret:ClientSecret");
            var keyVaultDirectoryId = builder.Configuration.GetSection("secret:DirectoryID");


            var credential = new ClientSecretCredential(keyVaultDirectoryId.Value, keyVaultClientId.Value,
                keyVaultClientSecret.Value);

            builder.Configuration.AddAzureKeyVault(keyVaultUrl.Value, keyVaultClientId.Value,
                keyVaultClientSecret.Value,
                new DefaultKeyVaultSecretManager());


            if (keyVaultUrl.Value != null)
            {
                var client = new SecretClient(new Uri(keyVaultUrl.Value), credential);


                hash = client.GetSecret("secret").Value.Value;
            }
        }

        if (hash != null)
        {
            var key = Encoding.ASCII.GetBytes(hash);
            var config = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, users.Nickname),
                        new Claim(ClaimTypes.Email, users.Email),

                        new Claim(
                            ClaimTypes.Role,
                            users.Password ?? throw new InvalidOperationException()
                        )
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreate = tokenHandler.CreateToken(config);
            var token = tokenHandler.WriteToken(tokenCreate);

            return token;
        }


        return null!;
    }
}