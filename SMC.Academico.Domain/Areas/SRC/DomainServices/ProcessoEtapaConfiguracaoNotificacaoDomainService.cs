using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security.Util;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ProcessoEtapaConfiguracaoNotificacaoDomainService : AcademicoContextDomain<ProcessoEtapaConfiguracaoNotificacao>
    {
        #region DoaminService

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        private ProcessoDomainService ProcessoDomainService { get => Create<ProcessoDomainService>(); }

        private ProcessoUnidadeResponsavelDomainService ProcessoUnidadeResponsavelDomainService => Create<ProcessoUnidadeResponsavelDomainService>();

        private ServicoTipoNotificacaoDomainService ServicoTipoNotificacaoDomainService => Create<ServicoTipoNotificacaoDomainService>();

        #endregion DoaminService

        #region Propriedade

        private const string INCLUIR = "incluir";
        private const string ALTERAR = "alterar";

        #endregion Propriedade

        /// <summary>
        /// Buscar configurações de notificação de um processo organizados por etapas
        /// </summary>
        /// <param name="seqProcesso">Sequencial do Processo</param>
        /// <returns>Lista de configurações de notificação de um processo organizado por etapas</returns>
        public SMCPagerData<ProcessoEtapaConfiguracaoNotificacaoListarVO> BuscarConfiguracaoNotificacaoPorProcesso(ProcessoEtapaConfiguracaoNotificacaoFiltroVO filtro)
        {
            var includes = IncludesProcesso.Etapas | IncludesProcesso.Servico;
            var specProcesso = new ProcessoFilterSpecification() { Seq = filtro.SeqProcesso };
            var result = this.ProcessoDomainService.SearchBySpecification(specProcesso, out int total, includes).ToList();

            var retorno = result.TransformList<ProcessoEtapaConfiguracaoNotificacaoListarVO>();

            ///Buscar a descrição da notificação
            foreach (var item in retorno)
            {
                List<ServicoTipoNotificacao> notificacoesPorServico = new List<ServicoTipoNotificacao>();

                item.SeqProcesso = (long)specProcesso.Seq;
                item.ProcessoEncerrado = item.DataEncerramento.HasValue && item.DataEncerramento.Value < DateTime.Now;

                foreach (var etapa in item.Etapas)
                {
                    var servico = result.FirstOrDefault(a => a.Seq == item.SeqProcesso)?.Servico;

                    if (servico != null)
                    {
                        etapa.HabilitaBtnComPermissaoManutencaoProcesso = SMCSecurityHelper.Authorize(servico.TokenPermissaoManutencaoProcesso);
                        notificacoesPorServico = BuscarNotificacoesObrigatoriasPorEtapa(result.FirstOrDefault().Servico.Seq, etapa.SeqEtapaSgf);
                    }

                    var specNotificacao = new ProcessoEtapaConfiguracaoNotificacaoSpecification()
                    {
                        SeqProcessoEtapa = etapa.Seq,
                        SeqProcessoUnidadeResponsavel = filtro.SeqProcessoUnidadeResponsavel,
                        SeqTipoNotificacao = filtro.SeqTipoNotificacao,
                        PermiteAgendamento = filtro.PermiteAgendamento
                    };

                    if (filtro.SeqsGrupoEscalonamento != null && filtro.SeqsGrupoEscalonamento.Any(a => a != 0) && filtro.PermiteAgendamento.HasValue && filtro.PermiteAgendamento.Value)
                    {
                        specNotificacao.SeqsGrupoEscalonamento = filtro.SeqsGrupoEscalonamento;
                    }

                    var configuracoes = SearchBySpecification(specNotificacao,
                                                              IncludesProcessoEtapaConfiguracaoNotificacao.ParametrosEnvioNotificacao
                                                            | IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoUnidadeResponsavel
                                                            | IncludesProcessoEtapaConfiguracaoNotificacao.TipoNotificacao
                                                            | IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoUnidadeResponsavel_EntidadeResponsavel).ToList();

                    etapa.ConfiguracoesNotificacao = configuracoes.TransformList<ProcessoEtapaConfiguracaoNotificacaoVO>();
                    var seqsConfiguracoesTipos = configuracoes.Select(s => s.SeqConfiguracaoTipoNotificacao).Distinct().ToArray();
                    var seqsTiposNotificacoes = configuracoes.Select(s => s.SeqTipoNotificacao).Distinct().ToArray();
                    if (seqsConfiguracoesTipos.Length > 0)
                    {
                        var configuracoesComNotificacao = this.NotificacaoService.VerificarListaConfiguracaoPossuiNotificacoes(seqsConfiguracoesTipos);
                        var tiposNotificacao = this.NotificacaoService.BuscarTiposNotificacao(seqsTiposNotificacoes);
                        foreach (var configuracao in etapa.ConfiguracoesNotificacao)
                        {
                            //configuracao.PossuiRegistroEnvioNotificacao = this.NotificacaoService.VerificarConfiguracaoPossuiNotificacoes(new long[] { configuracao.SeqConfiguracaoTipoNotificacao });
                            //configuracao.DescricaoTipoNotificacao = NotificacaoService.BuscarTipoNotificacao(configuracao.TipoNotificacao.Seq)?.Descricao;
                            configuracao.PossuiRegistroEnvioNotificacao = configuracoesComNotificacao.Contains(configuracao.SeqConfiguracaoTipoNotificacao);
                            configuracao.DescricaoTipoNotificacao = tiposNotificacao.FirstOrDefault(f => f.Seq == configuracao.SeqTipoNotificacao)?.Descricao;
                            configuracao.TipoNotificacaoPermiteAgendamento = configuracao.TipoNotificacao.PermiteAgendamento;
                            configuracao.HabilitaBtnComPermissaoManutencaoProcesso = etapa.HabilitaBtnComPermissaoManutencaoProcesso;

                            if (notificacoesPorServico.Count > 0)
                            {
                                configuracao.NotificacaoObrigatoriaNaEtapa = notificacoesPorServico.Any(c => c.SeqTipoNotificacao == configuracao.SeqTipoNotificacao);
                            }
                        }
                    }
                    etapa.ConfiguracoesNotificacao.ForEach(a => a.SituacaoEtapa = etapa.SituacaoEtapa);
                    etapa.ConfiguracoesNotificacao = etapa.ConfiguracoesNotificacao.OrderBy(o => o.DescricaoTipoNotificacao).ThenBy(t => t.ProcessoUnidadeResponsavel.EntidadeResponsavel.Nome).ToList();
                }
            }

            return new SMCPagerData<ProcessoEtapaConfiguracaoNotificacaoListarVO>(retorno, total);
        }

        /// <summary>
        /// Buscar Configuração de notificação
        /// </summary>
        /// <param name="seq">Sequencial configuração notificação</param>
        /// <returns>Dados da configuração da notificação</returns>
        public ProcessoEtapaConfiguracaoNotificacaoVO BuscarConfiguracaoNotificacao(long seq)
        {
            var includes = IncludesProcessoEtapaConfiguracaoNotificacao.ParametrosEnvioNotificacao
                          | IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoEtapa_Processo
                          | IncludesProcessoEtapaConfiguracaoNotificacao.ProcessoUnidadeResponsavel
                          | IncludesProcessoEtapaConfiguracaoNotificacao.TipoNotificacao;

            var result = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapaConfiguracaoNotificacao>(seq), includes);

            ///Busca no serviço de notificação os dados da configurações de email
            var ConfiguracaoNotificacaoEmail = this.NotificacaoService.BuscarConfiguracaoNotificacaoEmail(result.SeqConfiguracaoTipoNotificacao);

            var retorno = result.Transform<ProcessoEtapaConfiguracaoNotificacaoVO>();

            retorno.ConfiguracaoNotificacao = ConfiguracaoNotificacaoEmail;
            retorno.SeqProcesso = result.ProcessoEtapa.SeqProcesso;
            retorno.SeqServico = result.ProcessoEtapa.Processo.SeqServico;
            retorno.SeqProcessoEtapa = result.SeqProcessoEtapa;
            retorno.ProcessoEncerrado = result.ProcessoEtapa.Processo.DataEncerramento.HasValue && result.ProcessoEtapa.Processo.DataEncerramento.Value < DateTime.Now;
            retorno.SituacaoEtapa = result.ProcessoEtapa.SituacaoEtapa;
            retorno.CamposReadyOnly = retorno.SituacaoEtapa == SituacaoEtapa.Encerrada || retorno.SituacaoEtapa == SituacaoEtapa.Liberada;

            return retorno;
        }

        /// <summary>
        /// Salvar processo etapa configuração de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Sequencial do processo etapa configuração notificação</returns>
        public long Salvar(ProcessoEtapaConfiguracaoNotificacaoVO modelo)
        {
            var modeloPublicacaoEtapaConfiguracao = modelo.Transform<ProcessoEtapaConfiguracaoNotificacao>();

            ConfiguracaoTipoNotificacaoData configuracaoTipoNotificacao = modelo.ConfiguracaoNotificacao.Transform<ConfiguracaoTipoNotificacaoData>();
            configuracaoTipoNotificacao.SeqUnidadeResponsavel = BuscarEntidadeResponsavelNotificacao(modelo.SeqProcessoUnidadeResponsavel);

            ///2.Verificar se no texto da mensagem da notificação existe alguma tag não cadastrada para o tipo de
            ///notificação informado. Caso exista, abortar a operação e emitir a mensagem de erro abaixo:
            Regex regex = new Regex(@"{{[_A-Za-z_]+}}");
            if (modelo != null && modelo.ConfiguracaoNotificacao != null && !string.IsNullOrEmpty(modelo.ConfiguracaoNotificacao.Mensagem))
            {
                var tagsMensagem = regex.Matches(modelo.ConfiguracaoNotificacao.Mensagem).Cast<Match>().Select(m => m.Value).ToList();

                string tagsInvalidas = string.Empty;

                if (modelo.ConfiguracaoNotificacao.Tags == null)
                    modelo.ConfiguracaoNotificacao.Tags = NotificacaoService.BuscarConfiguracaoTipoNotificacaoCompleta(modelo.SeqConfiguracaoTipoNotificacao).Tags;

                foreach (var item in tagsMensagem)
                {
                    if (modelo.ConfiguracaoNotificacao.Tags == null || !modelo.ConfiguracaoNotificacao.Tags.Any(a => a.Nome.ToLower() == item.ToLower()))
                        tagsInvalidas += $"<br />- {item}";
                }

                if (!string.IsNullOrEmpty(tagsInvalidas))
                    throw new ProcessoEtapaConfiguracaoNotificacaoTagException(tagsInvalidas, modelo.ConfiguracaoNotificacao.DescricaoTipoNotificacao);
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var seqNot = this.NotificacaoService.SalvarConfiguracaoTipoNotificacao(configuracaoTipoNotificacao);
                modeloPublicacaoEtapaConfiguracao.SeqConfiguracaoTipoNotificacao = seqNot;
                this.SaveEntity(modeloPublicacaoEtapaConfiguracao);
                unitOfWork.Commit();
            }

            return modeloPublicacaoEtapaConfiguracao.Seq;
        }

        public long? BuscarEntidadeResponsavelNotificacao(long seqProcessoUnidadeResponsavel)
        {
            var spec = new SMCSeqSpecification<ProcessoUnidadeResponsavel>(seqProcessoUnidadeResponsavel);
            return ProcessoUnidadeResponsavelDomainService.SearchProjectionByKey(spec, p => p.EntidadeResponsavel.SeqUnidadeResponsavelNotificacao);
        }

        /// <summary>
        /// Excluir a configuração notificação e todos os seus parametros
        /// </summary>
        /// <param name="seq">Sequencial da configuração notificação</param>
        public void Excluir(long seq)
        {
            var processoEtapaConfiguracao = this.BuscarConfiguracaoNotificacao(seq);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    var configToDelete = this.SearchByKey(new SMCSeqSpecification<ProcessoEtapaConfiguracaoNotificacao>(seq));
                    this.DeleteEntity(configToDelete);

                    NotificacaoService.ExcluirConfiguracaoTipoNotificacao(new long[] { processoEtapaConfiguracao.SeqConfiguracaoTipoNotificacao });

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        public List<ServicoTipoNotificacao> BuscarNotificacoesObrigatoriasPorEtapa(long? seqServico , long? seqEtapaSgf, long? seqTipoNotificacao = null)
        {
            var specTipoNotificalaoServico = new ServicoTipoNotificacaoFilterSpecification() { SeqServico = seqServico, SeqEtapaSgf = seqEtapaSgf, SeqTipoNotificacao = seqTipoNotificacao};
            var notificacoesServico = ServicoTipoNotificacaoDomainService.SearchBySpecification(specTipoNotificalaoServico).Where(c => c.Obrigatorio).ToList();

            return notificacoesServico;
        }

        public bool ExisteConfiguracaoNotificacao(List<long> excluidos, long seqServico)
        {
            var specProcesso = new ProcessoEtapaConfiguracaoNotificacaoSpecification { SeqsTipoNotificacao = excluidos.ToArray(), SeqServico = seqServico };
            var configuracoesNotificacao = this.SearchBySpecification(specProcesso);
           
            return configuracoesNotificacao.Any();
        }
    }
}