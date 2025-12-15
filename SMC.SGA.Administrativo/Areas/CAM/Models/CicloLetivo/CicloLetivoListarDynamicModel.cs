using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCOrder(4)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCSortable(true, true, Framework.SMCSortDirection.Descending)]
        public short Ano { get; set; }

        [SMCOrder(5)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSortable(true, true, Framework.SMCSortDirection.Descending)]
        public short Numero { get; set; }

        [SMCOrder(0)]
        [SMCKey]
        [SMCSortable]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCHidden]
        [SMCInclude("InstituicaoEnsino")]
        [SMCMapProperty("InstituicaoEnsino.SeqUnidadeResponsavelAgd")]
        public long SeqUnidadeResponsavelAgd { get; set; }

        [SMCOrder(2)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCDescription]
        [SMCSortable]
        public string Descricao { get; set; }

        [SMCOrder(3)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCInclude("NiveisEnsino")]
        [SMCMapMethod("RecuperarDescricaoNiveisEnsino")]
        public List<string> DescricaoNiveisEnsino { get; set; }

        [SMCOrder(6)]
        [SMCCssClass("smc-size-md-5 smc-size-xs-5 smc-size-sm-5 smc-size-lg-5")]
        [SMCSortable(true, false, "RegimeLetivo.Descricao")]
        [SMCInclude("RegimeLetivo")]
        [SMCMapProperty("RegimeLetivo.Descricao")]
        public string DescricaoRegimeLetivo { get; set; }
    }
}