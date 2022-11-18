using PurchaseManagement.Domain.Interfaces;
using PurchaseManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PurchaseManagement.Infrastructure.Repositories;

public class PurchaseRequestRepository : IPurchaseRequestRepository
{
    private readonly PurchaseContext _context;
    public PurchaseRequestRepository(PurchaseContext context)
    {
        _context = context;
    }

    public async Task Add(PurchaseRequest purchaseRequest,CancellationToken cancellationToken)
    {
        purchaseRequest.CreateAt = DateTime.Now;
        _context.PurchaseRequests.Add(purchaseRequest);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<PurchaseRequest> GetAsync(string id)
    {
        return await _context.PurchaseRequests.Include(o => o.PurchaseRequestItems).FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<List<PurchaseRequest>> GetListAsync(int pageSize,int pageNumber)
    {
        return await _context.PurchaseRequests.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.PurchaseRequests.AsNoTracking().CountAsync();
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken){
        await _context.SaveChangesAsync(cancellationToken);
    }
}