using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DiarioTurmaProfessorData : ISMCMappable
    {
        public long SeqColaborador { get; set; }

        public string NomeProfessor { get; set; }

		public bool ProfessorTurma { get; set; }
	}
}
