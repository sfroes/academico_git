using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ComponentesCreditosVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqComponente { get; set; }

        public TipoConclusao TipoConclusao { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public string SemAnoCicloLetivo { get; set; }

        public string CodigoComponente { get; set; }

        public string DescricaoComponente { get; set; }

        public string DescricaoComponenteAssunto { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string ListaProfessores { get; set; }

        public string Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? CargaHoraria { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public short? CargaHorariaExecutada { get; set; }

        public short? Credito { get; set; }

        public short? Faltas { get; set; }

        public string PercentualFrequencia { get; set; }

        public SituacaoHistoricoEscolar? SituacaoHistoricoEscolar { get; set; }

        public long? SeqSolicitacaoServico { get; set; }

        public long? SeqGrupoCurricular { get; set; }

        public short? QuantidadeItens { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

        public long? SeqPlanoEstudo { get; set; }

        public bool? CicloAtual { get; set; }

        public DateTime? DataExame { get; set; }

        public string Observacao { get; set; }

        public bool PermitirCredito { get; set; }

        public string Ano => SemAnoCicloLetivo.Substring(5, 4);

        public string Semestre => SemAnoCicloLetivo.Substring(0, 1);

        public TipoArredondamento? TipoArredondamento { get; set; }
    }
}