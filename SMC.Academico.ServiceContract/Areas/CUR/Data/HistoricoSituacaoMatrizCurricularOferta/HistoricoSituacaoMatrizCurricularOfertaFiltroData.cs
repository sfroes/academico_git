using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class HistoricoSituacaoMatrizCurricularOfertaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqMatrizCurricularOferta { get; set; }
        
    }
}
