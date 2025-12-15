using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CUR.Controllers;
using SMC.SGA.Administrativo.Areas.CUR.Views.Periodico.App_LocalResources;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class PeriodicoImportacaoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqClassificacaoPeriodico { get; set; }

        [SMCOrder(3)]
        [SMCRequired]
        [SMCMask("9999")]
        [SMCMinValue(1950)]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public int? AnoInicio { get; set; }

        [SMCOrder(4)]
        [SMCMask("9999")]
        [SMCMinValue(nameof(AnoInicio))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public int? AnoFim { get; set; }

        [SMCFile(HideDescription = true)]
        [SMCInclude(Ignore = true)]
        [SMCCssClass("smc-sga-upload-linha-unica")]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCUploadFile ArquivoAnexado { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCDependency(nameof(AnoInicio), nameof(PeriodicoController.BuscarDescricaoClassificacaoCapes), "Periodico", false, new string[] { nameof(AnoFim) })]
        [SMCDependency(nameof(AnoFim), nameof(PeriodicoController.BuscarDescricaoClassificacaoCapes), "Periodico", false, new string[] { nameof(AnoInicio) })]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInformativa { get; set; } = UIResource.MensagemInformativa;
    }
}