using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DBA.Structure;

namespace Business
{
    public class DBAConnection : IDBAConnection
    {
        private Data.DBA.Functions _dba;

        public DBAConnection(Data.DBA.Functions dba)
        {
            _dba = dba;
        }

        /// <summary>
        /// Check the connection string is valid
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dba"></param>
        /// <returns></returns>
        public bool IsConnectionValid(string connectionString  )
        {
            return _dba.IsConnectionValid(connectionString);
        }

        /// <summary>
        /// List store procedures available 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dba"></param>
        /// <returns></returns>
        public List<Viewmodel.Procedure> ListStoreProcedures(string connectionString)
        {
            System.Data.DataTable dt = _dba.ListStoredProcedure(connectionString);

            List<Viewmodel.Procedure> results = new List<Viewmodel.Procedure>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                results.Add(new Viewmodel.Procedure(row));
            }

            return results;
        }

        public DataSet ExecStoredProcedure(string connectionString, string schema, string procedure, List<SPParam> spParams)
        {
            return this._dba.ExecStoredProcedure(connectionString, schema, procedure, spParams);
        }

        /// <summary>
        /// get the parameters for the procedure
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="dba"></param>
        /// <returns></returns>
        public List<Viewmodel.ProcedureParameter> GetProcedureParameters(string connectionString, string schema, string procedure)
        {
            System.Data.DataTable dt = _dba.GetProcedureParameters(connectionString, schema, procedure);


            List<Viewmodel.ProcedureParameter> results = new List<Viewmodel.ProcedureParameter>();

            foreach (System.Data.DataRow row in dt.Rows)
            {
                results.Add(new Viewmodel.ProcedureParameter(row));
            }

            return results;
        }

  
      
    }

}
