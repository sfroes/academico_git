using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class ApuracaoAvaliacaoData : ISMCMappable, ISMCSeq
    {
         public long Seq { get; set; }

        public long SeqAplicacaoAvaliacao { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public decimal? Nota { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }
        
        public bool Comparecimento { get; set; }

        public long? SeqArquivoAnexadoAtaDefesa { get; set; }

        public string ComentarioApuracao { get; set; }
    }
}
