using LandonApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LandonApi.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public JsonExceptionFilter(IHostingEnvironment env)
        {
            _env = env ?? throw new System.ArgumentNullException(nameof(env));
        }

        public void OnException(ExceptionContext context)
        {
            var error = new ApiError
            {
                Message = _env.IsDevelopment() ? context.Exception.Message : "A server error occured.",
                Detail = _env.IsDevelopment() ? context.Exception.StackTrace : context.Exception.Message
            };

            context.Result = new ObjectResult(error)
            {
                StatusCode = 500
            };
        }
    }
}
