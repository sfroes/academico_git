using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeIdiomaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public Idioma Idioma { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public CampoIdioma CampoIdioma { get; set; }

        [SMCMaxLength(255)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid14_24)]
        public string ValorCampo { get; set; }
    }
}