using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RelatorioConsolidadoSituacaoFiltroVO : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public List<TipoAtuacao> TipoAtuacoes { get; set; }

    }
}