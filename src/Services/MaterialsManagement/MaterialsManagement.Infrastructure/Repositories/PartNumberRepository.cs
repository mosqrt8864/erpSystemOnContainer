using MaterialsManagement.Domain.Entities;
using MaterialsManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace MaterialsManagement.Infrastructure.Repositories;

public class PartNumberRepository : IPartNumberRepository
{
    private readonly MaterialsContext _context;
    public PartNumberRepository(MaterialsContext context)
    {
        _context = context;
    }
    public async Task Add(PartNumber partNumber,CancellationToken cancellationToken)
    {
        _context.PartNumbers.Add(partNumber);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<PartNumber> GetAsync(string id)
    {
        return await _context.PartNumbers.FirstOrDefaultAsync(x=>x.Id == id);
    }

    public async Task<IEnumerable<PartNumber>> GetListAsync(int pageSize,int pageNumber)
    {
        return await _context.PartNumbers.AsNoTracking().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.PartNumbers.AsNoTracking().CountAsync();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken){
        await _context.SaveChangesAsync(cancellationToken);
    }
}