using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class ProcessoLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoServico { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string DescricaoCicloLetivo { get; set; }
    }
}