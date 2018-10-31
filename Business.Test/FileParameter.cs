using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Business.Test
{
    [TestClass]
    public class FileParameter
    { 
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
        public void FileParameter_VB_ConstructorField()
        {
            string expectedString = $"If drReq.Tables.Columns.Contains(\"BodID\") Then Me._BodID = SparkBase.Convert.ToInt32(drReq.Item(\"BodID\"))"; 

            Business.TemplateGenerator.FileParameter_VB vb = new Business.TemplateGenerator.FileParameter_VB("BodID", typeof(Int32));

            TestContext.WriteLine(vb.ConstructorField());
            Assert.AreEqual(expectedString, vb.ConstructorField()); 
        }

        [TestMethod]
        public void FileParameter_VB_PrivateField()
        {
            string expectedString = $"private _BodID as Int32";

            Business.TemplateGenerator.FileParameter_VB vb = new Business.TemplateGenerator.FileParameter_VB("BodID", typeof(Int32)); 

            TestContext.WriteLine(vb.PrivateField());
            Assert.AreEqual(expectedString, vb.PrivateField()); 
        }

        [TestMethod]
        public void FileParameter_VB_PublicField()
        {
            string expectedString = "Public ReadOnly Property BodID as Int32 \n \t Get \n \t \t return _BodID \n \t End Get \n End Property";

            Business.TemplateGenerator.FileParameter_VB vb = new Business.TemplateGenerator.FileParameter_VB("BodID", typeof(Int32));

            TestContext.WriteLine(vb.PublicField());
            Assert.AreEqual(expectedString, vb.PublicField()); 
        }


        [TestMethod]
        public void FileParameter_CS_ConstructorField()
        {
            string expectedString = "if (drReq.Table.Columns.Contains(\"BodID\")) { this._BodID = SparkBase.Convert.ToInt32(drReq.Item[\"BodID\"]); }";

            Business.TemplateGenerator.FileParameter_CS cs = new Business.TemplateGenerator.FileParameter_CS("BodID",  typeof(Int32));

            TestContext.WriteLine(cs.ConstructorField());
            Assert.AreEqual(expectedString, cs.ConstructorField()); 
        }

        [TestMethod]
        public void FileParameter_CS_PrivateField()
        {
            string expectedString = "private Int32 _BodID;";

            Business.TemplateGenerator.FileParameter_CS cs = new Business.TemplateGenerator.FileParameter_CS("BodID", typeof(Int32));

            TestContext.WriteLine(cs.PrivateField());
            Assert.AreEqual(expectedString, cs.PrivateField());
        }

        [TestMethod]
        public void FileParameter_CS_PublicField()
        {
            string expectedString = "public Int32 BodID { get { return _BodID; } }";

            Business.TemplateGenerator.FileParameter_CS cs = new Business.TemplateGenerator.FileParameter_CS("BodID", typeof(Int32));

            TestContext.WriteLine(cs.PublicField());
            Assert.AreEqual(expectedString, cs.PublicField()); 
        }

    }
}
