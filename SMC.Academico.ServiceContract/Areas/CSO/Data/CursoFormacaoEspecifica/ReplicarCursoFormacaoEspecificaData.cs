using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class ReplicarCursoFormacaoEspecificaData : ISMCMappable
    {
        public long SeqCurso { get; set; }

        public long SeqFormacaoEspecifica { get; set; }

        public List<long> SeqsCursosOfertasLocalidades { get; set; }

        public string Mensagem { get; set; }
    }
}
