using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(prop => prop.Id);
            builder
                .Property(prop => prop.Address)
                .HasColumnName("Address")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.Entrance)
                .HasColumnName("Entrances")
                .HasColumnType("varchar(10)");
            builder
                .Property(prop => prop.Payment)
                .HasColumnName("Payments")
                .HasColumnType("decimal(4,2)");
            builder
                .Property(prop => prop.Value)
                .HasColumnName("Values")
                .HasColumnType("decimal(4,2)");

            builder.Property(prop => prop.Type).HasColumnName("Types").HasColumnType("varchar(10)");
            builder
                .Property(prop => prop.Permanence)
                .HasColumnName("Permanences")
                .HasColumnType("varchar(10)");

            builder
                .Property(prop => prop.Entrance)
                .HasColumnName("Moneys")
                .HasColumnType("decimal(4,2)");
            builder
                .Property(prop => prop.Transshipment)
                .HasColumnName("Transshipments")
                .HasColumnType("decimal(4,2)");
        }
    }
}
