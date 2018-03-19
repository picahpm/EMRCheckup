using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class newPatientProfileUC : UserControl
    {
        public newPatientProfileUC()
        {
            InitializeComponent();
            foreach (Binding bindImage in pictureBox1.DataBindings)
            {
                if (bindImage.PropertyName == "Image")
                {
                    bindImage.Format += new ConvertEventHandler(bindImage_Format);
                    bindImage.Parse += new ConvertEventHandler(bindImage_Parse);
                }
            }
        }
        private void bindImage_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.GetType() == typeof(System.Byte[]))
                {
                    System.Byte[] val = (System.Byte[])e.Value;
                    MemoryStream ms = new MemoryStream(val);
                    e.Value = Image.FromStream(ms);
                }
            }
        }
        private void bindImage_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.GetType() == typeof(Image))
                {
                    Image val = (Image)e.Value;
                    MemoryStream ms = new MemoryStream();
                    val.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    e.Value = ms.ToArray();
                }
            }
        }

        private trn_patient _Patient;
        public trn_patient Patient
        {
            get
            {
                return _Patient;
            }
            set
            {
                if (value != _Patient)
                {
                    if (value != null)
                    {
                        value.PropertyChanged += new PropertyChangedEventHandler(Patient_PropertyChanged);
                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
                        {
                            Patient_PropertyChanged(value, new PropertyChangedEventArgs(prop.Name));
                        }
                        bsPatient.DataSource = value;
                        bsPatientRegis.MoveLast();
                        trn_patient_regi patientRegis = (trn_patient_regi)bsPatientRegis.Current;
                        lblsite.Text = patientRegis.mst_hpc_site.mhs_ename;
                        dataFullName.Text = value.tpt_othername;
                    }
                    _Patient = value;
                }
            }
        }
        private void Patient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
            switch (e.PropertyName)
            {
                case "tpt_gender":
                    char? gender = (char?)val;
                    if (gender == 'M')
                    {
                        dataGender.Text = "ชาย(Male)";
                    }
                    else if (gender == 'F')
                    {
                        dataGender.Text = "หญิง(Female)";
                    }
                    else
                    {
                        dataGender.Text = "";
                    }
                    break;
                case "tpt_dob":
                    DateTime? dob = (DateTime?)val;
                    DateTime? dateNow = Program.GetServerDateTime();
                    dataAge.Text = Program.CalculateAge((DateTime)dob, (DateTime)dateNow);
                    break;
                case "tpt_dob_text":
                    if (val == null)
                    {
                        dataDOB.Text = "";
                    }
                    else
                    {
                        dataDOB.Text = val.ToString();
                    }
                    break;
            }
        }

        public void Clear()
        {
            Patient = null;
        }
    }
}
