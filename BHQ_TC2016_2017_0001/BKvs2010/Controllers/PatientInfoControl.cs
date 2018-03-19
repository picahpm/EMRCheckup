using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using BKvs2010.Models;
using DBCheckup;

namespace BKvs2010.Controllers
{
    public class PatientInfoControl
    {
        public PatientInfoModel loadData(int? tpr_id)
        {
            PatientInfoModel model = new PatientInfoModel();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var data = cdc.trn_patient_regis
                        .Where(x => x.tpr_id == tpr_id)
                        .Select(x => new
                        {
                            hn = x.trn_patient.tpt_hn_no,
                            en = x.tpr_en_no,
                            fullname = x.trn_patient.tpt_othername,
                            gender = x.trn_patient.tpt_gender,
                            dob = x.trn_patient.tpt_dob,
                            dob_text = x.trn_patient.tpt_dob_text,
                            arrived_date = x.trn_patient_regis_detail.tpr_real_arrived_date,
                            address = x.tpr_other_address,
                            picture = x.trn_patient.tpt_image,
                            allerry = x.trn_patient.tpt_allergy,
                            nationality = x.trn_patient.tpt_nation_desc
                        }).FirstOrDefault();
                    if (data != null)
                    {
                        model.hn = data.hn;
                        model.en = data.en;
                        model.fullname = data.fullname;
                        model.gender = data.gender == 'M' ? "Male" : data.gender == 'F' ? "Female" : "Unknown";
                        model.dob = data.dob_text;
                        model.age = CalculateAge(data.dob.Value, data.arrived_date.Value);
                        model.picture = byteArrayToImage(data.picture);
                        model.address = data.address;
                        model.allergy = data.allerry;
                        model.visit_date = data.arrived_date.Value.ToString("dd MMMM yyyy");
                        model.visit_time = data.arrived_date.Value.ToString("hh:mm:ss");
                        model.nationality = data.nationality;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Models.PatientInfoModel", "loadData()", ex, false);
            }
            return model;
        }
        public PatientInfoModel RetrieveInfo(int? tpr_id)
        {
            if (tpr_id != null)
            {
                new APITrakcare.GetPatientInfoCls().GetInfo((int)tpr_id);
            }
            return loadData(tpr_id);
        }

        private string CalculateAge(DateTime startDate, DateTime endDate)
        {
            if (startDate == null || endDate == null)
            {
                return "";
            }
            if (startDate.Date > endDate.Date)
            {
                throw new ArgumentException("startDate cannot be higher then endDate", "startDate");
            }
            int years = endDate.Year - startDate.Year;
            int months = 0;
            int days = 0;

            // Check if the last year, was a full year.
            if (endDate < startDate.AddYears(years) && years != 0)
            { years--; }

            // Calculate the number of months.
            startDate = startDate.AddYears(years);
            if (startDate.Year == endDate.Year)
            { months = endDate.Month - startDate.Month; }
            else
            { months = (12 - startDate.Month) + endDate.Month; }

            // Check if last month was a complete month.
            if (endDate < startDate.AddMonths(months) && months != 0)
            { months--; }

            // Calculate the number of days.
            startDate = startDate.AddMonths(months);
            days = (endDate - startDate).Days;
            return string.Format("{0} Year(s) {1} Month(s) {2} Day(s)", years, months, days);
        }
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}

