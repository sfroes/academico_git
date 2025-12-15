using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class CondicoesObrigatoriedadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqCondicaoObrigatoriedade { get; set; }

        public string DescricaoCondicaoObrigatoriedade { get; set; }

        public bool Ativo { get; set; }
    }
}