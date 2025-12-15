using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
	/// <summary>
	/// O objetivo desta classe é reunir os dados necessários para que seja possível efetuar os cálculos
	/// necessários para verificar se um aluno foi aprovado ou reprovado.
	/// </summary>
	public class HistoricoEscolarSituacaoFinalViewModel : SMCViewModelBase
	{
		/// <summary>
		/// Nota do aluno
		/// </summary>
		public short? Nota { get; set; }

		/// <summary>
		/// Sequencial da escala de apuração do critério de aprovação do componente cursado pelo aluno
		/// </summary>
		public long? SeqEscalaApuracao { get; set; }

		/// <summary>
		/// Sequencial do item da escala de apuração caso o critério de aprovação do componente seja por escala
		/// </summary>
		public long? SeqEscalaApuracaoItem { get; set; }

		/// <summary>
		/// Número de faltas.
		/// </summary>
		public short? Faltas { get; set; }

		/// <summary>
		/// Indica se o critério de aprovação do componente cursado apura frequência.
		/// </summary>
		public bool IndicadorApuracaoFrequencia { get; set; }

		/// <summary>
		/// Indica se o critério de aprovação do componente cursado apura nota.
		/// </summary>
		public bool IndicadorApuracaoNota { get; set; }

		/// <summary>
		/// Indica se o critério de aprovação do componente cursado apura por escala.
		/// </summary>
		public bool IndicadorApuracaoEscala { get; set; }

		/// <summary>
		/// Percentula mínimo de frequência para ser aprovado no critério de aprovação do componente cursado.
		/// </summary>
		public short? PercentualMinimoFrequencia { get; set; }

		/// <summary>
		/// Percentual mínimo de nota para ser aprovado no critério de aprovação do componente cursado.
		/// </summary>
		public short? PercentualMinimoNota { get; set; }

		/// <summary>
		/// Nota máxima possível no critério de aprovação do componente cursado.
		/// </summary>
		public short? NotaMaxima { get; set; }

		/// <summary>
		/// Carga horária do componente cursado.
		/// </summary>
		public short? CargaHoraria { get; set; }

		public long? SeqDivisaoComponente { get; set; }

		public long? SeqMatrizCurricular { get; set; }

		public TipoArredondamento? TipoArredondamento { get; set; }
	}
}