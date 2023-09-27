using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("ticket");
            builder.HasKey(prop => prop.id);

            builder.Property(prop => prop.date).HasColumnName("date").HasColumnType("varchar(100)");
            builder.Property(prop => prop.hour).HasColumnName("hour").HasColumnType("varchar(100)");
            builder.Property(prop => prop.sequence).HasColumnName("sequence").HasColumnType("int");
            builder
                .Property(prop => prop.ticketNumber)
                .HasColumnName("ticketNumber")
                .HasColumnType("int");
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();
        }
    }
}
