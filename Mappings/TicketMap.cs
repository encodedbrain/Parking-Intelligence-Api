using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Hour).HasColumnName("Hours").HasColumnType("varchar(5)");
            builder.Property(prop => prop.Date).HasColumnName("Dates").HasColumnType("varchar(11)");
            builder
                .Property(prop => prop.Address)
                .HasColumnName("Address")
                .HasColumnType("varchar(20)");
        }
    }
};
