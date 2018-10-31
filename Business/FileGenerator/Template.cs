using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Business.TemplateGenerator
{
    public class Template
    {

        private string _className;
        private string _fileString;
        private FileType _fileType;
        private List<string> _privateFields;
        private List<string> _publicFields;
        private List<string> _constructorFields;

        public string ClassName { get { return _className; } }

        public string PrivateFields {
            get {
                StringBuilder sb = new StringBuilder("");
                foreach (string field in _privateFields)
                {
                    string formattedField = $"\t\t{field}";
                    sb.AppendLine(formattedField);
                }

                return sb.ToString();
            }
        }

        public string PublicFields {
            get {
                StringBuilder sb = new StringBuilder("");
                foreach (string field in _publicFields)
                {
                    string formattedField = $"\t\t{field}";
                    sb.AppendLine(formattedField);
                }

                return sb.ToString();
            }
        }

        public string ConstructorFields {
            get {
                StringBuilder sb = new StringBuilder("");
                foreach (string field in _constructorFields)
                {
                    string formattedField = $"\t\t{field}";
                    sb.AppendLine(formattedField);
                }

                return sb.ToString();
            }
        }

        public string FileString { get { return _fileString; } }

        public string FileName { get {

                string ext = "";
                switch (this._fileType)
                {
                    case FileType.CS:
                        ext = "cs";
                        break;
                    case FileType.VB:
                        ext = "vb";
                        break;
                }

                return $"{_className}.{ext}";
            } }

        /// <summary>
        /// Create Template
        /// </summary>
        /// <param name="className"></param>
        /// <param name="fileParams"></param>
        /// <param name="fileType"></param>
        public Template(string className, List<IFileParameter> fileParams, FileType fileType)
        {

            this._className = className.Replace(".","_");
            this._fileType = fileType;

            GenerateBookmarks(fileParams);
            GetRawTemplate();
            AddBookmarks();

        }

        /// <summary>
        /// Generate bookmarks
        /// </summary>
        /// <param name="fileParams"></param>
        private void GenerateBookmarks(List<IFileParameter> fileParams)
        {
            this._publicFields = new List<string>();
            this._privateFields = new List<string>();
            this._constructorFields = new List<string>();

            foreach (IFileParameter param in fileParams)
            {
                this._publicFields.Add(param.PublicField());
                this._privateFields.Add(param.PrivateField());
                this._constructorFields.Add(param.ConstructorField());
            }

        }

        /// <summary>
        /// Add the bookmarks to the file stirng 
        /// </summary>
        private void AddBookmarks()
        {
            this._fileString = _fileString.Replace("%classname%", this.ClassName);
            this._fileString = _fileString.Replace("%privatefields%", this.PrivateFields);
            this._fileString = _fileString.Replace("%publicfields%", this.PublicFields);
            this._fileString = _fileString.Replace("%constructorfields%", this.ConstructorFields);
        }

        /// <summary>
        /// Get the template file
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        private void GetRawTemplate()
        {
             var assembly = Assembly.GetExecutingAssembly();

            string fileName ="";
            switch (this._fileType)
            {
                case FileType.CS:
                    fileName = "CSTemplate.txt";
                    break;
                case FileType.VB:
                    fileName = "VBTemplate.txt";
                    break;
            }

            string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));


            string result = "";  
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            this._fileString = result;
        }

    }
}
