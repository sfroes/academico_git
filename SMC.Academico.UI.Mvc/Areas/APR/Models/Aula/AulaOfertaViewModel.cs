using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AulaOfertaViewModel : SMCViewModelBase
    {
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public List<ApuracaoFrequenciaViewModel> ApuracoesFrequencia { get; set; }
    }
}