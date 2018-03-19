using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmBookPrint : Form
    {
        public frmBookPrint()
        {
            InitializeComponent();
        }
        private void frmBookPrint_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            btnClear_Click(null, null);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            dateTimePicker2.Checked = false;
            dateTimePicker1.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            chkNB.Checked = true;
            chkBC.Checked = true;
            chkBP.Checked = true;
            chkBF.Checked = true;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == true)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Short;
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Checked == true)
            {
                dateTimePicker2.Format = DateTimePickerFormat.Short;
            }
            else
            {
                dateTimePicker2.Format = DateTimePickerFormat.Custom;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {

                List<string> status = new List<string>();
                if (chkNB.Checked) status.Add("NB");
                if (chkBC.Checked) status.Add("BC");
                if (chkBP.Checked) status.Add("BP");
                if (chkBF.Checked) status.Add("BF");
                DateTime? arrive = null;
                if (dateTimePicker1.Checked) arrive = dateTimePicker1.Value.Date;
                DateTime? confirm = null;
                if (dateTimePicker2.Checked) confirm = dateTimePicker2.Value.Date;
                var result = dbc.trn_patient_books.Where(x =>
                    (x.trn_patient_regi.trn_patient.tpt_hn_no.Contains(txtSearch.Text.Trim()) ||
                     x.trn_patient_regi.trn_patient.tpt_othername.Contains(txtSearch.Text.Trim()) ||
                     x.trn_patient_regi.tpr_en_no.Contains(txtSearch.Text.Trim())) &&
                    x.trn_patient_regi.tpr_arrive_date.Value.Date == (dateTimePicker1.Checked ? dateTimePicker1.Value.Date : x.trn_patient_regi.tpr_arrive_date.Value.Date) &&
                    (!dateTimePicker2.Checked ? true :
                     x.tpb_confirm_date == null ? false :
                     x.tpb_confirm_date.Value.Date == dateTimePicker2.Value.Date) &&
                    status.Contains(x.tpb_status)).ToList()
                    .Select((x, index) => new
                    {
                        no = index + 1,
                        hn = x.tpb_hn_no,
                        en = x.tpb_en_no,
                        name = x.trn_patient_regi.trn_patient.tpt_othername,
                        arriveDate = x.trn_patient_regi.tpr_arrive_date,
                        preview = string.IsNullOrEmpty(x.tpb_file_name) ? null : "",
                        finish = new List<string> { "BP", "BC" }.Contains(x.tpb_status) ? "" : null,
                        printDate = x.tpb_print_date,
                        printBy = x.tpb_print_by,
                        confirmDate = x.tpb_confirm_date,
                        confirmBy = x.tpb_confirm_by,
                        pathFile = string.IsNullOrEmpty(x.tpb_server_path) || string.IsNullOrEmpty(x.tpb_path_file) || string.IsNullOrEmpty(x.tpb_file_name) 
                                   ? ""
                                   : @"\\" + x.tpb_server_path + @"\" + x.tpb_path_file + @"\" + x.tpb_file_name,
                        tpb_id = x.tpb_id,
                        type = x.tpb_type == "BC" ? "Book Color" :
                               x.tpb_type == "BK" ? "Book" :
                               x.tpb_type == "OP" ? "One Page" :
                               "Other",
                        language = x.tpb_language == "TH" ? "Thai" :
                                   x.tpb_language == "EN" ? "English" :
                                   "Other",
                        package = x.tpb_package_code
                    }).ToList();
                dataGridView1.DataSource = result;
                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells["colPreview"].Value == null)
                    {
                        dr.Cells["colPreview"] = new DataGridViewTextBoxCell();
                        dr.Cells[0].ReadOnly = true;
                    }
                    else
                    {
                        dr.Cells[0].ReadOnly = false;
                    }
                    if (dr.Cells["colFinish"].Value == null) dr.Cells["colFinish"] = new DataGridViewTextBoxCell();
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "colPreview")
                {
                    int tpb_id = (int)dataGridView1["col_tpb_id", e.RowIndex].Value;
                    previewBook(tpb_id);
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "colFinish")
                {
                    int tpb_id = (int)dataGridView1["col_tpb_id", e.RowIndex].Value;
                    if (popupFinish(tpb_id))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["colFinish"] = new DataGridViewTextBoxCell();
                    }
                }
            }
        }
        private bool popupFinish(int tpb_id)
        {
            using (frmPopupBookFinishRemark frm = new frmPopupBookFinishRemark())
            {
                frmPopupBookFinishRemark.bookFinish _bookFinish = frm.popupFinish();
                if (_bookFinish.save == true)
                {
                    try
                    {
                        using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                        {
                            DateTime dateNow = Program.GetServerDateTime();
                            int tpr_id = dbc.trn_patient_books.Where(x => x.tpb_id == tpb_id).Select(x => x.tpr_id).FirstOrDefault();
                            trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            tpr.tpr_status = "CP";

                            trn_patient_book tpb = tpr.trn_patient_books.Where(x => x.tpb_id == tpb_id).FirstOrDefault();
                            tpb.tpb_status = "BF";
                            tpb.tpb_send_email = _bookFinish.detail.send_email;
                            tpb.tpb_send_post = _bookFinish.detail.send_post;
                            tpb.tpb_finish_remark = _bookFinish.detail.remark;
                            tpb.tpb_update_by = Program.CurrentUser.mut_username;
                            tpb.tpb_update_date = dateNow;
                            tpb.tpb_finish_by = Program.CurrentUser.mut_username;
                            tpb.tpb_finish_date = dateNow;


                            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                            mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                            mst_room_hdr mrm_finish = mst.GetMstRoomHdr("FN", mhs.mhs_code);
                            //mst_room_dtl mrd_finish = dbc.mst_room_dtls.Where(x => x.mrm_id == mrm_finish.mrm_id).FirstOrDefault();
                            mst_event mvt_finish = mst.GetMstEvent("FN");
                            trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_finish.mrm_id).FirstOrDefault();

                            if (tps == null)
                            {
                                tps = new trn_patient_queue();
                                tpr.trn_patient_queues.Add(tps);
                            }
                            //tps.mrd_id = mrd_finish.mrd_id;
                            tps.mrm_id = mrm_finish.mrm_id;
                            tps.mvt_id = mvt_finish.mvt_id;
                            tps.tps_bm_seq = null;
                            tps.tps_end_date = dateNow;
                            tps.tps_call_by = Program.CurrentUser.mut_username;
                            tps.tps_call_date = dateNow;
                            tps.tps_call_status = null;
                            tps.tps_update_by = Program.CurrentUser.mut_username;
                            tps.tps_update_date = dateNow;
                            tps.tps_ns_status = null;
                            tps.tps_status = "LR";

                            dbc.SubmitChanges();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            return false;
        }
        private void previewBook(int tpb_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_patient_book tpb = dbc.trn_patient_books.Where(x => x.tpb_id == tpb_id).FirstOrDefault();
                    string path = @"\\" + tpb.tpb_server_path + @"\" + tpb.tpb_path_file + @"\" + tpb.tpb_file_name;
                    if (File.Exists(path))
                    {
                        try
                        {
                            Process.Start(path);
                        }
                        catch
                        {
                            lbAlert.Text = "Cannot Open File.";
                        }
                    }
                    else
                    {
                        lbAlert.Text = "File not Exists or Connect Server Error.";
                    }


                    if (tpb.tpb_print_by == null)
                    {
                        tpb.tpb_reprint = 0;
                    }
                    else
                    {
                        tpb.tpb_reprint++;
                    }

                    DateTime dateNow = Program.GetServerDateTime();
                    tpb.tpb_status = "BP";
                    tpb.tpb_update_by = Program.CurrentUser.mut_username;
                    tpb.tpb_update_date = dateNow;
                    tpb.tpb_print_by = Program.CurrentUser.mut_username;
                    tpb.tpb_print_date = dateNow;

                    trn_patient_regi tpr = tpb.trn_patient_regi;
                    tpr.tpr_print_book = "C";

                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                    mst_room_hdr mrm_book = mst.GetMstRoomHdr("BK", mhs.mhs_code);
                    mst_room_dtl mrd_book = dbc.mst_room_dtls.Where(x => x.mrm_id == mrm_book.mrm_id).FirstOrDefault();
                    mst_event mvt_book = mst.GetMstEvent("BK");
                    trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_book.mrm_id).FirstOrDefault();

                    if (tps == null)
                    {
                        tps = new trn_patient_queue();
                        tpr.trn_patient_queues.Add(tps);
                    }
                    tps.mrd_id = mrd_book.mrd_id;
                    tps.mrm_id = mrm_book.mrm_id;
                    tps.mvt_id = mvt_book.mvt_id;
                    tps.tps_bm_seq = null;
                    tps.tps_call_by = Program.CurrentUser.mut_username;
                    tps.tps_call_date = dateNow;
                    tps.tps_call_status = null;
                    tps.tps_end_date = dateNow;
                    tps.tps_update_by = Program.CurrentUser.mut_username;
                    tps.tps_update_date = dateNow;
                    tps.tps_ns_status = null;
                    tps.tps_status = "LR";

                    mst_room_hdr mrm_finish = mst.GetMstRoomHdr("FN", mhs.mhs_code);
                    mst_event mvt_finish = mst.GetMstEvent("FN");
                    tpr.trn_patient_queues.Add(new trn_patient_queue
                    {
                        mrd_id = null,
                        mrm_id = mrm_finish.mrm_id,
                        mvt_id = mvt_finish.mvt_id,
                        tps_bm_seq = null,
                        tps_start_date = dateNow,
                        tps_call_by = Program.CurrentUser.mut_username,
                        tps_call_date = dateNow,
                        tps_call_status = null,
                        tps_update_by = Program.CurrentUser.mut_username,
                        tps_update_date = dateNow,
                        tps_ns_status = "QL",
                        tps_status = "NS"
                    });

                    dbc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                bool val;
                if (dr.Cells[0].Value == null)
                {
                    val = false;
                }
                else
                {
                    val = (bool)dr.Cells[0].Value;
                }
                if (val == true)
                {
                    previewBook((int)dr.Cells["col_tpb_id"].Value);
                }
            }
        }
    }
}