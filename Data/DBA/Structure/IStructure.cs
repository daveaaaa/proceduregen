using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBA.Structure
{
    public interface SPParam
    {
        string name { get;  } 
         
        System.Data.SqlClient.SqlParameter ToSQLParameter();
    }

}
