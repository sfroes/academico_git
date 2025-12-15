using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class GrupoEscalonamentoLookupSelectData : ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        [SMCKeyModel]
        public long? Seq { get; set; }

        public bool DisparaExcecao { get; set; }
    }
}
