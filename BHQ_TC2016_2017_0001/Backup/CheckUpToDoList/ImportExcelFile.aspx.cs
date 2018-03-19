using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.OleDb;

namespace CheckUpToDoList
{
    public partial class ImportExcelFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                string FolderPath = "/tempUpload/";// ConfigurationManager.AppSettings["FolderPath"];

                string FilePath = Server.MapPath(FolderPath + FileName);

                FileUpload1.SaveAs(FilePath);

                ReadExcel(FilePath);
            }
        }
        private void ReadExcel(string filePath)
        {
            DataTable table = new DataTable();
            string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"", filePath);
            using (OleDbConnection dbConnection = new OleDbConnection(strConn))
            {
                using (OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", dbConnection)) //rename sheet if required!
                dbAdapter.Fill(table);

                int rows = table.Rows.Count;
                Boolean icount = false;
                foreach (DataRow dr in table.Rows)
                {
                    //1No.	2Thai	3Eng	4Address	5Tambon	6District	7Province	8Postcode	9Tel.	10Fax.	
                    //11E-mail 12Address	Name	13Tel.	14Fax.	15Email	16Form	17To	18Company Name(Eng)   19Address	20Tambon
                    //	21District	22Province	23Postcode	24Tel.	25Fax.	26E-mail Address	27Type	28Payment Type 29วงเงิน(บาท)	30Remark
                    //	31Billing Method	32Check-up Rate	33Main Program	34Options items as mentioned in quotation 	35วงเงิน(บาท)	36Options items as mentioned not in quotation 	37วงเงิน(บาท)	
                    //38Term of receiving Medicine	39Meal Coupon	40Family's welfare	41Name	42Doctor Cat.	43Name	44Remark	45Name	46Remark	47ฉบับจริง	
                    //48ไม่รับกลับ	49สำเนา	50เงื่อนไขการเข้ารับบริการ	51Name	52Tel.	53Fax.	54Email	55Remark

                    if (icount == true)
                    {
                        var dat1No = dr[0].ToString();	
                        var dat2Thai=dr[1].ToString();
                        var	dat3Eng=dr[2].ToString();	
                        var dat4Address=dr[3].ToString();
                        var dat5Tambon=dr[4].ToString();
                        var dat6District=dr[5].ToString();
                        var	dat7Province=dr[6].ToString();
                        var dat8Postcode=dr[8].ToString();
                        var dat9Tel=dr[8].ToString();
                        var dat10Fax = dr[9].ToString();
                        var dat11E_mail=dr[10].ToString();
                        var dat12AddressName=dr[11].ToString();
                        var dat13Tel=dr[12].ToString();
                        var dat14Fax=dr[13].ToString();
                        var dat15Email=dr[14].ToString();
                        var dat16Form=dr[15].ToString();
                        var dat17To=dr[16].ToString();
                        var dat18CompanyNameEng=dr[17].ToString();
                        var dat19Address=dr[18].ToString();
                        var dat20Tambon=dr[19].ToString();
                        var dat21District=dr[20].ToString();
                        var dat22Province=dr[21].ToString();
                        var dat23Postcode=dr[22].ToString();
                        var dat24Tel=dr[23].ToString();
                        var dat25Fax=dr[24].ToString();
                        var dat26EmailAddress=dr[25].ToString();
                        var dat27Type=dr[26].ToString();
                        var dat28PaymentType=dr[27].ToString();
                        var dat29Amount=dr[28].ToString();
                        var dat30Remark=dr[29].ToString();
                        var dat31BillingMethod=dr[30].ToString();
                        var dat32CheckupRate=dr[31].ToString();
                        var dat33MainProgram=dr[32].ToString();
                        var dat34OptionsItemsAsMentionedInQuotation=dr[33].ToString();
                        var dat35AmountInQuotation=dr[34].ToString();
                        var dat36OptionsItemsAsMentionedNotInQuotation=dr[35].ToString();
                        var dat37AmountNotInQuotation=dr[36].ToString();
                        var dat38TermOfReceivingMedicine=dr[37].ToString();
                        var dat39MealCoupon=dr[38].ToString();
                        var dat40Familyswelfare=dr[39].ToString();
                        var dat41Name=dr[40].ToString();
                        var dat42Doctor_Cat=dr[41].ToString();
                        var dat43Name=dr[42].ToString();
                        var dat44Remark=dr[43].ToString();
                        var dat45Name=dr[44].ToString();
                        var dat46Remark=dr[45].ToString();
                        var dat47AllRight=dr[46].ToString();
                        var dat48NotReturn=dr[47].ToString();
                        var dat49Copy=dr[48].ToString();
                        var dat50IfRecrive=dr[49].ToString();
                        var dat51Name=dr[50].ToString();
                        var dat52Tel=dr[51].ToString();
                        var dat53Fax=dr[52].ToString();
                        var dat54Email=dr[53].ToString();
                        var dat55Remark=dr[54].ToString();
                        
                    }
                    if (dr[0].ToString() == "No.")
                    {
                        icount = true;
                    }

                }
                //Response.Write(rows.ToString());
            }
        }

    }
}