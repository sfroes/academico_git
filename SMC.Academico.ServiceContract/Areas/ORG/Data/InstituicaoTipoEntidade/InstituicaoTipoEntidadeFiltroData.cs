using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Service.ORG.Data
{
    public class InstituicaoTipoEntidadeFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqTipoEntidade { get; set; }
    }
}