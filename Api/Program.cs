using System.Text;
using System.Text.Json.Serialization;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parking_Intelligence_Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddDbContext<ParkingDb>();

// if (builder.Environment.IsDevelopment())
// {
//     builder.Services.AddDbContext<ParkingDb>(options =>
//     {
//         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//     });
// }

// if (builder.Environment.IsProduction())
// {
//     var keyVaultUrl = builder.Configuration.GetSection("KeyVault:KeyVaultURL");
//     var keyVaultClientId = builder.Configuration.GetSection("KeyVaultClient:ClientId");
//     var keyVaultClientSecret = builder.Configuration.GetSection("KeyVaultClient:ClientSecret");
//     var keyVaultDirectoryId = builder.Configuration.GetSection("KeyVaultClient:DirectoryId");
//     var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!,keyVaultClientId.Value,keyVaultClientSecret.Value);
//
//     var client = new SecretClient(new Uri(keyVaultUrl.Value ?? string.Empty),credential);
//
//
//     builder.Services.AddDbContext<ParkingDb>(options =>
//     {
//         options.UseSqlServer(client.GetSecret("ProdConnection").Value.ToString());
//     });
// }


builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Open",
        cors =>
            cors
                .SetIsOriginAllowed(_ => true)
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
    );
});

// solving JsonException object cycle problem
builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
IdentityModelEventSource.ShowPII = true;

var hash = builder.Configuration.GetConnectionString("secret");

if (hash is null) return;

var key = Encoding.ASCII.GetBytes(hash);

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Configuration of the "Bearer Token" field
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        }
    );
    // Add security operation to all routes
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Open");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }