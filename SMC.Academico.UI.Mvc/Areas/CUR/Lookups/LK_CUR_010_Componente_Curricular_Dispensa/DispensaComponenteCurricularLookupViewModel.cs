using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class DispensaComponenteCurricularLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCCssClass("smc-size-md-8 smc-size-xs-8 smc-size-sm-8 smc-size-lg-8")]
        [SMCOrder(1)]
        public string DescricaoOfertaCurso { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCOrder(2)]
        public string DescricaoCicloLetivo { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-12 smc-size-xs-12 smc-size-sm-12 smc-size-lg-12")]
        [SMCOrder(3)]
        public string DescricaoCompleta { get; set; }

        [SMCHidden]
        public short? CargaHoraria { get; set; }

        [SMCHidden]
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        [SMCHidden]
        public short? Credito { get; set; }
    }
}