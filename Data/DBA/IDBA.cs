using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBA
{   
        public interface Functions
        {
            bool IsConnectionValid(string connectionString);
            System.Data.DataTable GetProcedureParameters(string connectionString, string schema, string procedure);
            System.Data.DataTable ListStoredProcedure(string connectionString); 
            System.Data.DataSet ExecStoredProcedure(string connectionString, string schema, string procedure, List<Structure.SPParam> sPParams);
        }
 
} 
