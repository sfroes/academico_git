using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class ConfiguracaoAvaliacaoPpaTurmaCabecalhoData : ISMCMappable
    {
        public long Seq { get; set; }

        public string DescricaoConfiguracaoAvaliacaoPpa { get; set; }

        public string EntidadeResponsavel { get; set; }

        public string TipoAvaliacao { get; set; }
    }
}
