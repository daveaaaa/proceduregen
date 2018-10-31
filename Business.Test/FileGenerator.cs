using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Test
{
    /// <summary>
    /// Summary description for TemplateGenerator
    /// </summary>
    [TestClass]
    public class FileGenerator
    {
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ClassName_Null_Table_CS()
        {

            string className = $"{schema}_{procedure}";
            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();

            Business.TemplateGenerator.Template template = fg.GenerateTemplate(null, schema, procedure, new System.Data.DataTable(), Business.TemplateGenerator.FileType.CS);

            TestContext.WriteLine(template.ClassName);
            Assert.AreEqual(className, template.ClassName);

        }

        [TestMethod]
        public void ClassName_One_Table_CS()
        {

            string className = $"{schema}_{procedure}_1";
            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();

            Business.TemplateGenerator.Template template = fg.GenerateTemplate(1, schema, procedure, new System.Data.DataTable(), Business.TemplateGenerator.FileType.CS);

            TestContext.WriteLine(template.ClassName);
            Assert.AreEqual(className, template.ClassName);

        }

        [TestMethod]
        public void ClassName_Null_Table_VB()
        {

            string className = $"{schema}_{procedure}";
            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();

            Business.TemplateGenerator.Template template = fg.GenerateTemplate(null, schema, procedure , new System.Data.DataTable(), Business.TemplateGenerator.FileType.VB);

            TestContext.WriteLine(template.ClassName);
            Assert.AreEqual(className, template.ClassName);

        }

        [TestMethod]
        public void ClassName_One_Table_VB()
        {

            string className = $"{schema}_{procedure}_1";
            Business.TemplateGenerator.TemplateGenerator fg = new Business.TemplateGenerator.TemplateGenerator();

            Business.TemplateGenerator.Template template = fg.GenerateTemplate(1, schema, procedure, new System.Data.DataTable(), Business.TemplateGenerator.FileType.VB);

            TestContext.WriteLine(template.ClassName);
            Assert.AreEqual(className, template.ClassName);

        }
    }
}
