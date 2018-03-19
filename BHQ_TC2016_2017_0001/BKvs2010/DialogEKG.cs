using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{
    public partial class DialogEKG : Form
    {
        public DialogEKG()
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
                    ekguc1.PatientRegis = null;
                    _tpr_id = null;
                }
                else
                {
                    dbc = new InhCheckupDataContext();
                    trn_patient_regi patientRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                    if (patientRegis == null)
                    {
                        btnSave.Enabled = false;
                        ekguc1.PatientRegis = null;
                        _tpr_id = null;
                    }
                    else
                    {
                        ekguc1.PatientRegis = patientRegis;
                        _tpr_id = value;
                        btnSave.Enabled = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ekguc1.EndEdit();
                try
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == this._tpr_id &&
                                                                        x.tpbr_radiology == "EK")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = (int)this._tpr_id,
                            tpbr_radiology = "EK",
                            tpbr_create_by = Program.CurrentUser.mut_username,
                            tpbr_create_date = dateNow
                        };
                        dbc.trn_patient_book_results.InsertOnSubmit(bookResult);
                    }
                    bookResult.tpbr_flag_saved = true;
                    bookResult.tpbr_show_sections = true;
                    bookResult.tpbr_show_summary = true;
                    bookResult.tpbr_not_show_report = false;
                    bookResult.tpbr_active = true;
                    bookResult.tpbr_update_by = Program.CurrentUser.mut_username;
                    bookResult.tpbr_update_date = dateNow;
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
