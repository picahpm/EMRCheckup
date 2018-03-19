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
    public partial class frmMappingItem : Form
    {
        private bool _StatusAdd = false;

        public frmMappingItem()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        
        private void frmMappingItem_Load(object sender, EventArgs e)
        {
            var objRaceGroup = (from t1 in dbc.mst_events
                                select new
                                {
                                    Value = t1.mvt_id,
                                    Text = t1.mvt_ename
                                }).ToList();
            DataGridViewComboBoxColumn dgvCmbForums = new DataGridViewComboBoxColumn();
            {
                // Hearder name of column
                dgvCmbForums.HeaderText = "Event Name";
                
                // Add items into Combobox
                dgvCmbForums.ValueMember = "Value";
                dgvCmbForums.DisplayMember = "Text";
                dgvCmbForums.DataPropertyName = "mvt_id";
                dgvCmbForums.DataSource = objRaceGroup;

                dgvCmbForums.Width = 150;
                dgvCmbForums.DisplayStyleForCurrentCellOnly = true;
                dgvCmbForums.FlatStyle = FlatStyle.Flat;
            }
            GridOrderplan.Columns.Add(dgvCmbForums);

            DD_EventName.ValueMember = "Value";
            DD_EventName.DisplayMember = "Text";
            DD_EventName.DataSource = objRaceGroup;

            LoadOrderPlan("");

        }
        private void LoadOrderPlan(string strSearch)
        {
            var orderlist = (from t1 in dbc.mst_order_plans select t1);
            if (strSearch != "")
            {
                orderlist = orderlist.Where(x => x.mop_od_item_code.Contains(strSearch)
                                            || x.mop_od_item_name.Contains(strSearch)
                                            || x.mop_item_row_id.Contains(strSearch));
            }
            PackageItembindingSource1.DataSource = orderlist;
        }

        private void btnSearchUsername_Click(object sender, EventArgs e)
        {
            LoadOrderPlan(txtUsername.Text);
        }
        private void GridOrderplan_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridOrderplan.SetRuningNumber();
        }
        private void btnClearUserName_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            SetbuttongSave();
            DateTime dtnow = Program.GetServerDateTime();
            PackageItembindingSource1.AddNew();
            DD_EventName.SelectedIndex = 0;
            mst_order_plan currentmut = (mst_order_plan)PackageItembindingSource1.Current;
            currentmut.mvt_id = Utility.GetInteger(DD_EventName.SelectedValue);
            currentmut.mop_update_by = Program.CurrentUser.mut_username;
            currentmut.mop_update_date = dtnow;
            currentmut.mop_create_by = Program.CurrentUser.mut_username;
            currentmut.mop_create_date = dtnow;
            _StatusAdd = true;
            btnEditUser_Click( new object(), new EventArgs());
        }
        private void SetbuttongSave()
        {
            GBAddEdit.Enabled = true;//เปิดให้แก้ไขได้
            btnAddNewUser.Enabled = false;
            btnEditUser.Enabled = false;
            btnSave.Enabled = true;
            if (_StatusAdd)
                _StatusAdd = false;
            else
                btnDel.Enabled = true;
            
        }
        private void Clearbutton()
        {
            GBAddEdit.Enabled = false;
            btnAddNewUser.Enabled = true;
            btnEditUser.Enabled = true;
            btnSave.Enabled = false;
            btnDel.Enabled = false;
        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            SetbuttongSave();
        }
        private void btnClaear_cancel_Click(object sender, EventArgs e)
        {
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            LoadOrderPlan("");
            Clearbutton();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Clearbutton();
            SaveData();
        }
        private void SaveData()
        {
            try
            {
                PackageItembindingSource1.EndEdit();
                dbc.SubmitChanges();
                lbmsgalert.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                lbmsgalert.Text = "Error:" + ex.Message;//Program.MessageError(ex.Message);
            }
        }

        private void PackageItembindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lbmsgalert.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveData();
                }
                else
                {
                    btnClaear_cancel_Click(null, null);
                    return;
                }
            }
            Clearbutton();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Clearbutton();
                    mst_order_plan currentmut = (mst_order_plan)PackageItembindingSource1.Current;
                    dbc.mst_order_plans.DeleteOnSubmit(currentmut);
                    dbc.SubmitChanges();
                    LoadOrderPlan("");
                    lbmsgalert.Text = "Delete data completed.";
                }

            }
            catch (Exception ex)
            {
                lbmsgalert.Text = "Error:" + ex.Message;//Program.MessageError(ex.Message);
            }
        }


    }
}
