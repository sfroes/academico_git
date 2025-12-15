using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.PES.Enums;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
	public class DivisaoTurmaVagasVO
	{
		/// <summary>
		/// Quantidade de vagas da divisão da turma
		/// </summary>
		public short QuantidadeVagas { get; set; }

		/// <summary>
		/// Quantidade de vagas ocupadas da divisão da turma
		/// </summary>
		public short QuantidadeVagasOcupadas { get; set; }

		/// <summary>
		/// Quantidade de vagas reservadas da divisão da turma
		/// </summary>
		public short QuantidadeVagasReservadas { get; set; }

		/// <summary>
		/// Existe ou não vagas disponíveis na divisão da turma
		/// </summary>
		public bool VagasLivres { get; set; }

		/// <summary>
		/// Motivo de não existir vagas disponíveis na divisão da turma
		/// </summary>
		public MotivoSituacaoMatricula? MotivoSemVaga { get; set; }

	}
}