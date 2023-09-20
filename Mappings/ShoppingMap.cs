using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class ShoppingMap : IEntityTypeConfiguration<Shopping>
    {
        public void Configure(EntityTypeBuilder<Shopping> builder)
        {
            builder.ToTable("Shoppings");
            builder.HasKey(
                prop =>
                    new
                    {
                        prop.InvoiceId,
                        prop.TicketId,
                        prop.VehicleId
                    }
            );
            builder
                .Property(prop => prop.Payment)
                .HasColumnName("Payments")
                .HasColumnType("decimal(4,2)")
                .HasPrecision(4, 2);
            builder
                .Property(prop => prop.Vacancy)
                .HasColumnName("Vacancys")
                .HasColumnType("varchar(10)");
            builder.Property(prop => prop.Type).HasColumnName("Types").HasColumnType("varchar(10)");
            builder
                .HasOne(prop => prop.Users)
                .WithMany(prop => prop.Shoppings)
                .HasForeignKey(prop => prop.UserId);

            builder
                .HasOne(prop => prop.Vehicle)
                .WithMany(prop => prop.Shopping)
                .HasForeignKey(prop => prop.VehicleId);
            builder
                .HasOne(prop => prop.Invoice)
                .WithMany(prop => prop.Shopping)
                .HasForeignKey(prop => prop.InvoiceId);
            builder
                .HasOne(prop => prop.Ticket)
                .WithMany(prop => prop.Shopping)
                .HasForeignKey(prop => prop.TicketId);
        }
    }
}
