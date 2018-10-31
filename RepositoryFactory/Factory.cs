using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFactory
{
    public static class Factory
    {
        public static Business.IDBAConnection GetDBAConnection()
        {
            return new Business.DBAConnection(new Data.DBA.DBA());
        }

        public static Business.IModelBuilder GetModelBuilder()
        {

            var _tg = new Business.TemplateGenerator.TemplateGenerator();
            var _dbaConnection = new Business.DBAConnection(new Data.DBA.DBA());

            return new Business.ModelBuilder(_tg,_dbaConnection);
        }
    }
}
