using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Util;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public long? SeqOrigemMaterial { get; set; }

        public long? SeqHistoricoConfiguracaoGradeAtual { get; set; }

        public long NumeroDivisaoComponente { get; set; }

        public long SeqTurma { get; set; }

        public short NumeroGrupo { get; set; }

        public short? NumeroGrupoValidado { get; set; }

        public short QuantidadeVagas { get; set; }

        public long? SeqLocalidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public short? QuantidadeVagasReservadas { get; set; }

        public int QuantidadeVagasDisponiveis { get { return QuantidadeVagas - QuantidadeVagasOcupadas.GetValueOrDefault() - QuantidadeVagasReservadas.GetValueOrDefault(); } }

        public string InformacoesAdicionais { get; set; }

        public OrigemAvaliacaoVO OrigemAvaliacao { get; set; }

        public bool DiarioFechado { get; set; }

        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }
        
        public bool? PermiteLancamentoFrequencia { get; set; }

        public string Situacao { get; set; }

        public MotivoSituacaoMatricula? Motivo { get; set; }

        public bool DivisaoTurmaPossuiConfiguracaoesGrade { get; set; }

        public bool GerarOrientacao { get; set; }
        public short? CargaHoraria { get; set; }
        public string DescricaoComponenteCurricularOrganizacao { get; set; }
        public string TipoDivisaoDescricao { get; set; }
        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string TurmaCodigoFormatado { get; set; }

        public string DescricaoFormatada
        {
            get
            {
                var result = $"{NumeroDivisaoComponente}";

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

        public string DivisaoTurmaRelatorioDescricao
        {
            get
            {
                var result = $"{TurmaCodigoFormatado}.{NumeroDivisaoComponente}.{NumeroGrupo.ToString().PadLeft(3, '0')}";

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

        public bool? LimparNumeroGrupoEdicao { get; set; }
    }
}