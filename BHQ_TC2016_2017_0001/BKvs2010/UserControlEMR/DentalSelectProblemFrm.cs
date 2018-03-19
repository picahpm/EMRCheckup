using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class DentalSelectProblemFrm : Form
    {
        public DentalSelectProblemFrm()
        {
            InitializeComponent();
        }

        private class sourceMaster
        {
            public bool check { get; set; }
            public int val { get; set; }
            public string dis { get; set; }
            public string code { get; set; }
        }
        public class sourceProblem
        {
            public int? mdr_id { get; set; }
            public string mdr_code { get; set; }
        }
        public class selectProblem
        {
            public bool Selected { get; set; }
            public int mhs_id { get; set; }
            public List<sourceProblem> Problem { get; set; }
        }

        private BindingList<sourceMaster> bsProblem;

        private selectProblem _newProblem;
        private selectProblem newProblem
        {
            get
            {
                return _newProblem;
            }
            set
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mrm_id = cdc.mst_room_hdrs.Where(x => x.mrm_code == "TE" && x.mhs_id == 1).Select(x => x.mrm_id).FirstOrDefault();
                    BindingList<sourceMaster> source = new BindingList<sourceMaster>(
                                                       cdc.mst_doc_results.Where(x => x.mst_doc_result_hdr.mrm_id == mrm_id && 
                                                                                      x.mst_doc_result_hdr.mrh_code == "TA")
                                                       .Select(x => new sourceMaster
                                                       {
                                                           check = false,
                                                           val = x.mdr_id,
                                                           dis = x.mdr_tname + "\r\n(" + x.mdr_ename + ")",
                                                           code = x.mdr_code
                                                       }).ToList());
                    foreach (var old_problem in value.Problem)
                    {
                        sourceMaster res = source.Where(x => x.val == old_problem.mdr_id).FirstOrDefault();
                        res.check = true;
                    }
                    bsProblem = source;
                    dataGridView2.DataSource = bsProblem;
                }
                _newProblem = value;
            }
        }
        public selectProblem getProblem(selectProblem old_problem)
        {
            old_problem.Selected = false;
            newProblem = old_problem;
            this.ShowDialog();
            return newProblem;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _newProblem.Selected = true;
            _newProblem.Problem = bsProblem.Where(x => x.check == true)
                                  .Select(x => new sourceProblem
                                  {
                                      mdr_id = x.val,
                                      mdr_code = x.code
                                  }).ToList();
            this.Close();
        }
    }
}
