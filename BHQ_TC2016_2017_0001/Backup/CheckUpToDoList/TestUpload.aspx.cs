using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Web.Script.Services;
using System.IO;

namespace CheckUpToDoList
{

    public partial class TestUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _docfileList = new List<PalliativeDocFileEntity>();
            }
        }
        private CultureInfo _defaultCultureInfo = new CultureInfo("en-US");
        private const string _formatDate = "dd/MM/yyyy";
        private static List<PalliativeDocFileEntity> _docfileList = null;
        private static DateTime _transDate = DateTime.Now;
        private string pathserverUpload = "/Upload/test01/{0}";//{0}=fileUpload.FileName
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileUpload.HasFile)
                {
                    lbmsgbox.InnerHtml = "Please select file.";
                    return;
                };
                string fileName = fileUpload.FileName;
                string attachFile = string.Format(pathserverUpload, fileUpload.FileName);
                Stream stream = fileUpload.PostedFile.InputStream;
               // _docfileList.Add(new PalliativeDocFileEntity() { file_name = fileName, attach_file = attachFile, fileStream = stream, UpdateDate = DateTime.Now, UpdateBy ="111" });
                BindUpload(_docfileList);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        protected void btnDelAttacheFile_Click(object sender, EventArgs e)
        {
            if (_docfileList != null)
            {
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                _docfileList.RemoveAt(Grow.RowIndex);
                BindUpload(_docfileList);
            }
        }

        protected void BindUpload(List<PalliativeDocFileEntity> datalist){
            gnvAttachFile.DataSource = datalist;
            gnvAttachFile.DataBind();
        }
        private void SavePalliativeDocFile()
        {
            try
            {
                if (_docfileList != null)
                {
                    foreach (PalliativeDocFileEntity item in _docfileList)
                    {
                        if (item.fileStream != null)
                        {
                            //WriteFile(item.attach_file, item.fileStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void WriteFile(string filePath, Stream stream)
        {
            string serverpath = Server.MapPath(filePath);
            using (FileStream fileStream = System.IO.File.Create(serverpath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SavePalliativeDocFile();
        }
    }

}