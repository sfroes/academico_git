using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.FIN.ValueObjects
{
    public class ContratoCursosListarVO : ISMCMappable
    {
        public long SeqCurso { get; set; }

        [SMCMapProperty("Curso.Nome")]
        public string Nome { get; set; }
    }
}
