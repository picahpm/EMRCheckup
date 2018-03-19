using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class TempUpload
{
    private String _tempFolderPath = "Temp";
    private FileUpload _file;

    public TempUpload(FileUpload file)
    {
        this._file = file;
    }

    public TempUpload(FileUpload file, String tempFolderPath)
    {
        this._file = file;
        this._tempFolderPath = tempFolderPath;
    }
    
    public String SaveFile()
    {
        String tempUploadFolder = String.Format("{0}/{1}_{2}_{3}_{4}_{5}_{6}", this._tempFolderPath, HttpContext.Current.Session.SessionID, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);

        if (!Directory.Exists(HttpContext.Current.Server.MapPath(String.Format("~/{0}", tempUploadFolder))))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(String.Format("~/{0}", tempUploadFolder)));
        }

        String uploadedFilePath = Path.Combine(tempUploadFolder, this._file.FileName.Replace("+", "_"));
        this._file.SaveAs(HttpContext.Current.Server.MapPath(String.Format("~/{0}", uploadedFilePath)));

        RemoveOldFiles();

        return uploadedFilePath.Replace('\\','/');
    }

    private void RemoveOldFiles()
    {
        try
        {
            DirectoryInfo tempUpload = new DirectoryInfo(HttpContext.Current.Server.MapPath(String.Format("~/{0}", this._tempFolderPath)));
            foreach (DirectoryInfo dir in tempUpload.GetDirectories())
            {
                String[] nameParts = dir.Name.Split(new char[] { '_' });
                if (nameParts.Length == 6)
                {
                    DateTime dirDate = new DateTime(Convert.ToInt32(nameParts[1]), Convert.ToInt32(nameParts[2]), Convert.ToInt32(nameParts[3]), Convert.ToInt32(nameParts[4]), Convert.ToInt32(nameParts[5]), 0);
                     //Delete files which older than 3 hours
                    if (dirDate.AddHours(1) <= DateTime.Now)
                    {
                        Directory.Delete(dir.FullName, true);
                    }
                }
            }
        }
        catch (IOException) { }
    }
}