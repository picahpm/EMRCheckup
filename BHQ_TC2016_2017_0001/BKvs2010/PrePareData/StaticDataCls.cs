using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.PrePareData
{
    public static class StaticDataCls
    {
        public static string ProjectName { get; set; }
        public static string WSTrakcareUrl { get; set; }
        public static string WSTrakcareLoginUrl { get; set; }
        public static string WSDocScanUrl { get; set; }
        public static string WSDocScanPreviewUrl { get; set; }
        public static string WSPathWay { get; set; }
        public static bool UseUnitDisplay { get; set; }
        public static bool UseMenu { get; set; }

        public static string ServerDataBase { get; set; }
        public static string ServerReport { get; set; }
        public static string DataBaseName { get; set; }
        public static string DataBaseUserName { get; set; }
        public static string DataBasePassword { get; set; }

        public static string ToDoListUrl { get; set; }
        public static string QuesOccmedUrl { get; set; }
        public static string UserManualPath { get; set; }
        public static string StatusLoginUrl { get; set; }
        public static string QueueDetailUrl { get; set; }
        public static string PathFileLogo { get; set; }

        public static string urlPreviewReport { get; set; }

        public static List<PrepareDataCls.ConfigHoldTime> HoldTime { get; set; }

        public static string RoomName(string name)
        {
            try
            {
                return ProjectName + "- [" + Program.CurrentSite.mhs_ename + "] - " + Program.CurrentRoom.mrd_ename;
            }
            catch
            {
                return name;
            }
        }
    }
}
