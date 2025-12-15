using SMC.Academico.Domain.Areas.ORG.ValueObjects;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoUnidadeVO: EntidadeVO
    {
        public long SeqCurso { get; set; }

        /// <summary>
        /// Sequencial do HierarquiaEntidadeItem que representa a Unidade
        /// </summary>
        public long SeqUnidade { get; set; }
    }
}
