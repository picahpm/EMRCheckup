using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace CheckUpToDoList
{
    public class ListFile
    {
        public int _fileid { get; set; }
        public string _filename { get; set; }
        public Stream _filestream { get; set; }
        public string _pathtemp { get; set; }
        public string _pathreal { get; set; }
        public string _status { get; set; }
    }
    public partial class webfromtest : System.Web.UI.Page
    {
       private static List<ListFile> _objFile = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                _objFile = new List<ListFile>();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //add file
            if (FileUpload1.HasFile == true)
            {
                //save to temp 
                TempUpload objTemp = new TempUpload(FileUpload1);
                string pathtemp = objTemp.SaveFile();
                
                string pathreal = string.Format(Constant.pathserverUpload, FileUpload1.FileName);
                _objFile.Add(new ListFile() { _fileid = _objFile.Count + 1, _filename = FileUpload1.FileName,_filestream = FileUpload1.PostedFile.InputStream,_pathtemp = pathtemp,_pathreal =  pathreal,_status = "N"});

                Repeater1.DataSource = _objFile;
                Repeater1.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (_objFile.Count > 0)
            {
                foreach (ListFile item in _objFile)
                {
                    FileStream writeStream = new FileStream(item._pathreal, FileMode.Create, FileAccess.Write);
                    ReadWriteStream(item._filestream, writeStream);
                    //File.Copy(Server.MapPath(item._pathtemp), Server.MapPath(item._pathreal));
                }
            }
        }

        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = (int)readStream.Length;
            Byte[] buffer = new Byte[Length];
            int byteRead = readStream.Read(buffer, 0, Length);
            //write the require byte
            while (byteRead > 0)
            {
                writeStream.Write(buffer, 0, byteRead);
                byteRead = readStream.Read(buffer, 0, Length);
            }

            readStream.Close();
            writeStream.Close();
        }
    }
}