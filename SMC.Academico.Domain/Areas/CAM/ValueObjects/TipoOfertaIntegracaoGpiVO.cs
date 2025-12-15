using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class TipoOfertaIntegracaoGpiVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Token { get; set; }

        public bool ExigeCursoOfertaLocalidadeTurno { get; set; }
    }
}
