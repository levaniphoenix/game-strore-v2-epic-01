using System.Diagnostics;
using System.Text;
using Serilog;

namespace Gamestore.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		var stopwatch = Stopwatch.StartNew();
		var request = await FormatRequest(context.Request);

		var originalResponseBodyStream = context.Response.Body;
		using var responseBodyStream = new MemoryStream();
		context.Response.Body = responseBodyStream;

		// Call the next delegate/middleware in the pipeline.
		await next(context);

		stopwatch.Stop();
		var response = await FormatResponse(context.Response);
		var elapsedTime = stopwatch.ElapsedMilliseconds;
		var statusCode = context.Response.StatusCode;
		var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

		string responseBody = string.Empty;
		var contentType = context.Response.ContentType ?? string.Empty;

		// Only read and log the response body if it's NOT a PDF
		responseBody = contentType.Contains("application/pdf", StringComparison.OrdinalIgnoreCase)
			? "[PDF Content Omitted]" :
			await FormatResponse(context.Response);

		Log.Information(
			"[Request] IP: {IPAddress}, Method: {Method}, URL: {Url}, RequestBody: {RequestBody}, [Response] StatusCode: {StatusCode}, TimeElapsed: {ElapsedTime}ms, ResponseBody: {ResponseBody}", ipAddress, context.Request.Method, context.Request.Path, request, statusCode, elapsedTime, responseBody);

		await responseBodyStream.CopyToAsync(originalResponseBodyStream);
	}

	private static async Task<string> FormatRequest(HttpRequest request)
	{
		request.EnableBuffering();
		using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
		string body = await reader.ReadToEndAsync();
		request.Body.Position = 0;
		return body;
	}

	private static async Task<string> FormatResponse(HttpResponse response)
	{
		response.Body.Seek(0, SeekOrigin.Begin);
		string body = await new StreamReader(response.Body).ReadToEndAsync();
		response.Body.Seek(0, SeekOrigin.Begin);
		return body;
	}
}
