namespace PurchaseManagement.Domain.Entities;

public class PurchaseRequestItem
{
    public int Id{set;get;}
    public string PRId{set;get;} = string.Empty;
    public string PNId{set;get;} = string.Empty;
    public string Name {set;get;} = string.Empty;
    public string Spec {set;get;} = string.Empty;
    public int Qty{set;get;}
}