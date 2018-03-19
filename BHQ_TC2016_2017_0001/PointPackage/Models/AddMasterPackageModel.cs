using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointPackage.Models
{
    public partial class AddMasterPackageModel
    {
        public string string_search { get; set; }
        public string queue_no { get; set; }
        public int tpr_id { get; set; }
        public string package_code { get; set; }
        public string package_name { get; set; }
        public double point { get; set; }
    }
}