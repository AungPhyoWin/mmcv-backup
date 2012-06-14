using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace MyanmarCV.Controllers
{
    class loginModel
    {
        [Required]
        public string userName { get; set; }

        [Required]
        public string password { get; set; }
    }
}
