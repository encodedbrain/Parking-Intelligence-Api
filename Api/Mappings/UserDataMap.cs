using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class UserDataMap : IEntityTypeConfiguration<UserData>
{
    public void Configure(EntityTypeBuilder<UserData> builder)
    {
        builder.ToTable("UserData");
        builder.Property(prop => prop.Id).ValueGeneratedNever();
        builder
            .Property(prop => prop.FullName)
            .HasColumnName("fullname")
            .HasColumnType("varchar(100)");
        builder.Property(prop => prop.Cpf).HasColumnName("cpf").HasColumnType("varchar(11)");
        builder
            .Property(prop => prop.Address)
            .HasColumnName("address")
            .HasColumnType("varchar(100)");
        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}