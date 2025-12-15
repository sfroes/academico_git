using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class JustificativaSolicitacaoServicoFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public bool? Ativo { get; set; }

        public long? SeqServico { get; set; }
    }
}