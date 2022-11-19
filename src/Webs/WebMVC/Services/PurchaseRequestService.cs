using WebMVC.ViewModels.PurchaseRequest.List;
using WebMVC.ViewModels;
using System.Text.Json;
using WebMVC.ViewModels.PurchaseRequest.Create;
using WebMVC.ViewModels.PurchaseRequest.Detail;
using WebMVC.ViewModels.PurchaseRequest.Update;
namespace WebMVC.Services;
public class PurchaseRequestService: IPurchaseRequestService
{
    private readonly HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;
    private readonly string _purchaseRequestItemUrl;
    public PurchaseRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _remoteServiceBaseUrl = "http://localhost:5081/api/v1/purchaserequests";
        _purchaseRequestItemUrl = "http://localhost:5081/api/v1/purchaserequestitems";
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

    public async Task<DetailPurchaseRequestViewModel> GetPurchaseRequest(string id)
    {
        if ( string.IsNullOrEmpty(id)){
            return new DetailPurchaseRequestViewModel();
        }
        var uri = _remoteServiceBaseUrl + "/"+id;
        var response = await _httpClient.GetAsync(uri);
        var responseString = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<DetailPurchaseRequestViewModel>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return string.IsNullOrEmpty(responseString) || json ==null ?
            new DetailPurchaseRequestViewModel() :json;
    }

    public async Task<bool> UpdatePurchaseRequest(UpdatePurchaseRequestViewModel purchaseRequest)
    {
        var uri = _remoteServiceBaseUrl + "/"+purchaseRequest.Id.ToString();
        var content = new StringContent(JsonSerializer.Serialize(purchaseRequest),System.Text.Encoding.UTF8,"application/json");
        var response = await _httpClient.PatchAsync(uri,content);
        response.EnsureSuccessStatusCode();
        return true;
    }
    public async Task<bool> DeletePurchaseRquestItem(int id)
    {
        var uri = _purchaseRequestItemUrl + "/" + id.ToString();
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();
        return true;
    }
}