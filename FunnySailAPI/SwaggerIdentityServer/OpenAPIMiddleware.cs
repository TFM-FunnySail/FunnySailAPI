using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Writers;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace FunnySailAPI.SwaggerIdentityServer
{
    public class OpenAPIMiddleware
    {
		private readonly RequestDelegate _next;

		public OpenAPIMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var _responseGenerator = context.RequestServices.GetService<IOpenAPIDocumentGenerator>() ?? new OpenAPIDocumentGenerator();
			var issuerUri = context.GetIdentityServerIssuerUri();
			context.Response.StatusCode = (int)HttpStatusCode.OK;
			var document = await _responseGenerator.CreateOpenAPIDocAsync(issuerUri);

			using (var outputString = new StringWriter())
			{
				var writer = new OpenApiJsonWriter(outputString);
				document.SerializeAsV3(writer);
				await context.Response.WriteAsync(outputString.ToString());
			}

			return;
		}
	}
}
