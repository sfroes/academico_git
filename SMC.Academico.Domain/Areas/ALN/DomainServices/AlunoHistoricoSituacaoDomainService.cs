using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoHistoricoSituacaoDomainService : AcademicoContextDomain<AlunoHistoricoSituacao>
    {
        #region [DOMAIN SERVICE]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        private SituacaoMatriculaDomainService SituacaoMatriculaDomainService => Create<SituacaoMatriculaDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        #endregion [DOMAIN SERVICE]

        /// <summary>
        /// Inclui uma nova situação no historico atual do aluno
        /// </summary>
        /// <param name="dados">Dados para inclusão da situação</param>
        public void IncluirAlunoHistoricoSituacao(IncluirAlunoHistoricoSituacaoVO dados)
        {
            // Busca a situação com o token informado.
            var seqSituacao = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(dados.TokenSituacao);

            // Se não encontrou a situação do token informado, erro
            if (seqSituacao <= 0)
                throw new TokenSituacaoMatriculaInvalidoException();

            // Verifica se foi informado o SeqAlunoHistoricoCicloLetivo, ou se precisa recuperar
            if (!dados.SeqAlunoHistoricoCicloLetivo.HasValue)
            {
                // Busca os seqs
                var seqsHistorico = AlunoDomainService.SearchProjectionByKey(dados.SeqAluno, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                {
                    SeqAlunoHistorico = h.Seq,
                    SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == dados.SeqCicloLetivo).Seq
                }).FirstOrDefault());

                // Busca a referencia do AlunoHistoricoCicloLetivo
                dados.SeqAlunoHistoricoCicloLetivo = seqsHistorico.SeqAlunoHistoricoCicloLetivo;

                // Se não conseguiu recuperar...
                if (!dados.SeqAlunoHistoricoCicloLetivo.HasValue)
                {
                    // Verifica se deve criar o histórico no ciclo letivo
                    if (dados.CriarAlunoHistoricoCicloLetivo)
                    {
                        var alunoHistoricoCiclo = new AlunoHistoricoCicloLetivo
                        {
                            SeqAlunoHistorico = seqsHistorico.SeqAlunoHistorico,
                            SeqCicloLetivo = dados.SeqCicloLetivo.Value,
                            TipoAluno = TipoAluno.Veterano,
                        };
                        AlunoHistoricoCicloLetivoDomainService.SaveEntity(alunoHistoricoCiclo);
                        dados.SeqAlunoHistoricoCicloLetivo = alunoHistoricoCiclo.Seq;
                    }
                    else // Senão, erro
                        throw new AlunoHistoricoCicloLetivoInvalidoException();
                }
            }

            // Inclui o histórico
            var historico = new AlunoHistoricoSituacao
            {
                SeqSituacaoMatricula = seqSituacao,
                SeqSolicitacaoServico = dados.SeqSolicitacaoServico,
                DataInicioSituacao = dados.DataInicioSituacao,
                SeqAlunoHistoricoCicloLetivo = dados.SeqAlunoHistoricoCicloLetivo.Value,
                Observacao = dados.Observacao,
                SeqPeriodoIntercambio = dados.SeqPeriodoIntercambio,
                SeqArquivoAnexado = dados.SeqArquivoAnexado,
                ArquivoAnexado = dados.ArquivoAnexado
            };

            this.EnsureFileIntegrity(historico, m => m.SeqArquivoAnexado, m => m.ArquivoAnexado);

            this.SaveEntity(historico);
        }

        /// <summary>
        /// Inclui uma nova situação no historico atual do aluno, retornando o Seq da situação incluída
        /// </summary>
        /// <param name="dados">Dados para inclusão da situação</param>
        public long IncluirAlunoHistoricoSituacaoRetornaSeq(IncluirAlunoHistoricoSituacaoVO dados)
        {
            // Busca a situação com o token informado.
            var seqSituacao = SituacaoMatriculaDomainService.BuscarSituacaoMatriculaPorToken(dados.TokenSituacao);

            // Se não encontrou a situação do token informado, erro
            if (seqSituacao <= 0)
                throw new TokenSituacaoMatriculaInvalidoException();

            // Verifica se foi informado o SeqAlunoHistoricoCicloLetivo, ou se precisa recuperar
            if (!dados.SeqAlunoHistoricoCicloLetivo.HasValue)
            {
                // Busca os seqs
                var seqsHistorico = AlunoDomainService.SearchProjectionByKey(dados.SeqAluno, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                {
                    SeqAlunoHistorico = h.Seq,
                    SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == dados.SeqCicloLetivo).Seq
                }).FirstOrDefault());

                // Busca a referencia do AlunoHistoricoCicloLetivo
                dados.SeqAlunoHistoricoCicloLetivo = seqsHistorico.SeqAlunoHistoricoCicloLetivo;

                // Se não conseguiu recuperar...
                if (!dados.SeqAlunoHistoricoCicloLetivo.HasValue)
                {
                    // Verifica se deve criar o histórico no ciclo letivo
                    if (dados.CriarAlunoHistoricoCicloLetivo)
                    {
                        var alunoHistoricoCiclo = new AlunoHistoricoCicloLetivo
                        {
                            SeqAlunoHistorico = seqsHistorico.SeqAlunoHistorico,
                            SeqCicloLetivo = dados.SeqCicloLetivo.Value,
                            TipoAluno = TipoAluno.Veterano,
                        };
                        AlunoHistoricoCicloLetivoDomainService.SaveEntity(alunoHistoricoCiclo);
                        dados.SeqAlunoHistoricoCicloLetivo = alunoHistoricoCiclo.Seq;
                    }
                    else // Senão, erro
                        throw new AlunoHistoricoCicloLetivoInvalidoException();
                }
            }

            // Inclui o histórico
            var historico = new AlunoHistoricoSituacao
            {
                SeqSituacaoMatricula = seqSituacao,
                SeqSolicitacaoServico = dados.SeqSolicitacaoServico,
                DataInicioSituacao = dados.DataInicioSituacao,
                SeqAlunoHistoricoCicloLetivo = dados.SeqAlunoHistoricoCicloLetivo.Value,
                Observacao = dados.Observacao,
                SeqPeriodoIntercambio = dados.SeqPeriodoIntercambio,
                SeqArquivoAnexado = dados.SeqArquivoAnexado,
                ArquivoAnexado = dados.ArquivoAnexado
            };

            this.EnsureFileIntegrity(historico, m => m.SeqArquivoAnexado, m => m.ArquivoAnexado);

            this.SaveEntity(historico);

            return historico.Seq;
        }

        /// <summary>
        /// Exclui uma situação do histórico do aluno
        /// </summary>
        /// <param name="seq">Sequencial do historico de situação a ser excluído</param>
        /// <param name="observacao">Observação da exclusão</param>
        /// <param name="seqSolicitacao">Sequencial da soliciação que realiza a exclusão</param>
        public void ExcluirAlunoHistoricoSituacao(long seq, string observacao, long? seqSolicitacao, string usuarioServico = null)
        {
            var alunoHistoricoSituacao = this.SearchByKey(seq);

            if (alunoHistoricoSituacao != null)
            {
                alunoHistoricoSituacao.DataExclusao = DateTime.Now;
                alunoHistoricoSituacao.UsuarioExclusao = string.IsNullOrEmpty(usuarioServico) ? SMCContext.User.Identity.Name : string.Format("{0}/{1}", SMCContext.User.Identity.Name, usuarioServico);
                alunoHistoricoSituacao.ObservacaoExclusao = observacao;
                alunoHistoricoSituacao.SeqSolicitacaoServicoExclusao = seqSolicitacao;
            }

            this.UpdateFields<AlunoHistoricoSituacao>(alunoHistoricoSituacao, x => x.DataExclusao, x => x.UsuarioExclusao, x => x.ObservacaoExclusao, x => x.SeqSolicitacaoServicoExclusao);
        }

        /// <summary>
        /// Exclui todas as situações do histórico do aluno em um ciclo letivo
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação do aluno</param>
        /// <param name="seqCicloLetivo">Ciclo letivo a ter as situações excluídas</param>
        /// <param name="observacao">Observação de exclusão</param>
        /// <param name="seqSolicitacao">Sequencial da soliciação que realiza a exclusão</param>
        public void ExcluirTodasSituacoesAlunoNoCiclo(long seqPessoaAtuacao, long seqCicloLetivo, string observacao, long? seqSolicitacao, string usuarioServico = null)
        {
            // Busca todas as situações do aluno no ciclo letivo
            var spec = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqPessoaAtuacao,
                SeqCicloLetivo = seqCicloLetivo
            };
            var seqsSituacao = this.SearchProjectionBySpecification(spec, s => s.Seq);
            foreach (var seq in seqsSituacao)
            {
                this.ExcluirAlunoHistoricoSituacao(seq, observacao, seqSolicitacao, usuarioServico);
            }
        }

        /// <summary>
        /// Busca as situações futuras de um aluno considerando uma data de referencia e um ciclo letivo (opcional)
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial do aluno</param>
        /// <param name="dataReferencia">Data de referencia para consulta as situações futuras</param>
        /// <param name="seqCicloLetivo">Ciclo letivo (opcional). Caso informado, considera as situações apenas do ciclo letivo informado.</param>
        /// <returns>Lista de situações futuras do aluno</returns>
        public List<SituacaoFuturaAlunoVO> BuscarSituacoesFuturasAluno(long seqPessoaAtuacao, DateTime? dataReferencia, long? seqCicloLetivo = null)
        {
            // Caso não seja informado NEM data de referencia NEM seq do ciclo letivo, considera a data atual.
            // Antes a data era obrigatória. Mudei para que seja obrigatória caso não passe o ciclo letivo
            if (!dataReferencia.HasValue && !seqCicloLetivo.HasValue)
                dataReferencia = DateTime.Now;

            var spec = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqPessoaAtuacao,
                SituacaoFutura = dataReferencia.HasValue,
                DataReferencia = dataReferencia,
                SeqCicloLetivo = seqCicloLetivo,
                Excluido = !dataReferencia.HasValue && seqCicloLetivo.HasValue ? (bool?)false : null // Coloquei essa validação para o caso de não ter data mas ter ciclo. Dai pegaria todas que sejam do ciclo e que não estão excluidas.
            };

            return this.SearchProjectionBySpecification(spec, x => new SituacaoFuturaAlunoVO()
            {
                SeqAlunoHistoricoSituacao = x.Seq,
                DescricaoSituacaoFutura = x.SituacaoMatricula.Descricao,
                DataInicio = x.DataInicioSituacao,
                ProtocoloSolicitacaoOrigem = x.SolicitacaoServico.NumeroProtocolo,
                DescricaoProcesso = x.SolicitacaoServico.ConfiguracaoProcesso.Processo.Descricao,
                TokenSituacaoFutura = x.SituacaoMatricula.Token
            }).ToList();
        }

        /// <summary>
        /// Buscar a situação de um aluno em um ciclo letivo em uma data de referencia
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="dataReferencia">Data de referencia para consulta</param>
        /// <returns>Informações da situação do aluno</returns>
        public SituacaoAlunoVO BuscarSituacaoAlunoNaData(long seqAluno, DateTime dataReferencia)
        {
            // Dados de origem do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, false);

            // Busca o ciclo letivo na data de referência
            var cicloLetivo = this.ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(dataReferencia, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            return BuscarSituacaoAlunoNaData(seqAluno, cicloLetivo.SeqCicloLetivo, dataReferencia);
        }

        /// <summary>
        /// Buscar a situação de um aluno em um ciclo letivo em uma data de referencia
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo a ser pesquisado</param>
        /// <param name="dataReferencia">Data de referencia para consulta</param>
        /// <returns>Informações da situação do aluno</returns>
        public SituacaoAlunoVO BuscarSituacaoAlunoNaData(long seqAluno, long seqCicloLetivo, DateTime dataReferencia)
        {
            // Busca a referencia do AlunoHistoricoCicloLetivo
            var seqAlunoHistoricoCicloLetivo = AlunoDomainService.SearchProjectionByKey(seqAluno, a => (long?)a.Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqCicloLetivo).Seq);

            // Se não conseguiu recuperar, retorna NULL
            if (!seqAlunoHistoricoCicloLetivo.HasValue)
                return null;
            else
            {
                // Busca a situação do aluno no historico ciclo
                var spec = new AlunoHistoricoSituacaoFilterSpecification()
                {
                    SeqPessoaAtuacaoAluno = seqAluno,
                    SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivo,
                    SituacaoFutura = false,
                    DataReferencia = dataReferencia
                };
                spec.SetOrderByDescending(x => x.DataInclusao);
                var situacao = this.SearchProjectionBySpecification(spec, s => new SituacaoAlunoVO
                {
                    SeqAlunoHistoricoSituacao = s.Seq,
                    SeqSituacao = s.SeqSituacaoMatricula,
                    Descricao = s.SituacaoMatricula.Descricao,
                    VinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                    TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                    TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                    SeqPeriodoIntercambio = s.SeqPeriodoIntercambio
                }).FirstOrDefault();

                return situacao;
            }
        }

        /// <summary>
        /// Busca a situação do aluno no ciclo letivo atual
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="desativarFiltroDados">Desabilita o filtro de dados de hierarquia_entidade_organizacional para atender o caso de entidades compartilhadas da tela de consulta de solicitações de serviço </param>
        /// <returns>Situação atual</returns>
        public SituacaoAlunoVO BuscarSituacaoAtualAluno(long seqAluno, bool desativarFiltroDados = false)
        {
            // Busca o ciclo letivo atual do aluno
            long seqCiclo = AlunoDomainService.BuscarCicloLetivoAtual(seqAluno, desativarFiltroDados);

            return this.BuscarSituacaoAtualAlunoNoCicloLetivo(seqAluno, seqCiclo, desativarFiltroDados);
        }

        /// <summary>
        /// Busca a situação do aluno em um determinado ciclo letivo, desconsiderando a situação que esteja com data de exclusao informada.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno para busca</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo para busca</param>
        /// <param name="desativarFiltroDados">Flag para desativar ou não o filtro de dados</param>
        /// <returns>Informações da situação atual do aluno no ciclo letivo</returns>
        public SituacaoAlunoVO BuscarSituacaoAtualAlunoNoCicloLetivo(long seqAluno, long seqCicloLetivo, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            // Busca a referencia do AlunoHistoricoCicloLetivo
            var seqAlunoHistoricoCicloLetivo = AlunoDomainService.SearchProjectionByKey(seqAluno, a => (long?)a.Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqCicloLetivo && !c.DataExclusao.HasValue).Seq);

            // Prepara retorno. Se não encontrar a referencia de AlunoHistoricoCicloLetivo, retorna NULL
            SituacaoAlunoVO situacao = null;

            // Busca a situação do aluno no historico ciclo
            if (seqAlunoHistoricoCicloLetivo.HasValue)
            {
                var spec = new AlunoHistoricoSituacaoFilterSpecification()
                {
                    SeqPessoaAtuacaoAluno = seqAluno,
                    SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivo,
                    SituacaoFutura = false
                };
                spec.SetOrderByDescending(x => x.DataInicioSituacao);
                situacao = this.SearchProjectionBySpecification(spec, s => new SituacaoAlunoVO
                {
                    SeqAlunoHistoricoSituacao = s.Seq,
                    SeqSituacao = s.SeqSituacaoMatricula,
                    SeqCiclo = s.AlunoHistoricoCicloLetivo.SeqCicloLetivo,
                    Descricao = s.SituacaoMatricula.Descricao,
                    DescricaoXSD = s.SituacaoMatricula.DescricaoXSD,
                    DescricaoCicloLetivo = s.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                    VinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                    TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                    TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                    SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                    DataInicioSituacao = s.DataInicioSituacao
                }).FirstOrDefault();
            }

            if (desativarFiltroDados)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return situacao;
        }

        /// <summary>
        /// Busca todas as situações não excluídas de um aluno em um ciclo letivo
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
        /// <returns>Lista de situações do aluno no ciclo letivo, ou NULL</returns>
        public List<SituacaoAlunoVO> BuscarSituacoesAlunoNoCicloLetivo(long seqAluno, long seqCicloLetivo)
        {
            // Busca todas as situações do aluno no ciclo letivo
            var spec = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo
            };
            spec.SetOrderBy(s => s.DataInicioSituacao);
            return this.SearchProjectionBySpecification(spec, s => new SituacaoAlunoVO
            {
                SeqAlunoHistoricoSituacao = s.Seq,
                SeqSituacao = s.SeqSituacaoMatricula,
                Descricao = s.SituacaoMatricula.Descricao,
                VinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                DataExclusao = s.DataExclusao,
                DataInicioSituacao = s.DataInicioSituacao,
                SeqCiclo = s.AlunoHistoricoCicloLetivo.SeqCicloLetivo
            }).ToList();
        }

        public List<SituacaoAlunoVO> BuscarSituacoesAlunoEntreCiclosLetivos(long seqAluno, long seqCicloLetivoInicio, long seqCicloLetivoFim)
        {
            // Busca todas as situações do aluno no ciclo letivo
            var spec = new AlunoHistoricoSituacaoFilterSpecification()
            {
                SeqPessoaAtuacaoAluno = seqAluno,
                FiltrarEntreCiclos = true,
                SeqCicloLetivoInicio = seqCicloLetivoInicio,
                SeqCicloLetivoFim = seqCicloLetivoFim,
                Excluido = false
            };
            spec.SetOrderBy(s => s.DataInicioSituacao);
            return this.SearchProjectionBySpecification(spec, s => new SituacaoAlunoVO
            {
                SeqAlunoHistoricoSituacao = s.Seq,
                SeqSituacao = s.SeqSituacaoMatricula,
                Descricao = s.SituacaoMatricula.Descricao,
                VinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                DataExclusao = s.DataExclusao,
                DataInicioSituacao = s.DataInicioSituacao,
                SeqCiclo = s.AlunoHistoricoCicloLetivo.SeqCicloLetivo
            }).ToList();
        }



        public SituacaoAlunoVO BuscarSituacaoAtualAlunoNoCicloLetivoSegundoDeposito(long seqAluno, long seqCicloLetivo, bool? excluido, bool desativarFiltroDados = false)
        {
            if (desativarFiltroDados)
            {
                this.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.AlunoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }


            var specAlunoHistoricoCicloLetivo = new AlunoHistoricoCicloLetivoFilterSpecification
            {
                SeqPessoaAtuacaoAluno = seqAluno,
                SeqCicloLetivo = seqCicloLetivo
            };

            var alunoHistoricoCicloLetivo = AlunoHistoricoCicloLetivoDomainService.SearchByKey(specAlunoHistoricoCicloLetivo);
            // Busca a referencia do AlunoHistoricoCicloLetivo
            //var seqAlunoHistoricoCicloLetivo = AlunoDomainService.SearchProjectionByKey(seqAluno, a => (long?)a.Historicos.FirstOrDefault(h => h.Atual).HistoricosCicloLetivo.FirstOrDefault(c => c.SeqCicloLetivo == seqCicloLetivo && !c.DataExclusao.HasValue).Seq);

            // Prepara retorno. Se não encontrar a referencia de AlunoHistoricoCicloLetivo, retorna NULL
            SituacaoAlunoVO situacao = null;

            // Busca a situação do aluno no historico ciclo
            if (alunoHistoricoCicloLetivo != null)
            {
                var spec = new AlunoHistoricoSituacaoFilterSpecification()
                {
                    SeqPessoaAtuacaoAluno = seqAluno,
                    SeqAlunoHistoricoCicloLetivo = alunoHistoricoCicloLetivo.Seq,
                    Excluido = excluido

                };
                spec.SetOrderByDescending(x => x.DataInicioSituacao);
                situacao = this.SearchProjectionBySpecification(spec, s => new SituacaoAlunoVO
                {
                    SeqAlunoHistoricoSituacao = s.Seq,
                    SeqSituacao = s.SeqSituacaoMatricula,
                    SeqCiclo = s.AlunoHistoricoCicloLetivo.SeqCicloLetivo,
                    Descricao = s.SituacaoMatricula.Descricao,
                    DescricaoXSD = s.SituacaoMatricula.DescricaoXSD,
                    DescricaoCicloLetivo = s.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                    VinculoAlunoAtivo = s.SituacaoMatricula.TipoSituacaoMatricula.VinculoAlunoAtivo,
                    TokenSituacaoMatricula = s.SituacaoMatricula.Token,
                    TokenTipoSituacaoMatricula = s.SituacaoMatricula.TipoSituacaoMatricula.Token,
                    SeqPeriodoIntercambio = s.SeqPeriodoIntercambio,
                    DataInicioSituacao = s.DataInicioSituacao,
                    DataExclusao = s.DataExclusao
                }).FirstOrDefault();
            }

            if (desativarFiltroDados)
            {
                this.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
                this.AlunoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);
            }

            return situacao;
        }
    }
}