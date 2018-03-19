using System.Data.Linq;
using System.ComponentModel;
using System;

namespace BKvs2010.UserControlEMR
{
    class HistoryDataCls
    {
        public HistoryDataCls()
        {

        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class HistoryResult : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _index;

        private string _en;

        private System.DateTime _visitDate;

        private EntitySet<HistoryResultDetail> _HistoryResultDetails;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnindexChanging(int value);
        partial void OnindexChanged();
        partial void OnenChanging(string value);
        partial void OnenChanged();
        partial void OnvisitDateChanging(System.DateTime value);
        partial void OnvisitDateChanged();
        #endregion

        public HistoryResult()
        {
            this._HistoryResultDetails = new EntitySet<HistoryResultDetail>(new Action<HistoryResultDetail>(this.attach_HistoryResultDetails), new Action<HistoryResultDetail>(this.detach_HistoryResultDetails));
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_index", IsDbGenerated = true)]
        public int index
        {
            get
            {
                return this._index;
            }
            set
            {
                if ((this._index != value))
                {
                    this.OnindexChanging(value);
                    this.SendPropertyChanging();
                    this._index = value;
                    this.SendPropertyChanged("index");
                    this.OnindexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_en", CanBeNull = false, IsPrimaryKey = true)]
        public string en
        {
            get
            {
                return this._en;
            }
            set
            {
                if ((this._en != value))
                {
                    this.OnenChanging(value);
                    this.SendPropertyChanging();
                    this._en = value;
                    this.SendPropertyChanged("en");
                    this.OnenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_visitDate")]
        public System.DateTime visitDate
        {
            get
            {
                return this._visitDate;
            }
            set
            {
                if ((this._visitDate != value))
                {
                    this.OnvisitDateChanging(value);
                    this.SendPropertyChanging();
                    this._visitDate = value;
                    this.SendPropertyChanged("visitDate");
                    this.OnvisitDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "HistoryResult_HistoryResultDetail", Storage = "_HistoryResultDetails", ThisKey = "en", OtherKey = "en")]
        public EntitySet<HistoryResultDetail> HistoryResultDetails
        {
            get
            {
                return this._HistoryResultDetails;
            }
            set
            {
                this._HistoryResultDetails.Assign(value);
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void attach_HistoryResultDetails(HistoryResultDetail entity)
        {
            this.SendPropertyChanging();
            entity.HistoryResult = this;
        }

        private void detach_HistoryResultDetails(HistoryResultDetail entity)
        {
            this.SendPropertyChanging();
            entity.HistoryResult = null;
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class MstEvents : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _index;

        private string _en;

        private string _mvt_code;

        private string _mvt_name;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnindexChanging(int value);
        partial void OnindexChanged();
        partial void OnenChanging(string value);
        partial void OnenChanged();
        partial void Onmvt_codeChanging(string value);
        partial void Onmvt_codeChanged();
        partial void Onmvt_nameChanging(string value);
        partial void Onmvt_nameChanged();
        #endregion

        public MstEvents()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_index", IsPrimaryKey = true)]
        public int index
        {
            get
            {
                return this._index;
            }
            set
            {
                if ((this._index != value))
                {
                    this.OnindexChanging(value);
                    this.SendPropertyChanging();
                    this._index = value;
                    this.SendPropertyChanged("index");
                    this.OnindexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_en", CanBeNull = false)]
        public string en
        {
            get
            {
                return this._en;
            }
            set
            {
                if ((this._en != value))
                {
                    this.OnenChanging(value);
                    this.SendPropertyChanging();
                    this._en = value;
                    this.SendPropertyChanged("en");
                    this.OnenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mvt_code", CanBeNull = false)]
        public string mvt_code
        {
            get
            {
                return this._mvt_code;
            }
            set
            {
                if ((this._mvt_code != value))
                {
                    this.Onmvt_codeChanging(value);
                    this.SendPropertyChanging();
                    this._mvt_code = value;
                    this.SendPropertyChanged("mvt_code");
                    this.Onmvt_codeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mvt_name", CanBeNull = false)]
        public string mvt_name
        {
            get
            {
                return this._mvt_name;
            }
            set
            {
                if ((this._mvt_name != value))
                {
                    this.Onmvt_nameChanging(value);
                    this.SendPropertyChanging();
                    this._mvt_name = value;
                    this.SendPropertyChanged("mvt_name");
                    this.Onmvt_nameChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class HistoryResultDetail : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _index;

        private string _en;

        private string _mvt_code;

        private string _ResultName;

        private System.Nullable<System.DateTime> _ResultDate;

        private System.Nullable<System.DateTime> _OrderDate;

        private string _OverseenBy;

        private string _ResultText;

        private EntityRef<HistoryResult> _HistoryResult;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnindexChanging(int value);
        partial void OnindexChanged();
        partial void OnenChanging(string value);
        partial void OnenChanged();
        partial void Onmvt_codeChanging(string value);
        partial void Onmvt_codeChanged();
        partial void OnResultNameChanging(string value);
        partial void OnResultNameChanged();
        partial void OnResultDateChanging(System.Nullable<System.DateTime> value);
        partial void OnResultDateChanged();
        partial void OnOrderDateChanging(System.Nullable<System.DateTime> value);
        partial void OnOrderDateChanged();
        partial void OnOverseenByChanging(string value);
        partial void OnOverseenByChanged();
        partial void OnResultTextChanging(string value);
        partial void OnResultTextChanged();
        #endregion

        public HistoryResultDetail()
        {
            this._HistoryResult = default(EntityRef<HistoryResult>);
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_index", IsPrimaryKey = true)]
        public int index
        {
            get
            {
                return this._index;
            }
            set
            {
                if ((this._index != value))
                {
                    this.OnindexChanging(value);
                    this.SendPropertyChanging();
                    this._index = value;
                    this.SendPropertyChanged("index");
                    this.OnindexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_en", CanBeNull = false)]
        public string en
        {
            get
            {
                return this._en;
            }
            set
            {
                if ((this._en != value))
                {
                    if (this._HistoryResult.HasLoadedOrAssignedValue)
                    {
                        throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
                    }
                    this.OnenChanging(value);
                    this.SendPropertyChanging();
                    this._en = value;
                    this.SendPropertyChanged("en");
                    this.OnenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_mvt_code", CanBeNull = false)]
        public string mvt_code
        {
            get
            {
                return this._mvt_code;
            }
            set
            {
                if ((this._mvt_code != value))
                {
                    this.Onmvt_codeChanging(value);
                    this.SendPropertyChanging();
                    this._mvt_code = value;
                    this.SendPropertyChanged("mvt_code");
                    this.Onmvt_codeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultName", CanBeNull = false)]
        public string ResultName
        {
            get
            {
                return this._ResultName;
            }
            set
            {
                if ((this._ResultName != value))
                {
                    this.OnResultNameChanging(value);
                    this.SendPropertyChanging();
                    this._ResultName = value;
                    this.SendPropertyChanged("ResultName");
                    this.OnResultNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultDate")]
        public System.Nullable<System.DateTime> ResultDate
        {
            get
            {
                return this._ResultDate;
            }
            set
            {
                if ((this._ResultDate != value))
                {
                    this.OnResultDateChanging(value);
                    this.SendPropertyChanging();
                    this._ResultDate = value;
                    this.SendPropertyChanged("ResultDate");
                    this.OnResultDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OrderDate")]
        public System.Nullable<System.DateTime> OrderDate
        {
            get
            {
                return this._OrderDate;
            }
            set
            {
                if ((this._OrderDate != value))
                {
                    this.OnOrderDateChanging(value);
                    this.SendPropertyChanging();
                    this._OrderDate = value;
                    this.SendPropertyChanged("OrderDate");
                    this.OnOrderDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OverseenBy", CanBeNull = false)]
        public string OverseenBy
        {
            get
            {
                return this._OverseenBy;
            }
            set
            {
                if ((this._OverseenBy != value))
                {
                    this.OnOverseenByChanging(value);
                    this.SendPropertyChanging();
                    this._OverseenBy = value;
                    this.SendPropertyChanged("OverseenBy");
                    this.OnOverseenByChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultText", CanBeNull = false)]
        public string ResultText
        {
            get
            {
                return this._ResultText;
            }
            set
            {
                if ((this._ResultText != value))
                {
                    this.OnResultTextChanging(value);
                    this.SendPropertyChanging();
                    this._ResultText = value;
                    this.SendPropertyChanged("ResultText");
                    this.OnResultTextChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.AssociationAttribute(Name = "HistoryResult_HistoryResultDetail", Storage = "_HistoryResult", ThisKey = "en", OtherKey = "en", IsForeignKey = true)]
        public HistoryResult HistoryResult
        {
            get
            {
                return this._HistoryResult.Entity;
            }
            set
            {
                HistoryResult previousValue = this._HistoryResult.Entity;
                if (((previousValue != value)
                            || (this._HistoryResult.HasLoadedOrAssignedValue == false)))
                {
                    this.SendPropertyChanging();
                    if ((previousValue != null))
                    {
                        this._HistoryResult.Entity = null;
                        previousValue.HistoryResultDetails.Remove(this);
                    }
                    this._HistoryResult.Entity = value;
                    if ((value != null))
                    {
                        value.HistoryResultDetails.Add(this);
                        this._en = value.en;
                    }
                    else
                    {
                        this._en = default(string);
                    }
                    this.SendPropertyChanged("HistoryResult");
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [global::System.Data.Linq.Mapping.TableAttribute(Name = "")]
    public partial class CurrentResultDetail : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _index;

        private string _mvt_code;

        private string _ResultName;

        private System.Nullable<System.DateTime> _ResultDate;

        private System.Nullable<System.DateTime> _OrderDate;

        private string _OverseenBy;

        private string _ResultText;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnindexChanging(int value);
        partial void OnindexChanged();
        partial void Onmvt_codeChanging(string value);
        partial void Onmvt_codeChanged();
        partial void OnResultNameChanging(string value);
        partial void OnResultNameChanged();
        partial void OnResultDateChanging(System.Nullable<System.DateTime> value);
        partial void OnResultDateChanged();
        partial void OnOrderDateChanging(System.Nullable<System.DateTime> value);
        partial void OnOrderDateChanged();
        partial void OnOverseenByChanging(string value);
        partial void OnOverseenByChanged();
        partial void OnResultTextChanging(string value);
        partial void OnResultTextChanged();
        #endregion

        public CurrentResultDetail()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_index", IsPrimaryKey = true)]
        public int index
        {
            get
            {
                return this._index;
            }
            set
            {
                if ((this._index != value))
                {
                    this.OnindexChanging(value);
                    this.SendPropertyChanging();
                    this._index = value;
                    this.SendPropertyChanged("index");
                    this.OnindexChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = "", Storage = "_mvt_code", CanBeNull = false)]
        public string mvt_code
        {
            get
            {
                return this._mvt_code;
            }
            set
            {
                if ((this._mvt_code != value))
                {
                    this.Onmvt_codeChanging(value);
                    this.SendPropertyChanging();
                    this._mvt_code = value;
                    this.SendPropertyChanged("mvt_code");
                    this.Onmvt_codeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultName", CanBeNull = false)]
        public string ResultName
        {
            get
            {
                return this._ResultName;
            }
            set
            {
                if ((this._ResultName != value))
                {
                    this.OnResultNameChanging(value);
                    this.SendPropertyChanging();
                    this._ResultName = value;
                    this.SendPropertyChanged("ResultName");
                    this.OnResultNameChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultDate")]
        public System.Nullable<System.DateTime> ResultDate
        {
            get
            {
                return this._ResultDate;
            }
            set
            {
                if ((this._ResultDate != value))
                {
                    this.OnResultDateChanging(value);
                    this.SendPropertyChanging();
                    this._ResultDate = value;
                    this.SendPropertyChanged("ResultDate");
                    this.OnResultDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OrderDate")]
        public System.Nullable<System.DateTime> OrderDate
        {
            get
            {
                return this._OrderDate;
            }
            set
            {
                if ((this._OrderDate != value))
                {
                    this.OnOrderDateChanging(value);
                    this.SendPropertyChanging();
                    this._OrderDate = value;
                    this.SendPropertyChanged("OrderDate");
                    this.OnOrderDateChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_OverseenBy", CanBeNull = false)]
        public string OverseenBy
        {
            get
            {
                return this._OverseenBy;
            }
            set
            {
                if ((this._OverseenBy != value))
                {
                    this.OnOverseenByChanging(value);
                    this.SendPropertyChanging();
                    this._OverseenBy = value;
                    this.SendPropertyChanged("OverseenBy");
                    this.OnOverseenByChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ResultText", CanBeNull = false)]
        public string ResultText
        {
            get
            {
                return this._ResultText;
            }
            set
            {
                if ((this._ResultText != value))
                {
                    this.OnResultTextChanging(value);
                    this.SendPropertyChanging();
                    this._ResultText = value;
                    this.SendPropertyChanged("ResultText");
                    this.OnResultTextChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
