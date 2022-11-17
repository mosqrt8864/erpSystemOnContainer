using MaterialsManagement.Domain.Interfaces;
using AutoMapper;
using MediatR;
using MaterialsManagement.Application.Models;
namespace MaterialsManagement.Application.Queries.GetPartNumbers;
public record GetPartNumbersQuery : IRequest<PaginatedList<PartNumbersDto>>
{
    public int PageNumber{get;init;}=1;
    public int PageSize{get;init;}=10;
}

public class GetPartNumbersQueryHandler : IRequestHandler<GetPartNumbersQuery,PaginatedList<PartNumbersDto>>
{
    private readonly IPartNumberRepository _context ;
    private readonly IMapper _mapper;
    public GetPartNumbersQueryHandler(IPartNumberRepository context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PartNumbersDto>> Handle(GetPartNumbersQuery request,CancellationToken cancellationToken)
    {
        var pnList = await _context.GetListAsync(request.PageSize,request.PageNumber);
        var count = await _context.GetCountAsync();
        var pnDtoList = _mapper.Map<List<PartNumbersDto>>(pnList);
        var pnDtoPageList = new PaginatedList<PartNumbersDto>(pnDtoList,count,request.PageNumber,request.PageSize);
        return pnDtoPageList;
    }
}