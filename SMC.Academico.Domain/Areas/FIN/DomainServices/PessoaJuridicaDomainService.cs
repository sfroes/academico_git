using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.FIN.Exceptions;
using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.DadosMestres.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SMC.Academico.Common.Constants;
using SMC.DadosMestres.Common;

namespace SMC.Academico.Domain.Areas.FIN.DomainServices
{
    public class PessoaJuridicaDomainService : AcademicoContextDomain<PessoaJuridica>
    {

        #region Serviços

        IIntegracaoDadoMestreService IntegracaoDadoMestreService
        {
            get { return Create<IIntegracaoDadoMestreService>(); }
        }

        #endregion


        #region Propriedade

        private const string INCLUSAO = "Inclusão";
        private const string ALTERAR = "Alteração";

        #endregion Propriedade

        /// <summary>
        /// Salvar uma pessoa juridica e suas entidades
        /// </summary>
        /// <param name="pessoaJuridica">Dados da pessoa juridica a ser salva</param>
        /// <returns>Sequencial da pessoa juridica</returns>
        public long SalvarPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            this.ValidarCamposObrigatorios(pessoaJuridica);

            pessoaJuridica.Cnpj = Regex.Replace(pessoaJuridica.Cnpj, "[^0-9,]", "");

            using (var transaction = SMCUnitOfWork.Begin())
            {
                this.SaveEntity(pessoaJuridica);
                ///Serviço dados mestre
                InserePessoaJuridicaData inserePessoaJuridica = new InserePessoaJuridicaData();

                inserePessoaJuridica.NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO;
                inserePessoaJuridica.NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_JURIDICA;
                inserePessoaJuridica.NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ_JURIDICA;
                inserePessoaJuridica.SeqAtributoChaveIntegracao = pessoaJuridica.Seq;
                inserePessoaJuridica.CodigoPessoaCAD = null;
                inserePessoaJuridica.Cnpj = pessoaJuridica.Cnpj;
                inserePessoaJuridica.RazaoSocial = pessoaJuridica.RazaoSocial;
                inserePessoaJuridica.NomeFantasia = pessoaJuridica.NomeFantasia;
                inserePessoaJuridica.ArquivoLogotipo = null;

                var enderecoCorrespondencia = pessoaJuridica.Enderecos.Where(w => w.Correspondencia == true).FirstOrDefault();

                inserePessoaJuridica.Cep = Regex.Replace(enderecoCorrespondencia.Cep, "[^0-9,]", "");
                inserePessoaJuridica.Bairro = enderecoCorrespondencia.Bairro;
                inserePessoaJuridica.NomeCidade = enderecoCorrespondencia.NomeCidade;
                inserePessoaJuridica.CodigoCidade = (int)enderecoCorrespondencia.CodigoCidade;
                inserePessoaJuridica.Complemento = enderecoCorrespondencia.Complemento;
                inserePessoaJuridica.Logradouro = enderecoCorrespondencia.Logradouro;
                inserePessoaJuridica.Numero = enderecoCorrespondencia.Numero;
                inserePessoaJuridica.CodigoPais = enderecoCorrespondencia.CodigoPais;
                inserePessoaJuridica.SiglaUF = enderecoCorrespondencia.SiglaUf;               

                IntegracaoDadoMestreService.InserePessoaJuridica(inserePessoaJuridica);

                transaction.Commit();

                return pessoaJuridica.Seq;
            }
        }

        /// <summary>
        /// Validar regras de obrigatoriedade
        /// </summary>
        /// <param name="pessoaJuridica">Dados a serem validados</param>
        public void ValidarCamposObrigatorios(PessoaJuridica pessoaJuridica)
        {

            var endCorrespondencia = pessoaJuridica.Enderecos.Count(f => f.Correspondencia.HasValue && f.Correspondencia.Value);

            /// Maior ou menor irá tratar como exception
            if (endCorrespondencia != 1)
            {
                if (pessoaJuridica.IsNew())
                {
                    throw new PessoaJuridicaEnderecoObrigatorioCorrespondenciaException(INCLUSAO);
                }
                else
                {
                    throw new PessoaJuridicaEnderecoObrigatorioCorrespondenciaException(ALTERAR);
                }
            }

            //var telefonePrincipal = pessoaJuridica.Telefones.Count(c => c.Preferencial.HasValue && c.Preferencial.Value);

            //if (telefonePrincipal == 0)
            //{
            //    if (pessoaJuridica.IsNew())
            //    {
            //        throw new PessoaJuridicaTelefoneObrigatorioPrincipalException(INCLUSAO);
            //    }
            //    else
            //    {
            //        throw new PessoaJuridicaTelefoneObrigatorioPrincipalException(ALTERAR);
            //    }
            //}

            //var nomeContato = pessoaJuridica.Telefones.Count(c => !string.IsNullOrEmpty(c.NomeContato));

            //if (nomeContato == 0)
            //{
            //    if (pessoaJuridica.IsNew())
            //    {
            //        throw new PessoaJuridicaNomeContatoObrigatorioException(INCLUSAO);
            //    }
            //    else
            //    {
            //        throw new PessoaJuridicaNomeContatoObrigatorioException(ALTERAR);
            //    }
            //}
        }

        /// <summary>
        /// Buscar lista de pessoas juridicas com a paginação
        /// </summary>
        /// <param name="filtro">Dados de filtro</param>
        /// <returns>Lista pagiana de pessoas juridicas</returns>
        public SMCPagerData<PessoaJuridica> BuscarPessoasJuridicas(PessoaJuridicaFilterSpecification filtro)
        {
            int total;
            filtro.Seq = (filtro.Seq == 0 ? null : filtro.Seq);
            var pessoasJuridicas = this.SearchBySpecification(filtro, out total);

            return new SMCPagerData<PessoaJuridica>(pessoasJuridicas, total);
        }

        public PessoaJuridica BuscarPessoaJuridica(long seq)
        {
            return this.SearchByKey(new SMCSeqSpecification<PessoaJuridica>(seq));
        }
    }
}
