using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.PES.Data
{
    public class PessoaAtuacaoDadosOrigemData : ISMCMappable
    {
        public long SeqOrigem { get; set; }

        public long CodigoServicoOrigem { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public long SeqAlunoHistoricoAtual { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long SeqEntidadeResponsavel { get; set; }
    }
}
