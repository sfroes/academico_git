using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class AssuntoComponeteMatrizCabecalhoVO : ISMCMappable
    {
        public string DescricaoMatriz { get; set; }
        public string DescricaoConfiguracaoCompoente { get; set; }
    }
}
