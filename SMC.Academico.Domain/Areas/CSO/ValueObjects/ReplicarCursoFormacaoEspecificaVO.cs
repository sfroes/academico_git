using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class ReplicarCursoFormacaoEspecificaVO : ISMCMappable
    {
        public long SeqCurso { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public List<long> SeqsCursosOfertasLocalidades { get; set; }

        public string Mensagem { get; set; }
    }
}
