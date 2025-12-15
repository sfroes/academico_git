using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
	public class DadosSimuladosVO : ISMCMappable
	{
		public long? SeqCursoOfertaLocalidade { get; set; }

		public long? SeqOrigemFinanceira { get; set; }

		public long? SeqCursoOfertaLocalidadeTurno { get; set; }

		public long? SeqCurso { get; set; }
	}
}