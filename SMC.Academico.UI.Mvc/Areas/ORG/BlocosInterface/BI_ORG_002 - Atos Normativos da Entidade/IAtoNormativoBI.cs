using SMC.Academico.UI.Mvc.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.InterfaceBlocks
{
    public interface IAtoNormativoBI
    {
        bool AtivaAbaAtoNormativo { get; set; }

        bool HabilitaColunaGrauAcademico { get; set; }

        List<AtoNormativoVisualizarViewModel> AtoNormativo { get; set; }
    }
}
