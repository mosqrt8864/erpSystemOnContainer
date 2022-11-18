using PurchaseManagement.Application.Mappings;
using PurchaseManagement.Domain.Entities;
namespace PurchaseManagement.Application.Queries.GetPurchaseRequest;
public record PurchaseRequestDto : IMapFrom<PurchaseRequest>
{
    public PurchaseRequestDto()
    {
        PurchaseRequestItems =  new List<PurchaseRequestItemDto>();
    }
    public string Id{set;get;} = string.Empty;
    public DateTime CreateAt{set;get;}
    public string Description{set;get;} = string.Empty;
    public List<PurchaseRequestItemDto> PurchaseRequestItems{set;get;}
}