using MediatR;
using PurchaseManagement.Domain.Interfaces;

namespace PurchaseManagement.Application.Commands.DeletePurchaseRequestItem;
public record DeletePurchaseRequestItemCommand :IRequest<bool>
{
    public int Id{set;get;} = 0;
}

public class DeletePurchaseRequestItemCommandHandler : IRequestHandler<DeletePurchaseRequestItemCommand,bool>
{
    private readonly IPurchaseRequestItemRepository _repository;
    public DeletePurchaseRequestItemCommandHandler(IPurchaseRequestItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeletePurchaseRequestItemCommand request,CancellationToken cancellationToken)
    {
        var purchaseRequestItem = await _repository.GetAsync(request.Id);
        await _repository.Delete(purchaseRequestItem,cancellationToken);
        return true;
    }
}