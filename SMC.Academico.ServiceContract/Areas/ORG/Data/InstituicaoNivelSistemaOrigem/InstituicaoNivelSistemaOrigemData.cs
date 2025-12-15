using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoNivelSistemaOrigemData : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqInstituicaoNivel { get; set; }
        public string TokenSistemaOrigemGAD { get; set; }
        public string DescricaoSistemaOrigemGAD { get; set; }
        public string DescricaoInstituicaoNivel { get; set; }
        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }
    }
}