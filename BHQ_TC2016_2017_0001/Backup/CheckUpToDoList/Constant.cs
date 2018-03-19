using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using DBToDoList;

namespace CheckUpToDoList
{
    public class Constant
    {
        public static string ConnectionStringTodolist = WebConfigurationManager.ConnectionStrings["TodolistConnectionString"].ConnectionString;
        public static string ConnectionStringPathway = WebConfigurationManager.ConnectionStrings["PathWayConnectionString"].ConnectionString;
        public static string DefaultPageLogin = "plogin.aspx";
        public static string Path_Img_calendar = "~/calendar/calendar3.gif";
        public static string pathserverUpload = WebConfigurationManager.AppSettings["PathUpload"];
        public static string pathTempUpload = WebConfigurationManager.AppSettings["pathTempImport"];
        public static string pathServer = WebConfigurationManager.AppSettings["pathserver"];
        public static string URLServer = WebConfigurationManager.AppSettings["URL"];
        public static string HttpUrl = WebConfigurationManager.AppSettings["DownloadFile"];
        public static string IsWriteLog = WebConfigurationManager.AppSettings["IsWriteLog"];
        public static string CurrentUserLogin
        {
            get
            {
                if (HttpContext.Current.Session["UserLogin"] != null)
                {
                    return HttpContext.Current.Session["UserLogin"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["UserLogin"] = value;
               
            }
        }
        public static string CurrentUserLoginName
        {
            get
            {
                if (HttpContext.Current.Session["UserLoginName"] != null)
                {
                    return HttpContext.Current.Session["UserLoginName"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["UserLoginName"] = value;
               
            }
        }
        public static string CurrentPageLogin
        {
            get
            {
                if (HttpContext.Current.Session["PageLogin"] != null)
                {
                    return HttpContext.Current.Session["PageLogin"].ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["PageLogin"] = value;
               
            }
        }

        public static List<mst_checkup_location> getListHpcSite()
        {
            List<mst_checkup_location> site = new List<mst_checkup_location>();
            InhToDoListDataContext tdc = new InhToDoListDataContext();
            site = tdc.mst_checkup_locations.ToList();
            return site;
        }
        public static void CheckPolicy(string TypeCode)
        {
            if (Constant.CurrentUserLogin != "")
            {//ถ้ายังไม่ได้ login ให้ ไปหน้า login
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var objpolicy = (from t1 in dbc.mst_user_logins
                                     where t1.mul_user_login == Constant.CurrentUserLogin
                                     && t1.mst_user_type.mut_code == TypeCode
                                     select t1).Count();
                    if (objpolicy > 0)
                    {
                        return;
                    }
                    else
                    {
                        if (Constant.CurrentUserLogin == "Admin") { return; }
                        HttpContext.Current.Response.Redirect("frmHome.aspx");
                    }
                }
            }
            else
            {
                string[] strpath = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
                string pagename = strpath[strpath.Length - 1];
                string qs = HttpContext.Current.Request.Url.Query;
                HttpContext.Current.Response.Redirect(Constant.DefaultPageLogin + "?backurl=" + Constant.URLEnCode(pagename + qs));
            }
            
        }

        public static void addListSiteToControl(DropDownList dropDown)
        {
            InhToDoListDataContext tdc = new InhToDoListDataContext();
            var dataobj = (from s in tdc.mst_checkup_locations.ToList()
                                   where s.mcl_status == 'A'
                                   select new DropdowData 
                                   {
                                        code = s.mcl_id,
                                        name = MargeString(s.mcl_tname, s.mcl_ename)
                                   }).ToList();
            DropdowData newitem = new DropdowData();
            newitem.code = 0;
            newitem.name = String.Empty;
            dataobj.Insert(0, newitem);
            dropDown.DataSource = dataobj;
            dropDown.DataValueField = "code";
            dropDown.DataTextField = "name";
            dropDown.DataBind();
        }
        public static string MargeString(object str1, object str2)
        {
            string nameTH = Convert1.ToString(str1);
            string nameEN = Convert1.ToString(str2);
            string NameAll = "";
            if (nameTH == "" || nameEN == "")
            {
                if (nameTH != "")
                {
                    NameAll = nameTH;
                }
                if (nameEN != "")
                {
                    NameAll = nameEN;
                }
            }
            else
            {
                NameAll = nameTH + " / " + nameEN;
            }
            return NameAll;
        }
        public static string MargeString(object str1, object str2,string strsplit)
        {
            string nameTH = Convert1.ToString(str1);
            string nameEN = Convert1.ToString(str2);
            string NameAll = "";
            if (nameTH == "" || nameEN == "")
            {
                if (nameTH != "")
                {
                    NameAll = nameTH;
                }
                if (nameEN != "")
                {
                    NameAll = nameEN;
                }
            }
            else
            {
                NameAll = nameTH + strsplit + nameEN;
            }
            return NameAll;
        }

        public static void addListDocCateToControl(DropDownList dropDown)
        {
            InhToDoListDataContext tdc = new InhToDoListDataContext();
           var dataobj  = (from s in tdc.mst_doctor_cats.ToList()
                                   where s.mdc_status == 'A'
                                   select new DropdowData
                                   {
                                       code = s.mdc_id,
                                       name = s.mdc_ename
                                   }).ToList();

           DropdowData newitem = new DropdowData();
           newitem.code = 0;
           newitem.name = String.Empty;
           dataobj.Insert(0, newitem);
           dropDown.DataSource = dataobj;
           dropDown.DataValueField = "code";
           dropDown.DataTextField = "name";
           dropDown.DataBind();
        }

        public static void SetTextKeyNumber(ref TextBox TxtBox)
        {
            TxtBox.Attributes.Add("onkeypress", "return checknum(arguments[0] , 'd','" + TxtBox.ClientID + "')");
        }
        public static string URLEnCode(string strdata)
        {
            return System.Web.HttpContext.Current.Server.UrlEncode(strdata);
        }
        public static string URLDeCode(string strdata)
        {
            return System.Web.HttpContext.Current.Server.UrlDecode(strdata);
        }

        public static DateTime ConvertStringToDate(String strdate)
        {
            try
            {
                string[] datespilt = strdate.Trim().Split('/');
                int d = Convert1.ToInt32(datespilt[0]);
                int m = Convert1.ToInt32(datespilt[1]);
                int y = Convert1.ToInt32(datespilt[2]);
                DateTime dnow = new DateTime(y, m, d);
                return dnow;

            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
        //public void DeleteFileFromFolder(string StrFilename)
        //{

        //    string strPhysicalFolder = Server.MapPath("..\\");

        //    string strFileFullPath = strPhysicalFolder + StrFilename;

        //    if (File.Exists(strFileFullPath))
        //    {
        //        File.Delete(strFileFullPath);
        //    }

        //}
        public static string ParseTemplate(string TemplatePath)
        {
            StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath(TemplatePath));
            string Output = SR.ReadToEnd();
            SR.Close();
            SR.Dispose();
            return Output;
        }

        public static List<fileclass> GetObjAttachFile(int tcd_id,string site)
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                //List<fileclass> objattachfile = (from file in dbc.trn_attach_files
                //                                 where file.mst_user_type.mut_code == site
                //                                 && file.mst_path_file.tcd_id == tcd_id
                //                                 select new fileclass
                //                                 {
                //                                     file_name = file.mst_path_file.mpf_file_name,
                //                                     file_path = Constant.HttpUrl + file.mst_path_file.mpf_path_name
                //                                 }).ToList();

                 List<fileclass> objattachfile  = (from t1 in dbc.mst_path_files
                                                   where t1.tcd_id == tcd_id && t1.trn_attach_files.Where(x => x.mst_user_type.mut_code == site).Count() > 0  
                 select new fileclass
                 {
                     file_name = t1.mpf_file_name,
                     file_path = Constant.HttpUrl + t1.mpf_path_name,
                 }).ToList();

                return objattachfile;
            }        
         }

        public static void RemoveOldFiles()
        {
            try
            {
                string FolderPath = Constant.pathTempUpload;
                DirectoryInfo tempUpload = new DirectoryInfo(HttpContext.Current.Server.MapPath(FolderPath));
                foreach ( FileInfo f in tempUpload.GetFiles())
                {
                    if (f.CreationTime.Date != DateTime.Now.Date)
                    {
                        File.Delete(f.FullName);
                    }
                    //String[] nameParts = dir.Name.Split(new char[] { '_' });
                    //if (nameParts.Length == 6)
                    //{
                    //    DateTime dirDate = new DateTime(Convert.ToInt32(nameParts[1]), Convert.ToInt32(nameParts[2]), Convert.ToInt32(nameParts[3]), Convert.ToInt32(nameParts[4]), Convert.ToInt32(nameParts[5]), 0);
                    //    //Delete files which older than 3 hours
                    //    if (dirDate.AddHours(1) <= DateTime.Now)
                    //    {
                    //        Directory.Delete(dir.FullName, true);
                    //    }
                    //}
                }
            }
            catch (IOException) { }
        }
    }
    public class fileclass
    {
        public string file_name { get; set; }
        public string file_path { get; set; }
    }

    public class Convert1
    {
        public static bool ToBoolean(object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;

            }
        }
        public static Int32 ToInt32(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;

            }
        }
        public static double ToDouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return 0;

            }
        }
        public static float ToFloat(object obj)
        {
            try
            {
                return Convert.ToSingle(obj);
            }
            catch (Exception)
            {
                return 0;

            }
        }
        public static char? ToChar(object obj)
        {
            try
            {
                return Convert.ToChar(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }

    }
}