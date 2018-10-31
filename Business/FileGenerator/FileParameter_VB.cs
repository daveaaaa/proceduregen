using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.TemplateGenerator
{
   public class FileParameter_VB : IFileParameter
    {
        private string _columnName;
        private Type _parameterType;

        public FileParameter_VB(string columnName, Type parameterType)
        {
            _columnName = columnName;
            _parameterType = parameterType;
        }

        private string GetParameterTypeStr()
        {
            string typeStr = "";

            if (typeof(Int32) == _parameterType)
            {
                typeStr = "Int32";

            }
            else if (typeof(DateTime) == _parameterType)
            {
                typeStr = "DateTime";
            }
            else if (typeof(Boolean) == _parameterType)
            {
                typeStr = "Boolean";
            }
            else if (typeof(String) == _parameterType)
            {
                typeStr = "String";
            }
            else if (typeof(Decimal) == _parameterType)
            {
                typeStr = "Decimal";
            }
            else if (typeof(Double) == _parameterType)
            {
                typeStr = "Double";
            }

            return typeStr;
        }


        public string ConstructorField()
        {
            return $"If drReq.Tables.Columns.Contains(\"{ _columnName }\") Then Me._{_columnName } = SparkBase.Convert.To{GetParameterTypeStr() }(drReq.Item(\"{ _columnName}\"))";
        }

        public string PrivateField()
        {
            return $"private _{_columnName} as {GetParameterTypeStr()}";
        }

        public string PublicField()
        {
            return $"Public ReadOnly Property {_columnName} as { GetParameterTypeStr()} \n \t Get \n \t \t return _{_columnName} \n \t End Get \n End Property";
        }
    }
}
