using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupWebService.Class
{
    public class GenerateChartCls
    {
        int width = 499;
        int height = 68;
        int bartop = 42;
        int barleft = 29;
        int toprange = 59;

        public Image Generate(double min, double max, double normal_min, double normal_max, double value, string ShowValue)
        {
            try
            {
                Bitmap bitmap = new Bitmap(width, height);
                Graphics flagGraphics = Graphics.FromImage(bitmap);
                Image bar = Properties.Resources.ChartBackGround;
                Image leftrange = Properties.Resources.normal_left;
                Image rightrange = Properties.Resources.normal_right;
                Image point = (value < normal_min || value > normal_max) ? Properties.Resources.PointRed : Properties.Resources.PointBlue;

                //Graphics gPoint = Graphics.FromImage(point);
                //gPoint.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                //SizeF size = TextRenderer.MeasureText(value.ToString(), new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Pixel));
                //TextRenderer.DrawText(gPoint, value.ToString(), new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Pixel), new Point((point.Width / 2) - (Convert.ToInt32(size.Width) / 2) + 1, 7), Color.White);

                Graphics gPoint = Graphics.FromImage(point);
                gPoint.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                SizeF size = gPoint.MeasureString(ShowValue, new Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Pixel));
                gPoint.DrawString(ShowValue, new Font("Microsoft Sans Serif", 10, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, new Point((point.Width / 2) - (Convert.ToInt32(size.Width) / 2), 8));

                double percent_value = GetPercent(value, min, max);
                double percent_range_left = GetPercent(normal_min, min, max);
                double percent_range_right = GetPercent(normal_max, min, max);

                LinearGradientBrush br = new LinearGradientBrush(new Rectangle(0, 0, bar.Width, bar.Height), Color.Black, Color.Black, 0, false);
                ColorBlend cb = new ColorBlend();
                float Point3 = (float)percent_range_left - 6 > 0 ? (float)percent_range_left - 6 : 0;
                float Point2 = Point3 / 2 > 0 ? Point3 / 2 : 0;
                float Point6 = (float)percent_range_right + 6 < 100 ? (float)percent_range_right + 6 : 100;
                float Point7 = (100 - Point6) / 2 > 0 ? Point6 + ((100 - Point6) / 2) : 100;
                cb.Positions = new[] { 0, Point2 / 100f, Point3 / 100f, (float)percent_range_left / 100f, (float)percent_range_right / 100f, Point6 / 100f, Point7 / 100f, 1 };
                cb.Colors = new[] { normal_min > min ? Color.Red : Color.FromArgb(26, 176, 26), normal_min > min ? Color.Orange : Color.FromArgb(26, 176, 26), normal_min > min ? Color.Yellow : Color.FromArgb(26, 176, 26), Color.FromArgb(26, 176, 26), Color.FromArgb(26, 176, 26), Color.Yellow, Color.Orange, Color.Red };
                br.InterpolationColors = cb;
                Graphics gBar = Graphics.FromImage(bar);
                gBar.FillRectangle(br, (new Rectangle(0, 0, bar.Width, bar.Height)));

                int point_value = GetPositon(percent_value > 100 ? 100 : percent_value < 0 ? 0 : percent_value, bar.Width, barleft);
                int point_range_min = GetPositon(percent_range_left, bar.Width, barleft);
                int point_range_max = GetPositon(percent_range_right, bar.Width, barleft) - rightrange.Width;
                int range_width = point_range_max - point_range_min - leftrange.Width;

                Image barrange = new Bitmap(range_width, leftrange.Height);
                Graphics range = Graphics.FromImage(barrange);

                for (int i = 0; i < range_width; i++)
                {
                    range.DrawImage(Properties.Resources.normal_mid, new Rectangle(new Point(i, 0), Properties.Resources.normal_mid.Size),
                    new Rectangle(new Point(), barrange.Size), GraphicsUnit.Pixel);
                }
                //range.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#38f538")), new Rectangle(Point.Empty, barrange.Size));

                flagGraphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), new Rectangle(Point.Empty, bitmap.Size));
                flagGraphics.DrawImage(bar, new Rectangle(new Point(barleft + 1, bartop), bar.Size),
                    new Rectangle(new Point(), bar.Size), GraphicsUnit.Pixel);
                flagGraphics.DrawImage(leftrange, new Rectangle(new Point(point_range_min, toprange), leftrange.Size),
                    new Rectangle(new Point(), leftrange.Size), GraphicsUnit.Pixel);
                flagGraphics.DrawImage(rightrange, new Rectangle(new Point(point_range_max, toprange), rightrange.Size),
                    new Rectangle(new Point(), rightrange.Size), GraphicsUnit.Pixel);
                flagGraphics.DrawImage(barrange, new Rectangle(new Point(point_range_min + leftrange.Width, toprange), barrange.Size),
                    new Rectangle(new Point(), barrange.Size), GraphicsUnit.Pixel);
                flagGraphics.DrawImage(point, new Rectangle(new Point(point_value - (point.Width / 2), 3), point.Size),
                    new Rectangle(new Point(), point.Size), GraphicsUnit.Pixel);
                return bitmap;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private double GetPercent(double value, double min, double max)
        {
            return ((value - min) / (max - min)) * 100;
        }

        private int GetPositon(double percent, int bar_width, int bar_left)
        {
            double w = 100 - percent;
            double pw = (bar_width * w) / 100;
            int pl = Convert.ToInt32(bar_width - pw + bar_left);
            return pl;
        }

    }
}