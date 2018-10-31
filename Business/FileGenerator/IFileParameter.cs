using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.TemplateGenerator
{

    public interface IFileParameter
    {
        string PrivateField();
        string PublicField();
        string ConstructorField();
    }
}
