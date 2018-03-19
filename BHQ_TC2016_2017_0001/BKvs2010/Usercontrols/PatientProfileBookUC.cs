using System;
using System.Windows.Forms;
using BKvs2010.Controllers;
using BKvs2010.Models;

namespace BKvs2010.Usercontrols
{
    public partial class PatientProfileBookUC : UserControl
    {
        public PatientProfileBookUC()
        {
            InitializeComponent();
            PatientInfoBS.DataSource = new PatientInfoModel();
        }

        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                _tpr_id = value;
                PatientInfoBS.DataSource = new PatientInfoControl().loadData(value);
            }
        }
        
        private void lbRetrieve_Click(object sender, EventArgs e)
        {
            PatientInfoBS.DataSource = new PatientInfoControl().RetrieveInfo(tpr_id);
        }
    }
}
