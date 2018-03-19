using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
namespace CheckupBO
{
    public partial class frmViewTeeth : Form
    {
        public frmViewTeeth()
        {
            InitializeComponent();
        }
        InhCheckupDataContext db = new InhCheckupDataContext();
        public int TprID;
        private void frmReportTeeth_Load(object sender, EventArgs e)
        {
            this.SetBindingSource();
            this.LoadCheckListRecommendation();
            this.ShowCheckTeethDocResult();
        }
        //yee test 
        private void SetBindingSource()
        {
            var datateethbinding = (from t1 in db.trn_teeth_hdrs
                                    where t1.tpr_id == TprID
                                    select t1).FirstOrDefault();
            if (datateethbinding != null)
            {
                TeethbindingSource1.DataSource = (from t1 in db.trn_teeth_hdrs
                                                  where t1.tpr_id == TprID
                                                  select t1).FirstOrDefault();
                trn_teeth_hdr objtrnteethhdr = (trn_teeth_hdr)TeethbindingSource1.Current;
                Program.SetValueRadioGroup(GBGumfoState, objtrnteethhdr.tth_gum_of_state);
                Program.SetValueRadioGroup(GBbedTooth, objtrnteethhdr.tth_bad_tooth);
                Program.SetValueRadioGroup(GBThimbleofState, objtrnteethhdr.tth_thimble_of_state);

                trnteethdtlsBindingSource.DataSource = objtrnteethhdr.trn_teeth_dtls;

                ResetTeethbutton();
                
            }
            else
            {
                TeethbindingSource1.DataSource = db.trn_teeth_hdrs;
                TeethbindingSource1.AddNew();
            }
        }
        private void ShowCheckTeethDocResult()
        {
            try
            {
                var objdeltrnteethdocresult = (from t1 in db.trn_teeth_doc_results
                                               where t1.trn_teeth_hdr.tpr_id == TprID
                                               select t1);
                foreach (trn_teeth_doc_result objitem in objdeltrnteethdocresult)
                {
                    Int32 index = 0;
                    for (index = 0; index < chList.Items.Count; index++)
                    {
                        string valuecheckbox = Program.GetCheckedListBoxValue(index, chList).ToString();
                        if (valuecheckbox == objitem.mdr_id.ToString())
                        {
                            chList.SetItemChecked(index, true);
                            if (valuecheckbox == "45")
                            {
                                txtRemarkRecommendations.Text = objitem.mst_doc_result.mdr_tname;
                            } break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ChkList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string datax = Program.GetCheckedListBoxValue(e.Index, chList);
            if (datax == "45")
            {// 45= Other อื่นๆ
                bool ischeck = Convert.ToBoolean(e.CurrentValue);
                if (ischeck)
                {
                    txtRemarkRecommendations.Visible = false;
                }
                else
                {
                    txtRemarkRecommendations.Visible = true;
                }
            }
        }


        private void ResetTeethbutton()
        {
            foreach (Control item in PanelTeethBtn.Controls)
            {
                if (item is Button)
                {
                    Button btn = (Button)item;
                    if (btn.Tag != null)
                    {
                        string[] btnvalue = btn.Tag.ToString().Split(',');
                        IList<trn_teeth_dtl> objlistteethdtl = (IList<trn_teeth_dtl>)trnteethdtlsBindingSource.List;
                        var objselect = objlistteethdtl.Where(x => x.ttd_teeth_location == Convert.ToInt32(btnvalue[1]) && x.ttd_teeth_up.Trim() == btnvalue[0].Trim()).OrderBy(c => c.ttd_seq);
                        string strstatus = "";
                        foreach (trn_teeth_dtl dr in objselect)
                        {
                            if (strstatus == "")
                            {
                                strstatus += dr.mdr_code.Trim();
                            }
                            else
                            {
                                strstatus += "," + dr.mdr_code.Trim();
                            }
                        }
                        btn.Text = strstatus;
                        if (objselect.Count() > 0)
                        {
                            btn.BackColor = Color.Pink;
                        }
                        else
                        {
                            btn.BackColor = Control.DefaultBackColor;
                        }
                    }
                }
            }
        }
        private void ClearCheckTeethDocResult()
        {
            for (int index = 0; index < chList.Items.Count; index++)
            {
                chList.SetItemChecked(index, false);
            }
        }

        private void LoadCheckListRecommendation()
        {
            var objproblems = (from t1 in db.mst_doc_results
                               where t1.mst_doc_result_hdr.mrm_id == 23
                               && t1.mst_doc_result_hdr.mrh_code == "TH"
                               orderby t1.mdr_id
                               select new { ID = t1.mdr_id, Title = t1.mdr_tname + " (" + t1.mdr_ename + ")" }).ToList();
            chList.DataSource = objproblems;
            chList.DisplayMember = "Title";
            chList.ValueMember = "ID";
        }


        private void btntooth_Click(object sender, EventArgs e)
        {
            frmTeethProblems frm = new frmTeethProblems();
            Button btnclick = (Button)sender;
            frm.GetDocResult = btnclick.Text;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string resultdata = frm.GetDocResult;

                string[] ListResultdata = resultdata.Split(',');
                string[] btnvalue = btnclick.Tag.ToString().Split(',');
                trn_teeth_hdr currentteethhdr = (trn_teeth_hdr)TeethbindingSource1.Current;
                IList<trn_teeth_dtl> objlist =  (IList<trn_teeth_dtl>)trnteethdtlsBindingSource.List;

                
                //clear Old Select
                List<trn_teeth_dtl> objselect = objlist.Where(x =>
                                                x.ttd_teeth_up.Trim() == btnvalue[0].Trim()
                                                && x.ttd_teeth_location == Convert.ToInt32(btnvalue[1])
                                                ).ToList();

                foreach (trn_teeth_dtl item in objselect) 
                {
                        objlist.Remove(item);
                        if (item.ttd_id > 0)
                        {
                            db.trn_teeth_dtls.DeleteOnSubmit(item);
                        }
                        
                }
                //db.SubmitChanges();

                //Add Item select 
                if (resultdata != "")
                {
                    int countitem = 0;
                    string statusdata = "";
                    foreach (string itemrow in ListResultdata)
                    {
                        countitem = countitem + 1;
                        trn_teeth_dtl dtx = new trn_teeth_dtl();
                        dtx.tth_id = currentteethhdr.tth_id;
                        dtx.ttd_seq = countitem;
                        dtx.ttd_teeth_location = Convert.ToInt32(btnvalue[1]);
                        dtx.ttd_teeth_up = btnvalue[0];

                        string[] mdrData = itemrow.Split('|');
                        dtx.mdr_id = Convert.ToInt32(mdrData[0]);
                        dtx.mdr_code = mdrData[1];

                        dtx.ttd_create_by = Program.CurrentUser.mut_username;
                        dtx.ttd_create_date = Program.GetServerDateTime();
                        dtx.ttd_update_by = dtx.ttd_create_by;
                        dtx.ttd_update_date = dtx.ttd_create_date;
                        objlist.Add(dtx);
                        if (statusdata == "")
                        {
                            statusdata += mdrData[1];
                        }
                        else
                        {
                            statusdata += "," + mdrData[1];
                        }
                    }
                    btnclick.Text = statusdata;
                    btnclick.BackColor = Color.Pink;

                }
                else
                {
                    btnclick.Text = resultdata;
                    btnclick.BackColor = Control.DefaultBackColor;
                }
                //Count summary
                currentteethhdr.tth_bad_tooth_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "F").Count();
                currentteethhdr.tth_down_tooth_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "A").Count();
                currentteethhdr.tth_thimble_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "R").Count();
                currentteethhdr.tth_replace_tooth_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "E").Count();
                currentteethhdr.tth_thimble_out_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "C").Count();
                currentteethhdr.tth_put_tooth_total = objlist.Where(x => x.mdr_code != null && x.mdr_code.ToUpper() == "X").Count();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save('N');
        }

