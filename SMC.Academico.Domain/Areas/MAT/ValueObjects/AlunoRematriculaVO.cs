using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class AlunoRematriculaVO : ISMCMappable
    {

        public long SeqEntidadeVinculo { get; set; }

        public long SeqPessoaAtuacao { get; set; }

        public string NomeAluno { get; set; }

        public long SeqAlunoHistorico { get; set; }

        public long SeqNivelEnsino { get; set; }

        public long SeqTipoVinculoAluno { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public long SeqEntidadeCurso { get; set; }

        public long SeqEntidadeInstituicaoEnsino { get; set; }

        public long? SeqMatrizCurriculaOferta { get; set; }

        public long SeqCicloLetivoProcesso { get; set; }
    }
}