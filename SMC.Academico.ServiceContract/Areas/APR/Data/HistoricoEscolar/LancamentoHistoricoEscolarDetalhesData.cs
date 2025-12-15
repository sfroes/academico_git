using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class LancamentoHistoricoEscolarDetalhesData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public bool IndicadorApuracaoFrequencia { get; set; }

        public bool IndicadorApuracaoNota { get; set; }

        public bool IndicadorPermiteAlunoSemNota { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public long? SeqEscalaApuracaoItem { get; set; }

        public short? NotaMaxima { get; set; }

        public short? PercentualMinimoFrequencia { get; set; }

        public short? PercentualMinimoNota { get; set; }

		public short? CargaHoraria { get; set; }

		public short? CargaHorariaRealizada { get; set; }

		public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public short? Nota { get; set; }

        public bool SemNota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public bool? Aprovado { get; set; }

        public short? Faltas { get; set; }

        public short? SomaFaltasApuracao { get; set; }

        public string DescricaoSituacaoHistoricoEscolar { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public bool AlunoFormado { get; set; }

        public bool AlunoDispensado { get; set; }

        public bool AlunoAprovado { get; set; }

        public TipoArredondamento? TipoArredondamento { get; set; }

        public string Observacao { get; set; }
    }
}