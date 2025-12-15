using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ItemRelatorioDisciplinasCursadasComponenteCurricularData : ISMCMappable
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

        public short? Nota { get; set; }
        public short? PercentualFrequencia { get; set; }

        public string DescricaoEscalaApuracaoItem { get; set; }
    }
}