using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.MAT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITipoSituacaoMatriculaService : ISMCService
    {
        /// <summary>
        /// Busca todos os tipos de situações de matrícula que tenham o token matriculado
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas com o token matriculado</returns>
        List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasTokenMatriculadoSelect();

        /// <summary>
        /// Busca todos os tipos de situações de matrícula
        /// </summary>
        /// <returns>Dados dos tipos de situaçoes de matrícula configuradas</returns>
        List<SMCDatasourceItem> BuscarTiposSituacoesMatriculasSelect();

        /// <summary>
        /// Busca o token do tipo da situação de matrícula por seq
        /// </summary>
        /// <param name="seqTipoSituacaoMatricula">Sequencial do tipo de situação da matrícula</param>
        /// <returns></returns>
        string BuscarTokenTipoSituacaoMatricula(long seqTipoSituacaoMatricula);
    }
}