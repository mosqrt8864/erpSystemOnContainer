namespace MaterialsManagement.Api.Errors;
class PartNumberErrorFeature
{
    public PartNumberErrorType PartNumberError {set;get;}
}

enum PartNumberErrorType
{
    CreateExistKeyError,
    GetNotExistKeyError
}