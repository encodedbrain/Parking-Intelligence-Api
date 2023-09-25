using Microsoft.EntityFrameworkCore;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Data
{
    public class ParkingDB : DbContext
    {
        public ParkingDB()
        {
        }

        public ParkingDB(DbContextOptions<ParkingDB> options)
            : base(options) { }
        public DbSet<Tables> PriceTable { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Buy> Buys { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Calendars> Calendars { get; set; }
        public DbSet<UserData> UserDatas { get; set; }


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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // account/user
            modelBuilder
                .Entity<UserData>()
                .HasOne(e => e.User)
                .WithOne(e => e.UserData)
                .HasForeignKey<UserData>(e => e.user_id)
                .IsRequired();

            modelBuilder
                .Entity<User>()
                .HasOne(e => e.UserData)
                .WithOne(e => e.User)
                .HasForeignKey<UserData>(e => e.user_id)
                .IsRequired();
            // invoice/buy
            modelBuilder
                .Entity<Buy>()
                .HasOne(e => e.Invoice)
                .WithOne(e => e.Buy)
                .HasForeignKey<Invoice>(e => e.buy_id)
                .IsRequired();

            modelBuilder
                .Entity<Invoice>()
                .HasOne(e => e.Buy)
                .WithOne(e => e.Invoice)
                .HasForeignKey<Invoice>(e => e.buy_id)
                .IsRequired();
            // payment/buy

            modelBuilder
                .Entity<Buy>()
                .HasOne(e => e.PaymentMethod)
                .WithOne(e => e.Buy)
                .HasForeignKey<PaymentMethod>(e => e.buy_id)
                .IsRequired();

            modelBuilder
                .Entity<PaymentMethod>()
                .HasOne(e => e.Buy)
                .WithOne(e => e.PaymentMethod)
                .HasForeignKey<PaymentMethod>(e => e.buy_id)
                .IsRequired();

            //table/calendar
            modelBuilder
                .Entity<Tables>()
                .HasOne(e => e.Calendar)
                .WithOne(e => e.Table)
                .HasForeignKey<Calendars>(e => e.tables_id)
                .IsRequired();

            modelBuilder
                .Entity<Calendars>()
                .HasOne(e => e.Table)
                .WithOne(e => e.Calendar)
                .HasForeignKey<Calendars>(e => e.tables_id)
                .IsRequired();
            // invoice /ticket
            modelBuilder
                .Entity<Invoice>()
                .HasOne(e => e.Ticket)
                .WithOne(e => e.Invoice)
                .HasForeignKey<Ticket>(e => e.invoice_id)
                .IsRequired();

            modelBuilder
                .Entity<Ticket>()
                .HasOne(e => e.Invoice)
                .WithOne(e => e.Ticket)
                .HasForeignKey<Ticket>(e => e.invoice_id)
                .IsRequired();
        }
    }
}
