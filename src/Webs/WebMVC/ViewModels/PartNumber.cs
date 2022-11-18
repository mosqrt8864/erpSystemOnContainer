using System.ComponentModel.DataAnnotations;
namespace WebMVC.ViewModels;

public record PartNumber
{
    [Required]
    public string Id{get;set;} = string.Empty;
    [Required]
    public string Name{get;set;} = string.Empty;
    [Required]
    public string Spec{get;set;} = string.Empty;
}