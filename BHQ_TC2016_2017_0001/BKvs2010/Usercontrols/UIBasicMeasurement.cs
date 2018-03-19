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
    public partial class UIBasicMeasurement : UserControl
    {
        public UIBasicMeasurement()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            if (Program.CurrentRegis != null)
            {
                LoadData(Program.CurrentRegis.tpr_id);
            }
            else
            {
                lbdataBW.Text = "";
                lbdataBP.Text = "";
                lbdataT.Text = "";
                lbdataHT.Text = "";
                lbdataPR.Text = "";
                lbdataRR.Text = "";
            }
        }
        public void LoadData(int tpr_id)
        {
            try
            {
                if (tpr_id > 0)
                {
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        trn_basic_measure_dtl objbmdtl = (from t1 in dbc.trn_basic_measure_dtls 
                                                             where t1.trn_basic_measure_hdr.tpr_id==tpr_id
                                                             orderby t1.tbd_date descending
                                                             select t1).FirstOrDefault();
                        if (objbmdtl != null)
                        {
                            lbdataBW.Text = objbmdtl.tbd_weight;
                            lbdataBP.Text = objbmdtl.tbd_systolic + "/" + objbmdtl.tbd_diastolic;
                            lbdataT.Text = objbmdtl.tbd_temp;
                            lbdataHT.Text = objbmdtl.tbd_height;
                            lbdataPR.Text = objbmdtl.tbd_pulse;
                            lbdataRR.Text = objbmdtl.tbd_rr;
                        }
                        else
                        {
                             lbdataBW.Text = "";
                            lbdataBP.Text = "";
                            lbdataT.Text = "";
                            lbdataHT.Text = "";
                            lbdataPR.Text = "";
                            lbdataRR.Text = "";
                        }
                    }
                }
                else
                {
                    lbdataBW.Text = "";
                    lbdataBP.Text = "";
                    lbdataT.Text = "";
                    lbdataHT.Text = "";
                    lbdataPR.Text = "";
                    lbdataRR.Text = "";
                }
             }
            catch (Exception )
            {
            }
       }
    }
}
