using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBA.Structure
{
    public class SPParam_Bit : SPParam
    {
        private string _name; 
        private bool _isOutput;

        public string name { get { return _name; } } 

        public SPParam_Bit(string name, bool isOutput)
        {
            _name = name; 
            _isOutput = isOutput;
        }

        public SqlParameter ToSQLParameter()
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = this.name;
            param.SqlDbType = System.Data.SqlDbType.Bit;
            if (_isOutput)
            {
                param.Direction = System.Data.ParameterDirection.Output;

            }
            else
            {
                param.Direction = System.Data.ParameterDirection.Input;
            }
            param.Value = false;

            return param;
        }
    }
}
