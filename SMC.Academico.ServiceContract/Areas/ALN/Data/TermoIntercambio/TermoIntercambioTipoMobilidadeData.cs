using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioTipoMobilidadeData : ISMCMappable
    { 
        public virtual long Seq { get; set; }
         
        public long SeqTermoIntercambio { get; set; }
         
        public TipoMobilidade TipoMobilidade { get; set; }
         
        public short QuantidadeVagas { get; set; }

        public List<TermoIntercambioPessoaData> Pessoas { get; set; }
    }
}
