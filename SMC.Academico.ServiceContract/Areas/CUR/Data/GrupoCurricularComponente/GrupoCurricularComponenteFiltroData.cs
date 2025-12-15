using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularComponenteFiltroData : ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        /// <summary>
        /// Sequencial da pessoa atuação
        /// </summary>
        public long SeqPessoaAtuacao { get; set; }

        /// <summary>
        /// Seqquencial do ciclo letivo
        /// </summary>
        public long? SeqCicloLetivo { get; set; }

        /// <summary>
        /// Matriz atualdo aluno
        /// </summary>
        public long? SeqMatrizCurricular { get; set; }

        /// <summary>
        /// Filtra por toda hierarquia de formações específicas do aluno
        /// </summary>
        public bool FiltrarFormacoesEspecificasAluno { get; set; }
    }
}