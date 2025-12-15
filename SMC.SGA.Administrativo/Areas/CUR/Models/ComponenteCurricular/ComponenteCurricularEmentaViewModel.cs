using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularEmentaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCMultiline]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCUnique]
        public string Ementa { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime DataInicio { get; set; }

        [SMCMinDate(nameof(DataInicio))]
        [SMCSize(SMCSize.Grid4_24)]
        public DateTime? DataFim { get; set; }
    }
}