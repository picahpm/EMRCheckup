using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010.Usercontrols
{
    /// <summary>
    /// paint on form and picturebox
    /// :Akkaradech
    /// </summary>
    class ToolPaint
    {
        #region Shape&Draw
        public class Shape
        {
            public Point Location;          
            public float Width;             
            public Color Colour;            
            public int ShapeNumber;         
            //CONSTRUCTOR
            public Shape(Point L, float W, Color C, int S)
            {
                Location = L;               
                Width = W;                  
                Colour = C;                
                ShapeNumber = S;           
            }
        }

        public class Shapes
        {
            private List<Shape> _Shapes;  
            public Shapes()
            {
                _Shapes = new List<Shape>();
            }
            public int NumberOfShapes()
            {
                return _Shapes.Count;
            }
            public void NewShape(Point L, float W, Color C, int S)
            {
                _Shapes.Add(new Shape(L, W, C, S));
            }
            public Shape GetShape(int Index)
            {
                return _Shapes[Index];
            }
            public void RemoveShape(Point L, float threshold)
            {
                for (int i = 0; i < _Shapes.Count; i++)
                {
                    if ((Math.Abs(L.X - _Shapes[i].Location.X) < threshold) && (Math.Abs(L.Y - _Shapes[i].Location.Y) < threshold))
                    {
                        _Shapes.RemoveAt(i);
                        for (int n = i; n < _Shapes.Count; n++)
                        {
                            _Shapes[n].ShapeNumber += 1;
                        }
                        i -= 1;
                    }
                }
            }
        }
        #endregion

        #region CreateHeaderGridview
        public void PaintOnGridview(PaintEventArgs e,DataGridView gv)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Rectangle recA = gv.GetCellDisplayRectangle(0, -1, true);
            recA.Width = recA.Width - 2;
            recA.Height = recA.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA);
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA, format);

            Rectangle recA1 = gv.GetCellDisplayRectangle(1, -1, true);
            recA1.Width = recA1.Width - 2;
            recA1.Height = recA1.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA1);
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA1, format);

            Rectangle recA2 = gv.GetCellDisplayRectangle(2, -1, true);
            recA2.Width = recA2.Width - 2;
            recA2.Height = recA2.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA2);
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA2, format);


            Rectangle recB = gv.GetCellDisplayRectangle(3, -1, true);
            int widthH2 = gv.Columns[1].Width;
            recB.Width = (recB.Width + widthH2) - 2;
            recB.Height = recB.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recB);
            e.Graphics.DrawString("Create", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recB, format);

            Rectangle recC = gv.GetCellDisplayRectangle(5, -1, true);
            int widthH3 = gv.Columns[1].Width;
            recC.Width = (recC.Width + widthH3) - 2;
            recC.Height = recC.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recC);
            e.Graphics.DrawString("Update", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recC, format);

            Rectangle recD = gv.GetCellDisplayRectangle(7, -1, true);
            int widthH4 = gv.Columns[1].Width;
            recD.Width = recD.Width - 2;
            recD.Height = recD.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recD);
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recD, format);

            Rectangle recE = gv.GetCellDisplayRectangle(8, -1, true);
            int widthH5 = gv.Columns[1].Width;
            recE.Width = recE.Width - 2;
            recE.Height = recE.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recE);
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recE, format);
        }
        #endregion
    }
}
