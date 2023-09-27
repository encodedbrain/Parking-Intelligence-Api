using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class InvoiceMap : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice");
            builder.HasKey(prop => prop.id);
            builder
                .Property(prop => prop.amountPaid)
                .HasColumnName("amountPaid")
                .HasColumnType("decimal(5,2)");
            builder
                .Property(prop => prop.dateEntry)
                .HasColumnName("dateEntry")
                .HasColumnType("varchar(100)");
            builder
                .Property(prop => prop.departureDate)
                .HasColumnName("departureDate")
                .HasColumnType("varchar(100)");
            builder
                .Property(prop => prop.expense)
                .HasColumnName("expense")
                .HasColumnType("decimal(5,2)");
            builder.Property(prop => prop.rest).HasColumnName("rest").HasColumnType("decimal(5,2)");
            builder
                .Property(prop => prop.stayTime)
                .HasColumnName("stayTime")
                .HasColumnType("varchar(100)");
            builder
                .Property(prop => prop.ticketNumber)
                .HasColumnName("ticketNumber")
                .HasColumnType("int");
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();
        }
    }
}
