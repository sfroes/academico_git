using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class TipoSituacaoMatriculaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24)]
        [SMCFilter]
        public long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(100)] 
        [SMCSize(SMCSize.Grid15_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public bool? VinculoAlunoAtivo { get; set; }

    }
}