using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data.Aluno
{
    public class AssociacaoFormacaoEspecificaAlunoData : ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long SeqCurso { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long? SeqFormacaoEspecifica { get; set; }

        public long[] SeqsFormacoesEspecificasOrigem { get; set; }

        public List<long> SeqTipoFormacaoEspecifica { get; set; }

        public List<FormacaoEspecificaHierarquiaData> FormacoesEspecificas { get; set; }
    }
}