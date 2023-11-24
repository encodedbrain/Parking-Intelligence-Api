using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class TicketMap : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("ticket");
        builder.HasKey(prop => prop.Id);

        builder.Property(prop => prop.Date).HasColumnName("date").HasColumnType("varchar(100)");
        builder.Property(prop => prop.Hour).HasColumnName("hour").HasColumnType("varchar(100)");
        builder.Property(prop => prop.Sequence).HasColumnName("sequence").HasColumnType("int");
        builder
            .Property(prop => prop.TicketNumber)
            .HasColumnName("ticketNumber")
            .HasColumnType("int");
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}