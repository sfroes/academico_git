using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TermoAdesaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string Titulo { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string ServicoDescricao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string TipoVinculoAlunoDescricao { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        public string Ativo { get; set; }
    }
}