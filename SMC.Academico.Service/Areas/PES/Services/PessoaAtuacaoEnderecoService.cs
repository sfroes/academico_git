using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaAtuacaoEnderecoService : SMCServiceBase, IPessoaAtuacaoEnderecoService
    {
        #region [ DataSources ]

        private PessoaAtuacaoEnderecoDomainService PessoaAtuacaoEnderecoDomainService
        {
            get { return this.Create<PessoaAtuacaoEnderecoDomainService>(); }
        }

        #endregion [ DataSources ]

        /// <summary>
        /// Recupera os endereços associados a uma PessoaAtuacaoEndereco.
        /// Caso o endereço não esteja associado recupera o endreço da PessoaEndereco
        /// </summary>
        /// <param name="filtro">Filtro com os sequencias dos PessoaEndereco associados e da Pessoa</param>
        /// <returns>Lista de PessoaAtuacaoEndereco asociada
        /// (os PessoaEndereco não associados à PessoaAtuacao serão retornados como novos registros sem seq)
        /// </returns>
        public List<PessoaAtuacaoEnderecoLookupData> BuscarPessoaAtuacaoEnderecosLookup(PessoaAtuacaoEnderecoFiltroLookupData filtro)
        {
            return this.PessoaAtuacaoEnderecoDomainService.BuscarPessoaAtuacaoEnderecosLookup(filtro.Transform<PessoaAtuacaoEnderecoFilterSpecification>()).TransformList<PessoaAtuacaoEnderecoLookupData>();
        }
    }
}