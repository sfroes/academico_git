using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class NotificacaoSolicitacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqSolicitacaoServico { get; set; }
    }
}