using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Test
{
    [TestClass]
    public class ModelBuilder
    {

        private const string connectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=modelbuilder; Server=David-Laptop\\SQLExpress;";
        private const string schema = "dbo";
        private const string procedure = "Bod_qGetDetail";
         
        private TestContext testContextInstance;
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void Generate_VBModel()
        {


            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();
            Business.DBAConnection dBAConnection = new Business.DBAConnection(new Data.DBA.DBA());

            Business.ModelBuilder mb = new Business.ModelBuilder(fg, dBAConnection);

            List<Business.TemplateGenerator.Template> result = mb.GenerateModels(connectionString, schema, procedure, Business.TemplateGenerator.FileType.VB);

            PrintTemplate(result);

            Assert.IsTrue(result.Count > 0, "No templates generated");
        }

        [TestMethod]
        public void Generate_CSModel()
        {
            
            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();
            Business.DBAConnection dBAConnection = new Business.DBAConnection(new Data.DBA.DBA());

            Business.ModelBuilder mb = new Business.ModelBuilder(fg, dBAConnection);
            List<Business.TemplateGenerator.Template> result = mb.GenerateModels(connectionString, schema, procedure, Business.TemplateGenerator.FileType.CS);

            PrintTemplate(result);

            Assert.IsTrue(result.Count > 0, "No templates generated");
        }

        /// <summary>
        ///print templates 
        /// </summary>
        /// <param name="templates"></param>
        private void PrintTemplate(List<Business.TemplateGenerator.Template> templates)
        {
            foreach (Business.TemplateGenerator.Template template in templates)
            {
                TestContext.WriteLine(template.FileName);
                TestContext.WriteLine("");
                TestContext.WriteLine(template.FileString);
            }
        }
    }
}
