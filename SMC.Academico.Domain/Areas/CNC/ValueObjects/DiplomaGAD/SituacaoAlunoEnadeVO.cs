using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoAlunoEnadeVO : ISMCMappable
    {
        public string Situacao { get; set; } //enum IRREGULAR, Ingressante / Participante, Dispensado, em razão do calendário trienal, Concluinte / Participante, Dispensado pelo MEC, Dispensado, em razão da natureza do curso, Dispensado, por razão de ordem pessoal, Dispensado, por ato da instituição de ensino, Dispensado, em razão de mobilidade acadêmica no exterior, Estudante não habilitado ao ENADE em razão do calendário do ciclo avaliativo
        public string OutraSituacao { get; set; }
    }
}
