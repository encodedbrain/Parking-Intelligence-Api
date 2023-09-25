using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class TablesMap : IEntityTypeConfiguration<Tables>
    {
        public void Configure(EntityTypeBuilder<Tables> builder)
        {
            builder.ToTable("priceTables");
            builder.HasKey(prop => prop.id);

            builder.Property(prop => prop.bus).HasColumnName("bus").HasColumnType("decimal(5,2)");
            builder.Property(prop => prop.car).HasColumnName("car").HasColumnType("decimal(5,2)");
            builder
                .Property(prop => prop.motorcycle)
                .HasColumnName("motorcycle")
                .HasColumnType("decimal(5,2)");
        }
    }
}
