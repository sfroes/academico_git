using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCParameter]
        [SMCFilter(true, true)]
        public long SeqDivisaoTurma { get; set; }

        public string DescricaoTurma { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }
    }
}