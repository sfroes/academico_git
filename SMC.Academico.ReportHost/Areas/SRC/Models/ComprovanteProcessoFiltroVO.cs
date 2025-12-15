using SMC.Framework.Mapper;

namespace SMC.Academico.ReportHost.Areas.MAT.Models
{
    public class ComprovanteProcessoFiltroVO : ISMCMappable
    {
        public long SeqSolicitacaoMatricula { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long? SeqProcessoEtapa { get; set; }
    }
}