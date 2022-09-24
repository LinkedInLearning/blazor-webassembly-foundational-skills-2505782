using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyBlazorShopHosted.Web.Server.Attributes
{
    public class RequiresAuthorizationHeader : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext?.HttpContext?.Request?.Headers != null)
            {
                if (!filterContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }

                var configuration = filterContext.HttpContext.RequestServices.GetService<IConfiguration>();
                var apiKey = configuration.GetValue<string>("ApiKey");

                if (filterContext.HttpContext.Request.Headers["Authorization"].ToString() != apiKey)
                {
                    filterContext.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
