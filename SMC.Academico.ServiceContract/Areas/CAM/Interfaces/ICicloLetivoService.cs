using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ICicloLetivoService : ISMCService
    {
        /// <summary>
        /// Grava um CicloLetivo aplicando suas validações e preenchendo o campo descrição
        /// </summary>
        /// <param name="cicloLetivo">Ciclo letivo a ser gravado</param>
        /// <returns>Sequencial do ciclo letivo gravado</returns>
        long SalvarCicloLetivo(CicloLetivoData cicloLetivo);

        /// <summary>
        /// Busca os ciclos letivos que atendam aos filtros informados
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos a Serem copiados</param>
        /// <returns>Lista paginada com os Ciclos Letivos que atendam aos filtros tendo o ano e o nº do cliclo subistituidos pelos do destino.</returns>
        SMCPagerData<CicloLetivoData> BuscarCiclosLetivosCopia(CicloLetivoCopiaFiltroData filtro);

        /// <summary>
        /// Copia todos Ciclos Letivos informados substituindo o Ano e Número pelos valores informados
        /// </summary>
        /// <param name="filtro">Filtro para obter os Ciclos Letivos originais</param>
        void CopiarCiclosLetivos(CicloLetivoCopiaFiltroData filtro);

        /// <summary>
        /// Busca os Ciclos Letivos com Níveis de Ensino para o lookup
        /// </summary>
        /// <param name="filtro">Filtro dos Ciclos Letivos</param>
        /// <returns>Lista páginada dos ciclos letivos com Níveis de Ensino</returns>
        SMCPagerData<CicloLetivoData> BuscarCiclosLetivosLookup(CicloLetivoFiltroData filtro);

        /// <summary>
        /// Busca os Ciclos Letivos com Níveis de Ensino para o lookup selecionado.
        /// </summary>
        List<CicloLetivoData> BuscarCiclosLetivosLookupSelect(long[] seqs);

        CicloLetivoData BuscarCicloLetivo(long seq);

        /// <summary>
        /// Busca todos os ciclos letivos por campanha e nível de ensino
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <param name="seqNivelEnsino">Sequencial do nivel ensino</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaNivelSelect(long seqCampanha, long seqNivelEnsino);

        /// <summary>
        /// Busca todos os ciclos letivos por campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos ciclos letivos ordenados por descrição</returns>
        List<SMCDatasourceItem> BuscarCiclosLetivosPorCampanhaSelect(long seqCampanha);

        /// <summary>
        /// Busca os ciclo letivos que o aluno possui vinculado às turmas cursadas/cursando
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Lista de ciclo letivos para seleção</returns>
        [OperationContract]
        List<SMCDatasourceItem> BuscarCiclosLetivosPorAluno(long seqAluno);
        
        /// <summary>
        /// Recupera todos os ciclos letivos do aluno independente da situação
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Ciclos letivos vinculados ao aluno</returns>
        List<SMCDatasourceItem> BuscarCiclosLetivosPorHistoricoAluno(long seqAluno);

        /// <summary>
        /// Busca o ciclo letivo com a formatação {Numero}º/{Ano}
        /// </summary>
        /// <param name="seqCicloLetivo"></param>
        /// <returns>Ciclo letivo formatado {Numero}º/{Ano}</returns>
        string BuscarDescricaoFormatadaCicloLetivo(long seqCicloLetivo);


        /// <summary>
        /// Busca uma quantidade de ciclos letivos do aluno, em que o mesmo possui situação no ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="quantidadeCiclosLetivos">Quantidade de ciclos letivos anteriores ao atual</param>
        /// <returns>Lista de ciclos letivos em que o aluno pussui situação</returns>
        List<SMCDatasourceItem> BuscarCiclosLetivosAlunoComSituacaoSelect(long seqAluno, int? quantidadeCiclosLetivosAnteriores = null);
    }
}