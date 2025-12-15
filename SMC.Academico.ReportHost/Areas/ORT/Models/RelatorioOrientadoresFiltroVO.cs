using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ReportHost.Areas.ORT.Models
{
    public class RelatorioOrientadoresFiltroVO : ISMCMappable
    {
        [SMCMapProperty("SeqsEntidadesResponsaveisHierarquiaItem")]
        public List<long?> SeqEntidadeResponsavel { get; set; }

        public long? SeqPessoaAtuacao { get; set; }

        public bool? ExibirOrientacoesFinalizadas { get; set; }

        public long SeqInstituicaoEnsino { get; set; }
        
        public long SeqCicloLetivo { get; set; }

        public List<long?> SeqsTiposSituacoesMatriculas { get; set; }

    }
}