using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class ParkingMap : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.ToTable("Parking");
            builder.HasKey(prop => prop.Id);

            builder
                .HasMany(prop => prop.Vehicles)
                .WithOne(prop => prop.Parkings)
                .HasForeignKey(prop => prop.ParkingId);
            builder
                .HasMany(prop => prop.Accounts)
                .WithOne(prop => prop.Parkings)
                .HasForeignKey(prop => prop.ParkingId);
            builder
                .HasMany(prop => prop.Weeks)
                .WithOne(prop => prop.Parkings)
                .HasForeignKey(prop => prop.ParkingId);
            builder
                .HasMany(prop => prop.Prices)
                .WithOne(prop => prop.Parkings)
                .HasForeignKey(prop => prop.ParkingId);
        }
    }
}
