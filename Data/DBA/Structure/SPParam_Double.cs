using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBA.Structure
{
    public class SPParam_Double : SPParam
    {

        private string _name;
        private byte _precision;
        private byte _scale; 
        private bool _isOutput;

        public string name { get { return _name; } }  

        public SPParam_Double(string name,  bool isOutput)
        {
            this._name = name; 
            _isOutput = isOutput;
        }

        public SqlParameter ToSQLParameter()
        {
            System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter();
            param.ParameterName = this.name;
            param.SqlDbType = System.Data.SqlDbType.Float; 
            if (_isOutput)
            {
                param.Direction = System.Data.ParameterDirection.Output;

            }
            else
            {
                param.Direction = System.Data.ParameterDirection.Input;
            }
            param.Value = 0;

            return param;
        }
    }
}