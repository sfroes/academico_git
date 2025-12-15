using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoServicoLookupViewModel : SMCViewModelBase, ISMCMappable
    {

        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string DescricaoLookupSolicitacao { get; set; }

    }
}