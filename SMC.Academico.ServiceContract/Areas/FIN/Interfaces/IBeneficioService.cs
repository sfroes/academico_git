using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IBeneficioService : ISMCService
    {
        /// <summary>
        /// Busca um beneficio
        /// </summary>
        /// <param name="seq">Sequencial do beneficio</param>
        /// <returns>Dados do beneficio</returns>
        BeneficioData BuscarBeneficio(long seq);

        /// <summary>
        /// Busca os benefícios por nível de ensino na instituição
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados dos benefícios do nível de ensino informado</returns>
        List<SMCDatasourceItem> BuscarBeneficioPorNivelEnsinoSelect(long seqNivelEnsino);

        List<SMCDatasourceItem> BuscarTipoBeneficioSelect();

        long SalvarBeneficio(BeneficioData beneficio);

        List<SMCDatasourceItem> BuscarBeneficiosGRASelect(long seqTipoBeneficio);

        /// <summary>
        /// Busca beneficios
        /// </summary>
        /// <returns>Lista de benefícios</returns>
        List<SMCDatasourceItem> BuscarBeneficiosSelect();
    }
}