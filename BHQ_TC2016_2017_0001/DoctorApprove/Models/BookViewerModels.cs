using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorApprove.Models
{
    [HandleError]
    public class PatientSearchModels
    {
        public PatientSearchModels()
        {
            patientSearch = new List<PatientSearch>();
        }

        public class PatientSearch
        {
            public int tpr_id { get; set; }
            public string hn { get; set; }
            public string name { get; set; }
            public string status { get; set; }
            public int express { get; set; }
            public string rpt_code { get; set; }
        }

        public List<PatientSearch> patientSearch { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("HN/EN/Firstname/Lastname")]
        public string TextSearch { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Visit Date :")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateSearch { get { return _DateSearch; } set { _DateSearch = value; } }
        private DateTime? _DateSearch = null;

        private bool _isCheckDate = false;
        public bool isCheckDate { get { return _isCheckDate; } set { _isCheckDate = value; } }
    }
}