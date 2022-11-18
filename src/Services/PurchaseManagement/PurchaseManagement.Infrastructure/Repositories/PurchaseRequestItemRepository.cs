using PurchaseManagement.Domain.Interfaces;
using PurchaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PurchaseManagement.Infrastructure.Repositories;

public class PurchaseRequestItemRepository : IPurchaseRequestItemRepository
{
    private readonly PurchaseContext _context;

    public PurchaseRequestItemRepository(PurchaseContext context)
    {
        _context = context;
    }
    public async Task<PurchaseRequestItem> GetAsync(int id)
    {
        return await _context.PurchaseRequestItems.FirstOrDefaultAsync(p=>p.Id == id);
    }

    public async Task Delete(PurchaseRequestItem item,CancellationToken cancellationToken)
    {
        _context.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
    }
}