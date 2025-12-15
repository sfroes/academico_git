using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.MAT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ISituacaoMatriculaService : ISMCService
    {
        List<SMCDatasourceItem> BuscarSituacoesMatriculasSelect();

        /// <summary>
        /// Busca as situações de matrícula configuradas na instituição
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados das situaçoes de matrícula configuradas na instituição</returns>
        List<SMCDatasourceItem> BuscarSituacoesMatriculasDaInstiuicaoSelect(SituacaoMatriculaFiltroData filtroData);

        /// <summary>
        /// Buscar situações de matricula pelo tipo de situacao
        /// </summary>
        /// <param name="seq">Sequencial do tipo de matricula</param>
        /// <returns>Lista de situaçãoes de matricula</returns>
        List<SMCDatasourceItem> BuscarSituacoesMatriculaPorTipo(long seq);

        /// <summary>
        /// Buscar situações de matricula pelo tipo de situacao
        /// </summary>
        /// <param name="filtro">filtros</param>
        /// <returns>Lista de situaçãoes de matricula</returns>
        List<SMCDatasourceItem> BuscarSituacoesMatricula(SituacaoMatriculaFiltroData filtro);

        /// <summary>
        /// Buscar situação de matrícula pelo seq
        /// </summary>
        /// <param name="seq">Sequencial da situação da matrícula</param>
        /// <returns>Situação de matrícula encontrada</returns>
        SituacaoMatriculaData BuscarSituacaoMatricula(long seq);

        /// <summary>
        /// Busca uma situação de matricula pelo seu token e a transforma em um SMCDataSourceItem
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        SMCDatasourceItem BuscarSituacaoMatriculaItemSelectPorToken(string token);
    }
}