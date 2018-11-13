using System.Web;
using System.Web.Mvc;

namespace StudentProject.Controllers
{
    public class SessionMaintainController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // if (HttpContext.Current.Session["userInfo"] == null)
            if(HttpContext.Current.Session["EmailInfo"].Equals(null) 
                && HttpContext.Current.Session["UserId"].Equals(null) 
                && HttpContext.Current.Session["RoleId"].Equals(null))
            {
                filterContext.Result = new RedirectResult("~/UserLogIn/UserLogIn");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}