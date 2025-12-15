using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoModeloRelatorioFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public ModeloRelatorio? ModeloRelatorio { get; set; }
    }
}
