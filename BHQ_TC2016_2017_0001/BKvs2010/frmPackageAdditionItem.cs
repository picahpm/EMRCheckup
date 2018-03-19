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
    public partial class frmPackageAdditionItem : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        public frmPackageAdditionItem()
        {
            InitializeComponent();
            GetPackageAndAdditionItem();
        }

        private void GetPackageAndAdditionItem()
        {
            var packageAndAdditionItemQuery = (from trnPatientAddItem in dbc.trn_patient_add_items
                                              where trnPatientAddItem.tpr_id == Program.CurrentPatient_queue.tpr_id
                                              select new
                                              { 
                                                  tpai_id = trnPatientAddItem.tpai_id,
                                                  AddItem = trnPatientAddItem.tpai_add_item,
                                              }).ToList();
            gvPackageAdditionItem.DataSource = packageAndAdditionItemQuery;

        }

        private void gvPackageAdditionItem_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < gvPackageAdditionItem.Rows.Count; i++)
            {
                gvPackageAdditionItem["index", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void gvPackageAdditionItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex > -1)
            {
                int deleteItem = (int)gvPackageAdditionItem["tpai_id", e.RowIndex].Value;
                var deleteAdditionItemQuery = (from trnPatientAddItem in dbc.trn_patient_add_items
                                               where trnPatientAddItem.tpai_id == deleteItem
                                               select trnPatientAddItem).FirstOrDefault();
                dbc.trn_patient_add_items.DeleteOnSubmit(deleteAdditionItemQuery);
                dbc.SubmitChanges();
                GetPackageAndAdditionItem();
            }
        }
    }
}
