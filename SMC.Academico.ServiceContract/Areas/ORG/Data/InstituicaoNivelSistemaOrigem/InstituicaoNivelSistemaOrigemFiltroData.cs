using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelSistemaOrigemFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqInstituicaoNivel { get; set; }

        public string TokenSistemaOrigemGAD { get; set; }

        public UsoSistemaOrigem? UsoSistemaOrigem { get; set; }
    }
}