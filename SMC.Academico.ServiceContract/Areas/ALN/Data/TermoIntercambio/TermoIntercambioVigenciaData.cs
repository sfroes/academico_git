using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioVigenciaData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public long SeqTermoIntercambio { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public string Justificativa { get; set; }
    }
}

 