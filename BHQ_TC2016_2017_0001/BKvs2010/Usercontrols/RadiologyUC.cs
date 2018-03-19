using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010.Usercontrols
{
    public partial class RadiologyUC : UserControl
    {
        private List<BtnRadiology> ButtonRadiology;
        int x = 78;
        int y = 3;
        int yCount = 1;

        public RadiologyUC()
        {
            InitializeComponent();
            ButtonRadiology = new List<BtnRadiology>();
        }

        public void AddButtonRadiology(BtnRadiology btn)
        {
            ButtonRadiology.Add(btn);
            btn.Location = new Point(x, y);
            if (yCount < 4)
            {
                y = y + 30;
                yCount = yCount + 1;
            }
            else
            {
                x = x + 30;
                y = 3;
                yCount = 1;
            }
            this.Controls.Add(btn);
        }
        public void ClearButtonRadiology()
        {
            this.Controls.Clear();
            ButtonRadiology = new List<BtnRadiology>();
            x = 78;
            y = 3;
            yCount = 1;
        }

        public class BtnRadiology : Button
        {
            public delegate void OnBtnRadiologyClick(object sender, EventArgs e);
            public event OnBtnRadiologyClick btnRadiologyClick;
            private void HandlerBtnRadiologyClick(object sender, EventArgs e)
            {
                // we'll explain this in a minute
                if (btnRadiologyClick != null)
                    btnRadiologyClick(sender, e);
            }

            ToolTip tltbtnRadiology;
            private string _tooltipText;
            public string tooltipText
            {
                get { return _tooltipText; }
                set
                {
                    tltbtnRadiology.SetToolTip(this, value);
                    _tooltipText = value;
                }
            }

            public BtnRadiology()
            {
                tltbtnRadiology = new ToolTip();
                this.Size = new Size { Width = 28, Height = 28 };
                this.Image = BKvs2010.Properties.Resources.add2;
                this.ImageAlign = ContentAlignment.MiddleCenter;
                this.Click += new EventHandler(BtnRadiology_Click);
            }

            private void BtnRadiology_Click(object sender, EventArgs e)
            {
                HandlerBtnRadiologyClick(sender, e);
            }

            private bool _StatusSaved = false;
            public bool StatusSaved
            {
                get { return _StatusSaved; }
                set
                {
                    if (value != _StatusSaved)
                    {
                        if (value)
                        {
                            this.Image = BKvs2010.Properties.Resources.correctGreen;
                        }
                        else
                        {
                            this.Image = BKvs2010.Properties.Resources.add2;
                        }
                        _StatusSaved = value;
                    }
                }
            }
        }
    }
}
