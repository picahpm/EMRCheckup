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
    public partial class frmHealthSelect : Form
    {
        public frmHealthSelect()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public string strPage, strOld;
        public string strSelectedName;
        public int SelectedID,intoldid; 
        private void frmHealthSelect_Load(object sender, EventArgs e)
        {
            try
            {
                frmRegister frm = new frmRegister();
                strOld = Oldtext.oldtxt;
                intoldid = Oldtext.oldID;
                DateTime datenow = Program.GetServerDateTime();
                var objmst_health_checkup = (from t1 in dbc.mst_health_checkups
                                             where t1.mhc_status == 'A'
                                             && datenow >= t1.mhc_effective_date.Value
                                             && (t1.mhc_expire_date != null ? (datenow <= t1.mhc_expire_date.Value) : true)
                                             select new  
                                             { 
                                                 Code = t1.mhc_id,
                                                 Name = t1.mhc_ename 
                                             }).ToList();
                GvHealthSelect.DataSource = objmst_health_checkup;
                GvHealthSelect.Columns[0].Visible = false;
                GvHealthSelect.Columns[1].HeaderText = "Name";
                GvHealthSelect.Columns[1].Width = 250;
               // this.ControlBox = false; //control
            }
            catch
            {
                return;
            }
        }

        private void GvHealthSelect_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selectedItems = GvHealthSelect.CurrentRow;
                SelectedID = Convert1.ToInt32(GvHealthSelect.Rows[e.RowIndex].Cells[0].Value);
                strSelectedName = GvHealthSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.Close();
            }
            catch
            {
                return;
            }
        }


        private void frmHealthSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SelectedID == 0 && strSelectedName == null)
            {
                strSelectedName = strOld;
                SelectedID = intoldid;
            }
            this.Close();
        }

    }
}
