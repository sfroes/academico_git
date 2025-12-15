using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ICalendarioService : ISMCService
    {
        List<SMCDatasourceItem> BuscarCalendariosUnidadeResponsavelSelect(long seqUnidadeResponsavel);

        string BuscarNomeCalendario(long seqCalendario);
    }
}
