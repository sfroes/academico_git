using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Views.CampanhaOferta.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConfigurarVagasOfertaCampanhaViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCHidden]
        public long SeqCampanha { get; set; }

        public List<ConfigurarVagasOfertaCampanhaListaViewModel> CampanhaOfertas { get; internal set; }

        public List<ProcessoSeletivoViewModel> ProcessosSeletivos { get; set; }

        [SMCHidden]
        public long[] SeqsProcessosSeletivos { get; set; }

        [SMCHidden]
        public long[] SeqsConvocacoes { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(1, 0)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string MensagemAlertaProcessoSeletivo { get { return UIResource.MensagemAlertaProcessoSeletivo; } }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(2, 0)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string MensagemAlertaOfertaCampanha { get { return UIResource.MensagemAlertaOfertaCampanha; } }

        public List<SMCTreeViewNode<ProcessoSeletivoListarViewModel>> ProcessosSeletivosTree { get; set; }

        public SMCSelectedValue<long>[] ProcessosSelecionados { get; set; }

        public List<long> gridCampanhaOferta { get; set; }
    }
}