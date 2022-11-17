using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseManagement.Domain.Entities;

namespace PurchaseManagement.Infrastructure.Configurations;

class PurchaseRequestItemConfiguration : IEntityTypeConfiguration<PurchaseRequestItem>
{
    public void Configure(EntityTypeBuilder<PurchaseRequestItem> purchaseRequestItemConfiguration)
    {
        purchaseRequestItemConfiguration.ToTable("purchaseRequest","purchaseRequest");
        purchaseRequestItemConfiguration.HasKey(o=>o.Id);
        purchaseRequestItemConfiguration.Property<string>("PRId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("PRId")
                    .IsRequired();
        purchaseRequestItemConfiguration.Property<string>("PNId")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("PNId")
                    .IsRequired();
        purchaseRequestItemConfiguration.Property<int>("Qty")
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName("Qty")
                    .IsRequired();
    }
}