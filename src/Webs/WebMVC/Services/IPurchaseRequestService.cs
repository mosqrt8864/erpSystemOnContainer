using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels.PurchaseRequest.Create;
using WebMVC.ViewModels.PurchaseRequest.Detail;
using WebMVC.ViewModels.PurchaseRequest.Update;
using WebMVC.ViewModels;

namespace WebMVC.Services;
public interface IPurchaseRequestService{
    Task<bool> Add(CreatePurchaseRequestViewModel purchaseRequest);
    Task<bool> UpdatePurchaseRequest(UpdatePurchaseRequestViewModel purchaseRequest);

    //Task<PartNumber> GetPartNumber(string id);
    Task<PaginatedList<PurchaseRequestViewModel>>GetPurchaseRequests(int pageNumber,int pageSize);
    Task<DetailPurchaseRequestViewModel> GetPurchaseRequest(string id);
    Task<bool> DeletePurchaseRquestItem(int id);
}