        private void Save(char strType)
        {
            Boolean saveIsCompleted = false;
            if (TeethbindingSource1.DataSource != null)
            {
                DateTime datenow = Program.GetServerDateTime();
                try
                {
                    trn_teeth_hdr objteethHdr = (trn_teeth_hdr)TeethbindingSource1.Current;
                    objteethHdr.tth_type = strType;
                    objteethHdr.tth_gum_of_state = Program.GetValueRadioTochar(GBGumfoState);
                    objteethHdr.tth_bad_tooth = Program.GetValueRadio(GBbedTooth);
                    objteethHdr.tth_thimble_of_state = Program.GetValueRadioTochar(GBThimbleofState);
                    objteethHdr.tth_create_by = Program.CurrentUser.mut_username;
                    objteethHdr.tth_create_date = datenow;
                    objteethHdr.tth_update_by = objteethHdr.tth_create_by;
                    objteethHdr.tth_update_date = objteethHdr.tth_create_date;
                    objteethHdr.tpr_id = TprID;

                    var objdeltrnteethdocresult = (from t1 in db.trn_teeth_doc_results
                                                   where t1.tth_id == objteethHdr.tth_id
                                                   select t1).ToList();
                    if (objdeltrnteethdocresult.Count != 0)
                    {
                        db.trn_teeth_doc_results.DeleteAllOnSubmit(objdeltrnteethdocresult);
                        db.SubmitChanges();
                    }

                    foreach (object itemChecked in chList.CheckedItems)
                    {

                        // เพิ่มรายการใหม่
                        trn_teeth_doc_result objnew = new trn_teeth_doc_result();
 
                        objnew.tdr_create_by = Program.CurrentUser.mut_username;
                        objnew.tdr_create_date = datenow;
                        objnew.tdr_update_date = datenow;
                        objnew.tdr_update_by = objnew.tdr_create_by;

                        objnew.tth_id = objteethHdr.tth_id;
                        objnew.trn_teeth_hdr = objteethHdr;
                        objnew.mdr_id = Convert.ToInt32(Program.GetCheckedListBoxValue(itemChecked, chList));
                        db.trn_teeth_doc_results.InsertOnSubmit(objnew);
                    }

                    trnteethdtlsBindingSource.EndEdit();
                    TeethbindingSource1.EndEdit();

                    db.SubmitChanges();
                    saveIsCompleted = true;
                }
                catch (Exception ex)
                {
                    Program.MessageError("=>Save Teeth==>Try Submitchanges() :" + ex.Message);
                }

                if (saveIsCompleted == true)
                {
                    lblMsg.Text = "Save data completed.";
                    timer1.Enabled = true;
                }
            }
            //return saveIsCompleted;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                if (i == 10)
                {
                    lblMsg.Text = "";
                    timer1.Enabled = false;
                }
            }
        }

        private void RDGumofstateP_CheckedChanged(object sender, EventArgs e)
        {
            if(RDGumofstateP.Checked == true)
            {
                txtGumforRemark.Enabled = true;
                Program.SetCheckedListBox(chList, "168");
                Program.SetCheckedListBox(chList, "169");
            }else
            {
                txtGumforRemark.Enabled = false;
                Program.SetCheckedListBox(chList, "168", false);
                Program.SetCheckedListBox(chList, "169", false);
            }
                
        }

        private void RdbadToothBM_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbadToothBM.Checked == true)
            {
                txtToothForRemark.Enabled = true;
            }
            else
            {
                txtToothForRemark.Enabled = false;
            }
        }

        private void RDthimbleT_CheckedChanged(object sender, EventArgs e)
        {
            if (RDthimbleT.Checked == true)
            {
                txtthimbleforRemark.Enabled = true;
            }
            else
            {
                txtthimbleforRemark.Enabled = false;
            }
        }

        private void RD1Strong_CheckedChanged(object sender, EventArgs e)
        {
            if (RD1Strong.Checked)
            {
                txtGumforRemark.Text = "";
                txtGumforRemark.Enabled = false;
            }
            else
            {
                txtGumforRemark.Enabled = true;
            }
        }
        //28 , 37
        private void RDGingivitis_CheckedChanged(object sender, EventArgs e)
        {
            if (RDGingivitis.Checked)
            {
                Program.SetCheckedListBox(chList, "28");
                Program.SetCheckedListBox(chList, "37");
            }
            else
            {
                Program.SetCheckedListBox(chList, "28", false);
                Program.SetCheckedListBox(chList, "37", false);
            }
        }

        //// 168 , 169
        //private void RDGumofstateP_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (RDGumofstateP.Checked)
        //    {
        //        Program.SetCheckedListBox(chList, "168");
        //        Program.SetCheckedListBox(chList, "169");
        //    }
        //    else
        //    {
        //        Program.SetCheckedListBox(chList, "168", false);
        //        Program.SetCheckedListBox(chList, "169", false);
        //    }
        //}

        private void RD2Noproblems_CheckedChanged(object sender, EventArgs e)
        {
            if (RD2Noproblems.Checked)
            {
                txtToothForRemark.Text = "";
                txtToothForRemark.Enabled = false;
            }
            else
            {
                txtToothForRemark.Enabled = true;
            }
        }


        private void RD3normal_CheckedChanged(object sender, EventArgs e)
        {
            if (RD3normal.Checked)
            {
                txtthimbleforRemark.Text = "";
                txtthimbleforRemark.Enabled = false;
            }
            else
            {
                txtthimbleforRemark.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt1BadToothTotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt1BadToothTotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "30");
                Program.SetCheckedListBox(chList, "39");
            }
            else if (Utility.GetInteger(txt2downtoothtotal.Text) == 0 && Utility.GetInteger(txt5thimbleOutTotal.Text) == 0)
            {
                Program.SetCheckedListBox(chList, "30", false);
                Program.SetCheckedListBox(chList, "39", false);
            }
        }

        private void txt2downtoothtotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt2downtoothtotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "30");
                Program.SetCheckedListBox(chList, "39");
            }
            else if (Utility.GetInteger(txt1BadToothTotal.Text) == 0 && Utility.GetInteger(txt5thimbleOutTotal.Text) == 0)
            {
                Program.SetCheckedListBox(chList, "30", false);
                Program.SetCheckedListBox(chList, "39", false);
            }
        }

        private void txt5thimbleOutTotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt5thimbleOutTotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "30");
                Program.SetCheckedListBox(chList, "39");
            }
            else if (Utility.GetInteger(txt2downtoothtotal.Text) == 0 && Utility.GetInteger(txt1BadToothTotal.Text) == 0)
            {
                Program.SetCheckedListBox(chList, "30", false);
                Program.SetCheckedListBox(chList, "39", false);
            }
        }

        private void txt4replacetoothtotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt4replacetoothtotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "170");
                Program.SetCheckedListBox(chList, "171");
            }
            else
            {
                Program.SetCheckedListBox(chList, "170", false);
                Program.SetCheckedListBox(chList, "171", false);
            }
        }

        private void txt6PutToothTotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt6PutToothTotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "33");
                Program.SetCheckedListBox(chList, "42");
            }
            else
            {
                Program.SetCheckedListBox(chList, "33", false);
                Program.SetCheckedListBox(chList, "42", false);
            }
        }

        private void txt3ThimbleTotal_TextChanged(object sender, EventArgs e)
        {
            if (Utility.GetInteger(txt3ThimbleTotal.Text) > 0)
            {
                Program.SetCheckedListBox(chList, "31");
                Program.SetCheckedListBox(chList, "40");
            }
            else
            {
                Program.SetCheckedListBox(chList, "31", false);
                Program.SetCheckedListBox(chList, "40", false);
            }
        }


    }
}
