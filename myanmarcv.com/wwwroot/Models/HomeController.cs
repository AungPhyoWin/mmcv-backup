using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Uco.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        [HttpPost]
        public ActionResult Index(string CaptchaValue, string InvisibleCaptchaValue)
        {
            bool cv = CaptchaController.IsValidCaptchaValue(CaptchaValue.ToUpper());
            bool icv = InvisibleCaptchaValue == "";

            if (!cv || !icv)
            {
                ModelState.AddModelError(string.Empty, "Captcha error.");
                return View();
            }

            if (ModelState.IsValid)
            {
                // do work

                return View();
            }
            else return View();
        }
    }
}
