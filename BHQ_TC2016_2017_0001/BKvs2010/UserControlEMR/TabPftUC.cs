using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class TabPftUC : UserControl
    {
        public TabPftUC()
        {
            InitializeComponent();
        }

        private bool _isDoctor = false;
        public bool isDoctor
        {
            get { return _isDoctor; }
            set { _isDoctor = value; }
        }

        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        pftMainUC1.isDoctorRoom = _isDoctor;
                        pftMainUC1.PatientRegis = value;
                        pftOccMedUC1.PatientRegis = value;

                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            pftMainUC1.Clear();
            pftOccMedUC1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                pftMainUC1.EndEdit();
                pftOccMedUC1.EndEdit();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }
    }
}


//private class linqDestination
//{
//    private List<DestinationControlCls> _DesControls = new List<DestinationControlCls>();
//    public List<DestinationControlCls> DesControls
//    {
//        get
//        {
//            return _DesControls;
//        }
//    }

//    private List<DestinationFieldCls> _DesFields = new List<DestinationFieldCls>();
//    public List<DestinationFieldCls> DesFields
//    {
//        get
//                {
//                    foreach (var result in _DesControls.Select(x => x.destinationField))
//                    {
//                        result.
//                    }
//                    return _DesControls.Select(x => x.destinationField.Select(y => new DestinationFieldCls
//                    {
//                        control_id = y.control_id,
//                        destinationControl = y.destinationControl,
//                        fieldName = y.fieldName
//                    }).ToList());
//                }
//    }

//    public class DestinationControlCls
//    {
//        private int _control_id;

//        private CheckBox _checkBox;

//        private EntitySet<DestinationFieldCls> _destinationField;

//        public DestinationControlCls()
//        {
//            this._destinationField = new EntitySet<DestinationFieldCls>(new Action<DestinationFieldCls>(this.attach_DestinationFieldCls), new Action<DestinationFieldCls>(this.detach_DestinationFieldCls));
//        }

//        private void attach_DestinationFieldCls(DestinationFieldCls entity)
//        {
//            entity.destinationControl = this;
//        }

//        private void detach_DestinationFieldCls(DestinationFieldCls entity)
//        {
//            entity.destinationControl = null;
//        }

//        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_control_id", AutoSync = System.Data.Linq.Mapping.AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true)]
//        public int control_id
//        {
//            get
//            {
//                return _control_id;
//            }
//            set
//            {
//                if (_control_id != value)
//                {
//                    _control_id = value;
//                }
//            }
//        }

//        public CheckBox checkBox
//        {
//            get
//            {
//                return _checkBox;
//            }
//            set
//            {
//                if (_checkBox != value)
//                {
//                    _checkBox = value;
//                }
//            }
//        }

//        public EntitySet<DestinationFieldCls> destinationField
//        {
//            get
//            {
//                return this._destinationField;
//            }
//            set
//            {
//                this._destinationField.Assign(value);
//            }
//        }
//    }

//    public class DestinationFieldCls
//    {
//        private EntityRef<DestinationControlCls> _destinationControl;

//        public DestinationControlCls destinationControl
//        {
//            get
//            {
//                return this._destinationControl.Entity;
//            }
//            set
//            {
//                DestinationControlCls previousValue = this._destinationControl.Entity;
//                if (((previousValue != value)
//                            || (this._destinationControl.HasLoadedOrAssignedValue == false)))
//                {
//                    if ((previousValue != null))
//                    {
//                        _destinationControl.Entity = null;
//                        previousValue.destinationField.Remove(this);
//                    }
//                    this._destinationControl.Entity = value;
//                    if ((value != null))
//                    {
//                        value.destinationField.Add(this);
//                        this._control_id = value.control_id;
//                    }
//                    else
//                    {
//                        this._control_id = default(int);
//                    }
//                }
//            }
//        }

//        private string _fieldName;

//        private int _control_id;

//        public DestinationFieldCls()
//        {
//            this._destinationControl = default(EntityRef<DestinationControlCls>);
//        }

//        public int control_id
//        {
//            get
//            {
//                return _control_id;
//            }
//            set
//            {
//                if ((this._control_id != value))
//                {
//                    if (this._destinationControl.HasLoadedOrAssignedValue)
//                    {
//                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
//                    }
//                    this._control_id = value;
//                }
//            }
//        }

//        public string fieldName
//        {
//            get
//            {
//                return this._fieldName;
//            }
//            set
//            {
//                if ((this._fieldName != value))
//                {
//                    this._fieldName = value;
//                }
//            }
//        }
//    }
//}
