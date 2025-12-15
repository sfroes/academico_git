using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ProcessoUnidadeResponsavelDetalheData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        public string NomeEntidadeResponsavel { get; set; }

        public long SeqProcesso { get; set; }

        public TipoUnidadeResponsavel TipoUnidadeResponsavel { get; set; }
    }
}