using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace BKvs2010
{
    public static class Utility
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
        (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        public static Int32 GetInteger(object obj)
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
        public static float ToFloat(object obj)
        {
            try
            {
                return Convert.ToSingle(obj);
            }
            catch (Exception)
            {
                return 0;

            }
        }
        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        public static bool CompareImage(Image img1, Image img2)
        {
            try
            {
                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(256, 256);
                imgList.Images.Add(img1);
                imgList.Images.Add(img2);

                List<bool> iHash1 = Utility.GetHash((Bitmap)imgList.Images[0]);
                List<bool> iHash2 = Utility.GetHash((Bitmap)imgList.Images[1]);
                int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
                return equalElements > 240;
            }
            catch
            {
                return false;
            }
        }


        public static Image ResizeImage(Image img, Size size)
        {
            int sourceWidth = img.Width;
            int sourceHeight = img.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((size.Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((size.Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(img.HorizontalResolution,
                             img.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(img,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
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
        public static float ToFloat(object obj)
        {
            try
            {
                return Convert.ToSingle(obj);
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

    public static class Oldtext
    {
        public static string oldtxt;
        public static int oldID;
    }

    public class ObjCompany
    {
        public string code { get; set; }
        public string name { get; set; }
    }
    public class DoctorName
    {
        public string NameTH { get; set; }
        public string NameEN { get; set; }
    }
    public class DoctorProfile
    {
        public string SSUSR_Initials { get; set; } //mut_username
        public string CTPCP_Desc { get; set; } //mut_fullname
        public string CTPCP_Code { get; set; } //mut_carevider_code
        public string CTCPT_Desc { get; set; }
        public string DoctorName { get; set; }
        public string CTPCP_SMCNo { get; set; }
    }
    public enum Language
    {
        TH,
        EN
    }
    public class clsSourceAutoCompleteDoctor
    {
        public string val { get; set; }
        public string dis { get; set; }
    }
    public class married
    {
        public char? status { get; set; }
        public string desc { get; set; }
    }
    public enum StatusTransaction
    {
        Error,
        True,
        False,
        NoProcess,
        ChangeSite,
        SendCheckB,
        ReSendManualSite2
    }
    public class StationStatus
    {
        public int mvt_id { get; set; }
        public string status { get; set; }
    }
    public class MapOrderEvent
    {
        public int mop_id { get; set; }
        public int mvt_id { get; set; }
        public string status { get; set; }
        public bool use_pac { get; set; }
        public string pac_sheet { get; set; }
        public string patho { get; set; }
        public string tk_orderitem_row_id { get; set; }
        public DateTime? excute_date { get; set; }
    }
    public class EntityGetPTPackage
    {
        public string ARCOS_RowId { get; set; }
        public string ARCOS_Code { get; set; }
        public string ARCOS_Desc { get; set; }
        public string ARCIM_RowId { get; set; }
        public string ARCIM_Code { get; set; }
        public string ARCIM_Desc { get; set; }
        public string OEORI_LabTestSetRow { get; set; }
        public string OEORI_AccessionNumber { get; set; }
        public string ARCIM_Text1 { get; set; }
        public string OSTAT_Code { get; set; }
        public string OEORI_RowId { get; set; }
    }
    class CarotidCustomer
    {
        public int tpr_id { get; set; }
        public string HN { get; set; }
        public string FullName { get; set; }
        public DateTime ArriveDate { get; set; }
        public string DoctorName { get; set; }
        public int cusid { get; set; }
        public string EN { get; set; }
    }
    public class SortableBindingList<T> : BindingList<T>
    {
        private ArrayList unsortedItems;
        private bool isSortedValue;

        public SortableBindingList()
        {
        }

        public SortableBindingList(IList<T> list)
            : base(list)
        {
            //foreach (object o in list)
            //{
            //    this.Add((T)o);
            //}
        }

        protected override bool SupportsSearchingCore
        {
            get
            {
                return true;
            }
        }

        protected override int FindCore(PropertyDescriptor prop, object key)
        {
            PropertyInfo propInfo = typeof(T).GetProperty(prop.Name);
            T item;

            if (key != null)
            {
                for (int i = 0; i < Count; ++i)
                {
                    item = (T)Items[i];
                    if (propInfo.GetValue(item, null).Equals(key))
                        return i;
                }
            }
            return -1;
        }

        public int Find(string property, object key)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            PropertyDescriptor prop = properties.Find(property, true);

            if (prop == null)
                return -1;
            else
                return FindCore(prop, key);
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override bool IsSortedCore
        {
            get { return isSortedValue; }
        }

        ListSortDirection sortDirectionValue;
        PropertyDescriptor sortPropertyValue;

        protected override void ApplySortCore(PropertyDescriptor prop,
            ListSortDirection direction)
        {
            Type interfaceType = prop.PropertyType.GetInterface("IComparable");

            if (interfaceType == null && prop.PropertyType.IsValueType)
            {
                Type underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);

                if (underlyingType != null)
                {
                    interfaceType = underlyingType.GetInterface("IComparable");
                }
            }

            if (interfaceType != null)
            {
                sortPropertyValue = prop;
                sortDirectionValue = direction;

                IEnumerable<T> query = base.Items;
                if (direction == ListSortDirection.Ascending)
                {
                    query = query.OrderBy(i => prop.GetValue(i));
                }
                else
                {
                    query = query.OrderByDescending(i => prop.GetValue(i));
                }
                int newIndex = 0;
                foreach (object item in query)
                {
                    Items[newIndex] = (T)item;
                    newIndex++;
                }
                isSortedValue = true;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));

            }
            else
            {
                //throw new NotSupportedException("Cannot sort by " + prop.Name +
                //    ". This" + prop.PropertyType.ToString() +
                //    " does not implement IComparable");
            }
        }

        protected override void RemoveSortCore()
        {
            int position;
            object temp;

            if (unsortedItems != null)
            {
                for (int i = 0; i < unsortedItems.Count; )
                {
                    position = Find("LastName",
                        unsortedItems[i].GetType().
                        GetProperty("LastName").GetValue(unsortedItems[i], null));
                    if (position > 0 && position != i)
                    {
                        temp = this[i];
                        this[i] = this[position];
                        this[position] = (T)temp;
                        i++;
                    }
                    else if (position == i)
                        i++;
                    else
                        unsortedItems.RemoveAt(i);
                }
                isSortedValue = false;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        public void RemoveSort()
        {
            RemoveSortCore();
        }
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortPropertyValue; }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirectionValue; }
        }
    }

    public class ComboboxItem
    {
        public ComboboxItem(string txt, object value)
        {
            Text = txt;
            Value = value;
        }
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            if (Value == null) { return ""; }
            return Value.ToString();
        }
    }
    public static class DataGridViewExtensions
    {
        public static void SetRuningNumber(this DataGridView DGV)
        {
            int indexrow = 1;
            DGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                DGV[0, i].Value = indexrow;

                indexrow = indexrow + 1;
            }
        }
        public static void SetRuningNumber(this DataGridView DGV, int Columnindex)
        {
            int indexrow = 1;
            DGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                DGV[Columnindex, i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        public static void SetRuningNumber(this DataGridView DGV, string ColumnName)
        {
            int indexrow = 1;
            DGV.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                DGV[ColumnName, i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
    }
    public class DropdownData
    {
        public int? Code { get; set; }
        public string Name { get; set; }
    }
    public class structDropdownIDCls
    {
        public int? id { get; set; }
        public string name { get; set; }
    }
    public enum SendType
    {
        Normal,
        Skip,
        Pending
    }
    public static class HistoryData
    {
        public static string HN;
        public static string EN;
        public static string page;
        public static string doctorCode;
        public static string locationCode;
        public static string documentCode;
        public static string item;
        public static byte[] img;
        public static Bitmap bmpData;
        public static Image newImage;
        public static int Totalpage;
        public static ArrayList arrlist = new ArrayList();
        public static int count = 0;
        public static int filelength = 0;
        public static string savestatus;
        public static char showform; //'N' is not show form ViewDocscan || '!=N' is Show form ViewDocscan
    }
    public static class CurrentPage
    {
        public static int currentPage = 0;
    }

    public class DropdownDataString
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
