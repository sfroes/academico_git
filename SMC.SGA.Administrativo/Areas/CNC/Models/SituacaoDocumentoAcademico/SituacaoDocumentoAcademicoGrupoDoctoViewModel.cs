using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class SituacaoDocumentoAcademicoGrupoDoctoViewModel
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCHideLabel]
        public GrupoDocumentoAcademico GrupoDocumentoAcademico { get; set; }

    }
}