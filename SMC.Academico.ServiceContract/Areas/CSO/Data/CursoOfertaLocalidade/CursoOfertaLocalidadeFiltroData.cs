using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoOfertaLocalidadeFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCurso { get; set; }

        public string NomeCurso { get; set; }

        public string DescricaoOferta { get; set; }

        public List<long> SeqsEntidadesResponsaveis { get; set; }

        public long? SeqEntidadeResponsavel { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<long> SeqsNiveisEnsino { get; set; }

        public long? SeqSituacaoAtual { get; set; }

        public long? SeqTipoFormacaoEspecifica { get; set; }

        public long? SeqLocalidade { get; set; }

        public long? SeqModalidade { get; set; }

        public string TokenTipoEntidade { get; set; }

        public long? Seq { get; set; }

		/// <summary>
		/// Desativa todos os filtros exceto o filtro de instituição
		/// </summary>
		public bool IgnorarFiltros { get; set; }
    }
}