using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings
{
    public class AccountMap : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(prop => prop.AccountId);
            builder
                .Property(prop => prop.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar(50)");
            builder
                .Property(prop => prop.Nickname)
                .HasColumnName("Nickname")
                .HasColumnType("varchar(20)");
            builder
                .Property(prop => prop.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar(20)");
            builder
                .HasOne(prop => prop.User)
                .WithOne(prop => prop.Account)
                .HasForeignKey<Account>(prop => prop.AccountId);
            builder
                .HasOne(prop => prop.Parkings)
                .WithMany(prop => prop.Accounts)
                .HasForeignKey(prop => prop.ParkingId);
        }
    }
}
