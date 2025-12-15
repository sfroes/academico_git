using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class RelatorioSolicitacoesBloqueioFiltroData : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public long? SeqProcessoEtapa { get; set; }
    }
}
