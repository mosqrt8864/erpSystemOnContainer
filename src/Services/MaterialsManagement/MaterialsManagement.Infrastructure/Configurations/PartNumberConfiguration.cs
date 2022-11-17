using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MaterialsManagement.Domain.Entities;

namespace MaterialsManagement.Infrastructure.Configurations;

class PartNumberConfiguration : IEntityTypeConfiguration<PartNumber>
{
    public void Configure(EntityTypeBuilder<PartNumber> partNumberConfiguration)
    {
        partNumberConfiguration.ToTable("part_numbers","part_numbers");
        partNumberConfiguration.HasKey(o=>o.Id);

        partNumberConfiguration.Property<string>("Name")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Name")
            .IsRequired();
        partNumberConfiguration.Property<string>("Spec")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Spec")
            .IsRequired();
    }
}