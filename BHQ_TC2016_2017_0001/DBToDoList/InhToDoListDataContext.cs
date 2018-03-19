using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConfigEMRCheckUp;

namespace DBToDoList
{
    public class InhToDoListDataContext : ToDoListDataContext
    {
        public InhToDoListDataContext() :
            this(ConfigCls.TodolistConnString)
        {

        }

        public InhToDoListDataContext(string connection) :
            base(connection)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhToDoListDataContext(System.Data.IDbConnection connection) :
            base(connection)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhToDoListDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }

        public InhToDoListDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
            base(connection, mappingSource)
        {
            base.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            this.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
        }
    }
}
