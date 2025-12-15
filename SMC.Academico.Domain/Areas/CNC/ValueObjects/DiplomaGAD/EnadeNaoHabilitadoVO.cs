using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class EnadeNaoHabilitadoVO : ISMCMappable
    {
        public string Motivo { get; set; } //enum Estudante não habilitado ao Enade em razão do calendário do ciclo avaliativo, Estudante não habilitado ao Enade em razão da natureza do projeto pedagógico do curso
        public string OutroMotivo { get; set; }
        public string Condicao { get; set; } //enum Ingressante, Concluinte
        public string Edicao { get; set; }
    }
}
