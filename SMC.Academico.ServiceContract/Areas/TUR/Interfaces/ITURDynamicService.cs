using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SMC.Framework.Dynamic;
using SMC.Framework.Model;
using SMC.Academico.Common.Constants;

namespace SMC.Academico.ServiceContract.Areas.TUR.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITURDynamicService : ISMCService
    {
    }
}
