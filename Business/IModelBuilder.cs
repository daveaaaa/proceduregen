using System.Collections.Generic;
using Business.TemplateGenerator;

namespace Business
{
    public interface IModelBuilder
    {
        List<Template> GenerateModels(string connectionString, string schema, string procedure, FileType fileType);
        List<Template> GenerateModels(string className, string connectionString, string schema, string procedure, FileType fileType);
    }
}