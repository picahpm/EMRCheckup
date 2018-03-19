using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Deployment.Application;
using setLogUser;
using DBCheckup;
using Microsoft.Win32;

namespace BKvs2010
{
    public static partial class Program
    {
        public static void MessageError(string strError)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                log_transaction objlog = new log_transaction();
                objlog.its_user_by = (Program.CurrentUser == null) ? "" : Program.CurrentUser.mut_username;
                objlog.its_program = (Program.CurrentRoom == null) ? "" : Program.CurrentRoom.mrd_ename;
                objlog.its_event = "";
                objlog.its_err_code = "";
                objlog.its_err_msg = strError;
                objlog.its_err_date = Program.GetServerDateTime();
                dbc.log_transactions.InsertOnSubmit(objlog);
                dbc.SubmitChanges();
            }
            MessageBox.Show("Error :" + strError, "Error Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void MessageError(string strEvent, string ErrorCode, string strError)
        {
            MessageError(strEvent, ErrorCode, strError, true);
        }
        public static void MessageError(string strEvent, string ErrorCode, string strError, bool isShowMsg)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = (Program.CurrentUser == null) ? "" : Program.CurrentUser.mut_username;
                    objlog.its_program = (Program.CurrentRoom == null) ? "" : Program.CurrentRoom.mrd_ename;
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
                MessageBox.Show("ระบบเกิดข้อผิดพลาดไม่สามารถทำงานต่อได้ กรุณาติดต่อผู้ดูแลระบบ " + Environment.NewLine + strError, "แจ้งเตือนปัญหาการใช้งาน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void MessageError(string strEvent, string ErrorCode, Exception ex, bool isShowMsg)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = (Program.CurrentUser == null) ? "" : Program.CurrentUser.mut_username;
                    objlog.its_program = (Program.CurrentRoom == null) ? "" : Program.CurrentRoom.mrd_ename;
                    objlog.its_tpr_id = (Program.CurrentRegis == null) ? 0 : Program.CurrentRegis.tpr_id;
                    objlog.its_event = strEvent;
                    objlog.its_err_code = ErrorCode;
                    objlog.its_err_msg = (ex == null) ? "" : ex.Message;
                    objlog.its_StackTrace = (ex == null) ? "" : ex.StackTrace;
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
                MessageBox.Show("ระบบเกิดข้อผิดพลาดไม่สามารถทำงานต่อได้ กรุณาติดต่อผู้ดูแลระบบ " + Environment.NewLine + ex.Message, "แจ้งเตือนปัญหาการใช้งาน", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DateTime GetServerDateTime()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                DateTime nowdatetime = Convert.ToDateTime(dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault());
                return nowdatetime;
            }
        }

        public static string GetLocalIP()
        {
            string _IP = null;

            // Resolves a host name or IP address to an IPHostEntry instance.
            // IPHostEntry - Provides a container class for Internet host address information. 
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            // IPAddress class contains the address of a computer on an IP network. 
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                // InterNetwork indicates that an IP version 4 address is expected 
                // when a Socket connects to an endpoint
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;
        }
        public static string GetAssemblyVersion()
        {
            try
            {
                var currentVer = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return currentVer.Major.ToString() + "." + currentVer.Minor.ToString() + "." + currentVer.Build.ToString() + "." + currentVer.Revision.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Boolean Login(string strUsername, int siteID, int roomID)
        {
            InhCheckupDataContext dbc = new InhCheckupDataContext();
            try
            {

                if (Program.CurrentUser != null)
                {//ตรวจสอบ Type หมอ หรือ อื่น เข้าห้องเกินจำนวนที่กำหนดหรือไม่
                    int countRoomLogin = (from t1 in dbc.log_user_logins
                                          where t1.mrd_id == roomID
                                          && t1.mut_id != CurrentUser.mut_id
                                          && t1.lug_end_date == null
                                          select t1).Count();
                    mst_room_dtl Currentroomdtl = (from t1 in dbc.mst_room_dtls where t1.mrd_id == roomID select t1).FirstOrDefault();
                    int limitUserinRoom = 0;
                    if (Program.CurrentUser.mut_type == 'D')
                    {//เป็นหมอ 
                        limitUserinRoom = Convert.ToInt32(Currentroomdtl.mst_room_hdr.mrm_count_doctor);
                    }
                    else
                    {//คนที่ไม่ใช่หมอ if (Program.CurrentUser.mut_type == 'O')
                        limitUserinRoom = Convert.ToInt32(Currentroomdtl.mst_room_hdr.mrm_count_person);
                        //if (Program.CurrentUser.mut_CanSendQueue==true)
                        //{// กรณีเป็น คนสามารถส่งQueue

                        //}
                    }

                    if (countRoomLogin + 1 > limitUserinRoom)
                    {
                        string msgalert = string.Format("ไม่สามารถ Login เข้า HPC Site :[{0}] Room :[{1}] นี้ได้เนื่องจากมีคนเข้ามาใช้เกินจากที่กำหนดไว้",
                                                    Currentroomdtl.mst_room_hdr.mst_hpc_site.mhs_ename,
                                                    Currentroomdtl.mrd_ename);
                        MessageBox.Show(msgalert, "Login Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Program.CurrentRoom = null;
                        Program.CurrentRegis = null;
                        return false;
                    }



                    //if (getLoginStatus(Program.CurrentUser.mut_id) == '1')
                    //{
                    bool Asking = Class.ClsManageUserLogin.checkAskingLoginRoom(roomID);
                    if (Asking)
                    {
                        log_user_login lug = getNotSignOut(Program.CurrentUser.mut_id).LastOrDefault();
                        string msgalert = string.Format("Username : [{0}] มีการ Login อยู่ที่ HPC:{1} Room : {2} คุณต้องการ Logout หรือไม่",
                                                    Program.CurrentUser.mut_username,
                                                    getSiteEnameByMrdID(lug.mrd_id),
                                                    getRoomEnameByMrdID(lug.mrd_id));
                        if (MessageBox.Show(msgalert, "Login Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //// Logout จากห้องเดิม
                            //log_user_login currentloguserlogin = (from t1 in dbc.log_user_logins
                            //                                      where t1.mrd_id == roomID
                            //                                      && t1.mut_id == Program.CurrentUser.mut_id
                            //                                      && t1.lug_end_date == null
                            //                                      select t1).FirstOrDefault();
                            //if (currentloguserlogin != null)
                            //{
                            //    currentloguserlogin.lug_end_date = Program.GetServerDateTime();
                            //}
                            //mst_user_type CurrentUser = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                            //CurrentUser.mut_login_status = '0';
                            //dbc.SubmitChanges();
                            Class.ClsManageUserLogin.kickLoginOldRoom(Program.CurrentUser.mut_id, roomID);
                        }
                        else
                        {
                            Program.CurrentRoom = null;
                            Program.CurrentRegis = null;
                            return false;
                        }
                    }
                    //}

                    mst_room_dtl curRoom = (from t1 in dbc.mst_room_dtls where t1.mrd_id == roomID select t1).FirstOrDefault();
                    curRoom.mrd_rm_status = 'N';
                    dbc.SubmitChanges();
                    Program.CurrentRoom = curRoom;

                    log_user_login newlog = new log_user_login();
                    newlog.mut_id = Program.CurrentUser.mut_id;
                    newlog.mrd_id = Program.CurrentRoom.mrd_id;
                    newlog.mhs_id = Program.CurrentSite.mhs_id;
                    newlog.lug_ip_address = Program.GetLocalIP();
                    newlog.lug_start_date = Program.GetServerDateTime();
                    dbc.log_user_logins.InsertOnSubmit(newlog);
                    dbc.SubmitChanges();
                    Class.ClsManageUserLogin.current_log = newlog;
                    Program.CurrentLogin = newlog;

                    mst_user_type CurrentUserlogin = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                    CurrentUserlogin.mut_login_status = '1';
                    dbc.SubmitChanges();

                    return true;
                }
                else
                {
                    Program.CurrentRoom = null;
                    Program.CurrentLogin = null;
                    MessageBox.Show("ไม่มี Username ที่ระบุไว้โปรดติดต่อเจ้าหน้าที่");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public static void ExitRoom()
        {
            if (Program.CurrentRoom != null && Program.CurrentUser != null && Class.ClsManageUserLogin.current_log != null)
            {
                InhCheckupDataContext dbc = new InhCheckupDataContext();
                //log_user_login currentloguserlogin = (from t1 in dbc.log_user_logins
                //                                      where t1.mrd_id == Program.CurrentRoom.mrd_id
                //                                       && t1.mut_id == Program.CurrentUser.mut_id
                //                                       && t1.lug_end_date == null
                //                                      select t1).FirstOrDefault();
                log_user_login currentloguserlogin = dbc.log_user_logins.Where(x => x.lug_id == Class.ClsManageUserLogin.current_log.lug_id).FirstOrDefault();
                if (currentloguserlogin != null)
                {
                    currentloguserlogin.lug_end_date = Program.GetServerDateTime();
                }
                mst_user_type CurrentUser = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                CurrentUser.mut_login_status = get_mutLoginStatus;
                dbc.SubmitChanges();
                Program.CurrentUser = CurrentUser;

                mst_room_dtl currentroom = (from t1 in dbc.mst_room_dtls where t1.mrd_id == Program.CurrentRoom.mrd_id select t1).FirstOrDefault();
                if (currentroom != null)
                {
                    currentroom.mrd_rm_status = 'E';
                    dbc.SubmitChanges();
                }
            }
            get_mutLoginStatus = '0';
            Class.ClsManageUserLogin.current_log = null;
            Program.CurrentRegis = null;
            Program.CurrentRoom = null;
            GC.Collect();//คำสั่ง Clear Memory ที่เคยเรียก data base มาใช้งาน 
            AlertOutDepartment.StopTime();//ปิดการทำงานการเตือน Out Department
        }
        public static void Logout()
        {
            if (Program.CurrentRoom != null && Program.CurrentUser != null)
            {
                InhCheckupDataContext dbc = new InhCheckupDataContext();
                log_user_login currentloguserlogin = (from t1 in dbc.log_user_logins
                                                      where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                       && t1.mut_id == Program.CurrentUser.mut_id
                                                       && t1.lug_end_date == null
                                                      select t1).FirstOrDefault();
                if (currentloguserlogin != null)
                {
                    currentloguserlogin.lug_end_date = Program.GetServerDateTime();
                }
                mst_user_type CurrentUser = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                CurrentUser.mut_login_status = get_mutLoginStatus;
                dbc.SubmitChanges();
            }
            Program.CurrentUser = null;
            Program.CurrentRegis = null;
            Program.CurrentRoom = null;
            Program.CurrentSite = null;
        }

        public static string GetRoomName()
        {
            if (Program.CurrentRoom != null)
            {
                return PrePareData.StaticDataCls.ProjectName + "- [" + Program.CurrentSite.mhs_ename + "] - " + Program.CurrentRoom.mrd_ename;
            }
            else
            {
                return ProjectName;
            }
        }
        public static string GetRoomName(string FormName)
        {
            if (FormName != "")
            {
                return ProjectName + " - " + FormName;
            }
            else
            {
                return ProjectName;
            }
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
                    if (RB.Tag != null && RB.Tag.ToString() == value.ToString())
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

        public static int? GetValueComboBoxInt(ComboBox cmd)
        {
            try
            {
                DropdownData castedItem = (DropdownData)cmd.SelectedItem;
                return Utility.GetInteger(castedItem.Code);
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static void ClearRadio(Panel pnl)
        {
            foreach (Control ctl in pnl.Controls)
            {
                if (ctl is RadioButton)
                {
                    var chk = (RadioButton)ctl;
                    chk.Checked = false;
                }
                else if (ctl is RadioButton)
                {
                    var rb = (RadioButton)ctl;
                    rb.Checked = false;
                }
            }
        }
        public static void ClearRadio(GroupBox gb)
        {
            foreach (Control ctl in gb.Controls)
            {

                if (ctl is RadioButton)
                {
                    var chk = (RadioButton)ctl;
                    chk.Checked = false;
                }
                else if (ctl is RadioButton)
                {
                    var rb = (RadioButton)ctl;
                    rb.Checked = false;
                }

            }
        }

        public static string CalculateAge(DateTime startDate, DateTime endDate)
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
        public static string GetPartiendAlert(List<trn_patient_alert> itemlist)
        {
            string stralert = "";
            foreach (trn_patient_alert item in itemlist)
            {
                stralert = stralert + "- " + item.tpa_alert + Environment.NewLine;
            }
            return stralert;
        }
        public static string GetFormattedString(DateTime objdate)
        {
            return objdate.ToString("dd/MM/yyyy");
        }
        public static string ConvertDateTimeToThai(DateTime d)
        {
            string tmpD = String.Empty;
            if (d.Year < 2500) { tmpD = d.AddYears(543).ToString(); }
            return String.Format("{0:dd/MM/yyyy}", DateTime.Parse(tmpD));
        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static bool chkBookComplete(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int result = cdc.trn_patient_books.Where(x => x.tpr_id == tpr_id && x.tpb_status == "BC").Count();
                    if (result == 0)
                    {
                        return true;
                    }
                    MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากได้ยืนยันสมุดรายงาน", "Warning.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public static string getFullName(int tpr_id)
        {
            try
            {
                trn_patient_regi tpr = new InhCheckupDataContext().trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                string name = null;
                name = tpr.trn_patient.tpt_othername;
                return name;
            }
            catch (Exception ex)
            {
                Program.MessageError("Program", "getFullName", ex, false);
                return "";
            }
        }

        public static bool chkVIP()
        {
            if (CurrentSite != null && CurrentRegis != null)
            {
                trn_patient tpt = getPatientByTprID(CurrentRegis.tpr_id);
                if (tpt != null)
                {
                    if (CurrentSite.mhs_code == "01HPC3" && tpt.tpt_vip_hpc == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static double GetLimitTime(string mfhCode)
        {
            DateTime datenow = GetServerDateTime();
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var litTime = (from t1 in cdc.mst_config_hdrs
                               where t1.mhs_id == Program.CurrentSite.mhs_id
                                    && t1.mfh_code == mfhCode
                                     && (t1.mfh_status == 'A'
                                           && (t1.mfh_effective_date == null ||
                                                   (t1.mfh_effective_date.Value.Date <= datenow
                                                   && (t1.mfh_expire_date == null ||
                                                       t1.mfh_expire_date.Value.Date >= datenow))
                                               )
                                            )
                               select t1.mst_config_dtls.FirstOrDefault()).FirstOrDefault();
                return (double)litTime.mfd_value;
            }
        }

        public static List<string> getMessegeCallQueue()
        {
            try
            {
                List<string> messege = getMessegeDisplay();
                if (messege != null)
                {
                    List<string> messegeCallQueue = new List<string>();
                    messege.ForEach(x => messegeCallQueue.Add(x + Program.CurrentRegis.tpr_queue_no));
                    return messegeCallQueue;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Program", "getMessegeCallQueue", ex, false);
            }
            return null;
        }
        public static List<string> getMessegeClearDisplay()
        {
            List<string> messege = getMessegeDisplay();
            if (messege != null)
            {
                List<string> messegeCallQueue = new List<string>();
                messege.ForEach(x => messegeCallQueue.Add(x + "00000"));
                return messegeCallQueue;
            }
            return null;
        }
        private static List<string> getMessegeDisplay()
        {
            try
            {
                string nationCode = string.Empty;
                string queueNO = string.Empty;
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == CurrentRegis.tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        queueNO = tpr.tpr_queue_no;
                        if (tpr.trn_patient != null)
                        {
                            nationCode = tpr.trn_patient.tpt_nation_code;
                        }
                    }
                }


                string siteId = string.Empty;
                if (CurrentSite != null)
                {
                    if (CurrentSite.mhs_id != 0) siteId = CurrentSite.mhs_id.ToString();
                }
                List<string> zone = new List<string>();
                if (CurrentRegis != null && CurrentSite != null && CurrentRoom != null)
                {
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        string mvt_code = dbc.mst_events.Where(x => x.mvt_id == CurrentPatient_queue.mvt_id).Select(x => x.mvt_code).FirstOrDefault();
                        zone = dbc.mst_room_screens.Where(x => x.mrm_code == CurrentRoom.mst_room_hdr.mrm_code
                                                            && x.mvt_code == mvt_code
                                                            && x.mhs_id == CurrentSite.mhs_id
                                                            && x.mrs_status == 'A'
                                                            && (x.mrs_effective_date != null ? x.mrs_effective_date.Value.Date : DateTime.Now.Date) <= DateTime.Now.Date
                                                            && (x.mrs_expire_date != null ? x.mrs_expire_date.Value.Date : DateTime.Now.Date) >= DateTime.Now.Date)
                                                          .Select(x => x.mze_code).ToList();
                    }
                }
                string roomNO = string.Empty;
                if (CurrentRoom != null)
                {
                    roomNO = CurrentRoom.mrd_room_no;
                }
                List<string> messege = new List<string>();
                zone.ForEach(x => messege.Add(nationCode + "_" + siteId + x + "_" + roomNO + "_"));
                return messege.Count > 0 ? messege : null;
            }
            catch (Exception ex)
            {
                Program.MessageError("Program", "getMessegeDisplay", ex, false);
                return null;
            }
        }

        public static trn_patient getPatientByTprID(int? tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient tpt = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.trn_patient).FirstOrDefault();
                    return tpt;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Program", "getPatientByTprID", ex, false);
                return null;
            }
        }

        public static string GenQueueNo(String QueueType)
        {
            string strType = "";
            if (QueueType == "2" || QueueType == "4")
            {
                strType = "1";//Appointment
            }
            else if (QueueType == "3" || QueueType == "5")
            {
                strType = "9"; //Walk-In
            }
            else if (QueueType == "1")
            {
                if (Program.Tmp_GetPtarrived.ser_rowid == 2808)
                {
                    strType = "9"; //Walk-In
                }
                else
                {
                    strType = "1";//Appointment
                }
            }
            string ENcode = Program.Tmp_GetPtarrived.paadm_admno;// Program.Tmp_GetPtarrived.papmi_no;
            int startPos = ENcode.Length - 4;

            ENcode = ENcode.Substring(startPos, 4);//O01-13-075427//01-12-00002a
            return String.Format("{0}{1}", strType, ENcode);
        }

        public static DateTime ConvertDateFromServer(string strdate, string strTime)
        {
            DateTime dateResult;
            try
            {
                DateTime dtdata = Convert.ToDateTime(strdate);
                TimeSpan tarrivaltime;
                if (strTime.IndexOf("S") != -1)
                {
                    tarrivaltime = TimeSpan.Parse(strTime.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", ""));
                }
                else
                {
                    tarrivaltime = TimeSpan.Parse(strTime.Replace("PT", "").Replace("H", ":").Replace("M", ""));
                }
                dateResult = new DateTime(dtdata.Year, dtdata.Month, dtdata.Day, tarrivaltime.Hours, tarrivaltime.Minutes, tarrivaltime.Seconds);
                return dateResult;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static mst_hpc_site getMstHpcSiteByMshCode(string mhs_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    mst_hpc_site mhs = cdc.mst_hpc_sites.Where(x => x.mhs_code == mhs_code &&
                                                                    x.mhs_status == 'A' &&
                                                                    dateNow.Date >= (x.mhs_effective_date != null ? x.mhs_effective_date.Value.Date : dateNow.Date) &&
                                                                    dateNow.Date <= (x.mhs_expire_date != null ? x.mhs_expire_date.Value.Date : dateNow.Date))
                                          .FirstOrDefault();
                    return mhs;
                }
            }
            catch
            {

            }
            return null;
        }

        public static string pacSheetReplace(string strpacsheet)
        {
            return strpacsheet.Replace("/", "-");
        }
        public static string pathoReplace(string strpatho, DateTime arriveDate)
        {
            if (strpatho == null) return "";
            if (strpatho.Trim().Length == 0) { return ""; }
            if (strpatho.IndexOf("||") > -1)
            {
                var strdata = arriveDate.ToString("yyyyMM") + @"\" + strpatho.Replace("||", "_") + ".doc";
                return strdata;
            }
            else
            {
                return strpatho;
            }
        }

        public static List<log_user_login> getNotSignOut(int mut_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var notSignOut = cdc.log_user_logins.Where(x => x.mut_id == mut_id && x.lug_end_date == null).OrderByDescending(x => x.lug_start_date);
                if (notSignOut.Count() > 0)
                {
                    return notSignOut.ToList();
                }
                return null;
            }
        }
        public static string getSiteEnameByMrdID(int? mrd_id)
        {
            if (mrd_id == null) return string.Empty;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string siteEname = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).Select(x => x.mst_room_hdr.mst_hpc_site.mhs_ename).FirstOrDefault();
                return siteEname;
            }
        }
        public static string getRoomEnameByMrdID(int? mrd_id)
        {
            if (mrd_id == null) return string.Empty;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string roomEname = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).Select(x => x.mrd_ename).FirstOrDefault();
                return roomEname;
            }
        }

        public static void OpenToDoList()
        {
            using (DBToDoList.InhToDoListDataContext cdc = new DBToDoList.InhToDoListDataContext())
            {
                string OTP = new RandomCls.RandomAlphanumeric().RandomString(20);
                DBToDoList.mst_user_otp mdo = cdc.mst_user_otps.Where(x => x.mul_user_login == Program.CurrentUser.mut_username).FirstOrDefault();
                if (mdo == null)
                {
                    mdo = new DBToDoList.mst_user_otp();
                    mdo.mul_user_login = Program.CurrentUser.mut_username;
                    cdc.mst_user_otps.InsertOnSubmit(mdo);
                }
                mdo.mul_otp = OTP;
                mdo.mul_expire = Program.GetServerDateTime().AddMinutes(10);
                try
                {
                    cdc.SubmitChanges();
                }
                catch
                {

                }
                string url = string.Format(PrePareData.StaticDataCls.ToDoListUrl, Program.CurrentUser.mut_username, OTP);
                Program.OpenLink(url);
            }
        }
        public static void OpenStatusLogin()
        {
            OpenLink(PrePareData.StaticDataCls.StatusLoginUrl);
        }
        public static void OpenQueueDetail()
        {
            OpenLink(PrePareData.StaticDataCls.QueueDetailUrl);
        }
        public static void OpenLink(string url)
        {
            List<string> ListProgram = new List<string> { "firefox.exe", "chrome.exe", "iexplore.exe" };
            foreach (var prog in ListProgram)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(prog, url);
                    System.Diagnostics.Process.Start(startInfo);
                    return;
                }
                catch (Exception ex)
                {
                    Program.MessageError("Program", "OpenToDoList()", ex, false);
                }
            }
        }
        public static void OpenReport(int tpr_id, string ReportCode, string username = "")
        {
            string url = string.Format(PrePareData.StaticDataCls.urlPreviewReport, tpr_id, ReportCode, username);
            List<string> ListProgram = new List<string> { "chrome.exe", "iexplore.exe" };
            foreach (var prog in ListProgram)
            {
                try
                {
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(prog, url);
                    System.Diagnostics.Process.Start(startInfo);
                    return;
                }
                catch (Exception ex)
                {
                    Program.MessageError("Program", "OpenToDoList()", ex, false);
                }
            }
        }
        public static void OpenUserManual()
        {
            if (File.Exists(BKvs2010.PrePareData.StaticDataCls.UserManualPath))
            {
                try
                {
                    System.Diagnostics.Process.Start(BKvs2010.PrePareData.StaticDataCls.UserManualPath);
                }
                catch (Exception ex)
                {
                    Program.MessageError("Program", "OpenUserManual()", ex, false);
                }
            }
        }
        public static Image GetLogo()
        {
            try
            {
                Image img = Image.FromFile(BKvs2010.PrePareData.StaticDataCls.PathFileLogo);
                Stream stm = new System.IO.MemoryStream();
                img.Save(stm, System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                return Image.FromStream(stm);
                //return Image.FromFile(EmrCheckupBHQ.PrePareData.StaticDataCls.PathFileLogo);
            }
            catch (Exception ex)
            {
                Program.MessageError("Program", "GetLogo()", ex, false);
                return null;
            }
        }

        public static string GetServerDateString()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                return cdc.ExecuteQuery<string>("SELECT CONVERT(varchar(10), GetDate(),126)").FirstOrDefault();
            }
        }

        //public static string GetChromePath()
        //{
        //    RegistryKey browserKeys;
        //    //on 64bit the browsers are in a different location
        //    browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
        //    if (browserKeys == null)
        //        browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");

        //    string[] browserNames = browserKeys.GetSubKeyNames();
        //    foreach (var sub in browserNames)
        //    {
        //        var key = browserKeys.OpenSubKey(sub);
        //        string[] subKeyNames = key.GetSubKeyNames();
        //        foreach (var skn in subKeyNames)
        //        {
        //            if (
        //            var subKey = key.OpenSubKey(skn);
        //        }
        //    }
        //    return browserNames;
        //}
    }
}
