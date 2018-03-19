using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Models
{
    public class TabAddressModels
    {
        public int tpr_id { get; set; }
        public string patientType { get; set; }
        public string nameEN { get; set; }
        public string nameTH { get; set; }
        public string lang { get; set; }

        public List<Address> listAddress { get; set; }

        public class Address
        {
            public string type { get; set; }
            public string company { get; set; }
            public string empID { get; set; }
            public string contactPerson { get; set; }
            public string address { get; set; }
            public string phone { get; set; }
            public string fax { get; set; }
            public string legel { get; set; }
        }
    }

}
