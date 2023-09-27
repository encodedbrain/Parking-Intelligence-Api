using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class UserDataMap : IEntityTypeConfiguration<UserData>
    {
        public void Configure(EntityTypeBuilder<UserData> builder)
        {
            builder.ToTable("UserData");
            builder.Property(prop => prop.id).ValueGeneratedNever();
            builder
                .Property(prop => prop.fullName)
                .HasColumnName("fullname")
                .HasColumnType("varchar(100)");
            builder.Property(prop => prop.Cpf).HasColumnName("cpf").HasColumnType("varchar(11)");
            builder
                .Property(prop => prop.address)
                .HasColumnName("address")
                .HasColumnType("varchar(100)");
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();
        }
    }
}
