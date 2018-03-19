using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointPackage.Models
{
    public partial class ViewPackageModel
    {
        public ViewPackageModel()
        {
            listPackage = new List<Package>();
        }

        public string queue_no { get; set; }
        public int tpr_id { get; set; }
        public List<Package> listPackage { get; set; }
    }
    public partial class Package
    {
        public string package_code { get; set; }
        public string package_name { get; set; }
        public string package_old_point { get; set; }
    }
}