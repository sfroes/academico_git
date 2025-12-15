using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CAM.Exceptions.CilcoLetivo;
using SMC.Academico.Common.Areas.CSO.Exceptions;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoReaberturaMatricula;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Models;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Financeiro.ServiceContract.Areas.GRA.Data;
using SMC.Financeiro.ServiceContract.BLT;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.MAT.DomainServices
{
    public class SolicitacaoReaberturaMatriculaDomainService : AcademicoContextDomain<SolicitacaoReaberturaMatricula>
    {
        #region [ Queries ]

        #region [ _inserirSolicitacaoDispensaPorSolicitacaoServico ]

        private string _inserirSolicitacaoReaberturaMatriculaPorSolicitacaoServico =
                        @"SET IDENTITY_INSERT MAT.solicitacao_reabertura_matricula ON
                          INSERT INTO MAT.solicitacao_reabertura_matricula (seq_solicitacao_servico) VALUES ({0})
                          SET IDENTITY_INSERT MAT.solicitacao_reabertura_matricula OFF";

        #endregion [ _inserirSolicitacaoDispensaPorSolicitacaoServico ]

        #endregion [ Queries ]

        #region [Domain Service]

        internal GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService => this.Create<GrupoEscalonamentoDomainService>();

        internal ServicoDomainService ServicoDomainService => this.Create<ServicoDomainService>();

        internal ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => this.Create<ConfiguracaoEventoLetivoDomainService>();

        internal CicloLetivoDomainService CicloLetivoDomainService => this.Create<CicloLetivoDomainService>();

        internal AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        internal SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        internal ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        internal SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        internal PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        internal AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();
        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        #endregion [Domain Service]

        #region [Services]

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        private IIntegracaoFinanceiroService IntegracaoFinanceiroService => Create<IIntegracaoFinanceiroService>();

        #endregion [Services]

        public AtendimentoReaberturaVO BuscarDadosAtendimentoReabertura(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var dadosSolicitacao = SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoReaberturaMatricula>(seqSolicitacaoServico), x => new
            {
                x.SeqGrupoEscalonamentoMatricula,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqEntidadeVinculo = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                SeqCursoOfertaLocalidadeTurno = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqCursoOfertaLocalidadeTurno,
            });

            // Recupera o processo do grupo selecionado
            long? seqProcessoGrupoSelecionado = null;
            if (dadosSolicitacao.SeqGrupoEscalonamentoMatricula.HasValue)
                seqProcessoGrupoSelecionado = GrupoEscalonamentoDomainService.SearchProjectionByKey(dadosSolicitacao.SeqGrupoEscalonamentoMatricula.Value, x => x.SeqProcesso);

            /*Listar os processos que:
            1. Sejam do serviço MATRICULA_REABERTURA e estejam ativos, isto é, com a data inicio do processo
            menor ou igual a data atual e a data fim do processo maior ou igual a data atual;
            1.1. Além disso, somente para aluno com a situação “TRANCADO”, o ciclo letivo do processo
            deverá ser igual ao ciclo letivo da situação em questão.
            2. Que a unidade responsável do processo seja igual a entidade do aluno (solicitante);
            3. Em ordem crescente pela data de início do processo*/

            var specServico = new ServicoFilterSpecification { Token = TOKEN_SERVICO.MATRICULA_REABERTURA };
            var processos = ServicoDomainService.SearchProjectionByKey(specServico, x => x.Processos.Where(p => p.DataInicio <= DateTime.Now && (!p.DataFim.HasValue || p.DataFim.Value >= DateTime.Now) && p.UnidadesResponsaveis.Any(u => u.SeqEntidadeResponsavel == dadosSolicitacao.SeqEntidadeVinculo))).ToList();

            var situacaoAtual = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(dadosSolicitacao.SeqPessoaAtuacao);
            if (situacaoAtual.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.TRANCADO)
            {
                processos = processos.Where(a => a.SeqCicloLetivo == situacaoAtual.SeqCiclo).ToList();
            }

            var processosRetorno = processos.OrderBy(p => p.DataInicio).Select(p => new SMCDatasourceItem
            {
                Seq = p.Seq,
                Descricao = p.Descricao
            }).ToList();

            return new AtendimentoReaberturaVO
            {
                SeqGrupoEscalonamentoMatricula = dadosSolicitacao.SeqGrupoEscalonamentoMatricula,
                SeqProcessoEscalonamentoReabertura = seqProcessoGrupoSelecionado,
                Processos = processosRetorno
            };
        }

        /// <summary>
        /// Realiza o deferimento de uma solicitação de reabertura de matrícula (RN_SRC_075)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço de reabertura</param>
        /// <returns>Dicionário para envio de notificação</returns>
        public Dictionary<string, string> DeferirReaberturaMatricula(long seqSolicitacaoServico)
        {
            // Busca informações da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoReaberturaMatricula>(seqSolicitacaoServico);
            var dadosSolicitacao = this.SearchProjectionByKey(specSolicitacao, x => new
            {
                SeqProcessoMatricula = (long?)x.GrupoEscalonamentoMatricula.SeqProcesso,
                DescricaoProcesso = x.GrupoEscalonamentoMatricula.Processo.Descricao,
                SeqGrupoEscalonamentoMatricula = x.SeqGrupoEscalonamentoMatricula,
                Escalonamento1Etapa = x.GrupoEscalonamentoMatricula.Itens.FirstOrDefault(i => i.Escalonamento.ProcessoEtapa.Ordem == 1).Escalonamento,
                SeqCicloLetivoProcesso = x.GrupoEscalonamentoMatricula.Processo.SeqCicloLetivo,
                AnoCicloLetivoProcesso = x.GrupoEscalonamentoMatricula.Processo.CicloLetivo.Ano,
                SemestreCicloLetivoProcesso = x.GrupoEscalonamentoMatricula.Processo.CicloLetivo.Numero,
                DataSolicitacao = x.DataSolicitacao,

                OrigemSolicitacaoServico = (OrigemSolicitacaoServico?)x.GrupoEscalonamentoMatricula.Processo.Servico.OrigemSolicitacaoServico,
                SeqServico = (long?)x.GrupoEscalonamentoMatricula.Processo.SeqServico,
                TokenServico = x.GrupoEscalonamentoMatricula.Processo.Servico.Token,

                SeqPessoaAtuacao = (long?)x.SeqPessoaAtuacao,
                SeqCursoOfertaLocalidadeTurno = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqCursoOfertaLocalidadeTurno,
                SeqEntidadeVinculo = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                SeqTipoVinculoAluno = (long?)(x.PessoaAtuacao as Aluno).SeqTipoVinculoAluno,
                SeqAlunoHistorico = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).Seq,
                SeqEntidadeInstituicaoEnsino = (long?)(x.PessoaAtuacao as Aluno).Pessoa.SeqInstituicaoEnsino,
                SeqNivelEnsino = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).SeqNivelEnsino,
                CodigoAlunoMigracao = (x.PessoaAtuacao as Aluno).CodigoAlunoMigracao,
                NomeAluno = (x.PessoaAtuacao as Aluno).DadosPessoais.Nome,
                NomeSocialAluno = (x.PessoaAtuacao as Aluno).DadosPessoais.NomeSocial,
                SeqEntidadeCurso = (long?)(x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoUnidade.SeqCurso,
                NomeCursoOferta = (x.PessoaAtuacao as Aluno).Historicos.FirstOrDefault(h => h.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                BloqueioPrazo = (x.PessoaAtuacao as Aluno).Bloqueios.FirstOrDefault(b => b.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.PRAZO_CONCLUSAO_CURSO_ENCERRADO)
            });

            // 1. Consistir se o campo “Grupo de escalonamento” foi preenchido
            // Caso o campo esteja vazio, abortar a operação e exibir a mensagem de erro:
            // “Para deferir a solicitação é necessário selecionar o grupo de escalonamento na página anterior”.
            if (!dadosSolicitacao.SeqGrupoEscalonamentoMatricula.HasValue)
                throw new GrupoEscalonamentoNaoInformadoException();

            // Verifica se os dados do aluno encontrados são válidos
            if (!dadosSolicitacao.SeqCicloLetivoProcesso.HasValue)
                throw new CicloLetivoInvalidoException();
            if (!dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.HasValue)
                throw new CursoOfertaLocalidadeTurnoInvalidoException();
            if (!dadosSolicitacao.CodigoAlunoMigracao.HasValue)
                throw new AlunoMigracaoInvalidoException();

            // Busca o PERIODO_CICLO_LETIVO do ciclo do processo para o aluno
            var evento = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(dadosSolicitacao.SeqCicloLetivoProcesso.Value, dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.Value, TipoAluno.Veterano, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

            // Busca os dados de origem do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosSolicitacao.SeqPessoaAtuacao.Value);

            // Task 35602
            /* Quando o serviço possuir o token SOLICITACAO_REABERTURA, ao clicar no comando "Confirmar" e a opção selecionada for "Deferido", incluir a consistência:
             * Verificar se existe oferta de matriz associada no plano de estudo atual do ciclo letivo mais recente que o aluno possui plano.
             * Caso não exista, abortar a operação e exibir a seguinte mensagem de erro:
             * "Para deferir a solicitação é necessário que o aluno possua oferta de matriz associada." */

            if (dadosOrigem.SeqMatrizCurricularOferta == 0)
                throw new SolicitacaoReaberturaAlunoSemOfertaMatrizException();

            // 1,5. Consistir se as parcelas do aluno no financeiro estão ok para reabrir a matrícula.
            VerificarReaberturaMatriculaFiltroData filtro = new VerificarReaberturaMatriculaFiltroData()
            {
                CodigoAluno = dadosSolicitacao.CodigoAlunoMigracao.Value,
                SeqOrigem = 1,
                CodigoServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                DataReferencia = evento.DataInicio
            };
            string validaGRA = IntegracaoFinanceiroService.VerificarReaberturaMatricula(filtro);
            if (!string.IsNullOrEmpty(validaGRA))
                throw new SMCApplicationException(validaGRA);

            // 2.Criar solicitação de matrícula de reabertura, utilizando a regra: RN_MAT_103 - Criação solicitação
            // matrícula - Renovação e Reabertura, considerando que o processo onde será criada a solicitação é o
            // processo do grupo de escalonamento de matrícula associado.

            // Prepara objeto para criar solicitação de reabertura
            var dadosReabertura = new SolicitacaoRematriculaVO()
            {
                // processo
                SeqProcesso = dadosSolicitacao.SeqProcessoMatricula.GetValueOrDefault(),
                DescricaoProcesso = dadosSolicitacao.DescricaoProcesso,
                SeqGrupoEscalonamento = dadosSolicitacao.SeqGrupoEscalonamentoMatricula.Value,
                SeqCicloLetivoProcesso = dadosSolicitacao.SeqCicloLetivoProcesso.Value,

                // servico
                OrigemSolicitacaoServico = dadosSolicitacao.OrigemSolicitacaoServico.GetValueOrDefault(),
                SeqServico = dadosSolicitacao.SeqServico.GetValueOrDefault(),
                TokenServico = dadosSolicitacao.TokenServico,

                // aluno
                SeqPessoaAtuacao = dadosSolicitacao.SeqPessoaAtuacao.GetValueOrDefault(),
                NomeAluno = string.IsNullOrEmpty(dadosSolicitacao.NomeSocialAluno) ? dadosSolicitacao.NomeAluno : dadosSolicitacao.NomeSocialAluno,
                SeqEntidadeVinculo = dadosSolicitacao.SeqEntidadeVinculo.GetValueOrDefault(),
                SeqTipoVinculoAluno = dadosSolicitacao.SeqTipoVinculoAluno.GetValueOrDefault(),
                SeqAlunoHistorico = dadosSolicitacao.SeqAlunoHistorico.GetValueOrDefault(),
                SeqCursoOfertaLocalidadeTurno = dadosSolicitacao.SeqCursoOfertaLocalidadeTurno.Value,
                SeqEntidadeCurso = dadosSolicitacao.SeqEntidadeCurso.GetValueOrDefault(),
                SeqEntidadeInstituicaoEnsino = dadosSolicitacao.SeqEntidadeInstituicaoEnsino.GetValueOrDefault(),
                SeqNivelEnsino = dadosSolicitacao.SeqNivelEnsino.GetValueOrDefault(),
                CriaBloqueioImpedimentoPrazo = false,
                SeqMatrizCurriculaOferta = dadosOrigem.SeqMatrizCurricularOferta
            };

            // Verifica se deve criar bloqueio de impedimento de prazo encerrado
            // Cria o bloqueio caso o bloqueio de prazo encerrado do aluno esteja bloqueado e a data
            // de início do bloqueio seja posterior a data atual e anterior ao inicio do ciclo letivo
            // do processo.
            if (dadosSolicitacao.BloqueioPrazo != null &&
                dadosSolicitacao.BloqueioPrazo.SituacaoBloqueio == SituacaoBloqueio.Bloqueado &&
                dadosSolicitacao.BloqueioPrazo.DataBloqueio > DateTime.Today &&
                dadosSolicitacao.BloqueioPrazo.DataBloqueio <= evento.DataInicio)
            {
                dadosReabertura.CriaBloqueioImpedimentoPrazo = true;
            }

            /*6.Chamar a rotina para atualizar a situação do aluno e criar parcela de matrícula, se for o caso:
                6.1. Se a situação de matrícula, no ciclo letivo do processo do grupo de escalonamento de matrícula
                associado, for "TRANCADO", chamar st_DescancDestranc_matric_aluno_PosGrad, passando os
                seguintes parâmetros:
                @ANO_CURSADA: ano ciclo letivo do processo do grupo de escalonamento de matrícula
                associado
                , @SEM_CURSADA: sem ciclo letivo do processo do grupo de escalonamento de matrícula
                associado
                , @COD_ALUNO: cod_aluno_migracao (tabela aluno)
                , @COD_SERVICO_ORIGEM: enviar o [código de curso-oferta do SGP]* correspondente do
                curso oferta localidade do histórico mais atual do aluno, quando este possui curso. Senão, simular qual
                seria o seu respectivo curso, conforme o nível de ensino e entidade responsável do histórico mais atual
                do aluno e que o tipo de oferta do curso seja Interno.
                , @DATA: data da solicitação de reabertura
                , @Operacao: 1 -- 1- Destrancamento
                , @usuario: usuário logado
                , @mensagem varchar(255) output
                [Código de curso-oferta do SGP]* = pesquisar direto no SGP através da tabela
                ingresso_aluno.cod_curso_oferta e mais atual.*/
            var situacaoAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAlunoNoCicloLetivo(dadosSolicitacao.SeqPessoaAtuacao.Value, dadosSolicitacao.SeqCicloLetivoProcesso.Value, true);
            //valida se não tem situacao para o processo, desta forma criarei uma situação vazia para validar a situação
            situacaoAluno = situacaoAluno == null ? new SituacaoAlunoVO() : situacaoAluno;
            if (situacaoAluno.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.TRANCADO)
            {
                var modeloDestrancamento = new DescancelaDestrancaMatriculaAlunoData()
                {
                    AnoCursada = dadosSolicitacao.AnoCicloLetivoProcesso,
                    SemCursada = dadosSolicitacao.SemestreCicloLetivoProcesso,
                    CodAluno = dadosSolicitacao.CodigoAlunoMigracao.Value,
                    CodServicoOrigem = dadosOrigem.CodigoServicoOrigem,
                    Data = dadosSolicitacao.DataSolicitacao,
                    Operacao = 1,
                    Usuario = SMCContext.User.Identity.Name
                };
                IntegracaoAcademicoService.DescancelarDestrancarMatriculaAluno(modeloDestrancamento);
            }
            var seqSolicitacao = SolicitacaoMatriculaDomainService.CriarSolicitacaoRematriculaAluno(dadosReabertura);

            // Se não criou a solicitação, erro
            string numeroProtocolo = string.Empty;
            if (!seqSolicitacao.HasValue)
                throw new SolicitacaoMatriculaReaberturaNaoCriadaException();
            else
                numeroProtocolo = SolicitacaoServicoDomainService.BuscarNumeroProtocoloSolicitacaoServico(seqSolicitacao.Value);

            // 3.Salvar no campo "Descrição da solicitação atualizada" em formato html:
            // "Foi criada a solicitação<numero protocolo> para que o aluno faça a matrícula de reabertura."
            // 4.Salvar o ID da solicitação(criada no item 2) na estrutura específica da solicitação de reabertura de
            // matricula. (campo seq_solicitacao_matricula)
            var solicitacaoAtualizada = new SolicitacaoReaberturaMatricula()
            {
                Seq = seqSolicitacaoServico,
                DescricaoAtualizada = string.Format("Foi criada a solicitação nº {0} para que o aluno faça a matrícula de reabertura.", numeroProtocolo),
                SeqSolicitacaoMatricula = seqSolicitacao.Value
            };
            this.UpdateFields<SolicitacaoReaberturaMatricula>(solicitacaoAtualizada, x => x.DescricaoAtualizada, x => x.SeqSolicitacaoMatricula);

            /* 6.2.Se não for, chamar st_retorna_aluno_SGP_ACADEMICO, passando os seguintes parâmetros:
                 @ano_ciclo_letivo: ano ciclo letivo do processo do grupo de escalonamento de matrícula associado
                 , @num_ciclo_let ivo sem ciclo letivo do processo do grupo de escalonamento de matrícula
                 associado
                 , @cod_aluno_SGP: cod_aluno_migracao(tabela aluno)
                 , @usuario: usuário logado
                 , @dsc_erro varchar(max) output*/
            if (situacaoAluno.TokenSituacaoMatricula != TOKENS_SITUACAO_MATRICULA.TRANCADO)
            {
                var modeloSGP = new RetornaAlunoSGPData()
                {
                    AnoCicloLetivo = evento.Ano,
                    NumeroCicloLetivo = evento.Numero,
                    CodigoAlunoSGP = dadosSolicitacao.CodigoAlunoMigracao.Value,
                    Usuario = SMCContext.User.Identity.Name
                };
                IntegracaoAcademicoService.RetornarAlunoSGP(modeloSGP);
            }
            // Prepara o dicionário para envio de notificação
            Dictionary<string, string> dicTagsNot = new Dictionary<string, string>();
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.DAT_INICIO_ESCALONAMENTO, dadosSolicitacao.Escalonamento1Etapa.DataInicio.ToString("dd/MM/yyyy"));
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.HOR_INICIO_ESCALONAMENTO, dadosSolicitacao.Escalonamento1Etapa.DataInicio.ToString("HH:mm"));
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.DAT_FIM_ESCALONAMENTO, dadosSolicitacao.Escalonamento1Etapa.DataFim.ToString("dd/MM/yyyy"));
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.HOR_FIM_ESCALONAMENTO, dadosSolicitacao.Escalonamento1Etapa.DataFim.ToString("HH:mm"));
            dicTagsNot.Add(TOKEN_TAG_NOTIFICACAO.CURSO_OFERTA, dadosSolicitacao.NomeCursoOferta);
            
            return dicTagsNot;
        }

        /// <summary>
        /// Transforma a solicitação de serviço em solicitação de reabertura de matrícula
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço a ser transformada em solicitação de reabertura</param>
        public void CriarSolicitacaoReaberturaMatriculaPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoReaberturaMatricula>(seqSolicitacaoServico));

            if (registro == null)
                ExecuteSqlCommand(string.Format(_inserirSolicitacaoReaberturaMatriculaPorSolicitacaoServico, seqSolicitacaoServico));
        }
    }
}