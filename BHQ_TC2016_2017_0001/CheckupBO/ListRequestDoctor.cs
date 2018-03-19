using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CheckupBO
{
    public partial class ListRequestDoctor : Form
    {
        public ListRequestDoctor()
        {
            InitializeComponent();
        }

        public void insertCmbCondition()
        {

        }

        public void setSourceGridDetail()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var result = cdc.trn_patient_regis.Where(x => x.tpr_arrive_date.Value.Date == CurrentDate.Value.Date).ToList();
                            
            }
        }
    }
}
