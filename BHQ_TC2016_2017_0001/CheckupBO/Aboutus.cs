using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CheckupBO
{
    partial class Aboutus : Form
    {
        public Aboutus()
        {
            InitializeComponent();
        }

        private void Aboutus_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult = DialogResult.OK;
        }
    }
}
