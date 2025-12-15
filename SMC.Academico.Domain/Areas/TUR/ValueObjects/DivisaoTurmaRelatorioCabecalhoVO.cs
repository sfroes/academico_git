using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DivisaoTurmaRelatorioCabecalhoVO : ISMCMappable
    {
        public long SeqDivisaoTurma { get; set; }

        public string NomeCursoOfertaLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public string DescricaoConfiguracaoComponente { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

        public short? CargaHorariaComponenteCurricular { get; set; }

        public short? CreditoComponenteCurricular { get; set; }

        public string CodigoDivisaoFormatado { get; set; }

        public string DescricaoComponenteCurricularOrganizacao { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public short? CargaHoraria { get; set; }

        public FormatoCargaHoraria? FormatoCargaHoraria { get; set; }

        public string Curso
        {
            get
            {
                return string.Join(" - ", new object[] { NomeCursoOfertaLocalidade, DescricaoTurno });
            }
        }

        public string Disciplina
        {
            get
            {
                string retorno = string.Empty;

                retorno = DescricaoConfiguracaoComponente;

                if (!string.IsNullOrEmpty(DescricaoComponenteCurricularAssunto))
                {
                    retorno += $": {DescricaoComponenteCurricularAssunto}";
                }

                if (CargaHorariaComponenteCurricular.GetValueOrDefault() > 0)
                {
                    string labelHoraAula = null;

                    if (FormatoCargaHoraria == Common.Areas.CUR.Enums.FormatoCargaHoraria.Hora)
                    {
                        labelHoraAula = CargaHorariaComponenteCurricular == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
                    }
                    else
                    {
                        labelHoraAula = CargaHorariaComponenteCurricular == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
                    }

                    retorno += $" - {CargaHorariaComponenteCurricular} {labelHoraAula}";
                }

                if (CreditoComponenteCurricular.GetValueOrDefault() > 0)
                {
                    string labelCredito = CreditoComponenteCurricular == 1 ? MessagesResource.Label_Credito : MessagesResource.Label_Creditos;

                    retorno += $" - {CreditoComponenteCurricular} {labelCredito}";
                }

                retorno += Environment.NewLine;
                retorno += CodigoDivisaoFormatado;

                if (!string.IsNullOrEmpty(DescricaoComponenteCurricularOrganizacao))
                {
                    retorno += $" - {DescricaoComponenteCurricularOrganizacao}";
                }

                retorno += $" - {TipoDivisaoDescricao}";

                if (CargaHoraria.GetValueOrDefault() > 0)
                {
                    string labelHoraAula = null;

                    if (FormatoCargaHoraria == Common.Areas.CUR.Enums.FormatoCargaHoraria.Hora)
                    {
                        labelHoraAula = CargaHoraria == 1 ? MessagesResource.Label_Hora : MessagesResource.Label_Horas;
                    }
                    else
                    {
                        labelHoraAula = CargaHoraria == 1 ? MessagesResource.Label_HoraAula : MessagesResource.Label_HorasAula;
                    }

                    retorno += $" - {CargaHoraria} {labelHoraAula}";
                }

                return retorno;
            }
        }
    }
}
