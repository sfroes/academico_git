using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Academico.Domain.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoServicoEtapaDomainService : AcademicoContextDomain<SolicitacaoServicoEtapa>
    {
        #region [ DomainServices ]

        private ProcessoEtapaDomainService ProcessoEtapaDomainService => this.Create<ProcessoEtapaDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => this.Create<SolicitacaoServicoDomainService>();

        private IngressanteDomainService IngressanteDomainService => this.Create<IngressanteDomainService>();

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => this.Create<AlunoHistoricoSituacaoDomainService>();

        #endregion [ DomainServices ]

        #region [Services]

        private IEtapaService EtapaService => this.Create<IEtapaService>(); 

        #endregion [Services]

        public SolicitacaoServicoEtapa ObterUltimaEtapaIniciadaMatricula(long seqSolicitacaoServico)
        {
            // Busca se existe alguma etapa para esta solicitação de serviço
            var spec = new SolicitacaoServicoEtapaFilterSpecification { SeqSolicitacaoServico = seqSolicitacaoServico };
            spec.SetOrderBy(e => e.DataInclusao);

            var solicitacaoServicoEtapas = this.SearchProjectionBySpecification(spec, x => new
            {
                Etapa = x,
                HistoricosNavegacao = x.HistoricosNavegacao,
                HistoricosSituacao = x.HistoricosSituacao.Where(h => !h.DataExclusao.HasValue).ToList(),
                SeqEtapaSGF = x.ConfiguracaoEtapa.ProcessoEtapa.SeqEtapaSgf,
                SeqConfiguracaoEtapa = x.SeqConfiguracaoEtapa
            });

            if (solicitacaoServicoEtapas != null && solicitacaoServicoEtapas.Count() >= 0)
            {
                // Pelo fato de ter sido feito uma projeção, não tem como dar includes automaticamente.
                // Desta maneira, faço a busca dos dados que preciso separadamente depois atribui ao objeto
                // ref: https://stackoverflow.com/questions/7917348/include-with-projection-does-not-work

                var dadosRet = solicitacaoServicoEtapas.OrderBy(s => s.Etapa.DataInclusao).LastOrDefault();
                var etapaRet = dadosRet.Etapa;
                etapaRet.HistoricosNavegacao = dadosRet.HistoricosNavegacao;
                etapaRet.HistoricosSituacao = dadosRet.HistoricosSituacao;
                return etapaRet;
            }
            else
            {
                // Não existe etapa criada.
                // Busca no SGF qual é a etapa inicial do processo e cria a mesma

                // Busca o sequencial do template de processos do SGF
                var dadosSolicitacaoServico = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico),
                    x => new
                    {
                        SeqTemplateProcessoSGF = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqPessoaAtuacao = x.SeqPessoaAtuacao
                    });

                // Busca as configurações das etapas da solicitação
                var configuracoesEtapasIngressante = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x =>
                    x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(c => new
                    {
                        SeqConfiguracaoEtapa = c.Seq,
                        SeqEtapaSGF = c.ProcessoEtapa.SeqEtapaSgf
                    })
                );

                // Busca as etapas de um template de processo
                var etapasSGF = SGFHelper.BuscarEtapasSGFCache(dadosSolicitacaoServico.SeqTemplateProcessoSGF);

                // Busca qual é a etapa configurada para ser a inicial da etapa
                var etapaInicial = etapasSGF.OrderBy(e => e.Ordem).FirstOrDefault();

                // Recupera da lista de configurações da etapa qual a configuração da etapa em questão
                var seqConfiguracaoEtapa = configuracoesEtapasIngressante.FirstOrDefault(c => c.SeqEtapaSGF == etapaInicial.Seq).SeqConfiguracaoEtapa;

                // Busca a configuração da etapa associada à solicitação
                // Cria a etapa inicial
                var solicitacaoServicoEtapa = new SolicitacaoServicoEtapa
                {
                    SeqConfiguracaoEtapa = seqConfiguracaoEtapa,
                    SeqSolicitacaoServico = seqSolicitacaoServico
                };

                this.SaveEntity(solicitacaoServicoEtapa);
                return solicitacaoServicoEtapa;
            }
        }

        public void VerificarAcessoEtapa(long seqSolicitacaoServico, long seqConfiguracaoEtapa, bool ignoreFinalizedValidation = false)
        {
            IEnumerable<EtapaProjecaoVO> etapas = null;

            // Busca as etapas da solicitação de matricula
            var possuiEscalonamento = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento != null);
            if (possuiEscalonamento)
            {
                etapas = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.GrupoEscalonamento.Itens.Select(y =>
                    new EtapaProjecaoVO
                    {
                        SeqEtapaSGF = y.Escalonamento.ProcessoEtapa.SeqEtapaSgf,
                        HistoricosSituacao = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq).HistoricosSituacao.OrderByDescending(h => h.Seq).Where(h => !h.DataExclusao.HasValue),
                        SeqTemplateProcessoSgf = y.Escalonamento.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq,
                        UltimaSituacaoEtapaSGF = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq).SituacaoAtual,
                        DataInicio = y.Escalonamento.DataInicio,
                        DataFim = y.Escalonamento.DataFim,
                        SituacaoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.Escalonamento.SeqProcessoEtapa).Seq).ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                        UltimaSituacaoIngressante = (x.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante,
                        SituacoesPermitidasServicoIngressante = x.ConfiguracaoProcesso.Processo.Servico.SituacoesIngressante,
                        SituacoesPermitidasServicoAluno = x.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno,
                        TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                        SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                        SeqProcessoEtapa = y.Escalonamento.SeqProcessoEtapa,
                        SeqCicloLetivoProcesso = y.Escalonamento.ProcessoEtapa.Processo.SeqCicloLetivo
                    })
                );
            }
            else
            {
                etapas = SolicitacaoServicoDomainService.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico), x => x.ConfiguracaoProcesso.ConfiguracoesEtapa.Select(y =>
                    new EtapaProjecaoVO
                    {
                        SeqEtapaSGF = y.ProcessoEtapa.SeqEtapaSgf,
                        HistoricosSituacao = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq).HistoricosSituacao.OrderByDescending(h => h.Seq).Where(h => !h.DataExclusao.HasValue),
                        SeqTemplateProcessoSgf = y.ProcessoEtapa.Processo.Servico.SeqTemplateProcessoSgf,
                        SeqConfiguracaoEtapa = x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq,
                        UltimaSituacaoEtapaSGF = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq).SituacaoAtual,
                        DataInicio = y.ProcessoEtapa.DataInicio ?? default(DateTime),
                        DataFim = y.ProcessoEtapa.DataFim,
                        SituacaoEtapa = x.Etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == x.ConfiguracaoProcesso.ConfiguracoesEtapa.FirstOrDefault(c => c.SeqProcessoEtapa == y.SeqProcessoEtapa).Seq).ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                        UltimaSituacaoIngressante = (x.PessoaAtuacao as Ingressante).HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoIngressante,
                        SituacoesPermitidasServicoIngressante = x.ConfiguracaoProcesso.Processo.Servico.SituacoesIngressante,
                        SituacoesPermitidasServicoAluno = x.ConfiguracaoProcesso.Processo.Servico.SituacoesAluno,
                        TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                        SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                        SeqProcessoEtapa = y.SeqProcessoEtapa,
                        SeqCicloLetivoProcesso = y.ProcessoEtapa.Processo.SeqCicloLetivo
                    })
                );
            }

            var seqTemplateProcessoSGF = etapas.FirstOrDefault()?.SeqTemplateProcessoSgf;
            EtapaSimplificadaData[] etapasSGF = SGFHelper.BuscarEtapasSGFCache(seqTemplateProcessoSGF.Value);

            var etapaAtual = etapas.FirstOrDefault(e => e.SeqConfiguracaoEtapa == seqConfiguracaoEtapa);
            var etapaAtualSGF = etapasSGF.FirstOrDefault(e => e.Seq == etapaAtual.SeqEtapaSGF);

            var situacaoInicialEtapaAtual = etapaAtualSGF.Situacoes.FirstOrDefault(s => s.SituacaoInicialEtapa);
            var situacoesFinaisProcesso = etapaAtualSGF.Situacoes.Where(s => s.SituacaoFinalProcesso);
            var seqsSituacoesFinaisProcesso = situacoesFinaisProcesso.Select(s => s.Seq);

            var situacoesFinaisEtapa = etapaAtualSGF.Situacoes.Where(s => s.SituacaoFinalEtapa);
            var seqsSituacoesFinaisEtapa = situacoesFinaisEtapa.Select(s => s.Seq);

            if (situacaoInicialEtapaAtual == null)
                throw new SMCApplicationException("Não foi possível identificar a configuração da situação inicial da etapa corrente desta solicitação.");

            if (situacoesFinaisEtapa == null || !situacoesFinaisEtapa.Any())
                throw new SMCApplicationException("Não foi possível identificar as configurações de situações finais da etapa corrente desta solicitação.");

            /*Situação da solicitação de matrícula:
            1. Não permitir acessar a etapa antes de ter finalizado a anterior através da regra:
            * Verificar se existe no histórico de situações (da solicitação em questão), da etapa que está sendo acessada, a situação configurada para ser a inicial da etapa. Desconsiderar situações que estão com data de exclusão.

            - Caso não exista, impedir o acesso à página e exibir mensagem:
            "Não é possível acessar, pois é necessário finalizar a etapa anterior."
            */
            if (!(etapaAtual.HistoricosSituacao?.Any(h => h.SeqSituacaoEtapaSgf == situacaoInicialEtapaAtual.Seq) ?? false))
                throw new SolicitacaoServicoComEtapaAnteriorNaoFinalizadaException();

            /*
            2. Não permitir acessar a etapa se a situação atual é final de processo, através da regra:
            *Verificar se a situação atual (da solicitação de matrícula em questão) foi configurada para ser a situação final  do processo.
            - Caso seja, impedir acesso à etapa e exibir mensagem:
            “Não é possível acessar, pois a solicitação de matrícula está na situação: <situação da solicitação de serviço de matrícula>.”

            Ao acionar o comando "Ok" da mensagem, direcionar para o home da etapa do processo.*/
            if (!ignoreFinalizedValidation && etapas.Any(e => seqsSituacoesFinaisProcesso.Contains(e.UltimaSituacaoEtapaSGF?.SeqSituacaoEtapaSgf ?? 0)))
                throw new SolicitacaoServicoEmSituacaoNaoPermiteAcessoException(situacoesFinaisProcesso.FirstOrDefault(s => s.Seq == etapaAtual.UltimaSituacaoEtapaSGF?.SeqSituacaoEtapaSgf).Descricao);

            /*Verificar se a última situação da etapa em questão é a situação configurada para ser final da etapa.
             Caso ocorra, impedir acesso à funcionalidade e exibir mensagem: "Acesso não permitido. Essa etapa já foi finalizada." Ao acionar o comando "Ok" da mensagem, direcionar para o home da etapa do processo
            Ao acionar o comando "Ok" da mensagem, direcionar para o home da etapa do processo.*/
            if (!ignoreFinalizedValidation && etapas.Any(e => seqsSituacoesFinaisEtapa.Contains(e.UltimaSituacaoEtapaSGF?.SeqSituacaoEtapaSgf ?? 0)))
                throw new SolicitacaoServicoEmSituacaoNaoPermiteAcessoException(situacoesFinaisEtapa.FirstOrDefault(s => s.Seq == etapaAtual.UltimaSituacaoEtapaSGF?.SeqSituacaoEtapaSgf).Descricao);

            /*
            3. Direcionar para a última página da etapa se já tiver finalizado a mesma
            * Verificar se existe no histórico de situações, a situação (da solicitação de matrícula em questão) que é a situação final da etapa com a classificação de situação "Finalizada com sucesso", configurada para a etapa que está sendo acessada. Desconsiderar situações que estejam marcadas com data de exclusão.
            - Caso seja, exibir a última página que foi configurada para a etapa do processo de matrícula por ciclo letivo da pessoa-atuação em questão.
            */
            // Isso é feito no método EntrarEtapa. ele verifica qual a situação da etapa e manda pra última página caso seja essa regra acima.

            /*
            Vigência da etapa:
            Verificar se o escalonamento da pessoa-atuação está vigente para a etapa do processo de matrícula em questão ou se a pessoa-atuação possui um ticket vigente nesse escalonamento. Caso não estiver, impedir acesso à página e exibir mensagem:
            “Não é possível acessar, pois a etapa está fora do período de vigência.”
            */
            bool estaFinalizadoComSucesso = etapaAtualSGF.Situacoes.Where(x => x.Seq == etapaAtual.UltimaSituacaoEtapaSGF.SeqSituacaoEtapaSgf).FirstOrDefault().SituacaoFinalEtapa && etapaAtualSGF.Situacoes.Where(x => x.Seq == etapaAtual.UltimaSituacaoEtapaSGF.SeqSituacaoEtapaSgf).FirstOrDefault().ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso;
            if ((etapaAtual.DataInicio > DateTime.Now || etapaAtual.DataFim < DateTime.Now) && !estaFinalizadoComSucesso)
                if (!ProcessoEtapaDomainService.ValidarDataEscalonamentoFinalProcesso(etapaAtual.SeqProcessoEtapa))
                    throw new SolicitacaoServicoComEtapaForaPeriodoVigenciaException();

            /*
            Situação da etapa:
            Verificar se a etapa do processo da pessoa-atuação em questão está "Liberada". Caso não estiver, impedir o acesso e exibir a seguinte mensagem:
            “Não é possível acessar, pois esta etapa se encontra na situação <situação da etapa>.”

            Ao acionar o comando "Ok" da mensagem, direcionar para o home do processo.
            */
            if (etapaAtual.SituacaoEtapa != SituacaoEtapa.Liberada)
                throw new SolicitacaoServicoComEtapaNãoLiberadaException(SMCEnumHelper.GetDescription(etapaAtual.SituacaoEtapa));

            /*
            Situação da pessoa-atuação:
            Verificar se a situação da pessoa-atuação em questão é uma das situações que foram parametrizadas para acessar o serviço do processo 
            em questão.
            Caso não seja, abortar a operação e exibir a seguinte mensagem:
            "Não é possível acessar, quando o <tipo de atuação em questão> se encontra na situação <situação da pessoa-atuação em questão>."
            */

            // Se a aplicação que está fazendo acesso à página for SGA.Aluno => Verificar a permissão de ABERTURA de solicitação
            if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO)
            {
                if (etapaAtual.TipoAtuacao == TipoAtuacao.Ingressante && (!etapaAtual.SituacoesPermitidasServicoIngressante.Any(s => s.SituacaoIngressante == etapaAtual.UltimaSituacaoIngressante && s.PermissaoServico == PermissaoServico.CriarSolicitacao)))
                    throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(SMCEnumHelper.GetDescription(etapaAtual.TipoAtuacao), SMCEnumHelper.GetDescription(etapaAtual.UltimaSituacaoIngressante), "criar esse tipo de solicitação");
                else if (etapaAtual.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // Verifica a situação do aluno no ciclo letivo do processo caso esteja preenchido. Senão olha a situação atual.

                    // Busca a situação do aluno no ciclo atual ou no ciclo do processo
                    long seqCicloReferencia = AlunoDomainService.BuscarCicloLetivoAtual(etapaAtual.SeqPessoaAtuacao, true);
                    if (etapaAtual.SeqCicloLetivoProcesso.HasValue && etapaAtual.SeqCicloLetivoProcesso.Value != seqCicloReferencia)
                        seqCicloReferencia = etapaAtual.SeqCicloLetivoProcesso.Value;
                    var situacaoReferencia = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(etapaAtual.SeqPessoaAtuacao, seqCicloReferencia, true);

                    // Verifica se o aluno está na situação que permite abertura de solicitação
                    if (!etapaAtual.SituacoesPermitidasServicoAluno.Any(s => s.SeqSituacaoMatricula == situacaoReferencia.SeqSituacao && s.PermissaoServico == PermissaoServico.CriarSolicitacao))
                    {
                        throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(SMCEnumHelper.GetDescription(etapaAtual.TipoAtuacao), situacaoReferencia.Descricao, "criar esse tipo de solicitação");
                    }
                }
            }
            // Se a aplicação que está fazendo acesso à página for SGA.Administrativo ou SGA.Professor => Verificar a permissão de ATENDIMENTO de solicitação
            else if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO || SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_PROFESSOR)
            {
                if (etapaAtual.TipoAtuacao == TipoAtuacao.Ingressante && (!etapaAtual.SituacoesPermitidasServicoIngressante.Any(s => s.SituacaoIngressante == etapaAtual.UltimaSituacaoIngressante && s.PermissaoServico == PermissaoServico.AtenderSolicitacao)))
                    throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(SMCEnumHelper.GetDescription(etapaAtual.TipoAtuacao), SMCEnumHelper.GetDescription(etapaAtual.UltimaSituacaoIngressante), "realizar o atendimento");
                else if (etapaAtual.TipoAtuacao == TipoAtuacao.Aluno)
                {
                    // Verifica a situação do aluno no ciclo letivo do processo caso esteja preenchido. Senão olha a situação atual.

                    // Busca a situação do aluno no ciclo atual ou no ciclo do processo
                    long seqCicloReferencia = AlunoDomainService.BuscarCicloLetivoAtual(etapaAtual.SeqPessoaAtuacao, true);
                    if (etapaAtual.SeqCicloLetivoProcesso.HasValue && etapaAtual.SeqCicloLetivoProcesso.Value != seqCicloReferencia)
                        seqCicloReferencia = etapaAtual.SeqCicloLetivoProcesso.Value;
                    var situacaoReferencia = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(etapaAtual.SeqPessoaAtuacao, seqCicloReferencia, true);

                    // Verifica se o aluno está na situação que permite abertura de solicitação
                    if (!etapaAtual.SituacoesPermitidasServicoAluno.Any(s => s.SeqSituacaoMatricula == situacaoReferencia.SeqSituacao && s.PermissaoServico == PermissaoServico.AtenderSolicitacao))
                    {
                        throw new SolicitacaoServicoInativaParaSolicitanteSituacaoException(SMCEnumHelper.GetDescription(etapaAtual.TipoAtuacao), situacaoReferencia.Descricao, "realizar o atendimento");
                    }
                }
            }
        }       
    }
}