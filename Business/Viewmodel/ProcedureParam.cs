using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Viewmodel
{
    public class ProcedureParameter
    {
        private Int32 _order;
        private string _parameterName;
        private string _type;
        private Int16 _length;
        private Int32 _prec;
        private bool _hasDefaultValue;
        private bool _isOutput;

        public Int32 Order { get { return _order; } }
        public string ParameterName { get { return _parameterName; } }
        public string Type { get { return _type; } }
        public Int16 Length { get { return _length; } }
        public Int32 Precision { get { return _prec; } }
        public bool HasDefaultValue { get { return _hasDefaultValue; } }
        public bool IsOutput { get { return _isOutput; } }


        public ProcedureParameter(System.Data.DataRow drReq)
        {
            if (drReq.Table.Columns.Contains("Order")) { this._order = (Int32)drReq["Order"]; }
            if (drReq.Table.Columns.Contains("ParameterName")) { this._parameterName = (string)drReq["ParameterName"]; }
            if (drReq.Table.Columns.Contains("Type")) { this._type = (string)drReq["Type"]; }
            if (drReq.Table.Columns.Contains("Length")) { this._length = (Int16)drReq["Length"]; }
            if (drReq.Table.Columns.Contains("prec")) { this._prec = (Int32)drReq["prec"]; }
            if (drReq.Table.Columns.Contains("hasDefaultValue")) { this._hasDefaultValue = (bool)drReq["hasDefaultValue"]; }
            if (drReq.Table.Columns.Contains("isOutput")) { this._isOutput = (bool)drReq["isOutput"]; }
        }

    }
}
