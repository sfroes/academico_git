using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoNivelSistemaOrigemVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqInstituicaoNivel { get; set; }
        public string TokenSistemaOrigemGAD { get; set; }
        public string DescricaoSistemaOrigemGAD { get; set; }
        public string DescricaoInstituicaoNivel { get; set; }
        public UsoSistemaOrigem UsoSistemaOrigem { get; set; }
    }
}