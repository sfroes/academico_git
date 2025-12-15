using SMC.Academico.UI.Mvc.Areas.SRC.Lookups;
using SMC.SGA.Administrativo.Areas.SRC.Views.GrupoEscalonamento.App_LocalResources;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoAssociarSolicitacaoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCParameter]
        [SMCHidden]
        public long SeqGrupoEscalonamento { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa smc-sga-mensagem-multiplas-linhas")]
        [SMCDisplay]
        [SMCHideLabel]
        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        public string MensageInformativaAssociacao => UIResource.MensagemInformativaAssociacao;

        [SMCDependency(nameof(SeqProcesso))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        [SolicitacaoServicoLookup]        
        public SolicitacaoServicoLookupViewModel SeqSolicitacaoServico { get; set; }
    }
}