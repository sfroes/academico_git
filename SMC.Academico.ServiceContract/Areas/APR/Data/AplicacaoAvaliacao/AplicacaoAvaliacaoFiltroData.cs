using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class AplicacaoAvaliacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }

        public TipoAvaliacao? TipoAvaliacao { get; set; }
    }
}
