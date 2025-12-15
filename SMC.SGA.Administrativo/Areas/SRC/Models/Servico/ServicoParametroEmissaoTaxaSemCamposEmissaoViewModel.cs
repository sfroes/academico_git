using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ServicoParametroEmissaoTaxaSemCamposEmissaoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        #region [ Campos Edição ]

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.TiposEmissaoTaxas))]
        [SMCDependency(nameof(ServicoViewModel.OrigemSolicitacaoServicoAuxiliarTaxas), nameof(ServicoController.BuscarTiposEmissaoTaxa), "Servico", true, includedProperties: new[] { nameof(TipoEmissaoTaxaAuxiliar) })]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.List)]
        public TipoEmissaoTaxa TipoEmissaoTaxa { get; set; }

        [SMCHidden]
        public TipoEmissaoTaxa TipoEmissaoTaxaAuxiliar => TipoEmissaoTaxa;

        #endregion [ Campos Edição ]

        #region [ Campos Lista ]

        [SMCCssClass("smc-size-md-22 smc-size-xs-22 smc-size-sm-22 smc-size-lg-22")]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string ListaTipoEmissaoTaxa => TipoEmissaoTaxa.SMCGetDescription();

        #endregion [ Campos Lista ]   
    }
}