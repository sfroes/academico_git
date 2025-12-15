using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DiarioTurmaProfessorVO : ISMCMappable
    {
        public long SeqColaborador { get; set; }

        public string NomeProfessor { get; set; }

        public bool ProfessorTurma { get; set; }
    }
}
