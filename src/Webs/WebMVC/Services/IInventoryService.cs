using WebMVC.ViewModels;

namespace WebMVC.Services;

public interface IInventoryService{
    Task<bool> AddPartNumber(PartNumber partNumber);
    Task<bool> UpdatePartNumber(PartNumber partNumber);

    Task<PartNumber> GetPartNumber(string id);
    Task<PaginatedList<PartNumber>>GetPartNumbers(int pageNumber,int pageSize);
}