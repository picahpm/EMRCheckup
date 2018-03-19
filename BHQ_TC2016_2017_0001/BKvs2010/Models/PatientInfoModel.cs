using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Models
{
    public class PatientInfoModel
    {
        public string hn { get; set; }
        public string en { get; set; }
        public string fullname { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string address { get; set; }
        private Image _picture;
        public Image picture
        {
            get { return _picture == null ? Properties.Resources.no_image : _picture; }
            set { _picture = value; }
        }
        public string allergy { get; set; }
        public string nationality { get; set; }
        public string visit_date { get; set; }
        public string visit_time { get; set; }
    }
}
