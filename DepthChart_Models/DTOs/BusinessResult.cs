using DepthChart_Models.Constants;

namespace DepthChart_Models.DTOs;

/// <summary>
/// A response business model to communicate with the controller and beyond
/// </summary>
/// <typeparam name="T"></typeparam>
public class BusinessResult<T>
{
    /// <summary>
    /// Varing constructors to accomodate different business response needs
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public BusinessResult()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public BusinessResult(T data)
    {
        this.Status = ReturnStatus.OK;
        this.Data = data;
    }

    public BusinessResult(T data, string remarks = null)
    {
        this.Status = ReturnStatus.OK;
        this.Data = data;
        this.Message = remarks;
    }

    public BusinessResult(ReturnStatus status, string responseCode = null, List<string> parameters = null)
    {
        this.Status = status;
        this.Message = this.CreateMessage(status, responseCode, parameters);
    }

    public BusinessResult(ReturnStatus status, string message)
    {
        this.Status = status;
        this.Message = message;
    }

    public BusinessResult(ReturnStatus status, List<string> parameters = null)
    {
        this.Status = status;
        this.Message = string.Join(", ", parameters);
    }

    public ReturnStatus Status { get; set; }

    public T Data { get; set; }

    public string BusinessResponseCode { get; set; } = "";

    public string Message { get; set; } = "";

    public List<string> Parameters { get; set; }

    public bool IsTranslated { get; set; }

    //Determines a messgae for each response code, in case any custom messages are not available
    private string CreateMessage(ReturnStatus status, string responseCode, List<string> parameters = null)
    {
        if (!string.IsNullOrEmpty(responseCode))
            return responseCode;
        string message;
        switch (this.Status)
        {
            case ReturnStatus.Error:
                message = "Error";
                break;
            case ReturnStatus.NotFound:
                message = "NotFound";
                break;
            case ReturnStatus.BadRequest:
                message = "BadRequest";
                break;
            case ReturnStatus.Unauthorized:
                message = "Unauthorized";
                break;
            case ReturnStatus.Forbidden:
                message = "Forbidden";
                break;
            case ReturnStatus.MethodNotAllowed:
                message = "MethodNotAllowed";
                break;
            default:
                message = "";
                break;
        }
        return message;
    }
}
