using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularComponenteFiltroVO : ISMCMappable
    {
        /// <summary>
        /// Sequencial da pessoa atuação
        /// </summary>
        public long SeqPessoaAtuacao { get; set; }

        /// <summary>
        /// Sequencial do ciclo letivo
        /// </summary>
        public long? SeqCicloLetivo { get; set; }

        /// <summary>
        /// Matriz atual do aluno
        /// </summary>
        public long? SeqMatrizCurricular { get; set; }

        /// <summary>
        /// Filtra por toda hierarquia de formações específicas do aluno
        /// </summary>
        public bool FiltrarFormacoesEspecificasAluno { get; set; }
    }
}