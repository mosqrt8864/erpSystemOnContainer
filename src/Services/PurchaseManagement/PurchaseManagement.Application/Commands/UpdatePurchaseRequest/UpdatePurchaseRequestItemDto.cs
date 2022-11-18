namespace PurchaseManagement.Application.Commands.UpdatePurchaseRequest;

public class UpdatePurchaseRequestItemDto
{
    public int Id {set;get;}=0;
    public string PRId{set;get;} = string.Empty;
    public string PNId{set;get;} = string.Empty;
    public int Qty{set;get;} = 0;
}