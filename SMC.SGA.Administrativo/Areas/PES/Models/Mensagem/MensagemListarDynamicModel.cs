using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class MensagemListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSize(SMCSize.Grid4_24)]
        public CategoriaMensagem CategoriaMensagem { get; set; }

        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        [SMCSize(SMCSize.Grid4_24)]        
        public string DescricaoTipoMensagem { get; set; }

        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        [SMCSize(SMCSize.Grid10_24)] 
        public string Mensagem { get; set; }

        [SMCHidden]
        public bool CadastroManual { get; set; }

        [SMCHidden]
        public string MensagemExcluir { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string PeriodoVigencia { get; set; }

        [SMCCssClass("smc-size-md-3 smc-size-xs-3 smc-size-sm-3 smc-size-lg-3")]
        public string DataUsuarioInclusao { get; set; }

    }
}