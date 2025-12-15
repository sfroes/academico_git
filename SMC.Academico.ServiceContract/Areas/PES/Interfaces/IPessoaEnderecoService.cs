using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaEnderecoService : ISMCService
    {
        /// <summary>
        /// Recupera os endereços de uma pessoa para associação a uma atuação
        /// </summary>
        /// <param name="filtro">Filtros do lookup</param>
        /// <returns>Endereços da pessoa</returns>
        SMCPagerData<PessoaEnderecoLookupData> BuscarPessoaEnderecosLookup(PessoaEnderecoFiltroLookupData filtro);

        /// <summary>
        /// Busca os tipos de endereço de correspondência para uma atuação
        /// </summary>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <returns>Tipos de endereço de correspondência válidos para a atuação</returns>
        List<SMCDatasourceItem> BuscarEnderecosCorrespondenciaSelect(TipoAtuacao tipoAtuacao);

        /// <summary>
        /// Grava um novo endereço para uma pessoa
        /// </summary>
        /// <param name="enderecoData">Dados do endereço</param>
        /// <returns>Sequencial do endereço gravado</returns>
        long SalvarEnderecoPessoa(PessoaEnderecoData enderecoData);

        /// <summary>
        /// Recupera um endereço de uma pessoa
        /// </summary>
        /// <param name="seq">Sequencial do endereço a ser recuperado</param>
        /// <returns>Dados do endereço recuperado</returns>
        PessoaEnderecoData BuscarPessoaEndereco(long seq);
    }
}