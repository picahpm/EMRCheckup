using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    [Designer(typeof(myControlDesigner))] 
    public partial class UltrasoundUC : UserControl
    {
        public UltrasoundUC()
        {
            InitializeComponent();
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    mstEventUS = contxt.mst_events
                                       .Where(x => new List<string> { "UU", "UB", "UL", "UW" }.Contains(x.mvt_code))
                                       .ToList();
                }
            }
            catch
            {

            }
        }

        private List<mst_event> mstEventUS;
        private BindingSource bsHistory;
        private BindingSource bsCurrent;
        private BindingSource bsHisEN;

        private int? _mrd_id;
        public int? mrd_id
        {
            get { return _mrd_id; }
            set
            {
                if (value != _mrd_id)
                {
                    if (value != null)
                    {
                        using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                        {
                            List<mst_event> room_event = contxt.mst_room_events
                                                               .Where(x => x.mst_room_hdr.mst_room_dtls.Any(y => y.mrd_id == value))
                                                               .Select(x => x.mst_event).ToList();
                        }
                    }
                    _mrd_id = value;
                }
            }
        }

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
                        List<MstEvents> evs = mstEventUS.Select(x => new MstEvents
                        {
                            mvt_code = x.mvt_code,
                            mvt_name = x.mvt_ename
                        }).ToList();

                        BsMstEvent = new BindingSource();
                        BsMstEvent.CurrentItemChanged += new EventHandler(BsMstEvent_CurrentItemChanged);
                        BsMstEvent.DataSource = evs;
                        DDUltrasoundType.DataSource = BsMstEvent;
                        DDUltrasoundType.DisplayMember = "mvt_name";

                        List<trn_ultrasound> PatientUltrasound = value.trn_ultrasounds.ToList();

                        List<historyEN> hisEn = value.trn_ultrasounds
                                                     .Where(x => x.tus_en_no != x.trn_patient_regi.tpr_en_no)
                                                     .GroupBy(x => x.tus_en_no)
                                                     .Select(x => x.OrderByDescending(y => y.tus_order_date).FirstOrDefault())
                                                     .OrderByDescending(x => x.tus_order_date)
                                                     .Select(x => new historyEN
                                                     {
                                                         en = x.tus_en_no,
                                                         orderDate = x.tus_order_date
                                                     }).ToList();
                        int inx = 1;
                        hisEn.ForEach(x => x.no = inx++);

                        bsHisEN = new BindingSource();
                        bsHisEN.CurrentItemChanged += new EventHandler(bsHisEN_CurrentItemChanged);
                        bsHisEN.DataSource = hisEn;
                        GridHistory.AutoGenerateColumns = false;
                        GridHistory.DataSource = bsHisEN;
                        GridHistory.Columns[0].DataPropertyName = "no";
                        GridHistory.Columns[1].DataPropertyName = "en";
                        GridHistory.Columns[2].DataPropertyName = "orderDate";
                        GridHistory.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
                        GridHistory.Columns[3].DataPropertyName = "orderDate";
                        GridHistory.Columns[3].DefaultCellStyle.Format = "HH:mm:ss";

                        List<result> resultHistoryUS = value.trn_ultrasounds
                                                            .Where(x => x.tus_en_no != x.trn_patient_regi.tpr_en_no)
                                                            .Select(x => new result
                                                            {
                                                                en = x.tus_en_no,
                                                                mvt_code = x.tus_ultra_type,
                                                                orderDate = x.tus_order_date,
                                                                resultDate = x.tus_result_date,
                                                                orderName = x.tus_order_name,
                                                                overseenBy = x.tus_overseen_by,
                                                                resultText = x.tus_result
                                                            }).ToList();
                        resultHistoryUS.Insert(0, new result());
                        bsHistory = new BindingSource();
                        bsHistory.DataSource = resultHistoryUS;

                        txtOldOrder.DataBindings.Clear();
                        txtOldOrderDate.DataBindings.Clear();
                        txtOldOrderTime.DataBindings.Clear();
                        txtOldResultDate.DataBindings.Clear();
                        txtOldResultTime.DataBindings.Clear();
                        txtOldOverSeenBy.DataBindings.Clear();
                        txtOldResult.DataBindings.Clear();

                        txtOldOrder.DataBindings.Add(new Binding("Text", bsHistory, "orderName", true));
                        txtOldOrderDate.DataBindings.Add(new Binding("Text", bsHistory, "orderDate", true) { FormatString = "dd/MM/yyyy" });
                        txtOldOrderTime.DataBindings.Add(new Binding("Text", bsHistory, "orderDate", true) { FormatString = "HH:mm:ss" });
                        txtOldResultDate.DataBindings.Add(new Binding("Text", bsHistory, "resultDate", true) { FormatString = "dd/MM/yyyy" });
                        txtOldResultTime.DataBindings.Add(new Binding("Text", bsHistory, "resultDate", true) { FormatString = "HH:mm:ss" });
                        txtOldOverSeenBy.DataBindings.Add(new Binding("Text", bsHistory, "overseenBy", true));
                        txtOldResult.DataBindings.Add(new Binding("Text", bsHistory, "resultText", true));

                        List<result> resultCurrentUS = value.trn_ultrasounds
                                                            .Where(x => x.tus_en_no == x.trn_patient_regi.tpr_en_no)
                                                            .Select(x => new result
                                                            {
                                                                en = x.tus_en_no,
                                                                mvt_code = x.tus_ultra_type,
                                                                orderDate = x.tus_order_date,
                                                                resultDate = x.tus_result_date,
                                                                orderName = x.tus_order_name,
                                                                overseenBy = x.tus_overseen_by,
                                                                resultText = x.tus_result
                                                            }).ToList();
                        resultCurrentUS.Insert(0, new result());
                        bsCurrent = new BindingSource();
                        bsCurrent.DataSource = resultCurrentUS;

                        txtOrder.DataBindings.Clear();
                        txtOrderDate.DataBindings.Clear();
                        txtOrderTime.DataBindings.Clear();
                        txtResultDate.DataBindings.Clear();
                        txtResultTime.DataBindings.Clear();
                        txtOverSeenBy.DataBindings.Clear();
                        txtResulttext.DataBindings.Clear();

                        txtOrder.DataBindings.Add(new Binding("Text", bsCurrent, "orderName", true));
                        txtOrderDate.DataBindings.Add(new Binding("Text", bsCurrent, "orderDate", true) { FormatString = "dd/MM/yyyy" });
                        txtOrderTime.DataBindings.Add(new Binding("Text", bsCurrent, "orderDate", true) { FormatString = "HH:mm:ss" });
                        txtResultDate.DataBindings.Add(new Binding("Text", bsCurrent, "resultDate", true) { FormatString = "dd/MM/yyyy" });
                        txtResultTime.DataBindings.Add(new Binding("Text", bsCurrent, "resultDate", true) { FormatString = "HH:mm:ss" });
                        txtOverSeenBy.DataBindings.Add(new Binding("Text", bsCurrent, "overseenBy", true));
                        txtResulttext.DataBindings.Add(new Binding("Text", bsCurrent, "resultText", true));

                        BsMstEvent_CurrentItemChanged(null, null);
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "patientRegis", ex, false);
                    }
                }
            }
        }
        private void bsHisEN_CurrentItemChanged(object sender, EventArgs e)
        {
            try
            {
                historyEN en = (historyEN)bsHisEN.Current;
                MstEvents ev = (MstEvents)BsMstEvent.Current;

                List<result> listHisResult = bsHistory.OfType<result>().ToList();
                result hi = listHisResult.Where(x => x.en == en.en && x.mvt_code == ev.mvt_code).FirstOrDefault();
                if (hi != null)
                {
                    bsHistory.Position = listHisResult.IndexOf(hi);
                }
                else
                {
                    bsHistory.Position = 0;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "BsResult_CurrentItemChanged(object sender, EventArgs e)", ex, false);
            }
        }
        private void BsMstEvent_CurrentItemChanged(object sender, EventArgs e)
        {
            try
            {
                historyEN en = (historyEN)bsHisEN.Current;
                MstEvents ev = (MstEvents)BsMstEvent.Current;

                List<result> listHisResult = bsHistory.OfType<result>().ToList();
                result hi = listHisResult.Where(x => x.en == en.en && x.mvt_code == ev.mvt_code).FirstOrDefault();
                if (hi != null)
                {
                    bsHistory.Position = listHisResult.IndexOf(hi);
                }
                else
                {
                    bsHistory.Position = 0;
                }

                List<result> listCurrent = bsCurrent.OfType<result>().ToList();
                result cr = listCurrent.Where(x => x.mvt_code == ev.mvt_code).FirstOrDefault();
                if (cr != null)
                {
                    bsCurrent.Position = listHisResult.IndexOf(cr);
                }
                else
                {
                    bsCurrent.Position = 0;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "BsResult_CurrentItemChanged(object sender, EventArgs e)", ex, false);
            }
        }

        private BindingSource BsMstEvent;

        public void Clear()
        {
            this.Enabled = false;
            if (bsHisEN != null) bsHisEN.DataSource = new List<historyEN>();
            if (bsCurrent != null) bsCurrent.DataSource = new List<result>();
            if (bsHistory != null) bsHistory.DataSource = new List<result>();
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {

        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel InnerPanel 
        {
            get { return panel1; }
            set { panel1 = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TableLayoutPanel InnerTableLayout
        {
            get { return tableLayoutPanel5; }
            set { tableLayoutPanel5 = value; }
        }

        class myControlDesigner : System.Windows.Forms.Design.ParentControlDesigner
        {
            public override void Initialize(IComponent component)
            {
                base.Initialize(component);
                UltrasoundUC myPanel = component as UltrasoundUC;
                this.EnableDesignMode(myPanel.InnerPanel, "InnerPanel"); 
                this.EnableDesignMode(myPanel.InnerTableLayout, "InnerTableLayout");
            }
        }

        private class historyEN
        {
            public int no { get; set; }
            public string en { get; set; }
            public DateTime? orderDate { get; set; }
        }

        public class result
        {
            public string en { get; set; }
            public string mvt_code { get; set; }
            public string orderName { get; set; }
            public DateTime? orderDate { get; set; }
            public DateTime? resultDate { get; set; }
            public string overseenBy { get; set; }
            public string resultText { get; set; }
        }
    }
}
