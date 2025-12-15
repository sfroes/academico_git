using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Data
{
    public class PublicacaoBdpPalavraChaveData : ISMCMappable
    {
        public long Seq { get; set; }

        public string PalavraChave { get; set; }
    }
}
