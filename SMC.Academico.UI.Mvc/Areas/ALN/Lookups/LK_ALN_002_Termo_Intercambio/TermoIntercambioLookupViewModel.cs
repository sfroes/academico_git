using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.ALN.Lookups
{
    public class TermoIntercambioLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string TipoTermoIntercambio { get; set; }

        public string InstituicaoEnsinoExterna { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }
    }
}
