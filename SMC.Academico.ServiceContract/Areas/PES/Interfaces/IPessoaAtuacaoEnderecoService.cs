using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    public interface IPessoaAtuacaoEnderecoService : ISMCService
    {
        /// <summary>
        /// Recupera os endereços associados a uma PessoaAtuacaoEndereco.
        /// Caso o endereço não esteja associado recupera o endreço da PessoaEndereco
        /// </summary>
        /// <param name="filtro">Filtro com os sequencias dos PessoaEndereco associados e da Pessoa</param>
        /// <returns>Lista de PessoaAtuacaoEndereco asociada
        /// (os PessoaEndereco não associados à PessoaAtuacao serão retornados como novos registros sem seq)
        /// </returns>
        List<PessoaAtuacaoEnderecoLookupData> BuscarPessoaAtuacaoEnderecosLookup(PessoaAtuacaoEnderecoFiltroLookupData filtro);
    }
}