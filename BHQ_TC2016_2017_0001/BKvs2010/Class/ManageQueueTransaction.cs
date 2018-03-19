using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using DBCheckup;
using System.Data.Common;

namespace BKvs2010.Class
{
    class ManageQueueTransaction
    {
        public bool StartManageTransaction()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction tran = cdc.Connection.BeginTransaction();
                        cdc.Transaction = tran;
                        int? tpr_id = null;
                        if (Program.CurrentRegis != null)
                        {
                            tpr_id = Program.CurrentRegis.tpr_id;
                        }
                        string user_name = null;
                        if (Program.CurrentUser != null)
                        {
                            user_name = Program.CurrentUser.mut_username;
                        }
                        manage_queue_transaction mqt = cdc.manage_queue_transactions.Where(x => x.mqt_id == 1).FirstOrDefault();
                        if (mqt == null)
                        {
                            mqt = new manage_queue_transaction
                            {
                                mqt_flag = null
                            };
                            cdc.manage_queue_transactions.InsertOnSubmit(mqt);
                        }
                        mqt.mqt_tpr_id = tpr_id;
                        mqt.mqt_update_date = Program.GetServerDateTime();
                        mqt.mqt_user_name = user_name;
                        mqt.mqt_flag = true;
                        try
                        {
                            cdc.SubmitChanges();
                        }
                        catch (ChangeConflictException)
                        {
                            return StartManageTransaction();
                        }
                        catch
                        {
                            return false;
                        }
                        cdc.Transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        cdc.Transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        //public bool EndManageTransaction()
        //{

        //}
    }
}
