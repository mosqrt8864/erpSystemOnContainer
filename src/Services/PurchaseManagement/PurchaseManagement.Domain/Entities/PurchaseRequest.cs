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
    public void UpdatePurchaseRequestItem(string prId,int Id,string pnId,int qty)
    {
        var existed = PurchaseRequestItems.Where(o=>o.Id == Id).SingleOrDefault();
        if (existed!=null){
            existed.PNId = pnId;
            existed.Qty = qty;
        }else{
            this.AddPurchaseRequestItem(prId,pnId,qty);
        }
    }
}