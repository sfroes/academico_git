using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class PosicaoConsolidadaFiltroData : ISMCMappable
    {
        public long? SeqProcesso { get; set; }

        public long? SeqGrupoEscalonamento { get; set; }
    }
}
