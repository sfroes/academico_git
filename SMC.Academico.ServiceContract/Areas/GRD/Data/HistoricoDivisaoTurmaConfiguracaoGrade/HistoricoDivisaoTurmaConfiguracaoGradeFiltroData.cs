using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string DescricaoTurma { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }
    }
}
