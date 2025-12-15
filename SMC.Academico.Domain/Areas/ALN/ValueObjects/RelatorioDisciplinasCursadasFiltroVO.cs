using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class RelatorioDisciplinasCursadasFiltroVO : ISMCMappable
    {
        public List<long> SelectedValues { get; set; }
        public TipoDeclaracaoDisciplinaCursada? ExibirNaDeclaracao { get; set; }
        public bool? ExibirEmentasComponentesCurriculares { get; set; }
    }
}