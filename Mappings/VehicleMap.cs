using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class VehicleMap : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");
        builder.HasKey(prop => prop.Id);


        builder
            .Property(prop => prop.Brand)
            .HasColumnName("brand")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.Color)
            .HasColumnName("color")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.LicensePlate)
            .HasColumnName("licenseplate")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.Model)
            .HasColumnName("model")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.Status)
            .HasColumnName("status")
            .HasColumnType("varchar(15)");
        builder
            .Property(prop => prop.Species)
            .HasColumnName("species")
            .HasColumnType("varchar(100)");
        builder.Property(prop => prop.Year).HasColumnName("year").HasColumnType("int");

        builder
            .HasOne(prop => prop.User)
            .WithMany(prop => prop.Vehicles)
            .HasForeignKey(prop => prop.UserId)
            .IsRequired();
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}