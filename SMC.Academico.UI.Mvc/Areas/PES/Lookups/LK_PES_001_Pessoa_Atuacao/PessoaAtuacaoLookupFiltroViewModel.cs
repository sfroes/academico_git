using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaAtuacaoLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid21_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid21_24)]
        public string Nome { get; set; }

        [SMCCpf]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        public string Cpf { get; set; }

        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid5_24)]
        public string NumeroPassaporte { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCConditionalReadonly(nameof(TipoAtuacaoSomenteleitura), true, PersistentValue = true)]
        public TipoAtuacao? TipoAtuacao { get; set; }

        [SMCHidden]
        public long? SeqPessoa { get; set; }

        [SMCHidden]
        public bool TipoAtuacaoSomenteleitura { get; set; }
    }
}