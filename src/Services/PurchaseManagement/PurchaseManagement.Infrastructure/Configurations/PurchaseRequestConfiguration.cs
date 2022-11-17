using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseManagement.Domain.Entities;

namespace PurchaseManagement.Infrastructure.Configurations;

class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> purchaseRequestConfiguration)
    {
        purchaseRequestConfiguration.ToTable("purchaseRequest","purchaseRequest");
        purchaseRequestConfiguration.HasKey(o=>o.Id);

        purchaseRequestConfiguration.Property<DateTime>("CreateAt")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("CreateAt")
            .IsRequired();
        purchaseRequestConfiguration.Property<string>("Description")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Description")
            .IsRequired(false);
        var navigation = purchaseRequestConfiguration.Metadata.FindNavigation(nameof(PurchaseRequest.PurchaseRequestItems));

        // DDD Patterns comment:
        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}