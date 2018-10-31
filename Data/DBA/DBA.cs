using Data.DBA.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Data.DBA
{
    public class DBA :  Functions
    {
        /// <summary>
        /// Is the connection string passed in valid?
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool IsConnectionValid(string connectionString)
        {
            bool isValid = false;

            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = connectionString;

            try
            {
                conn.Open();

                isValid = true;

                conn.Close();
            }
            catch (System.InvalidOperationException)
            {
                if (System.Data.ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                if (System.Data.ConnectionState.Open == conn.State)
                {
                    conn.Close();
                }
            }

            return isValid;
        }


        /// <summary>
        /// List all of the stored procedures 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public System.Data.DataTable ListStoredProcedure(string connectionString)
        {
            string commandSQL;
            commandSQL = " SELECT sys.schemas.name    [Schema]";
            commandSQL += "      ,sys.procedures.name [Procedure]";
            commandSQL += " FROM sys.procedures";
            commandSQL += " INNER JOIN sys.objects on procedures.object_id = objects.object_id";
            commandSQL += " INNER JOIN sys.schemas on objects.schema_id = sys.schemas.schema_id";


            System.Data.SqlClient.SqlConnection conn = getDBConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(commandSQL, conn);

            conn.Open();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            conn.Close();
            da.Dispose();

            return dt;
        }

        /// <summary>
        /// List all of the stored procedures 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public System.Data.DataTable GetProcedureParameters(string connectionString, string schema, string procedure)
        {
            string commandSQL = "";
            commandSQL += " SELECT parameter_id [Order],";
            commandSQL += "        'ParameterName' = [name],";
            commandSQL += "        'Type'   = type_name(user_type_id),";
            commandSQL += "        'Length' = max_length,";
            commandSQL += "        'Prec'   = case when type_name(system_type_id) = 'uniqueidentifier' then [precision] else OdbcPrec(system_type_id, max_length, precision) end,";
            commandSQL += "         has_default_value [HasDefaultValue],";
            commandSQL += "         is_output [IsOutput]";
            commandSQL += " FROM sys.parameters ";
            commandSQL += $" WHERE object_id = object_id('{schema}.{procedure}')";


            System.Data.SqlClient.SqlConnection conn = getDBConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(commandSQL, conn);

            conn.Open();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            conn.Close();
            da.Dispose();

            return dt;
        }


        /// <summary>
        /// Execute the stored procedure
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="sPParams"></param>
        /// <returns></returns>
        public DataSet ExecStoredProcedure(string connectionString, string schema, string procedure, List<SPParam> sPParams)
        { 
            System.Data.SqlClient.SqlConnection conn = getDBConnection(connectionString);
             
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand($"{schema}.{procedure}", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            foreach ( SPParam param in sPParams)
            {
                cmd.Parameters.Add(param.ToSQLParameter());
            }

            conn.Open(); 
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);

            // this will query your database and return the result to your datatable
            System.Data.DataSet ds = new System.Data.DataSet();
            da.Fill(ds);

            conn.Close();
            da.Dispose();

            return ds;
        }



        /// <summary>
        /// Get a database connection
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private System.Data.SqlClient.SqlConnection getDBConnection(string connectionString)
        {
            System.Data.SqlClient.SqlConnection conn = null;

            if (IsConnectionValid(connectionString))
            {
                conn = new System.Data.SqlClient.SqlConnection(connectionString);
            }

            return conn;
        }

      
    }
}
