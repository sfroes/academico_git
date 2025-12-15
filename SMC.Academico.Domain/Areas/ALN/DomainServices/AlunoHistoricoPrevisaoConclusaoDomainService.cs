using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoHistoricoPrevisaoConclusaoDomainService : AcademicoContextDomain<AlunoHistoricoPrevisaoConclusao>
    {
        #region [DomainServices]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();

        #endregion [DomainServices]

        #region [Servicos outros Dominios]

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();

        #endregion [Servicos outros Dominios]

        /// <summary>
        /// Realiza o deferimento de uma solicitação de prorrogação de prazo de conclusão. (RN_SRC_046)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço de prorrogação</param>
        /// <returns>Dicionário para envio de notificação</returns>
        public Dictionary<string, string> DeferirProrrogacaoPrazoConclusao(long seqSolicitacaoServico)
        {
            // Busca informações da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dadosSolicitacao = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                PrevisaoConclusaoAtual = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).PrevisoesConclusao.OrderByDescending(p => p.Seq).FirstOrDefault(),
                NovaDataPrevisaoString = x.Formularios.FirstOrDefault().DadosCampos.FirstOrDefault(d => d.TokenElemento == TOKEN_ELEMENTO_FORMULARIO.NOVA_DATA_PREVISAO_CONCLUSAO).Valor,
                SeqCursoOfertaLocalidadeTurno = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqCursoOfertaLocalidadeTurno,
                CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                ValorPercentualServicoAdicional = x.ConfiguracaoProcesso.Processo.ValorPercentualServicoAdicional,
            });

            // ITEM 1
            // Verifica se o aluno já possui data de deposito cadastrada para o trabalho que gera cobrança financeira
            var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(dadosSolicitacao.SeqPessoaAtuacao);
            if (trabalho.DataDeposito.HasValue && trabalho.Situacao != Common.Areas.APR.Enums.SituacaoHistoricoEscolar.Reprovado)
                throw new DeferirProrrogacaoPrazoConclusaoComDataDepositoException(trabalho.TituloTrabalho, trabalho.DataDeposito.Value);

            // Busca a nova data de conclusão do formulário da solicitação
            if (string.IsNullOrEmpty(dadosSolicitacao.NovaDataPrevisaoString))
                throw new NovaDataPrevisaoNaoInformadaException();
            DateTime NovaDataPrevisaoConclusao = DateTime.Parse(dadosSolicitacao.NovaDataPrevisaoString);

            // Prepara o dicionário para envio de notificação
            Dictionary<string, string> dicTagsNot = new Dictionary<string, string>();
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.DAT_PRORROGACAO, dadosSolicitacao.NovaDataPrevisaoString);

            // ITEM 2
            // Se o dia da nova data for menor que o último dia do mês informado, abortar a operação
            // e exibir a mensagem de erro: "O dia da nova data de previsão de conclusão deverá ser o
            // último dia do mês”
            if (NovaDataPrevisaoConclusao.AddDays(1).Month == NovaDataPrevisaoConclusao.Month)
                throw new NovaDataPrevisaoAnteriorNaoUltimoDiaMesException();

            // Se a nova data for menor que a data atual do sistema, abortar a operação e exibir a
            // mensagem de erro: “A nova data de previsão de conclusão deverá ser maior que a data atual”
            if (NovaDataPrevisaoConclusao < DateTime.Now)
                throw new NovaDataPrevisaoAnteriorDataAtualException();

            // Se a nova data for menor ou igual à atual data de previsão de conclusão, abortar a
            // operação e exibir a mensagem de erro: “A nova data de previsão de conclusão deverá ser
            // maior que a data atual de previsão de conclusão”
            if (NovaDataPrevisaoConclusao <= dadosSolicitacao.PrevisaoConclusaoAtual.DataPrevisaoConclusao)
                throw new NovaDataPrevisaoAnteriorPrevisaoAtualException();

            //  Se a nova data for menor que a maior data da autorização de novo depósito do trabalho(caso exista)
            //  abortar a operação e exibir a mensagem de erro:
            //  A nova data de previsão de conclusão deverá ser maior que a data da autorização de novo depósito do trabalho.
            var specTrabalho = new TrabalhoAcademicoFilterSpecification()
            {
                SeqAluno = dadosSolicitacao.SeqPessoaAtuacao,
                PossuiDataDeposito = true
            };
            var dataSegundoDeposito = TrabalhoAcademicoDomainService.SearchProjectionByKey(specTrabalho, x =>  x.DataAutorizacaoSegundoDeposito);
            
            if (NovaDataPrevisaoConclusao < dataSegundoDeposito)
                throw new DataPrevisaoMenorAutorizacaoException();

            var specHistorico = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = dadosSolicitacao.SeqPessoaAtuacao,
                TokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO,
                Excluido = false
            };
            var alunoHistoricos = AlunoHistoricoSituacaoDomainService.SearchBySpecification(specHistorico).ToList();

            // Se a nova data for menor que a maior data início da situação de matrícula(independente de ciclo letivo), 
            // cujo token é PROVAVEL_FORMANDO(desconsiderar situações com data de exclusão), abortar a operação e exibir a mensagem de erro:
            // A nova data de previsão de conclusão deverá ser maior que a data de início da situação de matrícula Matriculado ou Provável formando.
            if (!alunoHistoricos.SMCIsNullOrEmpty())
            {
                var ultimaDataMatricula = alunoHistoricos.Select(x => x.DataInicioSituacao).OrderByDescending(data => data).FirstOrDefault();

                if (NovaDataPrevisaoConclusao < ultimaDataMatricula)
                    throw new DataPrevisaoMenorInicioSituacaoException();
            }

            // ITEM 3
            // Salvar os dados da solicitação no campo "Descrição da solicitação atualizada" em formato html
            // conforme a regra RN_SRC_060 - Solicitação - Formatação prorrogação
            string descricaoAtualizada = string.Format("Nova data prevista para conclusão de curso: {0}.", dadosSolicitacao.NovaDataPrevisaoString);
            SolicitacaoServicoDomainService.UpdateFields<SolicitacaoServico>(new SolicitacaoServico { Seq = seqSolicitacaoServico, DescricaoAtualizada = descricaoAtualizada }, x => x.DescricaoAtualizada);

            // ITEM 6
            // Atualizar Histórico-previsão-conclusão: copiar o último registro referente ao aluno-histórico,
            // alterar a data de previsão de conclusão para a nova data informada no formulário e salvar no
            // registro copiado o sequencial da solicitação de serviço em questão
            AlunoHistoricoPrevisaoConclusao novaPrevisao = new AlunoHistoricoPrevisaoConclusao()
            {
                SeqAlunoHistorico = dadosSolicitacao.PrevisaoConclusaoAtual.SeqAlunoHistorico,
                DataLimiteConclusao = dadosSolicitacao.PrevisaoConclusaoAtual.DataLimiteConclusao,
                DataPrevisaoConclusao = NovaDataPrevisaoConclusao,
                SeqSolicitacaoServico = seqSolicitacaoServico
            };
            this.SaveEntity(novaPrevisao);

            // ITEM 4
            // Busca o ciclo letivo referente a nova data de conclusao
            long seqCicloNovaData = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(NovaDataPrevisaoConclusao, dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // 8. Excluir logicamente qualquer situação de matrícula que possua o token PRAZO_CONCLUSAO_ENCERRADO, sem data de exclusão, conforme:
            // -Usuário de exclusão: usuário logado
            // -Data de exclusão: data corrente do sistema
            // -Observação de exclusão: Excluído devido ao novo prazo de conclusão: << Nova data de previsão de conclusão >>
            // -Solicitação de serviço de exclusão: sequencial da solicitação em questão

            // Verifica se o aluno possui ALGUMA situação de PRAZO_CONCLUSAO_ENCERRADO não excluída
            var specSit = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = dadosSolicitacao.SeqPessoaAtuacao,
                TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                Excluido = false
            };
            var sitPrazoEncerrado = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specSit, x => new
            {
                SeqAlunoHistoricoSituacao = x.Seq,
                SeqCicloLetivo = x.AlunoHistoricoCicloLetivo.SeqCicloLetivo
            }).ToList();// Para cada situação de prazo encerrado encontrado

            // Realiza a exclusão lógica da situação de PRAZO_CONCLUSAO_ENCERRADO
            foreach (var situacao in sitPrazoEncerrado)
            {
                var obs = string.Format("Excluído devido ao novo prazo de conclusão: {0}", dadosSolicitacao.NovaDataPrevisaoString);
                AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao, obs, seqSolicitacaoServico);
            }

            // 9. Verificar se no ciclo letivo* da nova data de previsão de conclusão(considerar também a data início e fim do ciclo letivo) [DUVIDA data inicio e fim ciclo letivo]
            // existe registro de situação de matrícula, com token diferente de APTO_MATRICULA. Caso exista, incluir a situação de matrícula, conforme:
            // - Situação de matrícula: PRAZO_CONCLUSAO_ENCERRADO
            // - Data início da situação: nova data de prazo de conclusão +1 dia
            // - Solicitação de serviço: sequencial da solicitação em questão
            //var eventoCicloNovaData = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloNovaData, dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Verificar situação de matricula
            var sitMatriculaCicloNovaData = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(dadosSolicitacao.SeqPessoaAtuacao, seqCicloNovaData);

            if (sitMatriculaCicloNovaData.Any(s => s.TokenSituacaoMatricula != TOKENS_TIPO_SITUACAO_MATRICULA.APTO_MATRICULA && !s.DataExclusao.HasValue))
            {
                IncluirAlunoHistoricoSituacaoVO nova = new IncluirAlunoHistoricoSituacaoVO()
                {
                    SeqAluno = dadosSolicitacao.SeqPessoaAtuacao,
                    SeqCicloLetivo = seqCicloNovaData,
                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.PRAZO_CONCLUSAO_ENCERRADO,
                    SeqSolicitacaoServico = seqSolicitacaoServico,
                    DataInicioSituacao = NovaDataPrevisaoConclusao.AddDays(1)
                };
                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(nova);
            }

            // ITEM 9
            // Calcula a data de inicio do serviço adicional para o envio ao GRA
            DateTime dataInicioAdicional = dadosSolicitacao.PrevisaoConclusaoAtual.DataPrevisaoConclusao.AddDays(1);
            if (dadosSolicitacao.PrevisaoConclusaoAtual.DataLimiteConclusao > dadosSolicitacao.PrevisaoConclusaoAtual.DataPrevisaoConclusao)
                dataInicioAdicional = dadosSolicitacao.PrevisaoConclusaoAtual.DataLimiteConclusao.AddDays(1);

            // Calcula a data de fim do serviço adicional para envio ao GRA
            DateTime dataFimAdicional = NovaDataPrevisaoConclusao;

            // Verifica se deve chamar a rotina do GRA para o serviço adicional
            if (dataInicioAdicional <= dataFimAdicional)
            {
                // Busca o CodigoServicoOrigem do aluno no SGP
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao);

                // Busca os ciclos letivos do período de serviço adicional
                var clAdicional = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventosLetivoPorPeriodo(dataInicioAdicional, dataFimAdicional, dadosOrigem.SeqCursoOfertaLocalidadeTurno, TipoAluno.Veterano);

                // Para cada ciclo letivo de serviço adicional, busca a situação do aluno
                var ciclosData = new List<CicloLetivoData>();
                foreach (var ciclo in clAdicional)
                {
                    var sitAlunoCicloData = SituacaoAluno.NaoMatriculado;
                    var specUltimaSit = new AlunoHistoricoSituacaoFilterSpecification()
                    {
                        SeqPessoaAtuacaoAluno = dadosSolicitacao.SeqPessoaAtuacao,
                        SeqCicloLetivo = ciclo.SeqCicloLetivo,
                        SituacaoFutura = false
                    };
                    specUltimaSit.SetOrderBy(s => s.DataInicioSituacao);
                    var tokenTipoUltimaSituacao = AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(specUltimaSit,
                                x => x.SituacaoMatricula.TipoSituacaoMatricula.Token).LastOrDefault();
                    if (tokenTipoUltimaSituacao != null &&
                        (tokenTipoUltimaSituacao == TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO ||
                         tokenTipoUltimaSituacao == TOKENS_TIPO_SITUACAO_MATRICULA.NAO_MATRICULADO ||
                         tokenTipoUltimaSituacao == TOKENS_TIPO_SITUACAO_MATRICULA.TRANCADO))
                    {
                        sitAlunoCicloData = SituacaoAluno.Matriculado;
                    }

                    ciclosData.Add(new CicloLetivoData()
                    {
                        Ano = ciclo.Ano,
                        DataFim = ciclo.DataFim,
                        DataInicio = ciclo.DataInicio,
                        Semestre = ciclo.Numero,
                        SituacaoAluno = sitAlunoCicloData
                    });
                }

                // Chamar a rotina do financeiro st_inclui_servico_adicional_ACADEMICO para incluir o serviço
                // adicional para o aluno
                ServicoAdicionalParametroData param = new ServicoAdicionalParametroData()
                {
                    SeqOrigem = (int)dadosOrigem.SeqOrigem,
                    SeqPessoaAtuacao = dadosSolicitacao.CodigoAlunoMigracao.GetValueOrDefault(),
                    TipoVinculoAlunoFinanceiro = (TipoVinculoAlunoFinanceiro)dadosOrigem.SeqTipoVinculoAluno,
                    CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                    DataSolicitacaoTransacao = DateTime.Now,
                    CiclosLetivos = ciclosData,
                    DataInicioServicoAdicional = dataInicioAdicional,
                    DataFimServicoAdicional = dataFimAdicional,
                    CodigoTipoTransacao = 46,
                    PercentualMultaServicoAdicional = dadosSolicitacao.ValorPercentualServicoAdicional.GetValueOrDefault()
                };
                string erroGRA = IntegracaoFinanceiroService.IncluirServicoAdicional(param);
                if (!string.IsNullOrEmpty(erroGRA))
                    throw new IncluirServicoAdicionalException(erroGRA);
            }

            // 7. Atualizar Pessoa - atuação - bloqueio: se a pessoa atuação referente ao aluno em questão possuir o bloqueio PRAZO_CONCLUSAO_CURSO_ENCERRADO,
            // atualizar a Data bloqueio para a nova data de previsão de conclusão de curso +1 e a situação para “Bloqueado”.
            var specPAB = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao,
                TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO
            };
            var bloqueio = PessoaAtuacaoBloqueioDomainService.SearchByKey(specPAB);
            if (bloqueio != null)
            {
                // Altera a data de bloqueio para a nova data de previsao + 1 dia
                bloqueio.SituacaoBloqueio = SituacaoBloqueio.Bloqueado;
                bloqueio.DataBloqueio = NovaDataPrevisaoConclusao.AddDays(1);
                PessoaAtuacaoBloqueioDomainService.UpdateFields(bloqueio, b => b.DataBloqueio, b => b.SituacaoBloqueio);
            }

            // ITEM 5
            // Busca o ultimo bloqueio de IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO do aluno
            specPAB.TokenMotivoBloqueio = TOKEN_MOTIVO_BLOQUEIO.IMPEDIMENTO_RENOVACAO_PRAZO_CONCLUSAO;
            var bloqueioImpedimento = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(specPAB).OrderByDescending(x => x.DataBloqueio).FirstOrDefault();
            if (bloqueioImpedimento != null && bloqueioImpedimento.SituacaoBloqueio == SituacaoBloqueio.Bloqueado)
            {
                // Busca o ciclo letivo da atual data de previsão de conclusão
                long seqCicloDataAtual = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(dadosSolicitacao.PrevisaoConclusaoAtual.DataPrevisaoConclusao, dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                // Se o ciclo letivo da nova data for DIFERENTE do ciclo letivo da data anterior, desbloqueia
                if (seqCicloNovaData != seqCicloDataAtual)
                {
                    // Desbloqueia o bloqueio de impedimento
                    bloqueioImpedimento.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                    bloqueioImpedimento.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                    bloqueioImpedimento.DataDesbloqueioEfetivo = DateTime.Now;
                    bloqueioImpedimento.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                    bloqueioImpedimento.JustificativaDesbloqueio = "Data de previsão de conclusão prorrogada.";
                    PessoaAtuacaoBloqueioDomainService.UpdateFields(bloqueioImpedimento, b => b.SituacaoBloqueio,
                                                                                         b => b.TipoDesbloqueio,
                                                                                         b => b.DataDesbloqueioEfetivo,
                                                                                         b => b.UsuarioDesbloqueioEfetivo,
                                                                                         b => b.JustificativaDesbloqueio);
                }
            }

            return dicTagsNot;
        }
    }
}