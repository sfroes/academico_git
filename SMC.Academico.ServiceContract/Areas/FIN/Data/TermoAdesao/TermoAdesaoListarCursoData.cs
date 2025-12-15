using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.FIN.Data
{
    public class TermoAdesaoListarCursoData : ISMCMappable
    {
        public long SeqCurso{ get; set; }
         
        [SMCMapProperty("Curso.Nome")]
        public string NomeCurso { get; set; } 

    }
}
