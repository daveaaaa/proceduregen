using System.Collections.Generic;
using System.Data;
using Business.Viewmodel;
using Data.DBA.Structure;

namespace Business
{
    public interface IDBAConnection
    {
        DataSet ExecStoredProcedure(string connectionString, string schema, string procedure, List<SPParam> spParams);
        List<ProcedureParameter> GetProcedureParameters(string connectionString, string schema, string procedure);
        bool IsConnectionValid(string connectionString);
        List<Procedure> ListStoreProcedures(string connectionString);
    }
}