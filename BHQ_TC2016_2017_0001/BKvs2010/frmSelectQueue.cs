using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010
{
    public partial class frmSelectQueue : Form
    {
        public frmSelectQueue()
        {
            InitializeComponent();
        }
        public int Getvalue { get; set; }
        private void SelectQueue_Load(object sender, EventArgs e)
        {
            DDQueue.Items.Add(new ComboboxItem("1", "1"));
            DDQueue.Items.Add(new ComboboxItem("2", "2"));
            DDQueue.Items.Add(new ComboboxItem("3", "3"));
            DDQueue.Items.Add(new ComboboxItem("4", "4"));
            DDQueue.Items.Add(new ComboboxItem("5", "5"));
            DDQueue.SelectedIndex = 4;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            Getvalue =Convert.ToInt32(DDQueue.SelectedItem.ToString());
            this.DialogResult = DialogResult.OK;
        }

        private void frmSelectQueue_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void frmSelectQueue_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
