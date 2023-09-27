using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class BuyMap : IEntityTypeConfiguration<Buy>
    {
        public void Configure(EntityTypeBuilder<Buy> builder)
        {
            builder.ToTable("buy");
            builder.HasKey(prop => prop.id);
            builder.Property(prop => prop.date).HasColumnName("date").HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.vacancyType)
                .HasColumnName("vacancyType")
                .HasColumnType("varchar(100)");
            builder
                .Property(prop => prop.value)
                .HasColumnName("value")
                .HasColumnType("decimal(5,2)");
            builder
                .HasOne(prop => prop.User)
                .WithMany(prop => prop.Buys)
                .HasForeignKey(prop => prop.user_id)
                .IsRequired();
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();
        }
    }
}
