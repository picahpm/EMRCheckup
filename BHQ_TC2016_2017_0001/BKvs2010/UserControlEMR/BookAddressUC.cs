using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DBCheckup;
using DBToDoList;

namespace BKvs2010.UserControlEMR
{
    public partial class BookAddressUC : UserControl
    {
        public BookAddressUC()
        {
            InitializeComponent();
        }

        public string PatientNameTH
        {
            get { return Address.nameTH; }
        }
        public string PatientNameEN
        {
            get { return Address.nameEN; }
        }
        public string PatientAddress
        {
            get { return Address.listAddress.Where(x => x.type == "H").Select(x => x.address).FirstOrDefault(); }
        }
        public string CompanyAddress
        {
            get { return Address.listAddress.Where(x => x.type == "C").Select(x => x.address).FirstOrDefault(); }
        }
        public string OtherAddress
        {
            get { return _book_cover.tpbc_patient_address; }
        }
        public string FlagName
        {
            get { return _book_cover.tpbc_cover_lang; }
        }
        public string FlagAddress
        {
            get { return _book_cover.tpbc_address; }
        }
        public string ContactPersonName
        {
            get { return Address.listAddress.Where(x => x.type == "C").Select(x => x.contactPerson).FirstOrDefault(); }
        }

        private trn_patient_book_cover _book_cover;
        public trn_patient_book_cover book_cover
        {
            get { return _book_cover; }
            set
            {
                Address = new Models.TabAddressModels();
                ClearAddress();
                ClearCompany();
                _book_cover = value;
                if (value == null)
                {
                    PatientBookCoverBS.DataSource = new trn_patient_book_cover();

                    panel1.Enabled = false;
                }
                else
                {
                    GetData(_book_cover.tpr_id);
                    BindAddress();
                    BindCompany();
                    if (_book_cover.tpbc_create_date == null)
                    {
                        _book_cover.tpbc_patient_name = Address.nameTH;
                        _book_cover.tpbc_patient_type = Address.patientType;
                        if (_book_cover.tpbc_patient_type == "G")
                        {
                            _book_cover.tpbc_address = "H";
                        }
                        else
                        {
                            _book_cover.tpbc_address = "C";
                        }
                        _book_cover.tpbc_cover_lang = Address.lang;
                    }
                    _book_cover_PropertyChanged(_book_cover, new PropertyChangedEventArgs("tpbc_patient_type"));
                    _book_cover_PropertyChanged(_book_cover, new PropertyChangedEventArgs("tpbc_cover_lang"));
                    _book_cover_PropertyChanged(_book_cover, new PropertyChangedEventArgs("tpbc_address"));
                    _book_cover_PropertyChanged(_book_cover, new PropertyChangedEventArgs("tcd_document_no"));


                    _book_cover.PropertyChanged -= _book_cover_PropertyChanged;
                    _book_cover.PropertyChanged += _book_cover_PropertyChanged;

                    panel1.Enabled = true;

                    PatientBookCoverBS.DataSource = _book_cover;
                }
            }
        }

