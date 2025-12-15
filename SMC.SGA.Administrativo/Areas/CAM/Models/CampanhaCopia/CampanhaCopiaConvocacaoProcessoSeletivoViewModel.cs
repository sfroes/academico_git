using SMC.Framework.UI.Mvc;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCopiaConvocacaoProcessoSeletivoViewModel : SMCViewModelBase
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<CampanhaCopiaConvocacaoProcessoSeletivoItemViewModel> Convocacoes { get; set; }
    }
}