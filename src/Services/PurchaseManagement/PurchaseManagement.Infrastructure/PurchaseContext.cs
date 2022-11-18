using Microsoft.EntityFrameworkCore;
using PurchaseManagement.Infrastructure.Configurations;
using PurchaseManagement.Domain.Entities;
namespace PurchaseManagement.Infrastructure;

public class PurchaseContext : DbContext
{
    public DbSet<PurchaseRequest>PurchaseRequests{set;get;}
    public DbSet<PurchaseRequestItem> PurchaseRequestItems{set;get;}
    public PurchaseContext(DbContextOptions<PurchaseContext>options):base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PurchaseRequestConfiguration());
        modelBuilder.ApplyConfiguration(new PurchaseRequestItemConfiguration());
    }
}