using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBA.Structure
{
    public class SPParam_String : SPParam
    {
        private string _name;
        private bool _isOutput;

        public string name { get { return _name; } }

        public SPParam_String(string name, bool isOutput)
        {
            _name = name;
            _isOutput = isOutput;
        }

        public System.Data.SqlClient.SqlParameter ToSQLParameter()
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter();
            param.ParameterName = this.name;
            param.SqlDbType = System.Data.SqlDbType.VarChar;

            if (_isOutput)
            {
                param.Direction = System.Data.ParameterDirection.Output;

            }
            else
            {
                param.Direction = System.Data.ParameterDirection.Input;
            }

            param.Value = "";

            return param;
        }
    }
}
