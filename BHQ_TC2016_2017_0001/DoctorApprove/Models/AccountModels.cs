using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DBCheckup;

namespace DoctorApprove.Models
{
    #region Models
    public class LogOnModel
    {
        [Required]
        [DisplayName("User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string strMsg { get; set; }
    }
    #endregion
}
