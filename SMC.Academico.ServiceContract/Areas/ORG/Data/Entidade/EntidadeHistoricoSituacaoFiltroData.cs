using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class EntidadeHistoricoSituacaoFiltroData: SMCPagerFilterData, ISMCMappable
    {
        public long SeqEntidade { get; set; }

        public long SeqTipoEntidade { get; set; }
    }
}
