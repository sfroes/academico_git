using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class ApuracaoFrequenciaGradeViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
        public long SeqAlunoHistoricoCicloLetivo { get; set; }
        public long SeqEventoAula { get; set; }
        public Frequencia? Frequencia { get; set; }
        public string ObservacaoAbono { get; set; }
    }
}