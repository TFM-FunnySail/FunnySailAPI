using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace FunnySailAPI.SwaggerIdentityServer
{
	public class IdentityServerDocumentFilter : IDocumentFilter
	{
		private readonly string issuerUri;
		public IdentityServerDocumentFilter(string IssuerUri)
		{
			issuerUri = IssuerUri;
		}
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			swaggerDoc.Servers = new List<OpenApiServer>
			{
				new OpenApiServer { Url = issuerUri }
			};


		}
	}
}
