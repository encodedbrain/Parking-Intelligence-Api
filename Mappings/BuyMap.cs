using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class  BuyMap : IEntityTypeConfiguration<Buy>
{
    public void Configure(EntityTypeBuilder<Buy> builder)
    {
        builder.ToTable("buy");
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Date).HasColumnName("date").HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.VacancyType)
            .HasColumnName("vacancyType")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.Value)
            .HasColumnName("value")
            .HasColumnType("decimal(5,2)");
        builder.Property(prop => prop.VehicleIdentifier).HasColumnName("identifier").HasColumnType("varchar(20)");
        builder
            .HasOne(prop => prop.User)
            .WithMany(prop => prop.Buys)
            .HasForeignKey(prop => prop.UserId)
            .IsRequired();
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}