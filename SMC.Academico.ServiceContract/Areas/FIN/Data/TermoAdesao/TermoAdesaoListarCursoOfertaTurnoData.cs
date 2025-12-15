using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoListarCursoOfertaTurnoData : ISMCMappable
    {
        [SMCMapProperty("Turno.Descricao")]
        public string Descricao { get; set; }
    }
}
