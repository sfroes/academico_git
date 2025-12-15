using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class CursoLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true)]
        public string Nome { get; set; }

        [SMCHidden]
        public long? SeqNivelEnsino { get; set; }

        public string DescricaoInstituicaoNivelEnsino { get; set; }

        [SMCHidden]
        public long? SeqSituacaoAtual { get; set; }

        public string DescricaoSituacaoAtual { get; set; }
    }
}
