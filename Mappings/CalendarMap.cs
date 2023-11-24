// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Parking_Intelligence_Api.Models;
//
// namespace Parking_Intelligence_Api.Mappings
// {
//     public class CalendarMap : IEntityTypeConfiguration<Calendars>
//     {
//         public void Configure(EntityTypeBuilder<Calendars> builder)
//         {
//             builder.ToTable("calendar");
//             builder.HasKey(prop => prop.id);
//
//             builder.Property(prop => prop.date).HasColumnType("datetime");
//
//             builder.Property(prop => prop.id).ValueGeneratedOnAdd();
//         }
//     }
// }
