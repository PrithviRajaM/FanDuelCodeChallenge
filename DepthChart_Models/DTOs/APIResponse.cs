namespace DepthChart_Models.DTOs;

/// <summary>
/// The response body conveyed out of the endpoint
/// </summary>
/// <typeparam name="T">Any type of data point to relay out</typeparam>
public class APIResponse<T>
{
    public APIResponse(T data, string status, string message)
    {
        Data = data;
        Status = status;
        Message = message;
    }

    //Business result data to relay out
    public T Data { get; set; }

    //User friendly string status other then HTTP Status
    public string Status { get; set; }

    //Any message to detail in addition the status
    public string Message { get; set; }
}
