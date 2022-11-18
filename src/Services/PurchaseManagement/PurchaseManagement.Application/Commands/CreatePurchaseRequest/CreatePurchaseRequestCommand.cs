using MediatR;
using PurchaseManagement.Application.Command.CreatePurchaseRequest;
using PurchaseManagement.Domain.Interfaces;
using PurchaseManagement.Domain.Entities;
using AutoMapper;
namespace PurchaseManagement.Application.Commands.CreatePurchaseRequest;

public record CreatePurchaseRequestCommand : IRequest<bool>
{
    public string Id {set;get;}=string.Empty;
    public string Description{set;get;}=string.Empty;
    public IEnumerable<PurchaseRequestItemDto>? PurchaseRequestItems{set;get;} 
}

public class CreatePurchaseRequestHandler : IRequestHandler<CreatePurchaseRequestCommand,bool>
{
    private readonly IPurchaseRequestRepository _repository;
    private readonly IMapper _mapper;
    public CreatePurchaseRequestHandler(IPurchaseRequestRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreatePurchaseRequestCommand request , CancellationToken cancellationToken)
    {
        var purchaseRequest = new PurchaseRequest(){
            Id = request.Id,
            Description = request.Description,
        };
        foreach(var item in request.PurchaseRequestItems)
        {
            purchaseRequest.AddPurchaseRequestItem(request.Id,item.PNId,item.Qty);
        }
        await _repository.Add(purchaseRequest,cancellationToken);
        return true;
    }
}