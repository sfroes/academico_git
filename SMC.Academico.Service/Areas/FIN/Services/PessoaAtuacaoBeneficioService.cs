using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.DomainServices;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Jobs;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.FIN.Services
{
    public class PessoaAtuacaoBeneficioService : SMCServiceBase, IPessoaAtuacaoBeneficio
    {
        #region [ DomainServices ]

        private PessoaAtuacaoBeneficioDomainService PessoaAtuacaoBeneficioDomainService => Create<PessoaAtuacaoBeneficioDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        #endregion [ DomainServices ]

        public SMCPagerData<PessoaAtuacaoBeneficioData> BuscarPesssoasAtuacoesBeneficios(PessoaAtuacaoBeneficioFiltroData filtro)
        {
            var spec = filtro.Transform<PessoaAtuacaoBeneficioFilterSpecification>();
            return PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficios(spec).Transform<SMCPagerData<PessoaAtuacaoBeneficioData>>();
        }

        public PessoaAtuacaoBeneficioData BuscarPesssoaAtuacaoBeneficioCabecalho(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarPessoaAtuacaoCabecalho(seqPessoaAtuacao).Transform<PessoaAtuacaoBeneficioData>();
        }

        public PessoaAtuacaoBeneficioData BuscarPessoaAtuacaoDocumentoCabecalho(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarPessoaAtuacaoDocumentoCabecalho(seqPessoaAtuacao).Transform<PessoaAtuacaoBeneficioData>();
        }

        public long SalvarPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio)
        {
            //var pessoaAtuacaoBeneficioDados = pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficio>(pessoaAtuacaoBeneficio);

            return PessoaAtuacaoBeneficioDomainService.SalvarPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficioVO>());
        }

        public List<SMCDatasourceItem> BuscarsBeneficiosSelect(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarsBeneficiosSelect(seqPessoaAtuacao);
        }

        public List<SMCDatasourceItem> BuscarConfiguracoesBeneficiosSelect(long seqBeneficio, long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarConfiguracoesBeneficiosSelect(seqBeneficio, seqPessoaAtuacao);
        }

        public TipoDeducao BuscarTipoDeducao(long seqConfiguracaoBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarTipoDeducao(seqConfiguracaoBeneficio);
        }

        public FormaDeducao BuscarFormaDeducao(long seqConfiguracaoBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarFormaDeducao(seqConfiguracaoBeneficio);
        }

        public decimal BuscarValorConfiguracaoBeneficio(long seqConfiguracaoBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarValorConfiguracaoBeneficio(seqConfiguracaoBeneficio);
        }

        public AssociarResponsavelFinanceiro BuscarIdAssociarResponsavelFinanceiro(long seqBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarIdAssociarResponsavelFinanceiro(seqBeneficio);
        }

        public bool BuscarIdDeducaoValorParcelaTitular(long seqBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarIdDeducaoValorParcelaTitular(seqBeneficio);
        }

        public PessoaAtuacaoBeneficioData AlterarPessoaAtuacaoBeneficio(long seq)
        {
            return PessoaAtuacaoBeneficioDomainService.AlterarPessoaAtuacaoBeneficio(seq).Transform<PessoaAtuacaoBeneficioData>();
        }

        /// <summary>
        /// Consuta de dados da pessoa atuação benefício
        /// </summary>
        /// <param name="seq">Sequencial pessoa atuação benefício</param>
        /// <returns>Dados para exibir dados</returns>
        public PessoaAtuacaoBeneficioData ConsultarPessoaAtuacaoBeneficio(long seq)
        {
            return PessoaAtuacaoBeneficioDomainService.ConsultarPessoaAtuacaoBeneficio(seq).Transform<PessoaAtuacaoBeneficioData>();
        }

        public DateTime BuscarDataAdmissaoIngressante(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarDataAdmissaoIngressante(seqPessoaAtuacao);
        }

        /// <summary>
        /// Realiza as validações da regra RN_FIN_002 Consistência associação benefício (apenas associação)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        public PessoaAtuacaoBeneficioData BuscarAssociacaoBeneficio(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.ValidarAssociacaoBeneficio(seqPessoaAtuacao).Transform<PessoaAtuacaoBeneficioData>();
        }

        /// <summary>
        /// Busca simples da pessoa atuação e suas colections
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <returns>Dados da pessoa atuação beneficio</returns>
        public PessoaAtuacaoBeneficioData BuscarPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarPessoaAtuacaoBeneficio(seqPessoaAtuacaoBeneficio).Transform<PessoaAtuacaoBeneficioData>();
        }

        public void ExcluirPesssoaAtuacaoBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio)
        {
            PessoaAtuacaoBeneficioDomainService.ExcluirPessoaAtuacaoBeneficio(pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficioVO>());
        }

        /// <summary>
        /// Realiza a consulta de beneficios com data atual no período de vigência de acordo com a pessoa atuação na matrícula e renovação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial de pessoa atuação</param>
        /// <returns>Lista de benefícios vigêntes para a pessoa atuação</returns>
        public List<PessoaAtuacaoBeneficioMatriculaData> BuscarPesssoasAtuacoesBeneficiosMatricula(long seqPessoaAtuacao)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarPesssoasAtuacoesBeneficiosMatricula(seqPessoaAtuacao).TransformList<PessoaAtuacaoBeneficioMatriculaData>();
        }

        public List<RelatorioBolsistasData> BuscarDadosRelatorioBolsistas(RelatorioBolsistasFiltroData filtro)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarDadosRelatorioBolsistas(filtro
                                                        .Transform<RelatorioBolsistasFiltroVO>())
                                                        .TransformList<RelatorioBolsistasData>();
        }

        /// <summary>
        /// Valida se a quantidade de parcelas parametrizadas na configuração de beneficio esta de acordo com a pessoa atuação, sendo aluno ou ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns></returns>
        public bool ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(long seqPessoaAtuacao, long seqBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(seqPessoaAtuacao, seqBeneficio);
        }

        /// <summary>
        /// Salvar a alteração de vigência da pessoa atuação beneficio
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Dados a serem salvos</param>
        /// <returns>Sequêncial pessoa atuação beneficio</returns>
        public long SalvarAlterarVigenciaBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio)
        {
            return this.PessoaAtuacaoBeneficioDomainService.SalvarAlterarVigenciaBeneficio(pessoaAtuacaoBeneficio.Transform<PessoaAtuacaoBeneficioVO>());
        }

        /// <summary>
        /// Listar tipo de responsável financeiro select baseado no beneficio
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>Lista de tipos de responsaveis financeiros select</returns>
        public List<SMCDatasourceItem> BuscarTipoResponsavelFinanceiroSelect(long seqBeneficio)
        {
            return this.PessoaAtuacaoBeneficioDomainService.BuscarTipoResponsavelFinanceiroSelect(seqBeneficio);
        }

        /// <summary>
        /// Atualiza as datas de término dos benefícos com conessão até o final do curso
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        public void AtualizarTerminoBeneficio(ISMCWebJobFilterModel filtro)
        {
            PessoaAtuacaoBeneficioDomainService.AtualizarTerminoBeneficio(filtro);
        }

        /// <summary>
        /// Buscar dados das notificações da pessoa atuação benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <returns>Lista de todas as notificações da pessoa atuação benefício</returns>
        public PessoaAtuacaoBeneficioData BuscarNotificacoesPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio)
        {
            return PessoaAtuacaoBeneficioDomainService.BuscarNotificacoesPessoaAtuacaoBeneficio(seqPessoaAtuacaoBeneficio).Transform<PessoaAtuacaoBeneficioData>();
        }
    }
}