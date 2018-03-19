using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace UserControlLibrary
{
    public class CheckBoxReadOnlyBinding : Label
    {
        public CheckBoxReadOnlyBinding()
        {
            Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            lbCheck = new Label();
            lbCheck.AutoSize = false;
            lbCheck.Paint += lbCheck_Paint;
            lbCheck.ForeColor = System.Drawing.Color.Green;
            Controls.Add(lbCheck);
            Checked = false;
        }
        private void lbCheck_Paint(object sender, PaintEventArgs e)
        {
            lbCheck.Height = Height;
            lbCheck.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                var TextSize = e.Graphics.MeasureString("B", Font);
                var CheckFont = new System.Drawing.Font("Wingdings 2", 12, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
                var CheckSize = e.Graphics.MeasureString("P", CheckFont);
                while (CheckSize.Height < TextSize.Height * 1.3)
                {
                    CheckFont = new System.Drawing.Font("Wingdings 2", CheckFont.Size + 1, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
                    CheckSize = e.Graphics.MeasureString("P", CheckFont);
                }
                lbCheck.Font = new System.Drawing.Font("Wingdings 2", CheckFont.Size, System.Drawing.FontStyle.Bold);
                lbCheck.Width = (int)CheckSize.Width;
                lbCheck.Location = new System.Drawing.Point();

                System.Drawing.StringFormat format = new System.Drawing.StringFormat();
                format.Alignment = System.Drawing.StringAlignment.Center;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                e.Graphics.DrawLine(System.Drawing.Pens.Green, new System.Drawing.Point(3, (int)CheckSize.Height - 6), new System.Drawing.Point((int)CheckSize.Width - 3, (int)CheckSize.Height - 6));
                //lbCheck.Top = Height - (int)ChkSize.Height;
                Padding = new Padding((int)CheckSize.Width, Padding.Top, Padding.Right, Padding.Bottom);
        }


        Label lbCheck;

        private bool _Checked;
        [Bindable(true)]
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                if (_Checked)
                {
                    lbCheck.Text = "P";
                }
                else
                {
                    lbCheck.Text = " ";
                }
            }
        }
    }
}
