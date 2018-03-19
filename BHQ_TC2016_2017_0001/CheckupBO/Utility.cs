using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckupBO
{
    class Utility
    {
        public static Int32 GetInteger(object obj){
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
               
            }
        }
    }
   public class Convert1
    {
       public static bool ToBoolean(object obj)
       {
           try
           {
               return Convert.ToBoolean(obj);
           }
           catch (Exception)
           {
               return false;

           }
       }

       public static Int32 ToInt32(object obj)
       {
           try
           {
               return Convert.ToInt32(obj);
           }
           catch (Exception)
           {
               return 0;

           }
       }
       public static double ToDouble(object obj)
       {
           try
           {
               return Convert.ToDouble(obj);
           }
           catch (Exception)
           {
               return 0;

           }
       }
       public static string ToString(object obj)
       {
           if (obj == null)
           {
               return string.Empty;
           }
           else
           {
               return obj.ToString();
           }
       }
    }

   class DropdownData
   {
       public int? Code { get; set; }
       public string Name { get; set; }
   }
   class DropdownDataString
   {
       public string Code { get; set; }
       public string Name { get; set; }
   }
   class DropdownDataChar
   {
       public char? Code { get; set; }
       public string Name { get; set; }
   }
   public static class DataGridViewExtensions
   {
       public static void SetRuningNumber(this DataGridView DGV)
       {
           int indexrow = 1;
           DGV.Columns[0].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
           for (int i = 0; i < DGV.Rows.Count; i++)
           {
               DGV[0, i].Value = indexrow;

               indexrow = indexrow + 1;
           }
       }
       public static void SetRuningNumber(this DataGridView DGV, int Columnindex)
       {
           int indexrow = 1;
           DGV.Columns[0].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
           for (int i = 0; i < DGV.Rows.Count; i++)
           {
               DGV[Columnindex, i].Value = indexrow;
               indexrow = indexrow + 1;
           }
       }
       public static void SetRuningNumber(this DataGridView DGV, string ColumnName)
       {
           int indexrow = 1;
           DGV.Columns[0].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
           for (int i = 0; i < DGV.Rows.Count; i++)
           {
               DGV[ColumnName, i].Value = indexrow;
               indexrow = indexrow + 1;
           }
       }

       //public static string GetEquation(this DataGridViewColumn column)
       //{
       //    if (column != null && column.Tag != null)
       //    {
       //        return column.Tag as string;
       //    }
       //    return string.Empty;
       //}

       //public static void SetEquation(this DataGridViewColumn column, string equation)
       //{
       //    if (column != null)
       //    {
       //        column.Tag = equation;
       //    }
       //}

   }

}
