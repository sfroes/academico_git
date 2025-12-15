using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class ParametroEnvioNotificacaoDomainService : AcademicoContextDomain<ParametroEnvioNotificacao>
    {
        #region DoaminService
        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        private ProcessoEtapaConfiguracaoNotificacaoDomainService ProcessoEtapaConfiguracaoNotificacaoDomainService { get => Create<ProcessoEtapaConfiguracaoNotificacaoDomainService>(); }

        private ServicoTipoNotificacaoDomainService ServicoTipoNotificacaoDomainService => Create<ServicoTipoNotificacaoDomainService>();

        #endregion DoaminService

        /// <summary>
        /// Buscar todos os parametros de envio da notificacao para processo etapa configuração notificação
        /// </summary>
        /// <param name="seqProcessoEtapaConfiguracaoNotificacao">Sequencial processo etapa configuração notificação</param>
        /// <returns>Lista de parametros envio notificação</returns>
        public ParametroEnvioNotificacaoVO BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(long seqProcessoEtapaConfiguracaoNotificacao)
        {
            var spec = new ParametroEnvioNotificacaoFilterSpecification() { SeqProcessoEtapaConfiguracaoNotificacao = seqProcessoEtapaConfiguracaoNotificacao };

            var result = this.SearchBySpecification(spec).ToList();

            var dadosComplementares = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<ProcessoEtapaConfiguracaoNotificacao>(seqProcessoEtapaConfiguracaoNotificacao),
                                                                                                            p => new
                                                                                                            {
                                                                                                                p.ProcessoEtapa.SeqProcesso,
                                                                                                                p.ProcessoEtapa.Processo.DataEncerramento,
                                                                                                                p.SeqProcessoEtapa,
                                                                                                                p.ProcessoEtapa.SituacaoEtapa,
                                                                                                                SeqTipoNotificacao = p.TipoNotificacao.Seq,
                                                                                                                EntidadeResponsavel = p.ProcessoUnidadeResponsavel.EntidadeResponsavel.NomeReduzido,
                                                                                                                p.ProcessoEtapa.Processo.Servico.TipoServico.ExigeEscalonamento,
                                                                                                                SeqEtapaSgf = p.ProcessoEtapa.Processo.Etapas.FirstOrDefault(c => c.SeqProcesso == p.ProcessoEtapa.SeqProcesso).SeqEtapaSgf,
                                                                                                                SeqServico = p.ProcessoEtapa.Processo.Servico.Seq
                                                                                                            });

            var dados = new ParametroEnvioNotificacaoVO()
            {
                SeqProcesso = dadosComplementares.SeqProcesso,
                SeqProcessoEtapaConfiguracaoNotificacao = seqProcessoEtapaConfiguracaoNotificacao,
                ProcessoEncerrado = dadosComplementares.DataEncerramento.HasValue && dadosComplementares.DataEncerramento.Value < DateTime.Now,
                BotoesDesabilitados = dadosComplementares.SituacaoEtapa == SituacaoEtapa.Encerrada || dadosComplementares.SituacaoEtapa == SituacaoEtapa.Liberada,
                SeqProcessoEtapa = dadosComplementares.SeqProcessoEtapa,
                SeqTipoNotificacao = dadosComplementares.SeqTipoNotificacao,
                TipoNotificacao = this.NotificacaoService.BuscarTipoNotificacao(dadosComplementares.SeqTipoNotificacao).Descricao,
                EntidadeResponsavel = dadosComplementares.EntidadeResponsavel,
                ExigeEscalonamento = dadosComplementares.ExigeEscalonamento,
                SeqServico = dadosComplementares.SeqServico,
                SeqEtapaSgf = dadosComplementares.SeqEtapaSgf,
                ParametroEnvioNotificacaoItem = result.TransformList<ParametroEnvioNotificacaoItemVO>(),
            };

            if (dados.ParametroEnvioNotificacaoItem != null)
            {
                dados.ParametroEnvioNotificacaoItem = dados.ParametroEnvioNotificacaoItem.OrderBy(p => p.SeqGrupoEscalonamento).ToList();
                foreach (var parametro in dados.ParametroEnvioNotificacaoItem)
                    parametro.PossuiNotificacaoEnviada = SolicitacaoServicoEnvioNotificacaoDomainService.Count(new SolicitacaoServicoEnvioNotificacaoFilterSpecification { SeqParametroEnvioNotificacao = parametro.Seq }) > 0;
            }

            return dados;
        }

        /// <summary>
        /// Salvar os parametros de notificação
        /// </summary>
        /// <param name="modelo">Dados a serem salvos</param>
        /// <returns>Modelo de parametros</returns>
        public ParametroEnvioNotificacaoVO SalvarParametros(ParametroEnvioNotificacaoVO modelo)
        {
            long seqProcessoEtapaConfiguracaoNotificacao;

            if (modelo.ParametroEnvioNotificacaoItem.FirstOrDefault() != null && modelo.ParametroEnvioNotificacaoItem.FirstOrDefault().SeqProcessoEtapaConfiguracaoNotificacao != 0)
                seqProcessoEtapaConfiguracaoNotificacao = modelo.ParametroEnvioNotificacaoItem.FirstOrDefault().SeqProcessoEtapaConfiguracaoNotificacao;
            else
                seqProcessoEtapaConfiguracaoNotificacao = modelo.SeqProcessoEtapaConfiguracaoNotificacao;

            var processoEtapaConfiguracao = this.ProcessoEtapaConfiguracaoNotificacaoDomainService.BuscarConfiguracaoNotificacao(seqProcessoEtapaConfiguracaoNotificacao).Transform<ProcessoEtapaConfiguracaoNotificacao>();

            if (processoEtapaConfiguracao.ProcessoEtapa.SituacaoEtapa == SituacaoEtapa.Liberada)
            {
                throw new ParametroEnvioNotificacaoLiberadaException();
            }

            var parametrosBD = processoEtapaConfiguracao.ParametrosEnvioNotificacao;
            var parametrosModelo = modelo.ParametroEnvioNotificacaoItem.TransformList<ParametroEnvioNotificacao>();

            /// Atualização do modelo a ser salvo com todos os novos parametros
            processoEtapaConfiguracao.ParametrosEnvioNotificacao = modelo.ParametroEnvioNotificacaoItem.TransformList<ParametroEnvioNotificacao>();

            /// 2.Ao alterar ou excluir um parâmetro de envio de notificação, verificar se já existem registros de envio
            /// notificação com o parâmetro a ser alterado/excluído.Em caso afirmativo emitir a mensagem de erro
            /// abaixo e abortar a operação.
            var alterados = ObterSequenciaisParametrosAlterados(parametrosModelo, parametrosBD);
            if (alterados.Any())
            {
                if (SolicitacaoServicoEnvioNotificacaoDomainService.Count(new SolicitacaoServicoEnvioNotificacaoFilterSpecification { SeqsParametrosEnvioNotificacao = alterados }) > 0)
                    throw new ParametroEnvioNotificacaoComNotificacaoJaEnviadaException();
            }

            /// 3. Verificar se o tipo de notificação está parametrizado como obrigatório na etapa, para o serviço em questão. 
            /// Caso seja, verificar se existe pelo menos um parâmetro de envio ativo para cada grupo existente no processo. 
            /// Se não existir, abortar a operação e exibir a seguinte mensagem impeditiva
            var tiposNotificacoesObrigatorias = BuscarNotificacoesObrigatoriasPorEtapa(modelo.SeqServico, modelo.SeqEtapaSgf, processoEtapaConfiguracao.SeqTipoNotificacao);
            if (tiposNotificacoesObrigatorias.Count > 0)
            {
                // Evitar excluir ou alterar para inativo quando é obrigatório
                if (!modelo.ParametroEnvioNotificacaoItem.Any(c => c.Ativo))
                {
                    throw new ProcessoEtavaConfiguracaoNotificacaoObrigatorioParaEsteTipoNotificacaoException();
                }

                var spec = new SMCSeqSpecification<Processo>(modelo.SeqProcesso);
                var processo = ProcessoDomainService.SearchByKey(spec, IncludesProcesso.Etapas | IncludesProcesso.Etapas_ConfiguracoesNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_TipoNotificacao | IncludesProcesso.Etapas_ConfiguracoesNotificacao_ParametrosEnvioNotificacao | IncludesProcesso.GruposEscalonamento | IncludesProcesso.Servico_TiposNotificacao);

                foreach (var grupoEscalonamento in processo.GruposEscalonamento)
                {
                    var notificacoesNoModelo = modelo.ParametroEnvioNotificacaoItem.Where(c => c.SeqGrupoEscalonamento == grupoEscalonamento.Seq).ToList();
                    if (notificacoesNoModelo.Count == 0 || notificacoesNoModelo.Count(d => d.Ativo == true) == 0)
                    {
                        throw new ProcessoEtavaConfiguracaoNotificacaoObrigatorioParaEsteTipoNotificacaoException();
                    }
                }
            }

            this.ProcessoEtapaConfiguracaoNotificacaoDomainService.SaveEntity(processoEtapaConfiguracao);

            var result = BuscarParametroEnvioNotificacaoPorConfiguracaoNotificacao(processoEtapaConfiguracao.Seq);

            return result;
        }

        /// <summary>
        /// Função de comparação se algum item de parametros foi alterado
        /// </summary>
        /// <param name="parametrosModelo">Parametros que vieram no modelo</param>
        /// <param name="parametrosBD">Parametros do banco de dados</param>
        /// <returns>Boleano caso exista alguma alteração</returns>
        private List<long> ObterSequenciaisParametrosAlterados(IList<ParametroEnvioNotificacao> parametrosModelo, IList<ParametroEnvioNotificacao> parametrosBD)
        {
            List<long> seqsRet = new List<long>();

            if (parametrosModelo.Count != parametrosBD.Count)
                return seqsRet;

            for (int i = 0; i < parametrosModelo.Count; i++)
            {
                if (parametrosModelo[i].AtributoAgendamento != parametrosBD[i].AtributoAgendamento ||
                    parametrosModelo[i].QuantidadeDiasInicioEnvio != parametrosBD[i].QuantidadeDiasInicioEnvio ||
                    parametrosModelo[i].QuantidadeDiasRecorrencia != parametrosBD[i].QuantidadeDiasRecorrencia ||
                    parametrosModelo[i].ReenviarNotificacao != parametrosBD[i].ReenviarNotificacao ||
                    parametrosModelo[i].Temporalidade != parametrosBD[i].Temporalidade)
                    seqsRet.Add(parametrosBD[i].Seq);
            }

            return seqsRet;
        }

        public List<ParametroEnvioNotificacao> BuscarParametroEnvioNotificacaoPorGrupoEscalonamento(long seqGrupoEscalonamento)
        {
            ParametroEnvioNotificacaoFilterSpecification spec = new ParametroEnvioNotificacaoFilterSpecification();
            spec.SeqGrupoEscalonamento = seqGrupoEscalonamento;
            return SearchBySpecification(spec).ToList();
        }

        public List<ServicoTipoNotificacao> BuscarNotificacoesObrigatoriasPorEtapa(long? seqServico, long? seqEtapaSgf, long? seqTipoNotificacao = null)
        {
            var specTipoNotificalaoServico = new ServicoTipoNotificacaoFilterSpecification() { SeqServico = seqServico, SeqEtapaSgf = seqEtapaSgf, SeqTipoNotificacao = seqTipoNotificacao };
            var notificacoesServico = ServicoTipoNotificacaoDomainService.SearchBySpecification(specTipoNotificalaoServico).Where(c => c.Obrigatorio).ToList();

            return notificacoesServico;
        }
    }
}