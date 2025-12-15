using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoDiscenteVO : ISMCMappable
    {
        public string PeriodoLetivo { get; set; }
        public string Tipo { get; set; } //enum Abandono, Desistencia, Formado, IntercambioInternacional, IntercambioNacional, Jubilado, Licenca, MatriculadoEmDisciplina, OutraSituacao, Trancamento
        public SituacaoIntercambioVO Intercambio { get; set; }
        public SituacaoFormadoVO Formado { get; set; }
        public string OutraSituacao { get; set; }
    }
}
