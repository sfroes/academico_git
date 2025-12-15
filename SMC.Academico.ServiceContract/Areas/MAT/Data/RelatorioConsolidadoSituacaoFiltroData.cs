using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class RelatorioConsolidadoSituacaoFiltroData : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public List<TipoAtuacao> TipoAtuacoes { get; set; }
    }
}