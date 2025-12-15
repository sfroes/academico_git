using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Common.Areas.ORT.Exceptions;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Formularios.ServiceContract.Areas.TMP.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class ApuracaoAvaliacaoDomainService : AcademicoContextDomain<ApuracaoAvaliacao>
    {
        #region [ Domain Services ]

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();
        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService { get => Create<AplicacaoAvaliacaoDomainService>(); }
        private AlunoHistoricoDomainService AlunoHistoricoDomainService { get => Create<AlunoHistoricoDomainService>(); }
        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService { get => Create<TrabalhoAcademicoDomainService>(); }
        private PublicacaoBdpDomainService PublicacaoBdpDomainService { get => Create<PublicacaoBdpDomainService>(); }
        private HistoricoEscolarDomainService HistoricoEscolarDomainService { get => Create<HistoricoEscolarDomainService>(); }
        private CursoDomainService CursoDomainService { get => Create<CursoDomainService>(); }
        private ComponenteCurricularDomainService ComponenteCurricularDomainService { get => Create<ComponenteCurricularDomainService>(); }
        private MembroBancaExaminadoraDomainService MembroBancaExaminadoraDomainService { get => Create<MembroBancaExaminadoraDomainService>(); }
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService { get => Create<ConfiguracaoEventoLetivoDomainService>(); }
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private PlanoEstudoDomainService PlanoEstudoDomainService => Create<PlanoEstudoDomainService>();
        private EntregaOnlineParticipanteDomainService EntregaOnlineParticipanteDomainService => Create<EntregaOnlineParticipanteDomainService>();
        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();
        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();
        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();
        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();
        private OrientacaoColaboradorDomainService OrientacaoColaboradorDomainService => Create<OrientacaoColaboradorDomainService>();

        #endregion [ Domain Services ]

        #region [Serviços]

        private INotificacaoService NotificacaoService => Create<INotificacaoService>();
        private ITipoTemplateProcessoService TipoTemplateProcessoService => Create<ITipoTemplateProcessoService>();

        private ISituacaoService SituacaoService => Create<ISituacaoService>();

        #endregion [Serviços]

        #region [query]

        private string _ciclosLetivosAnterios = @"SELECT seq_ciclo_letivo as Seq FROM CAM.ciclo_letivo WHERE ano_num_ciclo_letivo < {0} ORDER BY ano_num_ciclo_letivo";

        #endregion

        public LancamentoNotaBancaExaminadoraVO BuscarLancamentoNotaBancaExaminadoraInsert(long seqAplicacaoAvaliacao)
        {
            return AplicacaoAvaliacaoDomainService.BuscarLancamentoNotaBancaExaminadoraInsert(seqAplicacaoAvaliacao);
        }

        #region [ Salvar Lançamento de Notas da Banca examinadora ]

        /// <summary>
        /// Salvar o Lançamento de Notas da Banca Examinadora
        /// </summary>
        /// <param name="lancamentoNotaBancaExaminadoraVO"></param>
        /// <returns></returns>
        public long SalvarLancamentoNotaBancaExaminadora(LancamentoNotaBancaExaminadoraVO lancamentoNotaBancaExaminadoraVO)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                // Pelo menos um membro da banca com participação confirmada deverá ser informado
                if (!(lancamentoNotaBancaExaminadoraVO?.MembrosBancaExaminadora?.Any(x => x.Participou.Value)).GetValueOrDefault())
                    throw new LancamentoNotaBancaExaminadoraMembroComParticipacaoObrigadorioException();

                // Valida a Lançamento de notas
                lancamentoNotaBancaExaminadoraVO.ApuracaoNota = AplicacaoAvaliacaoDomainService.ApuracaoNota(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value);
                if (lancamentoNotaBancaExaminadoraVO.ApuracaoNota.GetValueOrDefault())
                {
                    if (!lancamentoNotaBancaExaminadoraVO.Nota.HasValue)
                        throw new LancamentoNotaBancaExaminadoraNotaObrigatoriaException();
                }
                else if (!lancamentoNotaBancaExaminadoraVO.SeqEscalaApuracaoItem.HasValue)
                    throw new LancamentoNotaBancaExaminadoraConceitoObrigatorioException();

                // Busca a aplicação da avaliação e vincular os membros
                var aplicacaoAvaliacao = AplicacaoAvaliacaoDomainService.SearchByKey(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value,
                                                                                        IncludesAplicacaoAvaliacao.Avaliacao |
                                                                                        IncludesAplicacaoAvaliacao.OrigemAvaliacao |
                                                                                        IncludesAplicacaoAvaliacao.MembrosBancaExaminadora |
                                                                                        IncludesAplicacaoAvaliacao.ApuracoesAvaliacao);

                // Atribui os membros da banca
                aplicacaoAvaliacao.MembrosBancaExaminadora = lancamentoNotaBancaExaminadoraVO.MembrosBancaExaminadora.TransformList<MembroBancaExaminadora>();

                // Atualiza a data da avaliação
                lancamentoNotaBancaExaminadoraVO.DataAvaliacao = aplicacaoAvaliacao.DataInicioAplicacaoAvaliacao;

                // Valida o tipo de trabalho e publicação no BDP
                bool publicacaoBibliotecaObrigatoria = TrabalhoAcademicoDomainService.TipoTrabalhoPublicacaoBibliotecaObrigatoria(lancamentoNotaBancaExaminadoraVO.SeqTrabalhoAcademico.Value);
                lancamentoNotaBancaExaminadoraVO.CriterioAprovacaoAprovado = AplicacaoAvaliacaoDomainService.CriterioAprovacaoAprovado(lancamentoNotaBancaExaminadoraVO);

                // Registrar a publicação do trabalho no BDP com a situação "Aguardando Cadastro pelo Aluno".
                if (publicacaoBibliotecaObrigatoria && lancamentoNotaBancaExaminadoraVO.CriterioAprovacaoAprovado.GetValueOrDefault())
                    PublicacaoBdpDomainService.CriarPublicacaoBDP(lancamentoNotaBancaExaminadoraVO.SeqTrabalhoAcademico.Value);

                // Cria uma nova apuração, ou retorna a existente, conforme os atributos preenchido
                var apuracao = VincularApuracao(lancamentoNotaBancaExaminadoraVO, aplicacaoAvaliacao);

                // Vincula o Aluno Historico com a apuração
                apuracao.SeqAlunoHistorico = AtualizarHistoricoAluno(lancamentoNotaBancaExaminadoraVO);

                // Salva a entidade de aplicação avaliação
                AplicacaoAvaliacaoDomainService.SaveEntity(aplicacaoAvaliacao);

                //RN_ORT_015
                //6.Caso tipo de trabalho esteja configurado por instituição e nível de ensino para que a publicação do trabalho seja realizadano BDP:
                if (publicacaoBibliotecaObrigatoria)
                {
                    //6.1 - Se aluno possuir solicitação de serviço cujo token seja SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU onde  aúltima situação seja da categoria “Novo” ou “Em andamento”
                    //cancelar a solicitação de acordo com a RN_SRC_025–Solicitação–Consistências quando cancelada a solicitação, passando como parâmetro os campos abaixo:
                    //Observação = ‘Solicitação cancelada devido ao lançamento de nota para a tese/ dissertação do aluno’
                    //Motivo = motivo cujo token seja ‘SOLICITACAO_CRIADA_INDEVIDAMENTE’

                    var specSolicitacao = new SolicitacaoServicoFilterSpecification
                    {
                        SeqPessoaAtuacao = lancamentoNotaBancaExaminadoraVO.SeqAluno,
                        TokenServico = TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                        CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.EmAndamento, CategoriaSituacao.Novo }

                    };
                    var solicitacaoRenovacaoAluno = SolicitacaoServicoDomainService.SearchByKey(specSolicitacao);

                    if (solicitacaoRenovacaoAluno != null)
                    {
                        var model = new CancelamentoSolicitacaoVO();

                        model.Observacao = "Solicitação cancelada devido ao lançamento de nota para a tese/dissertação do aluno";
                        model.TokenMotivoCancelamento = TOKEN_MOTIVO_SITUACAO_CANCELAMENTO.SOLICITACAO_CRIADA_INDEVIDAMENTE;
                        model.SeqSolicitacaoServico = solicitacaoRenovacaoAluno.Seq;

                        SolicitacaoServicoDomainService.SalvarCancelamentoSolicitacao(model);
                    }

                    if (lancamentoNotaBancaExaminadoraVO.SeqAluno.HasValue)
                    {
                        var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(lancamentoNotaBancaExaminadoraVO.SeqAluno.Value);

                        // Ciclo Letivo* Encontrar o ciclo letivo de acordo com a RN_CAM_029 -Retorna ciclo letivo, passando como parâmetro: -Tipo de evento(obrigatório): Periodo ciclo letivo
                        //- Data de referência(obrigatório): data de defesa
                        //- Curso oferta localidade turno: Oferta de curso localidade turno do aluno em questão
                        //- Tipo Aluno: tipo de aluno do aluno em questão
                        var cicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(lancamentoNotaBancaExaminadoraVO.DataAvaliacao.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                        //OBSERVAÇÃO: se não existir ciclo letivo posterior ao ciclo letivo da data de defesa, não executar as regras abaixo(6.2 e 6.3):
                        if (cicloLetivo != null)
                        {
                            var ciclosAluno = CicloLetivoDomainService.BuscarCiclosLetivosPorAlunoLancamentoNota(lancamentoNotaBancaExaminadoraVO.SeqAluno.Value);
                            if (ciclosAluno != null && ciclosAluno.Count() > 0)
                            {
                                var listaCiclosMaiores = ciclosAluno.Where(x => long.Parse(x.AnoNumeroCicloLetivo) > long.Parse(cicloLetivo.AnoNumeroCicloLetivo));

                                foreach (var ciclo in listaCiclosMaiores)
                                {

                                    var specAlunoHistorico = new AlunoHistoricoSituacaoFilterSpecification
                                    {
                                        SeqCicloLetivo = ciclo.Seq,
                                        Excluido = false,
                                        SeqPessoaAtuacaoAluno = lancamentoNotaBancaExaminadoraVO.SeqAluno.Value
                                    };

                                    var situacoesAluno = AlunoHistoricoSituacaoDomainService.SearchBySpecification(specAlunoHistorico);

                                    //6.2 - Se existir alguma situação de matrícula, sem data de exclusão, no ciclo letivo posterior ao ciclo letivo* da data da defesa.
                                    //Setar os valores abaixo para todas as situações encontradas:
                                    //Data de exclusão: data atual(do sistema)
                                    //Usuário de exclusão: usuário logado
                                    //Descrição observação exclusão: "Excluída devido ao lançamento de nota para a tese/dissertação do aluno"
                                    if (situacoesAluno.Any())
                                    {
                                        foreach (var situacao in situacoesAluno)
                                        {
                                            situacao.DataExclusao = DateTime.Now;
                                            situacao.UsuarioExclusao = SMCContext.User.Identity.Name;
                                            situacao.ObservacaoExclusao = "Excluída devido ao lançamento de nota para a tese/dissertação do aluno";

                                            AlunoHistoricoSituacaoDomainService.UpdateEntity(situacao);
                                        }
                                    }

                                    //6.3.Se existir plano de estudos ATUAL, no ciclo letivo posterior ao ciclo letivo* da data de defesa:
                                    var specPlanoEstudo = new PlanoEstudoFilterSpecification
                                    {
                                        Atual = true,
                                        SeqCicloLetivo = ciclo.Seq
                                    };

                                    var planoEstudo = PlanoEstudoDomainService.SearchProjectionByKey(specPlanoEstudo, x=> new 
                                    {
                                        Seq = x.Seq,
                                        Itens = x.Itens
                                    });
                                    if (planoEstudo != null)
                                    {

                                        //6.3.1.Para cada divisão de turma existente no plano de estudos, subtrair 1 da qtd_vagas_ocupadas.
                                        var divisoesTurma = DivisaoTurmaDomainService.BuscarDivisoesTurmaCicloAtualAluno(lancamentoNotaBancaExaminadoraVO.SeqAluno.Value);

                                        foreach (var divisaoTurma in divisoesTurma)
                                            if (divisaoTurma != null)
                                                DivisaoTurmaDomainService.AtualizarQuantidadeVagasOcupadas(divisaoTurma.Seq, (int)divisaoTurma.QuantidadeVagasOcupadas--);

                                        //6.3.2.Caso algum item do plano de estudos possua orientação associada, setar no campo data fim dos colaboradores associados à orientação em questão, a data atual(do sistema).
                                        //6.3.3.Alterar o indicador de atual do plano de estudos para o valor não.
                                        //6.3.4. .Incluir um novo registro de plano de estudos sem item, de acordo com a regra RN_MAT_112 -Inclusão plano de estudo sem item, passando como parâmetro:
                                        //Ciclo letivo: ciclo letivo posterior ao ciclo letivo da data de defesa
                                        //Oferta de matriz: oferta de matriz do plano de estudo em questão;
                                        //Observação: “Plano de estudo sem componente curricular, devido ao lançamento de nota para a tese / dissertação do aluno".
                                        PlanoEstudoDomainService.IncluirNovoPlanoEstudoSemItem(ciclo.Seq, "Plano de estudo sem componente curricular, devido ao lançamento de nota para a tese/dissertação do aluno", lancamentoNotaBancaExaminadoraVO.SeqAluno.Value);
                                    }
                                }
                            }
                        }
                    }
                }

                unitOfWork.Commit();
            }
            // Retorna o seq de trabalho acadêmico
            return (long)lancamentoNotaBancaExaminadoraVO.SeqTrabalhoAcademico;
        }

        #region [ Validações para Salvar Lançamento de Notas da Banca examinadora ]

        private ApuracaoAvaliacao VincularApuracao(LancamentoNotaBancaExaminadoraVO lancamentoNotaBancaExaminadoraVO, AplicacaoAvaliacao aplicacaoAvaliacao)
        {
            var apuracao = BuscarApuracao(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value);

            if (apuracao != null)
            {
                apuracao.Nota = lancamentoNotaBancaExaminadoraVO.Nota;
                apuracao.NumeroDefesa = lancamentoNotaBancaExaminadoraVO.NumeroDefesa;
                apuracao.SeqAplicacaoAvaliacao = lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value;
                apuracao.SeqEscalaApuracaoItem = lancamentoNotaBancaExaminadoraVO.SeqEscalaApuracaoItem;

                // Se o arquivo do logotipo não foi alterado, atualiza com conteúdo com o que está no banco
                apuracao.ArquivoAnexadoAtaDefesa = lancamentoNotaBancaExaminadoraVO.ArquivoAnexadoAtaDefesa.Transform<ArquivoAnexado>();
                this.EnsureFileIntegrity(apuracao, x => x.SeqArquivoAnexadoAtaDefesa, x => x.ArquivoAnexadoAtaDefesa);

                aplicacaoAvaliacao.ApuracoesAvaliacao.Clear();
                aplicacaoAvaliacao.ApuracoesAvaliacao.Add(apuracao);

                SaveEntity(apuracao);

                // FIX: Tive que fazer isso pois não estava atualizando o seq do arquivo por algum motivo maluco do graphdiff
                if (apuracao.ArquivoAnexadoAtaDefesa != null)
                    apuracao.SeqArquivoAnexadoAtaDefesa = apuracao.ArquivoAnexadoAtaDefesa.Seq;

                return apuracao;
            }

            apuracao = new ApuracaoAvaliacao
            {
                Nota = lancamentoNotaBancaExaminadoraVO.Nota,
                NumeroDefesa = lancamentoNotaBancaExaminadoraVO.NumeroDefesa,
                ArquivoAnexadoAtaDefesa = lancamentoNotaBancaExaminadoraVO.ArquivoAnexadoAtaDefesa.Transform<ArquivoAnexado>(),
                SeqAplicacaoAvaliacao = lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value,
                SeqEscalaApuracaoItem = lancamentoNotaBancaExaminadoraVO.SeqEscalaApuracaoItem,
                Comparecimento = true
            };

            // Atribuir a apuração a aplicação da avaliação
            aplicacaoAvaliacao.ApuracoesAvaliacao.Add(apuracao);

            return apuracao;
        }

        private long AtualizarHistoricoAluno(LancamentoNotaBancaExaminadoraVO lancamentoNotaBancaExaminadoraVO)
        {
            // Busca o critério de aprovação
            var criterioAprovacao = AplicacaoAvaliacaoDomainService.SearchProjectionByKey(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value, x => x.OrigemAvaliacao.DivisoesComponente.FirstOrDefault().DivisaoComponente.ConfiguracaoComponente.DivisoesMatrizCurricularComponente.FirstOrDefault().CriterioAprovacao.Seq);

            // Busca o Aluno responsável pelo Trabalho Acadêmico
            lancamentoNotaBancaExaminadoraVO.SeqAluno = TrabalhoAcademicoDomainService.BuscarSeqAlunoTrabalhoAcademico(lancamentoNotaBancaExaminadoraVO.SeqTrabalhoAcademico.Value);

            //Busca informações do curso do aluno responsável pelo trabalho acadêmico
            var cursoAluno = CursoDomainService.BuscarCursoDoAluno(lancamentoNotaBancaExaminadoraVO.SeqAluno.Value);

            var alunoHistoricoAtual = AlunoHistoricoDomainService.BuscarHistoricoAtualAluno(lancamentoNotaBancaExaminadoraVO.SeqAluno.Value, Common.Areas.ALN.Includes.IncludesAlunoHistorico.Nenhum);
            var historicosEscolares = AplicacaoAvaliacaoDomainService.SearchProjectionByKey(lancamentoNotaBancaExaminadoraVO.SeqAplicacaoAvaliacao.Value, x => x.OrigemAvaliacao.HistoricosEscolares);

            // Action para atualizar o histórico
            Action<HistoricoEscolar> actUpdate = (HistoricoEscolar historicoEscolar) =>
            {
                historicoEscolar.Nota = lancamentoNotaBancaExaminadoraVO.Nota;
                historicoEscolar.SeqEscalaApuracaoItem = lancamentoNotaBancaExaminadoraVO.SeqEscalaApuracaoItem;
                historicoEscolar.SeqOrigemAvaliacao = lancamentoNotaBancaExaminadoraVO.SeqOrigemAvaliacao;
                historicoEscolar.SeqCriterioAprovacao = criterioAprovacao;
                historicoEscolar.SituacaoHistoricoEscolar = lancamentoNotaBancaExaminadoraVO.CriterioAprovacaoAprovado.GetValueOrDefault() ? SituacaoHistoricoEscolar.Aprovado : SituacaoHistoricoEscolar.Reprovado;
                historicoEscolar.SeqAlunoHistorico = alunoHistoricoAtual.Seq;

                historicoEscolar.SeqCicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(lancamentoNotaBancaExaminadoraVO.DataAvaliacao.GetValueOrDefault(), cursoAluno.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                var componenteCurricular = ComponenteCurricularDomainService.SearchByKey(new SMCSeqSpecification<ComponenteCurricular>(lancamentoNotaBancaExaminadoraVO.SeqComponenteCurricular.Value));
                historicoEscolar.Credito = componenteCurricular.Credito;
                historicoEscolar.SeqComponenteCurricular = componenteCurricular.Seq;

                historicoEscolar.Colaboradores = historicoEscolar.Colaboradores ?? new List<HistoricoEscolarColaborador>();
                foreach (var colaborador in lancamentoNotaBancaExaminadoraVO.MembrosBancaExaminadora.Where(m => m.Participou.Value == true))
                {
                    historicoEscolar.Colaboradores.Add(new HistoricoEscolarColaborador()
                    {
                        SeqColaborador = colaborador.SeqColaborador,
                        NomeColaborador = colaborador.NomeColaborador,
                        SeqInstituicaoExterna = colaborador.SeqInstituicaoExterna,
                        NomeInstituicaoExterna = colaborador.NomeInstituicaoExterna,
                        SeqHistoricoEscolar = historicoEscolar.Seq,
                        TipoMembroBanca = colaborador.TipoMembroBanca,
                        DescricaoComplementoInstituicao = colaborador.ComplementoInstituicao
                    });
                }

                HistoricoEscolarDomainService.SaveEntity(historicoEscolar);
            };

            if (historicosEscolares == null || !historicosEscolares.Any())
            {
                var historicoEscolar = new HistoricoEscolar
                {
                    Faltas = null,
                    PercentualFrequencia = null,
                    CargaHorariaRealizada = null,
                    Optativa = false,
                    Colaboradores = new List<HistoricoEscolarColaborador>()
                };
                actUpdate(historicoEscolar);
            }
            else
            {
                foreach (var historicoEscolar in historicosEscolares)
                    actUpdate(historicoEscolar);
            }

            return alunoHistoricoAtual.Seq;
        }

        #endregion [ Validações para Salvar Lançamento de Notas da Banca examinadora ]

        #endregion [ Salvar Lançamento de Notas da Banca examinadora ]

        #region [ Excluir Lançamento de Nota ]

        /// <summary>
        /// Caso o usuário clique em "Sim":
        /// Se houver somente uma avaliação com apuração · registrada para o componente curricular em questão:
        ///     · Excluir a apuração registrada para a avaliação.
        ///     · Excluir do histórico escolar do aluno o registro relativo ao componente curricular em questão.
        /// Se houver mais de uma avaliação com apuração registrada para o componente curricular em questão:
        ///     · Atualizar no histórico escolar do aluno o registro relativo ao componente curricular em questão com a
        ///     nota/conceito da avaliação com apuração registrada que não será excluída
        ///     · Excluir a apuração registrada para a avaliação.
        /// · Excluir o registro de publicação do trabalho no BDP e a situação deste associada.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao"></param>
        public void ExcluirNotaLancadaApuracaoAvaliacao(long seqAplicacaoAvaliacao)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                var dados = AplicacaoAvaliacaoDomainService.SearchProjectionByKey(seqAplicacaoAvaliacao, x => new
                {
                    MembrosBancaExaminadora = x.MembrosBancaExaminadora.ToList(),
                    Apuracoes = x.ApuracoesAvaliacao.Select(a => a.Seq),
                    HistoricosEscolares = x.OrigemAvaliacao.HistoricosEscolares.ToList(),
                    OutrasAvaliacoes = x.OrigemAvaliacao.AplicacoesAvaliacao.Where(a =>
                                       a.Seq != seqAplicacaoAvaliacao && !a.DataCancelamento.HasValue &&
                                       (a.ApuracoesAvaliacao.FirstOrDefault().Nota.HasValue || a.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem.HasValue))
                                       .Select(a => new
                                       {
                                           a.Seq,
                                           a.ApuracoesAvaliacao.FirstOrDefault().Nota,
                                           a.ApuracoesAvaliacao.FirstOrDefault().SeqEscalaApuracaoItem,
                                           a.MembrosBancaExaminadora,
                                           a.SeqOrigemAvaliacao,
                                           a.DataInicioAplicacaoAvaliacao
                                       }),
                });

                // Valido os registros
                if (dados == null) { throw new LancamentoNotaBancaExaminadoraApuracaoAvaliacaoNaoExisteException(); }

                // Tem algum histórico?
                if (dados != null && dados.HistoricosEscolares.Any())
                {
                    foreach (var historico in dados.HistoricosEscolares)
                    {
                        // Tem outra avaliação? Se sim, ela é reprovada. Atualiza o histórico. Se não, deleta o histórico
                        if (!dados.OutrasAvaliacoes.Any())
                            HistoricoEscolarDomainService.DeleteEntity(historico.Seq);
                        else
                        {
                            //Busca informações do curso do aluno responsável pelo trabalho acadêmico
                            var cursoAluno = CursoDomainService.BuscarCursoDoAluno(historico.SeqPessoaAtuacao);
                            var avaliacaoUtilizar = dados.OutrasAvaliacoes.OrderByDescending(o => o.Seq).FirstOrDefault();
                            var seqCicloLetivo = ConfiguracaoEventoLetivoDomainService.BuscarSeqCicloLetivo(avaliacaoUtilizar.DataInicioAplicacaoAvaliacao, cursoAluno.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault(), null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                            historico.SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Reprovado;
                            historico.Nota = avaliacaoUtilizar.Nota;
                            historico.SeqEscalaApuracaoItem = avaliacaoUtilizar.SeqEscalaApuracaoItem;
                            historico.SeqOrigemAvaliacao = avaliacaoUtilizar.SeqOrigemAvaliacao;
                            historico.SeqCicloLetivo = seqCicloLetivo;
                            historico.Colaboradores = avaliacaoUtilizar.MembrosBancaExaminadora.Select(m => new HistoricoEscolarColaborador()
                            {
                                SeqColaborador = m.SeqColaborador,
                                NomeColaborador = m.NomeColaborador,
                                SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                                NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                                SeqHistoricoEscolar = historico.Seq,
                                TipoMembroBanca = m.TipoMembroBanca,
                                DescricaoComplementoInstituicao = m.ComplementoInstituicao
                            }).ToList();

                            HistoricoEscolarDomainService.SaveEntity(historico);

                            /*
                            // Alguem alterou para editar o historico escolar salvando apenas algumas propriedades, dai estava sendo zerada as outras...
                            // Mudei para pegar o objeto historico, alterar nele e salvar.
                            HistoricoEscolarDomainService.SaveEntity(new HistoricoEscolar
                            {
                                Seq = historico.SeqHistoricoEscolar,
                                SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.Reprovado,
                                Nota = avaliacaoUtilizar.Nota,
                                SeqEscalaApuracaoItem = avaliacaoUtilizar.SeqEscalaApuracaoItem,
                                SeqOrigemAvaliacao = avaliacaoUtilizar.SeqOrigemAvaliacao,
                                SeqCicloLetivo = seqCicloLetivo,
                                Colaboradores = avaliacaoUtilizar.MembrosBancaExaminadora.Select(m => new HistoricoEscolarColaborador()
                                {
                                    SeqColaborador = m.SeqColaborador,
                                    NomeColaborador = m.NomeColaborador,
                                    SeqInstituicaoExterna = m.SeqInstituicaoExterna,
                                    NomeInstituicaoExterna = m.NomeInstituicaoExterna,
                                    SeqHistoricoEscolar = historico.SeqHistoricoEscolar,
                                    TipoMembroBanca = m.TipoMembroBanca,
                                    DescricaoComplementoInstituicao = m.ComplementoInstituicao
                                }).ToList()
                            });*/
                        }
                    }
                }

                // Excluo o registro de apuração da avaliação
                foreach (var apuracao in dados.Apuracoes)
                    this.DeleteEntity(apuracao);

                // Busco o registro de publicação do trabalho acadêmico
                var publicacaoBdp = BuscarPublicacaoBDPAvaliacao(seqAplicacaoAvaliacao);

                if (publicacaoBdp != null)
                {
                    // Excluo o registro de publicação do trabalho acadêmico
                    PublicacaoBdpDomainService.DeleteEntity(publicacaoBdp);
                }

                //Ao "Excluir nota lançada", atualizar o atributo "ind_participou" com o valor nulo, para todos os membros registrados na banca.
                dados.MembrosBancaExaminadora.ForEach(m =>
                {
                    m.Participou = null;
                    MembroBancaExaminadoraDomainService.UpdateFields(m, w => w.Participou);
                });

                //Confirmo a transação
                unitOfWork.Commit();
            }
        }

        private PublicacaoBdp BuscarPublicacaoBDPAvaliacao(long seqAplicacaoAvaliacao)
        {
            return AplicacaoAvaliacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>(seqAplicacaoAvaliacao)
                     , x => x.OrigemAvaliacao.DivisoesComponente.Select(t => t.TrabalhoAcademico.PublicacaoBdp).FirstOrDefault()).FirstOrDefault();
        }

        private ApuracaoAvaliacao BuscarApuracao(long seqAplicacaoAvaliacao)
        {
            return AplicacaoAvaliacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AplicacaoAvaliacao>
                   (seqAplicacaoAvaliacao), x => x.ApuracoesAvaliacao.FirstOrDefault());
        }

        #endregion [ Excluir Lançamento de Nota ]

        /// <summary>
        /// Busca todas aplicações de avaliação numa turma para uma origem
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <param name="seqProfessor">Sequencial do professor logado</param>
        /// <param name="administrativo">Indicativo que o relaótio foi solicitado pela secretraria</param>
        /// <returns>Dados para o lançamento</returns>
        public LancamentoAvaliacaoVO BuscarLancamentosAvaliacao(long seqOrigemAvaliacao, long seqProfessor, bool administrativo = false)
        {
            var dicSituacao = SMCEnumHelper.GenerateKeyValuePair<SituacaoHistoricoEscolar>()
                .ToDictionary(k => k.Value, v => v.Key);
            dicSituacao.Add("", SituacaoHistoricoEscolar.Nenhum);
            var dadosOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao, p => new
            {
                p.Descricao,
                p.TipoOrigemAvaliacao
            });
            long seqOrigemAvaliacaoTurma;
            long seqTurma;
            long seqCicloLetivoTurma;
            long? seqDivisaoTurma = null;
            var descricaoOrigemAvaliacao = dadosOrigemAvaliacao.Descricao;
            long seqComponenteCurricularTurma;
            long? seqComponenteCurricularAssuntoTurma;
            if (string.IsNullOrEmpty(descricaoOrigemAvaliacao))
            {
                descricaoOrigemAvaliacao = OrigemAvaliacaoDomainService.BuscarDescricaoOrigemAvaliacao(seqOrigemAvaliacao);
            }

            var lancamentoAvaliacao = OrigemAvaliacaoDomainService
                .SearchProjectionByKey(seqOrigemAvaliacao, p => new LancamentoAvaliacaoVO()
                {
                    SeqOrigemAvaliacao = p.Seq,
                    ApuracaoFrequencia = p.CriterioAprovacao.ApuracaoFrequencia,
                    ApuracaoNota = p.CriterioAprovacao.ApuracaoNota,
                    MateriaLecionada = p.MateriaLecionada,
                    DescricaoOrigemAvaliacao = descricaoOrigemAvaliacao,
                    AplicacaoAvaliacoes = p.AplicacoesAvaliacao.Select(s => new AplicacaoAvaliacaoVO()
                    {
                        Seq = s.Seq,
                        Avaliacao = new AvaliacaoVO()
                        {
                            Seq = s.SeqAvaliacao,
                            Descricao = s.Avaliacao.Descricao,
                            Valor = s.Avaliacao.Valor
                        },
                        Sigla = s.Sigla,
                        EntregaWeb = s.EntregaWeb
                    }).OrderBy(o => o.Sigla).ToList()
                });

            if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                var specDivisaoTurma = new DivisaoTurmaFilterSpecification()
                {
                    SeqOrigemAvaliacao = seqOrigemAvaliacao
                };
                var dadosOrigemAvaliacaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisaoTurma, p => new
                {
                    p.Seq,
                    p.SeqTurma,
                    p.Turma.SeqOrigemAvaliacao,
                    p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.PermiteAlunoSemNota,
                    Orientacao = p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.DivisoesComponente.Any(a => a.TipoDivisaoComponente.GeraOrientacao),
                    ResponsavelTurma = p.Turma.Colaboradores.Any(a => a.SeqColaborador == seqProfessor),
                    p.Turma.OrigemAvaliacao.CriterioAprovacao.ApuracaoFrequencia,
                    p.Turma.OrigemAvaliacao.CriterioAprovacao.ApuracaoNota,
                    DiarioFechado = p.Turma.HistoricosFechamentoDiario.Count > 0 ? p.Turma.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                    p.OrigemAvaliacao.MateriaLecionadaObrigatoria,
                    p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.SeqComponenteCurricular,
                    p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                    SeqCicloLetivoTurma = p.Turma.SeqCicloLetivoInicio
                }).FirstOrDefault();
                seqTurma = dadosOrigemAvaliacaoTurma.SeqTurma;
                seqCicloLetivoTurma = dadosOrigemAvaliacaoTurma.SeqCicloLetivoTurma;
                seqDivisaoTurma = dadosOrigemAvaliacaoTurma.Seq;
                seqOrigemAvaliacaoTurma = dadosOrigemAvaliacaoTurma.SeqOrigemAvaliacao;
                seqComponenteCurricularAssuntoTurma = dadosOrigemAvaliacaoTurma.SeqComponenteCurricularAssunto;
                seqComponenteCurricularTurma = dadosOrigemAvaliacaoTurma.SeqComponenteCurricular;
                lancamentoAvaliacao.Orientacao = dadosOrigemAvaliacaoTurma.Orientacao;
                lancamentoAvaliacao.OrigemAvaliacaoTurma = false;
                lancamentoAvaliacao.ResponsavelTurma = dadosOrigemAvaliacaoTurma.ResponsavelTurma;
                lancamentoAvaliacao.DiarioFechado = dadosOrigemAvaliacaoTurma.DiarioFechado;
                lancamentoAvaliacao.ApuracaoFrequencia = dadosOrigemAvaliacaoTurma.ApuracaoFrequencia;
                lancamentoAvaliacao.ApuracaoNota = dadosOrigemAvaliacaoTurma.ApuracaoNota;
                lancamentoAvaliacao.PermiteAlunoSemNota = dadosOrigemAvaliacaoTurma.PermiteAlunoSemNota;
                lancamentoAvaliacao.PermiteMateriaLecionada = true;
                lancamentoAvaliacao.MateriaLecionadaObrigatoria = (bool)dadosOrigemAvaliacaoTurma.MateriaLecionadaObrigatoria.GetValueOrDefault();
            }
            else
            {
                var specTurma = new TurmaFilterSpecification()
                {
                    SeqOrigemAvaliacao = seqOrigemAvaliacao
                };
                var dadosTurma = TurmaDomainService.SearchProjectionBySpecification(specTurma, p => new
                {
                    p.Seq,
                    p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.PermiteAlunoSemNota,
                    Orientacao = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.DivisoesComponente.Any(a => a.TipoDivisaoComponente.GeraOrientacao),
                    ResponsavelTurma = p.Colaboradores.Any(a => a.SeqColaborador == seqProfessor),
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
                    MateriaLecionadaCadastrada = p.DivisoesTurma.Where(w => w.OrigemAvaliacao.MateriaLecionadaObrigatoria == true && string.IsNullOrEmpty(w.OrigemAvaliacao.MateriaLecionada.Trim())).Any(),
                    p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.SeqComponenteCurricular,
                    p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                    SeqCicloLetivoTurma = p.SeqCicloLetivoInicio
                }).FirstOrDefault();
                seqTurma = dadosTurma.Seq;
                seqCicloLetivoTurma = dadosTurma.SeqCicloLetivoTurma;
                seqOrigemAvaliacaoTurma = seqOrigemAvaliacao;
                seqComponenteCurricularTurma = dadosTurma.SeqComponenteCurricular;
                seqComponenteCurricularAssuntoTurma = dadosTurma.SeqComponenteCurricularAssunto;
                lancamentoAvaliacao.Orientacao = dadosTurma.Orientacao;
                lancamentoAvaliacao.OrigemAvaliacaoTurma = true;
                lancamentoAvaliacao.ResponsavelTurma = dadosTurma.ResponsavelTurma;
                lancamentoAvaliacao.DiarioFechado = dadosTurma.DiarioFechado;
                lancamentoAvaliacao.PermiteAlunoSemNota = dadosTurma.PermiteAlunoSemNota;
                lancamentoAvaliacao.PermiteMateriaLecionada = false;
                lancamentoAvaliacao.MateriaLecionadaObrigatoria = false;
                lancamentoAvaliacao.MateriaLecionadaCadastrada = dadosTurma.MateriaLecionadaCadastrada;

                if (lancamentoAvaliacao.MateriaLecionadaCadastrada)
                    lancamentoAvaliacao.DescricoesDivisaoTurma = DivisaoTurmaDomainService.DescricoesDivisaoTurma(dadosTurma.Seq);
            }

            // Validação de dados, nunca deveria ocorrer.
            if (lancamentoAvaliacao.ApuracaoFrequencia == null || lancamentoAvaliacao.ApuracaoNota == null)
            {
                throw new SMCApplicationException("Configuração de critério de aprovação inválida!");
            }

            //var filtroOrientador =
            //    !lancamentoAvaliacao.ResponsavelTurma
            //    && lancamentoAvaliacao.Orientacao
            //    && !lancamentoAvaliacao.OrigemAvaliacaoTurma
            //    ? new List<long>() { seqProfessor } : null;

            //Faz a validação se é para relatorio e não for o professor responsavel irá buscar todos os professores da divisão
            //para no caso de ser divisão de uma turma de orientação assim trazer todos os alunos da divisão
            List<long> filtroOrientador = new List<long>();
            if (!lancamentoAvaliacao.ResponsavelTurma && lancamentoAvaliacao.Orientacao && !lancamentoAvaliacao.OrigemAvaliacaoTurma && !administrativo)
            {
                filtroOrientador.Add(seqProfessor);
            }
            else if (!lancamentoAvaliacao.ResponsavelTurma && lancamentoAvaliacao.Orientacao && !lancamentoAvaliacao.OrigemAvaliacaoTurma && administrativo)
            {
                filtroOrientador.AddRange(OrigemAvaliacaoDomainService.BuscarProfessoresPorOrigemAvaliacao(seqOrigemAvaliacao)
                                                                                .Select(s => s.SeqColaborador).ToList());
            }

            var specApuracao = new ApuracaoAvaliacaoFilterSpecification()
            {
                SeqsAplicacaoAvaliacao = lancamentoAvaliacao.AplicacaoAvaliacoes.Select(s => s.Seq).ToList()
            };
            var apuracoesAvaliacao = SearchProjectionBySpecification(specApuracao, p => new
            {
                p.Seq,
                p.SeqAplicacaoAvaliacao,
                p.SeqAlunoHistorico,
                p.Nota,
                p.Comparecimento,
                p.ComentarioApuracao,
                EntregasOnline = p.AplicacaoAvaliacao.EntregasOnline.Select(s => new EntregaOnlineVO
                {
                    Seq = s.Seq,
                    SeqAplicacaoAvaliacao = s.SeqAplicacaoAvaliacao,
                    SituacaoEntrega = s.SituacaoAtual.SituacaoEntregaOnline,
                    DataEntrega = s.DataEntrega
                }).ToList(),
            }).ToList();

            var alunosDiario = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma, null, filtroOrientador);
            lancamentoAvaliacao.Alunos = alunosDiario.Select(s => new LancamentoAvaliacaoAlunosVO()
            {
                SeqAlunoHistorico = s.SeqAlunoHistorico,
                NumeroRegistroAcademico = s.NumeroRegistroAcademico,
                Nome = s.NomeAluno, // Já implementa a RN_PES_037 - Nome e Nome Social - Visão Aluno
                Faltas = s.SomaFaltasApuracao ?? 0,
                Formado = s.AlunoFormado,
                AlunoAprovado = s.AlunoAprovado,
                AlunoDispensado = s.AlunoDispensado,
                Nota = s.Nota,
                SituacaoFinal = dicSituacao[s.DescricaoSituacaoHistoricoEscolar ?? ""],
                DescricaoSituacaoFinal = s.DescricaoSituacaoHistoricoEscolar,
            }).ToList();

            foreach (var aluno in lancamentoAvaliacao.Alunos)
            {
                aluno.Nome = aluno.Nome.Trim().ToUpper();
                aluno.Apuracoes = new List<LancamentoAvaliacaoAlunoApuracaoVO>();

                foreach (var aplicacaoAvaliacao in lancamentoAvaliacao.AplicacaoAvaliacoes)
                {
                    var apuracao = apuracoesAvaliacao
                        .FirstOrDefault(f => f.SeqAlunoHistorico == aluno.SeqAlunoHistorico
                                          && f.SeqAplicacaoAvaliacao == aplicacaoAvaliacao.Seq);
                    var apuracaoAluno = new LancamentoAvaliacaoAlunoApuracaoVO()
                    {
                        Seq = apuracao?.Seq ?? 0,
                        SeqAlunoHistorico = aluno.SeqAlunoHistorico,
                        SeqAplicacaoAvaliacao = aplicacaoAvaliacao.Seq,
                        Nota = apuracao?.Nota,
                        // Assume como true caso ainda não exista uma Apuração para que a tela não assuma como não comparecido
                        Comparecimento = apuracao?.Comparecimento ?? true,
                        //no mesmo componente + assunto ou estiver matriculado em outro ciclo letivo e ainda estiver sem nota final no historico - escolar
                        AlunoComComponenteOutroHistorico = ValidarPermitirLancacarNotaAlunoAtriculado(aluno.SeqAlunoHistorico,
                                                                                                        seqComponenteCurricularTurma,
                                                                                                        seqCicloLetivoTurma,
                                                                                                        seqComponenteCurricularAssuntoTurma),
                        //Permitir lançamento de nota pra entrega online se aluno não tiver feito entrega e se o prazo para entrega estiver finalizado
                        PermitirAlunoEntregarOnlinePosPrazo = aplicacaoAvaliacao.EntregaWeb ? ValidarPermitirLancarNotaOnline(aplicacaoAvaliacao.Seq, aluno.SeqAlunoHistorico) : false,
                    };
                    aluno.Apuracoes.Add(apuracaoAluno);
                }
            }
            lancamentoAvaliacao.Alunos = lancamentoAvaliacao.Alunos.OrderBy(o => o.Nome).ToList();
            return lancamentoAvaliacao;
        }

        private bool ValidarPermitirLancarNotaOnline(long seqAplicacaoAvaliacao, long seqAlunoHistorico)
        {

            var aplicacaoAvaliacao = this.AplicacaoAvaliacaoDomainService.SearchProjectionByKey(seqAplicacaoAvaliacao, p => new
            {
                p.DataFimAplicacaoAvaliacao
            });

            var specEntregaOnlineParticipante = new EntregaOnlineParticipanteFilterSpecification() { SeqAlunoHistorico = seqAlunoHistorico, SeqAplicacaoAvaliacao = seqAplicacaoAvaliacao };
            var entregaOnlineParticipante = this.EntregaOnlineParticipanteDomainService.SearchProjectionBySpecification(specEntregaOnlineParticipante, p => new
            {
                p.EntregaOnline.DataEntrega,
            }).ToList();

            bool retorno = false;

            if (!entregaOnlineParticipante.SMCAny() && (!aplicacaoAvaliacao.DataFimAplicacaoAvaliacao.HasValue || DateTime.Now > aplicacaoAvaliacao.DataFimAplicacaoAvaliacao))
            {
                retorno = true;
            }

            return retorno;
        }

        private bool ValidarPermitirLancacarNotaAlunoAtriculado(long seqAlunoHistorico, long seqComponenteCurricularTurma, long seqCicloLetivoTurma, long? seqComponenteCurricularAssuntoTurma)
        {
            bool retorno = false;
            //Não permitir salvar nota se aluno estiver aprovado ou dispensado no mesmo componente + assunto ou estiver matriculado
            //em outro ciclo letivo e ainda estiver sem nota final no historico - escolar
            //barrar se
            //aln.aluno_historico_ciclo_letivo com anteriores
            //plano_estudo ind_atual = 1
            //plano_estudo_item com mesmo componente assunto
            //historico_escolar com mesmo componente e assunto
            long seqAluno = this.AlunoHistoricoDomainService.SearchProjectionByKey(seqAlunoHistorico, p => p.SeqAluno);

            //Bucar o ciclo letivo da turma
            var cicloLetivo = this.CicloLetivoDomainService.SearchByKey(seqCicloLetivoTurma);
            var query = string.Format(_ciclosLetivosAnterios, cicloLetivo.AnoNumeroCicloLetivo);
            //Buscar todos os ciclos letivos anteriores refenrente ao atual
            var seqsCiclosLetivosAnteriores = RawQuery<CicloLetivoVO>(query).Select(s => s.Seq).ToList();

            //Buscar todos os planos de estudos anteriores com com suas turmas
            var specPlanoEstudo = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = seqAluno,
                CicloLetivoDiferente = seqCicloLetivoTurma,
                Atual = true
            };

            var planosEstudosDiferentes = this.PlanoEstudoDomainService.SearchProjectionBySpecification(specPlanoEstudo, p => new
            {
                p.Seq,
                SeqAlunoHistorico = p.AlunoHistoricoCicloLetivo.SeqAlunoHistorico,
                Itens = p.Itens.Select(s => new
                {
                    s.SeqDivisaoTurma,
                    SeqTurma = s.SeqDivisaoTurma == null ? 0 : s.DivisaoTurma.SeqTurma
                }).ToList()
            }).ToList();

            //Percorre todas a turmas anteriores
            foreach (var plano in planosEstudosDiferentes)
            {
                //Percorre todas as turmas que validando se a mesma tem os mesmo componetes e assunto 
                foreach (var item in plano.Itens)
                {
                    //Valida se por ventura tem divisão para aquele item do plano de estudo
                    if (item.SeqDivisaoTurma == null)
                    {
                        continue;
                    }
                    var specTurma = new TurmaFilterSpecification()
                    {
                        Seq = item.SeqTurma,
                        SeqComponenteCurricular = seqComponenteCurricularTurma,
                        SeqComponenteCurricularAssunto = seqComponenteCurricularAssuntoTurma
                    };
                    var turma = this.TurmaDomainService.SearchBySpecification(specTurma).ToList();
                    //Caso tenha valida se a mesma ja consta com historico escolar
                    if (turma.SMCAny())
                    {
                        var specHistorico = new HistoricoEscolarFilterSpecification()
                        {
                            SeqComponenteCurricular = seqComponenteCurricularTurma,
                            SeqComponenteCurricularAssunto = seqComponenteCurricularAssuntoTurma,
                            SeqAlunoHistorico = plano.SeqAlunoHistorico,
                            SeqCicloLetivoDiferente = seqCicloLetivoTurma
                        };
                        var componenteComHistorico = this.HistoricoEscolarDomainService.SearchBySpecification(specHistorico).ToList();
                        //verifica se o componente encontrado esta aprovado ou dispensado
                        if (componenteComHistorico.SMCAny() && (componenteComHistorico.Any(a => a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Aprovado) ||
                                                                componenteComHistorico.Any(a => a.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)))
                        {
                            retorno = true;
                        }
                    }
                };
            }

            return retorno;
        }

        public void SalvarLancamentosAvaliacao(LancamentoAvaliacaoVO model)
        {
            var origemAvaliacao = new OrigemAvaliacao()
            {
                Seq = model.SeqOrigemAvaliacao,
                MateriaLecionada = model.MateriaLecionada?.Trim()
            };
            OrigemAvaliacaoDomainService.UpdateFields(origemAvaliacao, p => p.MateriaLecionada);

            // Criação / Edição
            if (model.Apuracoes.SMCAny())
            {
                // Recuperar dados de apoio para notificação
                long seqTurma = BuscarSeqTurmaPorOrigemAvaliacao(model.SeqOrigemAvaliacao);
                var dicDadosOrigemPorAlunoHistorico = new Dictionary<long, PessoaAtuacaoDadosOrigemVO>();
                var dicSeqAlunoPorAlunoHistorico = new Dictionary<long, long>();
                var dicDescricaoTurmaPorCurriculo = new Dictionary<long, string>();
                var dicDescricaoTurmaPorHistorico = new Dictionary<long, string>();
                var seqsAlunosHistorico = model.Apuracoes.Select(s => s.SeqAlunoHistorico).Distinct().ToList();
                foreach (var seqAlunoHistorico in seqsAlunosHistorico)
                {
                    var dadosAluno = BuscarDadosOrigemPorAlunoHistorico(seqAlunoHistorico);
                    dicDadosOrigemPorAlunoHistorico.Add(seqAlunoHistorico, dadosAluno.DadosOrigem);
                    dicSeqAlunoPorAlunoHistorico.Add(seqAlunoHistorico, dadosAluno.SeqAluno);
                }
                var seqsMatrizCurricularOferta = dicDadosOrigemPorAlunoHistorico.Values.Select(s => s.SeqMatrizCurricularOferta).Distinct();
                foreach (var seqMatrizCurricularOferta in seqsMatrizCurricularOferta)
                {
                    var descricaoTurma = TurmaDomainService
                        .BuscarDescricaoTurmaConcatenado(seqTurma, seqMatrizCurricularOferta);
                    dicDescricaoTurmaPorCurriculo.Add(seqMatrizCurricularOferta, descricaoTurma);
                }
                foreach (var seqAlunoHistorico in seqsAlunosHistorico)
                {
                    var seqMatrizCurricularOferta = dicDadosOrigemPorAlunoHistorico[seqAlunoHistorico].SeqMatrizCurricularOferta;
                    dicDescricaoTurmaPorHistorico.Add(seqAlunoHistorico, dicDescricaoTurmaPorCurriculo[seqMatrizCurricularOferta]);
                }
                var dicAvaliacoes = OrigemAvaliacaoDomainService
                    .SearchProjectionByKey(model.SeqOrigemAvaliacao, p =>
                        p.AplicacoesAvaliacao.Select(s => new
                        {
                            s.Seq,
                            Descricao = s.Sigla + " - " + s.Avaliacao.Descricao
                        })
                    ).ToDictionary(k => k.Seq, v => v.Descricao);

                foreach (var apuracao in model.Apuracoes)
                {
                    // Criação
                    if (apuracao.Seq == 0)
                    {
                        var modelApuracao = new ApuracaoAvaliacao()
                        {
                            SeqAlunoHistorico = apuracao.SeqAlunoHistorico,
                            SeqAplicacaoAvaliacao = apuracao.SeqAplicacaoAvaliacao,
                            Nota = apuracao.Nota,
                            Comparecimento = apuracao.Comparecimento,
                            ComentarioApuracao = apuracao.ComentarioApuracao
                        };
                        SaveEntity(modelApuracao);
                        EnviarNotificacaoApuracao(
                            dicSeqAlunoPorAlunoHistorico[apuracao.SeqAlunoHistorico],
                            dicDadosOrigemPorAlunoHistorico[apuracao.SeqAlunoHistorico].SeqInstituicaoEnsino,
                            dicDadosOrigemPorAlunoHistorico[apuracao.SeqAlunoHistorico].SeqNivelEnsino,
                            apuracao.Nota?.ToString("n2"),
                            dicDescricaoTurmaPorHistorico[apuracao.SeqAlunoHistorico],
                            dicAvaliacoes[apuracao.SeqAplicacaoAvaliacao]);
                    }
                    // Edição
                    else
                    {
                        var modelApuracao = new ApuracaoAvaliacao()
                        {
                            Seq = apuracao.Seq,
                            Nota = apuracao.Nota,
                            Comparecimento = apuracao.Comparecimento,
                            ComentarioApuracao = apuracao.ComentarioApuracao
                        };
                        UpdateFields(modelApuracao, p => p.Nota,
                                                    p => p.Comparecimento,
                                                    p => p.ComentarioApuracao);
                        EnviarNotificacaoApuracao(
                            dicSeqAlunoPorAlunoHistorico[apuracao.SeqAlunoHistorico],
                            dicDadosOrigemPorAlunoHistorico[apuracao.SeqAlunoHistorico].SeqInstituicaoEnsino,
                            dicDadosOrigemPorAlunoHistorico[apuracao.SeqAlunoHistorico].SeqNivelEnsino,
                            apuracao.Nota?.ToString("n2"),
                            dicDescricaoTurmaPorHistorico[apuracao.SeqAlunoHistorico],
                            dicAvaliacoes[apuracao.SeqAplicacaoAvaliacao]);
                    }
                }
            }
            // Exclusão
            if (model.SeqsApuracaoExculida.SMCAny())
            {
                foreach (var seqApuracao in model.SeqsApuracaoExculida)
                {
                    this.DeleteEntity(seqApuracao);
                }
            }
        }

        private long BuscarSeqTurmaPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            var dadosOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao, p => new
            {
                p.Descricao,
                p.TipoOrigemAvaliacao
            });

            if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                var specDivisaoTurma = new DivisaoTurmaFilterSpecification()
                {
                    SeqOrigemAvaliacao = seqOrigemAvaliacao
                };
                return DivisaoTurmaDomainService
                    .SearchProjectionBySpecification(specDivisaoTurma, p => p.SeqTurma)
                    .FirstOrDefault();
            }
            else
            {
                var specTurma = new TurmaFilterSpecification()
                {
                    SeqOrigemAvaliacao = seqOrigemAvaliacao
                };
                return TurmaDomainService
                    .SearchProjectionBySpecification(specTurma, p => p.Seq)
                    .FirstOrDefault();
            }
        }

        private (long SeqAluno, PessoaAtuacaoDadosOrigemVO DadosOrigem) BuscarDadosOrigemPorAlunoHistorico(long seqAlunoHistorico)
        {
            long seqAluno = AlunoHistoricoDomainService.SearchProjectionByKey(seqAlunoHistorico,
                p => p.SeqAluno);
            return (seqAluno, PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno));
        }

        private void EnviarNotificacaoApuracao(long seqAluno, long seqInstituicaoEnsino, long seqNivelEnsino, string nota, string descricaoTurma, string descricaoAvaliacao)
        {
            /* RN_APR_043 - Mensagem de nota parcial para a linha do tempo
                Ao salvar (incluir ou alterar) um lançamento de nota, gravar uma mensagem para os alunos da turma em questão cujas notas foram incluídas ou alteradas.

                Filtrar:
                - Tipo de mensagem: com Token "NOTA_PARCIAL"
                - Categoria igual a “Linha do Tempo”

                Gravar:
                - Descrição da mensagem: mensagem padrão parametrizada por instituição e nível para o tipo de mensagem em questão .
                - Data início igual a data da postagem e data fim vazia
                - A data e o usuário de inclusão.
                - TAGs possíveis:
                   {{TURMA}} - utilizar a RN_TUR_025 - Exibição Descrição Turma
                   {{NOTA_AVALIACAO}} - nota lançada para o aluno na avaliação
                   {{AVALIACAO}} - Sigla + ' - ' + descrição da avaliação

                - Controle de origem FUTURO
            */

            // Caso tenha alterado a nota ou criado uma nova apuração, salva na linha do tempo
            Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.NOTA_AVALIACAO, nota);
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.TURMA, descricaoTurma);
            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.AVALIACAO, descricaoAvaliacao);

            MensagemDomainService.EnviarMensagemPessoaAtuacao(seqAluno,
                                                              seqInstituicaoEnsino,
                                                              seqNivelEnsino,
                                                              TOKEN_TIPO_MENSAGEM.NOTA_AVALIACAO,
                                                              CategoriaMensagem.LinhaDoTempo,
                                                              dicTagsMsg);
        }

        /// <summary>
        /// Buscar todas a apuracoes do aluno nas divisões da turma vinculada à origem avaliação informada
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        public List<ApuracaoAvaliacao> BuscarApuracoesDasDivisioesPorAlunoTurma(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            var specDivisaoTurma = new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacaoTurma = seqOrigemAvaliacao };
            var seqsOrigemAvaliacaoTurminhas = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisaoTurma, p => p.SeqOrigemAvaliacao).ToList();
            ApuracaoAvaliacaoFilterSpecification specApuracoes = new ApuracaoAvaliacaoFilterSpecification() { SeqAlunoHistorico = seqAlunoHistorico, SeqsOrigemAvaliacao = seqsOrigemAvaliacaoTurminhas };

            List<ApuracaoAvaliacao> retorno = SearchBySpecification(specApuracoes).ToList();

            return retorno;
        }

        /// <summary>
        /// Busca todas as apurações do aluno na turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        public List<ApuracaoAvaliacaoVO> BuscarApuracoesPorAlunoOrigemAvaliacao(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            ApuracaoAvaliacaoFilterSpecification specApuracoes = new ApuracaoAvaliacaoFilterSpecification() { SeqAlunoHistorico = seqAlunoHistorico, SeqOrigemAvaliacao = seqOrigemAvaliacao };
            var retorno = SearchProjectionBySpecification(specApuracoes, p => new ApuracaoAvaliacaoVO()
            {
                Seq = p.Seq,
                SeqAlunoHistorico = p.SeqAlunoHistorico,
                SeqAplicacaoAvaliacao = p.SeqAplicacaoAvaliacao,
                ComentarioApuracao = p.ComentarioApuracao,
                Comparecimento = p.Comparecimento,
                Nota = p.Nota,
                TipoAvaliacao = p.AplicacaoAvaliacao.Avaliacao.TipoAvaliacao
            }).ToList();

            return retorno;
        }

        /// <summary>
        /// Preenche os históricos dos alunos e fecha o diário da turma
        /// </summary>
        /// <param name="model">Dados para o fechamento</param>
        public void FecharLancamentoDiario(LancamentoNotaFechamentoDiarioVO model)
        {
            TurmaFilterSpecification specTurma = new TurmaFilterSpecification() { SeqOrigemAvaliacao = model.SeqOrigemAvaliacao };
            var turma = TurmaDomainService.SearchProjectionBySpecification(specTurma, p => new { p.Seq }).FirstOrDefault();

            AlunoHistoricoFilterSpecification specAlunos = new AlunoHistoricoFilterSpecification()
            {
                Seqs = model.Alunos.Select(s => s.SeqAlunoHistorico).ToList()
            };
            Dictionary<long, long> dicAlunoHistoricoPessoa = AlunoHistoricoDomainService.SearchProjectionBySpecification(specAlunos, p => new
            {
                p.Seq,
                p.SeqAluno
            }).ToDictionary(k => k.Seq, v => v.SeqAluno);

            //Caso seja um aluno irá validar para atualização de historico caso contrario ira validar para fechar diario para turma            
            if (dicAlunoHistoricoPessoa.Count == 1)
            {
                //Se o aluno já tiver nota/ situação lançada no histórico escolar, 
                //permitir limpar o histórico escolar(excluir o registro) quando as notas de todas as avaliações e reavaliações estiverem nulas
                //--nesse caso não precisa chegar se tem aula não apurada
                long? seqAluno = dicAlunoHistoricoPessoa.FirstOrDefault().Value;

                bool todasNotasNulas = model.Alunos.All(a => a.Nota == null);
                if (!todasNotasNulas)
                {
                    ValidarTurmaPermiteApuracaoFrequencia(turma.Seq, seqAluno);
                }
            }
            else
            {
                ValidarTurmaPermiteApuracaoFrequencia(turma.Seq, null);
            }


            LancamentoHistoricoEscolarVO fechamentoVO = new LancamentoHistoricoEscolarVO
            {
                SeqTurma = turma.Seq,
                SeqOrigemAvaliacao = model.SeqOrigemAvaliacao,
                MateriaLecionada = model.MateriaLecionada,
                Lancamentos = new List<LancamentoHistoricoEscolarDetalhesVO>()
            };
            foreach (var aluno in model.Alunos)
            {
                LancamentoHistoricoEscolarDetalhesVO lancamento = new LancamentoHistoricoEscolarDetalhesVO()
                {
                    SeqAlunoHistorico = aluno.SeqAlunoHistorico,
                    SeqPessoaAtuacao = dicAlunoHistoricoPessoa[aluno.SeqAlunoHistorico],
                    Faltas = Convert.ToInt16(aluno.Faltas),
                    Nota = (short?)aluno.Nota,
                    Observacao = null,
                    SemNota = !aluno.Nota.HasValue,
                    SomaFaltasApuracao = Convert.ToInt16(aluno.Faltas),
                    SituacaoHistoricoEscolar = aluno.SituacaoHistoricoEscolar
                };
                fechamentoVO.Lancamentos.Add(lancamento);
            }

            //Ao salvar histórico escolar(atualizando individualmente ou ao fechar diário) não permitir salvar
            //situação final de um aluno que já tenha aquele componente + assunto aprovado ou dispensado
            //msg: "Operação não permitida. O aluno <RA+nome> não já aprovado ou dispensado neste componente/assunto"
            ValidarAlunosComponenteAsuntoAprovadoDsipensado(model, turma.Seq);

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                long? seqOrientador = OrientacaoDomainService.ValidarOrientadores(model.SeqOrigemAvaliacao, model.SeqProfessor)?.First();
                HistoricoEscolarDomainService.SalvarLancamentoNotasFrequenciaFinal(fechamentoVO, seqOrientador, false);
                if (!model.FechamentoIndividual)
                {
                    TurmaDomainService.FecharDiarioTurma(turma.Seq, model.SeqProfessor, true);
                }

                unitOfWork.Commit();
            }
        }

        private void ValidarAlunosComponenteAsuntoAprovadoDsipensado(LancamentoNotaFechamentoDiarioVO model, long seqTurma)
        {
            bool alunoPossuiComonenteAssuntoAprovado;
            string nomeAluno = string.Empty;
            var dadosTurma = TurmaDomainService.SearchProjectionByKey(seqTurma, p => new
            {
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.SeqComponenteCurricular,
                p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                SeqCicloLetivoTurma = p.SeqCicloLetivoInicio,
            });

            if (model.FechamentoIndividual)
            {
                alunoPossuiComonenteAssuntoAprovado = ValidarPermitirLancacarNotaAlunoAtriculado(model.Alunos.FirstOrDefault().SeqAlunoHistorico,
                                                                                                  dadosTurma.SeqComponenteCurricular,
                                                                                                  dadosTurma.SeqCicloLetivoTurma,
                                                                                                  dadosTurma.SeqComponenteCurricularAssunto);

                if (alunoPossuiComonenteAssuntoAprovado)
                {
                    var dadosAluno = AlunoHistoricoDomainService.SearchProjectionByKey(model.Alunos.FirstOrDefault().SeqAlunoHistorico, p => new
                    {
                        p.Aluno.DadosPessoais.Nome,
                        p.Aluno.DadosPessoais.NomeSocial
                    });

                    nomeAluno = dadosAluno.Nome;

                    if (!string.IsNullOrEmpty(dadosAluno.NomeSocial))
                        nomeAluno = $"{dadosAluno.NomeSocial} ({dadosAluno.Nome})";

                    throw new AplicacaoAvaliacaoAlunoAprovadoDispensadoComponenteAssunto(nomeAluno);
                }
            }
            else
            {
                var professores = new List<long>() { model.SeqProfessor };
                var alunosDiarioTurmaAlunoVO = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, null, professores);

                foreach (var aluno in alunosDiarioTurmaAlunoVO)
                {
                    alunoPossuiComonenteAssuntoAprovado = ValidarPermitirLancacarNotaAlunoAtriculado(aluno.SeqAlunoHistorico,
                                                                                                     dadosTurma.SeqComponenteCurricular,
                                                                                                     dadosTurma.SeqCicloLetivoTurma,
                                                                                                     dadosTurma.SeqComponenteCurricularAssunto);
                    if (alunoPossuiComonenteAssuntoAprovado)
                    {
                        throw new AplicacaoAvaliacaoAlunoAprovadoDispensadoComponenteAssunto(aluno.NomeAluno);
                    }
                }
            }
        }

        /// <summary>
        /// Calcula os totais e situação final para os alunos de uma turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial de origem de avaliação da turma</param>
        /// <param name="seqsAlunoHistorico">Sequenciais dos alunos</param>
        /// <param name="permiteAlunoSemNota">Configuração da turma permite aluno sem nota</param>
        /// <returns>Totais e situações finais dos alunos</returns>
        public List<LancamentoNotaTurmaAlunoVO> CalcularTotaisTurma(long seqOrigemAvaliacao, long[] seqsAlunoHistorico, bool permiteAlunoSemNota)
        {
            var result = new List<LancamentoNotaTurmaAlunoVO>();
            foreach (var seqAlunoHistorico in seqsAlunoHistorico)
            {
                var aluno = new LancamentoNotaTurmaAlunoVO()
                {
                    SeqAlunoHistorico = seqAlunoHistorico
                };

                var avaliacoesDivisao = BuscarApuracoesDasDivisioesPorAlunoTurma(seqAlunoHistorico, seqOrigemAvaliacao);
                var quantidadeAvaliacoesAluno = AplicacaoAvaliacaoDomainService.BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(seqAlunoHistorico, seqOrigemAvaliacao);
                aluno.TodasApuracoesDivisaoLancadas = quantidadeAvaliacoesAluno == avaliacoesDivisao.Count && avaliacoesDivisao.Count > 0;
                aluno.TodasApuracoesVazias = avaliacoesDivisao.Count == 0;
                decimal? totalParcial = null;
                if (avaliacoesDivisao.SMCAny()
                 && avaliacoesDivisao.All(a => !a.Comparecimento)
                 && aluno.TodasApuracoesDivisaoLancadas)
                {
                    aluno.TotalParcial = "*";
                    aluno.TodasApuracoesDivisaoLancadas = true;
                }
                else
                {
                    totalParcial = SomarNotas(avaliacoesDivisao.Select(s => s.Nota));
                    aluno.TotalParcial = totalParcial?.ToString(CultureInfo.InvariantCulture);
                }

                if (totalParcial.HasValue || permiteAlunoSemNota || aluno.TotalParcial == "*")
                {
                    var avaliacoesTurma = BuscarApuracoesPorAlunoOrigemAvaliacao(seqAlunoHistorico, seqOrigemAvaliacao);
                    var totalTurma = SomarNotas(avaliacoesTurma.Select(s => s.Nota));

                    // RN_APR_044 Gravação de histórico escolar para turmas com avaliação parcial
                    // Nota final: interpretar fórmula associada a turma, por enquanto utilizar a seguinte fórmula:
                    /*
                     * - se aluno com nota de realiavação:
                     *   ((soma de notas das avaliações de todas a divisões que o aluno estiver matriculado) + (soma de avaliação golbal - se houver) + nota de reavaliação )/2
                     * - se aluno sem nota de realiavação:
                     *   (soma de notas das avaliações de todas a divisões que o aluno estiver matriculado) + (soma de avaliação golbal - se houver)
                     *
                     * A nota final deve ser arredondada para valor inteiro
                     *
                     * */
                    aluno.TotalFinal = (totalParcial ?? 0) + (totalTurma ?? 0);
                    if (avaliacoesTurma.Any(a => a.TipoAvaliacao == TipoAvaliacao.Reavaliacao))
                    {
                        aluno.TotalFinal = Math.Round(((aluno.TotalFinal ?? 0) / 2), 0, MidpointRounding.AwayFromZero);
                    }

                    if (aluno.TodasApuracoesDivisaoLancadas)
                    {
                        aluno.SituacaoHistoricoEscolar = HistoricoEscolarDomainService
                            .CalcularSituacaofinalLancamentoNotaParcial(seqOrigemAvaliacao, seqAlunoHistorico, Convert.ToInt16(aluno.TotalFinal), null);
                    }
                    else if (permiteAlunoSemNota && totalParcial == null)
                    {
                        aluno.SituacaoHistoricoEscolar = SituacaoHistoricoEscolar.AlunoSemNota;
                        aluno.TotalFinal = null;
                    }
                }
                result.Add(aluno);
            }
            return result;
        }

        public decimal? SomarNotas(IEnumerable<decimal?> notas)
        {
            decimal? total = null;
            foreach (var nota in notas)
            {
                if (nota.HasValue)
                {
                    total = (total ?? 0) + nota;
                }
            }
            return total;
        }

        public void AdicionarArquivoAta(long seqAplicacaoAvaliacao, SMCUploadFile arquivoAnexado)
        {
            // Recupera a apuração
            var apuracao = BuscarApuracao(seqAplicacaoAvaliacao);
            if (apuracao != null)
            {
                if (!apuracao.SeqArquivoAnexadoAtaDefesa.HasValue)
                {
                    var arquivoSalvar = arquivoAnexado.Transform<ArquivoAnexado>();
                    ArquivoAnexadoDomainService.SaveEntity(arquivoSalvar);

                    this.UpdateFields(new ApuracaoAvaliacao { Seq = apuracao.Seq, SeqArquivoAnexadoAtaDefesa = arquivoSalvar.Seq }, x => x.SeqArquivoAnexadoAtaDefesa);
                }
            }
        }

        /// <summary>
        /// Se se a origem de avaliação em questão possuir apuração por frequência, verificar:
        /// 1) Se o diário está sendo fechado verificar:
        /// Se houver divisão de turma com grade lançada, verificar se existe aula não apurada.
        /// Se sim abortar operação e exibir mensagem:MENSAGEM
        /// "Operação não permitida. Existe aula não apurada"
        ///
        /// 2) Se for atualização de histórico escolar de 1 aluno, verificar:
        /// Se houver divisão de turma com grade lançada, verificar se existe aula não apurada com data anterior a data atual.
        /// Se sim abortar operação e exibir mensagem:MENSAGEM
        /// "Operação não permitida. Existe aula não apurada"
        /// </summary>
        /// <param name="seqTurma">Sequencial de turma</param>
        /// <param name="seqAluno">Sequencial do aluno, caso não seja iformado significa que valida a turma</param>
        public void ValidarTurmaPermiteApuracaoFrequencia(long seqTurma, long? seqAluno)
        {
            EventoAulaFilterSpecification specEventoAula = new EventoAulaFilterSpecification();
            DivisaoTurmaFilterSpecification specDivisao = new DivisaoTurmaFilterSpecification();
            if (seqAluno.HasValue)
            {
                specDivisao.Seqs = DivisaoTurmaDomainService.BuscarDivisoesTurmaPorAlunoParticipaTurma(seqAluno.Value, seqTurma)
                                                            .Select(s => s.Seq).ToList();
                //Caso seja atualização de historico irá levar em consideração os eventos aula até um dia antes.
                specEventoAula.FimPeriodo = DateTime.Now.Date.AddDays(-1);
            }
            else
            {
                specDivisao.SeqTurma = seqTurma;
            }

            var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisao, p => new
            {
                p.Seq,
                p.OrigemAvaliacao.ApurarFrequencia
            }).ToList();

            dadosDivisao.ForEach(divisao =>
            {
                if (divisao.ApurarFrequencia.GetValueOrDefault())
                {
                    specEventoAula.SeqDivisaoTurma = divisao.Seq;

                    List<EventoAula> eventosAula = EventoAulaDomainService.SearchBySpecification(specEventoAula).ToList();

                    if (eventosAula.Any(a => a.SituacaoApuracaoFrequencia == SituacaoApuracaoFrequencia.NaoApurada))
                    {
                        throw new ApuracaoAvaliacaoAulasNaoApuradasException();
                    }
                }
            });
        }
    }
}