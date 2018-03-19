using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CorporateSummaryReport.Models
{
    public partial class LoginModel
    {
        public LoginModel()
        {

        }

        public string username { get; set; }
        public string password { get; set; }
    }

    public partial class Criterias
    {
        public Criterias()
        {
            DateTime dateNow = DateTime.Now;
            startdate = dateNow;
            enddate = dateNow;
        }

        public string companyname { get; set; }
        public string sub_companyname { get; set; }

        public int patients { get; set; }
        public int totalservice { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startdate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime enddate { get; set; }

    }

    public partial class SelectReportModel : Criterias
    {
        public List<SelectListItem> DropdownReport { get; set; }
        public int totalservice { get; set; }

      
    }

    public partial class SearchModel : Criterias 
    {
        public SearchModel()
        {
            listPatient = new List<patient>();
            dataListCompany = new List<string>();
        }

        public functionCriterias func { get; set; }
        public enum functionCriterias
        {
            funcSearch = 0,
            funcConfirm = 1
        }
        public int totalservice { get; set; }
        public List<patient> listPatient { get; set; }
        public List<string> dataListCompany { get; set; }
    }

    public class patient
    {
        public string hn { get; set; }
        public string en { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public DateTime arrived { get; set; }
    }
}