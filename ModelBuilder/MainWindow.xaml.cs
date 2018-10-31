using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModelBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Business.TemplateGenerator.FileType> ft = new List<Business.TemplateGenerator.FileType>();

            ft.Add(Business.TemplateGenerator.FileType.VB);
            ft.Add(Business.TemplateGenerator.FileType.CS);

            ddlFileType.ItemsSource = ft;
            ddlFileType.SelectedIndex = 0;
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            Business.IDBAConnection dba = RepositoryFactory.Factory.GetDBAConnection();

            string connectionString = txtConnectionString.Text;
            bool isValid = dba.IsConnectionValid(connectionString);

            if (isValid)
            {
                List<Business.Viewmodel.Procedure> sp = dba.ListStoreProcedures(connectionString);

                ddlSP.ItemsSource = sp;
                ddlSP.DisplayMemberPath = "DisplayText";

                ddlSP.SelectedIndex = 0;
            }
        }

        private void btnGenerateModel_Click(object sender, RoutedEventArgs e)
        {
            String className = txtClassName.Text;
            String connectionString = txtConnectionString.Text;

            Business.TemplateGenerator.FileType fileType = (Business.TemplateGenerator.FileType)ddlFileType.SelectedItem;

            Business.Viewmodel.Procedure selectedP = (Business.Viewmodel.Procedure)ddlSP.SelectedItem;

            Business.IModelBuilder mb = RepositoryFactory.Factory.GetModelBuilder();

            List<Business.TemplateGenerator.Template> templates = mb.GenerateModels(className, connectionString, selectedP.Schema, selectedP.ProcedureName, fileType); 

            foreach(Business.TemplateGenerator.Template temp in templates)
            {
                txtResult.Text = temp.FileString;
            } 
        } 
    }
}
