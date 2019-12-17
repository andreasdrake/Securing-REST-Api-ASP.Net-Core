using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandonApi.Filters
{
    public class RequireHttpsOrCloseAttribute 
        : RequireHttpsAttribute
    {
        protected override void HandleNonHttpsRequest
            (AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new StatusCodeResult(400);
        }
    }
}
