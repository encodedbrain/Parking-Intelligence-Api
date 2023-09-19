using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Data
{
    public class ParkingDB : DbContext
    {
        public ParkingDB(DbContextOptions<ParkingDB> options)
            : base(options) { }

        DbSet<Parking> Parking { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Week> Weeks { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Shopping> Shoppings { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configRoot = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                //replace appsettings.Development.json with appsettings.json
                .AddJsonFile("appsettings.Development.json")
                .Build();
            optionsBuilder.UseSqlServer(configRoot.GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
