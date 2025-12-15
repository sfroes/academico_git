using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class CalculoConclusaoCursoAlunoVO : ISMCMappable
    {
        public decimal PercentualConclusaoGeral { get; set; }

        public List<PercentualConclusaoGrupoVO> PercentualGrupo { get; set; }
    }

    public class PercentualConclusaoGrupoVO : ISMCMappable
    {
        public long SeqGrupoCurricular { get; set; }

        public long? SeqGrupoCurricularSuperior { get; set; }

        public decimal? PercentualConclusaoGrupo { get; set; }
    }
}