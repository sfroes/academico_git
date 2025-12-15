using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaProcessamentoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public List<long> SeqsGruposEscalonamento { get; set; }
    }
}
