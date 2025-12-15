using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class RematriculaJOBVO : ISMCMappable
    {
        public long SeqHistoricoAgendamento { get; set; }

        public long SeqProcesso { get; set; }
    }
}
