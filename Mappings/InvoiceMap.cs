using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class InvoiceMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoice");
        builder.HasKey(prop => prop.Id);
        builder
            .Property(prop => prop.AmountPaid)
            .HasColumnName("amountPaid")
            .HasColumnType("decimal(5,2)");
        builder
            .Property(prop => prop.DateEntry)
            .HasColumnName("dateEntry")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.DepartureDate)
            .HasColumnName("departureDate")
            .HasColumnType("datetime");
        builder
            .Property(prop => prop.Expense)
            .HasColumnName("expense")
            .HasColumnType("decimal(5,2)");
        builder.Property(prop => prop.Change).HasColumnName("change").HasColumnType("decimal(5,2)");
        builder
            .Property(prop => prop.StayTime)
            .HasColumnName("stayTime")
            .HasColumnType("varchar(20)");
        builder
            .Property(prop => prop.TicketNumber)
            .HasColumnName("ticketNumber")
            .HasColumnType("int");
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}