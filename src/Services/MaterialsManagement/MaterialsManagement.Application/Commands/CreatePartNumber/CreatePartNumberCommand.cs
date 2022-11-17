using MediatR;
using MaterialsManagement.Domain.Interfaces;
using MaterialsManagement.Domain.Entities;
namespace MaterialsManagement.Application.Commands.CreatePartNumber;

public class CreatePartNumberCommand : IRequest<bool>
{
    public string Id{set;get;} = string.Empty;
    public string Name {set;get;}= string.Empty;
    public string Spec{set;get;}= string.Empty;
}

public class CreatePartNumberCommandHandler : IRequestHandler<CreatePartNumberCommand,bool>
{
    private readonly IPartNumberRepository _repository;
    public CreatePartNumberCommandHandler(IPartNumberRepository repository)
    {
        _repository= repository;
    }
    public async Task<bool> Handle(CreatePartNumberCommand request,CancellationToken cancellationToken)
    {
        var entity = new PartNumber(){
            Id=request.Id,
            Name =request.Name,
            Spec=request.Spec
        };
        await _repository.Add(entity,cancellationToken);
        return true;
    }
}