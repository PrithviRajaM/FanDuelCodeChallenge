
using DepthChart_Models.Constants;
using DepthChart_Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DepthChart_Service.Controllers;

public class BaseController : Controller
{
    /// <summary>
    /// Generates a standard response body consuming varied business responses
    /// </summary>
    /// <typeparam name="T">Can be any type of data to convey out of the API</typeparam>
    /// <param name="businessResponse">response from the business logic</param>
    /// <returns>Return standard IActionResult encapsulating the business response</returns>
    protected IActionResult GenerateAPIResponse<T>(BusinessResult<T> businessResponse)
    {
        switch (businessResponse.Status)
        {
            case ReturnStatus.OK:
            case ReturnStatus.NotFound:
                return Ok(new APIResponse<T>(
                businessResponse.Data,
                ReturnStatus.OK.ToString(),
                businessResponse.Message));
            case ReturnStatus.BadRequest:
            case ReturnStatus.Error:
                return BadRequest(new APIResponse<T>(
                businessResponse.Data,
                ReturnStatus.BadRequest.ToString(),
                businessResponse.Message));
            default: return BadRequest(businessResponse.Message);
        }
    }
}
