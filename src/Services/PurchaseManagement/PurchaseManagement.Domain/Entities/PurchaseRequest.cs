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

    public void AddPurchaseRequestItem(string prId,string pnId,string name,string spec,int qty)
    {
        var item = new PurchaseRequestItem(){
            PRId = prId,
            PNId = pnId,
            Name = name,
            Spec = spec,
            Qty = qty,
        };
        PurchaseRequestItems.Add(item);
    }
    public void UpdatePurchaseRequestItem(string prId,int Id,string pnId,string name,string spec,int qty)
    {
        var existed = PurchaseRequestItems.Where(o=>o.Id == Id).SingleOrDefault();
        if (existed!=null){
            existed.PNId = pnId;
            existed.Name = name;
            existed.Spec = spec;
            existed.Qty = qty;
        }else{
            this.AddPurchaseRequestItem(prId,pnId,name,spec,qty);
        }
    }
}