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
    public partial class frmSelectDoctor : Form
    {
        public enum selectDocType
        {
            SelectedDoctor,
            NoRequestDoctor,
            Cancel
        }
        selectDocType type = selectDocType.Cancel;
        public frmSelectDoctor()
        {
            InitializeComponent();
            this.ShowDialog();
        }
        public frmSelectDoctor(string docName)
        {
            InitializeComponent();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var result = new EmrClass.FunctionDataCls().getSourceDoctor();
                    autoCompDoctor.DataSource = result;
                    autoCompDoctor.ValueMember = "val";
                    autoCompDoctor.DisplayMember = "dis";
                }
            }
            catch
            {

            }
            if (!string.IsNullOrEmpty(docName))
            {
                autoCompDoctor.Text = docName;
            }
            //this.ShowDialog();
        }

        public selectDocType getSelectType
        {
            get
            {
                return type;
            }
        }

        public string get_mut_username
        {
            get
            {
                if (autoCompDoctor.SelectedValue != null)
                {
                    return autoCompDoctor.SelectedValue.ToString();
                }
                return null;
            }
        }

        private void btnCancelReqDoc_Click(object sender, EventArgs e)
        {
            type = selectDocType.NoRequestDoctor;
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (autoCompDoctor.SelectedValue == null)
            {
                MessageBox.Show("Please Select Doctor.");
            }
            else 
            {
                type = selectDocType.SelectedDoctor;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // add by pang 09/06/2015
        public string LabelWarningText
        {
            get { return labWarning.Text; }
            set { labWarning.Text = value; }
        }

        public bool LabelWarningVisible
        {
            get { return labWarning.Visible; }
            set { labWarning.Visible = value; }
        }
        // end add 09/06/2015
    }
}
