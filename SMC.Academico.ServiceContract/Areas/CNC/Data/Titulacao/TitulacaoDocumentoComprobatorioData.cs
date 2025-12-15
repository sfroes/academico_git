using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class TitulacaoDocumentoComprobatorioData : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqTipoDocumento { get; set; }
    }
}
