using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarCabecalhoVO : ISMCMappable
    {
        public string NomeAluno { get; set; }

        public long NumeroRegistroAcademico { get; set; }

        public string NomeCursoOferta { get; set; }

        public string NomeLocalidade { get; set; }

        public string DescricaoTurno { get; set; }

        public int? TotalCargaHorariaObrigatoria { get; set; }

        public int? TotalCargaHorariaOptativa { get; set; }

        public int? TotalCreditosObrigatorios { get; set; }

        public int? TotalCreditosOptativos { get; set; }

		public bool ExibirOfertaMatriz { get; set; }

		public string OfertaMatriz { get; set; }

		public string SituacaoOfertaMatriz { get; set; }

		public bool ExibirDisciplinaIsolada { get; set; }

		public string VinculoDisciplinaIsolada { get; set; }
	}
}