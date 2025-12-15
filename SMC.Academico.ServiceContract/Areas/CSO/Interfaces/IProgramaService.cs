using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IProgramaService : ISMCService
    {
        /// <summary>
        /// Busca as configurações de entidade para cadastro de um programa
        /// </summary>
        /// <returns>Dados da configuração de entidade</returns>
        EntidadeData BuscaConfiguracoesEntidadePrograma();

        /// <summary>
        /// Busca os programas com suas formações específicas e cursos
        /// </summary>
        /// <param name="filtros">Filtros da listagem de programas</param>
        /// <returns>Programas com as descrições das formações específicas e cursos</returns>
        SMCPagerData<ProgramaListaData> BuscarProgramas(ProgramaFiltroData filtros);

        /// <summary>
        /// Busca um programa com as confirgurações de entidade
        /// </summary>
        /// <param name="seq">Sequencia do programa a ser recuperado</param>
        /// <returns>Dados do Programa e configurações de enditade</returns>
        ProgramaData BuscarPrograma(long seq);

        /// <summary>
        /// Buscar as situações do tipo de entidade programa para a instituição
        /// </summary>
        /// <returns>Lista de situações de um tipo de entidade programa na insituição</returns>
        List<SMCDatasourceItem> BuscarSituacoesPrograma(bool listarInativos);

        /// <summary>
        /// Grava um programa e suas dependências. Idiomas e sua hierarquia(gerada no serviço)
        /// </summary>
        /// <param name="programa">Programa a ser gravado incluindo idiomas</param>
        /// <returns>Sequencia do programa gravado</returns>
        long SalvarPrograma(ProgramaData programa);

        /// <summary>
        /// Recupera os programas de um grupo de programas
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial do grupo de programas</param>
        /// <returns>Sequenciais dos programas filhos do grupo de programas informado</returns>
        List<long> BuscarSeqsProgramasGrupo(long seqEntidadeVinculo);

        /// <summary>
        /// Buscar programa do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Sequencial do progrma</returns>
        long BuscarProgramaPorAluno(long seqAluno);

        /// <summary>
        /// Busca os itens selecionados para confirmação durante a replicação da formação específica de programa
        /// </summary>
        /// <param name="model">Modelo com os itens selecionados</param>
        /// <returns>Itens para a exibição da TreeView de confirmação</returns>
        List<ReplicaFormacaoEspecificaProgramaConfirmacaoData> BuscarItensSelecionadosReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaData model);

        /// <summary>
        /// Salva a replicação da formação específica do programa
        /// </summary>
        /// <param name="modelo">Dados para serem persistidos</param>
        void  SalvarReplicarFormacaoEspecificaPrograma(ReplicaFormacaoEspecificaProgramaData model);
    }
}