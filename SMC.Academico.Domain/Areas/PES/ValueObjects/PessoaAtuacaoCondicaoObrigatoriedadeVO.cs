using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqCondicaoObrigatoriedade { get; set; }

        public bool Ativo { get; set; }

        [SMCMapProperty("CondicaoObrigatoriedade.Descricao")]
        public string DescricaoCondicaoObrigatoriedade { get; set; }

        public List<CondicoesObrigatoriedadeVO> CondicoesObrigatoriedade { get; set; }

        public bool PossuiSituacaoImpeditivaIngressante { get; set; }
    }
}