        private Models.TabAddressModels Address;
        private Models.CorpMedicalRptModels Medical;
        private void GetData(int? tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    string nation = regis.trn_patient.tpt_nation_code == "TH" ? "TH" : "EN";
                    string nameTH = "";
                    string nameEN = "";
                    if (nation == "TH")
                    {
                        nameTH = string.Join(" ", regis.trn_patient.tpt_othername.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                        if (regis.trn_patient.tpt_en_name1 != null && regis.trn_patient.tpt_en_name1.Trim() != "")
                        {
                            List<string> colectName = new List<string>();
                            colectName.Add(regis.trn_patient.tpt_pre_name.Trim());
                            colectName.Add(regis.trn_patient.tpt_en_name1.Trim());

                            if (regis.trn_patient.tpt_en_name3 != null && regis.trn_patient.tpt_en_name3.Trim() != "")
                                colectName.Add(regis.trn_patient.tpt_en_name3.Trim());

                            if (regis.trn_patient.tpt_en_name2 != null && regis.trn_patient.tpt_en_name2.Trim() != "")
                                colectName.Add(regis.trn_patient.tpt_en_name2.Trim());

                            nameEN = string.Join(" ", colectName);
                            nameEN = replaceTitle(nameEN);
                        }
                    }
                    else
                    {
                        nameTH = string.Join(" ", regis.trn_patient.tpt_othername.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                        nameEN = string.Join(" ", regis.trn_patient.tpt_othername.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));
                        nameEN = replaceTitle(nameEN);
                    }

                    Address = new Models.TabAddressModels
                              {
                                  tpr_id = regis.tpr_id,
                                  nameTH = nameTH,
                                  nameEN = nameEN,
                                  patientType = regis.tpr_patient_type == '1' ? "G" : "C",
                                  lang = regis.trn_patient.tpt_nation_code == "TH" ? "TH" : "EN"
                              };

                    var addHome = new Models.AddressModels
                                  {
                                      address = regis.tpr_other_address == null || regis.tpr_other_address == ""
                                                ? (regis.tpr_main_address == null ? "" : regis.tpr_main_address.Trim())
                                                : (regis.tpr_other_address == null ? "" : regis.tpr_other_address.Trim()),
                                      subArea = regis.tpr_other_address == null || regis.tpr_other_address == ""
                                                ? (regis.tpr_main_tumbon == null ? "" : regis.tpr_main_tumbon.Trim())
                                                : (regis.tpr_other_tumbon == null ? "" : regis.tpr_other_tumbon.Trim()),
                                      area = regis.tpr_other_address == null || regis.tpr_other_address == ""
                                             ? (regis.tpr_main_amphur == null ? "" : regis.tpr_main_amphur.Trim())
                                             : (regis.tpr_other_amphur == null ? "" : regis.tpr_other_amphur.Trim()),
                                      province = regis.tpr_other_address == null || regis.tpr_other_address == ""
                                                 ? (regis.tpr_main_province == null ? "" : regis.tpr_main_province.Trim())
                                                 : (regis.tpr_other_province == null ? "" : regis.tpr_other_province.Trim()),
                                      postal = regis.tpr_other_address == null || regis.tpr_other_address == ""
                                               ? (regis.tpr_main_zip_code == null ? "" : regis.tpr_main_zip_code.Trim())
                                               : (regis.tpr_other_zip_code == null ? "" : regis.tpr_other_zip_code.Trim()),
                                      phone = regis.tpr_mobile_phone == null || regis.tpr_mobile_phone == ""
                                              ? (regis.tpr_home_phone == null ? "" : regis.tpr_home_phone.Trim())
                                              : (regis.tpr_mobile_phone == null ? "" : regis.tpr_mobile_phone.Trim())
                                  };
                    Address.listAddress = new List<Models.TabAddressModels.Address>();
                    Models.TabAddressModels.Address aHome = new Models.TabAddressModels.Address()
                    {
                        type = "H",
                        address = CheckAddress(addHome),
                        phone = addHome.phone,
                        fax = addHome.fax
                    };
                    Address.listAddress.Add(aHome);
                    Models.TabAddressModels.Address aCom = new Models.TabAddressModels.Address()
                    {
                        type = "C"
                    };
                    Address.listAddress.Add(aCom);
                    AddressComBS.DataSource = aCom;

                    Medical = new Models.CorpMedicalRptModels();
                    MedReportBS.DataSource = Medical;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TabAddressUC", "GetData(int? tpr_id)", ex, false);
            }
        }

