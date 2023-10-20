using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class TablesMap : IEntityTypeConfiguration<Tables>
{
    public void Configure(EntityTypeBuilder<Tables> builder)
    {
        builder.ToTable("priceTables");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Passengers).HasColumnName("passengers").HasColumnType("decimal(5,2)");
        builder.Property(prop => prop.Freight).HasColumnName("freight").HasColumnType("decimal(5,2)");
        builder
            .Property(prop => prop.Mixed)
            .HasColumnName("mixed")
            .HasColumnType("decimal(5,2)");
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}