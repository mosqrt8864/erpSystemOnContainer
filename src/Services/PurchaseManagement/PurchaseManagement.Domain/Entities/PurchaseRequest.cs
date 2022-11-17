namespace PurchaseManagement.Domain.Entities;

public class PurchaseRequest
{
    public PurchaseRequest(){
        PurchaseRequestItems = new List<PurchaseRequestItem>();
    }
    public string Id{set;get;} = string.Empty;
    public DateTime CreateAt{set;get;}
    public string Description{set;get;} = string.Empty;
    public List<PurchaseRequestItem> PurchaseRequestItems{set;get;}

    public void AddPurchaseRequestItem(string prId,string pnId,int qty)
    {
        var item = new PurchaseRequestItem(){
            PRId = prId,
            PNId = pnId,
            Qty = qty,
        };
        PurchaseRequestItems.Add(item);
    }
}