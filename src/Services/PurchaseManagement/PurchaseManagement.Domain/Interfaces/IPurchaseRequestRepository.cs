using PurchaseManagement.Domain.Entities;

namespace PurchaseManagement.Domain.Interfaces;
public interface IPurchaseRequestRepository
{
    Task Add(PurchaseRequest purchaseRequest,CancellationToken cancellationToken);
    Task<PurchaseRequest> GetAsync(string prId);
    Task<IEnumerable<PurchaseRequest>> GetListAsync(int pageSize,int pageNumber);
    Task<int> GetCountAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task Delete(PurchaseRequest purchaseRequest,CancellationToken cancellationToken);
}