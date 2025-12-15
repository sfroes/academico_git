using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ICurriculoCursoOfertaService : ISMCService
    {
        /// <summary>
        /// Buscar curriculo, curso e curso oferta para o cabeçalho
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta</param>
        /// <returns>CurriculoCursoOfertaVO com o cabeçalho</returns>
        CurriculoCursoOfertaData BuscarCurriculoCursoOfertaCabecalho(long seq, bool total);

        /// <summary>
        /// Recupera o currículo de um aluno pelo plano de ensino no histórico atual do cilco letivo informado
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Currículo configurado no plano de ensino do aluno do histórico ciclo letivo do histórico atual</returns>
        CurriculoCursoOfertaData BuscarCurriculoCursoOfertaPorAluno(long seqAluno, long seqCicloLetivo);
    }
}