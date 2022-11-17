using MaterialsManagement.Domain.Interfaces;
using AutoMapper;
using MediatR;
namespace MaterialsManagement.Application.Queries.GetPartNumber;
public record GetPartNumberQuery : IRequest<PartNumberDto>
{
    public string Id{get;set;}=string.Empty;
}

public class GetPartNumberQueryHandler : IRequestHandler<GetPartNumberQuery,PartNumberDto>
{
    private readonly IPartNumberRepository _context ;
    private readonly IMapper _mapper;
    public GetPartNumberQueryHandler(IPartNumberRepository context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PartNumberDto> Handle(GetPartNumberQuery request,CancellationToken cancellationToken)
    {
        var result = await _context.GetAsync(request.Id);
        return _mapper.Map<PartNumberDto>(result);
    }
}