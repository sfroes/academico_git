using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Models
{
    public class AtoNormativoVisualizarViewModel: SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCMapProperty("AssuntoNormativo.Descricao")]
        [SMCSortable(true, true, "AssuntoNormativo.Descricao")]
        public string DescricaoAssuntoNormativo { get; set; }

        [SMCMapProperty("TipoAtoNormativo.Descricao")]
        [SMCSortable(true, false, "TipoAtoNormativo.Descricao")]
        public string DescricaoTipoAtoNormativo { get; set; }

        //[SMCSortable(true, false)]
        public string NumeroDocumento { get; set; }

        [SMCMapProperty("AtoNormativo.DataDocumento")]
        [SMCSortable(true, false, "AtoNormativo.DataDocumento")]
        public DateTime DataDocumento { get; set; }

        //[SMCSortable(true, false)]
        public string DescricaoGrauAcademico { get; set; }
    }
}
