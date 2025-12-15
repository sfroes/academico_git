using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data.DivisaoComponente
{
	public class ConfiguracaoDivisaoComponenteData : ISMCMappable
	{
		public short? NotaMaxima { get; set; }
		public bool ApuracaoNota { get; set; }

		public bool ApuracaoEscala { get; set; }
		public bool ApuracaoFrequencia { get; set; }

		public long? SeqEscalaApuracao { get; set; }

		public short? PercentualFrequenciaAprovado { get; set; }
		public short? PercentualNotaAprovado { get; set; }
		public bool PermiteAlunoSemNota { get; set; }
	}
}