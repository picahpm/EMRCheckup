using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBCheckup;
using DBToDoList;

namespace BKvs2010.Class
{
    public class CompanyCls
    {
        public class MapPatientResult
        {
            public string emp_id { get; set; }
            public string document_no { get; set; }
            public string position { get; set; }
            public string department { get; set; }
        }
        public MapPatientResult MappingPatient(int tpr_id, List<int> listID)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var patient = cdc.trn_patient_regis
                                     .Where(x => x.tpr_id == tpr_id)
                                     .Select(x => new
                                     {
                                         en = x.tpr_en_no,
                                         arrived = x.trn_patient_regis_detail.tpr_real_arrived_date,
                                         loc = x.mst_hpc_site.mhs_code
                                     }).FirstOrDefault();
                    if (patient != null)
                    {
                        using (WS_Trakcare.WS_GetDataBytrakSoapClient ws = new WS_Trakcare.WS_GetDataBytrakSoapClient())
                        {
                            var result = ws.GetPTArrivedCheckUpFilter(patient.loc, patient.en, patient.arrived.Value.ToString("yyyy-MM-dd")).AsEnumerable()
                                           .Select(x => new
                                           {
                                               titlename = x.Field<string>("TTL_Desc") == null ? "" : x.Field<string>("TTL_Desc").Trim(),
                                               firstname = x.Field<string>("PAPMI_Name") == null ? "" : x.Field<string>("PAPMI_Name").Trim(),
                                               lastname = x.Field<string>("PAPMI_Name2") == null ? "" : x.Field<string>("PAPMI_Name2").Trim(),
                                               firstname_eng = x.Field<string>("PAPER_Name5") == null ? "" : x.Field<string>("PAPER_Name5").Trim(),
                                               middlename_eng = x.Field<string>("PAPER_Name7") == null ? "" : x.Field<string>("PAPER_Name7").Trim(),
                                               lastname_eng = x.Field<string>("PAPER_Name6") == null ? "" : x.Field<string>("PAPER_Name6").Trim(),
                                               idcard = x.Field<string>("PAPER_ID") == null ? "" : x.Field<string>("PAPER_ID").Trim()
                                           }).FirstOrDefault();
                            if (result != null)
                            {
                                using (InhToDoListDataContext tdc = new InhToDoListDataContext())
                                {
                                    MapPatientResult person;
                                    if (!string.IsNullOrEmpty(result.idcard))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_personal_id.Trim() == result.idcard)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname) && !string.IsNullOrEmpty(result.lastname))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim() == result.firstname &&
                                                                x.tnc_lname.Trim() == result.lastname)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim() == result.firstname_eng &&
                                                                x.tnc_lname.Trim() == result.lastname_eng)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.middlename_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim() == result.firstname_eng + " " + result.middlename_eng &&
                                                                x.tnc_lname.Trim() == result.lastname_eng)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname) && !string.IsNullOrEmpty(result.lastname))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim().EndsWith(result.firstname) &&
                                                                x.tnc_lname.Trim() == result.lastname)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim().EndsWith(result.firstname_eng) &&
                                                                x.tnc_lname.Trim() == result.lastname_eng)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.middlename_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        person = tdc.trn_name_checks
                                                    .Where(x => listID.Contains(x.tcd_id) &&
                                                                x.tnc_fname.Trim().EndsWith(result.firstname_eng + " " + result.middlename_eng) &&
                                                                x.tnc_lname.Trim() == result.lastname_eng)
                                                    .Select(x => new MapPatientResult
                                                    {
                                                        document_no = x.trn_company_detail.tcd_document_no,
                                                        emp_id = x.tnc_emp_id,
                                                        position = x.tnc_position,
                                                        department = x.tnc_department
                                                    }).FirstOrDefault();
                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return new MapPatientResult();
                }
            }
            catch (Exception ex)
            {
                return new MapPatientResult();
            }
        }
        public MapPatientResult MappingPatient(int tpr_id, List<string> listCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var patient = cdc.trn_patient_regis
                                     .Where(x => x.tpr_id == tpr_id)
                                     .Select(x => new
                                     {
                                         en = x.tpr_en_no,
                                         arrived = x.trn_patient_regis_detail.tpr_real_arrived_date,
                                         loc = x.mst_hpc_site.mhs_code
                                     }).FirstOrDefault();
                    if (patient != null)
                    {
                        using (WS_Trakcare.WS_GetDataBytrakSoapClient ws = new WS_Trakcare.WS_GetDataBytrakSoapClient())
                        {
                            var result = ws.GetPTArrivedCheckUpFilter(patient.loc, patient.en, patient.arrived.Value.ToString("yyyy-MM-dd")).AsEnumerable()
                                           .Select(x => new
                                           {
                                               titlename = x.Field<string>("TTL_Desc") == null ? "" : x.Field<string>("TTL_Desc").Trim(),
                                               firstname = x.Field<string>("PAPMI_Name") == null ? "" : x.Field<string>("PAPMI_Name").Trim(),
                                               lastname = x.Field<string>("PAPMI_Name2") == null ? "" : x.Field<string>("PAPMI_Name2").Trim(),
                                               firstname_eng = x.Field<string>("PAPER_Name5") == null ? "" : x.Field<string>("PAPER_Name5").Trim(),
                                               middlename_eng = x.Field<string>("PAPER_Name7") == null ? "" : x.Field<string>("PAPER_Name7").Trim(),
                                               lastname_eng = x.Field<string>("PAPER_Name6") == null ? "" : x.Field<string>("PAPER_Name6").Trim(),
                                               idcard = x.Field<string>("PAPER_ID") == null ? "" : x.Field<string>("PAPER_ID").Trim()
                                           }).FirstOrDefault();
                            if (result != null)
                            {
                                using (InhToDoListDataContext tdc = new InhToDoListDataContext())
                                {
                                    MapPatientResult person;
                                    if (!string.IsNullOrEmpty(result.idcard))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_personal_id.Trim() == result.idcard)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_personal_id.Trim() == result.idcard
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname) && !string.IsNullOrEmpty(result.lastname))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim() == result.firstname &&
                                        //                        x.tnc_lname.Trim() == result.lastname)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim() == result.firstname &&
                                                        tnc.tnc_lname.Trim() == result.lastname
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim() == result.firstname_eng &&
                                        //                        x.tnc_lname.Trim() == result.lastname_eng)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim() == result.firstname_eng &&
                                                        tnc.tnc_lname.Trim() == result.lastname_eng
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.middlename_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim() == result.firstname_eng + " " + result.middlename_eng &&
                                        //                        x.tnc_lname.Trim() == result.lastname_eng)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim() == result.firstname_eng + " " + result.middlename_eng &&
                                                        tnc.tnc_lname.Trim() == result.lastname_eng
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname) && !string.IsNullOrEmpty(result.lastname))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim().EndsWith(result.firstname) &&
                                        //                        x.tnc_lname.Trim() == result.lastname)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim().EndsWith(result.firstname) &&
                                                        tnc.tnc_lname.Trim() == result.lastname
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim().EndsWith(result.firstname_eng) &&
                                        //                        x.tnc_lname.Trim() == result.lastname_eng)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim().EndsWith(result.firstname_eng) &&
                                                        tnc.tnc_lname.Trim() == result.lastname_eng
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(result.firstname_eng) && !string.IsNullOrEmpty(result.middlename_eng) && !string.IsNullOrEmpty(result.lastname_eng))
                                    {
                                        //person = tdc.test_name_checks
                                        //            .Where(x => listCode.Contains(x.tcd_document_no) &&
                                        //                        x.tnc_fname.Trim().EndsWith(result.firstname_eng + " " + result.middlename_eng) &&
                                        //                        x.tnc_lname.Trim() == result.lastname_eng)
                                        //            .Select(x => new MapPatientResult
                                        //            {
                                        //                document_no = x.tcd_document_no,
                                        //                emp_id = x.tnc_emp_id,
                                        //                position = x.tnc_position,
                                        //                department = x.tnc_department
                                        //            }).FirstOrDefault();

                                        person = (from tnc in tdc.trn_name_checks
                                                  join itcd in tdc.index_trn_company_details
                                                  on tnc.tcd_id equals itcd.tcd_id
                                                  where listCode.Contains(itcd.tcd_document_no) &&
                                                        tnc.tnc_fname.Trim().EndsWith(result.firstname_eng + " " + result.middlename_eng) &&
                                                        tnc.tnc_lname.Trim() == result.lastname_eng
                                                  select new MapPatientResult
                                                  {
                                                      document_no = itcd.tcd_document_no,
                                                      emp_id = tnc.tnc_emp_id,
                                                      position = tnc.tnc_position,
                                                      department = tnc.tnc_department
                                                  }).FirstOrDefault();

                                        if (person != null)
                                        {
                                            return person;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return new MapPatientResult();
                }
            }
            catch (Exception ex)
            {
                return new MapPatientResult();
            }
        }

        public class CompResult
        {
            public int tcd_id { get; set; }
            public string doc_no { get; set; }
            public string comName { get; set; }
        }
        public List<CompResult> GetListComp(DateTime dateArrived)
        {
            using (InhToDoListDataContext tdc = new InhToDoListDataContext())
            {
                var clist = tdc.GetCompByArrived(dateArrived)
                               .Select(x => new CompResult
                               {
                                   tcd_id = x.tcd_id,
                                   doc_no = x.tcd_document_no,
                                   comName = x.tcd_tname
                               }).ToList();

                //var clist = tdc.test_document_nos
                //               .Where(x => x.tcd_date_from.Value.Date <= dateArrived.Date &&
                //                           x.tcd_date_to.Value.Date >= dateArrived.Date)
                //               .Select(x => new CompResult
                //               {
                //                   doc_no = x.tcd_document_no,
                //                   comName = x.tcd_tname
                //               }).OrderBy(x => x.comName).ToList();
                return clist;
            }
        }
    }
}
