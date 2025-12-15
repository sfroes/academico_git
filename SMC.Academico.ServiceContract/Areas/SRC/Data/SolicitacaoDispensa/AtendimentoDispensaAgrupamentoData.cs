using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class AtendimentoDispensaAgrupamentoData : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public List<SMCDatasourceItem> ItensOrigensInternas { get; set; }

        public List<SMCDatasourceItem> ItensOrigensExternas { get; set; }

        public List<SMCDatasourceItem> ItensDestinos { get; set; }

        public List<SolicitacaoDispensaGrupoData> Grupos { get; set; }
    }
}