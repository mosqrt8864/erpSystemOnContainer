using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels.PurchaseRequest.Create;
using WebMVC.ViewModels;

namespace WebMVC.Services;
public interface IPurchaseRequestService{
    Task<bool> Add(CreatePurchaseRequestViewModel purchaseRequest);
    //Task<bool> UpdatePartNumber(PartNumber partNumber);

    //Task<PartNumber> GetPartNumber(string id);
    Task<PaginatedList<PurchaseRequestViewModel>>GetPurchaseRequests(int pageNumber,int pageSize);
}