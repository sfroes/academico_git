using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ContratoNiveisEnsinoVO : ISMCMappable
    {
        public long? SeqNivelEnsino { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string DescricaoNivelEnsino { get; set; }
    }
}
