using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public override long Seq { get; set; }

        [SMCSortable(true, true)]
        [SMCDescription]
        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        public string Descricao { get; set; }

        [SMCSortable(true, false, "TipoProcessoSeletivo.Descricao")]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public string TipoProcessoSeletivo { get; set; }

        [SMCSortable(true, false, "NiveisEnsino.NivelEnsino.Descricao")]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public List<string> NiveisEnsino { get; set; }

        [SMCIgnoreProp]
        public long SeqCampanha { get; set; }

        [SMCHidden]
        public bool IngressoDireto { get; set; }
    }
}