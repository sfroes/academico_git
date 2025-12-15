using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class MatrizCurricularOfertaIntegralizacaoFiltroVO : ISMCMappable
    {
        public long Seq { get; set; }

        public List<long> SeqsBeneficios { get; set; }

        public List<long> SeqsCondicoes { get; set; }

        public List<long> SeqsFormacoes { get; set; }
        
        public List<long> SeqsComponentesFiltros { get; set; }

        public List<long> SeqsComponentesDiferentesFiltros { get; set; }

        public string DescricaoComponentesFiltro { get; set; }

		public bool DesconsiderarFiltroCondicoes { get; set; }

		public bool DesconsiderarFiltroBeneficios { get; set; }
    }
}
