using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CorporateSummaryReport.Models;
using DBCheckup;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Configuration;
using System.Drawing;

namespace CorporateSummaryReport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            return View("Search", new SearchModel() { companyname = "", dataListCompany = getListCorp() });
        }

        public ActionResult Search()
        {
            return View("Search", new SearchModel() { companyname = "", dataListCompany = getListCorp() });
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            int totalservice = 0;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                totalservice = cdc.vw_patient_corporates
                                  .Where(x => (x.companyname == null ? "" : x.companyname) == (model.companyname == null ? "" : model.companyname) &&
                                               x.arrived_date.Value.Date >= model.startdate.Date &&
                                               x.arrived_date.Value.Date <= model.enddate.Date)
                                  .Count();
                model.totalservice = totalservice;
            }
            try
            {
                
                if (model.func == SearchModel.functionCriterias.funcSearch)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                         
                        model.dataListCompany = getListCorp();
                        using (DBToDoList.InhToDoListDataContext itc = new DBToDoList.InhToDoListDataContext())
                        {
                            string sub_com = "";
                            if (!string.IsNullOrEmpty(model.companyname))
                            {
                                var doc_no = (from itd in itc.index_trn_company_details
                                              join tcd in itc.trn_company_details
                                              on itd.tcd_id equals tcd.tcd_id
                                              where tcd.tcd_tname == model.companyname
                                              select itd.tcd_document_no).FirstOrDefault();

                                sub_com = (from tcd in itc.trn_company_details
                                               join itd in itc.index_trn_company_details
                                               on tcd.tcd_id equals itd.tcd_id
                                               where tcd.tcd_document_no == doc_no
                                               select tcd.tcd_legal).FirstOrDefault();

                                model.sub_companyname = sub_com;

                                if (!string.IsNullOrEmpty(doc_no))
                                {
                                    List<patient> pt = cdc.trn_patient_book_covers
                                                          .Where(x => x.tcd_document_no == doc_no && x.trn_patient_regi.tpr_arrive_date.Value.Date >= model.startdate.Date &&
                                                             x.trn_patient_regi.tpr_arrive_date.Value.Date <= model.enddate.Date)
                                                          .Select(x => new patient
                                                          {
                                                              hn = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                              en = x.trn_patient_regi.tpr_en_no,
                                                              id = x.trn_patient_regi.trn_patient_book_cover.tpbc_emp_id,
                                                              name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                             // arrived = x.trn_patient_regi.trn_patient_regis_detail.tpr_real_arrived_date.Value
                                                              arrived = cdc.GetArrivedate(x.tpr_id).Value
                                                             
                                                          }).OrderBy(x => x.arrived).ToList();


                                    
                                    model.listPatient = pt;
                                    model.patients = pt.Count();
                                    model.sub_companyname = sub_com;
                                }
                                else
                                {
                                    model.listPatient = new List<patient>();
                                    model.patients = model.listPatient.Count();
                                    model.sub_companyname = sub_com;
                                }
                            }
                            else
                            {
                                model.listPatient = new List<patient>();
                                model.patients = model.listPatient.Count();
                                model.sub_companyname = sub_com;
                            }

                            //List<patient> pt = cdc.vw_patient_corporates
                            //                      .Where(x => (x.companyname == null ? "" : x.companyname) == (model.companyname == null ? "" : model.companyname) &&
                            //                                  x.arrived_date.Value.Date >= model.startdate.Date &&
                            //                                  x.arrived_date.Value.Date <= model.enddate.Date)
                            //                      .Select(x => new patient
                            //                      {
                            //                          hn = x.hn,
                            //                          en = x.en,
                            //                          name = x.patient_name,
                            //                          arrived = x.arrived_date.Value
                            //                      }).ToList();
                            //model.listPatient = pt;
                        }
                    }

                    
                   
                }
                else
                {
                    Criterias cModel = new Criterias
                    {
                        companyname = model.companyname,
                        startdate = model.startdate,
                        enddate = model.enddate,
                        patients = model.patients,
                        totalservice = model.totalservice,
                        sub_companyname = model.sub_companyname


                    };
                    return SelectReport(model);
                }
            }
            catch
            {

            }
            return View("Search", model);
        }

        public ActionResult SelectReport(Criterias model)
        {
            int totalservice = 0;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                totalservice = cdc.vw_patient_corporates
                                  .Where(x => (x.companyname == null ? "" : x.companyname) == (model.companyname == null ? "" : model.companyname) &&
                                               x.arrived_date.Value.Date >= model.startdate.Date &&
                                               x.arrived_date.Value.Date <= model.enddate.Date)
                                  .Count();
            }
            SelectReportModel sModel = new SelectReportModel
            {
                companyname = model.companyname,
                startdate = model.startdate,
                enddate = model.enddate,
                patients = model.patients,
                totalservice = totalservice,
                sub_companyname = model.sub_companyname
            };
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                List<mst_corp_report> mstRpt = cdc.mst_corp_reports
                                                  .Where(x => (x.mcr_active == true)&&( x.mcr_type == 'F')).ToList();
                sModel.DropdownReport = mstRpt.OrderBy(x => x.mcr_seq)
                                        .Select(x => new SelectListItem
                                        {
                                            Selected = false,
                                            Value = x.mcr_id.ToString(),
                                            Text = x.mcr_report_name
                                        }).ToList();
                if (sModel.DropdownReport.Count > 0) sModel.DropdownReport.FirstOrDefault().Selected = true;
                return View("SelectReport", sModel);
            }
        }

        [HttpPost]
        public ActionResult SelectReport(SelectReportModel model)
        {
            return Report(model);
        }

        private Stream MergedPDF(List<Stream> ListPdf)
        {
            try
            {
                using (MemoryStream newStream = new MemoryStream())
                {
                    iTextSharp.text.pdf.PdfCopyFields pdf = new iTextSharp.text.pdf.PdfCopyFields(newStream);
                    try
                    {
                        foreach (Stream filePdf in ListPdf)
                        {
                            pdf.AddDocument(new iTextSharp.text.pdf.PdfReader(filePdf));
                        }
                        pdf.Close();
                        return new MemoryStream(newStream.ToArray());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        pdf.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Report(SelectReportModel model)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string rptID = model.DropdownReport[0].Value;

                    string path = cdc.mst_corp_reports.Where(x => x.mcr_id == Convert.ToInt32(rptID)).Select(x => x.mcr_report_path).FirstOrDefault();
                    if (path == null)
                    {
                        List<Stream> listStream = new List<Stream>();
                        List<string> listPath = cdc.mst_corp_reports.Where(x => (x.mcr_is_multi_report == true) && (x.mcr_type == 'F')).OrderBy(x => x.mcr_seq).Select(x => x.mcr_report_path).ToList();
                        foreach (string rptPath in listPath)
                        {
                            listStream.Add(LoadReport(rptPath, model));
                        }
                        return File(MergedPDF(listStream), "application/pdf", "Corporate Summary.pdf");
                    }
                    else
                    {
                        string nameReport = cdc.mst_corp_reports.Where(x => x.mcr_report_path == path).Select(y => y.mcr_report_name).FirstOrDefault();
                        string filename = nameReport + ".pdf";
                        return File(LoadReport(path, model), "application/pdf", filename);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("Report", model);
            }
        }

        private Stream LoadReport(string path, SelectReportModel model)
        {
            try
            {
                string reportPath = Request.MapPath(path);
                ReportDocument rptDoc = new ReportDocument();
                rptDoc.Load(reportPath);
                SetDBLogonForReport(rptDoc);
                rptDoc.Refresh();
                rptDoc.VerifyDatabase();
                rptDoc.SetParameterValue("@companyName", string.IsNullOrEmpty(model.companyname) ? "" : model.companyname);
                rptDoc.SetParameterValue("@subcompanyName", string.IsNullOrEmpty(model.sub_companyname) ? "" : model.sub_companyname);
                rptDoc.SetParameterValue("@startDate", model.startdate);
                rptDoc.SetParameterValue("@endDate", model.enddate);

                if (CheckParamReport(rptDoc, "@totalpatient")) rptDoc.SetParameterValue("@totalpatient", model.patients);
                if (CheckParamReport(rptDoc, "@totalservice")) rptDoc.SetParameterValue("@totalservice", model.totalservice);
                if (CheckParamReport(rptDoc, "@totalnoservice")) rptDoc.SetParameterValue("@totalnoservice", model.patients - model.totalservice);
                Stream pdf = rptDoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                rptDoc.Close();
                rptDoc.Dispose();
                return pdf;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckParamReport(ReportDocument rptDoc, string paramName)
        {
            foreach (CrystalDecisions.Shared.ParameterField param in rptDoc.ParameterFields)
            {
                if (param.Name == paramName && param.ReportName == rptDoc.Name) return true;
            }
            return false;
        }

        private void SetDBLogonForReport(ReportDocument reportDocument)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                connectionInfo.ServerName = cdc.mst_project_configs.Where(x => x.mpc_code == "ServerDataBase").Select(x => x.mpc_value).FirstOrDefault();
                connectionInfo.DatabaseName = cdc.mst_project_configs.Where(x => x.mpc_code == "DataBaseName").Select(x => x.mpc_value).FirstOrDefault();
                connectionInfo.UserID = cdc.mst_project_configs.Where(x => x.mpc_code == "DataBaseUserName").Select(x => x.mpc_value).FirstOrDefault();
                connectionInfo.Password = cdc.mst_project_configs.Where(x => x.mpc_code == "DataBasePassword").Select(x => x.mpc_value).FirstOrDefault();
            }
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        private List<string> getListCorp()
        {
            try
            {
                using (DBToDoList.InhToDoListDataContext itc = new DBToDoList.InhToDoListDataContext())
                {
                    var coms = (from icd in itc.index_trn_company_details
                                join tcd in itc.trn_company_details
                                on icd.tcd_id equals tcd.tcd_id
                                select tcd.tcd_tname).ToList();
                    return coms;
                }
            }
            catch
            {
                return new List<string>();
            }

            //try
            //{
            //    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            //    {
            //        List<string> listComp = cdc.vw_patient_corporates
            //                                   .Where(x => x.companyname != null)
            //                                   .Select(x => x.companyname)
            //                                   .Distinct()
            //                                   .ToList();
            //        return listComp;
            //    }
            //}
            //catch
            //{
            //    return new List<string>();
            //}
        }

        
    }
}
