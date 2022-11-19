namespace WebMVC.ViewModels.PurchaseRequest.Update;
public class UpdatePurchaseRequestViewModel{
    public UpdatePurchaseRequestViewModel()
    {
        PurchaseRequestItems = new List<UpdatePurchaseRequestItemViewModel>();
    }
    public string Id{set;get;} = string.Empty;
    public string Description{set;get;} = string.Empty;
    public DateTime CreateAt{set;get;}
    public List<UpdatePurchaseRequestItemViewModel> PurchaseRequestItems{set;get;}
}

public class UpdatePurchaseRequestItemViewModel
{
    public int Id {set;get;} = 0;
    public string PRId {set;get;} = string.Empty;
    public string PNId{set;get;} = string.Empty;
    public string Name{set;get;} = string.Empty;
    public string Spec{set;get;} = string.Empty;
    public int Qty{set;get;} = 1;
}