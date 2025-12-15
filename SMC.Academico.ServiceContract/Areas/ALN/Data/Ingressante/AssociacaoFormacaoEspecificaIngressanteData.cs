using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class AssociacaoFormacaoEspecificaIngressanteData : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqCurso { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }


        public List<FormacaoEspecificaHierarquiaData> FormacoesEspecificas { get; set; }
    }
}