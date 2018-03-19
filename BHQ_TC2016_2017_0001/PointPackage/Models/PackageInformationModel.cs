using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointPackage.Models
{
    public partial class PackageInformationModel
    {
        public PackageInformationModel()
        {
            listPackageInformation = new List<PackageInformation>();
        }

        public string string_search { get; set; }
        public List<PackageInformation> listPackageInformation { get; set; }
    }
    public partial class PackageInformation
    {
        public string package_code { get; set; }
        public string package_name { get; set; }
    }
}