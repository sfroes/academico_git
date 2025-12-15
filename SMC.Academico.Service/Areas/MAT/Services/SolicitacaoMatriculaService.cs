using SMC.Academico.Common.Areas.MAT.Includes;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Data.Efetivacao;
using SMC.Academico.ServiceContract.Areas.MAT.Data.SolicitacaoMatricula;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.MAT.Services
{
    public class SolicitacaoMatriculaService : SMCServiceBase, ISolicitacaoMatriculaService
    {
        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService
        {
            get { return this.Create<SolicitacaoMatriculaDomainService>(); }
        }

        public CabecalhoMenuMatriculaData BuscarCabecalhoMenu(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.BuscarCabecalhoMenu(seqSolicitacaoMatricula).Transform<CabecalhoMenuMatriculaData>();
        }

        public SolicitacaoMatriculaData BuscarSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.SearchByKey(new SMCSeqSpecification<SolicitacaoMatricula>(seqSolicitacaoMatricula),
                IncludesSolicitacaoMatricula.Etapas |
                IncludesSolicitacaoMatricula.Etapas_HistoricosNavegacao |
                IncludesSolicitacaoMatricula.Etapas_HistoricosSituacao |
                IncludesSolicitacaoMatricula.ConfiguracaoProcesso |
                IncludesSolicitacaoMatricula.ConfiguracaoProcesso_Processo_Servico).Transform<SolicitacaoMatriculaData>();
        }

        public void SalvarCondicaoPagamento(long seqSolicitacaoMatricula, int? seqCondicaoPagamento)
        {
            SolicitacaoMatriculaDomainService.SalvarCondicaoPagamento(seqSolicitacaoMatricula, seqCondicaoPagamento);
        }

        public CondicaoPagamentoSolicitacaoMatriculaData BuscarCondicaoPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula, bool considerarApenasCondicaoPagamentoSelecionada = false)
        {
            return SolicitacaoMatriculaDomainService.BuscarCondicaoPagamentoSolicitacaoMatricula(seqSolicitacaoMatricula).Transform<CondicaoPagamentoSolicitacaoMatriculaData>();
        }

        public List<CondicaoPagamentoAcademicoData> BuscarCondicoesPagamentoAcademico(long seqSolicitacaoMatricula, bool considerarCondicaoSelecionada = false)
        {
            return SolicitacaoMatriculaDomainService.BuscarCondicoesPagamentoAcademico(seqSolicitacaoMatricula, considerarCondicaoSelecionada).TransformList<CondicaoPagamentoAcademicoData>();
        }

        public ParcelasPagamentoSolicitacaoMatriculaData BuscarParcelasPagamentoSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.BuscarParcelasPagamentoSolicitacaoMatricula(seqSolicitacaoMatricula).Transform<ParcelasPagamentoSolicitacaoMatriculaData>();
        }

        public BoletoMatriculaData GerarBoletoMatricula(long seqTitulo, long seqServico, long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.GerarBoletoMatricula(seqTitulo, seqServico, seqSolicitacaoMatricula).Transform<BoletoMatriculaData>();
        }

        public SMCUploadFile InserirArquivoTermoAdesao(long seqSolicitacaoMatricula, byte[] arquivoConvertidoPdf)
        {
            return SolicitacaoMatriculaDomainService.InserirArquivoTermoAdesao(seqSolicitacaoMatricula, arquivoConvertidoPdf);
        }

        /// <summary>
        /// Buscar as solicitações de matricula que precisam ser chancelada de acordo com o token do serviço
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de matricula</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="orientacao">identifica se possui orientador</param>
        /// <param name="desativarFiltroDados">Desconsidera o filtro de dados</param>
        /// <returns></returns>
        public ChancelaData BuscarSolicitacaoMatriculaChancela(long seq, string tokenEtapa, bool? orientacao, bool desabilitarFiltro = false)
        {
            return SolicitacaoMatriculaDomainService.BuscarSolicitacaoMatriculaChancela(seq, tokenEtapa, orientacao, desabilitarFiltro).Transform<ChancelaData>();
        }

        public List<SMCDatasourceItem> BuscarSituacoesItensChancela(long seqSolicitacao)
        {
            return SolicitacaoMatriculaDomainService.BuscarSituacoesItensChancela(seqSolicitacao);
        }

        public SMCPagerData<ChancelaItemListaData> BuscarChancelas(ChancelaFiltroData filtro)
        {
            var dados = SolicitacaoMatriculaDomainService.BuscarChancelas(filtro.Transform<ChancelaFilterSpecification>())?.Transform<SMCPagerData<ChancelaItemListaData>>();
            return dados;
        }

        /// <summary>
        /// Busca todos os processos das solicitações que estão em grupos de escalonamentos ativos,
        /// com alguma situação da etapa de chancela ou a situação inicial da etapa posterior para filtro da chancela do orientador
        /// </summary>
        /// <param name="apenasProcessoVigente">Filtro por data vigente no grupo de escalonamento do processo</param>
        /// <returns>Lista de processos para utilizar no filtro da chancela</returns>
        public List<SMCDatasourceItem> BuscarProcessosFiltroChancela(bool apenasProcessoVigente)
        {
            return SolicitacaoMatriculaDomainService.BuscarProcessosFiltroChancela(apenasProcessoVigente);
        }

        /// <summary>
        /// Chancela a solicitação de serviço e dependendo a etapa cria o plano de estudo
        /// </summary>
        /// <param name="chancela">Objeto para ser chancelado</param>
        /// <param name="tokenEtapa">token definido do processo da etapa</param>
        /// <param name="desabilitarFiltro">Desabilita o filtro de HIERARQUIA_ENTIDADE_ORGANIZACIONAL</param>
        public void SalvarChancelaMatricula(ChancelaData chancelaData, string token, bool desabilitarFiltro = false)
        {
            SolicitacaoMatriculaDomainService.SalvarChancelaMatricula(chancelaData.Transform<ChancelaVO>(), token, desabilitarFiltro);
        }

        /// <summary>
        /// Realiza o processo de reabrir a chancela voltando as situações dos itens no histórico
        /// </summary>
        /// <param name="seq">Sequencial da solicitação de serviço</param>
        /// <returns>Sequencial da configuração etapa de chancela que reabriu</returns>
        public long ReabrirChancelaMatricula(long seq)
        {
            return SolicitacaoMatriculaDomainService.ReabrirChancelaMatricula(seq);
        }

        public void EfetivarMatricula(EfetivacaoMatriculaData model)
        {
            SolicitacaoMatriculaDomainService.EfetivarMatricula(model.Transform<EfetivacaoMatriculaVO>());
        }

        /// <summary>
        /// Realizar a efetivação da rematricula atualizando o histórico do aluno e os dados do SGP
        /// </summary>
        /// <param name="model">Dados da solicitação para efetivação</param>
        public void EfetivarRenovacaoMatricula(EfetivacaoMatriculaData model)
        {
            SolicitacaoMatriculaDomainService.EfetivarRenovacaoMatricula(model.Transform<EfetivacaoMatriculaVO>());
        }

        /// <summary>
        /// Verifica se existe solicitação de matrícula para solicitação de serviço, se não existir cria um solicitação de matrícula apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoMatriculaPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            SolicitacaoMatriculaDomainService.CriarSolicitacaoMatriculaPorSolicitacaoServico(seqSolicitacaoServico);
        }

        /// <summary>
        /// Busca o sequencial do processo etapa de acordo com a solicitação de matrícula e a configuração etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial solicitação matrícula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial configuração etapa</param>
        /// <returns>Retorna o sequencial do processo etapa</returns>
        public long BuscarProcessoEtapaPorSolicitacaoMatricula(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa)
        {
            return SolicitacaoMatriculaDomainService.BuscarProcessoEtapaPorSolicitacaoMatricula(seqSolicitacaoMatricula, seqConfiguracaoEtapa);
        }

        /// <summary>
        /// Buscar ciclo letivo do processor por solicitação matricula 
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <returns>Sequencial do ciclo letivo</returns>
        public long? BuscarCicloLetivoProcessoSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.BuscarCicloLetivoProcessoSolicitacaoMatricula(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// RN_MAT_066 - Procedimentos ao finalizar etapa
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração etapa</param>
        /// <param name="classificacaoSituacaoFinal">Classificação da situação final da etapa</param>
        public void ProcedimentosFinalizarEtapa(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, ClassificacaoSituacaoFinal classificacaoSituacaoFinal)
        {
            SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(seqSolicitacaoMatricula, seqConfiguracaoEtapa, classificacaoSituacaoFinal, null);
        }

        /// <summary>
        /// Cria as solicitações de renovação para os alunos do processo
        /// </summary>
        /// <param name="rematriculaJOBData">Parâmetros para criação das solicitações de renovação</param>
        public void CriarSolicitacoesRematricula(RematriculaJOBData rematriculaJOBData)
        {
            SolicitacaoMatriculaDomainService.CriarSolicitacoesRematricula(rematriculaJOBData.Transform<RematriculaJOBVO>());
        }

        /// <summary>
        /// Busca as solicitações de matrícula de uma determinada pessoa de acordo com o filtro informado
        /// </summary>
        /// <param name="solicitacaoMatriculaFiltroData">Filtro</param>
        /// <returns>Solicitações de matrícula</returns>
        public List<SolicitacaoMatriculaListaData> BuscarSolicitacoesMatriculaLista(SolicitacaoMatriculaFiltroData solicitacaoMatriculaFiltroData)
        {
            return SolicitacaoMatriculaDomainService.BuscarSolicitacoesMatriculaLista(solicitacaoMatriculaFiltroData.Transform<SolicitacaoMatriculaFilterSpecification>()).TransformList<SolicitacaoMatriculaListaData>();
        }

        public SolicitacaoMatriculaCabecalhoData BuscarCabecalhoMatricula(long seqSolicitacaoServico)
        {
            return SolicitacaoMatriculaDomainService.BuscarCabecalhoMatricula(seqSolicitacaoServico).Transform<SolicitacaoMatriculaCabecalhoData>();
        }

        /// <summary>
        /// Retorna os dados de renovação
        /// </summary>
        /// <param name="seq">Sequencial do aluno logado</param>
        /// <returns>Dados de renovação</returns>
        public DadosSolicitacaoMatriculaRenovacaoData BuscarDadosRematricula(DadosSolicitacaoMatriculaRenovacaoFiltroData filtro)
        {
            var dados = SolicitacaoMatriculaDomainService.BuscarDadosRematricula(filtro.SeqPessoaAtuacao, filtro.SeqPessoa);
            return new DadosSolicitacaoMatriculaRenovacaoData
            {
                DataFimRematricula = dados.DataFimRematricula,
                DataInicioRematricula = dados.DataInicioRematricula,
                ExisteRematriculaAberta = dados.ExisteRematriculaAberta,
                SeqSolicitacaoServico = dados.SeqSolicitacaoServico,
                TipoMatricula = dados.TipoMatricula,
                DescricaoProcesso = dados.DescricaoProcesso
            };
        }

        /// <summary>
        /// Efetivar a matricula de forma automatica
        /// </summary>
        /// <param name="filtro">Fitlro com o sequencial do historico de agendamento para o feedack</param>
        public void EfetivarRenovacaoMatriculaAutomatica(EfetivarRenovacaoMatriculaAutomaticaSATData filtro)
        {
            SolicitacaoMatriculaDomainService.EfetivarRenovacaoMatriculaAutomatica(filtro.Transform<EfetivarRenovacaoMatriculaAutomaticaSATVO>());
        }

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarChancelaExclusaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long, long)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados)
        {
            return SolicitacaoMatriculaDomainService.VerificarChancelaExclusaoAtividadesAcademicasComHistoricoEscolar(seqSolicitacaoMatricula, seqConfiguracaoEtapa, seqsSolicitacaoMatriculaItem, desativarFiltroDados);
        }

        /// <summary>
        /// Verifica se alguma atividade acadêmica da solicitação que esteja com situação finalizada com sucesso e possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarEfetivacaoAtividadesAcademicasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<long> seqsSolicitacaoMatriculaItem)
        {
            return SolicitacaoMatriculaDomainService.VerificarEfetivacaoAtividadesAcademicasComHistoricoEscolar(seqSolicitacaoMatricula, seqConfiguracaoEtapa, seqsSolicitacaoMatriculaItem);
        }

        /// <summary>
        /// Verifica se alguma turma da solicitação que esteja com situação finalizada com sucesso e pertença ao plano (ou seja, solicitação de exclusão), possui histórico escolar
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de serviço</param>
        /// <param name="desativarFiltroDados">Desativar filtro de dados ou não</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração da etapa</param>
        /// <param name="seqsSolicitacaoMatriculaItem">Sequencial dos itens da solicitação de matrícula e suas situações selecionadas na tela</param>
        /// <returns>Lista de descrição dos componentes afetados</returns>
        public List<string> VerificarChancelaExclusaoTurmasComHistoricoEscolar(long seqSolicitacaoMatricula, long seqConfiguracaoEtapa, List<(long, long)> seqsSolicitacaoMatriculaItem, bool desativarFiltroDados)
        {
            return SolicitacaoMatriculaDomainService.VerificarChancelaExclusaoTurmasComHistoricoEscolar(seqSolicitacaoMatricula, seqConfiguracaoEtapa, seqsSolicitacaoMatriculaItem, desativarFiltroDados);
        }

        public bool TemCodigoAdesao(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.TemCodigoAdesao(seqSolicitacaoMatricula);
        }

        public List<TurmaOfertadaData> BuscarTurmasGraduacaoSelecionadas(long seqSolicitacaoMatricula)
        {
            return SolicitacaoMatriculaDomainService.BuscarTurmasGraduacaoSelecionadas(seqSolicitacaoMatricula).TransformList<TurmaOfertadaData>();
        }

        public void SelecaoPlanoEstudoExcluirTurma(long seqSolicitacaoMatriculaItem, long seqSolicitacaoMatricula)
        {
            SolicitacaoMatriculaDomainService.SelecaoPlanoEstudoExcluirTurma(seqSolicitacaoMatriculaItem, seqSolicitacaoMatricula);
        }

        public void PlanoEstudosConsistirProsseguirEtapa(long seqSolicitacaoMatricula)
        {
            SolicitacaoMatriculaDomainService.PlanoEstudosConsistirProsseguirEtapa(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Retorna a lista de turmas que o Aluno pode se matricular. Este método não retorna turmas já matriculadas.
        /// </summary>
        public List<TurmaOfertadaData> RetornaListaTurmasOfertadas(long seqSolicitacaoMatricula, string descricaoTurma = null)
        {
            return SolicitacaoMatriculaDomainService.RetornaListaTurmasOfertadas(seqSolicitacaoMatricula, descricaoTurma).TransformList<TurmaOfertadaData>();
        }

        /// <summary>
        /// Salva as turams selecionadas na modal de seleção de turma no Plano de Estudo
        /// </summary>
        public void PlanoEstudoSalvarTurmasSelecionadas(long seqSolicitacaoMatricula, List<long?> seqTurmas)
        {
            SolicitacaoMatriculaDomainService.PlanoEstudoSalvarTurmasSelecionadas(seqSolicitacaoMatricula, seqTurmas);
        }
    }
}