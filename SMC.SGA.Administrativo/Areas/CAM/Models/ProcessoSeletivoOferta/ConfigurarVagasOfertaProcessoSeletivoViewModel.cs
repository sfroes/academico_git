using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Views.ProcessoSeletivoOferta.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConfigurarVagasOfertaProcessoSeletivoViewModel : SMCWizardViewModel, ISMCStatefulView
    {
        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCHidden]
        public long SeqProcessoSeletivo { get; set; }

        public List<ConfigurarVagasOfertaProcessoSeletivoListaViewModel> ProcessoSeletivoOfertas { get; internal set; }

        public List<ConvocacaoListarViewModel> Convocacoes { get; set; }

        [SMCHidden]
        public long[] SeqsConvocacoes { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(2, 0)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string MensagemAlertaOfertaConvocacao { get { return UIResource.MensagemAlertaOfertaConvocacao; } }

        public List<SMCTreeViewNode<ConvocacaoListarViewModel>> ConvocacoesTree { get; set; }

        public List<SMCSelectedValue<long>> ConvocacoesSelecionadas { get; set; }

        public List<ConfigurarVagasOfertaCampanhaListaViewModel> CampanhaOfertas { get; internal set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string MensagemConfirmacaoCopiarVagasCampanhaOferta { get { return UIResource.MSG_ConfirmarCopiarVagasOfertaCampanha; } }

        [SMCDetail(SMCDetailType.Tabular)]
        public SMCMasterDetailList<ConfigurarVagasOfertaCampanhaListaViewModel> Inscricoes { get; set; }

        public List<long> gridListaOfertasProcessoSeletivo { get; set; }

        public List<long> selectedValues { get; set; }
    }
}