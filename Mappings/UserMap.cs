using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(prop => prop.id);
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();

            builder
                .Property(prop => prop.email)
                .HasColumnName("email")
                .HasColumnType("varchar(100)");
            builder
                .Property(prop => prop.password)
                .HasColumnName("password")
                .HasColumnType("varchar(256)");
            builder
                .Property(prop => prop.nickname)
                .HasColumnName("nickname")
                .HasColumnType("varchar(100)");
            builder
                .HasMany(prop => prop.Buys)
                .WithOne(prop => prop.User)
                .HasForeignKey(prop => prop.user_id)
                .IsRequired();
            builder
                .HasMany(prop => prop.Vehicles)
                .WithOne(prop => prop.User)
                .HasForeignKey(prop => prop.user_id)
                .IsRequired();
            builder.Property(prop => prop.id).ValueGeneratedOnAdd();
        }
    }
}
