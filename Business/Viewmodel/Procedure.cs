using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Viewmodel
{
    public class Procedure
    {
        private string _schema;
        private string _procedureName;
        public string Schema { get { return _schema; } }
        public string ProcedureName { get { return _procedureName; } }
        public string DisplayText { get { return $"{_schema}.{_procedureName}"; } }
         
        public Procedure(System.Data.DataRow drReq)
        {
            if (drReq.Table.Columns.Contains("schema")) { this._schema = (string)drReq["schema"]; }
            if (drReq.Table.Columns.Contains("procedure")) { this._procedureName = (string)drReq["procedure"]; }

        }

    }
}
