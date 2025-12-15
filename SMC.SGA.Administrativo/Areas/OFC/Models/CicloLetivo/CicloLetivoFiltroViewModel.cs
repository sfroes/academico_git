using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class CicloLetivoFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }


        [SMCFilter]
        [SMCSize(SMCSize.Grid6_24)]
        [LookupNivelEnsino]
        public SGALookupViewModel SeqNivelEnsino { get; set; }

        [SMCFilter]
        [SMCMask("0000")]
        [SMCSize(SMCSize.Grid2_24)]
        public int? AnoCiclo { get; set; }
    }
}