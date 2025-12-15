using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    public interface IOrientacaoService : ISMCService
    {
        SMCPagerData<OrientacaoListarData> BuscarOrientacoes(OrientacaoFiltroData filtro);

        long SalvarOrientacao(OrientacaoData dadosOrientacao);

        OrientacaoData AlterarOrientacao(long seq);

        (string ParticipacaoOrientacaoObrigatoria, string OrientadoresSemVinculosAtivos) ValidarOrientacoes(OrientacaoData dadosOrientacao);

        /// <summary>
        /// Buscar todas as orientação de uma determinda divisao de turma
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Todas as orientações de uma divisão de turma</returns>
        SMCPagerData<OrientacaoTurmaData> BuscarOrientacoesPorDivisaoTurma(OrientacaoTurmaFiltroData filtro);

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        OrientacaoTurmaData BuscarOrientacaoPorDivisaoTurma(OrientacaoTurmaFiltroData filtro);

        /// <summary>
        /// Buscar orientação de uma determinado filtro
        /// </summary>
        /// <param name="filtro">Parametros para filtrar a orientação da turma</param>
        /// <returns>Orientação de uma divisão de turma</returns>
        long SalvarOrientacaoTurma(OrientacaoTurmaData dadosOrietacao);

        /// <summary>
        /// Exlcluir orientação
        /// </summary>
        /// <param name="seq">Sequencial da orientação</param>
        void ExcluirOrientacao(long seq);

        /// <summary>
        /// Buscar orientacoes do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Lista das orientações do aluno</returns>
        List<OrientacaoData> BuscarOrientacoesPorAluno(long seqPessoaAtuacao);

        /// <summary>
        /// Metodo para buscar informações do relatorio de Orientadores
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Retorna um relatorio data</returns>

        List<OrientadoresRelatorioData> BuscarOrientacoesRelatorio(OrientacaoFiltroData filtro);
    }
}
