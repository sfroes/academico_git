using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ItemRelatorioDisciplinasCursadasComponenteCurricularVO : ISMCMappable
    {
        public long? SeqAluno { get; set; }

        public long? SeqHistoricoEscolar { get; set; }

        public string DescricaoCicloLetivo { get; set; }

		public string DescricaoComponenteCurricular { get; set; }

		public string DescricaoComponenteCurricularAssunto { get; set; }

		public string ListaProfessores { get; set; }

        public int? CargaHorariaAula { get; set; }

        public int? CargaHorariaRelogio { get; set; }

        public int? Credito { get; set; }

        public decimal? Nota { get; set; }
        
        public decimal? PercentualFrequencia { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }
    }
}