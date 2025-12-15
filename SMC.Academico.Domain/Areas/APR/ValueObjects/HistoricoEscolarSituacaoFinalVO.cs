using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    /// <summary>
    /// O objetivo desta classe é reunir os dados necessários para que seja possível efetuar os cálculos
    /// necessários para verificar se um aluno foi aprovado ou reprovado.
    /// </summary>
    public class HistoricoEscolarSituacaoFinalVO : ISMCMappable
    {
		/// <summary>
		/// Sequencial da Divisão do Componente para recuperar as configurações utilizadas na apuração da nota.
		/// Caso passe este sequencial, é necessário passar apenas a nota, item de escala de apuração e número de faltas.
		/// </summary>
		public long? SeqDivisaoComponente { get; set; }

		public long? SeqMatrizCurricular { get; set; }

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
        /// Indica se o critério de aprovação do componente cursado permite não lançar nota.
        /// </summary>
        public bool IndicadorPermiteAlunoSemNota { get; set; }

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

        /// <summary>
        /// Indicador de que foi marcado o check de sem nota.
        /// </summary>
        public bool SemNota { get; set; }

		/// <summary>
		/// Tipo de arredondamento da frequência
		/// </summary>
		public TipoArredondamento? TipoArredondamento { get; set; }

        public short? SomaFaltasApuracao { get; set; }

        public string NomeAluno { get; set; }

        public short CargaHorariaExecutada { get; set; }

        public short CargaHorariaGrade { get; set; }
    }
}