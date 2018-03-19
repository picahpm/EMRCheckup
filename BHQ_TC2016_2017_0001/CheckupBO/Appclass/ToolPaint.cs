using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckupBO
{
    /// <summary>
    /// paint on form and picturebox
    /// created by Akkaradech.M
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
    }
}
