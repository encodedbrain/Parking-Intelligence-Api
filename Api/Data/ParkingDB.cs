using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Mappings;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Data;

public class ParkingDb : DbContext
{
    public ParkingDb()
    {
    }

    public ParkingDb(DbContextOptions<ParkingDb> options)
        : base(options)
    {
    }
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<Buy> Buys { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<Calendars> Calendars { get; set; } = null!;
    public DbSet<UserData> UserDatas { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configRoot = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //replace appsettings.Development.json with appsettings.json
            .AddJsonFile("appsettings.Development.json")
            .Build();
        optionsBuilder.UseSqlServer(configRoot.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // account/user
        modelBuilder
            .Entity<UserData>()
            .HasOne(e => e.User)
            .WithOne(e => e.UserData)
            .HasForeignKey<UserData>(e => e.UserId)
            .IsRequired();

        // invoice/buy
        modelBuilder
            .Entity<Buy>()
            .HasOne(e => e.Invoice)
            .WithOne(e => e.Buy)
            .HasForeignKey<Invoice>(e => e.BuyId)
            .IsRequired();
        modelBuilder
            .Entity<Invoice>()
            .HasOne(e => e.Buy)
            .WithOne(e => e.Invoice)
            .HasForeignKey<Invoice>(e => e.BuyId)
            .IsRequired();

        // payment/buy

        modelBuilder
            .Entity<Buy>()
            .HasOne(e => e.PaymentMethod)
            .WithOne(e => e.Buy)
            .HasForeignKey<PaymentMethod>(e => e.BuyId)
            .IsRequired();

        modelBuilder
            .Entity<PaymentMethod>()
            .HasOne(e => e.Buy)
            .WithOne(e => e.PaymentMethod)
            .HasForeignKey<PaymentMethod>(e => e.BuyId)
            .IsRequired();
        //ticket / invoice
        modelBuilder.Entity<Ticket>().HasOne(ticket =>
            ticket.Buy).WithOne(ticket => ticket.Ticket).HasForeignKey<Ticket>(
            ticket => ticket.BuyId);
        


        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new BuyMap());
        modelBuilder.ApplyConfiguration(new InvoiceMap());
        modelBuilder.ApplyConfiguration(new TicketMap());
        modelBuilder.ApplyConfiguration(new VehicleMap());
        modelBuilder.ApplyConfiguration(new PaymentMethodMap());
        modelBuilder.ApplyConfiguration(new UserDataMap());
    }
}