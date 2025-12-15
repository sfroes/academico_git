using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoPessoasListarViewModel :  SMCPagerViewModel
    {
        [SMCKey]
        [SMCIgnoreProp]
        [SMCCssClass("smc-size-xs-1 smc-size-sm-1 smc-size-md-1 smc-size-lg-1")]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqNotificacaoEmailDestinatario { get; set; }

        [SMCCssClass("smc-size-xs-1 smc-size-sm-1 smc-size-md-1 smc-size-lg-1")]
        public long NumeroRegistroAcademico { get; set; }

        [SMCCssClass("smc-size-xs-6 smc-size-sm-6 smc-size-md-6 smc-size-lg-6")]
        public string Nome { get; set; }

        [SMCCssClass("smc-size-xs-4 smc-size-sm-4 smc-size-md-4 smc-size-lg-4")]
        public string SituacaoMatricula { get; set; }

        [SMCCssClass("smc-size-xs-3 smc-size-sm-3 smc-size-md-3 smc-size-lg-3")]
        public string Vinculo { get; set; }

        [SMCCssClass("smc-size-xs-8 smc-size-sm-8 smc-size-md-8 smc-size-lg-8")]
        public string DadosVinculo { get; set; }

        [SMCCssClass("smc-size-xs-2 smc-size-sm-2 smc-size-md-2 smc-size-lg-2")]
        public string Turma { get; set; }

        [SMCCssClass("smc-size-xs-14 smc-size-sm-14 smc-size-md-14 smc-size-lg-14")]
        public string Entidade { get; set; }

        [SMCCssClass("smc-size-xs-14 smc-size-sm-14 smc-size-md-14 smc-size-lg-14")]
        public string Email { get; set; }

        public bool PermiteVisualizarNotificacao { get;set; }
    }
}