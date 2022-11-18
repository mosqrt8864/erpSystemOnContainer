using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels;
using System.Text.Json;
using WebMVC.ViewModels.PurchaseRequest.Create;
namespace WebMVC.Services;
public class PurchaseRequestService: IPurchaseRequestService
{
    private readonly HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;
    public PurchaseRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _remoteServiceBaseUrl = "http://localhost:5081/api/v1/purchaserequests";
    }

    public async Task<PaginatedList<PurchaseRequestViewModel>> GetPurchaseRequests(int pageNumber,int pageSize)
    {
        var uri = _remoteServiceBaseUrl+"?pagenumber="+pageNumber.ToString()+"&pagesize="+pageSize.ToString();
        var response = await _httpClient.GetAsync(uri);
        var respString = await response.Content.ReadAsStringAsync();
        var result = new PaginatedList<PurchaseRequestViewModel>();
        result.Items = new List<PurchaseRequestViewModel>();
        var purchaseRequest = JsonDocument.Parse(respString);
        foreach (JsonElement item  in purchaseRequest.RootElement.GetProperty("items").EnumerateArray())
        {
            result.Items.Add(new PurchaseRequestViewModel()
            {
                Id = item.GetProperty("id").ToString(),
                Description = item.GetProperty("description").ToString(),
                CreateAt = item.GetProperty("createAt").GetDateTime()
            });
        }
        result.PageNumber =purchaseRequest.RootElement.GetProperty("pageNumber").GetInt32();
        result.TotalPages =purchaseRequest.RootElement.GetProperty("totalPages").GetInt32();
        result.HasNextPage =purchaseRequest.RootElement.GetProperty("hasNextPage").GetBoolean();
        result.HasPreviousPage =purchaseRequest.RootElement.GetProperty("hasPreviousPage").GetBoolean();
        return result;
    }

    public async Task<bool> Add(CreatePurchaseRequestViewModel purchaseRequest)
    {
        var uri = _remoteServiceBaseUrl;
        var purchaseRequestContent = new StringContent(JsonSerializer.Serialize(purchaseRequest),System.Text.Encoding.UTF8,"application/json");
        var response = await _httpClient.PostAsync(uri,purchaseRequestContent);
        response.EnsureSuccessStatusCode();
        return true;
    }
}