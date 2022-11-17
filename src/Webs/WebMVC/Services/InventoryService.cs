using WebMVC.ViewModels;
using System.Text.Json;
namespace WebMVC.Services;

public class InventoryService : IInventoryService
{
    private HttpClient _httpClient;
    private readonly string _remoteServiceBaseUrl;

    public InventoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _remoteServiceBaseUrl = "http://localhost:5259/api/v1/partnumbers";
    }

    public async Task<bool> AddPartNumber(PartNumber partNumber)
    {
        var uri = _remoteServiceBaseUrl;
        var partNumberContent = new StringContent(JsonSerializer.Serialize(partNumber),System.Text.Encoding.UTF8,"application/json");
        var response = await _httpClient.PostAsync(uri,partNumberContent);
        Console.WriteLine(response);
        response.EnsureSuccessStatusCode();
        return true;
    }

    public async Task<bool> UpdatePartNumber(PartNumber partNumber)
    {
        var uri = _remoteServiceBaseUrl + "/"+partNumber.Id.ToString();
        Console.WriteLine(uri);
        var partNumberContent = new StringContent(JsonSerializer.Serialize(partNumber),System.Text.Encoding.UTF8,"application/json");
        var response = await _httpClient.PatchAsync(uri,partNumberContent);
         Console.WriteLine(response);
        response.EnsureSuccessStatusCode();
        return true;
    }
    public async Task<PartNumber> GetPartNumber(string id)
    {
        if ( string.IsNullOrEmpty(id)){
            return new PartNumber();
        }
        var uri = _remoteServiceBaseUrl + "/"+id;
        var response = await _httpClient.GetAsync(uri);
        var responseString = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<PartNumber>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        return string.IsNullOrEmpty(responseString) || json ==null ?
            new PartNumber() :json;
    }
    public async Task<PaginatedList<PartNumber>> GetPartNumbers(int pageNumber,int pageSize)
    {
        var uri = _remoteServiceBaseUrl+"?pagenumber="+pageNumber.ToString()+"&pagesize="+pageSize.ToString();
        var response = await _httpClient.GetAsync(uri);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = new PaginatedList<PartNumber>();
        result.Items = new List<PartNumber>();
        var partNumbers = JsonDocument.Parse(responseString);
        Console.WriteLine(uri);
        Console.WriteLine(partNumbers.RootElement);
        foreach (JsonElement partNumber  in partNumbers.RootElement.GetProperty("items").EnumerateArray())
        {
            result.Items.Add(new PartNumber()
            {
                Name = partNumber.GetProperty("name").ToString(),
                Spec = partNumber.GetProperty("spec").ToString(),
                Id = partNumber.GetProperty("id").ToString()
            });
        }
        result.PageNumber =partNumbers.RootElement.GetProperty("pageNumber").GetInt32();
        result.TotalPages =partNumbers.RootElement.GetProperty("totalPages").GetInt32();
        result.HasNextPage =partNumbers.RootElement.GetProperty("hasNextPage").GetBoolean();
        result.HasPreviousPage =partNumbers.RootElement.GetProperty("hasPreviousPage").GetBoolean();
        return result;
    }
}