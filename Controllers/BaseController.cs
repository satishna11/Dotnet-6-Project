using inventory_re.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace inventory_re.Controllers
{
    public class BaseController : Controller
    {

        


        protected Users SessionUser
        {
            get
            {
                string ss = HttpContext.Session.GetString("USER_INFO");
                if (string.IsNullOrEmpty(ss))
                {
                    return new Users();
                }
                else
                {
                    return JsonConvert.DeserializeObject<Users>(ss);
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string ss = HttpContext.Session.GetString("USER_INFO");
            if (string.IsNullOrEmpty(ss))
            {
                context.Result = Redirect("/Auth/Login");
            }

            base.OnActionExecuting(context);
        }
    }
}
