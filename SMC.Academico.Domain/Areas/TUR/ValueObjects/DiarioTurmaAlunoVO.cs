using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class DiarioTurmaAlunoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqAlunoHistoricoCicloLetivo { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public decimal? Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public bool? Aprovado { get; set; }

        public short? Faltas { get; set; }

        public short? SomaFaltasApuracao { get; set; }

        public string DescricaoSituacaoHistoricoEscolar { get; set; }

        public bool IndicadorApuracaoFrequencia { get; set; }

        public bool IndicadorApuracaoNota { get; set; }

        public bool IndicadorPermiteAlunoSemNota { get; set; }

		public TipoArredondamento? TipoArredondamento { get; set; }

		public long? SeqEscalaApuracao { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public short? NotaMaxima { get; set; }

        [SMCMapForceFromTo]
        [SMCMapProperty("PercentualFrequencia")]
        public short? PercentualMinimoFrequencia { get; set; }

        public short? PercentualMinimoNota { get; set; }

        [SMCMapForceFromTo]
		public short? CargaHoraria { get; set; }

		[SMCMapForceFromTo]
		public short? CargaHorariaRealizada { get; set; }

		public long SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public short? Credito { get; set; }

        public bool Optativa { get; set; }

        public bool TurmaOrientacao { get; set; }

        public long SeqPlanoEstudo { get; set; }

        public bool AlunoDI { get; set; }

        public bool AlunoFormado { get; set; }

        public bool AlunoDispensado { get; set; }

        public bool AlunoAprovado { get; set; }

        public long? SeqMatrizCurricular { get; set; }

		public long SeqDivisaoComponente { get; set; }

        public string Observacao { get; set; }

        public short CargaHorariaExecutada { get; set; }

        public short CargaHorariaGrade { get; set; }
    }
}