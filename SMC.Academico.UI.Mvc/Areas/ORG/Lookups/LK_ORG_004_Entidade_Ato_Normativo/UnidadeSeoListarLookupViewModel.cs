using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class UnidadeSeoListarLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {

        [SMCKey]
        [SMCMapProperty("Codigo")]
        public int CodigoUnidade { get; set; }

        [SMCHidden]
        [SMCMapProperty("Nome")]
        public string Descricao { get; set; }

        [SMCDescription]
        public string Descricaoformatada => string.IsNullOrEmpty(Descricao) ? null : $"{CodigoUnidade} - {Descricao}";
    }
}
