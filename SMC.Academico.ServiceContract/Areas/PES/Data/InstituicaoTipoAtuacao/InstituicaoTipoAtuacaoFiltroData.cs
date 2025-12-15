using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class InstituicaoTipoAtuacaoFiltroData : SMCPagerFilterData, ISMCMappable
    {
       public long? SeqInstituicaoEnsino { get; set; }
    }
}
