using StudentProject.Models;
using StudentProject.ModelStudent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace StudentProject.Controllers
{
    public class UserLogInController : Controller
    {
        private StudentProjectEntities3 db = new StudentProjectEntities3();
        /// <summary>
        /// UserlogIN method with assign session as well cookies if required 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        // GET: UserLogIn
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UserLogIn()
        {
            UserLogInViewModel model = new UserLogInViewModel
            {
                //Email = Convert.ToString(Session["Email"])
                
            };

            //for to check whether stored in cookies or not

            if (Request.Cookies["userInfo"] != null)
            {
                model.Email = Request.Cookies["userInfo"]["userEmail"];
                model.Password = Request.Cookies["userInfo"]["userPassword"];
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogIn(UserLogInViewModel userLogInViewModel)
        {
            try
            {
                
                //password Hashing passwords with MD5 or sha-256 C#
                var TempPassword = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                        .ComputeHash(Encoding.UTF8.GetBytes(userLogInViewModel.Password)));

                var obj = db.UserRegistrations.Where(s => s.Email == userLogInViewModel.Email && s.Password == TempPassword).FirstOrDefault();
                
                if(obj != null)
                {
                    if(obj.IsActive)
                    {
                        //var result=(from item in db.UserRegistrations
                        //             join ct in db.UserInRoles on item.UserId equals ct.UserId
                        //             join cy in db.Roles on ct.RoleId equals cy.RoleId
                        //             where item.Email.Equals(obj.Email)
                        //               select new UserRegistrationViewModel
                        //               {
                        //                   UserId = item.UserId,
                        //                   Email = item.Email,
                        //                   RoleName = cy.RoleName
                        //               }).ToList();

                        // UserInRole userInRole = new UserInRole();
                        Session["EmailInfo"] = userLogInViewModel.Email;
                        Session["UserId"] = obj.UserId;
                        Session["RoleId"] = db.UserInRoles.Where(m => m.UserId == obj.UserId).Select(x => x.RoleId).FirstOrDefault(); ;
                        if (userLogInViewModel.RememberMe)
                        {
                            Response.Cookies["userInfo"]["userEmail"] = userLogInViewModel.Email;
                            Response.Cookies["userInfo"]["userPassword"] = userLogInViewModel.Password;
                            Response.Cookies["userInfo"]["lastVisit"] = DateTime.Now.ToString();
                           // Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(1);
                            Response.Cookies["domain"].Domain = "http://localhost/StudentProject/";

                        }
                        if (Session["EmailInfo"] != null)
                        {
                            // return RedirectToAction("Index", "Default");

                            return View("~/Views/UserLogIn/UserDashBoard.cshtml");

                        }
                        else
                        {
                            return RedirectToAction("UserLogIn");
                        }
                    }
                    else
                    {
                        return RedirectToAction("DeActivatedPage");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email or Password is Worng. Please check. ");
                   // return RedirectToAction("UserLogIn");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception source: {0} sorry to show list", ex.Message);
            }
            return View(userLogInViewModel);
        }
        //[HttpGet]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}
        //[NonAction]
        //public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        //{
        //    var verifyUrl = "/User/" + emailFor + "/" + activationCode;
        //    var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

        //    var fromEmail = new MailAddress("hitestmore.systematix@gmail.com", "hitesh@2018");
        //    var toEmail = new MailAddress(emailID);
        //    var fromEmailPassword = "******"; // Replace with actual password

        //    string subject = "";
        //    string body = "";
        //    if (emailFor == "VerifyAccount")
        //    {
        //        subject = "Your account is successfully created!";
        //        body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
        //            " successfully created. Please click on the below link to verify your account" +
        //            " <br/><br/><a href='" + link + "'>" + link + "</a> ";
        //    }
        //    else if (emailFor == "ResetPassword")
        //    {
        //        subject = "Reset Password";
        //        body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
        //            "<br/><br/><a href=" + link + ">Reset Password link</a>";
        //    }


        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false,
        //        Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
        //    };

        //    using (var message = new MailMessage(fromEmail, toEmail)
        //    {
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    })
        //        smtp.Send(message);
        //}

        //[HttpPost]
        //public ActionResult ForgotPassword(string Email)
        //{
        //    //Verify Email ID
        //    //Generate Reset password link 
        //    //Send Email 
        //    string message = "";
        //    bool status = false;


        //        var account = db.UserRegistrations.Where(a => a.Email == Email).FirstOrDefault();
        //        if (account != null)
        //        {
        //            //Send email for reset password
        //            string resetCode = Guid.NewGuid().ToString();
        //            SendVerificationLinkEmail(account.Email, resetCode, "ResetPassword");
        //            //account.ResetPasswordCode = resetCode;
        //            //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
        //            //in our model class in part 1
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.SaveChanges();
        //            message = "Reset password link has been sent to your email id.";
        //        }
        //        else
        //        {
        //            message = "Account not found";
        //        }

        //    ViewBag.Message = message;
        //    return View();
        //}
        
    /// <summary>
    /// this for forgot password 
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
        [HttpGet]
        public ActionResult ForgotPassword(string Email)
        {
            ForgotPasswordViewModel forgetPasswordViewModel = new ForgotPasswordViewModel();
            forgetPasswordViewModel.Email = Email;
            
            return View(forgetPasswordViewModel);
        }
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgetPasswordViewModel)
        {
            try
            {
                var objUserRegistration = db.UserRegistrations.Where(val => val.Email == forgetPasswordViewModel.Email).SingleOrDefault();
                //var result= (from item in db.UserRegistrations
                //             join c in db.SecurityTables on item.UserId equals c.UserId
                if (objUserRegistration != null)
                {
                    var temp = db.SecurityTables.Where(val => val.UserId == objUserRegistration.UserId).ToList();
                    if (temp != null)
                    {
                        var finalResult=db.SecurityTables.Where(val => val.SecurityQuestion == forgetPasswordViewModel.SecurityPIN).SingleOrDefault();

                        if(finalResult!=null)
                        {

                            return RedirectToAction("ResetPassword",new {Email=forgetPasswordViewModel.Email});

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }
           
            return RedirectToAction("ForgotPassword");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword(string Email)
        {
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel();
            resetPasswordViewModel.Email = Email;
            return View(resetPasswordViewModel);
        }
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            try
            {
                var result = db.UserRegistrations.Where(val => val.Email == resetPasswordViewModel.Email).FirstOrDefault();
                result.Password = Convert.ToBase64String(System.Security.Cryptography.SHA256.Create()
                                                       .ComputeHash(Encoding.UTF8.GetBytes(resetPasswordViewModel.Password)));
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception source: {0}", ex.Message);
            }

            return RedirectToAction("UserLogIn");
        }
        public ActionResult DeActivatedPage()
        {
            return View();
        }

        public ActionResult UserDashboard()
        {
            return View();
        }
    }

}