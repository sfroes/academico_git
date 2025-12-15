using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class AtendimentoDispensaAgrupamentoVO : ISMCMappable
    {
        public List<SMCDatasourceItem> ItensOrigensInternas { get; set; }

        public List<SMCDatasourceItem> ItensOrigensExternas { get; set; }

        public List<SMCDatasourceItem> ItensDestinos { get; set; }

        public long SeqSolicitacaoServico { get; set; }

        public List<SolicitacaoDispensaGrupoVO> Grupos { get; set; }
    }
}