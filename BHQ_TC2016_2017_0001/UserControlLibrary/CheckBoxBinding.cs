using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace UserControlLibrary
{
    public class CheckBoxBinding : CheckBox
    {
        public CheckBoxBinding()
        {
            
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (this.Checked == true)
            {
                if (this._BindingType == TypeData.Char)
                {
                    SetBindingValue(_CheckedTrueValue == null ? null : (char?)Convert.ToChar(_CheckedTrueValue));
                }
                else if (this._BindingType == TypeData.String)
                {
                    SetBindingValue(_CheckedTrueValue == null ? null : Convert.ToString(CheckedTrueValue));
                }
                else if (this._BindingType == TypeData.Boolean)
                {
                    SetBindingValue(Convert.ToBoolean(CheckedTrueValue));                    
                }
            }
            else
            {                
                if (this._BindingType == TypeData.Char)
                {
                    SetBindingValue(_CheckedFalseValue == null ? null : (char?)Convert.ToChar(_CheckedFalseValue));
                }
                else if (this._BindingType == TypeData.String)
                {
                    SetBindingValue(_CheckedFalseValue == null ? null : Convert.ToString(_CheckedFalseValue));
                }
                else if (this._BindingType == TypeData.Boolean)
                {
                    SetBindingValue(Convert.ToBoolean(CheckedFalseValue));                    
                }
            }
            base.OnCheckedChanged(e);
        }

        public delegate void BindingValueChanged(object value);
        public BindingValueChanged _OnBindingValueChanged;
        private void OnBindingValueChanged(object value)
        {
            if (_OnBindingValueChanged != null)
            {
                _OnBindingValueChanged(value);
            }
        }

        public enum TypeData
        {
            Char,
            String,
            Boolean
        }

        private string _CheckedTrueValue;
        public string CheckedTrueValue 
        {
            get { return _CheckedTrueValue; }
            set
            {
                if (_BindingType != TypeData.Boolean)
                {
                    if (_BindingType == TypeData.Char)
                    {
                        if (value != null && Convert.ToString(value) != string.Empty)
                        {
                            string chTrue = Convert.ToString(value);
                            if (chTrue.Length == 1)
                            {
                                _CheckedTrueValue = chTrue;
                            }
                        }
                        else
                        {
                            _CheckedTrueValue = null;
                        }
                    }
                    else
                    {
                        _CheckedTrueValue = value;
                    }
                }
                else
                {
                    _CheckedTrueValue = "true";
                }
            }
        }
        private string _CheckedFalseValue;
        public string CheckedFalseValue 
        {
            get { return _CheckedFalseValue; }
            set
            {
                if (_BindingType != TypeData.Boolean)
                {
                    if (_BindingType == TypeData.Char)
                    {
                        if (value != null && Convert.ToString(value) != string.Empty)
                        {
                            string chFalse = Convert.ToString(value);
                            if (chFalse.Length == 1)
                            {
                                _CheckedFalseValue = chFalse;
                            }
                        }
                        else
                        {
                            _CheckedFalseValue = null;
                        }
                    }
                    else
                    {
                        _CheckedFalseValue = value;
                    }
                }
                else
                {
                    _CheckedFalseValue = "false";
                }
            }
        }
        
        private TypeData _BindingType = TypeData.Boolean;
        public TypeData BindingType 
        {   
            get { return _BindingType; } 
            set 
            { 
                _BindingType = value;

                if (_BindingType == TypeData.Boolean)
                {
                    _CheckedTrueValue = Convert.ToString(true);
                    _CheckedFalseValue = Convert.ToString(false);                    
                }
                else
                {
                    _CheckedTrueValue = null;
                    _CheckedFalseValue = null;
                }
            } 
        }

        private object _BindingValue;
        [Bindable(true)]
        public object BindingValue
        {
            get { return _BindingValue; }
            set
            {
                if (value != _BindingValue)
                {
                    if (_BindingType == TypeData.Char)
                    {

                        if (value != null && value.GetType() != typeof(DBNull))
                        {
                            char? valueChar = Convert.ToChar(value);
                            char? valueTrueChar = Convert.ToChar(CheckedTrueValue);
                            char? valueFalseChar = Convert.ToChar(CheckedFalseValue);

                            if (!(valueChar == valueTrueChar || valueChar == valueFalseChar))
                            {
                                //SetBindingValue(valueFalseChar);
                                this.Checked = false;
                            }
                            else
                            {
                                if (valueChar == valueTrueChar)
                                {
                                    this.Checked = true;
                                }
                                else
                                {
                                    this.Checked = false;
                                }
                                //SetBindingValue(value);
                            }
                        }
                        else
                        {
                            //SetBindingValue(Convert.ToChar(CheckedFalseValue));
                            this.Checked = false;
                        }

                    }
                    else if (_BindingType == TypeData.String)
                    {
                        if (value != null && value.GetType() != typeof(DBNull))
                        {
                            string valueString = Convert.ToString(value);
                            string valueTrueString = Convert.ToString(CheckedTrueValue);
                            string valueFalseString = Convert.ToString(CheckedFalseValue);

                            if (!(valueString == valueTrueString || valueString == valueFalseString))
                            {
                                //SetBindingValue(valueFalseString);
                                this.Checked = false;
                            }
                            else
                            {
                                if (valueString == valueTrueString)
                                {
                                    this.Checked = true;
                                }
                                else
                                {
                                    this.Checked = false;
                                }
                                //SetBindingValue(value);
                            }
                        }
                        else
                        {
                            //SetBindingValue(Convert.ToString(CheckedFalseValue));
                            this.Checked = false;
                        }
                    }
                    else if (_BindingType == TypeData.Boolean)
                    {
                        if (value != null && value.GetType() != typeof(DBNull))
                        {
                            bool? valueBool = Convert.ToBoolean(value);
                            bool? valueTrueBool = Convert.ToBoolean(CheckedTrueValue);
                            bool? valueFalseBool = Convert.ToBoolean(CheckedFalseValue);

                            if (!(valueBool == valueTrueBool || valueBool == valueFalseBool))
                            {
                                //SetBindingValue(valueFalseBool);
                                this.Checked = false;
                            }
                            else
                            {
                                if (valueBool == valueTrueBool)
                                {
                                    this.Checked = true;
                                }
                                else
                                {
                                    this.Checked = false;
                                }
                                //SetBindingValue(value);
                            }
                        }
                        else
                        {
                            //SetBindingValue(Convert.ToBoolean(CheckedFalseValue));
                            this.Checked = false;
                        }
                    }
                }
            }
        }

        private void SetBindingValue(object value)
        {
            _BindingValue = value;
            foreach (Binding binding in this.DataBindings)
            {
                binding.WriteValue();
            }
            OnBindingValueChanged(value);
        }
    }
}
