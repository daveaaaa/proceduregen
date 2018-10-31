using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class ModelBuilder : IModelBuilder
    {
        private Business.TemplateGenerator.TemplateGenerator  _templateGenerator;
        private Business.DBAConnection _dba;

        public ModelBuilder(Business.TemplateGenerator.TemplateGenerator templateGenerator, Business.DBAConnection dba)
        {
            _templateGenerator = templateGenerator;
            _dba = dba;
        }


        /// <summary>
        /// Generate a new model
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="dba"></param>
        /// <returns></returns>
        public List<Business.TemplateGenerator.Template> GenerateModels(string connectionString, string schema, string procedure, TemplateGenerator.FileType fileType)
        {
            return GenerateModels("", connectionString, schema, procedure, fileType);
        }

        /// <summary>
        ///  Generate a new model
        /// </summary>
        /// <param name="className"></param>
        /// <param name="connectionString"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="fileType"></param>
        /// <param name="templateGenerator"></param>
        /// <param name="dba"></param>
        /// <returns></returns>
        public List<Business.TemplateGenerator.Template> GenerateModels(string className, string connectionString, string schema, string procedure, TemplateGenerator.FileType fileType)
        {
            List<Viewmodel.ProcedureParameter> procedureParam = _dba.GetProcedureParameters(connectionString, schema, procedure);

            List<Data.DBA.Structure.SPParam> spParams = GetNonDefaultParameters(procedureParam);

            System.Data.DataSet ds = _dba.ExecStoredProcedure(connectionString, schema, procedure, spParams);

            List<Business.TemplateGenerator.Template> templates = GenerateTemplates(className, schema, procedure, fileType, ds);

            return templates;
        }

        /// <summary>
        /// Generate a list of templates to write to disk / stream
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="schema"></param>
        /// <param name="procedure"></param>
        /// <param name="fileType"></param>
        /// <param name="templateGenerator"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<Business.TemplateGenerator.Template> GenerateTemplates(string classname, string schema, string procedure, TemplateGenerator.FileType fileType, System.Data.DataSet ds)
        {
            List<Business.TemplateGenerator.Template> results = new List<TemplateGenerator.Template>();
            Business.TemplateGenerator.Template template;

            if (ds.Tables.Count > 1)
            {
                for (Int32 i = 0; i < ds.Tables.Count; i++)
                {
                    System.Data.DataTable dt = ds.Tables[i];

                    if (string.IsNullOrEmpty(classname))
                    {
                        template = _templateGenerator.GenerateTemplate(i, schema, procedure, dt, fileType);
                    }
                    else
                    {
                        template = _templateGenerator.GenerateTemplate(classname, dt, fileType);
                    }

                    results.Add(template);
                }
            }
            else if ( 1 ==  ds.Tables.Count)
            {
                if (string.IsNullOrEmpty(classname))
                {
                    template = _templateGenerator.GenerateTemplate(null, schema, procedure, ds.Tables[0], fileType);
                }
                else
                {
                    template = _templateGenerator.GenerateTemplate(classname, ds.Tables[0], fileType);
                }

                results.Add(template);
            }

            return results;
        }

        /// <summary>
        /// Find all non defaulted parameters and generate a default value
        /// </summary>
        /// <param name="procedureParam"></param>
        /// <returns></returns>
        private List<Data.DBA.Structure.SPParam> GetNonDefaultParameters(List<Viewmodel.ProcedureParameter> procedureParam)
        {
            List<Data.DBA.Structure.SPParam> spParams = new List<Data.DBA.Structure.SPParam>();

            foreach (Viewmodel.ProcedureParameter param in procedureParam)
            {
                if (!param.HasDefaultValue)
                {
                    switch (param.Type.ToLower())
                    {
                        case "int":
                            spParams.Add(new Data.DBA.Structure.SPParam_Int(param.ParameterName, param.IsOutput));
                            break;
                        case "intreger":
                            spParams.Add(new Data.DBA.Structure.SPParam_Int(param.ParameterName, param.IsOutput));
                            break;

                        case "decimal":
                            spParams.Add(new Data.DBA.Structure.SPParam_Decimal(param.ParameterName, param.Precision, param.Length, param.IsOutput));
                            break;
                        case "double":
                            spParams.Add(new Data.DBA.Structure.SPParam_Double(param.ParameterName,  param.IsOutput));
                            break;
                        case "float":
                            spParams.Add(new Data.DBA.Structure.SPParam_Double(param.ParameterName,  param.IsOutput));
                            break;

                        case "date":
                            spParams.Add(new Data.DBA.Structure.SPParam_Datetime(param.ParameterName, param.IsOutput));
                            break;
                        case "datetime":
                            spParams.Add(new Data.DBA.Structure.SPParam_Datetime(param.ParameterName, param.IsOutput));
                            break;

                        case "varchar":
                            spParams.Add(new Data.DBA.Structure.SPParam_String(param.ParameterName, param.IsOutput));
                            break;
                        case "nvarchar":
                            spParams.Add(new Data.DBA.Structure.SPParam_String(param.ParameterName, param.IsOutput));
                            break;

                        case "bit":
                            spParams.Add(new Data.DBA.Structure.SPParam_Bit(param.ParameterName, param.IsOutput));
                            break;

                        case "money":
                            spParams.Add(new Data.DBA.Structure.SPParam_Money(param.ParameterName, param.IsOutput));
                            break;
                    }
                }
            }

            return spParams;
        }


    }
}
