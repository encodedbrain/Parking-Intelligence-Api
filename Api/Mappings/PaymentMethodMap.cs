using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Intelligence_Api.Models;

namespace Parking_Intelligence_Api.Mappings;

public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethod");
        builder.HasKey(prop => prop.Id);
        builder
            .Property(prop => prop.Method)
            .HasColumnName("method")
            .HasColumnType("varchar(100)");

        builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
    }
}