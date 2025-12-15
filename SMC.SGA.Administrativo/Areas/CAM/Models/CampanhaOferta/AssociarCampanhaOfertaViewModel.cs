using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CAM.Views.CampanhaOferta.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class AssociarCampanhaOfertaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCSize(Framework.SMCSize.Grid24_24)]
        [SMCDependency(nameof(SeqCampanha))]
        [SelecaoOfertaCampanhaLookup]
        public List<CampanhaOfertaViewModel> OfertasCampanha { get; set; }

        public List<ProcessoSeletivoViewModel> ProcessosSeletivos { get; set; }

        [SMCHidden]
        public long?[] gridListarProcessosSeletivosConvocacao { get; set; }


        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCStep(1, 0)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string MensagemInfoAssociarProcessoSeletivoConvocacao { get { return UIResource.MensagemInfoAssociarProcessoSeletivoConvocacao; } }

        public List<SMCTreeViewNode<ProcessoSeletivoListarViewModel>> ProcessosSeletivosTree { get; set; }

        public SMCSelectedValue<long>[] ProcessosSelecionados { get; set; }

        [SMCHidden]
        public long[] SeqsProcessosSeletivos { get; set; }

        [SMCHidden]
        public long[] SeqsConvocacoes { get; set; }
    }
}