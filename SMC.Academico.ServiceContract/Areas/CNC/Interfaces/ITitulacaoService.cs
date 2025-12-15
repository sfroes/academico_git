using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface ITitulacaoService : ISMCService
    {
        /// <summary>
        /// Busca as titulação de acordo com o filtro passado.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        SMCPagerData<TitulacaoData> BuscarTitulacoes(TitulacaoFiltroData filtro);

        /// <summary>
        /// Busca as titulações que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Dados do filtro</param>
        /// <returns>Lista de titulações para select.
        /// A descrição será apresentada conforme o flag DescricaoAbreviada, sendo por padrão completa.
        /// Caso o campo sexo seja informado, será retornada apenas a descrição do genero informado.</returns>
        List<SMCDatasourceItem> BuscarTitulacoesSelect(TitulacaoFiltroData filtros);

        /// <summary>
        /// Salva a Titulação
        /// </summary>
        /// <param name="titulacao">Objeto a ser salvo</param> 
        /// <returns></returns>
        long SalvarTitulacao(TitulacaoData titulacao);

        /// <summary>
        /// Busca a titulação de acordo com o seq passado
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        TitulacaoData BuscarTitulacao(long seq);
    }
}