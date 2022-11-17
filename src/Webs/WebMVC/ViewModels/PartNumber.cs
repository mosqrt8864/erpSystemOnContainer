using System.ComponentModel.DataAnnotations;
namespace WebMVC.ViewModels;

public class PaginatedList<T>
{
    public List<T> Items { get;set; } = new List<T>();
    public int PageNumber { get;set; }
    public int TotalPages { get;set; }
    public int TotalCount { get;set; }
    public bool HasPreviousPage {get;set;}
    public bool HasNextPage {get;set;}

}
public record PartNumber
{
    [Required]
    public string Id{get;set;} = string.Empty;
    [Required]
    public string Name{get;set;} = string.Empty;
    [Required]
    public string Spec{get;set;} = string.Empty;
}