using System.Diagnostics;
using DevQuestions.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevQuestions.Presenters.ResponseExtensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Failure failure)
    {
        if (!failure.Any())
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        
        var distinctErrorsType = failure.Select(x => x.Type).Distinct().ToList();
        
        int statusCode = distinctErrorsType.Count > 1 ? StatusCodes.Status500InternalServerError : GetStatusCodeFromErrorType(distinctErrorsType.First());

        return new ObjectResult(failure)
        {
            StatusCode = statusCode
        };
    }

    private static int GetStatusCodeFromErrorType(ErrorType errorType) => errorType switch
    {
        ErrorType.NOT_FOUND => StatusCodes.Status404NotFound,
        ErrorType.VALIDATION => StatusCodes.Status400BadRequest,
        ErrorType.CONFLICT => StatusCodes.Status409Conflict,
        ErrorType.FAILURE => StatusCodes.Status500InternalServerError,
        _ => StatusCodes.Status500InternalServerError
    };
}