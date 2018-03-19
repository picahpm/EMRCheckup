using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;
using DBToDoList;

namespace CheckUpToDoList.InterfaceTrakCare
{
    public partial class RunJob : System.Web.UI.Page
    {
        /// <summary>
        /// Created by : Akkaradech.M
        /// Description : AutoRun job Interface to TrakCare
        /// Table : mst_payor
        /// </summary>
        /// 

        string closePage = @"<script type='text/javascript'> window.returnValue = true; window.open('', '_self', '');window.close();</script>";
        Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
        InhToDoListDataContext dbc = new InhToDoListDataContext();
        DataTable Tmptable = new DataTable("items");
        bool isSuccess = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {
                    AutoRunJob();
                }
                catch (Exception ex)
                {
                   // throw new Exception(ex.Message);
                    WriteToLog(ex.Message);
                    Response.Write(closePage);
                }
            }
        }

        private DataTable ListPayor()
        {
            try
            {
                DataTable distinctValues = new DataTable();
                DataTable dt = ws.ListPayor();
                DataSet ds = new DataSet("ds");
                Tmptable.Columns.Add("Item");
                Tmptable.Columns.Add("Code");
                Tmptable.Columns.Add("Name");
                ds.Tables.Add(Tmptable);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow newRow = Tmptable.NewRow();
                        newRow["Item"] = i + 1;
                        newRow["Code"] = dt.Rows[i]["INST_Code"].ToString();
                        newRow["Name"] = dt.Rows[i]["INST_Desc"].ToString();
                        Tmptable.Rows.Add(newRow);
                    }
                }
                DataView view = new DataView(Tmptable);
                distinctValues = view.ToTable(true, "Code", "Name");
                return distinctValues;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InsertPayor()
        {
            try
            {
                DataTable newtable = new DataTable();
                newtable = ListPayor();
                string message = string.Empty;
                if (newtable.Rows.Count > 0)
                {
                    for (int i = 0; i <= newtable.Rows.Count - 1; i++)
                    {
                        int countItem = (from t in dbc.mst_payors where t.msp_code.ToString() == newtable.Rows[i]["Code"].ToString() select t.msp_code).Count();
                        message = "Code :" + newtable.Rows[i]["Code"].ToString() + " :" + " Name :" + newtable.Rows[i]["Name"].ToString();
                        if (countItem == 0)
                        {
                            mst_payor objpayor = new mst_payor
                            {
                                msp_code = newtable.Rows[i]["Code"].ToString(),
                                msp_name = newtable.Rows[i]["Name"].ToString(),
                                msp_create_by = "System",
                                msp_create_date = funcCls.GetServerDateTime(),
                                mul_user_login = "System",
                                msp_update_date = funcCls.GetServerDateTime()
                            };
                            dbc.mst_payors.InsertOnSubmit(objpayor);
                            dbc.SubmitChanges();
                            isSuccess = true;
                        }
                        if (isSuccess == true)
                            WriteToLog(message + ":::" + "Completed >>> Insert to mst_payor");
                        else
                            WriteToLog(message + ":::" + "Failed >>> Duplicate Data");
                    }
                }
            }
            catch (Exception ex)
            {
               // throw new Exception(ex.Message);
                WriteToLog(ex.Message);
            }
        }

        private void AutoRunJob()
        {
            try
            {
                WriteToLogHdr();
                InsertPayor();
                Response.Write(closePage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void WriteToLogHdr()
        {
            try
            {
                string strLogFilePath = (Server.MapPath("../LogsInterface/") + "log_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + ".txt");
                if (!File.Exists(strLogFilePath))
                {
                    File.Create(strLogFilePath).Close();
                }
                using (StreamWriter w = File.AppendText(strLogFilePath))
                {
                    w.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                    w.WriteLine("Start Run : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void WriteToLog(string msg)
        {
            try
            {
                string strLogFilePath = (Server.MapPath("../LogsInterface/") + "log_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + ".txt"); 
                if (!File.Exists(strLogFilePath))
                {
                    File.Create(strLogFilePath).Close();
                }
                using (StreamWriter w = File.AppendText(strLogFilePath))
                {
                    string strmsg = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Status Message:" + msg;
                    w.WriteLine(strmsg);
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}