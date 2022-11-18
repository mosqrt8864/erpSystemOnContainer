using PurchaseManagement.Domain.Entities;
using PurchaseManagement.Application.Mappings;
namespace PurchaseManagement.Application.Command.CreatePurchaseRequest;

public record PurchaseRequestItemDto : IMapFrom<PurchaseRequestItem>
{
    public string PNId{set;get;} = string.Empty;
    public string Name{set;get;} = string.Empty;
    public string Spec{set;get;}= string.Empty;
    public int Qty{set;get;} = 0;
}