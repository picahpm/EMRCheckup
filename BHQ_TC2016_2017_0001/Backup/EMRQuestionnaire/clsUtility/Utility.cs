using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.IO;
using System.Text;
using QuestionnaireWebSite.clsExecuteSQL;

namespace QuestionnaireWebSite.clsUtility
{
    public class Utility
    {
        executeDC clsEX = new executeDC();
        DataTable dtMasLabel = new DataTable();
        public string GET_USER_ID_RESULT()
        {
            string u_id = RandomString(200, true);
            while (getUserDetails(u_id).Rows.Count > 1)
            {
                u_id = RandomString(200, true);

            }
            return u_id;
        }
        public string resultAllergy(DataTable dtAllergy)
        {
            string resultAllergy = string.Empty;
            for (int i = 0; i < dtAllergy.Rows.Count; i++)
            {
                if (i < dtAllergy.Rows.Count-1)
                {
                    resultAllergy += dtAllergy.Rows[i]["Allergy"].ToString() + ",";
                }
                else { resultAllergy += dtAllergy.Rows[i]["Allergy"].ToString(); }
            }
            return resultAllergy;
        }

        private DataTable getUserDetails(string u_id)
        {
            return dtMasLabel = clsEX.get_mas_patient_from_BHQ(u_id);
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string getLabelDt(DataRow[] result)
        {
            string total = string.Empty;
            try
            {

                return total = (result[0][1]).ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return total;
        }

        public static string ConvertDateToString(DateTime sDate, string spliter)
        {
            if (sDate.Equals(DBNull.Value))
            {
                return null;
            }
            return ConvertDateToString((Nullable<DateTime>)sDate);
        }
        public static string ConvertDateToString(Nullable<DateTime> sDate)
        {
            if (!sDate.HasValue)
            {
                return null;
            }
            string dtStr = string.Empty;
            dtStr = ConvertDateToString((Nullable<DateTime>)sDate, "/");
            return dtStr;
        }
        public static string ConvertDateToString(object sDate)
        {
            if (sDate == null)
            {
                return null;
            }
            string dtStr = string.Empty;
            dtStr = ConvertDateToString((Nullable<DateTime>)sDate, "/");
            return dtStr;
        }
        public static string ConvertDateToString(Nullable<DateTime> sDate, string spliter)
        {
            if (!sDate.HasValue)
            {
                return null;
            }
            string dtStr = string.Empty;
            dtStr = string.Format("{0:00}", sDate.Value.Day) + spliter + string.Format("{0:00}", sDate.Value.Month) + spliter + string.Format("{0:0000}", sDate.Value.Year + 543);
            return dtStr;
        }
        public static string ConvertDateToString(DBNull sDate)
        {
            return null;
        }

        #region "Generate Javascripts"
        public static void AlertAndRedirect(Page ePage, string customMsg, string strRedirect)
        {
            string clnScriptName = "Alert_Redirect";
            Type ClscriptType = ePage.GetType();
            string strTag = "";
            customMsg = customMsg.Replace("'", "\'");
            customMsg = customMsg.Replace(";", "-");
            strTag += "alert('" + customMsg + "');";
            strTag += "window.location.replace('" + strRedirect.Trim() + "');";
            ePage.ClientScript.RegisterStartupScript(ePage.GetType(), clnScriptName, strTag, true);

        }
        //public static void Alert(ref Page ePage, ref string customMsg)
        //{
        //    string clnScriptName = "Alert_Scripts";
        //    Type ClscriptType = ePage.GetType();
        //    string strTag = "";
        //    customMsg = customMsg.Replace("'", "\'");
        //    customMsg = customMsg.Replace(";", "-");
        //    strTag += "alert('" + customMsg + "');";
        //    ePage.ClientScript.RegisterStartupScript(ePage.GetType(), clnScriptName, strTag, true);

        //}
        public static void Alert(Page ePage, string customMsg)
        {
            string clnScriptName = "Alert_Scripts";
            Type ClscriptType = ePage.GetType();
            string strTag = "";
            customMsg = customMsg.Replace("'", "\'");
            customMsg = customMsg.Replace(";", "-");
            strTag += "alert('" + customMsg + "');";
            ePage.ClientScript.RegisterStartupScript(ePage.GetType(), clnScriptName, strTag, true);
        }

        public static string FormatCurrecyString(object str)
        {
            string ret = string.Empty;
            if (str == DBNull.Value)
            {
                ret = "0.00";
            }
            else
            {
                ret = (Convert.ToDouble(str)).ToString("#,##0.00");
            }
            return ret;
        }
        public void ExportDataTableToExcel(DataTable ds, string ptFilename)
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object
            response.Clear();
            response.Charset = "UTF-8";

            // set the response mime type for excel

            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            response.ContentType = "application/vnd.ms-excel";
            //response.ContentType = "application/vnd.ppt";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + ptFilename + "\"");

            // create a string writer
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds;
                    dg.DataBind();
                    dg.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                    response.Clear();
                }
            }
        }
        //Open file in to a filestream and read data in a byte array.
        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        public static Nullable<decimal> CheckStringtoID(string obj)
        {
            Nullable<decimal> id;
            if (string.IsNullOrEmpty(obj))
            {
                id = null;
            }
            else
            {
                id = Convert.ToDecimal(obj);
            }

            return id;
        }
        public static string CheckIdtoString(object obj)
        {
            string id = string.Empty;
            if (obj == DBNull.Value || obj == null)
            {
                id = "";
            }
            else
            {
                id = obj.ToString();
            }
            return id;
        }
        public static Nullable<decimal> CheckDBNull(object obj)
        {
            Nullable<decimal> id;
            if (obj == DBNull.Value)
            {
                id = null;
            }
            else
            {
                id = Convert.ToDecimal(obj);
            }
            return id;
        }
        public static Nullable<DateTime> CheckDBNullToDatetime(object obj)
        {
            Nullable<DateTime> id;
            if (obj == DBNull.Value)
            {
                id = null;
            }
            else
            {
                id = Convert.ToDateTime(obj);
            }
            return id;
        }
        public static string CheckDBNullStr(object obj)
        {
            string ret;
            if (obj == DBNull.Value)
            {
                ret = null;
            }
            else
            {
                ret = (string)obj;
            }
            return ret;
        }


