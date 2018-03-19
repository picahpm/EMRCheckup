using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PointPackage.Models
{
    public class AddDummyModel
    {
        public AddDummyModel()
        {

        }

        public string queue_no { get; set; }
        public int tpr_id { get; set; }
        public double point { get; set; }
    }
}