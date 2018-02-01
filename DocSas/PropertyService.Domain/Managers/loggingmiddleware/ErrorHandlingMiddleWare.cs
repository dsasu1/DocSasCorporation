using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using PropertyService.Domain.DataBaseContext;
using PropertyService.Domain.DataEntities;
using DSCAppEssentials.Extensions;

namespace PropertyService.Domain.Managers.loggingmiddleware
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IPSRepository<LogException> logRepo)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                await HandleExceptionAsync(context,ex, logRepo);
            }
        }

        private async  Task HandleExceptionAsync(HttpContext context, Exception exception, IPSRepository<LogException> logger)
        {
            try
            {
                LogException log = new LogException()
                {
                    ExceptionMessage = context.Request.Path + ": " + context.Request.QueryString + ": " + exception.ToJson()
                };

                await logger.AddAsync(log);
            }
            catch (Exception ex)
            {
                
            }
           
            context.Response.StatusCode = 400;
            await  context.Response.WriteAsync("SomethingWrong");
        }
    }

    #region ExtensionMethod
    public static class ErrorHandlingException
    {
        /// <summary>
        /// Uses the API key validator.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>IApplicationBuilder.</returns>
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlingMiddleWare>();
         
        }
    }
    #endregion
}
