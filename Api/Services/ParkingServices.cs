using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Parking_Intelligence_Api.Services;

public class ParkingServices
{
    public static object GenerateToken(Models.User users)
    {
        var builder = WebApplication.CreateBuilder();

        string? hash = builder.Configuration.GetConnectionString("secret");

        if (hash != null)
        {
            var key = Encoding.ASCII.GetBytes(hash);

            var config = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, users.Password),
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

            return new { token };
        }

        return null!;
    }
}