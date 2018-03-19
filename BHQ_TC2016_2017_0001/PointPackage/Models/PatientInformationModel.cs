using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PointPackage.Models
{
    public partial class PatientInformationModel
    {
        public PatientInformationModel()
        {
            listPatientInformation = new List<PatientInformation>();
        }

        public string queue_no { get; set; }
        public List<PatientInformation> listPatientInformation { get; set; }
    }
    public partial class PatientInformation
    {
        public int tpr_id { get; set; }
        public string hn { get; set; }
        public string en { get; set; }
        public string name { get; set; }
        public string lastname { get; set; } 
        public DateTime arrived_date { get; set; }
    }
}