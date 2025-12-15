using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class InstituicaoNivelBeneficioListarConfiguracaBeneficioViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public TipoDeducao TipoDeducao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid8_24)]
        public FormaDeducao FormaDeducao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public decimal? ValorDeducao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime DataInicioValidade { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime? DataFimValidade { get; set; }
    }
}