using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaEnderecoEletronicoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqPessoa { get; set; }

        public long SeqEnderecoEletronico { get; set; }

        [SMCMapProperty("Descricao")]
        [SMCMapProperty("EnderecoEletronico.Descricao")]
        public string DescricaoEnderecoEletronico { get; set; }

        [SMCMapProperty("EnderecoEletronico.TipoEnderecoEletronico")]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

    }
}
