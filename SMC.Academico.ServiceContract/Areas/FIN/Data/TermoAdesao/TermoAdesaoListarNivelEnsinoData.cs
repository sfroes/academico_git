using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoListarNivelEnsinoData : ISMCMappable
    {
        [SMCMapProperty("NivelEnsino.Descricao")]
        public string NivelEnsinoDescricao { get; set; }
    }
}
