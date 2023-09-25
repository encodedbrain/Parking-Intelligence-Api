using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");
            builder.HasKey(prop => prop.id);

            builder.Property(prop => prop.bodywork).HasColumnName("bodywork").HasColumnType("int");
            builder
                .Property(prop => prop.brand)
                .HasColumnName("brand")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.color)
                .HasColumnName("color")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.licensePlate)
                .HasColumnName("licenseplate")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.model)
                .HasColumnName("model")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.species)
                .HasColumnName("species")
                .HasColumnType("varchar(20)");
            builder.Property(prop => prop.year).HasColumnName("year").HasColumnType("int");

            builder
                .HasOne(prop => prop.User)
                .WithMany(prop => prop.Vehicles)
                .HasForeignKey(prop => prop.user_id)
                .IsRequired();
        }
    }
}
