using System.ComponentModel.DataAnnotations;
namespace WebMVC.ViewModels.PurchaseRequest.Create;
public class CreatePurchaseRequestViewModel 
{
    public CreatePurchaseRequestViewModel(){
        PurchaseRequestItems = new List<CreatePurchaseRequestItemViewModel>();
    }
    [Required]
    public string Id {set;get;} = string.Empty;
    [Required]
    public string Description{set;get;} = string.Empty;
    [Required, MinLength(1, ErrorMessage = "At least one item required in work order")]
    public List<CreatePurchaseRequestItemViewModel> PurchaseRequestItems{set;get;}
}

public class CreatePurchaseRequestItemViewModel
{
    public string PRId {set;get;} = string.Empty;
    public string PNId{set;get;} = string.Empty;
    public string Name{set;get;} = string.Empty;
    public string Spec{set;get;} = string.Empty;
    public int Qty{set;get;} = 1;
}