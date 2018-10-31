using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.TemplateGenerator
{
    public class FileParameter_CS : IFileParameter
    {
        private string _columnName;
        private Type _parameterType;

        public FileParameter_CS(string columnName, Type parameterType)
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

            } else if (typeof(DateTime) == _parameterType)
            {
                typeStr = "DateTime";
            } else if (typeof(Boolean) == _parameterType)
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
            string str = $"if (drReq.Table.Columns.Contains(\"{ _columnName }\")) ";
            str += "{ this._" + _columnName + " = SparkBase.Convert.To" + GetParameterTypeStr() + "(drReq.Item[\"" + _columnName + "\"]); }";

            return str;
        }

        public string PrivateField()
        {
            return $"private {GetParameterTypeStr()} _{_columnName};";
        }

        public string PublicField()
        {
            string str = $"public { GetParameterTypeStr()} {_columnName}";
            str += " { get { return _" + _columnName + "; } }";
            return str;
        }
    }
}
