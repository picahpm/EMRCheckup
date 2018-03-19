using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace UserControlLibrary
{
    public class LabelAutoResizeFont : Label
    {
        public LabelAutoResizeFont()
        {
            this.AutoSize = false;
        }

        private float maxFontSize = 11.5F;
        public float MaxFontSize
        {
            get
            {
                return maxFontSize;
            }
            set
            {
                maxFontSize = value;
            }
        }

        private bool autoResizeFont = true;
        public bool AutoResizeFont
        {
            get
            {
                return autoResizeFont;
            }
            set
            {
                if (value != autoResizeFont)
                {
                    ResizeText();
                    autoResizeFont = value;
                }
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            ResizeText();
            base.OnTextChanged(e);
        }

        private void ResizeText()
        {
            try
            {
                this.Font = new Font(this.Font.FontFamily, MaxFontSize, this.Font.Style);
                while (this.Width < System.Windows.Forms.TextRenderer.MeasureText(this.Text, new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style)).Width)
                {
                    this.Font = new Font(this.Font.FontFamily, this.Font.Size - 0.5f, this.Font.Style);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
