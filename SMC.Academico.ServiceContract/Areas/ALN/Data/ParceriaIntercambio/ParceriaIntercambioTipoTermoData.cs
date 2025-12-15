using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioTipoTermoData : ISMCMappable
    {
        public long Seq { get; set; }
         
        public long SeqTipoTermoIntercambio { get; set; }

        public bool Ativo { get; set; }
    }
}
