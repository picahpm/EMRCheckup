using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010
{
    public static partial class Program
    {
        public static string flagLanguageToEditPE { get; set; }
        public static int CheckPointBSiteUse = 0;
        public static bool FooterIsclick { get; set; }
        public static bool IsViewHistory = false;
        public static bool IsDummy = false;

        public static mst_hpc_site CurrentSite { get; set; }
        public static mst_room_dtl CurrentRoom { get; set; }
        public static mst_user_type CurrentUser { get; set; }
        public static tmp_getptarrived Tmp_GetPtarrived { get; set; }
        public static tmp_getptappointment Tmp_GetAppointment { get; set; }

        private static trn_patient_regi propRegis = null;
        public static trn_patient_regi CurrentRegis
        {
            get { return propRegis; }
            set
            {
                if (value == null)
                {
                    //AlertOutDepartment.StopTime();
                }
                propRegis = value;
            }
        }
        public static trn_patient_queue CurrentPatient_queue { get; set; }
        public static trn_doctor_hdr CurrentHDR { get; set; }

        public static log_user_login current_log { get; set; }
        public static log_user_login CurrentLogin { get; set; }

        public static string AssemblyVersion { get; set; }
        public static string LocalIP { get; set; }
        public static string ProjectName { get; set; }

        private static char? _get_mutLoginStatus = '0';
        public static char? get_mutLoginStatus
        {
            get
            {
                return _get_mutLoginStatus;
            }
            set
            {
                _get_mutLoginStatus = value;
            }
        }

        public static int CurrentFrmShow { get; set; }

        public static List<string> aviaTypeThaiForEyes = new List<string>() { "TH" };
        public static List<string> aviaTypeFAAForEyes = new List<string>() { "FA" };
        public static List<string> aviaTypeAusForEyes = new List<string>() { "AS" };
        public static List<string> aviaTypeCanForEyes = new List<string>() { "CN" };

        public static List<string> patientTypeForAircrew = new List<string> { "4" };

        public static Boolean RefreshWaiting = true;

        public static string MsgHold = "Queue No. {0} Hold";//ข้อความแสดงหลังจากกดปุ่ม Hold
        public static string MsgCancel = "Queue No. {0} Cancelled";//ข้อความแสดงหลังจากกดปุ่ม Cancel
        public static string MsgSend = "Send Queue no. {3} to {0} [{1} {2}]";//ข้อความแสดงหลังจากกดปุ่ม Send auto &manual completed. //Save Data Completed. 
        public static string MsgCancelAndSend = "Queue No. {0} Cancelled. Send to {1} [{2} {3}]";
    }
}
