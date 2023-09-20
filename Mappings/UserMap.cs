using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(prop => prop.UserId);

            builder.Property(prop => prop.Cpf).HasColumnName("Cpfs").HasColumnType("varchar(11)");
            builder
                .HasMany(prop => prop.Shoppings)
                .WithOne(prop => prop.Users)
                .HasForeignKey(prop => prop.UserId);
            // builder
            //     .HasOne(prop => prop.Parkings)
            //     .WithMany(prop => prop.Users)
            //     .HasForeignKey(prop => prop.ParkingId);
            builder
                .HasOne(prop => prop.Account)
                .WithOne(prop => prop.User)
                .HasForeignKey<User>(prop => prop.AccountId);
        }
    }
}
