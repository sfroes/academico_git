using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
	public class TurmaDivisoesVO : ISMCMappable
	{
		public long? Seq { get; set; }

		public long SeqConfiguracaoComponente { get; set; }

		public long SeqTurma { get; set; }

		public long SeqDivisaoComponente { get; set; }

		public long? SeqComponenteCurricular { get; set; }

		public string DivisaoComponenteDescricao { get; set; }

		public bool PermitirGrupo { get; set; }

		public bool? CriterioAprovacaoFrequencia { get; set; }

		public bool? TurmaDiarioAberto { get; set; }

		public List<TurmaDivisoesDetailVO> DivisoesComponentes { get; set; }

		public List<TurmaDivisoesDetailVO> DivisoesComponentesDisplay { get; set; }

		public string TipoDivisaoDescricao { get; set; }

		public short Numero { get; set; }

		public short? CargaHoraria { get; set; }

		public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

		public bool GerarOrientacao { get; set; }

		public long? SeqTipoOrientacao { get; set; }

		public string DescricaoComponenteCurricularOrganizacao { get; set; }

		public string DescricaoFormatada
		{
			get
			{
				var result = $"{Numero}";

				if (!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
					result += $" - {DescricaoComponenteCurricularOrganizacao}";

				if (!string.IsNullOrEmpty(TipoDivisaoDescricao))
					result += $" - {TipoDivisaoDescricao}";

				if (CargaHoraria.HasValue)
					result += $" - {CargaHoraria}";

				if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
					result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria.Value)}";

				return result;
			}
		}

		public string DescricaoFormatadaSemNumero
		{
			get
			{
				var result = string.Empty;

				if (!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
					result += $"{DescricaoComponenteCurricularOrganizacao}";

				if (!string.IsNullOrEmpty(TipoDivisaoDescricao))
					result += $"{(string.IsNullOrEmpty(result) ? "" : " - ")}{TipoDivisaoDescricao}";

				if (CargaHoraria.HasValue)
					result += $"{(string.IsNullOrEmpty(result) ? "" : " - ")}{CargaHoraria}";

				if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
					result += $"{(string.IsNullOrEmpty(result) ? "" : " ")}{SMCEnumHelper.GetDescription(FormatoCargaHoraria.Value)}";

				return result;
			}
		}

		public string TurmaCodigoFormatado { get; set; }

		public long NumeroDivisaoComponente { get; set; }

		public short NumeroGrupo { get; set; }

		public string DivisaoTurmaRelatorioDescricao
		{
			get
			{
				var result = $"{TurmaCodigoFormatado}.{NumeroDivisaoComponente}.{NumeroGrupo.ToString().PadLeft(3, '0')}";

				if(!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
					result += $" - {DescricaoComponenteCurricularOrganizacao}";

				if (!string.IsNullOrEmpty(TipoDivisaoDescricao))
					result += $" - {TipoDivisaoDescricao}";

				if (CargaHoraria.HasValue)
					result += $" - {CargaHoraria}";

				if (FormatoCargaHoraria.HasValue && FormatoCargaHoraria.Value != Common.Areas.CUR.Enums.FormatoCargaHoraria.Nenhum)
					result += $" {SMCEnumHelper.GetDescription(FormatoCargaHoraria.Value)}";

				return result;
			}
		}

		public long? SeqOrigemMaterial { get; internal set; }

		public bool TurmaCancelada { get; set; }

		public bool TurmaVigente { get; set; }

		public long SeqOrigemAvaliacao { get; set; }

		public bool DivisaoTurmaPossuiConfiguracaoesGrade { get; set; }

		public string Assunto { get; set; }
	}
}