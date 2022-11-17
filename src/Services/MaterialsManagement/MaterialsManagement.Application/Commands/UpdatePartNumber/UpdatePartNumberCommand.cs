using MediatR;
using MaterialsManagement.Domain.Entities;
using MaterialsManagement.Domain.Interfaces;

namespace MaterialsManagement.Application.Commands.UpdatePartNumber;
public record UpdatePartNumberCommand : IRequest<bool>
{
    public string Id{get;set;} = string.Empty;
    public string Name{get;set;} = string.Empty;
    public string Spec{get;set;} = string.Empty;
}

public class UpdatePartNumberCommandHandler : IRequestHandler<UpdatePartNumberCommand,bool>
{
    private readonly IPartNumberRepository _context;
    private readonly IMediator _mediator;
    public UpdatePartNumberCommandHandler(IPartNumberRepository context,IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<bool> Handle(UpdatePartNumberCommand request,CancellationToken cancellationToken)
    {
        var partNumber = _context.GetAsync(request.Id).Result;
        partNumber.Name = request.Name;
        partNumber.Spec = request.Spec;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}