using System;
using System.Net;
using System.Threading.Tasks;
using hanimyhoney.Domain.Dto;
using hanimyhoney.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace hanimyhoney.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
		}

		public static Task HandleException(HttpContext context, Exception exception)
		{
			Log.Error($"{DateTime.UtcNow.ToString("HH:mm:ss")} : {exception}");
			HttpStatusCode code = HttpStatusCode.OK;

			var result = new MobileResponseError();

			switch (exception)
			{
				case GlobalException globalException:
					result.Error = globalException.Error;
					break;
				case ValidationException validationException:
					code = HttpStatusCode.BadRequest;
					result.Message = JsonConvert.SerializeObject(validationException.Failures);
					break;
				case NotFoundException _:
					code = HttpStatusCode.NotFound;
					break;
				default:
					result.Error = new ExceptionModel
					{
						Message = exception.Message
					};
					break;
			}

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)code;

			return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
		}
	}
}

public static class ExceptionMiddlewareExtensions
{
	public static void UseExceptionMiddleware(this IApplicationBuilder app)
	{
		app.UseMiddleware<hanimyhoney.Middlewares.ExceptionMiddleware>();
	}
}