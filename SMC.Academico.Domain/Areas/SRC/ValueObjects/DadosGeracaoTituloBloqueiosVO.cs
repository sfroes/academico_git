using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class DadosGeracaoTituloBloqueiosVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }

        public long SeqSolicitacaoServicoBoleto { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NumeroProtocolo { get; set; }

        public string DescricaoProcesso { get; set; }

        public string DescricaoPessoaAtuacao { get; set; }
    }
}
