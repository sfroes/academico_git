using SMC.Academico.Common.Areas.CAM.Exceptions.CilcoLetivo;
using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Resources;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
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
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ProcessoDomainService : AcademicoContextDomain<Processo>
    {
        #region Serviços outros domínios

        private IAplicacaoService AplicacaoService => this.Create<IAplicacaoService>();

        private IEtapaService EtapaService => this.Create<IEtapaService>();

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion Serviços outros domínios

        #region Domain Services

        private PlanoEstudoDomainService PlanoEstudoDomainService => this.Create<PlanoEstudoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService => Create<GrupoEscalonamentoDomainService>();

        private SolicitacaoServicoEtapaDomainService SolicitacaoServicoEtapaDomainService => Create<SolicitacaoServicoEtapaDomainService>();

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();

        private ProcessoSeletivoProcessoMatriculaDomainService ProcessoSeletivoProcessoMatriculaDomainService => Create<ProcessoSeletivoProcessoMatriculaDomainService>();

        private IngressanteDomainService IngressanteDomainService => Create<IngressanteDomainService>();

        private ConfiguracaoEtapaBloqueioDomainService ConfiguracaoEtapaBloqueioDomainService => Create<ConfiguracaoEtapaBloqueioDomainService>();

        private GrupoEscalonamentoItemDomainService GrupoEscalonamentoItemDomainService => Create<GrupoEscalonamentoItemDomainService>();

        private GrupoEscalonamentoItemParcelaDomainService GrupoEscalonamentoItemParcelaDomainService => Create<GrupoEscalonamentoItemParcelaDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private ProcessoUnidadeResponsavelDomainService ProcessoUnidadeResponsavelDomainService => Create<ProcessoUnidadeResponsavelDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private CampanhaDomainService CampanhaDomainService => Create<CampanhaDomainService>();

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => Create<ProcessoEtapaDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();

        private ServicoDomainService ServicoDomainService => Create<ServicoDomainService>();

        private ParametroEnvioNotificacaoDomainService ParametroEnvioNotificacaoDomainService => Create<ParametroEnvioNotificacaoDomainService>();

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();

        private GrupoDocumentoRequeridoDomainService GrupoDocumentoRequeridoDomainService => Create<GrupoDocumentoRequeridoDomainService>();

        private GrupoDocumentoRequeridoItemDomainService GrupoDocumentoRequeridoItemDomainService => Create<GrupoDocumentoRequeridoItemDomainService>();

        private ConfiguracaoEtapaPaginaDomainService ConfiguracaoEtapaPaginaDomainService => Create<ConfiguracaoEtapaPaginaDomainService>();

        private TextoSecaoPaginaDomainService TextoSecaoPaginaDomainService => Create<TextoSecaoPaginaDomainService>();

        private ArquivoSecaoPaginaDomainService ArquivoSecaoPaginaDomainService => Create<ArquivoSecaoPaginaDomainService>();

        private ProcessoEtapaFiltroDadoDomainService ProcessoEtapaFiltroDadoDomainService => Create<ProcessoEtapaFiltroDadoDomainService>();

        private EscalonamentoDomainService EscalonamentoDomainService => Create<EscalonamentoDomainService>();

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService { get => Create<HierarquiaEntidadeItemDomainService>(); }

        private SituacaoItemMatriculaDomainService SituacaoItemMatriculaDomainService { get => Create<SituacaoItemMatriculaDomainService>(); }
        private EntidadeDomainService EntidadeDomainService { get => Create<EntidadeDomainService>(); }

        #endregion Domain Services

        public SMCPagerData<ProcessoListaVO> BuscarProcessos(ProcessoFiltroVO filtros)
        {
            var total = 0;

            var spec = filtros.Transform<ProcessoFilterSpecification>()
                      .SetOrderBy(o => o.Servico.TipoServico.Descricao)
                      .SetOrderByDescending(t => t.DataInicio);

            var result = SearchBySpecification(spec, out total,
                                                IncludesProcesso.Configuracoes |
                                                IncludesProcesso.CicloLetivo |
                                                IncludesProcesso.Servico_TipoServico |
                                                IncludesProcesso.UnidadesResponsaveis_EntidadeResponsavel |
                                                IncludesProcesso.Etapas_Escalonamentos_GruposEscalonamento).ToList();

            var retorno = PrepararModelo(result);

            return new SMCPagerData<ProcessoListaVO>(retorno, total);
        }

        public long SalvarProcesso(ProcessoVO modelo)
        {
            ValidarModelo(modelo);

            var dominio = modelo.Transform<Processo>();

            List<ProcessoUnidadeResponsavel> processoUnidadeResponsaveis = new List<ProcessoUnidadeResponsavel>();

            var listaServicos = new List<string>
                    {
                       TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU,
                       TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                       TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                       TOKEN_SERVICO.MATRICULA_REABERTURA,
                       TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO,
                       TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA,
                       TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA,
                       TOKEN_SERVICO.TRANCAMENTO_MATRICULA,
                       TOKEN_SERVICO.CANCELAMENTO_MATRICULA
                    };

            // Considerar a regra:
            // Verificar se já existe um processo, para a entidade responsável e o ciclo letivo do processo, que está sendo criado.
            // Caso existir, abortar a operação e exibir a seguinte mensagem:
            // "Não é possível prosseguir. Já existe processo deste serviço para o ciclo letivo <Descrição do ciclo letivo>, para as seguintes entidades responsáveis:
            // - < Nome da entidade 1 >
            // - < Nome da entidade 2 >."
            //Caso não existir, prosseguir com o cadastro.
            var tokenServico = ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(modelo.SeqServico));
            if (listaServicos.Contains(tokenServico.Token))
            {
                // Verifica se tem algum processo na base que tenha o mesmo ciclo, unidade e token de serviço
                var seqsEntidades = new List<long>();
                foreach (var item in modelo.EntidadesResponsaveis)
                    seqsEntidades.Add(item.Seq);

                var specProcesso = new ProcessoUnidadeResponsavelFilterSpecification
                {
                    SeqCicloLetivo = modelo.SeqCicloLetivo,
                    SeqServico = modelo.SeqServico,
                    TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel,
                    SeqsEntidadesResponsaveis = seqsEntidades.ToArray()
                };

                if (modelo.Seq > 0)
                    specProcesso.SeqProcessoDiferenteDe = modelo.Seq;

                var processosUnidade = ProcessoUnidadeResponsavelDomainService.SearchBySpecification(specProcesso).ToList();

                if (processosUnidade != null && processosUnidade.Count() > 0)
                {
                    var entidades = string.Empty;
                    foreach (var item in processosUnidade)
                    {
                        var entidade = EntidadeDomainService.SearchByKey(new SMCSeqSpecification<Entidade>(item.SeqEntidadeResponsavel));
                        entidades += $"<br />- {entidade.Nome}";
                    }

                    var descricaoCiclo = CicloLetivoDomainService.BuscarDescricaoFormatadaCicloLetivo(modelo.SeqCicloLetivo.Value);

                    throw new ExisteProcessoServicoCicloLetivoException(descricaoCiclo, entidades);
                }
            }

            foreach (var entidadeResponsavel in modelo.EntidadesResponsaveis)
            {
                var processoUnidadeResponsavel = new ProcessoUnidadeResponsavel()
                {
                    Seq = entidadeResponsavel.SeqProcessoUnidadeResponsavel.GetValueOrDefault(),
                    SeqEntidadeResponsavel = entidadeResponsavel.Seq,
                    TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel
                };

                if (modelo.Seq > 0 && processoUnidadeResponsavel.Seq == 0)
                {
                    var processoUnidadesResponsaveisAssociadas = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.Seq), x => x.UnidadesResponsaveis);

                    //SE FOR A EDIÇÃO DO PROCESSO, VERIFICA NAS UNIDADES RESPONSAVEIS QUAL É O SEQUENCIAL 
                    var entidadesAuxiliares = processoUnidadesResponsaveisAssociadas.Where(a => a.SeqEntidadeResponsavel == entidadeResponsavel.Seq).ToList();

                    if (entidadesAuxiliares.Any())
                    {
                        if (entidadesAuxiliares.Count() > 1)
                        {
                            processoUnidadeResponsavel.Seq = entidadesAuxiliares.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).Seq;
                        }

                        processoUnidadeResponsavel.Seq = entidadesAuxiliares.FirstOrDefault().Seq;
                    }
                }

                processoUnidadeResponsaveis.Add(processoUnidadeResponsavel);
            }

            if (modelo.EntidadesCompartilhadas != null && modelo.EntidadesCompartilhadas.Any(a => a != null))
            {
                foreach (var entidadeCompartilhada in modelo.EntidadesCompartilhadas)
                {
                    var processoUnidadeResponsavel = new ProcessoUnidadeResponsavel()
                    {
                        Seq = entidadeCompartilhada.SeqProcessoUnidadeResponsavel.GetValueOrDefault(),
                        SeqEntidadeResponsavel = entidadeCompartilhada.Seq,
                        TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeCompartilhada
                    };

                    if (modelo.Seq > 0 && processoUnidadeResponsavel.Seq == 0)
                    {
                        var processoUnidadesResponsaveisAssociadas = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(modelo.Seq), x => x.UnidadesResponsaveis);

                        //SE FOR A EDIÇÃO DO PROCESSO, VERIFICA NAS UNIDADES RESPONSAVEIS QUAL É O SEQUENCIAL 
                        var entidadesAuxiliares = processoUnidadesResponsaveisAssociadas.Where(a => a.SeqEntidadeResponsavel == entidadeCompartilhada.Seq).ToList();

                        if (entidadesAuxiliares.Any())
                        {
                            if (entidadesAuxiliares.Count() > 1)
                            {
                                processoUnidadeResponsavel.Seq = entidadesAuxiliares.FirstOrDefault(a => a.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel).Seq;
                            }

                            processoUnidadeResponsavel.Seq = entidadesAuxiliares.FirstOrDefault().Seq;
                        }
                    }

                    processoUnidadeResponsaveis.Add(processoUnidadeResponsavel);
                }
            }

            dominio.UnidadesResponsaveis = processoUnidadeResponsaveis;

            //TRECHO COMENTADO POIS SERÁ PERMITDO CRIAR PROCESSOS COM DATAS RETROATIVAS
            //ValidarModeloNovoProcessoPeriodoFinanceiro(modelo, processoUnidadeResponsaveis);

            this.SaveEntity(dominio);

            if (modelo.Seq == 0)
            {
                List<long> seqsEtapasSGF = modelo.EtapasSGF.Where(a => a.AssociarEtapa == true).Select(a => a.Seq).ToList();
                ProcessoEtapaDomainService.SalvarProcessoEtapaOrigemSGF(dominio.Seq, seqsEtapasSGF);
            }

            return dominio.Seq;
        }

        public ProcessoVO BuscarProcessoEditar(long seqProcesso)
        {
            var spec = new SMCSeqSpecification<Processo>(seqProcesso);

            var processo = this.SearchByKey(spec, IncludesProcesso.UnidadesResponsaveis_EntidadeResponsavel | IncludesProcesso.Servico | IncludesProcesso.CicloLetivo);

            var retorno = processo.Transform<ProcessoVO>();

            retorno.ProcessoEncerrado = processo.DataEncerramento.HasValue && processo.DataEncerramento.Value < DateTime.Now;
            retorno.UnidadesResponsaveis = retorno.UnidadesResponsaveis.OrderBy(a => a.TipoUnidadeResponsavel).ThenBy(a => a.NomeEntidadeResponsavel).ToList();

            List<EntidadeVO> entidadesResponsaveis = new List<EntidadeVO>();
            List<EntidadeVO> entidadesCompartilhadas = new List<EntidadeVO>();

            foreach (var processoUnidadeResponsavel in processo.UnidadesResponsaveis)
            {
                var entidade = new EntidadeVO()
                {
                    SeqProcessoUnidadeResponsavel = processoUnidadeResponsavel.Seq,
                    Seq = processoUnidadeResponsavel.SeqEntidadeResponsavel,
                    Nome = processoUnidadeResponsavel.EntidadeResponsavel.Nome
                };

                if (processoUnidadeResponsavel.TipoUnidadeResponsavel == TipoUnidadeResponsavel.EntidadeResponsavel)
                    entidadesResponsaveis.Add(entidade);
                else
                    entidadesCompartilhadas.Add(entidade);
            }

            retorno.EntidadesResponsaveis = entidadesResponsaveis.OrderBy(a => a.Nome).ToList();
            retorno.EntidadesCompartilhadas = entidadesCompartilhadas.OrderBy(a => a.Nome).ToList();

            return retorno;
        }

        public CopiarProcessoVO BuscarProcessoCopiar(long seqProcesso)
        {
            var gruposEscalonamento = new List<CopiarProcessoDetalheGrupoEscalonamentoVO>();
            var spec = new SMCSeqSpecification<Processo>(seqProcesso);
            var processo = this.SearchByKey(spec, IncludesProcesso.Etapas | IncludesProcesso.Etapas_ConfiguracoesNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_TipoNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_ParametrosEnvioNotificacao | IncludesProcesso.GruposEscalonamento | IncludesProcesso.CicloLetivo | IncludesProcesso.Servico_TipoServico | IncludesProcesso.Servico_TiposNotificacao);

            if (processo.GruposEscalonamento.Count > 0)
            {
                foreach (var grupoEscalonamento in processo.GruposEscalonamento)
                {
                    bool obrigatoriasPossuemAgendamento = true;
                    List<(long, long)> seqsPorEtapa = new List<(long, long)>();
                    var tiposNotificacoesObrigatorias = grupoEscalonamento.Processo.Servico.TiposNotificacao.Where(c => c.Obrigatorio == true).ToList();

                    // Criando lista de seq da etapa x notificação obrigatória, pois o mesmo tipo de notificação pode estar em mais de uma etapa
                    foreach (var obrigatoria in tiposNotificacoesObrigatorias)
                    {
                        seqsPorEtapa.Add((obrigatoria.SeqEtapaSgf, obrigatoria.SeqTipoNotificacao));
                    }

                    // Verificar de etapa em etapa do processo se a obrigatória está configurada, permite agendamento e tem parâmetro de envio
                    // caso obrigatórias estejam cadastradas
                    if (seqsPorEtapa.Count() > 0)
                    {
                        foreach (var etapa in grupoEscalonamento.Processo.Etapas)
                        {
                            var obrigatoriasDaEtapa = seqsPorEtapa.Where(c => c.Item1 == etapa.SeqEtapaSgf).ToList();
                            if (obrigatoriasDaEtapa.Count > 0)
                            {
                                var configsNotificacoes = etapa.ConfiguracoesNotificacao.Where(c => obrigatoriasDaEtapa.Select(d => d.Item2).Contains(c.SeqTipoNotificacao)).ToList();
                                var configsPermitemAgendamento = configsNotificacoes.Where(c => c.TipoNotificacao.PermiteAgendamento).ToList();

                                if (configsPermitemAgendamento.Count() > 0)
                                {
                                    if (!configsPermitemAgendamento.Any(c => c.ParametrosEnvioNotificacao.Count(d => d.SeqGrupoEscalonamento == grupoEscalonamento.Seq) > 0))
                                        obrigatoriasPossuemAgendamento = false;
                                }
                            }
                        }
                    }

                    if (obrigatoriasPossuemAgendamento)
                    {
                        gruposEscalonamento.Add(grupoEscalonamento.Transform<CopiarProcessoDetalheGrupoEscalonamentoVO>());
                    }
                }
            }

            CopiarProcessoVO retorno = new CopiarProcessoVO()
            {
                ProcessoOrigem = processo.Transform<CopiarProcessoOrigemVO>(),
                EtapasCopiar = processo.Etapas.TransformList<CopiarProcessoDetalheEtapaVO>(),
                ExibirSecaoGrupoEscalonamento = processo.GruposEscalonamento.Any(),
                TokenTipoServico = processo.Servico.TipoServico.Token,
                TokenServico = processo.Servico.Token,
                //GruposEscalonamentoCopiar = processo.GruposEscalonamento.TransformList<CopiarProcessoDetalheGrupoEscalonamentoVO>().OrderBy(a => a.Descricao).ToList(),
                GruposEscalonamentoCopiar = gruposEscalonamento.OrderBy(a => a.Descricao).ToList(),
                ValorPercentualServicoAdicional = processo.ValorPercentualServicoAdicional
            };

            retorno.EtapasCopiar.ForEach(a => a.Obrigatoria = SGFHelper.BuscarEtapaSGFPorSeqEtapaCache(a.SeqEtapaSgf).Obrigatorio);

            return retorno;
        }

        private List<ProcessoListaVO> PrepararModelo(List<Processo> processos)
        {
            var retorno = new List<ProcessoListaVO>();

            foreach (var processo in processos)
            {
                // Cria uma lista para retorno
                var processoRetorno = processo.Transform<ProcessoListaVO>();

                // As opções Alterar, Excluir, Copiar, Configurar, Configurar Escalonamento das Etapas,
                // Configurar Grupo de Escalonamento, Configurar Notificação das Etapas, Preparar Renovação Matrícula,
                // e Encerrar Processo serão habilitadas somente se o papel do usuário logado possui 
                // o token de permissão de manutenção de processo parametrizado para o respectivo serviço
                processoRetorno.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(processo.Servico.TokenPermissaoManutencaoProcesso);

                // Visibilidade do botão "Copiar processo" na interface
                processoRetorno.HabilitaBtnCopiarProcesso = true;
                processoRetorno.InstructionCopiarProcesso = string.Empty;

                if (processo.Etapas.Any(a => a.SituacaoEtapa == SituacaoEtapa.EmManutencao || a.SituacaoEtapa == SituacaoEtapa.AguardandoLiberacao))
                {
                    processoRetorno.HabilitaBtnCopiarProcesso = false;
                    processoRetorno.InstructionCopiarProcesso = MessagesResource.MSG_InstructionCopiarProcesso;
                }

                // Visibilidade do botão "Excluir processo" na interface
                processoRetorno.HabilitaBtnExcluirProcesso = true;
                processoRetorno.InstructionExcluirProcesso = string.Empty;

                var solicitacaoServicoAssociada = SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqProcesso = processo.Seq }) > 0;

                if (processo.DataEncerramento.HasValue)
                {
                    processoRetorno.HabilitaBtnExcluirProcesso = false;
                    processoRetorno.InstructionExcluirProcesso = MessagesResource.MSG_InstructionExcluirProcesso_1;
                }
                else if (solicitacaoServicoAssociada)
                {
                    processoRetorno.HabilitaBtnExcluirProcesso = false;
                    processoRetorno.InstructionExcluirProcesso = MessagesResource.MSG_InstructionExcluirProcesso_2;
                }

                // Visibilidade do botão "Configurar etapa" na interface
                processoRetorno.HabilitaBtnConfigurarEtapa = true;
                processoRetorno.InstructionConfigurarEtapa = string.Empty;

                // Caso o processo não tenha configuração associada, não habilita botão de configurar etapa
                if (!processo.Configuracoes.Any())
                {
                    processoRetorno.HabilitaBtnConfigurarEtapa = false;
                    processoRetorno.InstructionConfigurarEtapa = MessagesResource.MSG_InstructionConfigurarEtapa;
                }

                // As opções Escalonamentos por etapa e Grupo de escalonamento deverão ser exibidas,
                // somente se o respectivo tipo do serviço do processo exige escalonamento.
                processoRetorno.ExibeBtnEscalonamentosEtapa = processo.Servico.TipoServico.ExigeEscalonamento;
                processoRetorno.ExibeBtnGrupoEscalonamento = processo.Servico.TipoServico.ExigeEscalonamento;

                // A opção Grupo de escalonamento deverá ser habilitada somente se há cadastro de escalonamentos por etapa
                processoRetorno.HabilitaBtnGrupoEscalonamento = HabilitaBtnGrupoEscalonamento(processo);

                // Informações do botão "Encerrar Processo"
                processoRetorno.ExibeBtnEncerrarProcesso = false;
                processoRetorno.HabilitaBtnEncerrarProcesso = true;
                processoRetorno.InstructionEncerrarProcesso = string.Empty;

                //lista de quais tokens devem ser checados ao permitir que o botão de Reabrir Processo seja exibido
                var listaTokensPermitir = new List<string>()
                {
                    TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                    TOKEN_SERVICO.MATRICULA_REABERTURA,
                    TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU,
                    TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA
                };

                if (listaTokensPermitir.Contains(processo.Servico.Token) && processo.DataEncerramento.HasValue)
                    processoRetorno.HabilitaBtnReabrirProcesso = true;

                // Busca o tipo de prazo que é utilizado pelas etapas do processo.
                // Segundo acordo com a Ellen, se uma etapa por do tipo "Escalonamento" ou "Periodo de Vigência",
                // todas as etapas devem ser do mesmo tipo. Isso é uma RN que será implementada na tela de cadastro
                // de etapas quando a mesma existir. Então é correto considerar o tipo de prazo de uma das etapas
                // como o tipo de prazo do processo inteiro
                TipoPrazoEtapa? tipoPrazoUsadoProcesso = null;

                if (processo.Etapas.Count > 0)
                    tipoPrazoUsadoProcesso = processo.Etapas.First().TipoPrazoEtapa;

                // Somente EXIBE o botão "Encerrar processo" se o tipo de prazo das etapas que compõem o processo
                // for igual a Período de Vigência ou Escalonamento
                if (tipoPrazoUsadoProcesso.HasValue &&
                    (tipoPrazoUsadoProcesso == TipoPrazoEtapa.Escalonamento || tipoPrazoUsadoProcesso == TipoPrazoEtapa.PeriodoVigencia))
                {
                    processoRetorno.ExibeBtnEncerrarProcesso = true;
                }

                // Se o processo já foi encerrado, o botão "Encerrar Processo" deverá ser desabilitado com a seguinte
                // mensagem informativa: "Opção indisponível. O processo está encerrado."
                if (processo.DataEncerramento.HasValue)
                {
                    processoRetorno.HabilitaBtnEncerrarProcesso = false;
                    processoRetorno.InstructionEncerrarProcesso = MessagesResource.MSG_InstructionEncerrarProcesso_1;
                }
                // Senão, se as [etapas que compõem o processo não foram encerradas]*, o botão deverá ser desabilitado
                // com a seguinte mensagem informativa: "Opção indisponível. Para o encerramento do processo é
                // necessário que todas as etapas sejam previamente encerradas."
                // [Etapas que compõem o processo não foram encerradas]* = É considerado que a etapa foi encerrada quando:
                // 1) Se o tipo de prazo da etapa for igual a Escalonamento, todos os escalonamentos associadas à etapa
                // estejam encerrados(data de encerramento do escalonamento preenchido).
                else if (tipoPrazoUsadoProcesso == TipoPrazoEtapa.Escalonamento)
                {
                    foreach (var etapa in processo.Etapas)
                    {
                        if (!etapa.Escalonamentos.All(a => a.DataEncerramento.HasValue))
                        {
                            processoRetorno.HabilitaBtnEncerrarProcesso = false;
                            processoRetorno.InstructionEncerrarProcesso = MessagesResource.MSG_InstructionEncerrarProcesso_2;
                            break;
                        }
                    }
                }
                // 2) Senão, se o tipo de prazo da etapa for igual a Período de Vigência, a etapa esteja encerrada (data
                // de encerramento da etapa preenchido).
                else if (tipoPrazoUsadoProcesso == TipoPrazoEtapa.PeriodoVigencia)
                {
                    if (!processo.Etapas.All(e => e.DataEncerramento.HasValue))
                    {
                        processoRetorno.HabilitaBtnEncerrarProcesso = false;
                        processoRetorno.InstructionEncerrarProcesso = MessagesResource.MSG_InstructionEncerrarProcesso_2;
                    }
                }

                foreach (var etapa in processoRetorno.Etapas)
                {
                    // As opções Colocar etapa em manutenção / Liberar a etapa, Alterar, Configurar e Encerrar serão habilitadas 
                    // somente se o papel do usuário logado possui o token de permissão de manutenção de processo parametrizado para o respectivo serviço
                    etapa.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(processo.Servico.TokenPermissaoManutencaoProcesso);

                    // Se o tipo de serviço do processo do serviço em questão, estiver configurado para exigir escalonamento
                    if (processo.Servico.TipoServico.ExigeEscalonamento)
                    {
                        /* ALTERAÇÃO - Task 24440:TSK - Implementar Melhoria - UC_SRC_002_01 - Central de Processos (versão simplificada)*/
                        if (!etapa.DataInicio.HasValue || etapa.DataInicio <= DateTime.MinValue)
                        {
                            etapa.DataInicio = null;
                            etapa.DataFim = null;
                        }

                        /* Alterações Seguindo a Task: 24596 * TSK - Alterar implementação UC_SRC_002_01 - Central de Processos*/
                        etapa.ExibirAcessoEtapa = true;
                    }

                    // Visibilidade do botão "Excluir etapa" na interface
                    etapa.HabilitaExcluirEtapa = true;
                    etapa.InstructionExcluirEtapa = string.Empty;

                    var etapaSGF = SGFHelper.BuscarEtapaSGFPorSeqEtapaCache(etapa.SeqEtapaSgf);

                    if (etapa.SituacaoEtapa == SituacaoEtapa.Encerrada)
                    {
                        etapa.HabilitaExcluirEtapa = false;
                        etapa.InstructionExcluirEtapa = MessagesResource.MSG_InstructionExcluirEtapa_1;
                    }
                    else if (solicitacaoServicoAssociada)
                    {
                        etapa.HabilitaExcluirEtapa = false;
                        etapa.InstructionExcluirEtapa = MessagesResource.MSG_InstructionExcluirEtapa_2;
                    }
                    else if (etapaSGF.Obrigatorio)
                    {
                        etapa.HabilitaExcluirEtapa = false;
                        etapa.InstructionExcluirEtapa = MessagesResource.MSG_InstructionExcluirEtapa_3;
                    }

                    // Visibilidade do botão "Encerrar etapa" na interface
                    etapa.HabilitaEncerrarEtapa = true;
                    etapa.InstructionEncerrarEtapa = string.Empty;

                    // Caso a situação já seja encerrada, não habilita botão de encerrar
                    if (etapa.SituacaoEtapa == SituacaoEtapa.Encerrada)
                    {
                        etapa.HabilitaEncerrarEtapa = false;
                        etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_1;
                    }

                    // Caso ainda esteja habilitado e o tipo de prazo for escalonamento e todos os escalonamentos estiverem encerrados
                    if (etapa.HabilitaEncerrarEtapa &&
                        (etapa.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento && !etapa.Escalonamentos.Any(a => !a.DataEncerramento.HasValue)))
                    {
                        etapa.HabilitaEncerrarEtapa = false;
                        etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_2;
                    }

                    if (etapa.HabilitaEncerrarEtapa && etapa.Ordem > 1)
                    {
                        // Buscando a etapa anterior para fazer as consistências.
                        var etapaAnterior = processoRetorno.Etapas.Where(a => a.Ordem == etapa.Ordem - 1).FirstOrDefault();

                        // if (etapaAnterior != null && etapaAnterior.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento && etapaAnterior.Escalonamentos.Any(a => a.DataEncerramento == null))
                        if (etapaAnterior != null && etapaAnterior.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento)
                        {
                            // Sequenciais dos grupos de escalonamento cujos escalonamentos estão expirados da etapa atual.
                            List<long> seqsGEAtual = etapa.Escalonamentos.Where(a => a.DataFim < DateTime.Today)
                                                                               .SelectMany(b => b.GruposEscalonamento)
                                                                               .Select(c => c.SeqGrupoEscalonamento)
                                                                               .Distinct()
                                                                               .ToList();

                            // Sequenciais dos grupos de escalonamentos da etapa anterior
                            List<long> seqsGEAnterior = etapaAnterior.Escalonamentos.SelectMany(b => b.GruposEscalonamento)
                                                                                               .Select(c => c.SeqGrupoEscalonamento)
                                                                                               .Distinct()
                                                                                               .ToList();
                            // "Join" dos seqs das duas listas acima.
                            List<long> gruposComum = seqsGEAnterior.Intersect(seqsGEAtual).ToList();

                            // Escalonamentos da etapa anterior que estão nos grupos de escalonamento da lista acima.
                            List<EscalonamentoVO> escalonamentos = etapaAnterior.Escalonamentos.Where(a => a.GruposEscalonamento.Any(b => gruposComum.Contains(b.SeqGrupoEscalonamento))).ToList();

                            if (escalonamentos != null && escalonamentos.Any(e => !e.DataEncerramento.HasValue))
                            {
                                etapa.HabilitaEncerrarEtapa = false;
                                etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_3;
                            }
                        }
                        else if (etapaAnterior != null && etapaAnterior.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia && processoRetorno.Etapas.Where(a => a.Ordem < etapa.Ordem).Any(e => !e.DataEncerramento.HasValue))
                        {
                            etapa.HabilitaEncerrarEtapa = false;
                            etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_4;
                        }
                    }

                    // Caso ainda esteja habilitado e o tipo de prazo for escalonamento e todas as etapas possuem data de encerramento, ou não possuem mas a data fim > que agora, desabilita o botão
                    if (etapa.HabilitaEncerrarEtapa &&
                        (etapa.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento &&
                             etapa.Escalonamentos.All(a => a.DataEncerramento.HasValue || (!a.DataEncerramento.HasValue && a.DataFim > DateTime.Now))))
                    {
                        etapa.HabilitaEncerrarEtapa = false;
                        etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_5;
                    }

                    // Caso ainda esteja habilitado e o tipo de prazo da etapa for igual a Período de vigência e, o período da etapa não está expirado
                    if (etapa.HabilitaEncerrarEtapa && (etapa.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia && (!etapa.DataFim.HasValue || etapa.DataFim > DateTime.Today)))
                    {
                        etapa.HabilitaEncerrarEtapa = false;
                        etapa.InstructionEncerrarEtapa = MessagesResource.MSG_InstructionEncerrarEtapa_6;
                    }
                }

                // Adiciona o processo a lista para retorno
                retorno.Add(processoRetorno);
            }

            return retorno;
        }

        private bool HabilitaBtnGrupoEscalonamento(Processo processo)
        {
            if (processo.Servico.TipoServico.ExigeEscalonamento)
            {
                foreach (var etapa in processo.Etapas)
                {
                    if (etapa.Escalonamentos == null || etapa.Escalonamentos.Count <= 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        public List<SMCDatasourceItem> BuscarProcessosPorServicoSelect(long seqServico)
        {
            var spec = new ProcessoFilterSpecification() { SeqServico = seqServico };

            spec.SetOrderByDescending(p => p.CicloLetivo.AnoNumeroCicloLetivo);
            spec.SetOrderBy(p => p.Descricao);

            var result = this.SearchProjectionBySpecification(spec, p => new SMCDatasourceItem()
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();

            return result;
        }

        public List<SMCDatasourceItem> BuscarProcessosPorAlunoSelect(ServicoPorAlunoFiltroVO filtro)
        {
            /* Os processos deverão ser listados inicialmente de acordo com os seguintes critérios:
               · O período esteja vigente (considerando Data Início e Data fim) e que esteja associado aos serviços do tipo selecionado e,
               · A entidade responsável do solicitante esteja associada aos serviços e parametrizada com o tipo de unidade igual a
                 "Entidade Responsável".
            */
            Dictionary<long, SituacaoAlunoVO> dicSituacoes = new Dictionary<long, SituacaoAlunoVO>();

            // Recupera qual a instituição nível tipo vínculo do aluno
            var instituicaoNivelTipoVinculoAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(filtro.SeqAluno);

            // Recupera a entidade responsável do aluno
            var seqEntidadeResponsavelAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(filtro.SeqAluno), x => x.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo);

            // Filtra os processos
            var specProcesso = new ProcessoFilterSpecification
            {
                OrigemSolicitacaoServico = filtro.OrigemSolicitacaoServico,
                SeqTipoServico = filtro.SeqTipoServico,
                TipoAtuacao = TipoAtuacao.Aluno,
                SeqInstituicaoNivelTipoVinculoAluno = instituicaoNivelTipoVinculoAluno.Seq,
                Com1EtapaAtiva = filtro.Com1EtapaAtiva,
                SeqUnidadeResponsavel = seqEntidadeResponsavelAluno,
                TipoUnidadeResponsavel = filtro.TipoUnidadeResponsavel
            };
            specProcesso.SetOrderBy(x => x.Descricao);
            var processos = SearchProjectionBySpecification(specProcesso, x => new
            {
                Seq = x.Seq,
                Descricao = x.Descricao,
                TokenTipoServico = x.Servico.TipoServico.Token,
                SeqCicloLetivo = x.SeqCicloLetivo,
                SeqsSituacoesMatricula = x.Servico.SituacoesAluno.Where(s => s.PermissaoServico == filtro.PermissaoServico).Select(s => s.SeqSituacaoMatricula).Distinct()
            }).ToList();

            List<SMCDatasourceItem> ret = new List<SMCDatasourceItem>();

            if (processos.Any())
            {
                //Spec para recuperar os processos vinculados a entidade responsavel do aluno
                var specProcessoUnidadeResponsavel = new ProcessoUnidadeResponsavelFilterSpecification()
                {
                    SeqsProcessos = processos.Select(p => p.Seq).ToArray(),
                    SeqEntidadeResponsavel = seqEntidadeResponsavelAluno
                };

                //Lista de processos vinculados a unidade responsável do aluno
                var processosUnidadesResponsaveisAluno = this.ProcessoUnidadeResponsavelDomainService.SearchProjectionBySpecification(specProcessoUnidadeResponsavel, p => new
                {
                    SeqProcesso = p.SeqProcesso,
                    TipoUnidadeResponsavel = p.TipoUnidadeResponsavel
                }).ToList();

                // Recupera a situação atual do aluno
                var situacaoAtualAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(filtro.SeqAluno);
                if (situacaoAtualAluno != null)
                    dicSituacoes.Add(situacaoAtualAluno.SeqCiclo, situacaoAtualAluno);

                // Cria lista de retorno
                foreach (var processo in processos)
                {
                    /* Se o campo "Tipo de serviço" for preenchido com o tipo que possui o token “PLANO_ESTUDO”,
                     * listar os processos que possuem ciclo letivo em que a pessoa-atuação tem registro de plano de estudo.
                     * Este filtro deve ser acrescentado dentre os demais filtros já realizados para o campo em questão.
                    */
                    if (processo.TokenTipoServico == TOKEN_TIPO_SERVICO.PLANO_ESTUDO && processo.SeqCicloLetivo.HasValue)
                    {
                        // Verifica se o aluno possui plano de estudo no ciclo letivo do processo
                        if (!AlunoDomainService.AlunoPossuiPlanoEstudoNoCicloLetivo(filtro.SeqAluno, processo.SeqCicloLetivo.Value))
                            continue;
                    }

                    //Se o tipo da unidade responsavel vinculada ao aluno for diferente de "entidade responsável" não adiciona na lista
                    var tipoUnidadeResponsavel = processosUnidadesResponsaveisAluno.FirstOrDefault(p => p.SeqProcesso == processo.Seq).TipoUnidadeResponsavel;

                    if (tipoUnidadeResponsavel != TipoUnidadeResponsavel.EntidadeResponsavel)
                        continue;

                    /*
                       Após a aplicação dos filtros iniciais (citados acima), deverá ser analisado se os processos retornados possuem a identificação
                       de ciclo letivo.
                       Se houver, deverá ser aplicado o critério abaixo:
                       · Deverá ser exibido somente os processos que o aluno possui para os [respectivos ciclos letivos]* a [situação]* parametrizada
                         por serviço que permite a criação de solicitação. [Respectivos ciclos letivos]* = são os ciclos letivos identificados nos
                         processos retornados após os filtros iniciais. [Situação]* = deverá ser considerada a situação dos respectivos ciclos que a
                         data início seja a maior de todas, desconsiderando as situações com data de exclusão setada.
                    */

                    // Caso seja para considerar a situação do aluno...
                    if (filtro.ConsiderarSituacaoAluno)
                    {
                        // Caso tenha ciclo letivo no processo, verifica a situação do aluno no ciclo letivo em questão
                        if (processo.SeqCicloLetivo.HasValue)
                        {
                            if (!dicSituacoes.ContainsKey(processo.SeqCicloLetivo.Value))
                            {
                                var situacaoDic = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(filtro.SeqAluno, processo.SeqCicloLetivo.Value);
                                if (situacaoDic != null)
                                    dicSituacoes.Add(processo.SeqCicloLetivo.Value, situacaoDic);
                            }

                            if (!dicSituacoes.ContainsKey(processo.SeqCicloLetivo.Value) ||
                                !processo.SeqsSituacoesMatricula.Contains(dicSituacoes[processo.SeqCicloLetivo.Value].SeqSituacao.GetValueOrDefault()))
                                continue;
                        }
                        // Caso contrário, verifica a situação do aluno atual
                        else if (situacaoAtualAluno == null || !processo.SeqsSituacoesMatricula.Contains(situacaoAtualAluno.SeqSituacao.GetValueOrDefault()))
                            continue;
                    }

                    // Adiciona na lista de retorno
                    ret.Add(new SMCDatasourceItem
                    {
                        Seq = processo.Seq,
                        Descricao = processo.Descricao
                    });
                }
            }

            // Retorna a lista preenchida com os processos
            return ret;
        }

        private void ValidarModelo(ProcessoVO modelo)
        {
            //Data fim não pode ser maior que a data inicio
            if (modelo.DataFim < modelo.DataInicio)
                throw new ProcessoDataFimMenorDataInicioException();

            //Validando se tem entidade responsável
            if (modelo.EntidadesResponsaveis == null || !modelo.EntidadesResponsaveis.Any(a => a != null))
                throw new ProcessoTodasUnidadesResponsaveisTipoCompartilhadaException();

            //A mesma entidade não pode estar configurada como entidade responsável e entidade compartilhada
            //UN_processo_unidade_responsavel_01
            if (modelo.EntidadesCompartilhadas != null)
            {
                foreach (var entidadeResponsavel in modelo.EntidadesResponsaveis)
                {
                    if (modelo.EntidadesCompartilhadas.Any(a => a != null && a.Seq == entidadeResponsavel.Seq))
                        throw new ProcessoUnidadeResponsavelRepetidaException();
                }
            }
        }

        public void ValidarModeloNovoProcessoPeriodoFinanceiro(ProcessoVO modelo, List<ProcessoUnidadeResponsavel> listaEntidadesResponsaveis)
        {
            if (modelo.SeqCicloLetivo.HasValue)
            {
                DateTime? dataFimPeriodoFinanceiro = BuscarDataFimPeriodoFinanceiroManterProcesso(modelo.Seq, modelo.SeqCicloLetivo.Value, listaEntidadesResponsaveis);

                if (modelo.DataFim.HasValue && dataFimPeriodoFinanceiro.HasValue && modelo.DataFim.Value.Date > dataFimPeriodoFinanceiro.Value.Date)
                {
                    throw new ProcessoDataFimMaiorDataFimPeriodoFinanceiroException();
                }
            }
        }

        public DateTime? BuscarDataFimPeriodoFinanceiroManterProcesso(long seqProcesso, long seqCicloLetivo, List<ProcessoUnidadeResponsavel> listaEntidadesResponsaveis)
        {
            /*FUNÇÃO QUE RETORNA A DATA FIM DO PERIODO FINANCEIRO DO CICLO LETIVO DO PROCESSO
            ESSA É A MESMA FUNÇÃO QUE EXISTE NO ESCALONAMENTODOMAINSERVICE, MAS LÁ SE BUSCA PELO PROCESSOETAPA
            E NESSE CASO É NECESSÁRIO BUSCAR PELO PROCESSO
            SÃO ENVIADAS AS ENTIDADES E CICLO LETIVO ASSOCIADAS AO MODELO, POIS PODE SER UM NOVO PROCESSO, E AS OUTRAS INFORMAÇÕES
            COMO AS CONFIGURAÇÕES SÃO RECUPERADAS DA BASE DE DADOS*/

            var includesCicloLetivo = IncludesCicloLetivo.TiposEvento |
                                      IncludesCicloLetivo.TiposEvento_InstituicaoTipoEvento |
                                      IncludesCicloLetivo.TiposEvento_Parametros |
                                      IncludesCicloLetivo.TiposEvento_Parametros_InstituicaoTipoEventoParametro |
                                      IncludesCicloLetivo.TiposEvento_EventosLetivos |
                                      IncludesCicloLetivo.TiposEvento_EventosLetivos_NiveisEnsino |
                                      IncludesCicloLetivo.TiposEvento_EventosLetivos_ParametrosEntidade;

            var cicloLetivo = this.CicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<CicloLetivo>(seqCicloLetivo), includesCicloLetivo);

            ///Entidades válidas como responsáveis
            var entidadesResponsaveis = this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(listaEntidadesResponsaveis.Select(s => s.SeqEntidadeResponsavel)).Select(s => s.SeqEntidade).ToList();

            ///Localidades do processo validas
            var localidadesResponsaveis = new List<long>();

            ///Curso oferta localidades do processo validas
            var cursoOfertaLocalidadesResponsaveis = new List<long>();

            ///Selecionar os niveis de ensino do processo
            var niveisEnsinoProcesso = new List<long>();

            if (seqProcesso != 0)
            {
                var includesProcesso = IncludesProcesso.Configuracoes_NiveisEnsino |
                                       IncludesProcesso.Configuracoes_Cursos |
                                       IncludesProcesso.Configuracoes_Cursos_CursoOfertaLocalidadeTurno |
                                       IncludesProcesso.Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade |
                                       IncludesProcesso.Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades |
                                       IncludesProcesso.Configuracoes_Cursos_CursoOfertaLocalidadeTurno_CursoOfertaLocalidade_HierarquiasEntidades_ItemSuperior;

                var processo = this.SearchByKey(new SMCSeqSpecification<Processo>(seqProcesso), includesProcesso);

                ///Localidades do processo validas
                localidadesResponsaveis = processo.Configuracoes.SelectMany(s => s.Cursos.Select(sc => sc.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.First().ItemSuperior.SeqEntidade)).ToList();

                ///Curso oferta localidades do processo validas
                cursoOfertaLocalidadesResponsaveis = processo.Configuracoes.SelectMany(s => s.Cursos.Select(sc => sc.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Seq)).ToList();

                ///Selecionar os niveis de ensino do processo
                niveisEnsinoProcesso = processo.Configuracoes.SelectMany(s => s.NiveisEnsino).Select(s => s.SeqNivelEnsino).Distinct().ToList();
            }

            ///Ciclo letivo periodo financeiro
            var ciclosLetivosTipoEvento = cicloLetivo.TiposEvento.Where(w => w.InstituicaoTipoEvento.Token == TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO)
                                                                 .Select(s => s).ToList();

            ///Todos os TIPOS DE EVENTO com os EVENTOS LETIVOS que contenham todos os NIVEIS DE ENSINO do PROCESSO
            var eventosPorTipo = ciclosLetivosTipoEvento.Select(s => new
            {
                Tipo = s,
                Eventos = s.EventosLetivos
                                .Where(ae => niveisEnsinoProcesso
                                        .All(al => ae.NiveisEnsino.Select(sn => sn.Seq).Contains(al)))
            }).ToList();

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

            return null;
        }

        private bool EtapaConfiguradaAplicacao(ProcessoEtapa etapa)
        {
            //Recupera a etapa no SGF
            var etapaSGF = EtapaService.BuscarEtapa(etapa.SeqEtapaSgf);

            //Recuprara a aplicação no SAS
            var aplicacao = AplicacaoService.BuscarAplicacaoPelaSigla(SIGLA_APLICACAO.SGA_ADMINISTRATIVO);

            //Verifica se a etapa está configurara para a aplicação
            return etapaSGF != null && aplicacao != null && etapaSGF.SeqAplicacaoSAS == aplicacao.Seq;
        }

        public SMCPagerData<PosicaoConsolidadaListarVO> BuscarPosicaoConsolidada(PosicaoConsolidadaFiltroVO filtro)
        {
            var lista = PreencherModelo(filtro);

            return new SMCPagerData<PosicaoConsolidadaListarVO>(lista, lista.Count);
        }

        public List<PosicaoConsolidadaListarVO> PreencherModelo(PosicaoConsolidadaFiltroVO filtro)
        {
            var lista = new List<PosicaoConsolidadaListarVO>();

            if (filtro.SeqProcesso.GetValueOrDefault() == 0)
            {
                throw new SMCApplicationException("O processo não foi informado!");
            }

            // Busca as estpas do SGF
            var servico = this.SearchByKey(new SMCSeqSpecification<Processo>(filtro.SeqProcesso.Value), IncludesProcesso.Servico).Servico;
            var etapasSgf = SGFHelper.BuscarEtapasSGFCache(servico.SeqTemplateProcessoSgf);

            //Solicitações de serviço do processo agrupadas pelo grupo de escalonamento
            var spec = filtro.Transform<SolicitacaoServicoFilterSpecification>();
            var solicitacoesServicoPorGrupoEscalonamentoDoProcesso = SolicitacaoServicoDomainService.GroupBy(spec, w => w.SeqGrupoEscalonamento);

            // Lista dos grupos de escalonamento
            var specGrupoEscalonamento = new SMCContainsSpecification<GrupoEscalonamento, long>(x => x.Seq, solicitacoesServicoPorGrupoEscalonamentoDoProcesso.Where(w => w.Key != null).Select(s => s.Key.Value).ToArray());
            var gruposEscalonamentos = GrupoEscalonamentoDomainService.SearchBySpecification(specGrupoEscalonamento).ToList();

            // Busca as etapas do processo
            var etapasDoProcesso = this.SearchByKey(new SMCSeqSpecification<Processo>(filtro.SeqProcesso.Value), IncludesProcesso.Etapas).Etapas.ToList();

            // Para cada grupo
            foreach (var solicitacaoGrupo in solicitacoesServicoPorGrupoEscalonamentoDoProcesso)
            {
                var obj = new PosicaoConsolidadaListarVO();

                if (solicitacaoGrupo.Key > 0)
                {
                    //Preenche o header
                    obj.GrupoEscalonamento = gruposEscalonamentos.FirstOrDefault(g => g.Seq == solicitacaoGrupo.Key.Value).Descricao;
                    obj.QuantidadeSolicitacoes = solicitacaoGrupo.Value.Count();
                    obj.SeqGrupoEscalonamento = gruposEscalonamentos.FirstOrDefault(g => g.Seq == solicitacaoGrupo.Key.Value).Seq;
                }

                PreencherListaEtapas(lista, etapasDoProcesso, solicitacaoGrupo.Value, obj, etapasSgf);
            }

            return lista.OrderBy(w => w.GrupoEscalonamento).ToList();
        }

        private void PreencherListaEtapas(List<PosicaoConsolidadaListarVO> lista, List<ProcessoEtapa> etapasDoProcesso, SolicitacaoServico[] solicitacoesServico, PosicaoConsolidadaListarVO obj, EtapaSimplificadaData[] etapasSgf)
        {
            // Busca as solicitações da lista
            var specSolicitacao = new SMCContainsSpecification<SolicitacaoServico, long>(s => s.Seq, solicitacoesServico.Select(s => s.Seq).ToArray());
            var solicitacoes = SolicitacaoServicoDomainService.SearchBySpecification(specSolicitacao, IncludesSolicitacaoServico.Etapas_HistoricosSituacao).ToList();

            // Busca as configurações de etapa
            var specConfiguracoes = new SMCContainsSpecification<ConfiguracaoEtapa, long>(c => c.Seq, solicitacoes.SelectMany(s => s.Etapas.Select(e => e.SeqConfiguracaoEtapa)).ToArray());
            var configuracoes = ConfiguracaoEtapaDomainService.SearchBySpecification(specConfiguracoes).ToList();

            //Etapas do processo para montar grid com a quantidade em cada status(NaoIniciada,EmAndamento,FinalizadaComSucesso,FinalizadaSemSucesso,Cancelada)
            foreach (var etapaProcesso in etapasDoProcesso)
            {
                var etapaVo = new PosicaoConsolidadaEtapaVO();

                etapaVo.Etapa = etapaProcesso.DescricaoEtapa;

                //Solicitacoes do agrupamento do lançamento
                foreach (var sol in solicitacoes)
                {
                    foreach (var etapaDaSolicitacao in sol.Etapas)
                    {
                        var configuracaoEtapa = configuracoes.FirstOrDefault(c => c.Seq == etapaDaSolicitacao.SeqConfiguracaoEtapa);

                        //Verifica se a solicitação está na mesma etapa do processo
                        if (etapaProcesso.Seq == configuracaoEtapa.SeqProcessoEtapa)
                        {
                            PreencheStatusSolicitacoesPosicaoConsolidada(etapaVo, etapaDaSolicitacao, etapasSgf.FirstOrDefault(e => e.Seq == etapaProcesso.SeqEtapaSgf));
                        }
                    }
                }

                etapaVo.Total = etapaVo.Cancelada + etapaVo.EmAndamento + etapaVo.FinalizadaComSucesso + etapaVo.FinalizadaSemSucesso + etapaVo.NaoIniciada;
                obj.Etapas.Add(etapaVo);
            }
            lista.Add(obj);
        }

        private void PreencheStatusSolicitacoesPosicaoConsolidada(PosicaoConsolidadaEtapaVO etapaVo, SolicitacaoServicoEtapa etapaDaSolicitacao, EtapaSimplificadaData etapaSgf)
        {
            // Ultima situação da etapa
            var ultimoHistoricoDaSolicitacao = etapaDaSolicitacao.HistoricosSituacao?.Where(w => w.DataExclusao == null)?.OrderByDescending(w => w.Seq).FirstOrDefault();

            if (ultimoHistoricoDaSolicitacao == null)
                return;

            var situacaoEtapaSGF = etapaSgf.Situacoes.FirstOrDefault(e => e.Seq == ultimoHistoricoDaSolicitacao.SeqSituacaoEtapaSgf);

            if (situacaoEtapaSGF == null)
                return;

            if (situacaoEtapaSGF.SituacaoInicialEtapa)
            {
                etapaVo.NaoIniciada++;
            }
            else if (!situacaoEtapaSGF.SituacaoInicialEtapa && !situacaoEtapaSGF.SituacaoFinalEtapa)
            {
                etapaVo.EmAndamento++;
            }
            else
            {
                switch (situacaoEtapaSGF.ClassificacaoSituacaoFinal)
                {
                    case ClassificacaoSituacaoFinal.FinalizadoComSucesso:
                        etapaVo.FinalizadaComSucesso++;
                        break;

                    case ClassificacaoSituacaoFinal.FinalizadoSemSucesso:
                        etapaVo.FinalizadaSemSucesso++;
                        break;

                    case ClassificacaoSituacaoFinal.Cancelado:
                        etapaVo.Cancelada++;
                        break;
                }
            }
        }

        /// <summary>
        /// Busca o ciclo letivo do processo atual
        /// </summary>
        /// <param name="seq">Sequencial do processo</param>
        /// <returns>Sequencial do ciclo letivo</returns>
        public long? BuscarCicloLetivoProcesso(long seq)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seq), p => p.SeqCicloLetivo);

            return result;
        }

        public List<SMCDatasourceItem> BuscarEtapasDoProcessoSelect(long seqProcesso)
        {
            var retorno = new List<SMCDatasourceItem>();

            //Não foi possivel colocar long? porque o Dynamic não reconheceu como tipo primitivo e por isso passava parâmetro errado
            if (seqProcesso > 0)
            {
                var etapasDoProcesso = this.SearchByKey(new SMCSeqSpecification<Processo>(seqProcesso), IncludesProcesso.Etapas);

                if (etapasDoProcesso != null && etapasDoProcesso.Etapas != null && etapasDoProcesso.Etapas.Count > 0)
                {
                    foreach (var item in etapasDoProcesso.Etapas)
                        retorno.Add(new SMCDatasourceItem(item.Seq, item.DescricaoEtapa));
                }
            }

            return retorno;
        }

        /// <summary>
        /// Busca a descrição do ciclo letivo do processo
        /// </summary>
        /// <param name="seq">Sequencial do processo</param>
        /// <returns>Descrição do ciclo letivo</returns>
        public string BuscarDescricaoCicloLetivoProcesso(long seq)
        {
            var result = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seq), p => p.CicloLetivo.Descricao);

            return result;
        }

        /// <summary>
        /// Busca uma lista contendo todos os processos
        /// </summary>
        /// <param name="filtros">Filtros e ordenação</param>
        /// <returns>Dados dos processos filtrados e ordenados</returns>
        public List<SMCDatasourceItem> BuscarProcessosSelect(ProcessoFiltroVO filtros)
        {
            // Caso não seja informado um filto, considera um filtro em branco com ordenação por descrição para manter o comportamento original
            var spec = filtros?.Transform<ProcessoFilterSpecification>() ?? new ProcessoFilterSpecification();
            if (!spec.OrderByClauses.SMCAny())
            {
                spec.SetOrderBy(o => o.Descricao);
            }

            var processos = SearchProjectionBySpecification(spec, s => new SMCDatasourceItem() { Seq = s.Seq, Descricao = s.Descricao })
                .ToList();

            return processos;
        }

        /// <summary>
        /// Busca um processo por dados do processo ou do seu relacionamento com processo seletivo
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Dados do processo</returns>
        public ProcessoVO BuscarProcesso(ProcessoFiltroVO filtros)
        {
            long? seqProcesso = null;
            // Caso seja para buscar por dados de ProcessoSeletivoProcessoMatricula, recupera primeiro o seq do processo nessa tabela
            if (filtros.SeqCampanhaCicloLetivo.HasValue || filtros.SeqProcessoSeletivo.HasValue)
            {
                var specProcessoMatricula = new ProcessoSeletivoProcessoMatriculaFilterSpecification()
                {
                    SeqCampanhaCicloLetivo = filtros.SeqCampanhaCicloLetivo,
                    SeqProcessoSeletivo = filtros.SeqProcessoSeletivo
                };
                seqProcesso = this.ProcessoSeletivoProcessoMatriculaDomainService.SearchProjectionByKey(specProcessoMatricula, p => p.SeqProcesso);
            }

            var specProcesso = filtros.Transform<ProcessoFilterSpecification>(new { Seq = seqProcesso });
            return this.SearchByKey(specProcesso).Transform<ProcessoVO>();
        }

        /// <summary>
        /// Buscar dados do processo para montar o cabeçalho das ações relativas ao mesmo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="quantidadeSolicitacoes">Exibir a quantidade de solicitações do processo</param>
        /// <returns></returns>
        public ProcessoCabecalhoVO BuscarCabecalhoProcesso(long seqProcesso, bool quantidadeSolicitacoes)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seqProcesso), p => new ProcessoCabecalhoVO()
            {
                SeqProcesso = p.Seq,
                DescricaoProcesso = p.Descricao,
                DescricaoCicloLetivo = p.CicloLetivo.Descricao,
                DataInicio = p.DataInicio,
                DataFim = p.DataFim,
                DataEncerramento = p.DataEncerramento,
                ProcessoEncerrado = p.DataEncerramento.HasValue && p.DataEncerramento.Value < DateTime.Now
            });

            registro.ExibirQuantidade = quantidadeSolicitacoes;
            if (quantidadeSolicitacoes)
                registro.QuantidadeSolicitacoesProcesso = SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqProcesso = seqProcesso });

            return registro;
        }

        public CabecalhoInformacaoProcessoVO BuscarCabecalhoDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seqProcesso), p => new CabecalhoInformacaoProcessoVO()
            {
                SeqProcesso = seqProcesso,
                DescricaoProcesso = p.Descricao,
                DescricaoGrupoEscalonamento = seqGrupoEscalonamento.HasValue && seqGrupoEscalonamento.Value > 0 ? p.GruposEscalonamento.FirstOrDefault(g => g.Seq == seqGrupoEscalonamento.Value).Descricao : null,
                SeqGrupoEscalonamento = seqGrupoEscalonamento
            });

            return registro;
        }

        public InformacaoProcessoListaVO BuscarDadosProcesso(long seqProcesso, long? seqGrupoEscalonamento)
        {
            var result = new InformacaoProcessoListaVO() { SeqProcesso = seqProcesso, SeqGrupoEscalonamento = seqGrupoEscalonamento };

            var processo = this.SearchProjectionByKey(new SMCSeqSpecification<Processo>(seqProcesso), p => new
            {
                ExibirData = p.Etapas.Any(e => e.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento || e.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia),
                ExibirPrazo = p.Etapas.Any(e => e.TipoPrazoEtapa != TipoPrazoEtapa.Escalonamento && e.TipoPrazoEtapa != TipoPrazoEtapa.PeriodoVigencia),
                InformacoesProcesso = p.Etapas.Select(e => new InformacaoProcessoItemVO()
                {
                    SeqProcesso = seqProcesso,
                    DescricaoEtapa = e.DescricaoEtapa,
                    SituacaoEtapa = e.SituacaoEtapa,
                    TipoPrazoEtapa = e.TipoPrazoEtapa,
                    NumeroDiasPrazoEtapa = e.NumeroDiasPrazoEtapa,
                    DataInicio = (e.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento || e.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia) && seqGrupoEscalonamento.HasValue && seqGrupoEscalonamento.Value > 0 ? p.GruposEscalonamento.FirstOrDefault(g => g.Seq == seqGrupoEscalonamento).Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == e.SeqEtapaSgf).Escalonamento.DataInicio : e.DataInicio,
                    DataFim = (e.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento || e.TipoPrazoEtapa == TipoPrazoEtapa.PeriodoVigencia) && seqGrupoEscalonamento.HasValue && seqGrupoEscalonamento.Value > 0 ? p.GruposEscalonamento.FirstOrDefault(g => g.Seq == seqGrupoEscalonamento).Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.SeqEtapaSgf == e.SeqEtapaSgf).Escalonamento.DataFim : e.DataFim
                }).ToList()
            });

            result.ExibirData = processo.ExibirData;
            result.ExibirPrazo = processo.ExibirPrazo;
            result.InformacoesProcesso = new SMCPagerData<InformacaoProcessoItemVO>(processo.InformacoesProcesso, processo.InformacoesProcesso.Count);

            return result;
        }

        public void CriarAgendamentoPreparacaoRematricula(long seqProcesso, long seqAgendamento)
        {
            var processo = new Processo()
            {
                Seq = seqProcesso,
                SeqAgendamentoSat = seqAgendamento,
                SituacaoAgendamento = SituacaoAgendamento.Criado
            };
            UpdateFields(processo, x => x.SeqAgendamentoSat, x => x.SituacaoAgendamento);
        }

        public void AtualizarStatusAgendamento(long seqProcesso, SituacaoAgendamento status)
        {
            var processo = new Processo()
            {
                Seq = seqProcesso,
                SituacaoAgendamento = status
            };
            UpdateFields(processo, x => x.SituacaoAgendamento);
        }

        public bool ValidarBloqueioSituacaoDocumentacao(List<long> seqsProcessos)
        {
            var spec = new SMCContainsSpecification<Processo, long>(p => p.Seq, seqsProcessos.ToArray());

            var processoPossuiConfiguracaoDocumentacao = this.SearchProjectionBySpecification(spec,
                p => p.Etapas.Any(e => e.ConfiguracoesEtapa.Any(c => c.DocumentosRequeridos.Any())));

            return !processoPossuiConfiguracaoDocumentacao.Any(p => p);
        }

        public List<SMCDatasourceItem> BuscarProcessosMatriculaPorCicloLetivoSelect(long seqCicloLetivo, long seqCampanha)
        {
            var seqEntidadeResponsavel = CampanhaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Campanha>(seqCampanha), x => x.SeqEntidadeResponsavel);

            var spec = new ProcessoMatriculaPorCicloLetivoSpecification(seqCicloLetivo, seqEntidadeResponsavel);
            return SearchProjectionBySpecification(spec, x => new SMCDatasourceItem
            {
                Seq = x.Seq,
                Descricao = x.Descricao
            }).ToList();
        }

        public ConsultaPosicaoGeralVO BuscarPosicaoConsolidadaGeral(ProcessoFiltroVO filtros)
        {
            var spec = filtros.Transform<ProcessoFilterSpecification>();
            spec.SetOrderBy(o => o.DataInicio);

            int total = 0;
            var lista = new List<PosicaoConsolidadaListarVO>();
            // Busca os dados dos processos (base da consulta)
            var dadosProcessos = SearchProjectionBySpecification(spec, p => new
            {
                p.Seq,
                DescricaoProcesso = p.Descricao,
                p.Servico.SeqTemplateProcessoSgf,
                Etapas = p.Etapas.ToList()
            }, out total).ToList();

            // Caso não encontre nenhuma solicitação, retona o modelo vazio para não fazer as consultas no sgf
            if (total == 0)
            {
                return new ConsultaPosicaoGeralVO();
            }

            // Busca as etapas do SGF
            var templates = dadosProcessos
                .Select(s => s.SeqTemplateProcessoSgf)
                .Distinct()
                .ToDictionary(k => k, e => SGFHelper.BuscarEtapasSGFCache(e));

            //Solicitações de serviço agrupadas por processo
            var specSolicitacao = new SolicitacaoServicoFilterSpecification()
            {
                SeqsProcessos = dadosProcessos.Select(s => s.Seq).ToList()
            };
            var solicitacoesServicoPorGrupoEscalonamentoDoProcesso = SolicitacaoServicoDomainService.GroupBy(specSolicitacao, w => w.ConfiguracaoProcesso.SeqProcesso);

            // Para cada processo
            foreach (var processo in dadosProcessos)
            {
                var etapasSgf = templates[processo.SeqTemplateProcessoSgf];
                var solicitacoes = solicitacoesServicoPorGrupoEscalonamentoDoProcesso.FirstOrDefault(f => f.Key == processo.Seq).Value ?? new SolicitacaoServico[] { };
                var obj = new PosicaoConsolidadaListarVO()
                {
                    DescricaoProcesso = processo.DescricaoProcesso,
                    QuantidadeSolicitacoes = solicitacoes.Length
                };
                PreencherListaEtapas(lista, processo.Etapas, solicitacoes, obj, etapasSgf);
            }

            // Recupera todos os códigos de processos ignorando a paginação
            var specTodosProcessos = new ProcessoFilterSpecification()
            {
                SeqsServicos = filtros.SeqsServicos,
                SeqsEntidadesResponsaveis = filtros.SeqsEntidadesResponsaveis,
                SeqCicloLetivo = filtros.SeqCicloLetivo,
                SeqsProcesso = filtros.SeqsProcesso
            };
            var seqsTodosProcessos = SearchProjectionBySpecification(specTodosProcessos, p => p.Seq).ToList();

            // Recupera o total de solicitações nos processos sem paginação
            var specSolicitacaoTotal = new SolicitacaoServicoFilterSpecification()
            {
                SeqsProcessos = seqsTodosProcessos
            };
            var totalSolicitacoes = SolicitacaoServicoDomainService.Count(specSolicitacaoTotal);

            return new ConsultaPosicaoGeralVO()
            {
                QuantidadeSolicitacoesTotal = totalSolicitacoes,
                Processos = new SMCPagerData<PosicaoConsolidadaListarVO>(lista, total)
            };
        }

        /// <summary>
        /// Executa procedimentos para encerramento do processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo a ser encerrado</param>
        public void EncerrarProcesso(long seqProcesso)
        {
            // Busca os dados do processo
            var spec = new SMCSeqSpecification<Processo>(seqProcesso);
            var dadosProcesso = this.SearchProjectionByKey(spec, p => new
            {
                TokenServico = p.Servico.Token,
                Etapas = p.Etapas.Select(e => new
                {
                    SeqProcessoEtapa = e.Seq,
                    TipoPrazoEtapa = e.TipoPrazoEtapa,
                    DataEncerramentoEtapa = e.DataEncerramento
                })
            });

            // Inicia a transação
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // De acordo com o token do serviço do processo, realiza as ações de encerramento
                switch (dadosProcesso.TokenServico)
                {
                    // RN_MAT_115 - ENCERRAMENTO RENOVAÇÃO MATRÍCULA
                    case TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU:
                    // Task 33503:TSK - Alterar UC_SRC_002_01 - Central de Processos - Encerrar Processo Reabertura
                    case TOKEN_SERVICO.MATRICULA_REABERTURA:
                        this.EncerrarProcessoRenovacao(seqProcesso, dadosProcesso.TokenServico == TOKEN_SERVICO.MATRICULA_REABERTURA);
                        break;
                }

                // Preencher a data de encerramento do processo
                Processo processoEncerrado = new Processo()
                {
                    Seq = seqProcesso,
                    DataEncerramento = DateTime.Now
                };
                this.UpdateFields<Processo>(processoEncerrado, p => p.DataEncerramento);

                // Caso o tipo de prazo seja escalonamento, preenche a data de fim da etapa e a situação da etapa = Encerrada.
                foreach (var etapa in dadosProcesso.Etapas.Where(e => e.TipoPrazoEtapa == TipoPrazoEtapa.Escalonamento))
                {
                    ProcessoEtapa processoEtapaEncerrado = new ProcessoEtapa()
                    {
                        Seq = etapa.SeqProcessoEtapa,
                        SituacaoEtapa = SituacaoEtapa.Encerrada,
                        DataEncerramento = DateTime.Now
                    };
                    ProcessoEtapaDomainService.UpdateFields<ProcessoEtapa>(processoEtapaEncerrado, p => p.DataEncerramento, p => p.SituacaoEtapa);
                }

                // Fim da transação
                transacao.Commit();
            }
        }

        public long SalvarCopiaProcesso(CopiarProcessoVO modelo)
        {
            /*DICIONÁRIO COM OS SEQUENCIAIS DE ORIGEM E DESTINO DO ESCALONAMENTO, PARA GARANTIR QUE VAI PEGAR 
            O ESCALONAMENTO CORRETO, SEM PRECISAR BUSCAR O ESCALOMENTO, CASO TENHA ALGUM OUTRO REGISTRO TENHA 
            A MESMA DATA E SEJA COMPLEXO DIFERENCIAR QUAL É O ESCALONAMENTO DAQUELE GRUPO*/
            Dictionary<long, long> MapeamentoSequenciaisEscalonamentoOrigemDestino = new Dictionary<long, long>();

            var processoOrigemSpec = new ProcessoFilterSpecification { Seq = modelo.ProcessoOrigem.Seq, TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel };
            var processoOrigem = this.SearchByKey(processoOrigemSpec,
                IncludesProcesso.Etapas | IncludesProcesso.Configuracoes_NiveisEnsino | IncludesProcesso.Configuracoes_Cursos | IncludesProcesso.Configuracoes_TiposVinculoAluno | IncludesProcesso.GruposEscalonamento | IncludesProcesso.Servico | IncludesProcesso.Servico_TipoServico);

            ValidarModeloCopiarProcesso(modelo, processoOrigem);

            var dominioProcesso = modelo.Transform<Processo>();

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    dominioProcesso.SeqServico = modelo.ProcessoOrigem.SeqServico;
                    dominioProcesso.SeqAgendamentoSat = null;
                    dominioProcesso.SituacaoAgendamento = null;
                    dominioProcesso.DataEncerramento = null;

                    this.SaveEntity(dominioProcesso);

                    var processoUnidadesResponsaveisOrigem = this.ProcessoUnidadeResponsavelDomainService.SearchBySpecification(new ProcessoUnidadeResponsavelFilterSpecification() { SeqProcesso = processoOrigem.Seq }).ToList();

                    //TASK 61414
                    //1) Ao copiar e criar um novo processo, incluir a seguinte consistência no comando "Salvar":
                    //Apenas SE o processo for de um serviço cujo token é um dos listados abaixo:
                    var listaServicos = new List<string>
                    {
                       TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU,
                       TOKEN_SERVICO.SOLICITACAO_MATRICULA_INGRESSANTE_STRICTO_SENSU_DISCIPLINA_ISOLADA,
                       TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                       TOKEN_SERVICO.MATRICULA_REABERTURA,
                       TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO,
                       TOKEN_SERVICO.SOLICITACAO_ALTERACAO_PLANO_ESTUDO_DISCIPLINA_ISOLADA,
                       TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA,
                       TOKEN_SERVICO.TRANCAMENTO_MATRICULA,
                       TOKEN_SERVICO.CANCELAMENTO_MATRICULA
                    };

                    // Considerar a regra:
                    // Verificar se já existe um processo, para a entidade responsável e o ciclo letivo do processo, que está sendo criado.
                    // Caso existir, abortar a operação e exibir a seguinte mensagem:
                    // "Não é possível prosseguir. Já existe processo deste serviço para o ciclo letivo <Descrição do ciclo letivo>, para as seguintes entidades responsáveis:
                    // - < Nome da entidade 1 >
                    // - < Nome da entidade 2 >."
                    //Caso não existir, prosseguir com o cadastro.
                    if (listaServicos.Contains(processoOrigem.Servico.Token))
                    {
                        // Busca as unidades responsáveis do serviço de origem
                        var unidadeProcessoSpec = new ProcessoUnidadeResponsavelFilterSpecification { SeqProcesso = processoOrigem.Seq, TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel };
                        var seqsUnidades = ProcessoUnidadeResponsavelDomainService.SearchProjectionBySpecification(unidadeProcessoSpec, x => x.SeqEntidadeResponsavel).ToArray();

                        // Verifica se tem algum processo na base que tenha o mesmo ciclo, unidade e token de serviço


                        var processosUnidade = ProcessoUnidadeResponsavelDomainService.SearchBySpecification(new ProcessoUnidadeResponsavelFilterSpecification
                        {
                            SeqCicloLetivo = modelo.SeqCicloLetivo,
                            SeqServico = processoOrigem.SeqServico,
                            TipoUnidadeResponsavel = TipoUnidadeResponsavel.EntidadeResponsavel,
                            SeqsEntidadesResponsaveis = seqsUnidades
                        }).ToList();


                        if (processosUnidade != null && processosUnidade.Count() > 0)
                        {
                            var entidades = string.Empty;
                            foreach (var item in processosUnidade)
                            {
                                var entidade = EntidadeDomainService.SearchByKey(new SMCSeqSpecification<Entidade>(item.SeqEntidadeResponsavel));
                                entidades += $"<br />- {entidade.Nome}";
                            }

                            var descricaoCiclo = CicloLetivoDomainService.BuscarDescricaoFormatadaCicloLetivo(modelo.SeqCicloLetivo.Value);

                            throw new ExisteProcessoServicoCicloLetivoException(descricaoCiclo, entidades);
                        }
                    }

                    foreach (var processoUnidadeResponsavelOrigem in processoUnidadesResponsaveisOrigem)
                    {
                        ProcessoUnidadeResponsavel dominioProcessoUnidadeResponsavel = processoUnidadeResponsavelOrigem.Transform<ProcessoUnidadeResponsavel>();
                        dominioProcessoUnidadeResponsavel.Seq = 0;
                        dominioProcessoUnidadeResponsavel.SeqProcesso = dominioProcesso.Seq;

                        this.ProcessoUnidadeResponsavelDomainService.SaveEntity(dominioProcessoUnidadeResponsavel);
                    }

                    foreach (var etapaCopiar in modelo.EtapasCopiar)
                    {
                        if (etapaCopiar.Associar.HasValue && etapaCopiar.Associar.Value)
                        {
                            var processoEtapaOrigem = processoOrigem.Etapas.FirstOrDefault(a => a.Seq == etapaCopiar.Seq);

                            ProcessoEtapa dominioProcessoEtapa = etapaCopiar.Transform<ProcessoEtapa>();
                            dominioProcessoEtapa.Seq = 0;
                            dominioProcessoEtapa.SeqProcesso = dominioProcesso.Seq;
                            dominioProcessoEtapa.DataEncerramento = null;
                            dominioProcessoEtapa.CentralAtendimento = processoEtapaOrigem.CentralAtendimento;
                            dominioProcessoEtapa.OrientacaoAtendimento = processoEtapaOrigem.OrientacaoAtendimento;
                            dominioProcessoEtapa.FinalizacaoEtapaAnterior = processoEtapaOrigem.FinalizacaoEtapaAnterior;
                            dominioProcessoEtapa.SolicitacaoEtapaAnteriorAtendida = processoEtapaOrigem.SolicitacaoEtapaAnteriorAtendida;
                            dominioProcessoEtapa.ExibeItemMatriculaSolicitante = processoEtapaOrigem.ExibeItemMatriculaSolicitante;
                            dominioProcessoEtapa.ExibeItemAposTerminoEtapa = processoEtapaOrigem.ExibeItemAposTerminoEtapa;
                            dominioProcessoEtapa.Token = processoEtapaOrigem.Token;
                            dominioProcessoEtapa.Ordem = processoEtapaOrigem.Ordem;
                            dominioProcessoEtapa.EtapaCompartilhada = processoEtapaOrigem.EtapaCompartilhada;
                            dominioProcessoEtapa.ControleVaga = processoEtapaOrigem.ControleVaga;

                            this.ProcessoEtapaDomainService.SaveEntity(dominioProcessoEtapa);

                            if (etapaCopiar.CopiarConfiguracoes)
                            {
                                var filtroDadosOrigem = this.ProcessoEtapaFiltroDadoDomainService.SearchBySpecification(new ProcessoEtapaFiltroDadoFilterSpecification() { SeqProcessoEtapa = processoEtapaOrigem.Seq }).ToList();

                                foreach (var filtroDadoOrigem in filtroDadosOrigem)
                                {
                                    ProcessoEtapaFiltroDado dominioFiltroDado = filtroDadoOrigem.Transform<ProcessoEtapaFiltroDado>();
                                    dominioFiltroDado.Seq = 0;
                                    dominioFiltroDado.SeqProcessoEtapa = dominioProcessoEtapa.Seq;

                                    this.ProcessoEtapaFiltroDadoDomainService.SaveEntity(dominioFiltroDado);
                                }

                                var situacoesMatriculaOrigem = this.SituacaoItemMatriculaDomainService.SearchBySpecification(new SituacaoItemMatriculaFilterSpecification() { SeqProcessoEtapa = processoEtapaOrigem.Seq }).ToList();

                                foreach (var situacaoMatriculaOrigem in situacoesMatriculaOrigem)
                                {
                                    SituacaoItemMatricula dominioSituacaoMatricula = situacaoMatriculaOrigem.Transform<SituacaoItemMatricula>();
                                    dominioSituacaoMatricula.Seq = 0;
                                    dominioSituacaoMatricula.SeqProcessoEtapa = dominioProcessoEtapa.Seq;

                                    this.SituacaoItemMatriculaDomainService.SaveEntity(dominioSituacaoMatricula);
                                }
                            }

                            var configuracaoNotificacoesOrigem = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchBySpecification(new ProcessoEtapaConfiguracaoNotificacaoSpecification() { SeqProcessoEtapa = processoEtapaOrigem.Seq },
                                IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoUnidadeResponsavel_EntidadeResponsavel |
                                IncludesProcessoEtapaConfiguracaoNotificacao.TipoNotificacao).ToList();

                            foreach (var configuracaoNotificacaoOrigem in configuracaoNotificacoesOrigem)
                            {
                                ProcessoEtapaConfiguracaoNotificacao dominioConfiguracaoNotificacao = configuracaoNotificacaoOrigem.Transform<ProcessoEtapaConfiguracaoNotificacao>();
                                dominioConfiguracaoNotificacao.Seq = 0;
                                dominioConfiguracaoNotificacao.SeqProcessoEtapa = dominioProcessoEtapa.Seq;

                                var processoUnidadeResponsavelDestino = this.ProcessoUnidadeResponsavelDomainService.SearchBySpecification(new ProcessoUnidadeResponsavelFilterSpecification() { SeqProcesso = dominioProcesso.Seq, SeqEntidadeResponsavel = configuracaoNotificacaoOrigem.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel }).FirstOrDefault();
                                dominioConfiguracaoNotificacao.SeqProcessoUnidadeResponsavel = processoUnidadeResponsavelDestino.Seq;

                                var configuracaoNotificacaoEmail = this.NotificacaoService.BuscarConfiguracaoNotificacaoEmail(configuracaoNotificacaoOrigem.SeqConfiguracaoTipoNotificacao);
                                configuracaoNotificacaoEmail.Seq = 0;
                                configuracaoNotificacaoEmail.SeqUnidadeResponsavel = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.BuscarEntidadeResponsavelNotificacao(processoUnidadeResponsavelDestino.Seq);

                                var seqNotificacao = this.NotificacaoService.SalvarConfiguracaoTipoNotificacao(configuracaoNotificacaoEmail);
                                dominioConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao = seqNotificacao;

                                this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SaveEntity(dominioConfiguracaoNotificacao);

                                if (!modelo.GruposEscalonamentoCopiar.Any() && configuracaoNotificacaoOrigem.TipoNotificacao.PermiteAgendamento)
                                {
                                    /*Se não tiver grupos de escalonamento para copiar, então não terá a opção de copiar ou não as notificações
                                    daquele grupo, sendo assim os parâmetros precisam ser copiados nesse momento*/

                                    /*Se tiver grupos de escalonamento para copiar, então terá a opção de copiar ou não as notificações
                                    daquele grupo, sendo assim os parâmetros serão copiados no momento em que irá copiar os grupos de escalonamento*/

                                    var parametrosEnvioNotificacaoOrigem = this.ParametroEnvioNotificacaoDomainService.SearchBySpecification(new ParametroEnvioNotificacaoFilterSpecification() { SeqProcessoEtapaConfiguracaoNotificacao = configuracaoNotificacaoOrigem.Seq }).ToList();

                                    foreach (var parametroEnvioNotificacaoOrigem in parametrosEnvioNotificacaoOrigem)
                                    {
                                        ParametroEnvioNotificacao dominioParametroEnvioNotificacao = parametroEnvioNotificacaoOrigem.Transform<ParametroEnvioNotificacao>();
                                        dominioParametroEnvioNotificacao.Seq = 0;
                                        dominioParametroEnvioNotificacao.SeqProcessoEtapaConfiguracaoNotificacao = dominioConfiguracaoNotificacao.Seq;

                                        this.ParametroEnvioNotificacaoDomainService.SaveEntity(dominioParametroEnvioNotificacao);
                                    }
                                }
                            }

                            var escalonamentosOrigem = this.EscalonamentoDomainService.SearchBySpecification(new EscalonamentoFilterSpecification() { SeqProcessoEtapa = processoEtapaOrigem.Seq }).ToList();

                            foreach (var escalonamentoOrigem in escalonamentosOrigem)
                            {
                                /*Verificando se os grupos escalonamento que estão relacionados à esse escalonamento estão selecionados
                                 para serem copiados. Só serão criados os escalonamentos daqueles grupos que serão copiados.*/

                                var gruposEscalonamentoItensOrigem = this.GrupoEscalonamentoItemDomainService.SearchBySpecification(new GrupoEscalonamentoItemFilterSpecification() { SeqEscalonamento = escalonamentoOrigem.Seq }).ToList();

                                var gruposEscalonamentoBanco = gruposEscalonamentoItensOrigem.Select(a => a.SeqGrupoEscalonamento).Distinct().ToList();
                                var gruposEscalonamentoCopiar = modelo.GruposEscalonamentoCopiar.Where(a => a.CriarGrupo.HasValue && a.CriarGrupo.Value).Select(a => a.Seq).Distinct().ToList();
                                var copiarEscalonamento = gruposEscalonamentoBanco.Intersect(gruposEscalonamentoCopiar).Any();

                                if (copiarEscalonamento)
                                {
                                    Escalonamento dominioEscalonamento = escalonamentoOrigem.Transform<Escalonamento>();
                                    dominioEscalonamento.Seq = 0;
                                    dominioEscalonamento.SeqProcessoEtapa = dominioProcessoEtapa.Seq;
                                    dominioEscalonamento.DataEncerramento = null;

                                    this.EscalonamentoDomainService.SaveEntity(dominioEscalonamento);

                                    MapeamentoSequenciaisEscalonamentoOrigemDestino.Add(escalonamentoOrigem.Seq, dominioEscalonamento.Seq);
                                }
                            }
                        }
                    }

                    foreach (var configuracaoProcessoOrigem in processoOrigem.Configuracoes)
                    {
                        ConfiguracaoProcesso dominioConfiguracaoProcesso = configuracaoProcessoOrigem.Transform<ConfiguracaoProcesso>();
                        dominioConfiguracaoProcesso.Seq = 0;
                        dominioConfiguracaoProcesso.SeqProcesso = dominioProcesso.Seq;

                        foreach (var curso in dominioConfiguracaoProcesso.Cursos)
                        {
                            curso.Seq = curso.SeqConfiguracaoProcesso = 0;
                        }

                        foreach (var nivelEnsino in dominioConfiguracaoProcesso.NiveisEnsino)
                        {
                            nivelEnsino.Seq = nivelEnsino.SeqConfiguracaoProcesso = 0;
                        }

                        foreach (var tipoVinculo in dominioConfiguracaoProcesso.TiposVinculoAluno)
                        {
                            tipoVinculo.Seq = tipoVinculo.SeqConfiguracaoProcesso = 0;
                        }

                        this.ConfiguracaoProcessoDomainService.SaveEntity(dominioConfiguracaoProcesso);

                        var configuracoesEtapaOrigem = this.ConfiguracaoEtapaDomainService.SearchBySpecification(new ConfiguracaoEtapaFilterSpecification() { SeqConfiguracaoProcesso = configuracaoProcessoOrigem.Seq }, IncludesConfiguracaoEtapa.ProcessoEtapa).ToList();

                        foreach (var configuracaoEtapaOrigem in configuracoesEtapaOrigem)
                        {
                            var processoEtapaDestino = this.ProcessoEtapaDomainService.SearchBySpecification(new ProcessoEtapaFilterSpecification() { SeqProcesso = dominioProcesso.Seq, SeqEtapaSGF = configuracaoEtapaOrigem.ProcessoEtapa.SeqEtapaSgf }).FirstOrDefault();

                            if (processoEtapaDestino != null)
                            {
                                var etapaCopiar = modelo.EtapasCopiar.FirstOrDefault(a => a.SeqEtapaSgf == processoEtapaDestino.SeqEtapaSgf);

                                if (etapaCopiar.CopiarConfiguracoes)
                                {
                                    ConfiguracaoEtapa dominioConfiguracaoEtapa = configuracaoEtapaOrigem.Transform<ConfiguracaoEtapa>();
                                    dominioConfiguracaoEtapa.Seq = 0;
                                    dominioConfiguracaoEtapa.SeqConfiguracaoProcesso = dominioConfiguracaoProcesso.Seq;
                                    dominioConfiguracaoEtapa.SeqProcessoEtapa = processoEtapaDestino.Seq;

                                    this.ConfiguracaoEtapaDomainService.SaveEntity(dominioConfiguracaoEtapa);

                                    var configuracaoEtapaOrigemCompleta = this.ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(configuracaoEtapaOrigem.Seq),
                                        IncludesConfiguracaoEtapa.ConfiguracoesBloqueio | IncludesConfiguracaoEtapa.DocumentosRequeridos | IncludesConfiguracaoEtapa.GruposDocumentoRequerido | IncludesConfiguracaoEtapa.ConfiguracoesPagina);

                                    foreach (var configuracaoBloqueioOrigem in configuracaoEtapaOrigemCompleta.ConfiguracoesBloqueio)
                                    {
                                        ConfiguracaoEtapaBloqueio dominioConfiguracaoEtapaBloqueio = configuracaoBloqueioOrigem.Transform<ConfiguracaoEtapaBloqueio>();
                                        dominioConfiguracaoEtapaBloqueio.Seq = 0;
                                        dominioConfiguracaoEtapaBloqueio.SeqConfiguracaoEtapa = dominioConfiguracaoEtapa.Seq;

                                        this.ConfiguracaoEtapaBloqueioDomainService.SaveEntity(dominioConfiguracaoEtapaBloqueio);
                                    }

                                    foreach (var documentoRequeridoOrigem in configuracaoEtapaOrigemCompleta.DocumentosRequeridos)
                                    {
                                        DocumentoRequerido dominioDocumentoRequerido = documentoRequeridoOrigem.Transform<DocumentoRequerido>();
                                        dominioDocumentoRequerido.Seq = 0;
                                        dominioDocumentoRequerido.SeqConfiguracaoEtapa = dominioConfiguracaoEtapa.Seq;

                                        this.DocumentoRequeridoDomainService.SaveEntity(dominioDocumentoRequerido);
                                    }

                                    foreach (var grupoDocumentoRequeridoOrigem in configuracaoEtapaOrigemCompleta.GruposDocumentoRequerido)
                                    {
                                        GrupoDocumentoRequerido dominioGrupoDocumentoRequerido = grupoDocumentoRequeridoOrigem.Transform<GrupoDocumentoRequerido>();
                                        dominioGrupoDocumentoRequerido.Seq = 0;
                                        dominioGrupoDocumentoRequerido.SeqConfiguracaoEtapa = dominioConfiguracaoEtapa.Seq;

                                        this.GrupoDocumentoRequeridoDomainService.SaveEntity(dominioGrupoDocumentoRequerido);

                                        var grupoDocumentoRequeridoOrigemCompleto = this.GrupoDocumentoRequeridoDomainService.SearchByKey(new SMCSeqSpecification<GrupoDocumentoRequerido>(grupoDocumentoRequeridoOrigem.Seq),
                                            IncludesGrupoDocumentoRequerido.Itens_DocumentoRequerido);

                                        foreach (var grupoDocumentoRequeridoItemOrigem in grupoDocumentoRequeridoOrigemCompleto.Itens)
                                        {
                                            GrupoDocumentoRequeridoItem dominioGrupoDocumentoRequeridoItem = grupoDocumentoRequeridoItemOrigem.Transform<GrupoDocumentoRequeridoItem>();
                                            dominioGrupoDocumentoRequeridoItem.Seq = 0;
                                            dominioGrupoDocumentoRequeridoItem.SeqGrupoDocumentoRequerido = dominioGrupoDocumentoRequerido.Seq;

                                            var documentoRequeridoDestino = this.DocumentoRequeridoDomainService.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = dominioConfiguracaoEtapa.Seq, SeqTipoDocumento = grupoDocumentoRequeridoItemOrigem.DocumentoRequerido.SeqTipoDocumento }).FirstOrDefault();
                                            dominioGrupoDocumentoRequeridoItem.SeqDocumentoRequerido = documentoRequeridoDestino.Seq;

                                            this.GrupoDocumentoRequeridoItemDomainService.SaveEntity(dominioGrupoDocumentoRequeridoItem);
                                        }
                                    }

                                    foreach (var configuracaoEtapaPaginaOrigem in configuracaoEtapaOrigemCompleta.ConfiguracoesPagina)
                                    {
                                        ConfiguracaoEtapaPagina dominioConfiguracaoEtapaPagina = configuracaoEtapaPaginaOrigem.Transform<ConfiguracaoEtapaPagina>();
                                        dominioConfiguracaoEtapaPagina.Seq = 0;
                                        dominioConfiguracaoEtapaPagina.SeqConfiguracaoEtapa = dominioConfiguracaoEtapa.Seq;

                                        this.ConfiguracaoEtapaPaginaDomainService.SaveEntity(dominioConfiguracaoEtapaPagina);

                                        var configuracaoEtapaPaginaOrigemCompleta = this.ConfiguracaoEtapaPaginaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapaPagina>(configuracaoEtapaPaginaOrigem.Seq),
                                            IncludesConfiguracaoEtapaPagina.Arquivos | IncludesConfiguracaoEtapaPagina.TextosSecao);

                                        foreach (var textoSecaoOrigem in configuracaoEtapaPaginaOrigemCompleta.TextosSecao)
                                        {
                                            TextoSecaoPagina dominioTextoSecaoPagina = textoSecaoOrigem.Transform<TextoSecaoPagina>();
                                            dominioTextoSecaoPagina.Seq = 0;
                                            dominioTextoSecaoPagina.SeqConfiguracaoEtapaPagina = dominioConfiguracaoEtapaPagina.Seq;

                                            this.TextoSecaoPaginaDomainService.SaveEntity(dominioTextoSecaoPagina);
                                        }

                                        foreach (var arquivoSecaoOrigem in configuracaoEtapaPaginaOrigemCompleta.Arquivos)
                                        {
                                            ArquivoSecaoPagina dominioArquivoSecaoPagina = arquivoSecaoOrigem.Transform<ArquivoSecaoPagina>();
                                            dominioArquivoSecaoPagina.Seq = 0;
                                            dominioArquivoSecaoPagina.SeqConfiguracaoEtapaPagina = dominioConfiguracaoEtapaPagina.Seq;

                                            ArquivoAnexado dominioArquivoAnexado = this.ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(dominioArquivoSecaoPagina.SeqArquivoAnexado));
                                            dominioArquivoAnexado.Seq = 0;
                                            dominioArquivoSecaoPagina.SeqArquivoAnexado = 0;
                                            dominioArquivoSecaoPagina.ArquivoAnexado = dominioArquivoAnexado;

                                            this.ArquivoSecaoPaginaDomainService.SaveEntity(dominioArquivoSecaoPagina);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    foreach (var grupoEscalonamentoCopiar in modelo.GruposEscalonamentoCopiar)
                    {
                        if (grupoEscalonamentoCopiar.CriarGrupo.HasValue && grupoEscalonamentoCopiar.CriarGrupo.Value)
                        {
                            var grupoEscalonamentoOrigem = processoOrigem.GruposEscalonamento.FirstOrDefault(a => a.Seq == grupoEscalonamentoCopiar.Seq);

                            GrupoEscalonamento dominioGrupoEscalonamento = grupoEscalonamentoOrigem.Transform<GrupoEscalonamento>();
                            dominioGrupoEscalonamento.Seq = 0;
                            dominioGrupoEscalonamento.SeqProcesso = dominioProcesso.Seq;
                            dominioGrupoEscalonamento.Ativo = false;

                            this.GrupoEscalonamentoDomainService.SaveEntity(dominioGrupoEscalonamento);

                            var gruposEscalonamentoItensOrigem = this.GrupoEscalonamentoItemDomainService.SearchBySpecification(new GrupoEscalonamentoItemFilterSpecification() { SeqGrupoEscalonamento = grupoEscalonamentoOrigem.Seq }).ToList();

                            foreach (var grupoEscalonamentoItemOrigem in gruposEscalonamentoItensOrigem)
                            {
                                GrupoEscalonamentoItem dominioGrupoEscalonamentoItem = grupoEscalonamentoItemOrigem.Transform<GrupoEscalonamentoItem>();
                                dominioGrupoEscalonamentoItem.Seq = 0;
                                dominioGrupoEscalonamentoItem.SeqGrupoEscalonamento = dominioGrupoEscalonamento.Seq;

                                var mapeamentoEscalonamento = MapeamentoSequenciaisEscalonamentoOrigemDestino.FirstOrDefault(a => a.Key == grupoEscalonamentoItemOrigem.SeqEscalonamento);

                                if (mapeamentoEscalonamento.Key != 0)
                                {
                                    dominioGrupoEscalonamentoItem.SeqEscalonamento = mapeamentoEscalonamento.Value;

                                    this.GrupoEscalonamentoItemDomainService.SaveEntity(dominioGrupoEscalonamentoItem);

                                    //Retirando cópia de parcela para grupo de escalonamento durante a cópia de processo, em acordo com a tarefa 63521
                                    //var grupoEscalonamentoItensParcelaOrigem = this.GrupoEscalonamentoItemParcelaDomainService.SearchBySpecification(new GrupoEscalonamentoItemParcelaFilterSpecification() { SeqGrupoEscalonamentoItem = grupoEscalonamentoItemOrigem.Seq }).ToList();

                                    //foreach (var grupoEscalonamentoItemParcelaOrigem in grupoEscalonamentoItensParcelaOrigem)
                                    //{
                                    //    GrupoEscalonamentoItemParcela dominioGrupoEscalonamentoItemParcela = grupoEscalonamentoItemParcelaOrigem.Transform<GrupoEscalonamentoItemParcela>();
                                    //    dominioGrupoEscalonamentoItemParcela.Seq = 0;
                                    //    dominioGrupoEscalonamentoItemParcela.SeqGrupoEscalonamentoItem = dominioGrupoEscalonamentoItem.Seq;

                                    //    this.GrupoEscalonamentoItemParcelaDomainService.SaveEntity(dominioGrupoEscalonamentoItemParcela);
                                    //}
                                }
                            }

                            if (grupoEscalonamentoCopiar.CopiarNotificacoes.HasValue && grupoEscalonamentoCopiar.CopiarNotificacoes.Value)
                            {
                                var parametrosEnvioNotificacaoOrigem = this.ParametroEnvioNotificacaoDomainService.SearchBySpecification(new ParametroEnvioNotificacaoFilterSpecification() { SeqGrupoEscalonamento = grupoEscalonamentoOrigem.Seq }).ToList();

                                foreach (var parametroEnvioNotificacaoOrigem in parametrosEnvioNotificacaoOrigem)
                                {
                                    ParametroEnvioNotificacao dominioParametroEnvioNotificacao = parametroEnvioNotificacaoOrigem.Transform<ParametroEnvioNotificacao>();
                                    dominioParametroEnvioNotificacao.Seq = 0;
                                    dominioParametroEnvioNotificacao.SeqGrupoEscalonamento = dominioGrupoEscalonamento.Seq;

                                    var processoEtapaConfiguracaoNotificacaoOrigem = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchByKey(new SMCSeqSpecification<ProcessoEtapaConfiguracaoNotificacao>(parametroEnvioNotificacaoOrigem.SeqProcessoEtapaConfiguracaoNotificacao), IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoEtapa);
                                    var processoEtapaDestino = this.ProcessoEtapaDomainService.SearchBySpecification(new ProcessoEtapaFilterSpecification() { SeqProcesso = dominioProcesso.Seq, SeqEtapaSGF = processoEtapaConfiguracaoNotificacaoOrigem.ProcessoEtapa.SeqEtapaSgf }).FirstOrDefault();

                                    if (processoEtapaDestino != null)
                                    {
                                        var processoEtapaConfiguracaoNotificacaoDestino = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchBySpecification(new ProcessoEtapaConfiguracaoNotificacaoSpecification() { SeqProcessoEtapa = processoEtapaDestino.Seq, SeqTipoNotificacao = processoEtapaConfiguracaoNotificacaoOrigem.SeqTipoNotificacao }).FirstOrDefault();
                                        dominioParametroEnvioNotificacao.SeqProcessoEtapaConfiguracaoNotificacao = processoEtapaConfiguracaoNotificacaoDestino.Seq;

                                        this.ParametroEnvioNotificacaoDomainService.SaveEntity(dominioParametroEnvioNotificacao);
                                    }
                                }
                            }
                        }
                    }

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }

            return dominioProcesso.Seq;
        }

        private void ValidarModeloCopiarProcesso(CopiarProcessoVO modelo, Processo processoOrigem)
        {
            if (modelo.DataFim.HasValue)
            {
                //DATA FIM NÃO PODE SER MENOR QUE A DATA DE INÍCIO
                if (modelo.DataFim < modelo.DataInicio)
                    throw new ProcessoDataFimMenorDataInicioException();

                //DATA FIM NÃO PODE SER MENOR QUE A DATA ATUAL
                if (modelo.DataFim < DateTime.Now)
                    throw new ProcessoEtapaDataFimMenorDataAtualException();
            }

            if (processoOrigem.Servico.Token != TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA && processoOrigem.Servico.TipoServico.ExigeEscalonamento == true)
            {
                if (modelo.GruposEscalonamentoCopiar?.Count(c => c.CriarGrupo == true) == 0)
                {
                    throw new ProcessoCopiarProcessoSemGrupoEscalonamentoException();
                }
            }

            //TRECHO COMENTADO POIS SERÁ PERMITDO COPIAR PROCESSOS COM DATAS RETROATIVAS
            //if (modelo.SeqCicloLetivo.HasValue)
            //{
            //    var processoOrigem = this.SearchByKey(new SMCSeqSpecification<Processo>(modelo.ProcessoOrigem.Seq), x => x.UnidadesResponsaveis);
            //    DateTime? dataFimPeriodoFinanceiro = BuscarDataFimPeriodoFinanceiroManterProcesso(modelo.ProcessoOrigem.Seq, modelo.SeqCicloLetivo.Value, processoOrigem.UnidadesResponsaveis.ToList());

            //    if (modelo.DataFim.HasValue && dataFimPeriodoFinanceiro.HasValue && modelo.DataFim.Value.Date > dataFimPeriodoFinanceiro.Value.Date)
            //    {
            //        throw new ProcessoDataFimMaiorDataFimPeriodoFinanceiroCopiarProcessoException();
            //    }
            //}
        }

        /// <summary>
        /// Encerra um processo de renovação
        /// Implementa a RN_MAT_115 - ENCERRAMENTO RENOVAÇÃO MATRÍCULA
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo a ser encerrado</param>
        private void EncerrarProcessoRenovacao(long seqProcesso, bool processoReabertura)
        {
            // Busca os dados do processo
            var spec = new SMCSeqSpecification<Processo>(seqProcesso);
            var dadosProcesso = this.SearchProjectionByKey(spec, p => new
            {
                SeqTemplateProcessoSGF = p.Servico.SeqTemplateProcessoSgf,
                SeqCicloLetivo = p.SeqCicloLetivo
            });

            // Se o processo não tem ciclo letivo, erro
            if (!dadosProcesso.SeqCicloLetivo.HasValue)
                throw new ProcessoSemCicloLetivoException();

            // Busca as situações de solicitação que não são finalizadas com sucesso
            var etapaSGF = SGFHelper.BuscarEtapasSGFCache(dadosProcesso.SeqTemplateProcessoSGF);
            var seqsSituacoesNaoFinalizadasComSucesso = etapaSGF.SelectMany(e => e.Situacoes?.Where(s => s.ClassificacaoSituacaoFinal != ClassificacaoSituacaoFinal.FinalizadoComSucesso).Select(es => es.Seq)).Distinct().ToList();

            // Busca as solicitações do processo que não estão em situação "Finalizado com sucesso"
            var solSpec = new SolicitacaoServicoFilterSpecification()
            {
                SeqProcesso = seqProcesso,
                TipoFiltroCentralSolicitacao = TipoFiltroCentralSolicitacao.EtapaSituacaoAtualSolicitacao,
                SeqsSituacoesEtapa = seqsSituacoesNaoFinalizadasComSucesso
            };
            var solicitacoes = SolicitacaoServicoDomainService.SearchProjectionBySpecification(solSpec, s => new
            {
                SeqSolicitacaoServico = s.Seq,
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                NumeroProtocolo = s.NumeroProtocolo,
                SeqAlunoHistorico = s.SeqAlunoHistorico,
                CodigoAlunoSGP = (s.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                NomeSolicitante = s.PessoaAtuacao.DadosPessoais.Nome,
                NomeSocialSolicitante = s.PessoaAtuacao.DadosPessoais.NomeSocial
            });

            // Busca as datas do ciclo do processo
            DatasEventoLetivoVO cicloProcesso = null;

            // Para cada solicitação encontrada
            foreach (var solicitacao in solicitacoes)
            {
                // Se encontrou alguma solicitação, busca a origem do aluno da primeira delas para recuperar as informações
                // do ciclo letivo do processo.
                // Como todos os alunos são do mesmo programa, as datas serão as mesmas para todos eles
                if (cicloProcesso == null)
                {
                    // Busca os dados de origem do aluno
                    var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacao.SeqPessoaAtuacao);

                    // Busca as informações do ciclo do processo
                    cicloProcesso = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosProcesso.SeqCicloLetivo.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                    if (cicloProcesso == null)
                        throw new CicloLetivoInvalidoException();
                }

                // Busca a situação do aluno no ciclo do processo
                var sitAlunoCicloProcesso = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(solicitacao.SeqPessoaAtuacao, dadosProcesso.SeqCicloLetivo.Value, DateTime.Now);

                // Passo 1: Desconsiderar alunos que não possuem nenhuma situação no ciclo letivo do processo:
                // Ex.: aluno cancelou a matrícula após a criação da solicitação de renovação. Nesse exemplo, o aluno não possui
                // situações no ciclo letivo do processo pois a mesma foi excluída.
                if (sitAlunoCicloProcesso == null)
                {
                    // Desconsidera o aluno
                    continue;
                }

                // Passo 2: Desconsiderar alunos que possui situação no ciclo letivo do processo diferente de APTO_MATRICULA:
                // Verificar se o aluno possui alguma situação de matrícula no mesmo ciclo letivo do processo diferente da
                // situação APTO_MATRICULA. Se existir, desconsiderar a solicitação.
                // Ex.: houve algum acerto de dados e a solicitação foi cancelada porque a matrícula foi renovada automaticamente.
                // Aluno em mobilidade no ciclo letivo da renovação.
                if (sitAlunoCicloProcesso.SeqSituacao.HasValue &&
                    !sitAlunoCicloProcesso.TokenSituacaoMatricula.Equals(TOKENS_SITUACAO_MATRICULA.APTO_MATRICULA))
                {
                    // Desconsidera o aluno
                    continue;
                }

                // Passo 3: Desconsiderar alunos que já defenderam:
                //Verificar se o aluno possui data de defesa cadastrada,
                //cujo o trabalho é de tipo de trabalho parametrizado de acordo com a instituição de ensino logada e o
                //nível de ensino da pessoa-atuação em questão gera financeiro na entrega trabalho.
                //Se possuir, desconsiderar a solicitação e excluir as situações futuras do aluno,
                //a partir do ciclo letivo do processo, setando as informações conforme abaixo:
                //-Data de exclusão: data atual do sistema
                //- Usuário de exclusão: usuário logado/ ENCERRAMENTO RENOVAÇÃO
                //- Descrição observação da exclusão: "Excluído, pois, o aluno já defendeu"
                //- Solicitação de serviço de exclusão: solicitação de serviço de renovação
                var dataDefesa = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(solicitacao.SeqPessoaAtuacao).DataDefesa;
                if (dataDefesa.HasValue)
                {
                    // Excluir as situações futuras do aluno
                    AlunoHistoricoSituacaoDomainService.ExcluirTodasSituacoesAlunoNoCiclo(solicitacao.SeqPessoaAtuacao, cicloProcesso.SeqCicloLetivo, "Excluído, pois, o aluno já defendeu.", solicitacao.SeqSolicitacaoServico, "ENCERRAMENTO RENOVAÇÃO");
                    // Desconsidera o aluno
                    continue;
                }

                // Passo 4: Cancelar a matrícula do aluno por não efetivação, de acordo com a regra

                // 4.1 - Excluir as situações de matrícula existentes no ciclo letivo do processo
                // Excluir as situações futuras do aluno, a partir do ciclo letivo do processo, setando as informações conforme abaixo:
                // - Data de exclusão: data atual do sistema
                // - Usuário de exclusão: usuário logado
                // - Descrição observação da exclusão: "Excluído devido à não efetivação da renovação de matrícula"
                // - Solicitação de serviço de exclusão: solicitação de serviço de renovação
                AlunoHistoricoSituacaoDomainService.ExcluirTodasSituacoesAlunoNoCiclo(solicitacao.SeqPessoaAtuacao, cicloProcesso.SeqCicloLetivo, "Excluído devido à não efetivação da renovação de matrícula.", solicitacao.SeqSolicitacaoServico);

                // 4.2 - Cancelar a matrícula do aluno:
                // RN_MAT_083 - Cancelamento de matrícula, passando os seguintes parâmetros:
                // - Token da situação de cancelamento = “CANCELADO_NAO_EFETIVACAO_MATRICULA“
                // - Observação de cancelamento = "O aluno não renovou a matrícula no prazo estabelecido."
                // - Data de cancelamento da matrícula = data de início do ciclo letivo do processo conforme PERIODO_CICLO_LETIVO do aluno.
                // - Solicitação de serviço do cancelamento(opcional) = sequencial da solicitação de renovação do aluno em questão.
                // - Jubilado = Não
                // - CancelarBeneficio = Não
                // - Observação cancelamento solicitação = 'Solicitação cancelada devido à não efetivação da renovação de matrícula.'
                // - Tipo de cancelamento SGP = 2 (Cancelamento POR FALTA DE RENOVAÇÃO)
                CancelarMatriculaVO dadosCancelamento = new CancelarMatriculaVO()
                {
                    TokenSituacaoCancelamento = TOKENS_SITUACAO_MATRICULA.CANCELADO_NAO_EFETIVACAO_MATRICULA,
                    ObservacaoSituacaoMatricula = "O aluno não renovou a matrícula no prazo estabelecido.",
                    DataReferencia = cicloProcesso.DataInicio,
                    SeqSolicitacaoServico = solicitacao.SeqSolicitacaoServico,
                    Jubilado = false,
                    CancelarBeneficio = false,
                    ObservacaoCancelamentoSolicitacao = "Solicitação cancelada devido à não efetivação da renovação de matrícula.",
                    TipoCancelamentoSGP = 2, // Cancelamento POR FALTA DE RENOVAÇÃO

                    SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                    SeqAlunoHistorico = solicitacao.SeqAlunoHistorico,
                    CodigoAlunoSGP = solicitacao.CodigoAlunoSGP,
                    NumeroProtocolo = solicitacao.NumeroProtocolo,
                    SeqCicloLetivoReferencia = dadosProcesso.SeqCicloLetivo
                };
                AlunoDomainService.CancelarMatricula(dadosCancelamento);


                // Task 33503:TSK - Alterar UC_SRC_002_01 - Central de Processos - Encerrar Processo Reabertura
                // Observação: No caso de cancelamento por encerramento do processo de reabertura, o aluno não receberá e-mail porque este não estará parametrizado no processo
                if (!processoReabertura)
                {
                    // Passo 5: Enviar notificação de acordo com a regra RN_MAT_117 - Notificação - Aluno cancelado ao encerrar
                    // processo renovação
                    // - 1.Enviar a notificação correspondente ao tipo ALUNO_CANCELADO_ENCERRAMENTO_PROCESSO_RENOVACAO associada
                    // à PRIMEIRA etapa, para a pessoa-atuação em questão.
                    // - 2.Substituir as TAGs, de acordo com as regras:
                    //   - 2.1.NOM_PESSOA: buscar o nome social do solicitante em questão, caso exista. Se não existir, buscar o nome do
                    // solicitante.
                    //   - 2.2.DSC_CICLO_LETIVO_PROCESSO: Buscar o ciclo letivo processo em questão.
                    // - 3.Associar, à solicitação de serviço da pessoa-atuação em questão, o sequencial da notificação que foi enviada.
                    string tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.ALUNO_CANCELADO_ENCERRAMENTO_PROCESSO_RENOVACAO;
                    Dictionary<string, string> dicTagsNot = new Dictionary<string, string>();
                    dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(solicitacao.NomeSocialSolicitante) ? solicitacao.NomeSolicitante : solicitacao.NomeSocialSolicitante);
                    dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.DSC_CICLO_LETIVO_PROCESSO, cicloProcesso.DescricaoCicloLetivo);

                    // Envia a notificação
                    var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                    {
                        SeqSolicitacaoServico = solicitacao.SeqSolicitacaoServico,
                        TokenNotificacao = tokenNotificacao,
                        DadosMerge = dicTagsNot,
                        EnvioSolicitante = true,
                        ConfiguracaoPrimeiraEtapa = true
                    };
                    SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                }
            }
        }

        public List<ProcessoEtapaSGFVO> BuscarEtapasSGFPorServico(long seqServico)
        {
            //Busca as etapas do SGF
            var servico = ServicoDomainService.SearchByKey(new SMCSeqSpecification<Servico>(seqServico));
            var etapasSgf = SGFHelper.BuscarEtapasSGFCache(servico.SeqTemplateProcessoSgf).OrderBy(a => a.Ordem).ToArray();

            List<ProcessoEtapaSGFVO> listaEtapasSGF = etapasSgf.Select(a => new ProcessoEtapaSGFVO()
            {
                Seq = a.Seq,
                Descricao = a.Descricao,
                Obrigatorio = a.Obrigatorio,
                AssociarEtapa = a.Obrigatorio ? true : false

            }).ToList();

            return listaEtapasSGF;
        }

        public bool VerificarValidadeTokenManutencaoProcesso(long seqProcesso)
        {
            var processo = this.SearchByKey(new SMCSeqSpecification<Processo>(seqProcesso), x => x.Servico);
            var habilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(processo.Servico.TokenPermissaoManutencaoProcesso);

            return habilitaBtnComPermissaoManutencaoProcesso;
        }


        public void ReabrirProcesso(long seqProcesso)
        {
            var spec = new SMCSeqSpecification<Processo>(seqProcesso);
            var dadosProcesso = this.SearchByKey(spec);

            var specProcessoEtapa = new ProcessoEtapaFilterSpecification { SeqProcesso = seqProcesso };
            var dadosProcessoEtapa = ProcessoEtapaDomainService.SearchBySpecification(specProcessoEtapa);

            dadosProcesso.DataEncerramento = null;
            if (dadosProcessoEtapa != null)
            {
                foreach (var item in dadosProcessoEtapa)
                {
                    item.DataEncerramento = null;
                    item.SituacaoEtapa = SituacaoEtapa.AguardandoLiberacao;

                    this.UpdateFields<ProcessoEtapa>(item, p => p.DataEncerramento, p => p.SituacaoEtapa);
                }

            }
            this.UpdateFields<Processo>(dadosProcesso, p => p.DataEncerramento);
        }
    }
}