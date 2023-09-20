using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");
            builder.HasKey(prop => prop.Id);

            builder
                .Property(prop => prop.Color)
                .HasColumnName("Colors")
                .HasColumnType("varchar(10)");
            builder
                .Property(prop => prop.Bodywork)
                .HasColumnName("Bodyworks")
                .HasColumnType("varchar(10)");
            builder
                .Property(prop => prop.LicensePlate)
                .HasColumnName("LicensePlates")
                .HasColumnType("varchar(6)");

            builder
                .Property(prop => prop.Model)
                .HasColumnName("Models")
                .HasColumnType("varchar(10)");

            builder.Property(prop => prop.Year).HasColumnName("Years").HasColumnType("varchar(4)");

            builder.Property(prop => prop.Type).HasColumnName("Types").HasColumnType("varchar(10)");

            builder
                .Property(prop => prop.Species)
                .HasColumnName("Species")
                .HasColumnType("varchar(10)");
            builder
                .HasOne(prop => prop.Parkings)
                .WithMany(prop => prop.Vehicles)
                .HasForeignKey(prop => prop.ParkingId);
        }
    }
}
