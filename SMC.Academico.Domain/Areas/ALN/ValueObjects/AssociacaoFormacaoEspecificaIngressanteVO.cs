using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class AssociacaoFormacaoEspecificaIngressanteVO : ISMCMappable
    {
        public long SeqIngressante { get; set; }

        public long SeqCurso { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaOrigem { get; set; }

        public IEnumerable<FormacaoEspecificaHierarquiaVO> FormacoesEspecificas { get; set; }
    }
}