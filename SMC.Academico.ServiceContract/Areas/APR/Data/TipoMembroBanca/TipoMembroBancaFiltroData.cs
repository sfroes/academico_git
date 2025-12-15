using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class TipoMembroBancaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }
    }
}