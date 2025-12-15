using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class ParceriaIntercambioTipoTermoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTipoTermoIntercambio { get; set; }
    }     
}
