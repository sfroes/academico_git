using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoEtapaProcessamentoListarData : ISMCMappable
    {
        public long SeqEscalonamento { get; set; }

        public string DescricaoEscalonamento { get; set; }

        public List<long> SeqsGruposEscalonamento { get; set; }

    }
}
