using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigEMRCheckUp;

namespace DBCheckup
{
    public class InhCheckupDataContext : CheckupDataContext
    {
        public InhCheckupDataContext() :
            this(ConfigCls.PathwayConnString)
        {

        }

        public InhCheckupDataContext(string connection) :
            base(connection)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhCheckupDataContext(System.Data.IDbConnection connection) :
            base(connection)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhCheckupDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhCheckupDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }
    }
}
