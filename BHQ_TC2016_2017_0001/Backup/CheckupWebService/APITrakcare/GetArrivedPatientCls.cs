using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupWebService.APITrakcare
{
    public class GetArrivedPatientCls
    {
        public GetPTArrivedResult ByGetPTArrivedCheckUpFilter(string location, string en, DateTime arrivedate)
        {
            try
            {

                return new GetPTArrivedResult();
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetArrivedPatientCls", "ByGetPTArrivedCheckUpFilter", ex.Message);
                throw ex;
            }
        }
    }

    public class GetPTArrivedResult
    {
        public string rowid { get; set; }
        public string hn { get; set; }
        public string en { get; set; }
        public DateTime? appointdate { get; set; }
        public DateTime? arrivaldate { get; set; }
        public int enrowid { get; set; }
        public string titlename { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string fullname { get; set; }
        public DateTime? admissiondate { get; set; }
        public string locationcode { get; set; }
        public string locationdesc { get; set; }
        public string vipcode { get; set; }
        public string vipdesc { get; set; }
        public int serviceid { get; set; }
        public string servicedesc { get; set; }
        public string nationcode { get; set; }
        public string nationdesc { get; set; }
        public string sexcode { get; set; }
        public string sexdesc { get; set; }
        public DateTime? dob { get; set; }
        public string dobtext { get; set; }
        public string photoname { get; set; }
        public string ageyears { get; set; }
        public string agemonths { get; set; }
        public string agedays { get; set; }
        public bool newpatient { get; set; }
        public string address { get; set; }
        public string tumbon { get; set; }
        public string amphur { get; set; }
        public string province { get; set; }
        public string zipcode { get; set; }
        public string idcard { get; set; }
        public string mobilephoneno { get; set; }
        public string officephoneno { get; set; }
        public string homephoneno { get; set; }
        public string marriedesc { get; set; }
        public string secondfullname { get; set; }
        public string secondfirstname { get; set; }
        public string secondmiddlename { get; set; }
        public string secondlastname { get; set; }
        public int prefl_rowid { get; set; }
        public string prefl_code { get; set; }
        public string prefl_desc { get; set; }
        public string anonycode { get; set; }
        public string anonydesc { get; set; }
    }
}