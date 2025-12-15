using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaEnderecoEletronicoLookupViewModel : ISMCMappable, ISMCSeq
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCMapProperty("SeqPessoa")]
        public long SeqPessoaEnderecoEletronico { get; set; }

        [SMCHidden]
        [SMCMapProperty("EnderecoEletronico.Seq")]
        [SMCMapProperty("SeqEnderecoEletronico")]
        public long SeqEnderecoEletronico { get; set; }

        [SMCHidden]
        public bool BloquearEdicao { get; set; }

        [SMCMapProperty("EnderecoEletronico.TipoEnderecoEletronico")]
        [SMCSize(SMCSize.Grid5_24)]
        public TipoEnderecoEletronico TipoEnderecoEletronico { get; set; }

        [SMCMapProperty("EnderecoEletronico.Descricao")]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid7_24)]
        public string Descricao { get; set; }
    }
}