using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaTelefoneLookupViewModel : ISMCMappable, ISMCSeq
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        [SMCMapProperty("SeqPessoa")]
        public long SeqPessoaTelefone { get; set; }

        [SMCHidden]
        [SMCMapProperty("SeqTelefone")]
        [SMCMapProperty("Telefone.Seq")]
        public long SeqTelefone { get; set; }

        [SMCMapProperty("Telefone.TipoTelefone")]
        [SMCSize(SMCSize.Grid4_24)]
        public TipoTelefone TipoTelefone { get; set; }

        [SMCMapProperty("Telefone.CodigoPais")]
        [SMCSize(SMCSize.Grid2_24)]
        public int CodigoPais { get; set; }

        [SMCMapProperty("Telefone.CodigoArea")]
        [SMCSize(SMCSize.Grid2_24)]
        public int CodigoArea { get; set; }

        [SMCMapProperty("Telefone.Numero")]
        [SMCSize(SMCSize.Grid4_24)]
        public string Numero { get; set; }
    }
}