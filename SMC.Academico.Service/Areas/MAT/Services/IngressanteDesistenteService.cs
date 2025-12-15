using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework.Jobs.Service;
using SMC.Framework.Service;
using SMC.Framework.UnitOfWork;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SMC.Academico.Service.Areas.MAT.Services
{
    public class IngressanteDesistenteService : SMCServiceBase, IIngressanteDesistenteService
    {
        #region [ Domains and Services]

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private EscalonamentoDomainService EscalonamentoDomainService
        {
            get { return Create<EscalonamentoDomainService>(); }
        }

        private ISituacaoEtapaService SituacaoEtapaService
        {
            get { return Create<ISituacaoEtapaService>(); }
        }

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService
        {
            get { return Create<IngressanteHistoricoSituacaoDomainService>(); }
        }

        private ProcessoSeletivoOfertaDomainService ProcessoSeletivoOfertaDomainService
        {
            get { return Create<ProcessoSeletivoOfertaDomainService>(); }
        }

        private IIntegracaoService IntegracaoService
        {
            get { return Create<IIntegracaoService>(); }
        }

        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService
        {
            get { return Create<ConfiguracaoEtapaBloqueioDomainService>(); }
        }

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService
        {
            get { return Create<SolicitacaoMatriculaDomainService>(); }
        }

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService
        {
            get { return Create<SolicitacaoMatriculaItemDomainService>(); }
        }

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService
        {
            get { return Create<PessoaAtuacaoBloqueioDomainService>(); }
        }

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService
        {
            get { return Create<IIntegracaoFinanceiroService>(); }
        }

        private CampanhaOfertaItemDomainService CampanhaOfertaItemDomainService
        {
            get { return Create<CampanhaOfertaItemDomainService>(); }
        }

        private SolicitacaoMatriculaItemHistoricoSituacaoDomainService SolicitacaoMatriculaItemHistoricoSituacaoDomainService
        {
            get { return Create<SolicitacaoMatriculaItemHistoricoSituacaoDomainService>(); }
        }

        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService
        {
            get { return Create<SituacaoItemMatriculaDomainService>(); }
        }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return Create<ProcessoEtapaDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ Domains and Services]

        #region [ Properties ]

        private short Progress { get; set; }

        private long SeqHistoricoAgendamento { get; set; }

        #endregion [ Properties ]

        #region [ Constants ]

        private const string queryPrimeiroCaso =
            @"select	ss.seq_solicitacao_servico as SeqSolicitacaoServico,
		                ce.seq_configuracao_etapa as SeqConfiguracaoEtapa,
		                ss.seq_pessoa_atuacao as SeqPessoaAtuacao,
		                iof.seq_inscricao_oferta_gpi as SeqInscricaoOfertaGpi,
		                intva.ind_exige_curso as ExigeCurso,
		                pdp.nom_pessoa as NomePessoa,
		                iof.seq_campanha_oferta as SeqCampanhaOferta,
		                i.seq_processo_seletivo as SeqProcessoSeletivo,
		                sm.cod_adesao as CodigoAdesao,
		                cl.num_ciclo_letivo NumeroCicloLetivo,
		                cl.ano_ciclo_letivo as AnoCicloLetivo,
                        pe.seq_processo_etapa as SeqProcessoEtapa
                from	SRC.escalonamento e
                join	SRC.grupo_escalonamento_item gei
		                on e.seq_escalonamento = gei.seq_escalonamento
                join	SRC.grupo_escalonamento ge
		                on gei.seq_grupo_escalonamento = ge.seq_grupo_escalonamento
                join	SRC.solicitacao_servico ss
		                on ge.seq_grupo_escalonamento = ss.seq_grupo_escalonamento
                join	SRC.configuracao_processo cp
		                on ss.seq_configuracao_processo = cp.seq_configuracao_processo
                join	src.processo pr
		                on pr.seq_processo = cp.seq_processo
                join	cam.ciclo_letivo cl
		                on cl.seq_ciclo_letivo = pr.seq_ciclo_letivo
                join	SRC.configuracao_etapa ce
		                on cp.seq_configuracao_processo = ce.seq_configuracao_processo
                join	src.processo_etapa pe
		                on ce.seq_processo_etapa = pe.seq_processo_etapa
                join	SRC.solicitacao_servico_etapa sse
		                on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
		                and ce.seq_configuracao_etapa = sse.seq_configuracao_etapa
                join	SRC.solicitacao_historico_situacao shs
		                on sse.seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
		                and shs.dat_exclusao is null
		                and shs.dat_inclusao = (select	MAX(dat_inclusao)
								                from	SRC.solicitacao_historico_situacao
								                where	seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
								                and		dat_exclusao is null)
                join	ALN.ingressante i
		                on ss.seq_pessoa_atuacao = i.seq_pessoa_atuacao
                join	ALN.ingressante_oferta iof
		                on i.seq_pessoa_atuacao = iof.seq_atuacao_ingressante
                join	PES.pessoa_atuacao pa
		                on i.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	PES.pessoa p
		                on pa.seq_pessoa = p.seq_pessoa
                join	ORG.instituicao_nivel inv
		                on i.seq_nivel_ensino = inv.seq_nivel_ensino
		                and p.seq_entidade_instituicao = inv.seq_entidade_instituicao
                join	ALN.instituicao_nivel_tipo_vinculo_aluno intva
		                on inv.seq_instituicao_nivel = intva.seq_instituicao_nivel
		                and i.seq_tipo_vinculo_aluno = intva.seq_tipo_vinculo_aluno
                join	pes.pessoa_dados_pessoais pdp
		                on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                join	mat.solicitacao_matricula sm
		                on ss.seq_solicitacao_servico = sm.seq_solicitacao_servico
                where	e.seq_processo_etapa = ce.seq_processo_etapa
                        and shs.seq_situacao_etapa_sgf in ({0}) /* O retorno do serviço do SGF vem aqui*/
                        and e.dat_fim_escalonamento < GETDATE()";

        private const string querySegundoCaso =
            @"select    distinct
		                ss.seq_solicitacao_servico as SeqSolicitacaoServico,
		                ce.seq_configuracao_etapa as SeqConfiguracaoEtapa,
		                ss.seq_pessoa_atuacao as SeqPessoaAtuacao,
		                iof.seq_inscricao_oferta_gpi as SeqInscricaoOfertaGpi,
		                intva.ind_exige_curso as ExigeCurso,
		                pdp.nom_pessoa as NomePessoa,
		                iof.seq_campanha_oferta as SeqCampanhaOferta,
		                i.seq_processo_seletivo as SeqProcessoSeletivo,
		                sm.cod_adesao as CodigoAdesao,
		                cl.num_ciclo_letivo NumeroCicloLetivo,
		                cl.ano_ciclo_letivo as AnoCicloLetivo,
		                pabi.cod_integracao_sistema_origem as NumeroParcela,
                        pe.seq_processo_etapa as SeqProcessoEtapa
                from	src.escalonamento e
                join	src.grupo_escalonamento_item gei
		                on e.seq_escalonamento = gei.seq_escalonamento
                join	src.grupo_escalonamento ge
		                on gei.seq_grupo_escalonamento = ge.seq_grupo_escalonamento
                join	src.solicitacao_servico ss
		                on ge.seq_grupo_escalonamento = ss.seq_grupo_escalonamento
                join	src.configuracao_processo cp
		                on ss.seq_configuracao_processo = cp.seq_configuracao_processo
                join	src.processo pr
		                on pr.seq_processo = cp.seq_processo
                join	cam.ciclo_letivo cl
		                on cl.seq_ciclo_letivo = pr.seq_ciclo_letivo
                join	src.configuracao_etapa ce
		                on cp.seq_configuracao_processo = ce.seq_configuracao_processo
                join	src.processo_etapa pe
		                on ce.seq_processo_etapa = pe.seq_processo_etapa
                join	src.solicitacao_servico_etapa sse
		                on ss.seq_solicitacao_servico = sse.seq_solicitacao_servico
		                and ce.seq_configuracao_etapa = sse.seq_configuracao_etapa
                join	src.solicitacao_historico_situacao shs
		                on sse.seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
		                and shs.dat_exclusao is null
		                and shs.dat_inclusao = (select	MAX(dat_inclusao)
								                from	SRC.solicitacao_historico_situacao
								                where	seq_solicitacao_servico_etapa = shs.seq_solicitacao_servico_etapa
								                and		dat_exclusao is null)
                join	aln.ingressante i
		                on ss.seq_pessoa_atuacao = i.seq_pessoa_atuacao
                join	aln.ingressante_oferta iof
		                on i.seq_pessoa_atuacao = iof.seq_atuacao_ingressante
                join	pes.pessoa_atuacao pa
		                on i.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
                join	pes.pessoa p
		                on pa.seq_pessoa = p.seq_pessoa
                join	org.instituicao_nivel inv
		                on i.seq_nivel_ensino = inv.seq_nivel_ensino
		                and p.seq_entidade_instituicao = inv.seq_entidade_instituicao
                join	aln.instituicao_nivel_tipo_vinculo_aluno intva
		                on inv.seq_instituicao_nivel = intva.seq_instituicao_nivel
		                and i.seq_tipo_vinculo_aluno = intva.seq_tipo_vinculo_aluno
                join	pes.pessoa_dados_pessoais pdp
		                on pa.seq_pessoa_dados_pessoais = pdp.seq_pessoa_dados_pessoais
                join	mat.solicitacao_matricula sm
		                on ss.seq_solicitacao_servico = sm.seq_solicitacao_servico

                join	SRC.configuracao_etapa_bloqueio ceb
		                on ce.seq_configuracao_etapa = ceb.seq_configuracao_etapa
		                and ceb.ind_impede_inicio_etapa = 1 -- bloqueio que impede o inicio da etapa
                join	PES.motivo_bloqueio mb
		                on ceb.seq_motivo_bloqueio = mb.seq_motivo_bloqueio
		                and mb.dsc_token in ('PARCELA_PRE_MATRICULA_PENDENTE', 'PARCELA_MATRICULA_PENDENTE')
                join	pes.pessoa_atuacao_bloqueio pab
		                on pab.seq_pessoa_atuacao = pa.seq_pessoa_atuacao
		                and pab.idt_dom_situacao_bloqueio = 1 -- Bloqueado
		                and pab.dat_bloqueio <= GETDATE()
		                and pab.seq_motivo_bloqueio = mb.seq_motivo_bloqueio
                join	SRC.grupo_escalonamento_item_parcela geip
		                on mb.seq_motivo_bloqueio = geip.seq_motivo_bloqueio
		                and geip.seq_grupo_escalonamento_item in (	select	seq_grupo_escalonamento_item
													                from	SRC.grupo_escalonamento_item
													                where	seq_grupo_escalonamento = ge.seq_grupo_escalonamento)
                join	pes.pessoa_atuacao_bloqueio_item pabi
		                on pabi.seq_pessoa_atuacao_bloqueio = pab.seq_pessoa_atuacao_bloqueio
                where	e.seq_processo_etapa = ce.seq_processo_etapa
		                -- apenas durante o período da etapa
		                and e.dat_inicio_escalonamento <= GETDATE()
		                and e.dat_fim_escalonamento >= GETDATE()
		                and DATEADD(dd, 5, geip.dat_vencimento_parcela) <= GETDATE()
                        and shs.seq_situacao_etapa_sgf in ({0}) /* O retorno do serviço do SGF vem aqui*/";

        #endregion [ Constants ]

        public void BuscarIngressantesDesistentes(IngressanteDesistenteSATData modelo)
        {
            SeqHistoricoAgendamento = modelo.SeqHistoricoAgendamento;

            #region [ Buscando dados dos ingressantes desistentes ]

            List<IngressanteDesistenteVO> result = new List<IngressanteDesistenteVO>();
            result = BuscarIngressantesDesistentesPrimeiroCaso(modelo.SeqHistoricoAgendamento);
            result.AddRange(BuscarIngressantesDesistentesSegundoCaso(modelo.SeqHistoricoAgendamento));

            #endregion [ Buscando dados dos ingressantes desistentes ]

            #region [ Executando as alterações necessárias... ]

            if (result.Count == 0)
            {
                Log("Não foram encontrados ingressantes desistentes para efetuar o processamento.", 100, true, TipoLog.Success, null, string.Empty);
                return;
            }

            try
            {
                Log(string.Format("Iniciando o processamento da desistência de {0} ingressante(s)...", result.Count), null, true, TipoLog.Info, null, string.Empty);

                double percentual = ((100.0 - Progress) / result.Count);
                double processamento = percentual;
                short qtdeErros = 0;

                foreach (var item in result)
                {
                    using (var unityOfWork = SMCUnitOfWork.Begin())
                    {
                        try
                        {
                            //1. Criar um registro no histórico de situação do Ingressante com o valor "Desistente";
                            IngressanteHistoricoSituacao ihs = new IngressanteHistoricoSituacao()
                            {
                                SeqIngressante = item.SeqPessoaAtuacao,
                                SituacaoIngressante = Common.Areas.ALN.Enums.SituacaoIngressante.Desistente
                            };
                            IngressanteHistoricoSituacaoDomainService.SaveEntity(ihs);

                            // FIX: Apenas se for a ultima etapa, executar o item 2
                            //2. Alterar a situação do Inscrito na oferta, referente ao Ingressante em questão, para a situação com token CONVOCADO_DESISTENTE(GPI);
                            //if (item.SeqInscricaoOfertaGpi.HasValue)
                            //{
                            //    IntegracaoService.AlterarSituacaoConvocadoParaDesistente(item.SeqInscricaoOfertaGpi.Value);
                            //}
                            //else
                            //{
                            //    throw new InscricaoOfertaGPINaoEncontradaException();
                            //}

                            //3. Alterar a situação do aluno no financeiro, referente ao Ingressante em questão, quando ele existir, para "Calouro desistente" (cod_dominio = 6 - GRA);
                            if (item.CodigoAdesao.HasValue)
                            {
                                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(item.SeqPessoaAtuacao);
                                string erroGRA = IntegracaoFinanceiroService.AlterarSituacaoMatriculaAcademico(new AlterarMatriculaAcademicoData()
                                {
                                    SeqPessoaAtuacao = item.SeqPessoaAtuacao,
                                    SeqOrigem = (int)dadosOrigem.SeqOrigem,
                                    CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                                    AnoCicloLetivo = item.AnoCicloLetivo,
                                    NumeroCicloLetivo = item.NumeroCicloLetivo,
                                    CodigoTipoTransacao = 47
                                });
                                if (!string.IsNullOrEmpty(erroGRA))
                                {
                                    Log(erroGRA, null, false, TipoLog.Info, item.SeqSolicitacaoServico, item.NomePessoa);
                                }
                            }

                            //4.Subtrair 1 na quantidade de vagas ocupadas de cada item da solicitação de matrícula do tipo de gestão "turma" e
                            //que tenham a situação ATUAL diferente das situações que possuem a classificação "finalizada sem sucesso", "Cancelado".
                            //  4.1 Para estes itens, alterar a situação para situação cuja classificação é final, com classificação
                            //  "Cancelado" e motivo "Etapa não finalizada"
                            SolicitacaoMatriculaItemFiltroVO filtroItem = new SolicitacaoMatriculaItemFiltroVO();
                            filtroItem.ClassificacaoSituacoesFinaisDiferentes = new ClassificacaoSituacaoFinal[] { ClassificacaoSituacaoFinal.Cancelado, ClassificacaoSituacaoFinal.FinalizadoSemSucesso };
                            filtroItem.SeqSolicitacaoMatricula = item.SeqSolicitacaoServico;
                            List<long> registrosItem = SolicitacaoMatriculaItemDomainService.BuscarSequenciaisSolicitacoesMatriculaItem(filtroItem);
                            var situacaoItemCancelado = SituacaoItemMatriculaDomainService.BuscarSituacaoItemMatriculaEtapa(item.SeqProcessoEtapa, null, true, ClassificacaoSituacaoFinal.Cancelado);
                            var processoEtapa = ProcessoEtapaDomainService.BuscarProcessoEtapa(item.SeqProcessoEtapa);
                            foreach (var itemSolicitacao in registrosItem)
                            {
                                SolicitacaoMatriculaItemHistoricoSituacao historico = new SolicitacaoMatriculaItemHistoricoSituacao();
                                historico.SeqSolicitacaoMatriculaItem = itemSolicitacao;
                                historico.SeqSituacaoItemMatricula = situacaoItemCancelado.Seq;
                                historico.MotivoSituacaoMatricula = MotivoSituacaoMatricula.EtapaNaoFinalizada;
                                SolicitacaoMatriculaItemHistoricoSituacaoDomainService.SaveEntity(historico);

                                if (processoEtapa != null && processoEtapa.ControleVaga)
                                {
                                    SolicitacaoMatriculaItemDomainService.LiberarVagaTurmaAtividadeIngressante(itemSolicitacao);
                                }
                            }

                            //Verificando se existe bloqueio parametrizado...
                            VerificarBloqueioParametrizado(item);

                            //Persistindo as inclusões e alterações...
                            unityOfWork.Commit();
                        }
                        catch (Exception ex)
                        {
                            qtdeErros++;

                            if (ex.InnerException != null && (ex.InnerException is SqlException) && (ex.InnerException as SqlException).Number == 1205)
                                Log("Ocorreu um problema de concorrência ao liberar a vaga para a turma.", null, false, TipoLog.Error, item.SeqSolicitacaoServico, item.NomePessoa);

                            unityOfWork.Rollback();
                            Log(string.Format("Erro ao processar a desistência de ingressante: {0}", ex.Message), null, false, TipoLog.Error, item.SeqSolicitacaoServico, item.NomePessoa);
                        }
                    };

                    //Informar o percentual de processamento...
                    Log(string.Empty, (short)Math.Round(Progress + processamento), null, TipoLog.Nenhum, null, string.Empty);
                    processamento += percentual;
                }

                //Finalizando o JOB e informando o resultado final...
                FinalizarExecucao(modelo.SeqHistoricoAgendamento, result.Count, qtdeErros);
            }
            catch (Exception ex)
            {
                Log(string.Format("Erro ao processar a desistência de ingressante: {0}", ex.Message), null, false, TipoLog.Error, null, string.Empty);
            }

            #endregion [ Executando as alterações necessárias... ]
        }

        #region [ Métodos auxiliares ]

        private List<IngressanteDesistenteVO> BuscarIngressantesDesistentesPrimeiroCaso(long seqHistoricoAgendamento)
        {
            List<IngressanteDesistenteVO> result = new List<IngressanteDesistenteVO>();
            try
            {
                Log("Iniciando a busca por ingressantes desistentes...", Progress, false, TipoLog.Info, null, string.Empty);
                result = BuscarDadosIngressantesDesistentes(seqHistoricoAgendamento, true);
                if (result == null || result.Count <= 0)
                {
                    Progress = 50;
                    Log("Não foram encontrados ingressantes desistentes.", Progress, true, TipoLog.Info, null, string.Empty);
                }
                else
                {
                    Progress = 10;
                    string msg = result.Count == 1 ? "Foi encontrado {0} ingressante desistente." : "Foram encontrados {0} ingressantes desistentes.";
                    Log(string.Format(msg, result.Count), Progress, true, TipoLog.Info, null, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Progress = 50;
                Log(string.Format("Erro ao processar a desistência de ingressante: {0}", ex.Message), Progress, false, TipoLog.Error, null, string.Empty);
            }
            return result;
        }

        private List<IngressanteDesistenteVO> BuscarIngressantesDesistentesSegundoCaso(long seqHistoricoAgendamento)
        {
            //Segundo caso (novo - 05/12/2017)
            //2.Após 5 dias do vencimento da parcela no financeiro, verificar se existem pessoas - atuação que possuem o
            //bloqueio com token PARCELA_PRE_MATRICULA_PENDENTE e PARCELA_MATRICULA_PENDENTE com
            //situação "Bloqueado" e data igual ou menor a data atual. Para buscar o vencimento da parcela no sistema
            //financeiro, verificar nos itens dos bloqueios em questão o número da parcela no campo "Integração com sistema de
            //origem".
            Progress += 10;
            List<IngressanteDesistenteVO> result = new List<IngressanteDesistenteVO>();
            try
            {
                Log("Iniciando a busca por ingressantes que não pagaram a parcela no financeiro...", Progress, null, TipoLog.Info, null, string.Empty);
                result = BuscarDadosIngressantesDesistentes(seqHistoricoAgendamento, false);
                if (result == null || result.Count <= 0)
                {
                    Progress = (short)(((Progress + 50) > 100) ? 100 : (Progress + 50));
                    Log("Não foram encontrados ingressantes que não pagaram a parcela no financeiro.", Progress, true, TipoLog.Info, null, string.Empty);
                }
                else
                {
                    Progress += 10;
                    string msg = result.Count == 1 ? "Foi encontrado {0} ingressante que não pagou a parcela no financeiro." : "Foram encontrados {0} ingressantes que não pagaram a parcela no financeiro.";
                    Log(string.Format(msg, result.Count), Progress, null, TipoLog.Info, null, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Log(string.Format("Erro ao processar a desistência de ingressante que não pagou a parcela no financeiro: {0}", ex.Message), Progress, false, TipoLog.Error, null, string.Empty);
            }
            return result;
        }

        private CursoOfertaLocalidade BuscarCursoOfertaLocalidade(long seqCampanhaOferta)
        {
            CampanhaOfertaItemFilterSpecification spec = new CampanhaOfertaItemFilterSpecification()
            {
                SeqCampanhaOferta = seqCampanhaOferta
            };

            var cursosOfertasLocalidade = CampanhaOfertaItemDomainService.SearchProjectionBySpecification(spec, a => a.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade).ToList();

            if (cursosOfertasLocalidade != null && cursosOfertasLocalidade.Count > 0)
                return cursosOfertasLocalidade.FirstOrDefault();
            else
                throw new ErroAntesChamarServicoIntegracaoFinanceiroException(seqCampanhaOferta);
        }

        /// <summary>
        ///1.   Se existir bloqueio parametrizado para a etapa em questão para "Gerar cancelamento da solicitação",
        ///     verificar se a pessoa-atuação desta solicitação possui este bloqueio parametrizado e com situação igual a "Bloqueado"
        ///     e a data do bloqueio tem que ser menor ou igual a data atual(do sistema).
        ///     Se possuir:
        ///         1.1. Setar a solicitação com a situação parametrizada para ser a final da etapa, final do processo e que possui a classificação "Cancelada";
        ///         1.2. Setar o motivo da alteração de situação como "Existência de bloqueio". TODO: Verificar isto com a Jéssica.
        ///2.   Caso não exista este bloqueio:
        ///         2.1. Setar a solicitação com a situação parametrizada para ser a final da etapa, final do processo e que possui a classificação "Finalizada sem sucesso".
        /// </summary>
        /// <param name="ingressante">Ingressante desistente.</param>
        private void VerificarBloqueioParametrizado(IngressanteDesistenteVO ingressante)
        {
            ConfiguracaoEtapaBloqueioFilterSpecification spec = new ConfiguracaoEtapaBloqueioFilterSpecification()
            {
                SeqsConfiguracoesEtapas = new List<long>() { ingressante.SeqConfiguracaoEtapa },
                GeraCancelamentoSolicitacao = true
            };

            var bloqueios = ConfiguracaoEtapaBloqueioDomainService.SearchProjectionBySpecification(spec, c => new { SeqMotivoBloqueio = c.SeqMotivoBloqueio, SeqConfiguracaoEtapa = c.SeqConfiguracaoEtapa }).ToList();

            if (bloqueios != null && bloqueios.Count > 0)
            {
                PessoaAtuacaoBloqueioFilterSpecification specPAB = new PessoaAtuacaoBloqueioFilterSpecification()
                {
                    SeqPessoaAtuacao = ingressante.SeqPessoaAtuacao,
                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                    DataBloqueioMenorOuIgualA = DateTime.Now,
                    SeqMotivoBloqueio = bloqueios.Select(b => b.SeqMotivoBloqueio).ToList()
                };

                var pessoasBloqueios = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specPAB).ToList();

                foreach (var bloqueio in bloqueios)
                {
                    if (pessoasBloqueios.Any(p => p.SeqMotivoBloqueio == bloqueio.SeqMotivoBloqueio))
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(ingressante.SeqSolicitacaoServico, bloqueio.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.Cancelado, null);
                    else
                        //Setar a solicitação com a situação parametrizada para ser a final da etapa, final do processo e que possui a classificação "Finalizada sem sucesso".
                        SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(ingressante.SeqSolicitacaoServico, ingressante.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoSemSucesso, null);
                }
            }
            else
            {
                //Setar a solicitação com a situação parametrizada para ser a final da etapa, final do processo e que possui a classificação "Finalizada sem sucesso".
                SolicitacaoServicoDomainService.ProcedimentosFinalizarEtapa(ingressante.SeqSolicitacaoServico, ingressante.SeqConfiguracaoEtapa, ClassificacaoSituacaoFinal.FinalizadoSemSucesso, null);
            }
        }

        private void FinalizarExecucao(long seqHistoricoAgendamento, int qtdeIngressantes, short qtdeErros)
        {
            if (qtdeErros == 0)
            {
                Log(string.Format("A alteração do(s) {0} ingressante(s) para desistente(s) foi finalizada com sucesso.", qtdeIngressantes), 100, true, TipoLog.Success, null, string.Empty);
            }
            else
            {
                Log(string.Format("Alteração de ingressantes para desistentes finalizada com erros. Não foi possível alterar os dados de {0} do(s) {1} ingressante(s).", qtdeErros, qtdeIngressantes), 100, false, TipoLog.Info, null, string.Empty);
            }
        }

        private List<IngressanteDesistenteVO> BuscarDadosIngressantesDesistentes(long seqHistoricoAgendamento, bool primeiroCaso)
        {
            EscalonamentoFilterSpecification spec = new EscalonamentoFilterSpecification();
            spec.EscalonamentoVencido = true;
            List<long> listaSeqEtapaSgf = EscalonamentoDomainService.SearchProjectionBySpecification(spec, a => a.ProcessoEtapa.SeqEtapaSgf).Distinct().ToList();

            var listaSGF = SituacaoEtapaService.ListaSeqSituacaoEtapaPorListaSeqEtapa(listaSeqEtapaSgf);
            if (listaSGF == null || listaSGF.Count <= 0)
            {
                throw new ListaSeqSituacaoEtapaVaziaException();
            }

            if (primeiroCaso)
            {
                return EscalonamentoDomainService.RawQuery<IngressanteDesistenteVO>(string.Format(queryPrimeiroCaso, string.Join(",", listaSGF)));
            }
            else
            {
                return EscalonamentoDomainService.RawQuery<IngressanteDesistenteVO>(string.Format(querySegundoCaso, string.Join(",", listaSGF)));
            }
        }

        private void Log(string message, short? progress, bool? success, TipoLog type, long? OriginID, string OriginName)
        {
            var logModel = new SMCSchedulerHistoryModel()
            {
                SeqSchedulerHistory = SeqHistoricoAgendamento,
                Log = message,
                DateTime = DateTime.Now,
                OriginID = OriginID,
                OriginName = OriginName,
                Success = false
            };

            switch (type)
            {
                case TipoLog.Error:
                    EscalonamentoDomainService.Scheduler.LogError(logModel);
                    break;

                case TipoLog.Info:
                    EscalonamentoDomainService.Scheduler.LogInfo(logModel);
                    break;

                case TipoLog.Success:
                    EscalonamentoDomainService.Scheduler.LogSucess(logModel);
                    break;

                default:
                    break;
            }

            if (progress.HasValue)
            {
                EscalonamentoDomainService.Scheduler.Progress(new SMCSchedulerHistoryModel { SeqSchedulerHistory = SeqHistoricoAgendamento, Progress = progress.Value, Success = success });
            }
        }

        #endregion [ Métodos auxiliares ]
    }
}