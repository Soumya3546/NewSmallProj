using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NewSmallProj.Filter
{
	public class AddAuthorizationHeaderOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)

		{

			var authAttributes = context.MethodInfo.GetCustomAttributes(true);

			var allowAnonymous = authAttributes.OfType<AllowAnonymousAttribute>().Any();

			var authorizeAttributes = authAttributes.OfType<AuthorizeAttribute>().ToList();

			if (!allowAnonymous && authorizeAttributes.Any())

			{

				var bearerScheme = new OpenApiSecurityScheme

				{

					Reference = new OpenApiReference

					{

						Type = ReferenceType.SecurityScheme,

						Id = "Bearer"

					}

				};

				operation.Security = new List<OpenApiSecurityRequirement>

		 {

			 new OpenApiSecurityRequirement

			 {

				 [bearerScheme] = new List<string>()

			 }

		 };

			}

		}
	}
}
