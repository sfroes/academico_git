using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
    public class TrabalhoAcademicoAutorListaVO : ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string Nome { get; set; }

        public string NomeSocial { get; set; }

		public string CursoOfertaLocalidade { get; set; }

        public string Turno { get; set; }

        public string CicloLetivo { get; set; }

        public string TipoSituacao { get; set; }

	}
}