using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORT.ValueObjects
{
	public class TrabalhoAcademicoDivisaoComponenteVO : ISMCMappable
	{
		public long Seq { get; set; }

		public long SeqDivisaoComponente { get; set; }

		public long? SeqOrigemAvaliacao { get; set; }

        public string DescricaoDivisaoComponente { get; set; }
    }
}