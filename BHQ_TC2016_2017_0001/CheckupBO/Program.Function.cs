using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace CheckupBO
{
    public partial class Program
    {
        public static mst_user_type CurrentUser = null;

        public static DateTime GetServerDateTime()
        {
            using (DBCheckup.InhCheckupDataContext dbc = new DBCheckup.InhCheckupDataContext())
            {
                DateTime nowdatetime = Convert.ToDateTime(dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault());
                return nowdatetime;
            }
        }
        public static void Logout()
        {
            if ( Program.CurrentUser != null)
            {
                InhCheckupDataContext dbc = new InhCheckupDataContext();
                log_user_login currentloguserlogin = (from t1 in dbc.log_user_logins
                                                      where t1.mut_id == Program.CurrentUser.mut_id
                                                       && t1.lug_end_date == null
                                                      select t1).FirstOrDefault();
                if (currentloguserlogin != null)
                {
                    currentloguserlogin.lug_end_date = Program.GetServerDateTime();
                }
                mst_user_type CurrentUser = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                CurrentUser.mut_login_status = '0';
                dbc.SubmitChanges();
            }
            Program.CurrentUser = null;

        }

        public static void MessageError(String strError)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                log_transaction objlog = new log_transaction();
                objlog.its_user_by = Program.CurrentUser.mut_username;
                objlog.its_program = "BackOffice";
                objlog.its_event = "";
                objlog.its_err_code = "";
                objlog.its_err_msg = strError;
                objlog.its_err_date = Program.GetServerDateTime();
                dbc.log_transactions.InsertOnSubmit(objlog);
                dbc.SubmitChanges();
            }
            MessageBox.Show("Error :" + strError, "Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void MessageError(String strEvent, String ErrorCode, String strError)
        {
            MessageError(strEvent, ErrorCode, strError, true);
        }
        public static void MessageError(String strEvent, String ErrorCode, String strError, Boolean isShowMsg)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = Program.CurrentUser.mut_username;
                    objlog.its_program = "BackOffice";
                    objlog.its_event = strEvent;
                    objlog.its_err_code = ErrorCode;
                    objlog.its_err_msg = strError;
                    objlog.its_err_date = Program.GetServerDateTime();
                    dbc.log_transactions.InsertOnSubmit(objlog);
                    dbc.SubmitChanges();
                }
            }
            catch (Exception)
            {
                if (isShowMsg)
                {
                    MessageBox.Show("บันทึก Log Tranaction ไม่ได้", "Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (isShowMsg)
            {
                MessageBox.Show("BackOffice เกิดข้อผิดพลาดไม่สามารถทำงานต่อได้ กรุณาติดต่อผู้ดูแลระบบ " + Environment.NewLine + strError, "แจ้งเตือนปัญหาการใช้งาน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GetValueRadio(Panel GB)
        {
            foreach (Control cx in GB.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Checked)
                    {
                        if (RB.Tag == null)
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return RB.Tag.ToString();
                        }
                    }
                }
            }
            return "";
        }
        public static char? GetValueRadioTochar(Panel GB)
        {
            foreach (Control cx in GB.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Checked)
                    {
                        if (RB.Tag == null)
                        {
                            return null;
                        }
                        else
                        {
                            return Convert.ToChar(RB.Tag);
                        }

                    }
                }
            }
            return null;
        }

        public static void SetValueRadioGroup(Panel GB, string value)
        {
            foreach (Control cx in GB.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Tag.Equals(value))
                    {
                        RB.Checked = true; return;
                    }
                    else
                    {
                        RB.Checked = false;
                    }
                }
            }
        }
        public static void SetValueRadioGroup(Panel GB, char value)
        {
            foreach (Control cx in GB.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (((char)RB.Tag) == value)
                    {
                        RB.Checked = true; return;
                    }
                    else
                    {
                        RB.Checked = false;
                    }
                }
            }

        }
        public static void SetValueRadioGroup(Panel GB, object value)
        {
            if (value == null) { return; }
            foreach (Control cx in GB.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Tag.ToString() == value.ToString())
                    {
                        RB.Checked = true; return;
                    }
                    else
                    {
                        RB.Checked = false;
                    }
                }
            }
        }

        public static Boolean CheckIsConnection()
        {
            try
            {
                using (DBCheckup.InhCheckupDataContext dbc = new DBCheckup.InhCheckupDataContext())
                {
                    DateTime nowdatetime = Convert.ToDateTime(dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault());
                    return true;
                }
            }
            catch
            {
                Program.MessageError("Check Connection", "001", "Can not Connect DataBase SQL");
                return false;
            }
        }

        //Get Value CheckedListBox
        public static string GetCheckedListDisplayText(object itemChecked, CheckedListBox checklistdata)
        {
            try
            {
                string castedItem = itemChecked.ToString().Replace("{", "").Replace("}", "");
                string comapnycode = castedItem.Substring(0, castedItem.IndexOf(','));
                string comapnyName = castedItem.Substring(castedItem.IndexOf(','), castedItem.Length - castedItem.IndexOf(','));
                for (int i = 0; i < comapnyName.Length; i++)
                {
                    string[] dc = comapnyName.Split('=');
                    string XfieldName = dc[0].ToString().Replace(",", "");
                    string XValue = dc[1].ToString().Trim();
                    if (XfieldName.Trim() == checklistdata.DisplayMember)
                    {
                        return XValue; //break;
                    }
                }
            }
            catch (Exception)
            {

            }
            return "";
        }
        public static string GetCheckedListBoxDisplayText(int index, CheckedListBox checklistdata)
        {
            string castedItem = checklistdata.Items[index].ToString();
            return GetCheckedListDisplayText(castedItem, checklistdata);
        }
        public static string GetCheckedListBoxValue(object itemChecked, CheckedListBox checklistdata)
        {
            try
            {
                string castedItem = itemChecked.ToString().Replace("{", "").Replace("}", "");
                string comapnycode = castedItem.Substring(0, castedItem.IndexOf(','));
                for (int i = 0; i < comapnycode.Length; i++)
                {
                    string[] dc = comapnycode.Split('=');
                    string XfieldName = dc[0].ToString().ToString().Replace(",", "");
                    string XValue = dc[1].ToString().Trim();
                    if (XfieldName.Trim() == checklistdata.ValueMember)
                    {
                        return XValue; //break;
                    }
                }
            }
            catch (Exception)
            {
            }

            return "";
        }

        public static string GetCheckedListBoxValue(int index, CheckedListBox checklistdata)
        {
            string castedItem = checklistdata.Items[index].ToString();
            return GetCheckedListBoxValue(castedItem, checklistdata);
        }
        public static void SetCheckedListBox(CheckedListBox GB, string value)
        {
            for (int i = 0; i < GB.Items.Count; i++)
            {
                string valueitem = GetCheckedListBoxValue(i, GB);
                if (valueitem == value)
                {
                    GB.SetItemChecked(i, true); return;
                }
            }
        }
        public static void SetCheckedListBox(CheckedListBox GB, string value, bool data)
        {
            for (int i = 0; i < GB.Items.Count; i++)
            {
                string valueitem = GetCheckedListBoxValue(i, GB);
                if (valueitem == value)
                {
                    GB.SetItemChecked(i, data); return;
                }
            }
        }

        public static void SelectItem(ComboBox cmb, string value)
        {
            cmb.SelectedText = value;
        }
        public static String GetValueComboBox(ComboBox cmd)
        {
            try
            {
                DropdownDataString castedItem = (DropdownDataString)cmd.SelectedItem;
                return castedItem.Code.ToString();
            }
            catch (Exception)
            {
            }

            return "";
        }
        public static int? GetValueComboBoxInt(ComboBox cmd)
        {
            try
            {
                DropdownData castedItem = (DropdownData)cmd.SelectedItem;
                return Convert1.ToInt32(castedItem.Code);
            }
            catch (Exception)
            {
            }
            return null;
        }
        //Get value groupbox
        public static char? GetValueGroupBox(GroupBox GBName)
        {
            foreach (Control cx in GBName.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Checked)
                    {
                        if (RB.Tag == null)
                        {
                            return null;
                        }
                        else
                        {
                            return Convert.ToChar(RB.Tag);
                        }
                    }
                }
            }
            return null;
        }
        public static string GetValueGroupBoxStr(GroupBox GBName)
        {
            foreach (Control cx in GBName.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Checked)
                    {
                        if (RB.Tag == null)
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return RB.Tag.ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }

        public static void SetValueRadioInGroupBox(GroupBox GBName, char value)
        {
            if (value == null) { return; }
            foreach (Control cx in GBName.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (((char)RB.Tag) == value)
                    {
                        RB.Checked = true; return;
                    }
                    else
                    {
                        RB.Checked = false;
                    }
                }
            }
        }

        public static void SetValueRadioGroupBox(GroupBox GBName, object value)
        {
            if (value == null) { return; }
            foreach (Control cx in GBName.Controls)
            {
                if (cx is RadioButton)
                {
                    RadioButton RB = (RadioButton)cx;
                    if (RB.Tag.ToString() == value.ToString())
                    {
                        RB.Checked = true; return;
                    }
                    else
                    {
                        RB.Checked = false;
                    }
                }
            }
        }



        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string AssemblyVersion
        {
            get
            {
                try
                {
                    var currentVer =  System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return currentVer.Major.ToString() + "." + currentVer.Minor.ToString() + "." + currentVer.Build.ToString() + "." + currentVer.Revision.ToString();

                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
