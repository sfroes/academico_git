using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class DiarioTurmaAlunoData : ISMCMappable
    {
        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoCursoOfertaLocalidadeTurno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeAluno { get; set; }

        public short? Nota { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }

        public short? Faltas { get; set; }

        public string DescricaoSituacaoHistoricoEscolar { get; set; }

        public bool IndicadorApuracaoFrequencia { get; set; }

        public bool IndicadorApuracaoNota { get; set; }

        public long? SeqEscalaAprovacao { get; set; }

        public short? NotaMaxima { get; set; }

        public short? PercentualMinimoFrequencia { get; set; }

        public short? PercentualMinimoNota { get; set; }

        public bool AlunoDI { get; set; }

        public bool AlunoFormado { get; set; }

        public long? SeqEscalaApuracao { get; set; }

    }
}