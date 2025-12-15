using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoTermoIntercambioViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoNivelTipoVinculoAluno { get; set; }
        
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid5_24)]
        [SMCSelect("TiposTermosIntercambios")]
        public long SeqTipoTermoIntercambio { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        public bool? ConcedeFormacao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        [SMCConditionalReadonly(nameof(ConcedeFormacao), SMCConditionalOperation.NotEqual, null)]
        [SMCConditional("smc.sga.instituicaoNivelTipoVinculoAluno.fieldValue", nameof(ConcedeFormacao), SMCConditionalOperation.Equals, true)]
        public bool? ExigePeriodoIntercambioTermo { get; set; }
 
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid4_24)]
        [SMCRadioButtonList]
        public bool? PermiteIngresso { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid5_24)]
        [SMCRadioButtonList]
        public bool? PermiteSaidaIntercambio { get; set; }
    }
}