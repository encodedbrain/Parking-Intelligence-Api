using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class PriceMap : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Prices");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Car).HasColumnName("Cars").HasColumnType("decimal(4,2)");
            builder.Property(prop => prop.Bus).HasColumnName("Bus").HasColumnType("decimal(4,2)");
            builder
                .Property(prop => prop.Motorcycle)
                .HasColumnName("Motorcycle")
                .HasColumnType("decimal(4,2)");
            builder
                .HasOne(prop => prop.Parkings)
                .WithMany(prop => prop.Prices)
                .HasForeignKey(prop => prop.ParkingId);
        }
    }
}
