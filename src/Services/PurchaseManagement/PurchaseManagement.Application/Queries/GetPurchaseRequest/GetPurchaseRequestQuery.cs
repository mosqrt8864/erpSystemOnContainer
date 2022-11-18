using PurchaseManagement.Domain.Interfaces;
using AutoMapper;
using MediatR;
namespace PurchaseManagement.Application.Queries.GetPurchaseRequest;
public record GetPurchaseRequestQuery : IRequest<PurchaseRequestDto>
{
    public string Id{get;set;}=string.Empty;
}

public class GetPurchaseRequestQueryHandler : IRequestHandler<GetPurchaseRequestQuery,PurchaseRequestDto>
{
    private readonly IPurchaseRequestRepository _context ;
    private readonly IMapper _mapper;
    public GetPurchaseRequestQueryHandler(IPurchaseRequestRepository context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PurchaseRequestDto> Handle(GetPurchaseRequestQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.GetAsync(request.Id);
        return _mapper.Map<PurchaseRequestDto>(result);
    }
}