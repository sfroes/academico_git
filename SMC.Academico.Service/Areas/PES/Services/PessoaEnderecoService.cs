using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.PES.Services
{
    public class PessoaEnderecoService : SMCServiceBase, IPessoaEnderecoService
    {
        #region DomainService

        private PessoaEnderecoDomainService PessoaEnderecoDomainService
        {
            get { return this.Create<PessoaEnderecoDomainService>(); }
        }

        #endregion DomainService

        /// <summary>
        /// Recupera um endereço de uma pessoa
        /// </summary>
        /// <param name="seq">Sequencial do endereço a ser recuperado</param>
        /// <returns>Dados do endereço recuperado</returns>
        public PessoaEnderecoData BuscarPessoaEndereco(long seq)
        {
            return PessoaEnderecoDomainService.SearchByKey(new SMCSeqSpecification<PessoaEndereco>(seq), IncludesPessoaEndereco.Endereco).Transform<PessoaEnderecoData>();
        }

        /// <summary>
        /// Recupera os endereços de uma pessoa para associação a uma atuação
        /// </summary>
        /// <param name="filtro">Filtros do lookup</param>
        /// <returns>Endereços da pessoa</returns>
        public SMCPagerData<PessoaEnderecoLookupData> BuscarPessoaEnderecosLookup(PessoaEnderecoFiltroLookupData filtro)
        {
            return PessoaEnderecoDomainService.BuscarPessoaEnderecosLookup(filtro.Transform<PessoaEnderecoFilterSpecification>()).Transform<SMCPagerData<PessoaEnderecoLookupData>>();
        }

        /// <summary>
        /// Busca os tipos de endereço de correspondência para uma atuação
        /// </summary>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <returns>Tipos de endereço de correspondência válidos para a atuação</returns>
        public List<SMCDatasourceItem> BuscarEnderecosCorrespondenciaSelect(TipoAtuacao tipoAtuacao)
        {
            return this.PessoaEnderecoDomainService.BuscarEnderecosCorrespondenciaSelect(tipoAtuacao);
        }

        public long SalvarEnderecoPessoa(PessoaEnderecoData enderecoData)
        {
            PessoaEndereco model = enderecoData.Transform<PessoaEndereco>();
            model.Endereco.Cep = model.Endereco.Cep?.SMCRemoveNonDigits();
            PessoaEnderecoDomainService.SaveEntity(model);
            return model.Seq;
        }
    }
}