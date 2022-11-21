using MaterialsManagement.Domain.Entities;
using MediatR;
namespace MaterialsManagement.Domain.Interfaces;

public interface IPartNumberRepository
{
    Task Add(PartNumber partNumber,CancellationToken cancellationToken);
    Task<PartNumber> GetAsync(string id);
    Task<IEnumerable<PartNumber>> GetListAsync(int pageSize,int pageNumber);
    Task<int> GetCountAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}