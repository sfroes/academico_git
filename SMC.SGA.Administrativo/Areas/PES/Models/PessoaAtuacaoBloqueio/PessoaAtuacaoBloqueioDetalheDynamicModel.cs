using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioDetalheDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid5_24)]
        public long SeqPessoaAtuacaoBloqueio { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoTipoBloqueio { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        [SMCSize(SMCSize.Grid6_24)]
        public string DescricaoMotivoBloqueio { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCSize(SMCSize.Grid6_24)]
        public string Descricao { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSize(SMCSize.Grid2_24)]
        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        [SMCSize(SMCSize.Grid2_24)]
        public DateTime DataBloqueio { get; set; }

        [SMCHidden]
        public FormaBloqueio FormaDesbloqueioMotivo { get; set; }

        [SMCHidden]
        public TipoDesbloqueio TipoDesbloqueio { get; set; }

        [SMCHidden]
        public bool HabilitaBotaoDesbloqueio { get; set; }
    }
}