using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;

namespace BKvs2010
{
    public partial class DialogPHM : Form
    {
        public DialogPHM()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc;

        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    btnSave.Enabled = false;
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        dbc = new InhCheckupDataContext();
                        trn_patient_regi patientRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (patientRegis == null)
                        {
                            btnSave.Enabled = false;
                            _tpr_id = null;
                        }
                        else
                        {
                            personalHealthManagmentUC1.PatientRegis = patientRegis;
                            //personalHealthManagmentUC1.isDoctorRoom = false;
                            _tpr_id = value;
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
                        _tpr_id = null;
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                personalHealthManagmentUC1.EndEdit();
                try
                {
                    //try
                    //{
                    //    trn_patient_regi patientRegis = dbc.trn_patient_regis
                    //                                       .Where(x => x.tpr_id == this._tpr_id)
                    //                                       .FirstOrDefault();
                    //    trn_eye_exam_hdr eyeHdr = patientRegis.trn_eye_exam_hdrs.FirstOrDefault();
                    //    if (eyeHdr != null)
                    //    {
                    //        eyeHdr.teh_is_eye_record = true;
                    //    }
                    //}
                    //catch
                    //{

                    //}
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                lbAlertMsg.Text = "Save Data Complete.";
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = "โปรดลองอีกครั้ง";
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
            }
        }
    }
}
