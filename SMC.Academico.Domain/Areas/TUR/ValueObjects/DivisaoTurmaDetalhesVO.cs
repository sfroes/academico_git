using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaDetalhesVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoComponenteCurricular { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqOrigemAvaliacao { get; set; }

        public int TurmaCodigo { get; set; }

        public short TurmaNumero { get; set; }

        public string TurmaCodigoFormatado { get { return $"{TurmaCodigo}.{TurmaNumero}"; } }

        public string TurmaDescricaoFormatado { get; set; }

        public string DescricaoTurmaConfiguracaoComponente { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string NomeCursoOfertaLocalidade { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoTurno { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public short Numero { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public short? Credito { get; set; }

        public short? CargaHorariaComponenteCurricular { get; set; }

        public short? CreditoComponenteCurricular { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

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

        public short NumeroGrupo { get; set; }

        public string GrupoFormatado { get { return $"{TurmaCodigoFormatado}.{Numero.ToString()}.{NumeroGrupo.ToString().PadLeft(3, '0')}"; } }

        public string DescricaoLocalidade { get; set; }

        public string InformacoesAdicionais { get; set; }

        public long QuantidadeVagas { get; set; }

        public TipoOrganizacao? TipoOrganizacao { get; set; }

        public string DescricaoTipoOrganizacao
        {
            get
            {
                if (TipoOrganizacao != null)
                    return SMCEnumHelper.GetDescription(TipoOrganizacao);
                else
                    return string.Empty;
            }
        }

        public List<DivisaoTurmaDetalhesColaboradorVO> Colaboradores { get; set; }

        public bool GeraOrientacao { get; set; }

        public bool DiarioFechado { get; set; }

        public bool Destaque { get; set; }

        public bool? TurmaPossuiAgenda { get; set; }

        public DateTime? DataInicioPeriodoLetivo { get; set; }

        public DateTime? DataFimPeriodoLetivo { get; set; }
    }
}