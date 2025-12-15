using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Data.PessoaAtuacaoBeneficio;
using SMC.Framework.Jobs;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface IPessoaAtuacaoBeneficio : ISMCService
    {
        /// <summary>
        /// Buscar lista de dados da pessoa atuação beneficio
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficio">Data pesssoa atuação beneficio</param>
        /// <returns></returns>
        SMCPagerData<PessoaAtuacaoBeneficioData> BuscarPesssoasAtuacoesBeneficios(PessoaAtuacaoBeneficioFiltroData filtro);

        /// <summary>
        /// Buscar dados cabeçalho da pessoa atuação
        /// </summary>
        /// <param name="seqpessoaAtuacao">Seq pessoa atuação</param>
        /// <returns></returns>
        PessoaAtuacaoBeneficioData BuscarPesssoaAtuacaoBeneficioCabecalho(long seqPessoaAtuacao);

        /// <summary>
        /// Salvar a Pessoa beneficio
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficio">Data a serem salvos</param>
        /// <returns></returns>
        long SalvarPessoaAtuacaoBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio);

        /// <summary>
        /// Busca todas as configurações conforme o beneficio selecionado
        /// </summary>
        /// <param name="seqBeneficio">Seq Beneficio</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarConfiguracoesBeneficiosSelect(long seqBeneficio, long seqPessoaAtuacao);

        /// <summary>
        /// Busca todos os beneficios selecionado conforme o nivel de ensino do ingressante
        /// </summary>
        /// <param name="seqBeneficio">Seq Beneficio</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarsBeneficiosSelect(long seqPessoaAtuacao);

        /// <summary>
        /// Buscar o tipo de deducção de uma determinada configuração
        /// </summary>
        /// <param name="seqConfiguracaoBeneficio">Seq Configuração de Beneficio</param>
        /// <returns></returns>
        TipoDeducao BuscarTipoDeducao(long seqConfiguracaoBeneficio);

        /// <summary>
        /// Buscar o forma de deducção de uma determinada configuração
        /// </summary>
        /// <param name="seqConfiguracaoBeneficio">Seq Configuração de Beneficio</param>
        /// <returns></returns>
        FormaDeducao BuscarFormaDeducao(long seqConfiguracaoBeneficio);

        /// <summary>
        /// Buscar Valor Configuracao de beneficio selecionada
        /// </summary>
        /// <param name="seqConfiguracaoBeneficio">Seq Configuração de Beneficio</param>
        /// <returns></returns>
        decimal BuscarValorConfiguracaoBeneficio(long seqConfiguracaoBeneficio);

        /// <summary>
        /// Buscar a associação a responsavel financeiro de um determinado beneficio
        /// </summary>
        /// <param name="seqConfiguracaoBeneficio">Seq Beneficio</param>
        /// <returns></returns>
        AssociarResponsavelFinanceiro BuscarIdAssociarResponsavelFinanceiro(long seqBeneficio);

        /// <summary>
        /// Buscar a deducação de valor parcela titular
        /// </summary>
        /// <param name="seqBeneficio">Seq Beneficio</param>
        /// <returns></returns>
        bool BuscarIdDeducaoValorParcelaTitular(long seqBeneficio);

        /// <summary>
        /// Para edição da Pessoa Atuação Beneficio
        /// </summary>
        /// <param name="Seq">Seq Pessoa Atuação do Beneficio</param>
        /// <returns></returns>
        PessoaAtuacaoBeneficioData AlterarPessoaAtuacaoBeneficio(long seq);

        /// <summary>
        /// Consuta de dados da pessoa atuação benefício
        /// </summary>
        /// <param name="seq">Sequencial pessoa atuação benefício</param>
        /// <returns>Dados para exibir dados</returns>
        PessoaAtuacaoBeneficioData ConsultarPessoaAtuacaoBeneficio(long seq);

        /// <summary>
        /// Buscar a data de admissao do ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Seq pessoa atuação</param>
        /// <returns></returns>
        DateTime BuscarDataAdmissaoIngressante(long seqPessoaAtuacao);

        /// <summary>
        /// Realiza as validações da regra RN_FIN_002 Consistência associação benefício (apenas associação)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        PessoaAtuacaoBeneficioData BuscarAssociacaoBeneficio(long seqPessoaAtuacao);

        /// <summary>
        /// Buscar dados da pessoa atuacao beneficio pela pessoa atuação
        /// </summary>
        /// <param name="SeqPessoaAtuacao">Seq pessoa atuacao</param>
        /// <returns></returns>
        PessoaAtuacaoBeneficioData BuscarPessoaAtuacaoBeneficio(long SeqPessoaAtuacao);

        /// <summary>
        /// Excluir pessoa atuação beneficio
        /// </summary>
        /// <param name="Seq">Seq pessoa atuação beneficio</param>
        void ExcluirPesssoaAtuacaoBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio);

        /// <summary>
        /// Realiza a consulta de beneficios com data atual no período de vigência de acordo com a pessoa atuação na matrícula e renovação
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial de pessoa atuação</param>
        /// <returns>Lista de benefícios vigêntes para a pessoa atuação</returns>
        List<PessoaAtuacaoBeneficioMatriculaData> BuscarPesssoasAtuacoesBeneficiosMatricula(long seqPessoaAtuacao);

        List<RelatorioBolsistasData> BuscarDadosRelatorioBolsistas(RelatorioBolsistasFiltroData filtro);

        /// <summary>
        /// Valida se a quantidade de parcelas parametrizadas na configuração de beneficio esta de acordo com a pessoa atuação, sendo aluno ou ingressante
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns></returns>
        bool ValidarNumeroParcelasParametrizadosConfiguracaoSaoDiferentes(long seqPessoaAtuacao, long seqBeneficio);

        /// <summary>
        /// Salvar a alteração de vigência da pessoa atuação beneficio
        /// </summary>
        /// <param name="pessoaAtuacaoBeneficioVO">Dados a serem salvos</param>
        /// <returns>Sequêncial pessoa atuação beneficio</returns>
        long SalvarAlterarVigenciaBeneficio(PessoaAtuacaoBeneficioData pessoaAtuacaoBeneficio);

        /// <summary>
        /// Listar tipo de responsável financeiro select baseado no beneficio
        /// </summary>
        /// <param name="seqBeneficio">Sequencial do beneficio</param>
        /// <returns>Lista de tipos de responsaveis financeiros select</returns>
        List<SMCDatasourceItem> BuscarTipoResponsavelFinanceiroSelect(long seqBeneficio);

        /// <summary>
        /// Atualiza as datas de término dos benefícos com conessão até o final do curso
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        void AtualizarTerminoBeneficio(ISMCWebJobFilterModel filtro);

        /// <summary>
        /// Buscar dados das notificações da pessoa atuação benefício
        /// </summary>
        /// <param name="seqPessoaAtuacaoBeneficio">Sequencial pessoa atuação beneficio</param>
        /// <returns>Lista de todas as notificações da pessoa atuação benefício</returns>
        PessoaAtuacaoBeneficioData BuscarNotificacoesPessoaAtuacaoBeneficio(long seqPessoaAtuacaoBeneficio);

        /// <summary>
        /// Buscar dados cabeçalho da pessoa atuação documento
        /// </summary>
        /// <param name="seqpessoaAtuacao">Seq pessoa atuação</param>
        /// <returns></returns>
        PessoaAtuacaoBeneficioData BuscarPessoaAtuacaoDocumentoCabecalho(long seqPessoaAtuacao);
    }
}