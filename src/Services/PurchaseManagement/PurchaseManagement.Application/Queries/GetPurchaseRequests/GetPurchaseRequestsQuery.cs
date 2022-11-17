using PurchaseManagement.Domain.Interfaces;
using AutoMapper;
using MediatR;
using PurchaseManagement.Application.Models;
namespace PurchaseManagement.Application.Queries.GetPurchaseRequests;

public record GetPurchaseRequestsQuery : IRequest<PaginatedList<PurchaseRequestsDto>>
{
    public int PageSize{set;get;}=10;
    public int PageNumber{set;get;}=1;
}

public class GetPurchaseRequestsQueryHandler : IRequestHandler<GetPurchaseRequestsQuery,PaginatedList<PurchaseRequestsDto>>
{
    private readonly IPurchaseRequestRepository _repository;
    private readonly IMapper _mapper;
    public GetPurchaseRequestsQueryHandler(IPurchaseRequestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<PaginatedList<PurchaseRequestsDto>> Handle(GetPurchaseRequestsQuery request, CancellationToken cancellationToken)
    {
        var purchaseRequests = await _repository.GetListAsync(request.PageSize,request.PageNumber);
        var count = await _repository.GetCountAsync();
        var purchaseRequestsDto = _mapper.Map<List<PurchaseRequestsDto>>(purchaseRequests);
        var result = new PaginatedList<PurchaseRequestsDto>(purchaseRequestsDto,count,request.PageNumber,request.PageSize);
        return result;
    }
}
