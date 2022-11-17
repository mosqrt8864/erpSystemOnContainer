using MaterialsManagement.Domain.Entities;
using MaterialsManagement.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
namespace MaterialsManagement.Infrastructure;

public class MaterialsContext : DbContext
{
    public DbSet<PartNumber> PartNumbers {set;get;}
    public MaterialsContext(DbContextOptions<MaterialsContext>options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PartNumberConfiguration());
    }
}