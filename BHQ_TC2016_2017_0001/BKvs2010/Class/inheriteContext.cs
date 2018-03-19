using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data.Common;

namespace BKvs2010.Class
{
    class inheriteContext : InhCheckupDataContext
    {
        public enum paramIndex
        {
            Site
        }
        InhCheckupDataContext cdc = new InhCheckupDataContext();
        public inheriteContext(paramIndex param)
            : base()
        {
            int mqt_id = 0;
            if (param == paramIndex.Site)
            {
                manage_queue_transaction pk_mqt = cdc.manage_queue_transactions.Where(x => x.mqt_id == Program.CurrentSite.mhs_id).FirstOrDefault();
                if (pk_mqt != null)
                {
                    mqt_id = pk_mqt.mqt_id;
                }
                else
                {
                    using (inheriteContext insContext = new inheriteContext())
                    {
                        manage_queue_transaction ins_mqt = new manage_queue_transaction();
                        ins_mqt.mqt_site_id = Program.CurrentSite.mhs_id;
                        ins_mqt.mqt_flag = true;
                        insContext.manage_queue_transactions.InsertOnSubmit(ins_mqt);
                        insContext.SubmitChanges();
                        mqt_id = ins_mqt.mqt_id;
                    }
                }
            }

            cdc.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            cdc.Connection.Open();
            DbTransaction trans = cdc.Connection.BeginTransaction();
            cdc.Transaction = trans;

            manage_queue_transaction mqt = cdc.manage_queue_transactions.Where(x => x.mqt_id == mqt_id).FirstOrDefault();
            mqt.mqt_tpr_id = 0;
            mqt.mqt_update_date = Program.GetServerDateTime();
            mqt.mqt_flag = false;
            mqt.mqt_user_name = "system";
            cdc.SubmitChanges();
        }

        public inheriteContext()
            : base()
        {
            cdc.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            cdc.Connection.Open();
            DbTransaction trans = cdc.Connection.BeginTransaction();
            cdc.Transaction = trans;

            manage_queue_transaction mqt = cdc.manage_queue_transactions.Where(x => x.mqt_id == 1).FirstOrDefault();
            mqt.mqt_tpr_id = 0;
            mqt.mqt_update_date = Program.GetServerDateTime();
            mqt.mqt_flag = false;
            mqt.mqt_user_name = "system";
            cdc.SubmitChanges();
        }

        protected override void Dispose(bool disposing)
        {
            cdc.Transaction.Rollback();
            cdc.Connection.Close();
            base.Dispose(disposing);
        }
    }
}
