using PurchaseManagement.Domain.Entities;
using PurchaseManagement.Application.Mappings;
namespace PurchaseManagement.Application.Queries.GetPurchaseRequests;

public record PurchaseRequestsDto : IMapFrom<PurchaseRequest>
{
    public string Id{set;get;} = string.Empty;
    public DateTime CreateAt{set;get;}
    public string Description{set;get;} = string.Empty;
}