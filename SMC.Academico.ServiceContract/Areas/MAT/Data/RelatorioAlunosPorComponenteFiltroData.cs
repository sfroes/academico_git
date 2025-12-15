using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class RelatorioAlunosPorComponenteFiltroData : ISMCMappable
    {
        public long SeqCicloLetivo { get; set; }
        public long SeqCursoOfertaLocalidade { get; set; }
        public long? SeqTurno { get; set; }
        public long? SeqTurma { get; set; }
        public long? SeqConfiguracaoComponente { get; set; }
        public bool? ExibirSolicitanteMatrículaNaoFinalizada { get; set; }
    }
}