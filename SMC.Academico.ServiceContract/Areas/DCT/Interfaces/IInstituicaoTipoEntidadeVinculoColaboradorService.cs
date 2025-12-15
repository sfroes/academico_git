using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IInstituicaoTipoEntidadeVinculoColaboradorService : ISMCService
    {
        /// <summary>
        /// Busca os tipos de vinculos de colaborador na instituição para o tipo de uma entidade
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial da entidade vinculada</param>
        /// <param name="seqColaboradorVinculo">Sequencial do colaborador vinculo</param>
        /// <returns>Dados dos tipos de vinculo na instituição para o tipo da entidade informada</returns>
        List<SMCDatasourceItem> BuscarTiposVinculoColaboradorPorEntidadeSelect(long seqEntidadeVinculo, long? seqColaboradorVinculo);
    }
}