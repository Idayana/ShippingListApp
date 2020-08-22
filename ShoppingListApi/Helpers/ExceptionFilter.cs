using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Helpers
{
    public class ExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var res = JsonConvert.SerializeObject(new
            {
                Error = context.Exception.Message,
                Status = "500",
            });
            context.Result = new ObjectResult(res) {
                StatusCode = 500
            };
        }
    }
}
