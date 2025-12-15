using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class ContratoNiveisEnsinoData : ISMCMappable
    {
        public long Seq { get; set; }
        public long? SeqNivelEnsino { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }
    }
}
