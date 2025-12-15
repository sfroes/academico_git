using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpAutorizacaoViewModel : SMCViewModelBase
    {
        [SMCStep(2)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCReadOnly]
        public DateTime? DataAutorizacao { get; set; }

        [SMCStep(2)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCSelect]
        [SMCRequired]
        [SMCReadOnly]
        public TipoAutorizacao? TipoAutorizacao { get; set; }

        [SMCStep(2)]
        [SMCSize(SMCSize.Grid11_24)]
        [SMCReadOnly]
        public string CodigoAutorizacao { get; set; }
    }
}