using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class DadosAtoNormativoEntidadeVO : ISMCMappable
    {
        public DadosAtoNormativoVO Credenciamento { get; set; }
        public DadosAtoNormativoVO Recredenciamento { get; set; }
        public DadosAtoNormativoVO RenovacaoDeRecredenciamento { get; set; }
    }
}
