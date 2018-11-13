using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentProject.Controllers
{
    public class UserLogOutController : Controller
    {
        // GET: UserLogOut
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Here this is used to logout or stop session as well as by this user can not get back after logout
        /// Session.Abandon() destroys the session
        ///Session.Clear() just removes all values
        ///Expires HTTP header is by default set to -1; this tells the client not to cache responses in the History 
        ///folder, so that when you use the back/forward buttons the client requests a new version of the response each time.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserLogOut()
        {
            //use this or next or next all other 
            Session.Clear();
            Session.Abandon();
           Session.RemoveAll();
            Session.Remove("userInfo");

            //this is still not showing error  
            FormsAuthentication.SignOut();

            //back button 
            //Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();
            Response.AddHeader("Cache-Control", "no-cache, no-store,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "0");

            Session.Abandon();
            Session.Clear();
            Response.Cookies["userInfo"].Expires.AddMilliseconds(1);
            Response.Cookies.Clear();

            
           

            return RedirectToAction("Index", "Home");
            
        }
    }
}