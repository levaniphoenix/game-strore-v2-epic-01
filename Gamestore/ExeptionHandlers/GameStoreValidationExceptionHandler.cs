using System.Net;
using System.Text.Json;
using Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Gamestore.ExeptionHandlers;

public class GameStoreValidationExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(
			HttpContext httpContext,
			Exception exception,
			CancellationToken cancellationToken)
	{
		if (exception is not GameStoreValidationException)
		{
			return false;
		}

		var exceptionMessage = exception.Message;
		var statusCode = HttpStatusCode.BadRequest;
		var error = new { message = exceptionMessage };
		var result = JsonSerializer.Serialize(error);
		httpContext.Response.ContentType = "application/json";
		httpContext.Response.StatusCode = (int)statusCode;
		await httpContext.Response.WriteAsync(result, cancellationToken);
		return true;
	}
}
