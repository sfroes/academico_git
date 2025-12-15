using SMC.Academico.Common.Constants;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface ITipoTermoIntercambioService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposTermosIntercambiosSelect();

        List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelSelect();

        /// <summary>
        /// Busca somente os Tipos de Termo que permitem associar Aluno
        /// </summary>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTiposTermosIntercambiosInstituicaoNivelPermiteAssociarAlunoSelect();

        /// <summary>
        /// Tipos de Termos que estão associados a uma Parceria de Intercâmbio.
        /// </summary>
        /// <param name="seqParceriaIntercambio">Sequencial da Parceria de Intercâmbio.</param>
        /// <returns>Lista dos Tipos de Termos de intercâmbio.</returns>
        List<SMCDatasourceItem> BuscarTiposTermosIntercambiosDaParceriaSelect(long seqParceriaIntercambio, long seqNivelEnsino);

        /// <summary>
        /// Busca todos os tipo de termo de intercambio de um nível de ensino.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino.</param>
        /// <param name="seqParceriaIntercambio">Sequencial da parceria de intercâmbio</param>
        /// <returns>Tipos de termo de intercâmbio.</returns>
        List<SMCDatasourceItem> BuscarTiposTermoIntercambioPorNivelEnsino(long seqNivelEnsino, long seqParceriaIntercambio);

        /// <summary>
        /// Buscar tipos termo intercambio por nivel de ensino e parceria de intercambio
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial nivel de ensino</param>
        /// <param name="seqParceriaIntercambio">Sequencial parceria intercambio</param>
        /// <param name="ativo">Indicativo situacao</param>
        /// <returns>Tipos de termo de intercambio</returns>
        List<SMCDatasourceItem> BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect(long? seqNivelEnsino, long seqParceriaIntercambio, bool? ativo = null);

        /// <summary>
        /// Valida se o tipo de termo intercambio é Cotutela
        /// </summary>
        /// <param name="seq">Sequencial tipo termo intercambio</param>
        /// <returns>Boleano caso</returns>
        bool ValidarTipoTermoIntercambioCoutela(long seq);
    }
}
