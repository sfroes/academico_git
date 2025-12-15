using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoServicoLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string NumeroProtocolo { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string GrupoEscalonamentoAtual { get; set; }

        [SMCSortable(false, true, "PessoaAtuacao.DadosPessoais.Nome")]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string Solicitante { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string EtapaAtual { get; set; }

        [SMCCssClass("smc-size-md-9 smc-size-xs-9 smc-size-sm-9 smc-size-lg-9")]
        public string SituacaoAtual { get; set; }        
    }
}