using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Linq;
using DBCheckup;
using BKvs2010.EmrClass;

namespace BKvs2010.UserControlEMR
{
    public partial class DentalUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public DentalUC()
        {
            InitializeComponent();

            foreach (Control ctrl in PanelTeethBtn.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    Button btn = (Button)ctrl;
                    if (btn.Tag != null)
                    {
                        string[] tag = btn.Tag.ToString().Split(',');
                        if (tag.Count() == 2)
                        {
                            btn.Click += new EventHandler(selectProblem_Click);
                        }
                    }
                }
            }
            
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_teeth_hdr teeth = BsPatientTeeth.OfType<trn_teeth_hdr>().FirstOrDefault();
                if (teeth != null)
                {
                    if (e == null)
                    {
                        txtDoctorCode.Text = "";
                        teeth.tth_doctor_code = null;
                        teeth.tth_doctor_license = null;
                        teeth.tth_doctor_name_en = null;
                        teeth.tth_doctor_name_th = null;
                    }
                    else
                    {
                        txtDoctorCode.Text = ((DoctorProfile)e).SSUSR_Initials;
                        teeth.tth_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        teeth.tth_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        teeth.tth_doctor_name_en = dn.NameEN;
                        teeth.tth_doctor_name_th = dn.NameTH;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }
        private void selectProblem_Click(object sender, EventArgs e)
        {
            if (_PatientRegis != null)
            {
                if (sender.GetType() == typeof(Button))
                {
                    Button btn = (Button)sender;
                    if (btn.Tag != null)
                    {
                        string[] tag = btn.Tag.ToString().Split(',');
                        if (tag.Count() == 2)
                        {
                            string teeth_up = tag[0].ToString().Trim();
                            int teeth_location = Convert.ToInt32(tag[1]);

                            List<trn_teeth_dtl> result_teeth = BSTeethDtl.OfType<trn_teeth_dtl>()
                                                               .Where(x => x.ttd_teeth_up.Trim() == teeth_up &&
                                                                           x.ttd_teeth_location == teeth_location).ToList();

                            List<DentalSelectProblemFrm.sourceProblem> old_problem = result_teeth.Select(x => new DentalSelectProblemFrm.sourceProblem
                            {
                                mdr_id = x.mdr_id,
                                mdr_code = x.mdr_code
                            }).ToList();

                            DentalSelectProblemFrm.selectProblem selectProblem = new DentalSelectProblemFrm.selectProblem
                            {
                                Selected = false,
                                mhs_id = bsPatientRegis.OfType<trn_patient_regi>().FirstOrDefault().mhs_id,
                                Problem = old_problem
                            };

                            DentalSelectProblemFrm frm = new DentalSelectProblemFrm();
                            DentalSelectProblemFrm.selectProblem new_selected = frm.getProblem(selectProblem);
                            if (new_selected.Selected == true)
                            {
                                List<DentalSelectProblemFrm.sourceProblem> new_problem = new_selected.Problem;

                                List<trn_teeth_dtl> list_remove_dtl = BSTeethDtl.OfType<trn_teeth_dtl>()
                                                                      .Where(x => x.ttd_teeth_up.Trim() == teeth_up &&
                                                                                  x.ttd_teeth_location == teeth_location &&
                                                                                  !new_problem.Contains(new DentalSelectProblemFrm.sourceProblem
                                                                                  {
                                                                                      mdr_id = x.mdr_id,
                                                                                      mdr_code = x.mdr_code
                                                                                  })).ToList();
                                if (list_remove_dtl.Count() > 0)
                                {
                                    list_remove_dtl.ForEach(x => BSTeethDtl.Remove(x));
                                }

                                List<DentalSelectProblemFrm.sourceProblem> list_add_dtl = new_problem.Where(x => !old_problem.Contains(x)).ToList();
                                if (list_add_dtl.Count() > 0)
                                {
                                    list_add_dtl.ForEach(x =>
                                    {
                                        BSTeethDtl.Add(new trn_teeth_dtl
                                        {
                                            mdr_id = x.mdr_id,
                                            mdr_code = x.mdr_code,
                                            ttd_teeth_up = teeth_up,
                                            ttd_teeth_location = teeth_location,
                                            ttd_create_by = username,
                                            ttd_update_by = username
                                        });
                                    });
                                }

                                trn_teeth_hdr patient_teeth = BsPatientTeeth.OfType<trn_teeth_hdr>().FirstOrDefault();
                                patient_teeth.tth_bad_tooth_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "F").Count();
                                patient_teeth.tth_down_tooth_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "A").Count();
                                patient_teeth.tth_thimble_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "R").Count();
                                patient_teeth.tth_replace_tooth_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "S").Count();
                                patient_teeth.tth_thimble_out_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "C").Count();
                                patient_teeth.tth_put_tooth_total = BSTeethDtl.OfType<trn_teeth_dtl>().Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "X").Count();

                                List<string> result = new_problem.Select(x => x.mdr_code).ToList();
                                if (result.Count() > 0)
                                {
                                    btn.Text = string.Join(",", result);
                                }
                                else
                                {
                                    btn.Text = "";
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool _isDoctorRoom = false;
        public bool isDoctorRoom
        {
            get { return _isDoctorRoom; }
            set { _isDoctorRoom = value; }
        }

        private string username = null;
        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;                        

                        var objproblems = new EmrClass.FunctionDataCls().getDoctorResult(1, "TE", "TH")
                                          .Select(x => new clsSourceMaster
                                          {
                                              val = x.mdr_id,
                                              dis = x.mdr_tname
                                          }).ToList();

                        chList.DataSource = objproblems;
                        chList.DisplayMember = "dis";
                        chList.ValueMember = "val";

                        trn_teeth_hdr patientTeeth = value.trn_teeth_hdrs.FirstOrDefault();
                        if (patientTeeth == null)
                        {
                            patientTeeth = new trn_teeth_hdr();
                            patientTeeth.tth_create_by = username;
                            value.trn_teeth_hdrs.Add(patientTeeth);
                        }
                        patientTeeth.tth_update_by = username;
                        
                        foreach (Control ctrl in PanelTeethBtn.Controls)
                        {
                            if (ctrl.GetType() == typeof(Button))
                            {
                                Button btn = (Button)ctrl;
                                if (btn.Tag != null)
                                {
                                    string[] tag = btn.Tag.ToString().Split(',');
                                    if (tag.Count() == 2)
                                    {
                                        string teeth_up = tag[0].ToString().Trim();
                                        int teeth_location = Convert.ToInt32(tag[1]);

                                        List<trn_teeth_dtl> list_teeth_dtl = patientTeeth.trn_teeth_dtls
                                                                             .Where(x => x.ttd_teeth_up.Trim() == teeth_up &&
                                                                                         x.ttd_teeth_location == teeth_location).ToList();

                                        if (list_teeth_dtl.Count() > 0)
                                        {
                                            List<string> list_mdr_code_dtl = list_teeth_dtl.Select(x => x.mdr_code).ToList();
                                            btn.Text = string.Join(",", list_mdr_code_dtl);
                                        }
                                    }
                                }
                            }
                        }

                        List<int> list_mdr_id_reult = patientTeeth.trn_teeth_doc_results.Select(x => x.mdr_id).ToList();
                        foreach (int mdr_id in list_mdr_id_reult)
                        {
                            for (int i = 0; i < chList.Items.Count; i++)
                            {
                                clsSourceMaster row = (clsSourceMaster)chList.Items[i];
                                if (mdr_id == row.val)
                                {
                                    chList.SetItemChecked(i, true);
                                    break;
                                }
                            }
                        }
                        patientTeeth_PropertyChanged(patientTeeth, new PropertyChangedEventArgs("tth_thimble_of_state"));
                        patientTeeth.PropertyChanged += new PropertyChangedEventHandler(patientTeeth_PropertyChanged);

                        _PatientRegis = value;
                        bsPatientRegis.DataSource = value;
                        this.Enabled = true;

                        if (_isDoctorRoom == true)
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                            txtDoctorCode.Enabled = false;
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = patientTeeth.tth_doctor_code;
                        }
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }
        private void patientTeeth_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tth_thimble_of_state")
            {
                var val = TypeDescriptor.GetProperties(sender)["tth_thimble_of_state"].GetValue(sender);
                if (val == null)
                {
                    TypeDescriptor.GetProperties(sender)["tth_thimble_for_remark"].SetValue(sender, "");
                    txtthimbleforRemark.Enabled = false;
                }
                else
                {
                    if ((char)val == 'N')
                    {
                        TypeDescriptor.GetProperties(sender)["tth_thimble_for_remark"].SetValue(sender, "");
                        txtthimbleforRemark.Enabled = false;
                    }
                    else
                    {
                        txtthimbleforRemark.Enabled = true;
                    }
                }
            }
        }

        private class clsSourceMaster
        {
            public int val { get; set; }
            public string dis { get; set; }
        }

        //private void autoCompleteDoctor_currentChangeHandler(object sender, UIcontrol.completeArgs e)
        //{
        //    txtDoctorCode.Text = e.valueData == null ? "" : e.valueData.ToString();
        //    trn_teeth_hdr patientTeeth = BsPatientTeeth.OfType<trn_teeth_hdr>().FirstOrDefault();
        //    patientTeeth.tth_doctor_code = txtDoctorCode.Text;
        //}
        private void chList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            clsSourceMaster item = (clsSourceMaster)chList.Items[e.Index];
            trn_teeth_doc_result result = BsTeethDoctor.OfType<trn_teeth_doc_result>().Where(x => x.mdr_id == item.val).FirstOrDefault();
            if (e.NewValue == CheckState.Checked)
            {
                if (result == null)
                {
                    BsTeethDoctor.Add(new trn_teeth_doc_result
                    {
                        mdr_id = item.val,
                        tdr_create_by = username,
                        tdr_update_by = username
                    });
                }
            }
            else
            {
                if (result != null)
                {
                    BsTeethDoctor.Remove(result);
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                trn_teeth_hdr patientTeeth = BsPatientTeeth.OfType<trn_teeth_hdr>().FirstOrDefault();

                if (patientTeeth.tth_create_by == null)
                {
                    patientTeeth.tth_create_date = dateNow;
                }
                patientTeeth.tth_update_date = dateNow;

                List<trn_teeth_dtl> listTeethDtl = BSTeethDtl.OfType<trn_teeth_dtl>().OrderBy(x => x.ttd_teeth_up).ThenBy(x => x.ttd_teeth_location).ToList();
                int seq = 0;
                listTeethDtl.ForEach(x =>
                {
                    x.ttd_seq = seq++;
                    x.ttd_create_date = dateNow;
                    x.ttd_update_date = dateNow;
                });

                List<trn_teeth_doc_result> listTeethDoctor = BsTeethDoctor.OfType<trn_teeth_doc_result>().ToList();
                listTeethDoctor.ForEach(x =>
                {
                    x.tdr_create_date = dateNow;
                    x.tdr_update_date = dateNow;
                });
                bsPatientRegis.EndEdit();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SaveData", ex, false);
            }
        }
    }
}
