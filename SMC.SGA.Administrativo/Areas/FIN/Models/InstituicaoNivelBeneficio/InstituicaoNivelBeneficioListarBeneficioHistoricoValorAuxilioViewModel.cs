using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class InstituicaoNivelBeneficioListarBeneficioHistoricoValorAuxilioViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelBeneficio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public decimal ValorAuxilio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public DateTime DataInicioValidade { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public DateTime? DataFimValidade { get; set; }
    }
}