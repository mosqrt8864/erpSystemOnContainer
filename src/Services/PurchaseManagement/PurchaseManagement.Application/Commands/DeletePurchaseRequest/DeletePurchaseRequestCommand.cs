using MediatR;
using PurchaseManagement.Domain.Interfaces;
namespace PurchaseManagement.Application.Commands.DeletePurchaseRequest;

public record DeletePurchaseRequestCommand : IRequest<bool>
{
    public string Id{set;get;} = string.Empty;
}

public class DeletePurchaseRequestCommandHandler : IRequestHandler<DeletePurchaseRequestCommand,bool>
{
    private readonly IPurchaseRequestRepository _repository;
    public DeletePurchaseRequestCommandHandler(IPurchaseRequestRepository repository)
    {
        _repository = repository;
    }
    public async Task<bool> Handle(DeletePurchaseRequestCommand request,CancellationToken cancellationToken)
    {
         var purchaseRequest = await _repository.GetAsync(request.Id);
         await _repository.Delete(purchaseRequest,cancellationToken);
        return true;
    }
}