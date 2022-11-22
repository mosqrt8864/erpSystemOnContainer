namespace PurchaseManagement.Api.Errors;

class PurchaseRequestErrorFeature
{
    public PurchaseRequestErrorType PurchaseRequestError{set;get;}
}

enum PurchaseRequestErrorType
{
    CreateExistKeyError,
    GetNotExistKeyError
}