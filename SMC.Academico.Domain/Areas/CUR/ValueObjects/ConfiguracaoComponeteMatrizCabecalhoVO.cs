using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ConfiguracaoComponeteMatrizCabecalhoVO : ISMCMappable
    {
        public string DescricaoMatriz { get; set; }
        public int TotalComponentes { get; set; }
        public int TotalComponentesComConfiguracao { get; set; }
    }
}
