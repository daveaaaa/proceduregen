using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business.TemplateGenerator
{
    public class TemplateGenerator
    { 


        /// <summary>
        /// Generate the columns from the datatable
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="dt"></param>
        /// <param name="filepath"></param>
        public Template GenerateTemplate(Int32? tableID, string schema, string procedure, System.Data.DataTable dt, FileType fileType)
        {
            String classname = $"{schema}.{procedure}";

            if (null != tableID)
            {
                classname += $"_{Convert.ToString(tableID)}";
            }

            return GenerateTemplate(classname, dt, fileType);
        }

        /// <summary>
        /// Generate Template
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="dt"></param>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public Template GenerateTemplate(string classname, System.Data.DataTable dt, FileType fileType)
        {

            List<IFileParameter> fileParams = new List<IFileParameter>();

            foreach (System.Data.DataColumn col in dt.Columns)
            {
                switch (fileType)
                {
                    case FileType.CS:
                        fileParams.Add(new FileParameter_CS(col.ColumnName, col.DataType));
                        break;
                    case FileType.VB:
                        fileParams.Add(new FileParameter_VB(col.ColumnName, col.DataType));
                        break;
                }

            }

            return new Template(classname, fileParams, fileType);
        }
         
       
    }
}