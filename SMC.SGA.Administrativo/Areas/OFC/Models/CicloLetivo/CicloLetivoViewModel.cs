using SMC.Academico.UI.Mvc.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class CicloLetivoViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid4_24)]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid6_24)]
        [LookupNivelEnsino]
        public SGALookupViewModel SeqNivelEnsino { get; set; }
        
        [SMCRequired]
        [SMCSelect("RegimesLetivos")]
        [SMCSize(SMCSize.Grid6_24)]
        public long RegimeLetivoSelecionado { get; set; }

        public List<SMCSelectItem> RegimesLetivos { get; set; }

        [SMCMask("0000")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public int? AnoCiclo { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public int? NumeroCiclo { get; set; }
    }
}