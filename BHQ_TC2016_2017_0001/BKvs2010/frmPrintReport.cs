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
    public partial class frmPrintReport : Form
    {
        public frmPrintReport()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private trn_patient_regi currentRegis = null;
        private void frmPrintReport_Load(object sender, EventArgs e)
        {
            /*
            select mhs.mhs_id, mhs.mhs_tname
            from mst_hpc_site mhs
            where mhs.mhs_type = 'P'
            and mhs.mhs_room_chkup = 'TRUE'
            and mhs.mhs_status = 'A'
            and CONVERT(date,GETDATE(),103) between
            CONVERT(date,isnull(mhs.mhs_effective_date,getdate()),103) and
            CONVERT(date,isnull(mhs.mhs_expire_date,getdate()),103)
            order by mhs.mhs_code;
             */
            List<mst_hpc_site> objsite = (from t1 in dbc.mst_hpc_sites
                                          where t1.mhs_status == 'A' && t1.mhs_type == 'P'
                                          && t1.mhs_room_chkup==true
                                          select t1).ToList();
            mst_hpc_site newselect = new mst_hpc_site();
            newselect.mhs_id = 0;
            newselect.mhs_ename = "Select All";
            objsite.Add(newselect);

            DDsite.DataSource = objsite.OrderBy(x => x.mhs_id).ToList();
            DDsite.DisplayMember = "mhs_ename";
            DDsite.ValueMember = "mhs_id";
            DDsite.SelectedValue = 0;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string strHN = txtHNEN.Text.Trim();
            var objPatient = (from t1 in dbc.trn_patient_regis
                              select new
                              {
                                  HN = t1.trn_patient.tpt_hn_no,
                                  EN = t1.tpr_en_no,
                                  Name = t1.trn_patient.tpt_othername,
                                  ArriveDte = t1.tpr_arrive_date
                              });
            if (strHN != "")
            {
                objPatient = objPatient.Where(x => x.HN.Contains(strHN));
            }
            if (txtDateArrive.Value.Date != null)
            {
                objPatient = objPatient.Where(x => x.ArriveDte.Value.Date == txtDateArrive.Value.Date);
            }
            GridPatient.DataSource = objPatient;
        }

        private void DDsite_SelectedValueChanged(object sender, EventArgs e)
        {
            /*
            select mrm.mrm_id,
            mrm.mrm_ename
            from mst_room_hdr mrm
            where mrm.mhs_id = 1
            and mrm.mrm_status = 'A'
            and CONVERT(date,GETDATE(),103) between
            CONVERT(date,isnull(mrm.mrm_effective_date,getdate()),103) and
            CONVERT(date,isnull(mrm.mrm_expire_date,getdate()),103);
            */
            int msh_id=Convert1.ToInt32( DDsite.SelectedValue);
            var datenow = Program.GetServerDateTime().Date;
            List<mst_room_hdr> objRoom = (from t1 in dbc.mst_room_hdrs
                                          where t1.mrm_status == 'A'
                                          && (t1.mrm_effective_date == null ||
                                                (t1.mrm_effective_date.Value.Date <= datenow
                                                    && (t1.mrm_expire_date == null ||
                                                        t1.mrm_expire_date.Value.Date >= datenow)
                                                     )
                                                )
                                          select t1).ToList();
            if (msh_id>0)
            {
                objRoom = objRoom.Where(x => x.mhs_id == msh_id).ToList();
            }

            mst_room_hdr roomselect = new mst_room_hdr();
            roomselect.mrm_id = 0;
            roomselect.mrm_ename = "Select All";
            objRoom.Add(roomselect);

            DDStation.DataSource = objRoom.OrderBy(x => x.mhs_id).ToList();
            DDStation.DisplayMember = "mrm_ename";
            DDStation.ValueMember = "mrm_id";
            if (msh_id==0)
            {
                DDStation.SelectedValue = 0;
            }

        }
        private void btnSearchReport_Click(object sender, EventArgs e)
        {
            int mhs_id = Convert1.ToInt32(DDsite.SelectedValue);
            int mrm_id = Convert1.ToInt32(DDStation.SelectedValue);
            ShowReport(mhs_id, mrm_id);
        }
        private void ShowReport(int mhs_id,int mrm_id)
        {
            //Load Grid
            /*
             select mhs.mhs_tname,
                mrm.mrm_ename,
                mrt.mrt_tname,
                mrt.mrt_path_file+mrt.mrt_file_name mrt_file
                from mst_hpc_site mhs inner join
                mst_room_hdr mrm on mhs.mhs_id = mrm.mhs_id inner join
                mst_report_grp mrg on mrm.mrm_id = mrg.mrm_id inner join
                mst_report_match mrh on mrg.mrg_id = mrh.mrg_id right outer join
                mst_report mrt on mrh.mrt_id = mrt.mrt_id
            where mhs.mhs_id = /ISNULL('All','All',mhs.mhs_id)/
                and mrm.mrm_id =
            order by mhs.mhs_id, mrm.mrm_id, mrt.mrt_id, mrt.mrt_report_seq
             */

            var objprintReport = (from t1 in dbc.vw_PrintReports
                                  select t1);
            if (mhs_id > 0)
            {
                objprintReport = objprintReport.Where(x => x.mhs_id==mhs_id);
            }
            if (mrm_id > 0)
            {
                objprintReport = objprintReport.Where(x => x.mrm_id == mrm_id);
            }
            GridReport.DataSource = objprintReport.OrderBy(x=>mhs_id).ThenBy(x=>x.mrm_id).ThenBy(x=>x.mrt_id).ThenBy(x=>x.mrt_report_seq);
            GridReport.Columns["ColReportPath"].Visible = false;
            GridReport.Columns["ColMrmID"].Visible = false;
            GridReport.Columns["ColSiteID"].Visible = false;
            GridReport.Columns["ColReportCode"].Visible = false;
            GridReport.Columns["Colmrt_report_seq"].Visible = false; 
            GridReport.Columns["Colmrt_id"].Visible = false;
            ch_selectAll.Checked = false;
        }

        private void GridPatient_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridPatient.Rows.Count; i++)
            {
                GridPatient.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        private void GridPatient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            string strEn = Convert1.ToString(GridPatient["ColEN", e.RowIndex].Value);
            var objcurrentRegi = (from t1 in dbc.trn_patient_regis
                                  where t1.tpr_en_no == strEn
                                  select t1).FirstOrDefault();
            if (objcurrentRegi != null)
            {
                txtHN.Text = objcurrentRegi.trn_patient.tpt_hn_no;
                txtEN.Text = objcurrentRegi.tpr_en_no;
                txtFullName.Text = objcurrentRegi.trn_patient.tpt_othername;
                txtDOB.Text = objcurrentRegi.trn_patient.tpt_dob_text; //objcurrentRegi.trn_patient.tpt_dob.Value.ToString("dd/MM/yyyy");
                txtGender.Text = (objcurrentRegi.trn_patient.tpt_gender == 'F') ? "Female" : "Male";
                txtNationality.Text = objcurrentRegi.trn_patient.tpt_nation_desc;
                txtAge.Text = Program.CalculateAge(objcurrentRegi.trn_patient.tpt_dob.Value, Program.GetServerDateTime());
                txtVisitDate.Text = (objcurrentRegi.tpr_arrive_date == null) ? "" : objcurrentRegi.tpr_arrive_date.Value.ToString("dd / MM / yyyy");
                txtVisitTime.Text = (objcurrentRegi.tpr_arrive_date == null) ? "" : objcurrentRegi.tpr_arrive_date.Value.ToString("HH:mm:ss");
                txtAllergy.Text = objcurrentRegi.trn_patient.tpt_allergy;
                PicProfile.Image = Program.byteArrayToImage(objcurrentRegi.trn_patient.tpt_image.ToArray());
                DDsite.SelectedValue = 0;
                DDStation.SelectedValue = 0;
                ShowReport(0, 0);
                currentRegis = objcurrentRegi;
            }

        }
        private void GridReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridReport.Rows.Count; i++)
            {
                GridReport.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        private void GridReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (GridReport.CurrentRow != null && GridReport.ReadOnly == false)
                {
                    bool ischeck = Convert1.ToBoolean(GridReport[1, e.RowIndex].Value);
                    if (ischeck == false)
                    {
                        GridReport[1, e.RowIndex].Value = true;
                    }
                    else
                    {
                        GridReport[1, e.RowIndex].Value = false;
                    }
                }
            }
        }

        private void btnPrintAllselect_Click(object sender, EventArgs e)
        {
            //previewRpt
            List<string> rptCode = new List<string>();
            for (int i = 0; i <= GridReport.Rows.Count - 1; i++)
            {
                if (Convert1.ToBoolean(GridReport[1, i].Value) == true)
                {
                    string ReportCode = Convert1.ToString(GridReport["ColReportCode", i].Value);
                    rptCode.Add(ReportCode);
                }
            }
            if (rptCode.Count > 0)
            {
                Report.frmPreviewReport frm = new Report.frmPreviewReport(currentRegis.tpr_id, rptCode);
                //Report.frmPreviewReport._propertyReport prop = new Report.frmPreviewReport._propertyReport();
                //prop.tpr_id = currentRegis.tpr_id;
                //prop.reportCode = rptCode;
                //frm.propertyReport = prop;
                frm.previewReport();
            }
            //if (rptCode.Count > 0) ClsReport.previewRpt(currentRegis.tpr_id, rptCode);
        }

        private void ch_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridReport.Rows.Count - 1; i++)
            {
                if (ch_selectAll.Checked)
                {
                    GridReport[1, i].Value = true;
                }
                else
                {
                    GridReport[1, i].Value = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

    }
}
