using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class ProcessoSeletivoOfertaData : ISMCMappable
    {
        public long SeqProcessoSeletivo { get; set; }

        public List<long> Ofertas { get; set; }

        public List<long> Convocacoes { get; set; }
    }
}