        private string replaceTitle(string str)
        {
            string res = null;

            string title = string.IsNullOrEmpty(str.Split(' ')[0]) ? string.Empty : str.Split(' ')[0];

            switch(title){
                case "นาย": res = Regex.Replace(str,"นาย","MR."); break;
                case "นาง": res = Regex.Replace(str, "นาง", "MRS."); break;
                case "นางสาว": res = Regex.Replace(str, "นางสาว", "MISS"); break;
                case "น.ส.": res = Regex.Replace(str, "น.ส.", "MISS"); break;
                default: res = str; break;
            }

            return res;
        }
        private void ClearAddress()
        {
            txtHomeAddress.Text = "";
            txtHomePhone.Text = "";
            txtHomeFax.Text = "";
        }
        private void ClearCompany()
        {
            txtComLegal.Text = "";
            txtContactPerson.Text = "";
            txtComAddress.Text = "";
            txtComPhone.Text = "";
            txtComFax.Text = "";
        }
        private void ClearMed()
        {
            checkBoxBinding1.Checked = false;
            checkBoxBinding2.Checked = false;
            checkBoxBinding3.Checked = false;
            checkBoxBinding4.Checked = false;
            checkBoxBinding6.Checked = false;
            checkBoxBinding7.Checked = false;
            checkBoxBinding5.Checked = false;
        }
        private void BindAddress()
        {
            if (Address != null)
            {
                var add = Address.listAddress.Where(x => x.type == "H").FirstOrDefault();
                txtHomeAddress.Text = add.address;
                txtHomePhone.Text = add.phone;
                txtHomeFax.Text = add.fax;
            }
        }
        private void BindCompany()
        {
            if (Address != null)
            {
                var add = Address.listAddress.Where(x => x.type == "C").FirstOrDefault();
                if (_book_cover.tpbc_create_date == null || _book_cover.tpbc_emp_id != null)
                {
                    txtEmpID.Text = string.IsNullOrEmpty(_book_cover.tpbc_emp_id) ? add.empID : _book_cover.tpbc_emp_id;
                }
                txtContactPerson.Text = add.contactPerson;
                txtComAddress.Text = add.address;
                txtComPhone.Text = add.phone;
                txtComFax.Text = add.fax;
            }
        }

