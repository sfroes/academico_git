using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteFormacaoEspecificaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaOrigem { get; set; }
    }
}
