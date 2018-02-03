using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;

namespace PropertyService.Domain.Managers.securitymiddleware
{
    public class ApiKeyValidatorMiddleWare
    {
        const string ApiKeyName = "ClientApi-key";
        const string AppNameKey = "APP-Name";
        private readonly RequestDelegate _next;
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyValidatorMiddleWare"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ApiKeyValidatorMiddleWare(RequestDelegate next)
        {
            _next = next;

        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="serviceApiRepo">The service API repo.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context, IPSRepository<ServiceApiKey> serviceApiRepo)
        {
            if (context.Request.Headers.ContainsKey(ApiKeyName))
            {
                Guid key;
                Guid.TryParse(context.Request.Headers[ApiKeyName], out key);
                if (key != Guid.Empty)
                {
                    var appName = context.Request.Headers[AppNameKey];
                    var appHeaderOrigin = context.Request.Headers["Origin"];
                    var serviceKeyData = await serviceApiRepo.GetSingleAsync(x => x.Id == key && x.IsValid);

                    if (serviceKeyData !=  null && serviceKeyData.AppName.Equals(appName, StringComparison.OrdinalIgnoreCase) && (string.IsNullOrEmpty(serviceKeyData.AppDomainUrl) || serviceKeyData.AppDomainUrl.Equals(appHeaderOrigin, StringComparison.OrdinalIgnoreCase)))
                    {                
                        await _next.Invoke(context);
                        return;
                    }
                   
                }

                context.Response.StatusCode = 400; //Bad Request     
                await context.Response.WriteAsync("Invalid API Key");
            }
            else
            {
                if (context.Request.Path == "/api/default")
                {
                    await _next.Invoke(context);
                    return;
                }
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("API Key is missing");
                return;
            }
        }
    }

    #region ExtensionMethod
    public static class UserKeyValidatorsExtension
    {
        /// <summary>
        /// Uses the API key validator.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>IApplicationBuilder.</returns>
        public static IApplicationBuilder UseApiKeyValidator(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiKeyValidatorMiddleWare>();
            
        }
    }
    #endregion
}
