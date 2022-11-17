using MaterialsManagement.Domain.Entities;
using MaterialsManagement.Application.Mappings;
namespace MaterialsManagement.Application.Queries.GetPartNumbers;

public record PartNumbersDto : IMapFrom<PartNumber>
{
    public string Id {get;set;} = string.Empty;
    public string Name {get;set;}= string.Empty;
    public string Spec {get;set;}= string.Empty;
}