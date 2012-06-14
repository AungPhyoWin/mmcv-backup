using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Threading;
using MyanmarCV.Models;

namespace MyanmarCV.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn
        private mmCVDataClassesDataContext db2 = new mmCVDataClassesDataContext();

        public struct EmailContents
        {
            public string To;
            public string FromName;
            public string FromEmailAddress;
            public string Subject;
            public string Body;
            public string ToName;
            public string Password;
            public string webHostLink;
        }
        public ActionResult test2()
        {
            return View();
        }
        public ActionResult home()
        {
           // from dt in db2
           
            return View();
        }
        public PartialViewResult LatestReview()
        {
            Thread.Sleep(2000);
            var apw = "test";
            return PartialView("_Review", apw);
        
        }

        public ActionResult jobHome()
        {
            ViewBag.Accounting = GetNumberOfJob(1);
            ViewBag.Admin = GetNumberOfJob(2);
            ViewBag.Banking = GetNumberOfJob(3);
            ViewBag.Beauty = GetNumberOfJob(4);
            ViewBag.Building = GetNumberOfJob(5);
            ViewBag.Design = GetNumberOfJob(6);
            ViewBag.Education = GetNumberOfJob(7);
            ViewBag.Engineering = GetNumberOfJob(8);
            ViewBag.Hospitality = GetNumberOfJob(9);
            ViewBag.IT = GetNumberOfJob(10);
            ViewBag.Insurance = GetNumberOfJob(11);
            ViewBag.Management = GetNumberOfJob(12);
            ViewBag.Manufacturing = GetNumberOfJob(13);
            ViewBag.Marketing = GetNumberOfJob(14);
            ViewBag.Advertising = GetNumberOfJob(15);
            ViewBag.Medical = GetNumberOfJob(16);
            ViewBag.Property = GetNumberOfJob(19);
            ViewBag.Transportation = GetNumberOfJob(23);
            ViewBag.sales = GetNumberOfJob(21);
            ViewBag.telecommunication = GetNumberOfJob(22);
            ViewBag.Others = GetNumberOfJob(24);
            return View();
        }
        public ActionResult test3()
        {
            ViewBag.DateTime = DateTime.Now.ToString();
            return View();
        }
        public ActionResult ourlocation()
        {
            return View();
        }
        public int GetNumberOfJob(int jobType)
        {
            int numberOfJobs = (from dt in db2.jobAdvertisments where dt.jobTypeId == jobType select dt).Count();
            return numberOfJobs;
        }

        public ActionResult contactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult contactUs(contactUs conUs)
        {
           
            if (ModelState.IsValid)
            {
                //conUs.employerid = Guid.NewGuid();
                //empRegistration.createdDate = DateTime.Now;
                contactUs dbConUs = new contactUs();
                dbConUs.requestId = Guid.NewGuid(); //conUs.requestId;
                dbConUs.name = conUs.name;
                dbConUs.email = conUs.email;
                dbConUs.subject = conUs.subject;
                dbConUs.description = conUs.description;
                dbConUs.createdDate = DateTime.Now;
                dbConUs.modifiedDate = DateTime.Now;
                db2.contactUs.InsertOnSubmit(dbConUs);
                db2.SubmitChanges();

                //return RedirectToAction("../Employer/", new { empid = employerid });
                return RedirectToAction("submitSuccessful");
            }
            return View();
        }
        public ActionResult submitSuccessful()
        {

            return View();
        }
        public ActionResult test()
        {
            string path = Server.MapPath("~/pdf/file2.csv");
            StreamReader sr = new StreamReader(path);
            // for set encoding
            // StreamReader sr = new StreamReader(@"file.csv", Encoding.GetEncoding(1250));

            string strline = "";
            string[] _values = null;
            int x = 0;


            while (!sr.EndOfStream)
            {
                x++;
                strline = sr.ReadLine();
                _values = strline.Split(',');

                for (int i = 0; i < _values.Length; i++)
                {
                    if (_values[i].Trim().Length > 0)
                    {
                        subscribedEmail se = new subscribedEmail();
                        se.createdDate = DateTime.Now;
                        se.email = _values[i];
                        //se.name = subEmail.name;
                        //se.mobileNo = subEmail.phoneNumber;


                        db2.subscribedEmails.InsertOnSubmit(se);
                        db2.SubmitChanges();
                    }
                }
                //if (_values[0].Trim().Length > 0)
                //{
                //    //MessageBox.show(_values[1]);
                //}
            }
            sr.Close();
            return View();
        }

        public ActionResult employerRegistration()
        {
            ViewBag.typeOfCompanyId = new SelectList(db2.typeOfCompanyDefs, "typeOfCompanyId", "typeOfCompanyName");
            ViewBag.industrySectorId = new SelectList(db2.industrySectors, "industrySectorId", "industrySectorName");
            ViewBag.countryId = new SelectList(db2.countryDefs, "countryId", "countryName");
            return View();
        }
        
        [HttpPost]
        public ActionResult employerRegistration(employerRegistration empRegistration)
        {
            string employerid = "";
            if (ModelState.IsValid)
            {
                empRegistration.employerid = Guid.NewGuid();
                empRegistration.createdDate = DateTime.Now;
                db2.employerRegistrations.InsertOnSubmit(empRegistration);
                db2.SubmitChanges();

                employerid = (from dt in db2.employerRegistrations where dt.employerLoginName == empRegistration.employerLoginName && dt.password == empRegistration.password select dt.employerid.ToString()).SingleOrDefault();
                Session["empUserName"] = empRegistration.employerLoginName;
            }
            
            //return View();
            if (!(employerid.Equals(null)))
            {
                return RedirectToAction("../Employer/JobPost/", new { empid = employerid });
            }
            return View(empRegistration);
        }

        public ActionResult ViewLatestJob(string jobTypeId,string pageNo)
        {
            pageNo = pageNo + "0";
            int skipNo;
            int intJobTypeId;
            if (jobTypeId == "" || jobTypeId == null)
            {
                skipNo = Convert.ToInt32(pageNo);
                List<jobAdvertisment> dbJobAdvertList = (from dt in db2.jobAdvertisments select dt).OrderByDescending(d => d.modifiedDate).Skip(skipNo).Take(10).ToList();
                return View(dbJobAdvertList);
            }
            else
            {
                skipNo = Convert.ToInt32(pageNo);
                intJobTypeId = Convert.ToInt32(jobTypeId);
                List<jobAdvertisment> dbJobAdvertList = (from dt in db2.jobAdvertisments where dt.jobTypeId == intJobTypeId select dt).OrderByDescending(d => d.modifiedDate).Skip(skipNo).Take(10).ToList();
                return View(dbJobAdvertList);
            }
            //return View();
        }



        public ActionResult ViewLatestJobDetail(string advId)
        {
            if (advId != "")
            {
                //int intAdvId = Convert.ToInt32(advId);
                jobAdvertisment dbJobAdv = (from dt in db2.jobAdvertisments where dt.jobAdvertismentId == advId select dt).SingleOrDefault();

                //jobAdvertisment jobAdv = new jobAdvertisment();
               // jobAdv.viewCount = dbJobAdv.viewCount.GetValueOrDefault() + 1;
                dbJobAdv.viewCount = dbJobAdv.viewCount.GetValueOrDefault() + 1;
                db2.SubmitChanges();
                ViewBag.JobTypeId = dbJobAdv.jobTypeId;
                ViewBag.JobType = (from dt in db2.jobTypeDefs where dt.jobTypeId == dbJobAdv.jobTypeId select dt.jobTypeName).SingleOrDefault();
                ViewBag.EmployerName = (from dt in db2.employerRegistrations where dt.employerid == dbJobAdv.employerid select dt.companyName).SingleOrDefault();
                return View(dbJobAdv);
            }
            return View();
        }

        public ActionResult forgetPassword()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult forgetPassword(string email)
        {
            
            //bool cv = CaptchaController.IsValidCaptchaValue(CaptchaValue.ToUpper());
            //bool icv = InvisibleCaptchaValue == "";
            //if (!cv || !icv)
            //{
            //    ModelState.AddModelError(string.Empty, "Captcha error.");
            //    return View();
            //}

            if (ModelState.IsValid)
            {
                personalDetail pDetail = (from dt in db2.personalDetails where dt.email == email.Trim() select dt).FirstOrDefault();
                if (pDetail != null)
                {
                    EmailContents emcon = new EmailContents();
                    emcon.To = email.Trim();
                    emcon.ToName = pDetail.name;
                    emcon.Password = pDetail.password;
                    emcon.FromName = "Myanmarcv.com. Pte.Ltd";
                    emcon.FromEmailAddress = "blast@myanmarcv.com";
                    Send(emcon);

                    return RedirectToAction("passwordRecoveryMessage", new { msg = "yesemail" });
                }
                else
                {
                    return RedirectToAction("passwordRecoveryMessage", new { msg = "noemail" });
                }

               
            }

            return View();
        }

        public ActionResult passwordRecoveryMessage(string msg,int id=0)
        {
            if (msg.Equals("noemail"))
            {
                ViewData.Add("ErrorMessage", "<span style='color:red;'> Your account cannot be found for password recovery. </span><br/> Please click <a href='../Account/forgetpassword'> the link </a> to try again.");
            }

            else if (msg.Equals("yesemail"))
            {
                ViewData.Add("ErrorMessage", "<span style='color:red;'>Your login Id and password will be sent to your email in shortly.</span> ");
            }
            return View();
        }
        

        public bool Send(EmailContents emailcontents)
        {
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            
            
            smtpClient.Credentials = new System.Net.NetworkCredential("myanmarcv.comrecovery@gmail.com", "mmcv@dmin");
            
            StringBuilder sb = new StringBuilder();
            string strhostname = System.Net.Dns.GetHostName();

            sb.AppendLine("<div>Dear " + emailcontents.ToName + ",");
            sb.Append("<br /> <br />");
            sb.Append(" Below is your personal details for password recovery. <br />");
            sb.Append("Login ID : ");
            sb.Append(emailcontents.To);
            sb.Append("<br />");
            sb.Append("Password : ");
            sb.Append(emailcontents.Password);
            sb.Append("<br />");
            sb.Append("Please click the link for login : <span style='text-decoration:underline;'>" + "<a href="+"http://www.myanmarcv.com/account/forgetpassword>" +" www.myanmarcv.com" + "</a>"+"</span><br/>");
            sb.Append("Thank you so much. <br/>");
            sb.Append(emailcontents.FromName);
            sb.Append("</div>");
            //string fullEmailContent = emailcontents.Body;
            //fullEmailContent = fullEmailContent.Replace("#raterName#", emailcontents.ToName);
            //fullEmailContent = fullEmailContent.Replace("#subjectName#", emailcontents.FromName);
            //fullEmailContent = fullEmailContent.Replace("#projectLink#", emailcontents.webHostLink + "raterHandler.ashx?InvitationID=" + emailcontents.InvitationCode);


            MailAddress from = new MailAddress(emailcontents.FromEmailAddress, emailcontents.FromName);
            MailAddress to = new MailAddress(emailcontents.To);

            MailMessage message = new MailMessage(from, to);

            message.Subject = "Password Recovery from MyanmarCV.com"; //emailcontents.Subject;
            message.Priority = MailPriority.High;
            message.IsBodyHtml = true;
            message.Body = sb.ToString(); //fullEmailContent; //sb.ToString(); //Utilities.FormatText(emailcontents.Body, true);

            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Subscribed()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Subscribed(SubscribedModel subEmail)
        {
            if (ModelState.IsValid)
            {
                subscribedEmail se = new subscribedEmail();
                se.createdDate = DateTime.Now;
                se.email = subEmail.email;
                se.name = subEmail.name;
                se.mobileNo = subEmail.phoneNumber;


                db2.subscribedEmails.InsertOnSubmit(se);
                db2.SubmitChanges();
                return RedirectToAction("thankyou");
            }
            else
            {
                return View();
            }
            
        }

        public ActionResult thankyou()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                int IsUserValidated = (from dt in db2.personalDetails where dt.email == model.email && dt.password == model.Password select dt).Count();
                if (IsUserValidated > 0)
                {
                    FormsAuthentication.SetAuthCookie(model.email, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                        
                    }
                     else
                    {
                        string email = model.email.ToString();
                        Session["userID"] = email;
                       // return RedirectToAction("Edit", "personalDetail", new { email = model.email, id = 0 });
                        return RedirectToAction("MyAccount", "personalDetail", new { email = model.email, id = 0 });
                       // return RedirectToAction("Index", "Home");
                    }
                }
                //if (Membership.ValidateUser(model.UserName, model.Password))
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                //    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                //        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //    {
                //        return Redirect(returnUrl);
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //}
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult EmployerLogin()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult EmployerLogin(LogOnModel model)
        {
             if (ModelState.IsValid)
            {   
                 
                employerRegistration empRegistration = (from dt in db2.employerRegistrations where dt.employerLoginName == model.email && dt.password == model.Password select dt).SingleOrDefault();
                if (empRegistration != null)
                {
                    Session["empUserName"] = model.email;
                    return RedirectToAction("JobPost", "Employer", new { employerId = empRegistration.employerid, id = 0 });
                }
                else
                {
                    ModelState.AddModelError("", "The employer ID name or password provided is incorrect.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
            
        }
        
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("logon", "Account");
        }

        //
        // GET: /Account/Register
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(personalDetail perDetail)
        {
            if (ModelState.IsValid)
            {
                int haveUserCount = (from dt in db2.personalDetails where dt.email == perDetail.email.Trim() select dt).Count();
                if (!(haveUserCount > 0))
                {
                    perDetail.createdDate = DateTime.Now;
                    db2.personalDetails.InsertOnSubmit(perDetail);
                    db2.SubmitChanges();
                    FormsAuthentication.SetAuthCookie(perDetail.email, false);
                    //return RedirectToAction("Create", "personalDetail", new { email = perDetail.email, id = 0 });
                    Session["userID"] = perDetail.email;
                    return RedirectToAction("Create", "personalDetail", new { email = perDetail.email, id = 0 });
                }
                else
                {
                    //call error message from model
                }
          }
            return View(perDetail);
        }
       

     

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(personalDetail perDetail)
        {
            if (ModelState.IsValid)
            {
                int haveUserCount = 0; //(from dt in db2.personalDetails where dt.email == perDetail.email.Trim() select dt).Count();
                if (!(haveUserCount > 0))
                {
                    perDetail.createdDate = DateTime.Now;
                   // db2.personalDetails.InsertOnSubmit(perDetail);
                    //db2.SubmitChanges();

                    FormsAuthentication.SetAuthCookie( perDetail.email,false);
                  
                   // return RedirectToAction("Create","personalDetail" );
                    return RedirectToAction("Create","personalDetail",new {email = perDetail.email, id = 0 });
                 }
                else
                { 
                    //call error message from model

                }
                // Attempt to register the user
                //MembershipCreateStatus createStatus;
                //Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //if (createStatus == MembershipCreateStatus.Success)
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                //}

            }

            // If we got this far, something failed, redisplay form
            return View(perDetail);
            
        }

        //
        // GET: /Account/ChangePassword

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            FooEditModel model = new FooEditModel
                                        {
                                            Foo = new Foo
                                            {
                                                Name = "Stuart",
                                                Date = new DateTime(2010, 12, 16),
                                                //Date2 = new DateTime(2010, 12, 16),
                                                //Date3 = new DateTime(2010, 12, 16),
                                                //Date4 = new DateTime(2010, 12, 16),

                                            }
                                        };
            return View(model);
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
