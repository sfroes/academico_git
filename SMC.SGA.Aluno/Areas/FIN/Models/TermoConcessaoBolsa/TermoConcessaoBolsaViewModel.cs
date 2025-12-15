using SMC.Framework.UI.Mvc;
using SMC.Framework.Util;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using System;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Aluno.Areas.FIN.Views.TermoConcessaoBolsa.App_LocalResources;
using System.Collections.Generic;
using SMC.Framework;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class TermoConcessaoBolsaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public bool ExibirMensagemAlerta { get; set; }

        [SMCConditionalDisplay(nameof(ExibirMensagemAlerta), SMCConditionalOperation.Equals, true)]
        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-alerta")]
        [SMCDisplay]
        [SMCHideLabel]
        public string MensagemAlerta { get; set; } = UIResource.MensagemAlerta;

        public List<TermoConcessaoBolsaListaViewModel> Beneficios { get; set; }

    }
}