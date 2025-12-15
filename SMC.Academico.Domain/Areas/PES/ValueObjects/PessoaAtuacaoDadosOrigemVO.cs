using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class PessoaAtuacaoDadosOrigemVO : ISMCMappable
    {
        public long SeqOrigem { get; set; }

        public long CodigoServicoOrigem { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long SeqMatrizCurricularOferta { get; set; }

        public TipoAtuacao TipoAtuacao { get; set; }

        public long SeqAlunoHistoricoAtual { get; set; }

        public long? SeqCicloLetivo { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public long SeqCursoOfertaLocalidade { get; set; }

		public long? CodigoAlunoMigracao { get; set; }

		public long? SeqMatrizCurricular { get; set; }

        public long SeqCurso { get; set; }

        public long SeqEntidadeResponsavel { get; set; }
    }
}
