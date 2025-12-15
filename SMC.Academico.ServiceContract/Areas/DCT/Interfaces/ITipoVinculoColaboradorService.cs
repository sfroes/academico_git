using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface ITipoVinculoColaboradorService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTipoVinculoColaboradorSelect();

        /// <summary>
        /// Buscar todos os tipos de vinculo baseado nas configurações da instituição.
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade para filtrar apenas os vinculos configurados para o tipo desta</param>
        /// <param name="criaVinculoInstitucional">Retorna apenas os tipos de vínculo que permitem criar vínculo institucional</param>
        /// <returns>Dados dos vínculos configurados na instituição ou apenas os vinculos do tipo de entidade informado</returns>
        List<SMCDatasourceItem> BuscarTipoVinculoColaboradorDeEntidadesVinculoSelect(long? seqEntidadeVinculo = null, bool? criaVinculoInstitucional = null);

        /// <summary>
        /// Retornar se tipo vinculo colaborador necessita acompanhamento de supervisor
        /// </summary>
        /// <param name="seqTipoVinculoColaborador">Sequencial tipo vinculo colaborador</param>
        /// <returns>Retornar boleano necessita acompanhamento de supervisor </returns>
        bool RetornarTipoVinculoNecessitaAcompanhamento(long seqTipoVinculoColaborador);

        List<SMCDatasourceItem> BuscarTipoVinculoColaboradorPorEntidadesSelect(List<long> seqsEntidadesResponsaveis);

    }
}