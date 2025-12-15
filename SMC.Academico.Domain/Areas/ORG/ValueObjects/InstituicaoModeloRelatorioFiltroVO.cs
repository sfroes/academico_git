using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoModeloRelatorioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public ModeloRelatorio? ModeloRelatorio { get; set; }

    }
}