        private void _book_cover_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tpbc_patient_type")
            {
                if (_book_cover != null)
                {
                    if (_book_cover.tpbc_patient_type == "G")
                    {
                        _book_cover.tpbc_emp_id = "";
                        _book_cover.tcd_document_no = "";
                        _book_cover.tpbc_position = "";
                        _book_cover.tpbc_department = "";
                        autoCompleteComName.DataSource = null;
                        autoCompleteComName.ReadOnly = true;
                        txtEmpID.ReadOnly = true;
                        txtPosition.ReadOnly = true;
                        txtDepart.ReadOnly = true;
                    }
                    else if (_book_cover.tpbc_patient_type == "C")
                    {
                        using (InhToDoListDataContext tdc = new InhToDoListDataContext())
                        {
                            DateTime dateArrived = _book_cover.trn_patient_regi.trn_patient_regis_detail.tpr_real_arrived_date.Value;

                            var clist = new Class.CompanyCls().GetListComp(dateArrived);
                            autoCompleteComName.DataSource = clist;
                            autoCompleteComName.DisplayMember = "comName";
                            autoCompleteComName.ValueMember = "doc_no";

                            if (string.IsNullOrEmpty(_book_cover.tcd_document_no))
                            {
                                var list_tcd = clist.Select(x => x.doc_no).ToList();
                                var res = new Class.CompanyCls().MappingPatient(_book_cover.tpr_id, list_tcd);
                                _book_cover.tcd_document_no = res.document_no;
                                if (string.IsNullOrEmpty(_book_cover.tpbc_emp_id)) _book_cover.tpbc_emp_id = res.emp_id;
                                if (string.IsNullOrEmpty(_book_cover.tpbc_position)) _book_cover.tpbc_position = res.position;
                                if (string.IsNullOrEmpty(_book_cover.tpbc_department)) _book_cover.tpbc_department = res.department;
                                //var list_tcd = clist.Select(x => x.tcd_id).ToList();
                                //var pers_id = _book_cover.trn_patient_regi.trn_patient.tpt_id_card;
                                //string doc_no = null;
                                //string emp_id = null;
                                //if (!string.IsNullOrEmpty(pers_id) && !string.IsNullOrEmpty(pers_id.Trim()))
                                //{
                                //    var person = tdc.trn_name_checks
                                //                    .Where(x => list_tcd.Contains(x.tcd_id) &&
                                //                                x.tnc_personal_id == pers_id)
                                //                    .Select(x => new
                                //                    {
                                //                        doc_no = x.trn_company_detail.tcd_document_no,
                                //                        emp_id = x.tnc_emp_id
                                //                    }).FirstOrDefault();
                                //    if (person != null)
                                //    {
                                //        doc_no = person.doc_no;
                                //        emp_id = person.emp_id;
                                //    }
                                //}

                                //if (doc_no == null)
                                //{
                                //    var fname = _book_cover.trn_patient_regi.trn_patient.tpt_first_name;
                                //    var lname = _book_cover.trn_patient_regi.trn_patient.tpt_last_name;
                                //    var person = tdc.trn_name_checks
                                //                .Where(x => list_tcd.Contains(x.tcd_id) &&
                                //                            x.tnc_fname == fname &&
                                //                            x.tnc_lname == lname)
                                //                .Select(x => new
                                //                {
                                //                    doc_no = x.trn_company_detail.tcd_document_no,
                                //                    emp_id = x.tnc_emp_id
                                //                }).FirstOrDefault();
                                //    if (person != null)
                                //    {
                                //        doc_no = person.doc_no;
                                //        emp_id = person.emp_id;
                                //    }
                                //}

                                //if (doc_no != null)
                                //{
                                //    _book_cover.tcd_document_no = doc_no;
                                //    _book_cover.tpbc_emp_id = emp_id;
                                //}
                            }
                        }
                        autoCompleteComName.ReadOnly = false;
                        txtEmpID.ReadOnly = false;
                        txtPosition.ReadOnly = false;
                        txtDepart.ReadOnly = false;
                    }
                }
            }
            else if (e.PropertyName == "tpbc_cover_lang")
            {
                if (_book_cover != null)
                {
                    if (_book_cover.tpbc_cover_lang == "TH")
                    {
                        _book_cover.tpbc_patient_name = Address.nameTH;
                    }
                    else if (_book_cover.tpbc_cover_lang == "EN")
                    {
                        _book_cover.tpbc_patient_name = Address.nameEN;
                    }
                }
            }
            else if (e.PropertyName == "tpbc_address")
            {
                if (_book_cover != null)
                {
                    if (_book_cover.tpbc_address == "H")
                    {
                        txtAddress.ReadOnly = true;
                        var addHome = Address.listAddress.Where(x => x.type == "H").FirstOrDefault();
                        _book_cover.tpbc_patient_address = addHome.address;
                    }
                    else if (_book_cover.tpbc_address == "C")
                    {
                        txtAddress.ReadOnly = true;
                        var addCom = Address.listAddress.Where(x => x.type == "C").FirstOrDefault();
                        _book_cover.tpbc_patient_address = addCom.address;
                    }
                    else if (_book_cover.tpbc_address == "O")
                    {
                        txtAddress.ReadOnly = false;
                    }
                }
            }
            else if (e.PropertyName == "tcd_document_no")
            {
                if (_book_cover != null)
                {
                    if (string.IsNullOrEmpty(_book_cover.tcd_document_no))
                    {
                        _book_cover.tcd_document_no = "";
                        //_book_cover.tpbc_emp_id = ""; //--  Still  Staff id
                      
                        
                        var addCom = Address.listAddress.Where(x => x.type == "C").FirstOrDefault();
                       
                        addCom.contactPerson = "";
                        addCom.address = "";
                        addCom.phone = "";
                        addCom.fax = "";
                    }
                    else
                    {
                        using (InhToDoListDataContext tdc = new InhToDoListDataContext())
                        {
                            var com = tdc.trn_company_details
                                         .Where(x => x.tcd_document_no == _book_cover.tcd_document_no)
                                         .OrderByDescending(x => x.tcd_create_date)
                                         .Select(x => new
                                         {
                                             tcd_id = x.tcd_id,
                                             contacts = x.trn_contact_persons,
                                             adress = x.tcd_address == null ? "" : x.tcd_address.Trim(),
                                             subArea = x.tcd_tambon == null ? "" : x.tcd_tambon.Trim(),
                                             area = x.tcd_district == null ? "" : x.tcd_district.Trim(),
                                             province = x.tcd_province == null ? "" : x.tcd_province.Trim(),
                                             postal = x.tcd_postcode == null ? "" : x.tcd_postcode.Trim(),
                                             phone = x.tcd_tel == null ? "" : x.tcd_tel.Trim(),
                                             fax = x.tcd_fax == null ? "" : x.tcd_fax.Trim(),
                                             sendRptMst = x.tcd_send_rep_real == "Y" ? "E" : x.tcd_send_rep_flag,
                                             sendRptCopy = x.tcd_send_rep_copy,
                                             legel = x.tcd_legal
                                         }).FirstOrDefault();
                            var addCom = Address.listAddress.Where(x => x.type == "C").FirstOrDefault();
                            addCom.contactPerson = com.contacts.Select(x => x.tcp_name).FirstOrDefault();
                            addCom.address = CheckAddress(new Models.AddressModels { address = com.adress, subArea = com.subArea, area = com.area, province = com.province, postal = com.postal });
                            addCom.phone = com.phone;
                            addCom.fax = com.fax;
                            addCom.legel = com.legel;

                            Medical.Clear();
                            ClearMed();
                            Medical.rptMst = com.sendRptMst;
                            Medical.rptCopy = com.sendRptCopy;
                            var rpt = tdc.trn_medical_reports
                                         .Where(x => x.tcd_id == com.tcd_id)
                                         .Select(x => new { x.mmr_id, x.tmr_rep_remark })
                                         .ToList();

                            foreach (var r in rpt)
                            {
                                switch (r.mmr_id)
                                {
                                    case 1:
                                        checkBoxBinding1.Checked = true;
                                        break;
                                    case 2:
                                        checkBoxBinding2.Checked = true;
                                        break;
                                    case 3:
                                        checkBoxBinding3.Checked = true;
                                        break;
                                    case 4:
                                        checkBoxBinding4.Checked = true;
                                        break;
                                    case 5:
                                        checkBoxBinding6.Checked = true;
                                        break;
                                    case 6:
                                        checkBoxBinding7.Checked = true;
                                        break;
                                    case 7:
                                        checkBoxBinding5.Checked = true;
                                        Medical.remarkOther = r.tmr_rep_remark;
                                        break;
                                }
                            }
                        }
                    }
                    AddressComBS.ResetCurrentItem();
                    MedReportBS.ResetCurrentItem();
                    _book_cover_PropertyChanged(_book_cover, new PropertyChangedEventArgs("tpbc_address"));
                }
            }
        }
        private List<string> Bangkok = new List<string> { "กทม", "กทม.", "กรุงเทพฯ", "กรุงเทพมหานคร" };
        private string CheckAddress(Models.AddressModels address)
        {
            Models.AddressModels nAdd = new Models.AddressModels
            {
                address = address.address,
                subArea = CropLastIsComma(address.subArea),
                area = CropLastIsComma(address.area),
                province = CropLastIsComma(address.province),
                postal = address.postal
            };
            if (nAdd.province.Length == 0 || !Regex.IsMatch(nAdd.province.Substring(0, 1), @"[a-zA-z]"))
            {
                if (Bangkok.Contains(nAdd.province))
                {
                    return nAdd.address + Environment.NewLine +
                           nAdd.subArea + " " + nAdd.area + Environment.NewLine +
                           nAdd.province + " " + nAdd.postal;
                }
                else
                {
                    return nAdd.address + Environment.NewLine +
                           nAdd.subArea + " " + nAdd.area + Environment.NewLine +
                           nAdd.province + " " + nAdd.postal;
                }
            }
            else
            {
                return nAdd.address + Environment.NewLine +
                       nAdd.subArea + ", " + nAdd.area + Environment.NewLine +
                       nAdd.province + " " + nAdd.postal;
            }
        }
        private string CropLastIsComma(string str)
        {
            string tmp = str;
            while (tmp.Length > 0 && tmp.LastIndexOf(",") == tmp.Length - 1)
            {
                tmp = tmp.Substring(0, tmp.Length - 1);
            }
            return tmp;
        }
    }
}
