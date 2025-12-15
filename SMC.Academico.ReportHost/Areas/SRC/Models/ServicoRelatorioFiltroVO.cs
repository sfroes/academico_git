using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.SRC.Models
{
    public class ServicoRelatorioFiltroVO : ISMCMappable
    {
        public long? SeqCicloLetivo { get; set; }

        public long? SeqServico { get; set; }

        public List<long> SeqsEntidadeResponsavel { get; set; }

        public List<long> SeqsProcessos { get; set; }

        public long? SeqProcessoEtapa { get; set; }

        public TipoRelatorioServico TipoRelatorioServico { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
    }
}