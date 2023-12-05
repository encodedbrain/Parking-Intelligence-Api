using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
        builder.Property(prop => prop.Photo).HasColumnName("photo").HasColumnType("varchar(256)");
        builder
            .Property(prop => prop.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");
        builder
            .Property(prop => prop.Password)
            .HasColumnName("password")
            .HasColumnType("varchar(256)");
        builder
            .Property(prop => prop.Nickname)
            .HasColumnName("nickname")
            .HasColumnType("varchar(100)");
        builder
            .HasMany(prop => prop.Buys)
            .WithOne(prop => prop.User)
            .HasForeignKey(prop => prop.UserId)
            .IsRequired();
        builder
            .HasMany(prop => prop.Vehicles)
            .WithOne(prop => prop.User)
            .HasForeignKey(prop => prop.UserId)
            .IsRequired();
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}