using MediatR;
using PurchaseManagement.Domain.Interfaces;
using PurchaseManagement.Domain.Entities;
using AutoMapper;
namespace PurchaseManagement.Application.Commands.UpdatePurchaseRequest;

public record UpdatePurchaseRequestCommand : IRequest<bool>
{
    public string Id {set;get;}=string.Empty;
    public string Description{set;get;}=string.Empty;
    public IEnumerable<UpdatePurchaseRequestItemDto> PurchaseRequestItems{set;get;} = new List<UpdatePurchaseRequestItemDto>();
}

public class UpdatePurchaseRequestCommandHandler : IRequestHandler<UpdatePurchaseRequestCommand,bool>
{
    private readonly IPurchaseRequestRepository _repository;
    private readonly IMapper _mapper;
    public UpdatePurchaseRequestCommandHandler(IPurchaseRequestRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle (UpdatePurchaseRequestCommand request,CancellationToken cancellationToken)
    {
        var purchaseRequest = await _repository.GetAsync(request.Id);
        purchaseRequest.Description = request.Description;
        foreach(var item in request.PurchaseRequestItems)
        {
            purchaseRequest.UpdatePurchaseRequestItem(purchaseRequest.Id,item.Id,item.PNId,item.Name,item.Spec,item.Qty);
        }
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}