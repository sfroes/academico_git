using SMC.Academico.UI.Mvc.Areas.CAM.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CAM.Views.ProcessoSeletivoOferta.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoOfertaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqProcessoSeletivo { get; set; }

        [SMCHidden]
        public long SeqCampanha { get; set; }

        [SMCHidden]
        public long SeqEntidadeResponsavel { get; set; }

        [SMCDependency(nameof(SeqCampanha))]
        [SMCDependency(nameof(SeqEntidadeResponsavel))]
        [SMCDependency(nameof(SeqProcessoSeletivo))]
        [SMCSize(SMCSize.Grid24_24)]
        [CampanhaOfertaLookup]
        public List<SMCLookupViewModel> Ofertas { get; set; }

        public List<ConvocacaoViewModel> Convocacoes { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensagemInfoAssociarConvocacao { get { return UIResource.MensagemInfoAssociarConvocacao; } }

    }
}