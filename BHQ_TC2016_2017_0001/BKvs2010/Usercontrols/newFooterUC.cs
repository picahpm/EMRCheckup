using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class newFooterUC : UserControl
    {
        public newFooterUC()
        {
            InitializeComponent();
        }

        private int? mhs_id = null;

        string _title = "All Patient HPC Site";
        public string SetTitle
        {
            set
            {
                _title = value;
            }
        }
        private void UIFooter_Load(object sender, EventArgs e)
        {
            try
            {
                for (var i = 7; i < this.dataGridView1.ColumnCount; i++)
                {
                    ((DataGridViewImageColumn)this.dataGridView1.Columns[i]).DefaultCellStyle.NullValue = null;
                }
                GC.Collect();
                setCmbSite();
            }
            catch
            {

            }
        }

        public void LoadData()
        {
            int? condition_mhs_id = (int?)DDSite.SelectedValue;
            string condition = txtSearch.Text.Trim();
            LoadData(condition_mhs_id, condition);
        }
        public void LoadData(int? mhs_id, string condition)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    //var a = cdc.vw_pat_status
                    //                               .Where(x => x.tpr_arrive_date.Value.Date == dateNow.Date &&
                    //                                           (x.tpr_queue_no.Contains(condition) ||
                    //                                            x.name.ToLower().Contains(condition.ToLower()) ||
                    //                                            x.tpt_hn_no.ToLower().Replace("-", "").Contains(condition.ToLower()) ||
                    //                                            x.EN.ToLower().Replace("-", "").Contains(condition.ToLower()))).ToList();
                    List<structFooter> result = cdc.vw_pat_status
                                                   .Where(x => x.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                               (x.tpr_queue_no.Contains(condition) ||
                                                                x.name.ToLower().Contains(condition.ToLower()) ||
                                                                x.tpt_hn_no.ToLower().Replace("-", "").Contains(condition.ToLower()) ||
                                                                x.EN.ToLower().Replace("-", "").Contains(condition.ToLower())))
                                                   .Select(x => new structFooter
                                                   {
                                                       tprID = x.tpr_id,
                                                       mhs_id = x.mhs_id,
                                                       Dept = x.mhs_ename,
                                                       queue_no = x.tpr_queue_no,
                                                       hn_no = x.tpt_hn_no,
                                                       name = x.name,
                                                       RG = x.RG,
                                                       BM = x.BM,
                                                       SC = x.SC,
                                                       CB = x.CB,
                                                       PE = x.PE,
                                                       CD = x.CD,
                                                       XR = x.XR,
                                                       US1 = x.UU,
                                                       US2 = x.UL,
                                                       US3 = x.UB,
                                                       US4 = x.UW,
                                                       DM = x.DM,
                                                       BD = x.BD,
                                                       EN = x.EN,
                                                       EM = x.EM,
                                                       HS = x.HS,
                                                       EK = x.EK,
                                                       AB = x.AB,
                                                       ES = x.ES,
                                                       PT = x.PT,
                                                       TX = x.TX,
                                                       TE = x.TE,
                                                       UG = x.UG,
                                                       PF = x.PF,
                                                       CC = x.CC,
                                                       DC = x.DC,
                                                       PH = x.PH,
                                                       BK = x.BK,
                                                       FN = x.FN
                                                   }).ToList();
                    dataGridView1.DataSource = result;
                }
            }
            catch (Exception)
            {

            }
        }
        private void openForm(string frmName, int tpr_id)
        {
            var type = Type.GetType(frmName);
            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
            form.tpr_id = tpr_id;
            form.ShowDialog();
        }

        private class structCmbSite
        {
            public int? mhs_id { get; set; }
            public string name { get; set; }
        }
        private void setCmbSite()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<structCmbSite> result = cdc.mst_hpc_sites
                                                    .Where(x => x.mhs_status == 'A' &&
                                                                x.mhs_type == 'P')
                                                    .Select(x => new structCmbSite
                                                    {
                                                        mhs_id = x.mhs_id,
                                                        name = x.mhs_ename
                                                    }).ToList();
                    result.Insert(0, new structCmbSite { mhs_id = null, name = "Select All" });
                    DDSite.ValueMember = "mhs_id";
                    DDSite.DisplayMember = "name";
                    DDSite.DataSource = result.OrderBy(x => x.name == null ? 0 : x.mhs_id).ToList();
                    DDSite.SelectedValue = mhs_id;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "setCmbSite", ex, false);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex > 6)
            {
                Image img;
                if (e.Value == null)
                {
                    img = new Bitmap(16, 16);
                }
                else
                {
                    string status = e.Value.ToString();
                    switch (status)
                    {
                        case "NS": img = imageList1.Images[0]; break;
                        case "WK": img = imageList1.Images[1]; break;
                        case "ED": img = imageList1.Images[2]; break;
                        case "LR": img = imageList1.Images[3]; break;
                        case "CL": img = imageList1.Images[4]; break;
                        case "WT": img = imageList1.Images[6]; break;
                        default : img = new Bitmap(16, 16); break;
                    }
                }
                e.Value = img;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int? current_mhs_id = (int?)DDSite.SelectedValue;
            if (current_mhs_id == null)
            {
                lbdataPatientHPCSITE.Text = "1.6 All Patient HPC Site";
            }
            else
            {
                string siteName = DDSite.SelectedText;
                lbdataPatientHPCSITE.Text = "1.6 " + siteName;
            }
            LoadData();
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    if (Program.CurrentRoom != null)
                    {
                        mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == Program.CurrentRoom.mrd_id).FirstOrDefault();
                        if (mrd.mst_room_hdr.mrm_code == "CC" || mrd.mst_room_hdr.mrm_code == "CB")
                        {
                            return;
                        }
                    }
                }
            }
            catch
            {

            }

            if (e.RowIndex == -1) { return; }

            try
            {
                if (e.ColumnIndex > 6)
                {
                    DataGridView dgv = (DataGridView)sender;
                    structFooter data = (structFooter)dgv.Rows[e.RowIndex].DataBoundItem;
                    var val = TypeDescriptor.GetProperties(data)[dgv.Columns[e.ColumnIndex].DataPropertyName].GetValue(data);
                    if (val != null)
                    {
                        if (val.ToString() == "ED" || val.ToString() == "LR")
                        {
                            showfrm(dgv.Columns[e.ColumnIndex].DataPropertyName, data.tprID, data.mhs_id);
                        }
                    }
                }

            }
            catch (Exception)
            {

            }
            GC.Collect();
        }
        private static bool CompareImages(Bitmap image1, Bitmap image2)
        {
            if (image1.Width == image2.Width && image1.Height == image2.Height)
            {
                for (int i = 0; i < image1.Width; i++)
                {
                    for (int j = 0; j < image1.Height; j++)
                    {
                        if (image1.GetPixel(i, j) != image2.GetPixel(i, j))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void showfrm(string pagecode, int tprID, int mhs_id)
        {
            Program.FooterIsclick = false;

            InhCheckupDataContext dbc = new InhCheckupDataContext();
            mst_room_dtl currentmrd = new mst_room_dtl();
            var currentRoomdtl = (from t1 in dbc.mst_room_dtls
                                  where t1.mst_room_hdr.mrm_code == pagecode
                                  select t1).FirstOrDefault();
            if (currentRoomdtl != null)
            {
                currentmrd = Program.CurrentRoom;
                //Program.CurrentRoom = null;
                Program.CurrentRoom = currentRoomdtl;
            }
            switch (pagecode)
            {
                case "RG": 
                    frmScreeningPage frmRG = new frmScreeningPage();
                    frmRG.SetTprID = tprID;
                    frmRG.siteitem = mhs_id;
                    frmRG.WindowState = FormWindowState.Maximized;
                    frmRG.ShowDialog();
                    break;
                case "BM": frmBasicMeasurement frmBM = new frmBasicMeasurement();
                    frmBM.SetTprID = tprID;
                    frmBM.siteitem = mhs_id;
                    frmBM.ShowDialog();
                    break;
                case "AB":
                    openForm("BKvs2010.Forms.ABIFrm", tprID);
                    break;
                case "HS":
                    openForm("BKvs2010.Forms.HearingFrm", tprID);
                    break;
                case "EN":
                case "EM":
                    openForm("BKvs2010.Forms.EyesFrm", tprID);
                    break;
                case "SC": frmScreeningPage frmSC = new frmScreeningPage();
                    frmSC.SetTprID = tprID; frmSC.siteitem = mhs_id;
                    frmSC.ShowDialog();
                    break;
                case "PT": 
                    frmObstetrics frmPT = new frmObstetrics();
                    frmPT.SetTprID = tprID; frmPT.siteitem = mhs_id;
                    frmPT.WindowState = FormWindowState.Maximized;
                    frmPT.ShowDialog();
                    break;
                case "ES":
                    openForm("BKvs2010.Forms.ESTFrm", tprID);
                    break;
                case "EK":
                    openForm("BKvs2010.Forms.EKGFrm", tprID);
                    break;
                case "CD":
                    frmCarotid_2 frmCD = new frmCarotid_2();
                    frmCD.SetTprID = tprID;
                    frmCD.siteitem = mhs_id;
                    frmCD.ShowDialog(); break;
                case "TE":
                    openForm("BKvs2010.Forms.DentalFrm", tprID);
                    break;
                case "DM":
                    openForm("BKvs2010.Forms.MammogramFrm", tprID);
                    break;
                case "DC":
                    frmPE_OutQueue frmPEOut = new frmPE_OutQueue();
                    frmPEOut.SetTprID = tprID;
                    frmPEOut.WindowState = FormWindowState.Maximized;
                    frmPEOut.ShowDialog();
                        break;
                case "CC": frmCheckPointC frmCC = new frmCheckPointC(); frmCC.ShowDialog(); break;
                case "XR":
                    openForm("BKvs2010.Forms.ChestXrayFrm", tprID);
                    break;
                case "UG":
                    openForm("BKvs2010.Forms.UGIFrm", tprID);
                    break;
                case "PF":
                    openForm("BKvs2010.Forms.PFTFrm", tprID);
                    break;
                case "OB":
                    frmObstetrics frmobg = new frmObstetrics();
                    frmobg.SetTprID = tprID;
                    frmobg.siteitem = mhs_id;
                    frmobg.ShowDialog(); break;
                case "BD":
                    openForm("BKvs2010.Forms.BMDFrm", tprID);
                    break;
                case "PH":
                    frmPHM frmPH = new frmPHM();
                    frmPH.SetTprID = tprID;
                    frmPH.siteitem = mhs_id;
                    frmPH.ShowDialog(); 
                    break;
                case "CB": frmCheckpointB2 frmCB = new frmCheckpointB2(); frmCB.ShowDialog(); break;
                case "US":
                    openForm("BKvs2010.Forms.UltrasoundFrm", tprID);
                    break;
                case "BK": 
                    break;
            }

            Program.CurrentRoom = currentmrd;
            Program.FooterIsclick = true;
            GC.Collect();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            string tooltipName = "Name : ";
                            string tooltipDoctor = "Doctor Name : ";
                            string tooltipSite = "Current Site : ";
                            string tooltipRoom = "Current Room : ";
                            int tpr_id = Convert.ToInt32(dataGridView1["coltprid", e.RowIndex].Value);
                            trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            if (patient_regis != null)
                            {
                                tooltipName += patient_regis.trn_patient.tpt_othername;
                                if (!string.IsNullOrEmpty(patient_regis.tpr_pe_doc_code))
                                {
                                    mst_user_type user = new EmrClass.GetDataMasterCls().GetUser(patient_regis.tpr_pe_doc_code);
                                    if (user != null)
                                    {
                                        tooltipDoctor += user.mut_fullname;
                                    }
                                }
                                trn_patient_queue tps = patient_regis.trn_patient_queues.Where(x => x.tps_status == "NS" || x.tps_status == "WK").FirstOrDefault();
                                if (tps != null)
                                {
                                    mst_room_hdr mrm = new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)tps.mrm_id);
                                    if (mrm != null)
                                    {
                                        mst_hpc_site mhs = new EmrClass.GetDataMasterCls().GetMstHpcSite(mrm.mhs_id);
                                        if (mhs != null)
                                        {
                                            tooltipSite += mhs.mhs_ename;
                                        }
                                        tooltipSite += ", " + mrm.mrm_ename;
                                    }
                                    if (tps.mrd_id != null)
                                    {
                                        mst_room_dtl room_dtl = cdc.mst_room_dtls.Where(x => x.mrd_id == tps.mrd_id).FirstOrDefault();
                                        if (room_dtl != null)
                                        {
                                            tooltipRoom += room_dtl.mrd_ename;
                                        }
                                    }
                                }
                            }
                            dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = tooltipName + Environment.NewLine +
                                                                                   tooltipDoctor + Environment.NewLine +
                                                                                   tooltipSite + Environment.NewLine +
                                                                                   tooltipRoom;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("UIFooter", "dataGridView1_CellMouseMove", ex.Message, false);
            }
        }

        class structFooter
        {
            public int tprID { get; set; }
            public int mhs_id { get; set; }
            public string Dept { get; set; }
            public string queue_no { get; set; }
            public string hn_no { get; set; }
            public string name { get; set; }
            public string RG { get; set; }
            public string BM { get; set; }
            public string SC { get; set; }
            public string CB { get; set; }
            public string PE { get; set; }
            public string CD { get; set; }
            public string XR { get; set; }
            public string US1 { get; set; }
            public string US2 { get; set; }
            public string US3 { get; set; }
            public string US4 { get; set; }
            public string DM { get; set; }
            public string BD { get; set; }
            public string EN { get; set; }
            public string EM { get; set; }
            public string HS { get; set; }
            public string EK { get; set; }
            public string AB { get; set; }
            public string ES { get; set; }
            public string PT { get; set; }
            public string TX { get; set; }
            public string TE { get; set; }
            public string UG { get; set; }
            public string PF { get; set; }
            public string CC { get; set; }
            public string DC { get; set; }
            public string PH { get; set; }
            public string BK { get; set; }
            public string FN { get; set; }
        }
    }
}
