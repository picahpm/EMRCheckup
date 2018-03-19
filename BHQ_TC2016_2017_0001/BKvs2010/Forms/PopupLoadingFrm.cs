using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010.Forms
{
    public partial class PopupLoadingFrm : Form
    {
        public PopupLoadingFrm(OnProcess _OnProcess)
        {
            InitializeComponent();
            process = _OnProcess;
            this.ShowDialog();
        }

        public delegate void OnProcess(object sender, EventArgs e);
        public event OnProcess process;
        private void _process(EventArgs e)
        {
            // Make sure someone is listening to event
            if (process == null) return;
            process(this, e);
        }

        protected override void OnActivated(EventArgs e)
        {
            _process(new EventArgs());
            this.Close();
        }
    }
}
