using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class WeekMap : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.ToTable("Weeks");
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Day_Week).HasColumnName("DaysWeek").HasPrecision(9);
            builder
                .Property(prop => prop.Status)
                .HasColumnName("Status")
                .HasColumnType("varchar(6)");
            builder
                .Property(prop => prop.Mounth)
                .HasColumnName("Mounths")
                .HasColumnType("varchar(10)");

            builder
                .HasOne(prop => prop.Parkings)
                .WithMany(prop => prop.Weeks)
                .HasForeignKey(prop => prop.ParkingId);
        }
    }
}
