using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Localidades.Common.Constants;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaEnderecoDomainService : AcademicoContextDomain<PessoaEndereco>
    {
        private ILocalidadeService LocalidadeService
        {
            get { return this.Create<ILocalidadeService>(); }
        }

        /// <summary>
        /// Recupera os endereços de uma pessoa para associação a uma atuação
        /// </summary>
        /// <param name="filtro">Filtros do lookup</param>
        /// <returns>Endereços da pessoa</returns>
        public SMCPagerData<PessoaEnderecoLookupVO> BuscarPessoaEnderecosLookup(PessoaEnderecoFilterSpecification filtro)
        {
            var paises = LocalidadeService.BuscarPaisesValidosCorreios();

            filtro.SetOrderByDescending(o => o.DataInclusao);

            int total = 0;
            var dados = this.SearchProjectionBySpecification(filtro, x => new PessoaEnderecoLookupVO
            {
                CEP = x.Endereco.Cep,
                Correspondencia = x.Endereco.Correspondencia,
                NomeCidade = x.Endereco.NomeCidade,
                SiglaUf = x.Endereco.SiglaUf,
                Logradouro = x.Endereco.Logradouro,
                Numero = x.Endereco.Numero,
                Bairro = x.Endereco.Bairro,
                Seq = x.Seq,
                SeqEndereco = x.SeqEndereco,
                CodigoPais = x.Endereco.CodigoPais,
                TipoEndereco = x.Endereco.TipoEndereco
            }, out total);

            // Preenche a descrição do país
            var dadosRet = dados.ToList();
            foreach (var item in dadosRet)
            {
                item.DescPais = paises.FirstOrDefault(p => p.Codigo == item.CodigoPais)?.Nome;
                if (item.CodigoPais == LocalidadesDefaultValues.SEQ_PAIS_BRASIL)
                    item.CEP = SMCMask.ApplyMaskCEP(item.CEP);
            }

            return new SMCPagerData<PessoaEnderecoLookupVO>(dadosRet, total);
        }

        /// <summary>
        /// Busca os tipos de endereço de correspondência para uma atuação
        /// </summary>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <returns>Tipos de endereço de correspondência válidos para a atuação</returns>
        public List<SMCDatasourceItem> BuscarEnderecosCorrespondenciaSelect(TipoAtuacao tipoAtuacao)
        {
            var retorno = new List<SMCDatasourceItem>();

            switch (tipoAtuacao)
            {
                case TipoAtuacao.Aluno:
                case TipoAtuacao.Ingressante:
                    retorno.Add(EnderecoCorrespondencia.Academica.SMCToSelectItem());
                    retorno.Add(EnderecoCorrespondencia.Financeira.SMCToSelectItem());
                    retorno.Add(EnderecoCorrespondencia.AcademicaFinanceira.SMCToSelectItem());
                    retorno.Add(EnderecoCorrespondencia.Nao.SMCToSelectItem());
                    break;

                default:
                    retorno.Add(EnderecoCorrespondencia.Sim.SMCToSelectItem());
                    retorno.Add(EnderecoCorrespondencia.Nao.SMCToSelectItem());
                    break;
            }

            return retorno;
        }

        public PessoaEnderecoLookupVO BuscarPessoaEndereco(long seq)
        {
            var paises = LocalidadeService.BuscarPaisesValidosCorreios();

            var endereco = this.SearchProjectionByKey(new SMCSeqSpecification<PessoaEndereco>(seq), x => new PessoaEnderecoLookupVO
            {
                CEP = x.Endereco.Cep,
                Correspondencia = x.Endereco.Correspondencia,
                NomeCidade = x.Endereco.NomeCidade,
                SiglaUf = x.Endereco.SiglaUf,
                Logradouro = x.Endereco.Logradouro,
                Numero = x.Endereco.Numero,
                Bairro = x.Endereco.Bairro,
                Seq = x.Seq,
                SeqEndereco = x.SeqEndereco,
                CodigoPais = x.Endereco.CodigoPais,
                TipoEndereco = x.Endereco.TipoEndereco
            });

            // Preenche a descrição do país
            endereco.DescPais = paises.FirstOrDefault(p => p.Codigo == endereco.CodigoPais)?.Nome;

            return endereco;
        }
    }
}