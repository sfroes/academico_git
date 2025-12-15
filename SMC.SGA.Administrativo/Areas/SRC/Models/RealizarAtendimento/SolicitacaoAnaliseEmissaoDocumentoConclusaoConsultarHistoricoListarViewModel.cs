using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoAnaliseEmissaoDocumentoConclusaoConsultarHistoricoListarViewModel : SMCViewModelBase
    {
        [SMCValueEmpty("-")]
        public string Codigo { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCValueEmpty("-")]
        public string Periodo { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCValueEmpty("-")]
        public string Disciplina { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCValueEmpty("-")]
        public string CargaHoraria { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        [SMCValueEmpty("-")]
        public string Nota { get; set; }

        [SMCValueEmpty("-")]
        public string FormaIntegralizacao { get; set; }

        [SMCValueEmpty("-")]
        public string Etiqueta { get; set; }

        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCValueEmpty("-")]
        public string NomeDocente { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCValueEmpty("-")]
        public string TitulacaoDocente { get; set; }
    }
}