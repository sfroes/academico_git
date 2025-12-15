using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.Seguranca.ServiceContract.Areas.APL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class EscalonamentoDomainService : AcademicoContextDomain<Escalonamento>
    {
        #region DomainService

        private ProcessoDomainService ProcessoDomainService { get => Create<ProcessoDomainService>(); }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService { get => Create<ProcessoEtapaDomainService>(); }

        private CicloLetivoTipoEventoDomainService CicloLetivoTipoEventoDomainService { get => Create<CicloLetivoTipoEventoDomainService>(); }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService { get => Create<HierarquiaEntidadeItemDomainService>(); }

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService { get => Create<GrupoEscalonamentoDomainService>(); }

        private SolicitacaoHistoricoSituacaoDomainService SolicitacaoHistoricoSituacaoDomainService { get => Create<SolicitacaoHistoricoSituacaoDomainService>(); }

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService { get => Create<SolicitacaoServicoEnvioNotificacaoDomainService>(); }

        private SolicitacaoHistoricoNavegacaoDomainService SolicitacaoHistoricoNavegacaoDomainService { get => Create<SolicitacaoHistoricoNavegacaoDomainService>(); }

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService { get => Create<ConfiguracaoEtapaPaginaDomainService>(); }

        private IngressanteDomainService IngressanteDomainService { get => Create<IngressanteDomainService>(); }

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService { get => Create<IngressanteHistoricoSituacaoDomainService>(); }

        private CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService { get => Create<CursoOfertaLocalidadeDomainService>(); }

        private CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService { get => Create<CursoOfertaLocalidadeTurnoDomainService>(); }

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => Create<ConfiguracaoEventoLetivoDomainService>(); }

        #endregion DomainService

        #region Services

        private IEtapaService IEtapaService { get => Create<IEtapaService>(); }

        private IAplicacaoService AplicacaoService { get => Create<IAplicacaoService>(); }

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion Services

        #region Constantes

        private const string GET_RESOURCE_BOTAO_INSTRUCTIONS_SITUACAO_LIBERADA = "botao_instructions_editar_situacao_liberada_vigente";

        private const string GET_RESOURCE_BOTAO_INSTRUCTIONS_SITUACAO_ENCERRADA = "botao_instructions_editar_situacao_encerrada";

        private const string GET_RESOURCE_BOTAO_INSTRUCTIONS_EXCLUIR_POR_GRUPO = "botao_instructions_excluir_por_grupo";

        #endregion Constantes

        /// <summary>
        /// Busca a lista de escalonamentos pelo sequencial do processo etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial processo etapa</param>
        /// <returns>Select com a lista de escalonamento</returns>
        public List<SMCDatasourceItem> BuscarEscalonamentoPorProcessoEtapaSelect(long seqProcessoEtapa)
        {
            var filtro = new EscalonamentoFilterSpecification() { SeqProcessoEtapa = seqProcessoEtapa };

            //Recupera os escalonamentos do processo etapa
            var result = this.SearchProjectionBySpecification(filtro, p => new
            {
                Seq = p.Seq,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim
            }).ToList();

            List<SMCDatasourceItem> listItem = new List<SMCDatasourceItem>();
            foreach (var item in result)
                listItem.Add(new SMCDatasourceItem() { Seq = item.Seq, Descricao = $"{item.DataInicio.ToShortDateString()} - {item.DataFim.ToShortDateString()}" });

            return listItem;
        }

        /// <summary>
        /// Busca a lista de pessoas da solicitação de serviço do grupo escalonamento do processo etapa
        /// </summary>
        /// <param name="filtro">Objeto de filtro com escalonamento</param>
        /// <returns>Objeto escalonamento com dados pessoais</returns>
        public ProcessoEtapaProcessamentoListarVO BuscarSeqsGrupoEscalonamentosPorProcessoEtapa(long seq)
        {
            var escalonamento = this.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(seq), p => new ProcessoEtapaProcessamentoListarVO()
            {
                SeqEscalonamento = p.Seq,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                SeqsGruposEscalonamento = p.GruposEscalonamento.Select(s => s.SeqGrupoEscalonamento).ToList()
            });

            return escalonamento;
        }

        /// <summary>
        /// Buscar escalonamentos de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de escalonamentos de um processo organizado por etapas</returns>
        public SMCPagerData<EscalonamentoListarVO> BuscarEscalonamentosPorProcesso(ProcessoFilterSpecification spec)
        {
            var total = 0;

            var includes = IncludesProcesso.Etapas
                           | IncludesProcesso.Etapas_Escalonamentos
                           | IncludesProcesso.GruposEscalonamento
                           | IncludesProcesso.Etapas_Escalonamentos_GruposEscalonamento
                           | IncludesProcesso.Etapas_Escalonamentos_GruposEscalonamento_Parcelas
                           | IncludesProcesso.Etapas_Escalonamentos_GruposEscalonamento_GrupoEscalonamento
                           | IncludesProcesso.Servico;

            var result = ProcessoDomainService.SearchBySpecification(spec, out total,
                                        includes).ToList();

            var retono = result.TransformList<EscalonamentoListarVO>();

            foreach (var item in retono)
            {
                item.SeqProcesso = (long)spec.Seq;

                foreach (var etapa in item.Etapas)
                {
                    var servico = result.FirstOrDefault(a => a.Seq == item.SeqProcesso)?.Servico;

                    if (servico != null)
                        etapa.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(servico.TokenPermissaoManutencaoProcesso);

                    foreach (var escalonamento in etapa.Escalonamentos)
                    {
                        escalonamento.HabilitaBtnComPermissaoManutencaoProcesso = etapa.HabilitaBtnComPermissaoManutencaoProcesso;

                        ///O escalonamento informa qual a situação da sua etapa
                        escalonamento.SituacaoEtapa = etapa.SituacaoEtapa;

                        var hoje = DateTime.Now;

                        ///Verifica se o escalonamento é vigente
                        if ((hoje >= escalonamento.DataInicio && (escalonamento.DataFim == null || hoje <= escalonamento.DataFim)))
                        {
                            escalonamento.Vigente = true;
                        }

                        ///Cria lista de string's com os grupos de escalonamentos
                        if (escalonamento.GruposEscalonamento != null && escalonamento.GruposEscalonamento.Count > 0)
                        {
                            escalonamento.DescricaoGruposEscalonamento = new List<string>();
                            int gruposComSolicitacaoServico = 0;

                            foreach (var grupo in escalonamento.GruposEscalonamento)
                            {
                                escalonamento.DescricaoGruposEscalonamento.Add(grupo.GrupoEscalonamento.Descricao);

                                var solicitacoesPorGrupo = this.ProcessoDomainService.PreencherModelo(new PosicaoConsolidadaFiltroVO() { SeqProcesso = spec.Seq, SeqGrupoEscalonamento = grupo.SeqGrupoEscalonamento });
                                gruposComSolicitacaoServico += (solicitacoesPorGrupo.SMCAny()) ? ((solicitacoesPorGrupo.FirstOrDefault().QuantidadeSolicitacoes > 0) ? 1 : 0) : 0;
                            }

                            escalonamento.ExisteSolicitacaoGrupoEscalonamento = gruposComSolicitacaoServico > 0 ? true : false;
                        }
                    }
                }
            }

            return new SMCPagerData<EscalonamentoListarVO>(retono, total);
        }

        /// <summary>
        /// Salvar um novo escalonamento
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        public long SalvarEscalonamento(Escalonamento modelo)
        {
            ///A "Data Fim" deverá ser maior ou igual a data ATUAL (do sistema)
            if (modelo.DataFim < DateTime.Now)
            {
                throw new EscalonamentoDataFimMenorDataAtualException();
            }

            var includes = IncludesProcessoEtapa.Processo | IncludesProcessoEtapa.Processo_UnidadesResponsaveis | IncludesProcessoEtapa.Escalonamentos | IncludesProcessoEtapa.Processo_Servico_TipoServico;

            var processoEtapa = this.ProcessoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(modelo.SeqProcessoEtapa), includes);

            if (processoEtapa.Processo.DataInicio > modelo.DataInicio)
                throw new EscalonamentoDataInicioMenorDataInicioProcessoException();

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                //Remoção das validações com a data do período financeiro conforme task 38259

                ///Se a data/hora fim do escalonamento for maior que a data/hora fim do processo,
                ///atualizar a data/hora fim do escalonamento em questão e a data/hora fim do processo.
                //var dataFimPeriodoFinanceiro = BuscarDataFimPeriodoFinanceiro(modelo.SeqProcessoEtapa);

                if (modelo.DataFim > processoEtapa.Processo.DataFim/* && modelo.DataFim <= dataFimPeriodoFinanceiro*/)
                {
                    var processo = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(processoEtapa.SeqProcesso));
                    processo.DataFim = modelo.DataFim;

                    this.ProcessoDomainService.SaveEntity(processo);
                }

                ///Senão se a data/hora fim do escalonamento for maior que o período financeiro do respectivo ciclo letivo
                ///do processo, abortar a operação e exibir a mensagem de erro: “Operação não permitida. A data/hora
                ///fim do escalonamento é maior que o período financeiro do ciclo letivo do processo.”
                //if (modelo.DataFim > dataFimPeriodoFinanceiro)
                //{
                //    throw new EscalonamentoDataFimMaiorDataFimPeriodoFinanceiroException();
                //}

                var includesEscalonamento = IncludesEscalonamento.GruposEscalonamento
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_HistoricosSituacao
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_HistoricosNavegacao
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao_DadosPessoais
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_PessoaAtuacao_EnderecosEletronicos
                                              | IncludesEscalonamento.ProcessoEtapa
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_ConfiguracaoProcesso
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_ConfiguracaoProcesso_Processo
                                              | IncludesEscalonamento.GruposEscalonamento_Parcelas
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa_Escalonamentos
                                              | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico_Etapas_ConfiguracaoEtapa_ProcessoEtapa_Escalonamentos_GruposEscalonamento;

                if (modelo.Seq > 0)
                {
                    var escalonamento = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), includesEscalonamento);

                    if (escalonamento.GruposEscalonamento.Count() > 0)
                    {
                        string nomeGruposQueEscalonamentoPertence = string.Empty;

                        foreach (var item in escalonamento.GruposEscalonamento)
                        {
                            nomeGruposQueEscalonamentoPertence += "<br />-" + item.GrupoEscalonamento.Descricao;
                        }

                        ///Se o parâmetro, da etapa em questão, ind_finalizacao_etapa_anterior for "Sim"
                        if (processoEtapa.FinalizacaoEtapaAnterior)
                        {
                            var includesProcesso = IncludesProcesso.Etapas | IncludesProcesso.Etapas_Escalonamentos | IncludesProcesso.Etapas_Escalonamentos_GruposEscalonamento_GrupoEscalonamento;
                            var todasEtapasProcesso = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(processoEtapa.Processo.Seq), includesProcesso).Etapas.OrderBy(o => o.Ordem).ToList();

                            for (int i = 0; i < todasEtapasProcesso.Count(); i++)
                            {
                                if (todasEtapasProcesso[i].Ordem == processoEtapa.Ordem)
                                {
                                    var etapaAnterior = i - 1;
                                    var etapaPosterior = i + 1;

                                    var seqsGruposEscalonamentosAtual = escalonamento.GruposEscalonamento.Select(s => s.SeqGrupoEscalonamento).ToList();

                                    //var listaTeste = todasEtapasProcesso[etapaAnterior].Escalonamentos.Select(se => se.GruposEscalonamento
                                    //                                                   .Where(wg => seqsGruposEscalonamentosAtual.Contains(wg.SeqGrupoEscalonamento)))
                                    //                                                   .Where(w => w.Any()).SelectMany(s => s.SelectMany(sm => sm.GrupoEscalonamento.Itens))
                                    //                                                   .OrderByDescending(o => o.Escalonamento.DataFim).FirstOrDefault().Escalonamento.DataFim;

                                    ///Verifica se existe etapa anterior
                                    if (etapaAnterior >= 0)
                                    {
                                        ///Se o parâmetro, da etapa em questão, ind_finalizacao_etapa_anterior for “Sim”, a data / hora início
                                        ///do escalonamento deverá ser maior que a maior data / hora fim dos escalonamentos da etapa anterior se
                                        ///houver.Em caso de violação, abortar operação e exibir a mensagem de erro: “Operação não permitida.
                                        ///A etapa deste escalonamento não pode iniciar antes da finalização da etapa anterior.” -- Do mesmo grupo de escalonamento.
                                        if (modelo.DataInicio <= todasEtapasProcesso[etapaAnterior].Escalonamentos.Select(se => se.GruposEscalonamento
                                                                                       .Where(wg => seqsGruposEscalonamentosAtual.Contains(wg.SeqGrupoEscalonamento)))
                                                                                       .Where(w => w.Any()).SelectMany(s => s.SelectMany(sm => sm.GrupoEscalonamento.Itens))
                                                                                       .OrderByDescending(o => o.Escalonamento.DataFim).FirstOrDefault().Escalonamento.DataFim)
                                        {
                                            throw new EscalonamentoDataInicioEscalonamentoMaiorDataFimEscalonamentoAnteriorException();
                                        }
                                    }

                                    ///Verifica se existe etapa posterior
                                    if (etapaPosterior < todasEtapasProcesso.Count())
                                    {
                                        ///Se o parâmetro, da etapa seguinte à etapa em questão, ind_finalizacao_etapa_anterior for “Sim”,
                                        ///a data / hora fim do escalonamento em questão deverá ser menor que a menor data / hora início dos
                                        ///escalonamentos da etapa seguinte, se houver.Em caso de violação, abortar operação e exibir a
                                        ///mensagem de erro: “Operação não permitida.A etapa deste escalonamento não pode finalizar depois
                                        ///do início da etapa seguinte.” -- Do mesmo grupo de escalonamento.
                                        if (todasEtapasProcesso[etapaPosterior].FinalizacaoEtapaAnterior)
                                        {
                                            if (modelo.DataFim >= todasEtapasProcesso[etapaAnterior].Escalonamentos.Select(se => se.GruposEscalonamento
                                                                                       .Where(wg => seqsGruposEscalonamentosAtual.Contains(wg.SeqGrupoEscalonamento)))
                                                                                       .Where(w => w.Any()).SelectMany(s => s.SelectMany(sm => sm.GrupoEscalonamento.Itens))
                                                                                       .OrderByDescending(o => o.Escalonamento.DataFim).FirstOrDefault().Escalonamento.DataFim)
                                            {
                                                throw new EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException(nomeGruposQueEscalonamentoPertence);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in escalonamento.GruposEscalonamento)
                            {
                                var includesGrupoEscalonamento = IncludesGrupoEscalonamento.Itens | IncludesGrupoEscalonamento.Itens_Escalonamento | IncludesGrupoEscalonamento.Itens_Escalonamento_ProcessoEtapa;
                                var grupoEscalonamento = this.GrupoEscalonamentoDomainService.SearchByKey(new SMCSeqSpecification<GrupoEscalonamento>(item.SeqGrupoEscalonamento), includesGrupoEscalonamento);

                                var itensGrupoEscalonamento = grupoEscalonamento.Itens.OrderBy(o => o.Escalonamento.ProcessoEtapa.Ordem).ToList();

                                for (int i = 0; i < itensGrupoEscalonamento.Count(); i++)
                                {
                                    if (itensGrupoEscalonamento[i].Escalonamento.ProcessoEtapa.Ordem == escalonamento.ProcessoEtapa.Ordem)
                                    {
                                        var etapaAnterior = i - 1;
                                        var etapaPosterior = i + 1;

                                        ///Verifica se existe etapa anterior
                                        if (etapaAnterior >= 0)
                                        {
                                            ///a.A data/ hora fim do escalonamento em questão não pode ser menor do que a data / hora fim
                                            ///da etapa anterior, se houver.Em caso de violação, abortar a operação e exibir a mensagem de erro:
                                            if (modelo.DataFim < itensGrupoEscalonamento[etapaAnterior].Escalonamento.DataFim)
                                            {
                                                throw new EscalonamentoEdicaoDataFimMaiorDataFimAnteriorException(nomeGruposQueEscalonamentoPertence);
                                            }

                                            ///b.A data/ hora início do escalonamento em questão não pode ser menor do que a data / hora início da
                                            ///etapa anterior, se houver.Em caso de violação, abortar a operação e exibir a mensagem de erro:
                                            if (modelo.DataInicio < itensGrupoEscalonamento[etapaAnterior].Escalonamento.DataInicio)
                                            {
                                                throw new EscalonamentoEdicaoDataInicioMaiorDataInicioAnteriorException(nomeGruposQueEscalonamentoPertence);
                                            }
                                        }

                                        ///Verifica se existe etapa posterior
                                        if (etapaPosterior < itensGrupoEscalonamento.Count())
                                        {
                                            ///c.A data/ hora fim do escalonamento em questão não pode ser maior do que a data / hora fim da
                                            ///etapa posterior, se houver.Em caso de violação, abortar a operação e exibir a mensagem de erro:
                                            if (modelo.DataFim > itensGrupoEscalonamento[etapaPosterior].Escalonamento.DataFim)
                                            {
                                                throw new EscalonamentoEdicaoDataFimMenorDataFimPosteriorException(nomeGruposQueEscalonamentoPertence);
                                            }

                                            ///d.A data/ hora início do escalonamento em questão não pode ser maior do que a data / hora início da
                                            ///etapa posterior, se houver.Em caso de violação, abortar a operação e exibir a mensagem de erro:
                                            if (modelo.DataInicio > itensGrupoEscalonamento[etapaPosterior].Escalonamento.DataInicio)
                                            {
                                                throw new EscalonamentoEdicaoDataInicioMenorDataInicioPosteriorException(nomeGruposQueEscalonamentoPertence);
                                            }

                                            ///Se o parâmetro, da etapa seguinte à etapa em questão, ind_finalizacao_etapa_anterior for “Sim”,
                                            ///a data / hora fim do escalonamento em questão deverá ser menor que a menor data / hora início dos
                                            ///escalonamentos da etapa seguinte, se houver.Em caso de violação, abortar operação e exibir a
                                            ///mensagem de erro: “Operação não permitida.A etapa deste escalonamento não pode finalizar depois
                                            ///do início da etapa seguinte.”.
                                            if (itensGrupoEscalonamento[etapaPosterior].Escalonamento.ProcessoEtapa.FinalizacaoEtapaAnterior)
                                            {
                                                if (modelo.DataFim >= itensGrupoEscalonamento[etapaPosterior].Escalonamento.DataInicio)
                                                {
                                                    throw new EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException(nomeGruposQueEscalonamentoPertence);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        ///4.Esta regra não deve ser considerada para processos do serviço que possuem um dos tokens: 
                        ///SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU, 
                        ///SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA e MATRICULA_REABERTURA.              
                        if (processoEtapa.Processo.Servico.Token != TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU &&
                            processoEtapa.Processo.Servico.Token != TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA &&
                            processoEtapa.Processo.Servico.Token != TOKEN_SERVICO.MATRICULA_REABERTURA)
                        {
                            foreach (var item in escalonamento.GruposEscalonamento)
                            {
                                if (item.Parcelas.SMCAny())
                                {
                                    foreach (var parcela in item.Parcelas)
                                    {
                                        ///Para os demais processos: 
                                        ///A "Data de Vencimento" de todas as parcelas deverá ser MAIOR ou IGUAL do que a 
                                        ///"Data/Hora Fim" do escalonamento em questão e MENOR do que a "Data/Hora Fim" do 
                                        ///processo em questão. Caso NÃO ocorra, abortar a operação e exibir mensagem de impeditiva: 
                                        ///"Configuração não permitida. A data de vencimento de todas as parcelas deverá ser 
                                        ///maior ou igual do que a data/hora fim do escalonamento em questão e menor do que a 
                                        ///data/hora fim do processo em questão."
                                        if (parcela.DataVencimentoParcela.Date < modelo.DataFim.Date ||
                                            parcela.DataVencimentoParcela.Date >= processoEtapa.Processo.DataFim.GetValueOrDefault().Date)
                                        {
                                            throw new EscalonamentoDataFimParcelaMenorDataFimEscalonamento();
                                        }
                                    }

                                    if (item.Parcelas.Count > 1)
                                    {
                                        item.Parcelas = item.Parcelas.OrderBy(p => p.NumeroParcela).ToList();

                                        for (int i = 0; i < item.Parcelas.Count - 1; i++)
                                        {
                                            ///Para cada parcela cadastrada, verificar se a "Data de Vencimento" da parcela é MENOR 
                                            ///que a "Data de Vencimento" da parcela seguinte, levando em consideração o número da 
                                            ///parcela. Caso NÃO ocorra, abortar a operação e exibir a seguinte mensagem de impeditiva: 
                                            ///"Configuração não permitida. A data de vencimento das parcelas deverá levar em 
                                            ///consideração o número da parcela, isto é, parcela com o número menor do que a outra 
                                            ///parcela deverá conter a data de vencimento menor também."
                                            if (item.Parcelas[i].DataVencimentoParcela >= item.Parcelas[i + 1].DataVencimentoParcela)
                                            {
                                                throw new GrupoEscalonamentoItemParcelaDataVencimentoMaiorProximaParcelaException();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #region Conforme tarefa Task 29766, não deve mais ser executada essa regra.

                    ///4.Se o escalonamento estiver sendo prorrogado(aumento da data / fim) e estiver associado a grupo(s)
                    ///de escalonamento, verificar se existem solicitações do grupo em questão, cuja etapa atual da
                    ///solicitação seja a etapa associada ao escalonamento prorrogado e, que a situação atual(SGF) esteja
                    ///parametrizada para ser a final do processo e a classificação da situação seja Cancelada ou
                    ///Finalizada sem sucesso.Se existirem solicitações nessas condições e, ao liberar as etapas, executar
                    ///a regra RN_SRC_037 -Grupo de escalonamento -Consistências prorrogação de escalonamento.
                    //if (modelo.DataFim > escalonamento.DataFim)
                    //{
                    //    foreach (var grupo in escalonamento.GruposEscalonamento)
                    //    {
                    //        foreach (var solicitacao in grupo.GrupoEscalonamento.SolicitacoesServico)
                    //        {
                    //            foreach (var etapa in solicitacao.Etapas)
                    //            {
                    //                var historico = etapa.HistoricosSituacao.OrderByDescending(o => o.DataInclusao).FirstOrDefault();

                    //                if (etapa.ConfiguracaoEtapa.SeqProcessoEtapa == modelo.SeqProcessoEtapa)
                    //                {
                    //                    var spec = new IngressanteHistoricoSituacaoFilterSpecification() { SeqIngressante = solicitacao.SeqPessoaAtuacao };
                    //                    var ingressanteHistoricoSituacao = this.IngressanteHistoricoSituacaoDomainService.SearchBySpecification(spec).OrderByDescending(o => o.DataInclusao).FirstOrDefault();

                    //                    if (solicitacao.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno
                    //                        || (solicitacao.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Ingressante
                    //                            && ingressanteHistoricoSituacao.SituacaoIngressante == SituacaoIngressante.AptoMatricula))
                    //                    {
                    //                        var nomeSolicitante = string.IsNullOrEmpty(solicitacao.PessoaAtuacao.DadosPessoais.NomeSocial) ? solicitacao.PessoaAtuacao.DadosPessoais.Nome : solicitacao.PessoaAtuacao.DadosPessoais.NomeSocial;
                    //                        var seqSolicitacaoServico = solicitacao.Seq;
                    //                        var descricaoProcesso = solicitacao.ConfiguracaoProcesso.Processo.Descricao;
                    //                        string etapaSolicitacao = string.Empty;
                    //                        var seqsGrupoEscalonamento = this.SearchProjectionByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), p => p.GruposEscalonamento.Select(s => s.SeqGrupoEscalonamento));

                    //                        foreach (var item in solicitacao.Etapas.OrderBy(o => o.ConfiguracaoEtapa.ProcessoEtapa.Ordem))
                    //                        {
                    //                            if (item.ConfiguracaoEtapa.ProcessoEtapa.Ordem == etapa.ConfiguracaoEtapa.ProcessoEtapa.Ordem)
                    //                            {
                    //                                ///Verifica se a etapa em questão é a mesma que está sendo alterado ou a posterior
                    //                                var dataInicioProcessoEtapa = (modelo.SeqProcessoEtapa == item.ConfiguracaoEtapa.SeqProcessoEtapa) ? modelo.DataInicio : item.ConfiguracaoEtapa.ProcessoEtapa.Escalonamentos.SelectMany(s => s.GruposEscalonamento.Where(w => seqsGrupoEscalonamento.Contains(w.SeqGrupoEscalonamento))).FirstOrDefault().Escalonamento.DataInicio;
                    //                                var dataFimProcessoEtapa = (modelo.SeqProcessoEtapa == item.ConfiguracaoEtapa.SeqProcessoEtapa) ? modelo.DataFim : item.ConfiguracaoEtapa.ProcessoEtapa.Escalonamentos.SelectMany(s => s.GruposEscalonamento.Where(w => seqsGrupoEscalonamento.Contains(w.SeqGrupoEscalonamento))).FirstOrDefault().Escalonamento.DataFim;

                    //                                etapaSolicitacao += $"{item.ConfiguracaoEtapa.ProcessoEtapa.DescricaoEtapa} - {dataInicioProcessoEtapa} a {dataFimProcessoEtapa} <br />";

                    //                                ///Preenche com as etapas posteriores
                    //                                var etapasPosteriores = this.ProcessoEtapaDomainService.BuscarProcessoEtapasPosteriores(item.ConfiguracaoEtapa.SeqProcessoEtapa).SelectMany(w => w.Escalonamentos.SelectMany(sm => sm.GruposEscalonamento.Where(ww => seqsGrupoEscalonamento.Contains(ww.SeqGrupoEscalonamento)))).ToList();

                    //                                foreach (var etapaPosterior in etapasPosteriores)
                    //                                {
                    //                                        etapaSolicitacao += $"{etapaPosterior.Escalonamento.ProcessoEtapa.DescricaoEtapa} - {etapaPosterior.Escalonamento.DataInicio} a {etapaPosterior.Escalonamento.DataFim} <br />";
                    //                                }                                                    
                    //                            }
                    //                        }

                    //                        Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                    //                        dadosMerge.Add("{{NOM_PESSOA}}", nomeSolicitante);
                    //                        dadosMerge.Add("{{DSC_PROCESSO}}", descricaoProcesso);
                    //                        dadosMerge.Add("{{DSC_ETAPA_SOLICITACAO}}", etapaSolicitacao);

                    //                        this.SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotivicacaoSolicitacaoServico(seqSolicitacaoServico, TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PERIODO_VIGENCIA, dadosMerge);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    #endregion
                }

                if (processoEtapa.Processo.Servico.Token == TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA)
                {
                    //Se continuar dando erro só verifica se é a primeira etapa, ProcessoEtapa.Ordem == 1                    
                    var listaEtapasOrdenadas = this.ProcessoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Processo>(processoEtapa.SeqProcesso), x => x.Etapas).OrderBy(o => o.Ordem).ToList();
                    var etapaDeMenorOrdem = listaEtapasOrdenadas[0];

                    ///5.Para os processos do serviço que possui o token SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA 
                    ///e o escalonamento alterado é da etapa de menor ordem, considerar a seguinte regra:
                    if (processoEtapa.Seq == etapaDeMenorOrdem.Seq)
                    {
                        var seqEntidadeResponsavel = processoEtapa.Processo.UnidadesResponsaveis.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).SeqEntidadeResponsavel;
                        var cursoOfertaLocalidades = CursoOfertaLocalidadeDomainService.BuscarCursoOfertasLocalidadeAtivasPorEntidadeResponsavel(seqEntidadeResponsavel);

                        if (cursoOfertaLocalidades.Any() && processoEtapa.Processo.SeqCicloLetivo.HasValue)
                        {
                            var cursoOfertaLocalidade = cursoOfertaLocalidades.First();
                            var cursoOfertaLocalidadeTurnos = CursoOfertaLocalidadeTurnoDomainService.BuscarTurnosPorLocalidadeCusroOfertaSelect(cursoOfertaLocalidade.RecuperarSeqLocalidade(), cursoOfertaLocalidade.SeqCursoOferta);

                            if (cursoOfertaLocalidadeTurnos.Any())
                            {
                                var seqCursoOfertaLocalidadeTurno = cursoOfertaLocalidadeTurnos.First().Seq;

                                var datasEventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(processoEtapa.Processo.SeqCicloLetivo.Value, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                                ///5.1. Se a data início do escalonamento da etapa for menor que a data início do [ciclo letivo]*
                                if (modelo.DataInicio.Date < datasEventoLetivo.DataInicio.Date)
                                {
                                    ///5.1.1. A data fim deverá ser maior ou igual a data início do escalonamento e menor que a 
                                    ///data início do ciclo letivo. Caso isso não ocorra, abortar a operação e exibir a seguinte mensagem:
                                    ///"Não é possível prosseguir. O escalonamento deve finalizar antes da data início do ciclo letivo."
                                    if (modelo.DataFim < modelo.DataInicio || modelo.DataFim.Date >= datasEventoLetivo.DataInicio.Date)
                                        throw new EscalonamentoFinalizarAntesInicioCicloLetivoException();

                                    ///5.1.2. Verificar se o escalonamento possui configuração de parcela. Caso possua, a data de
                                    ///vencimento das parcelas deve ser maior ou igual a data fim do respectivo escalonamento e 
                                    ///menor ou igual a data início do [ciclo letivo]*. Caso isto não ocorra, abortar a operação 
                                    ///e exibir a seguinte mensagem impeditiva:
                                    ///"Operação não permitida. A data de vencimento da parcela deverá ser maior ou igual a 
                                    ///data/hora fim do escalonamento e menor ou igual a data início do ciclo letivo."
                                    if (modelo.Seq > 0)
                                    {
                                        var escalonamento = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), includesEscalonamento);

                                        if (escalonamento.GruposEscalonamento.Count() > 0)
                                        {
                                            foreach (var item in escalonamento.GruposEscalonamento)
                                            {
                                                if (item.Parcelas.SMCAny())
                                                {
                                                    foreach (var parcela in item.Parcelas)
                                                    {
                                                        if (parcela.DataVencimentoParcela.Date < modelo.DataFim.Date ||
                                                            parcela.DataVencimentoParcela.Date > datasEventoLetivo.DataInicio.Date)
                                                            throw new EscalonamentoDataVencimentoParcelaMaiorDataFimException();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                ///5.2. Se a data início do escalonamento estiver compreendida em um mês do [ciclo letivo]*
                                if (modelo.DataInicio.Date >= datasEventoLetivo.DataInicio.Date && modelo.DataInicio.Date <= datasEventoLetivo.DataFim.Date)
                                {
                                    #region Recuperando os meses do ciclo letivo

                                    //Lista que terá os meses do ciclo letivo
                                    List<int> mesesCicloLetivo = new List<int>() { datasEventoLetivo.DataInicio.Month };

                                    //Variáveis para auxiliar a verificar qual o último mês do ciclo
                                    var achouMesFimCiclo = false;
                                    var contadorMeses = 1;

                                    //Recupera os meses do ciclo, se a data for menor ou igual que a data fim do ciclo, ela será validada
                                    while (!achouMesFimCiclo)
                                    {
                                        var dataVerificar = datasEventoLetivo.DataInicio.AddMonths(contadorMeses);

                                        /*Validação para adicionar na lista somente as datas que são menores que a data fim
                                        Exemplo: se a data inicio do ciclo for 10/01 e a data fim 01/08 por exemplo, considera que o
                                        último mês do ciclo seria o mês 07, pois considera quando completa o mês considerando o dia*/
                                        if (dataVerificar <= datasEventoLetivo.DataFim)
                                            mesesCicloLetivo.Add(dataVerificar.Month);
                                        else
                                            achouMesFimCiclo = true;

                                        contadorMeses++;
                                    }

                                    var ultimoMesCiclo = mesesCicloLetivo.Last();

                                    var dataInicioAjustadaMesAno = datasEventoLetivo.DataInicio.Year * 12 + datasEventoLetivo.DataInicio.Month;
                                    var dataFimAjustadaMesAno = datasEventoLetivo.DataFim.Year * 12 + datasEventoLetivo.DataFim.Month;
                                    var qtdeMesesCicloLetivo = dataFimAjustadaMesAno - dataInicioAjustadaMesAno;
                                    if (datasEventoLetivo.DataInicio.Day > datasEventoLetivo.DataFim.Day)
                                    {
                                        qtdeMesesCicloLetivo--;
                                    }

                                    //Trecho para considerar a quantidade de meses do ciclo letivo contando o primeiro mês 
                                    qtdeMesesCicloLetivo = mesesCicloLetivo.Count();

                                    #endregion

                                    ///5.2.1. Verificar se a data fim do escalonamento está no mesmo mês que a data início. 
                                    ///Caso isso não ocorra, abortar a operação e exibir a seguinte mensagem: 
                                    ///"Não é possível prosseguir. A data início e a data fim devem estar no mesmo mês."
                                    if (modelo.DataFim.Month != modelo.DataInicio.Month)
                                        throw new EscalonamentoDataFimEscalonamentoForaMesmoMesException();

                                    ///5.2.2.Verificar se o escalonamento está associado a um grupo. Se sim:
                                    if (modelo.Seq > 0)
                                    {
                                        var escalonamento = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), includesEscalonamento);

                                        if (escalonamento.GruposEscalonamento.Count() > 0)
                                        {
                                            ///5.2.2.1. Verificar se a data fim do escalonamento está no último mês do 
                                            ///[ciclo letivo]*. Se estiver, o fator de divisão só pode ser igual a 1. 
                                            ///Caso isto não ocorra, abortar a operação e exibir a seguinte mensagem:
                                            ///"Não é possível prosseguir. As parcelas não podem ultrapassar o fim do ciclo 
                                            ///letivo, portanto de acordo com a data fim informada para o escalonamento, 
                                            ///o fator de divisão deve ser igual a 1."
                                            if (modelo.DataFim.Month == ultimoMesCiclo)
                                            {
                                                foreach (var item in escalonamento.GruposEscalonamento)
                                                {
                                                    if (item.GrupoEscalonamento.NumeroDivisaoParcelas.HasValue &&
                                                        item.GrupoEscalonamento.NumeroDivisaoParcelas.Value != 1)
                                                        throw new EscalonamentoParcelasUltrapassamFimCicloLetivoException();
                                                }
                                            }
                                            ///5.2.2.1.1. Se não estiver no último mês, verificar se o fator de divisão do grupo 
                                            ///é menor ou igual a quantidade de meses do [ciclo letivo]* menos a ordem do mês 
                                            ///(em número natural) no ciclo letivo, em que se encontra a data fim do 
                                            ///escalonamento. Caso isto não ocorra, abortar a operação e exibir a seguinte 
                                            ///mensagem:
                                            ///"Não é possível prosseguir. As parcelas não podem ultrapassar o fim do ciclo 
                                            ///letivo, portanto de acordo com a data fim informada para o escalonamento, o 
                                            ///fator de divisão deve ser menor ou igual a [quantidade de meses do ciclo letivo – 
                                            ///ordem do mês no ciclo letivo]."
                                            else
                                            {
                                                var ordemMesCicloLetivoDataFimEscalonamento = 0;

                                                for (int i = 0; i < mesesCicloLetivo.Count(); i++)
                                                {
                                                    var mes = mesesCicloLetivo[i];
                                                    var auxiliarIndice = i;

                                                    if (mes == modelo.DataFim.Month)
                                                        ordemMesCicloLetivoDataFimEscalonamento = auxiliarIndice + 1;
                                                }

                                                var qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento = qtdeMesesCicloLetivo - ordemMesCicloLetivoDataFimEscalonamento;

                                                foreach (var item in escalonamento.GruposEscalonamento)
                                                {
                                                    if (item.GrupoEscalonamento.NumeroDivisaoParcelas.HasValue &&
                                                        item.GrupoEscalonamento.NumeroDivisaoParcelas.Value > qtdeMesesCicloMenosOrdemMesCicloDataFimEscalonamento)
                                                        throw new EscalonamentoParcelasUltrapassamFimCicloLetivoFatorDivisaoException();
                                                }
                                            }
                                        }
                                    }

                                    ///5.2.3. Verificar se o escalonamento possui configuração de parcela. Caso possua:
                                    if (modelo.Seq > 0)
                                    {
                                        var escalonamento = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), includesEscalonamento);

                                        if (escalonamento.GruposEscalonamento.Count() > 0)
                                        {
                                            foreach (var item in escalonamento.GruposEscalonamento)
                                            {
                                                if (item.Parcelas.SMCAny())
                                                {
                                                    foreach (var parcela in item.Parcelas)
                                                    {
                                                        ///5.2.3.1. Se a data fim do escalonamento estiver no último mês do 
                                                        ///[ciclo letivo]* do processo, a data de vencimento da parcela deverá 
                                                        ///ser maior ou igual a data fim do escalonamento e menor que a data fim 
                                                        ///do ciclo letivo. Caso isto não ocorra, abortar a operação e exibir a 
                                                        ///seguinte mensagem de erro:
                                                        ///"Configuração não permitida. A data de vencimento da parcela deverá 
                                                        ///ser maior ou igual à data/hora fim do escalonamento e menor que a 
                                                        ///data fim do ciclo letivo do processo."
                                                        if (modelo.DataFim.Month == ultimoMesCiclo)
                                                        {
                                                            if (parcela.DataVencimentoParcela.Date < modelo.DataFim.Date ||
                                                                parcela.DataVencimentoParcela.Date >= datasEventoLetivo.DataFim.Date)
                                                                throw new EscalonamentoDataVencimentoParcelaDeveSerMaiorDataEscalonamentoException();
                                                        }
                                                        ///5.2.3.2. Se a data fim do escalonamento estiver nos demais meses do 
                                                        ///[ciclo letivo]*, a data de vencimento da parcela deverá ser maior ou 
                                                        ///igual a data fim do escalonamento e menor ou igual ao dia primeiro do 
                                                        ///mês subsequente à data fim do escalonamento. Caso isto não ocorra, 
                                                        ///abortar a operação e exibir a seguinte mensagem de erro:
                                                        ///"Configuração não permitida. A data de vencimento da parcela deverá 
                                                        ///ser maior ou igual à data/hora fim do escalonamento e menor ou igual 
                                                        ///ao dia primeiro do mês subsequente à data fim do escalonamento."
                                                        else
                                                        {
                                                            var dataMesSubsequenteDataFimEscalonamento = modelo.DataFim.AddMonths(1);
                                                            var dataPrimeiroDiaMesSubsequenteDataFimEscalonamento = new DateTime(dataMesSubsequenteDataFimEscalonamento.Year, dataMesSubsequenteDataFimEscalonamento.Month, 1);

                                                            if (parcela.DataVencimentoParcela.Date < modelo.DataFim.Date ||
                                                                parcela.DataVencimentoParcela.Date > dataPrimeiroDiaMesSubsequenteDataFimEscalonamento.Date)
                                                                throw new EscalonamentoDataVencimentoParcelaDeveSerMaiorEscalonamentoPrimeiroDiaException();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                ///Se o parâmetro, da etapa em questão, ind_finalizacao_etapa_anterior for “Sim”
                if (processoEtapa.FinalizacaoEtapaAnterior)
                {
                    var includesProcesso = IncludesProcesso.Etapas | IncludesProcesso.Etapas_Escalonamentos;
                    var todasEtapasProcesso = this.ProcessoDomainService.SearchByKey(new SMCSeqSpecification<Processo>(processoEtapa.Processo.Seq), includesProcesso).Etapas.OrderBy(o => o.Ordem).ToList();

                    for (int i = 0; i < todasEtapasProcesso.Count(); i++)
                    {
                        if (todasEtapasProcesso[i].Ordem == processoEtapa.Ordem)
                        {
                            var etapaAnterior = i - 1;
                            var etapaPosterior = i + 1;

                            ///Verifica se existe etapa anterior
                            //if (etapaAnterior >= 0)
                            //{
                            //    ///Se o parâmetro, da etapa em questão, ind_finalizacao_etapa_anterior for “Sim”, a data / hora início
                            //    ///do escalonamento deverá ser maior que a maior data / hora fim dos escalonamentos da etapa anterior se
                            //    ///houver.Em caso de violação, abortar operação e exibir a mensagem de erro: “Operação não permitida.
                            //    ///A etapa deste escalonamento não pode iniciar antes da finalização da etapa anterior.”.
                            //    if (modelo.DataInicio < todasEtapasProcesso[etapaAnterior].Escalonamentos.OrderByDescending(o => o.DataFim).FirstOrDefault().DataFim)
                            //    {
                            //        throw new EscalonamentoDataInicioEscalonamentoMaiorDataFimEscalonamentoAnteriorException();
                            //    }
                            //}

                            /////Verifica se existe etapa posterior
                            //if (etapaPosterior < todasEtapasProcesso.Count())
                            //{
                            //    ///Se o parâmetro, da etapa seguinte à etapa em questão, ind_finalizacao_etapa_anterior for “Sim”,
                            //    ///a data / hora fim do escalonamento em questão deverá ser menor que a menor data / hora início dos
                            //    ///escalonamentos da etapa seguinte, se houver.Em caso de violação, abortar operação e exibir a
                            //    ///mensagem de erro: “Operação não permitida.A etapa deste escalonamento não pode finalizar depois
                            //    ///do início da etapa seguinte.”.
                            //    if (todasEtapasProcesso[etapaPosterior].FinalizacaoEtapaAnterior)
                            //    {
                            //        if (modelo.DataFim > todasEtapasProcesso[etapaPosterior].Escalonamentos.OrderBy(o => o.DataInicio).FirstOrDefault().DataInicio)
                            //        {
                            //            throw new EscalonamentoDataFimEscalonamentoMenorDataInicioEscalonamentoAnteriorException(nomeGruposQueEscalonamentoPertence);
                            //        }
                            //    }
                            //}
                        }
                    }
                }

                this.SaveEntity(modelo);

                ///Rollback caso alguma das funções provoquem erro
                unitOfWork.Commit();
            }

            return modelo.Seq;
        }

        /// <summary>
        /// Buscar data fim do periodo financeiro de um respectivo ciclo letivo de um processo, segundo RN_SRC_036 CONSISTÊNCIAS NA INCLUSÃO / ALTERAÇÃO
        /// </summary>
        /// <param name="modelo">Modelo de escalonamento</param>
        /// <returns>Data fim de periodo financeiro</returns>
        public DateTime BuscarDataFimPeriodoFinanceiro(long seqProcessoEtapa)
        {
            var includes = IncludesProcessoEtapa.Processo
                         | IncludesProcessoEtapa.Processo_CicloLetivo
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_EventosLetivos
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_EventosLetivos_NiveisEnsino
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_InstituicaoTipoEvento
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_Parametros
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_Parametros_InstituicaoTipoEventoParametro
                         | IncludesProcessoEtapa.Processo_Configuracoes
                         | IncludesProcessoEtapa.Processo_Configuracoes_NiveisEnsino
                         | IncludesProcessoEtapa.Processo_CicloLetivo_TiposEvento_EventosLetivos_ParametrosEntidade
                         | IncludesProcessoEtapa.Processo_UnidadesResponsaveis
                         | IncludesProcessoEtapa.Processo_Configuracoes_Cursos
                         | IncludesProcessoEtapa.Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno
                         | IncludesProcessoEtapa.Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade
                         | IncludesProcessoEtapa.Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades
                         | IncludesProcessoEtapa.Processo_Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior;

            var dados = this.ProcessoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), includes);

            ///Entidades válidas como responsáveis
            var entidadesResponsaveis = this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(dados.Processo.UnidadesResponsaveis.Select(s => s.SeqEntidadeResponsavel))
                                                                                    .Select(s => s.SeqEntidade).ToList();

            ///Localidades do processo validas
            var localidadesResponsaveis = dados.Processo.Configuracoes.SelectMany(s => s.Cursos.Select(sc => sc.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.First().ItemSuperior.SeqEntidade)).ToList();

            ///Curso oferta localidades do processo validas
            var cursoOfertaLocalidadesResponsaveis = dados.Processo.Configuracoes.SelectMany(s => s.Cursos.Select(sc => sc.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq)).ToList();

            ///Selecionar os niveis de ensino do processo
            var niveisEnsinoProcesso = dados.Processo.Configuracoes.SelectMany(s => s.NiveisEnsino).Select(s => s.SeqNivelEnsino).Distinct();

            ///Ciclo letivo periodo financeiro
            var ciclosLetivosTipoEvento = dados.Processo.CicloLetivo.TiposEvento.Where(w => w.InstituicaoTipoEvento.Token == TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO)
                                                                 .Select(s => s);

            ///Todos os TIPOS DE EVENTO com os EVENTOS LETIVOS que contenham todos os NIVEIS DE ENSINO do PROCESSO
            var eventosPorTipo = ciclosLetivosTipoEvento.Select(s => new
            {
                Tipo = s,
                Eventos = s.EventosLetivos
                                .Where(ae => niveisEnsinoProcesso
                                        .All(al => ae.NiveisEnsino.Select(sn => sn.Seq).Contains(al)))
            });

            foreach (var tipoEventoLetivo in eventosPorTipo)
            {
                var eventoLetivoValidos = tipoEventoLetivo.Eventos;
                foreach (var parametro in tipoEventoLetivo.Tipo.Parametros)
                {
                    switch (parametro.InstituicaoTipoEventoParametro.TipoParametroEvento)
                    {
                        case Common.Areas.CAM.Enums.TipoParametroEvento.Localidade:
                            eventoLetivoValidos = eventoLetivoValidos.Where(w => w.ParametrosEntidade.Any(a => localidadesResponsaveis.Contains(a.SeqEntidade)));
                            break;

                        case Common.Areas.CAM.Enums.TipoParametroEvento.CursoOfertaLocalidade:
                            eventoLetivoValidos = eventoLetivoValidos.Where(w => w.ParametrosEntidade.Any(a => cursoOfertaLocalidadesResponsaveis.Contains(a.SeqEntidade)));
                            break;

                        case Common.Areas.CAM.Enums.TipoParametroEvento.EntidadeResponsavel:
                            eventoLetivoValidos = eventoLetivoValidos.Where(w => w.ParametrosEntidade.Any(a => entidadesResponsaveis.Contains(a.SeqEntidade)));
                            break;

                        case Common.Areas.CAM.Enums.TipoParametroEvento.TipoAluno:
                            break;

                        default:
                            break;
                    }
                }
                if (eventoLetivoValidos.Any())
                    return eventoLetivoValidos.First().DataFim;
            }

            throw new Exception();
        }

        /// <summary>
        /// Validar se a data final do escalonametno e maior que a data final do processo, segundo RN_SRC_036 CONSISTÊNCIAS NA INCLUSÃO / ALTERAÇÃO
        /// </summary>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <param name="seqProcessoEtapa">Sequencial do processo etapa</param>
        /// <returns>True e False</returns>
        public bool ValidarDataFimEscalonamento(long seqProcessoEtapa, DateTime dataFimEscalonamento)
        {
            bool retorno = false;

            //var dataFimPeriodoFinanceiro = BuscarDataFimPeriodoFinanceiro(seqProcessoEtapa);

            var dataFimProcesso = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(seqProcessoEtapa), p => p.Processo.DataFim);

            ///Se a data/hora fim do escalonamento for maior que a data/hora fim do processo, exibir a seguinte mensagem de confirmação
            if (dataFimEscalonamento > dataFimProcesso /*&& dataFimEscalonamento <= dataFimPeriodoFinanceiro*/)
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Recupera os grupos de escalonamentos de um escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <returns>Grupos de escalonamentos separados por virgua</returns>
        public string RecuperaGrupoEscalonamentoPorEscalonamento(long seqEscalonamento)
        {
            List<string> listaDescricaoRetorno = new List<string>();

            var escalonameto = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(seqEscalonamento), IncludesEscalonamento.GruposEscalonamento | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento);

            foreach (var item in escalonameto.GruposEscalonamento)
            {
                listaDescricaoRetorno.Add(item.GrupoEscalonamento.Descricao);
            }

            return string.Join(", ", listaDescricaoRetorno);
        }

        /// <summary>
        /// Verifica se a data fim do escalonamento e a data data fim da parcela são iguais
        /// </summary>
        /// <param name="modelo">Modelo com os dados escalonamento</param>
        /// <returns>True se as parcelas forem iguais</returns>
        public bool VerificarDataParcelasDataFimEscalonamento(EscalonamentoVO modelo)
        {
            bool retorno = false;

            var includes = IncludesEscalonamento.GruposEscalonamento_Parcelas;

            var escalonamento = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(modelo.Seq), includes);

            var dadosProcesso = this.ProcessoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapa>(modelo.SeqProcessoEtapa), p => new { p.Processo.DataFim, p.Processo.Servico.Token });

            ///3.Se o escalonamento possuir configuração de parcela, a data de vencimento das parcelas deve ser
            ///maior ou igual que a data fim do respectivo escalonamento e menor ou igual que o período de vigência
            ///do processo. Exceto para processos do serviço que possuem o token SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU ou MATRICULA_REABERTURA.
            ///Se a data de vencimento das parcelas for igual à data fim do escalonamento, exibir a seguinte
            ///mensagem de confirmação:
            foreach (var item in escalonamento.GruposEscalonamento)
            {
                var dataFimEscalonamento = DateTime.Parse(modelo.DataFim.SMCDataAbreviada());
                foreach (var parcela in item.Parcelas)
                {
                    if (parcela.DataVencimentoParcela.Date >= dataFimEscalonamento.Date
                      && parcela.DataVencimentoParcela.Date <= dadosProcesso.DataFim.GetValueOrDefault().Date
                      && parcela.DataVencimentoParcela.Date == dataFimEscalonamento.Date
                      && !(dadosProcesso.Token == TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU || dadosProcesso.Token == TOKEN_SERVICO.MATRICULA_REABERTURA))
                    {
                        retorno = true;
                    }
                }
            }

            return retorno;
        }

        /// <summary>
        /// Buscar escalonamento
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        /// <returns>Dados do escalonamento</returns>
        public EscalonamentoVO BuscarEscalonamento(long seq)
        {
            var includes = IncludesEscalonamento.GruposEscalonamento
                           | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento;

            var result = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(seq), includes);

            var retono = result.Transform<EscalonamentoVO>();

            if (retono.GruposEscalonamento != null && retono.GruposEscalonamento.Count > 0)
            {
                //List<string> listaDescricao = new List<string>();
                retono.DescricaoGruposEscalonamento = new List<string>();
                foreach (var grupo in retono.GruposEscalonamento)
                {
                    retono.DescricaoGruposEscalonamento.Add(grupo.GrupoEscalonamento.Descricao);
                }
                //retono.DescricaoGruposEscalonamento = string.Join("<br />", listaDescricao);
            }
            else
            {
                retono.DescricaoGruposEscalonamento = new List<string>();
                retono.DescricaoGruposEscalonamento.Add("-");
            }

            return retono;
        }

        /// <summary>
        /// Exluir escalonamento selecionado
        /// </summary>
        /// <param name="seq">Sequencial do escalonamento</param>
        public void ExlcuirEscalonamento(long seq)
        {
            var modelo = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(seq));

            this.DeleteEntity(modelo);
        }

        /// <summary>
        /// Verificar se existem solicitações associadas a um grupo de escalonamento
        /// </summary>
        /// <param name="seqEscalonamento">Sequencial do escalonamento</param>
        /// <param name="dataFimEscalonamento">Data final do escalonamento</param>
        /// <returns>Boleano afirmando se existe solicitação de serviço</returns>
        public bool VerificarExisteSolicitacaoServicoGrupoPorEscalonamento(long seqEscalonamento, DateTime dataFimEscalonamento)
        {
            bool retorno = false;

            var escalonameto = this.SearchByKey(new SMCSeqSpecification<Escalonamento>(seqEscalonamento), IncludesEscalonamento.GruposEscalonamento
                                                                                                          | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento
                                                                                                          | IncludesEscalonamento.GruposEscalonamento_GrupoEscalonamento_SolicitacoesServico);

            if (escalonameto.GruposEscalonamento.Count() > 0 && escalonameto.DataFim < dataFimEscalonamento)
            {
                foreach (var item in escalonameto.GruposEscalonamento)
                {
                    if (item.GrupoEscalonamento.SolicitacoesServico.Count() > 0)
                    {
                        retorno = true;
                    }
                }
            }

            return retorno;
        }
    }
}