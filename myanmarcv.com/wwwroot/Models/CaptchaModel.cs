using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel;

namespace Uco.Models
{
    public class Captcha
    {
        [Display(Name = "", Order = 20)]
        [Remote("ValidateCaptcha", "Captcha", "", ErrorMessage = "Error in what you see")]
        [Required(ErrorMessage = "Required")]
        public virtual string CaptchaValue { get; set; }
        public Captcha()
        {

        }
    }

    public class InvisibleCaptcha
    {
        [Display(Name = "InvisibleCaptcha", Order = 20)]
        [Remote("ValidateInvisibleCaptcha", "Captcha", "", ErrorMessage = "ErrorMessage")]
        public virtual string InvisibleCaptchaValue { get; set; }
        public InvisibleCaptcha()
        {

        }
    }
}