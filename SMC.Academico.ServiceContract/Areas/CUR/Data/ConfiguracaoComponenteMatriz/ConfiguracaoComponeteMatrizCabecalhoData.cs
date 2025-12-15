using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class ConfiguracaoComponeteMatrizCabecalhoData : ISMCMappable
    {
        public string DescricaoMatriz { get; set; }
        public int TotalComponentes { get; set; }
        public int TotalComponentesComConfiguracao { get; set; }
    }
}