        public static string FormatIntString(object amount)
        {
            string ret = string.Empty;
            if (amount == DBNull.Value)
            {
                ret = "0";
            }
            else
            {
                ret = (Convert.ToInt32(amount)).ToString("#,##0");
            }
            return ret;
        }
        public static string FormatIntAllowEmptyString(object amount)
        {
            string ret = string.Empty;
            if (amount == DBNull.Value)
            {
                ret = "";
            }
            else
            {
                ret = (Convert.ToInt32(amount)).ToString("#,##0");
            }
            return ret;
        }
        public static string FormatDecimalAllowEmptyString(object amount)
        {
            string ret = string.Empty;
            if (amount == DBNull.Value)
            {
                ret = "";
            }
            else
            {
                ret = (Convert.ToDecimal(amount)).ToString("#,##0");
            }
            return ret;
        }
        public static string FormatDateToString(object date)
        {
            string ret = string.Empty;
            if (date == DBNull.Value)
            {
                ret = "";
            }
            else
            {
                ret = Utility.ConvertDateToString(date);
            }
            return ret;
        }

        #endregion
        public String ConvertDateToStringFormat(String dateTimeValue, String format)
        {
            try
            {
                string returnStr = "";
                if (dateTimeValue != null && !dateTimeValue.Equals(""))
                {
                    String dateTimeStr = "";
                    String[] dateTimeSplit = dateTimeValue.Split(new Char[] { ' ' });
                    String dateStr = "";
                    String dayStr = "";
                    String monthStr = "";
                    String yearStr = "";
                    String timeStr = "";
                    String hourStr = "00";
                    String minuteStr = "00";
                    String secondStr = "00";
                    if (dateTimeSplit.Length > 0)
                    {
                        dateStr = dateTimeSplit[0];
                        String[] dateSplit = dateStr.Split(new Char[] { '-', '/' });
                        if (dateSplit.Length > 0)
                        {
                            dayStr = dateSplit[0];
                            monthStr = dateSplit[1];
                            yearStr = dateSplit[2];
                            if (Convert.ToInt32(yearStr) > 2400)
                            {
                                yearStr = (Convert.ToInt32(yearStr) - 543).ToString();
                            }
                        }
                        if (dateTimeSplit.Length > 1)
                        {
                            timeStr = dateTimeSplit[1];
                            String[] timeSplit = timeStr.Split(new Char[] { ':' });
                            if (timeSplit.Length > 0)
                            {
                                hourStr = timeSplit[0];
                                minuteStr = timeSplit[1];
                                secondStr = timeSplit[2];
                            }
                        }
                        switch (format)
                        {
                            case "yyyy/MM/dd HH:mm:ss":
                                dateTimeStr = yearStr + "/" + Right("00" + monthStr, 2) + "/" + Right("00" + dayStr, 2) + " " + hourStr + ":" + minuteStr + ":" + secondStr;
                                break;
                            case "yyyy/MM/dd":
                                dateTimeStr = yearStr + "/" + Right("00" + monthStr, 2) + "/" + Right("00" + dayStr, 2);
                                break;
                            case "yyyy-MM-dd HH:mm:ss":
                                dateTimeStr = yearStr + "-" + Right("00" + monthStr, 2) + "-" + Right("00" + dayStr, 2) + " " + hourStr + ":" + minuteStr + ":" + secondStr;
                                break;
                            case "yyyy-MM-dd":
                                dateTimeStr = yearStr + "-" + Right("00" + monthStr, 2) + "-" + Right("00" + dayStr, 2);
                                break;
                            case "dd/MM/yyyy HH:mm:ss":
                                dateTimeStr = Right("00" + dayStr, 2) + "/" + Right("00" + monthStr, 2) + "/" + yearStr + " " + hourStr + ":" + minuteStr + ":" + secondStr;
                                break;
                            case "dd/MM/yyyy":
                                dateTimeStr = Right("00" + dayStr, 2) + "/" + Right("00" + monthStr, 2) + "/" + yearStr;
                                break;
                            case "dd-MM-yyyy HH:mm:ss":
                                dateTimeStr = Right("00" + dayStr, 2) + "-" + Right("00" + monthStr, 2) + "-" + yearStr + " " + hourStr + ":" + minuteStr + ":" + secondStr;
                                break;
                            case "dd-MM-yyyy":
                                dateTimeStr = Right("00" + dayStr, 2) + "-" + Right("00" + monthStr, 2) + "-" + yearStr;
                                break;
                        }
                    }
                    returnStr = dateTimeStr;

                }

                return returnStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String Right(String value, int length)
        {
            String returnStr = "";
            returnStr = value.Substring(value.Length - length, length);
            return returnStr;
        }

        public String Left(String value, int length)
        {
            String returnStr = "";
            returnStr = value.Substring(0, length);
            return returnStr;
        }
        public double retValueToDouble(string valueInput)
        {
            double ret = 0.0;
            try
            {

                if (valueInput != string.Empty)
                {
                    ret = Convert.ToDouble(valueInput);
                }
                else
                {
                    ret = 0.0;
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return ret;
        }
        public char retValueToChar(string valueInput)
        {
            char ret = ' ';
            try
            {
                if (valueInput != string.Empty)
                {
                    ret = Convert.ToChar(valueInput);
                }
                else
                {
                    ret = ' ';
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return ret;
        }
        public string retValueToString(string valueInput)
        {
            string ret = string.Empty;
            try
            {
                if (valueInput != string.Empty)
                {
                    ret = Convert.ToString(valueInput);
                }
                else
                {
                    ret = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return ret;
        }
        public bool retValueToBoolean(string valueInput)
        {
            bool ret = false;
            try
            {
                if (valueInput == "on")
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return ret;
        }
    }
}