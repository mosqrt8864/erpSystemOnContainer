using PurchaseManagement.Domain.Entities;

namespace PurchaseManagement.Domain.Interfaces;

public interface IPurchaseRequestItemRepository
{
    Task<PurchaseRequestItem> GetAsync(int id);
    Task Delete(PurchaseRequestItem purchaseRequestItem,CancellationToken cancellationToken);
}