namespace DepthChart_Models.Constants;

/// <summary>
/// An internal return status definition for more elaborate response needs
/// </summary>
public enum ReturnStatus
{
    OK,
    MultipleRecordsUpdated,
    Error,
    NotFound,
    BadRequest,
    Unauthorized,
    Forbidden,
    MethodNotAllowed,
    Conflict,
    ValidationError,
}
