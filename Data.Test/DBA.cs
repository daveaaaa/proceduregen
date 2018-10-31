using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Test
{
    [TestClass]
    public class DBA
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
        public void IsValidConnectionString_NoString_False()
        {
            string connectionString = "";

            Data.DBA.DBA dba = new Data.DBA.DBA();

            bool isValid = dba.IsConnectionValid(connectionString);

            Assert.IsFalse(isValid, "No connection string passed and it was a valid response");
        }

        [TestMethod]
        public void IsValidConnectionString_StringPassed_False()
        {
            string connectionString = " ";


            Data.DBA.DBA dba = new Data.DBA.DBA();

            bool isValid = dba.IsConnectionValid(connectionString);

            Assert.IsFalse(isValid, "No connection string passed and it was a valid response");
        }

        [TestMethod]
        public void IsValidConnectionString_StringPassed_True()
        {
            string connectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=modelbuilder; Server=David-Laptop\\SQLExpress;";

            Data.DBA.DBA dba = new Data.DBA.DBA();

            bool isValid = dba.IsConnectionValid(connectionString);

            Assert.IsTrue(isValid, "Valid connection string passed and failed");
        }

        [TestMethod]
        public void ListStoredProcedures_True()
        {
            string connectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=modelbuilder; Server=David-Laptop\\SQLExpress;";

            Data.DBA.DBA dba = new Data.DBA.DBA();
            System.Data.DataTable dt = dba.ListStoredProcedure(connectionString);

            Assert.IsTrue((dt.Rows.Count > 0), "No data retreved");

            PrintDataTable(dt);
        }

        [TestMethod]
        public void GetProcedureParameters_True()
        {
            string connectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=modelbuilder; Server=David-Laptop\\SQLExpress;";
            string schema = "dbo";
            string procedure = "Bod_qGetDetail";

            Data.DBA.DBA dba = new Data.DBA.DBA();
            System.Data.DataTable dt = dba.GetProcedureParameters(connectionString, schema, procedure);

            Assert.IsTrue((dt.Rows.Count > 0), "No data retreved");

            PrintDataTable(dt);
        }

        [TestMethod]
        public void ExecStoredProcedure()
        {
            string connectionString = "Persist Security Info=False;Integrated Security=true; Initial Catalog=modelbuilder; Server=David-Laptop\\SQLExpress;";
            string schema = "dbo";
            string procedure = "Bod_qGetDetail";
            List<Data.DBA.Structure.SPParam> paramCollection = new List<Data.DBA.Structure.SPParam>() { new Data.DBA.Structure.SPParam_Int("@BodID", false), new Data.DBA.Structure.SPParam_Int("@ReturnValue", true) };

            Data.DBA.DBA dba = new Data.DBA.DBA();

            System.Data.DataSet ds = dba.ExecStoredProcedure(connectionString, schema, procedure, paramCollection);

            Assert.IsTrue(ds.Tables.Count == 1, "No tables returned");

            PrintDataTable(ds.Tables[0]);
        }

        private void PrintDataTable(System.Data.DataTable dt)
        {
            Int32 columns = dt.Columns.Count;
            string strHeader = "";

            for (Int32 i = 0; i < columns; i++)
            {
                strHeader += $"{dt.Columns[i].ColumnName}\t";
            }

            string strRow = "";
            foreach (System.Data.DataRow row in dt.Rows)
            {
                for (Int32 i = 0; i < columns; i++)
                {
                    strRow += $"{row[i]}\t";
                }
                strRow += "\n";
            }

            TestContext.WriteLine(strHeader);
            TestContext.WriteLine(strRow);
        }
    }
}
