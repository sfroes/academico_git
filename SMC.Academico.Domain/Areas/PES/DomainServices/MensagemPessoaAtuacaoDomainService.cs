using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Academico.Domain.Areas.PES.Resources;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Framework.Domain;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Framework.UnitOfWork;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Framework.Security.Util;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Common.Constants;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class MensagemPessoaAtuacaoDomainService : AcademicoContextDomain<MensagemPessoaAtuacao>
    {
        #region [ DomainService ]

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();
        private InstituicaoNivelTipoMensagemDomainService InstituicaoNivelTipoMensagemDomainService => Create<InstituicaoNivelTipoMensagemDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private TipoMensagemDomainService TipoMensagemDomainService => Create<TipoMensagemDomainService>();
        private ApuracaoFrequenciaGradeDomainService ApuracaoFrequenciaGradeDomainService => Create<ApuracaoFrequenciaGradeDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();
        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Listar todas as mensagens dos dados
        /// </summary>
        /// <param name="filtro">Fitros de pesquisa</param>
        /// <returns>Lista de com as mensagens e seus dados</returns>
        public SMCPagerData<MensagemListaVO> ListarMensagens(MensagemFiltroVO filtro)
        {
            var spec = filtro.Transform<MensagemPessoaAtuacaoFilterSpecification>(filtro);
            var includes = IncludesMensagemPessoaAtuacao.Mensagem_ArquivoAnexado |
                            IncludesMensagemPessoaAtuacao.Mensagem_TipoMensagem |
                            IncludesMensagemPessoaAtuacao.Mensagem;
            var mensagens = SearchBySpecification(spec, out int total, includes).ToList();
            var lista = mensagens.TransformList<MensagemListaVO>();

            foreach (var item in lista)
            {
                var dataFimVigencia = item.DataFimVigencia.HasValue ? item.DataFimVigencia.Value.ToShortDateString() : "";
                item.DataUsuarioInclusao = $"{item.DataInclusao.ToShortDateString()} - {item.UsuarioInclusao}";
                item.PeriodoVigencia = $"{item.DataInicioVigencia.ToShortDateString()} - {dataFimVigencia}";
            }

            MontarMensagemExclusao(lista);

            return new SMCPagerData<MensagemListaVO>(lista, total);
        }

        /// <summary>
        /// Recupera uma mensagem e algumas configurações desta
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação associada à mensagem</param>
        /// <param name="seq">Sequencial da mensagem pessoa atuação</param>
        /// <returns>Dados da mensagem e algumas configurações</returns>
        public MensagemVO BuscarMensagem(long seqPessoaAtuacao, long seq)
        {
            var obj = SearchByKey(new SMCSeqSpecification<MensagemPessoaAtuacao>(seq), a => a.Mensagem, a => a.Mensagem.ArquivoAnexado, a => a.PessoaAtuacao, a => a.Mensagem.TipoMensagem);
            var mensagemVO = obj.Transform<MensagemVO>();

            if (obj.Mensagem.SeqArquivoAnexado.HasValue)
                mensagemVO.ArquivoAnexado.GuidFile = obj.Mensagem.ArquivoAnexado.UidArquivo.ToString();

            //Verificando se o arquivo é obrigatório
            var filtro = new InstituicaoNivelTipoMensagemFilterSpecification();
            filtro.SeqTipoMensagem = mensagemVO.SeqTipoMensagem;
            filtro.PermiteCadastroManual = true;
            filtro.SeqPessoaAtuacao = seqPessoaAtuacao;
            var lista = InstituicaoNivelTipoMensagemDomainService.BuscarInstituicaoNivelTipoMensagens(filtro);
            mensagemVO.ArquivoObrigatorio = lista != null && lista.Count > 0 ? lista[0].ExigeArquivo : false;

            if (obj.PessoaAtuacao.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var situacao = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqPessoaAtuacao);
                mensagemVO.AlunoFormado = situacao?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO;
            }

            if (obj.Mensagem.TipoMensagem.CategoriaMensagem == CategoriaMensagem.Ocorrencia)
            {
                mensagemVO.DataFimObrigatoria = true;
                mensagemVO.TipoMensagemBloqueado = true;
            }

            mensagemVO.PermitirOcorrenciaCicloLetivoAnterior = SMCSecurityHelper.Authorize(UC_PES_005_02_02.PERMITIR_OCORRENCIA_CICLO_LETIVO_ANTERIOR);
            mensagemVO.DataLimiteInicioVigencia = BuscarDataInicioVigenciaPermitida(mensagemVO.PermitirOcorrenciaCicloLetivoAnterior, seqPessoaAtuacao);
            if (mensagemVO.PermitirOcorrenciaCicloLetivoAnterior)
            {
                mensagemVO.DataInicioVigenciaCicloAnterior = mensagemVO.DataInicioVigencia;
            }

            return mensagemVO;
        }

        /// <summary>
        /// Verifica se a pessoa atuação informada é um aluno formado
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Mensagem com o campo AlunoFormado preenchido</returns>
        public MensagemVO BuscarConfiguracaoMensagem(long seqPessoaAtuacao)
        {
            var situacaoAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(seqPessoaAtuacao);
            bool PermitirOcorrenciaCicloLetivoAnterior = SMCSecurityHelper.Authorize(UC_PES_005_02_02.PERMITIR_OCORRENCIA_CICLO_LETIVO_ANTERIOR);

            MensagemVO retorno = new MensagemVO()
            {
                AlunoFormado = situacaoAluno?.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.FORMADO,
                PermitirOcorrenciaCicloLetivoAnterior = PermitirOcorrenciaCicloLetivoAnterior,
                DataLimiteInicioVigencia = BuscarDataInicioVigenciaPermitida(PermitirOcorrenciaCicloLetivoAnterior, seqPessoaAtuacao),
            };

            return retorno;
        }

        /// <summary>
        /// Salva um registro de Mensagem e, em seguida,
        /// salva um registro em Mensagem Pessoa Atuação, relacioada à mensagem recém salva.
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Sequencial do Mensagem Pessoa Atuação.</returns>
        public long SalvarMensagem(MensagemVO mensagem)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                Mensagem m = mensagem.Transform<Mensagem>();

                if (mensagem.PermitirOcorrenciaCicloLetivoAnterior)
                {
                    m.DataInicioVigencia = mensagem.DataInicioVigenciaCicloAnterior;
                    mensagem.DataInicioVigencia = mensagem.DataInicioVigenciaCicloAnterior;
                }

                ValidarMensagensPeriodoConcorrente(mensagem);

                //Salvando a mensagem...
                if (mensagem.Seq == 0)
                {
                    m.Seq = mensagem.SeqMensagem;
                    MensagemDomainService.SaveEntity(m);
                    //Atualiza para receber seq mensgem atualizado
                    mensagem.SeqMensagem = m.Seq;
                    AtualizarFrequencias(mensagem, false);
                }
                else
                {
                    AtualizarFrequencias(mensagem, false);
                    m.Seq = mensagem.SeqMensagem;
                    MensagemDomainService.SaveEntity(m);
                }

                //Salvando a mensagem pessoa atuação...
                MensagemPessoaAtuacao mpa = new MensagemPessoaAtuacao();
                mpa.SeqMensagem = m.Seq;
                mpa.Seq = mensagem.Seq;
                mpa.SeqPessoaAtuacao = mensagem.SeqPessoaAtuacao;

                SaveEntity(mpa);

                AtualizarHistoricoEscolcarAlunoMensagem(mensagem);
                unitOfWork.Commit();

                return mpa.Seq;
            }
        }

        /// <summary>
        /// Exclui o registro da tabela Mensagem Pessoa Atuacao e,
        /// em seguida, tenta excluir o registro da tabela Mensagem.
        /// Se o registro estiver relacionado a outros 'Mensagem Pessoa Atuação'
        /// a mensagem não será excluída.
        /// </summary>
        public void ExcluirMensagem(long seq)
        {
            //Recuperando o sequencial da Mensagem...
            var dadosMensagemPessoaAtuacao = this.SearchProjectionByKey(seq, p => new { p.SeqMensagem, p.SeqPessoaAtuacao });
            AtualizarExclusaoFrequencia(seq);
            MensagemVO mensagem = BuscarMensagem(dadosMensagemPessoaAtuacao.SeqPessoaAtuacao, seq);
            AtualizarHistoricoEscolcarAlunoMensagem(mensagem);
            //Excluindo o Mensagem Pessoa Atuacao...
            this.DeleteEntity(seq);
            //Tentando excluir a Mensagem...
            MensagemDomainService.DeleteEntity(dadosMensagemPessoaAtuacao.SeqMensagem);

        }

        /// <summary>
        /// Validar se assert referente a regras irá ser exibido
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Boleano permitindo ou não a exibiçaõ de assert</returns>
        public bool validarMensagemAssert(MensagemVO mensagem)
        {
            bool retorno = false;

            if (mensagem.PermitirOcorrenciaCicloLetivoAnterior)
            {
                mensagem.DataInicioVigencia = mensagem.DataInicioVigenciaCicloAnterior;
            }

            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);
            //RN_PES_033 - Inclusão/alteração de mensagem
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
            {
                List<ApuracaoFrequenciaGradeVO> ausenciasPeriodo = BuscarAusenciasPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                retorno = ausenciasPeriodo.Count() > 0;
            }

            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {
                List<EventoAulaVO> aulas = BusacarEventosAulaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                retorno = aulas.Count() > 0;
            }

            if (mensagem.Seq > 0)
            {
                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                    tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                {
                    List<ApuracaoFrequenciaGradeVO> ausenciasForaPeriodo = BuscarAusenciaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                    if (ausenciasForaPeriodo.Count > 0)
                    {
                        retorno = true;
                    }
                }

                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                {
                    List<ApuracaoFrequenciaGradeVO> ausenciaForaPeriodo = BuscarAusenciaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                    if (ausenciaForaPeriodo.Count > 0)
                    {
                        retorno = true;
                    }
                }
            }

            if (ValidarHistoricoEscolcarAlunoMensagem(mensagem).historicoAlterado)
            {
                retorno = true;
            }

            return retorno;
        }

        /// <summary>
        /// Mensagem que será exibida no assert
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Mensagem que será exibida no assert de mensagem</returns>
        public string BuscarMensagemAssert(MensagemVO mensagem)
        {
            string retorno = string.Empty;

            if (mensagem.PermitirOcorrenciaCicloLetivoAnterior)
            {
                mensagem.DataInicioVigencia = mensagem.DataInicioVigenciaCicloAnterior;
            }

            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);

            //RN_PES_033 - Inclusão/alteração de mensagem
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
            {
                List<ApuracaoFrequenciaGradeVO> ausenciasPeriodo = BuscarAusenciasPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                if (ausenciasPeriodo.Count() > 0)
                {
                    retorno = string.Format(MessagesResource.MSG_FALTAS_ABONADAS_PERIODO, ausenciasPeriodo.Count());
                }
            }

            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {
                List<EventoAulaVO> aulas = BusacarEventosAulaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                if (aulas.Count() > 0)
                {
                    retorno = string.Format(MessagesResource.MSG_SACAO_PERIODO, aulas.Count());
                }
            }

            if (mensagem.Seq > 0)
            {
                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                    tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                {
                    List<ApuracaoFrequenciaGradeVO> ausenciasForaPeriodo = BuscarAusenciaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                    if (ausenciasForaPeriodo.Count > 0)
                    {
                        retorno += !string.IsNullOrEmpty(retorno) ? "<br />" : string.Empty;
                        retorno += string.Format(MessagesResource.MSG_FALTAS_ABONADAS_FORA_PERIODO, ausenciasForaPeriodo.Count());
                    }
                }

                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                {
                    List<ApuracaoFrequenciaGradeVO> ausenciaForaPeriodo = BuscarPresencaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);

                    if (ausenciaForaPeriodo.Count > 0)
                    {
                        retorno += !string.IsNullOrEmpty(retorno) ? "<br />" : string.Empty;
                        retorno += string.Format(MessagesResource.MSG_SANCAO_FORA_PERIODO, ausenciaForaPeriodo.Count());
                    }
                }
            }

            string mensagemHistoricoEscolar = ValidarHistoricoEscolcarAlunoMensagem(mensagem).mensagemHistorico;
            if (!string.IsNullOrEmpty(mensagemHistoricoEscolar))
            {
                retorno += !string.IsNullOrEmpty(retorno) ? "<br />" : string.Empty;
                retorno += mensagemHistoricoEscolar;
            }

            retorno += !string.IsNullOrEmpty(retorno) ? " Deseja continuar?" : string.Empty;

            return retorno;
        }

        /// <summary>
        /// Buscar mensagens de alunos de uma determinda categoria de mensagem
        /// </summary>
        /// <param name="seqsPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <param name="categoriaMensagem">Categoria da mensagem</param>
        /// <returns>Lista de mensagens</returns>
        public List<MensagemListaVO> BuscarMensagensPorCategoria(List<long> seqsPessoaAtuacao, CategoriaMensagem categoriaMensagem)
        {
            var spec = new MensagemPessoaAtuacaoFilterSpecification() { SeqsPessoaAtuacao = seqsPessoaAtuacao, CategoriaMensagem = categoriaMensagem };
            var mensagens = this.SearchProjectionBySpecification(spec, p => new MensagemListaVO
            {
                SeqPessoaAtuacao = p.SeqPessoaAtuacao,
                SeqMensagem = p.SeqMensagem,
                DataInicioVigencia = p.Mensagem.DataInicioVigencia,
                DataFimVigencia = p.Mensagem.DataFimVigencia.Value,
                DescricaoMensagem = p.Mensagem.Descricao,
                TokenTipoMensagem = p.Mensagem.TipoMensagem.Token,
                SeqTipoMensagem = p.Mensagem.SeqTipoMensagem
            }).ToList();

            return mensagens;
        }

        /// <summary>
        /// Atualizar faltas ou presença
        /// <param name="mensagem">Dados mensagem</param>
        /// <param name="atualizarSomenteEventosCriadosPosteiores">Valida se é para atualizar somente frequencia de eventos criados posteriores a mensagem</param>
        /// </summary>
        /// <param name="mensagem">Dados das mensagens</param>
        public void AtualizarFrequencias(MensagemVO mensagem, bool atualizarSomenteEventosCriadosPosteiores)
        {
            var eventosAulaAluno = BusacarEventosAulaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);
            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);
            //RN_PES_033 - Inclusão/alteração de mensagem
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
            {
                AtualizarFrequenciaPeriodo(mensagem, OcorrenciaFrequencia.AbonoRetificacao, eventosAulaAluno, atualizarSomenteEventosCriadosPosteiores);
            }

            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {
                AtualizarFrequenciaPeriodo(mensagem, OcorrenciaFrequencia.Sancao, eventosAulaAluno, atualizarSomenteEventosCriadosPosteiores);
            }

            if (mensagem.Seq > 0 && !atualizarSomenteEventosCriadosPosteiores)
            {
                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                    tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                {
                    AtualizarFrequenciaForaPeriodo(mensagem);
                }

                if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                {
                    AtualizarFrequenciaForaPeriodo(mensagem);
                }
            }
        }

        /// <summary>
        /// Verificar se existem frequências já lançadas para o aluno no período informado.
        /// Se já existe apuração alterar:
        /// -sequencial de mensagem = sequencial da mensagem de referência
        /// -domínio de ocorrência de frequência = ‘Retificação / abono’
        /// Se não existe apuração, incluir apuração com:
        /// -sequencial de mensagem = sequencial da mensagem de referência
        /// -domínio de ocorrência de frequência = ‘Retificação / abono’
        /// -domínio frequência = ‘Presença’*/
        /// </summary>
        /// <param name="mensagem">Mensagem a ser salva</param>
        /// <param name="ocorrenciaFrequencia">Ocorrencia frequencia</param>
        /// <param name="eventosAulaAluno">Eventos aula do aluno</param>
        /// <param name="atualizarSomenteEventosCriadosPosteiores">Valida se é para atualizar somente frequencia de eventos criados posteriores a mensagem</param>
        private void AtualizarFrequenciaPeriodo(MensagemVO mensagem, OcorrenciaFrequencia ocorrenciaFrequencia, List<EventoAulaVO> eventosAulaAluno, bool atualizarSomenteEventosCriadosPosteiores)
        {
            List<long> seqsEventoAulas = eventosAulaAluno.Select(s => s.Seq).ToList();

            List<ApuracaoFrequenciaGradeVO> frequenciasApuradas = ApuracaoFrequenciaGradeDomainService
                                                                  .BuscarApuracaoFrequenciaGradePorAluno(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia);
            //Valido se todos os eventos daquele periodo 
            List<ApuracaoFrequenciaGradeVO> frequenciasSeremAbonadas = frequenciasApuradas.Where(w => seqsEventoAulas.Contains(w.SeqEventoAula)).ToList();
            //valido aulas que ja foram sancionadas
            List<long> seqsAulasSemFrequencia = seqsEventoAulas.Where(w => !frequenciasApuradas.Select(s => s.SeqEventoAula).Contains(w)).ToList();

            //Atualizar todos os eventos
            if (!atualizarSomenteEventosCriadosPosteiores)
            {
                foreach (var item in frequenciasSeremAbonadas)
                {
                    ApuracaoFrequenciaGradeDomainService.UpdateFields(new ApuracaoFrequenciaGrade
                    {
                        Seq = item.Seq,
                        SeqMensagem = mensagem.SeqMensagem,
                        OcorrenciaFrequencia = ocorrenciaFrequencia,
                    }, x => x.SeqMensagem, x => x.OcorrenciaFrequencia);
                }
            }

            foreach (var item in seqsAulasSemFrequencia)
            {
                long seqDivisao = eventosAulaAluno.Where(w => w.Seq == item).FirstOrDefault().SeqDivisaoTurma;
                long seqAlunoHistoricoCicloLetivo = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia)
                                                                                .Where(w => w.SeqDivisaoTurma == seqDivisao).FirstOrDefault().SeqAlunoHistoricoCicloLetivo;
                ApuracaoFrequenciaGrade apuracao = new ApuracaoFrequenciaGrade()
                {
                    SeqEventoAula = item,
                    SeqMensagem = mensagem.SeqMensagem,
                    OcorrenciaFrequencia = ocorrenciaFrequencia,
                    Frequencia = Frequencia.Presente,
                    SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivo,
                };
                ApuracaoFrequenciaGradeDomainService.SaveEntity(apuracao);
            }
        }

        /// <summary>
        /// Verificar se existem frequências com sequencial da mensagem e fora do novo intervalo
        /// Se sim e existe a situação a2, alterar:
        /// - sequencial de mensagem = null
        /// - domínio de ocorrência de frequência =null
        /// </summary>
        /// <param name="mensagem">Mensagem a ser salva</param>
        private void AtualizarFrequenciaForaPeriodo(MensagemVO mensagem)
        {
            List<ApuracaoFrequenciaGradeVO> apuracoesForaPeriodo = new List<ApuracaoFrequenciaGradeVO>();
            apuracoesForaPeriodo.AddRange(BuscarPresencaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia));
            apuracoesForaPeriodo.AddRange(BuscarAusenciaForaPeriodo(mensagem.SeqPessoaAtuacao, mensagem.SeqMensagem, mensagem.DataInicioVigencia, mensagem.DataFimVigencia));

            foreach (var item in apuracoesForaPeriodo)
            {
                ApuracaoFrequenciaGradeDomainService.UpdateFields(new ApuracaoFrequenciaGrade
                {
                    Seq = item.Seq,
                    SeqMensagem = null,
                    OcorrenciaFrequencia = null,
                }, x => x.SeqMensagem, x => x.OcorrenciaFrequencia);
            }

        }

        /// <summary>
        /// Atualizar todas as frequencias.
        /// </summary>
        /// <param name="seq">Sequencial da mensagem pessoa atuação</param>
        /// <param name="seqMensagem">Sequencial da mensagem</param>
        private void AtualizarExclusaoFrequencia(long seq)
        {
            var mensagemPessoaAtuacao = this.SearchProjectionByKey(seq, p => new
            {
                p.SeqPessoaAtuacao,
                Mensagem = new
                {
                    p.Mensagem.Seq,
                    p.Mensagem.SeqTipoMensagem,
                    p.Mensagem.DataInicioVigencia,
                    p.Mensagem.DataFimVigencia
                }
            });

            List<ApuracaoFrequenciaGradeVO> apuracoes = ApuracaoFrequenciaGradeDomainService
                                                        .BuscarApuracaoFrequenciaGradePorAluno(mensagemPessoaAtuacao.SeqPessoaAtuacao,
                                                                                               mensagemPessoaAtuacao.Mensagem.DataInicioVigencia,
                                                                                               mensagemPessoaAtuacao.Mensagem.DataFimVigencia)
                                                                                               .Where(w => w.SeqMensagem == mensagemPessoaAtuacao.Mensagem.Seq).ToList();

            foreach (var item in apuracoes)
            {
                ApuracaoFrequenciaGrade apuracao = new ApuracaoFrequenciaGrade();
                apuracao.Seq = item.Seq;
                apuracao.SeqMensagem = null;
                apuracao.OcorrenciaFrequencia = null;
                //Se por ventura tiver uma retificação e uma mensagem no momento de se excluir a ocorrencia sempre deve voltar para abono.
                if (!string.IsNullOrEmpty(item.ObservacaoRetificacao))
                {
                    apuracao.OcorrenciaFrequencia = OcorrenciaFrequencia.AbonoRetificacao;
                }
                ApuracaoFrequenciaGradeDomainService.UpdateFields(apuracao, x => x.SeqMensagem, x => x.OcorrenciaFrequencia);
            }
        }

        /// <summary>
        /// Buscar se existem frequências já lançadas para o aluno no período informado, com situação da frequência = 'ausência' e que não tem data de retificação.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="inicioPeriodo">Inicio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns></returns>
        private List<ApuracaoFrequenciaGradeVO> BuscarAusenciasPeriodo(long seqAluno, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            List<ApuracaoFrequenciaGradeVO> apuracoes = ApuracaoFrequenciaGradeDomainService.BuscarApuracaoFrequenciaGradePorAluno(seqAluno, inicioPeriodo, fimPeriodo);

            return apuracoes.Where(w => w.Frequencia == Frequencia.Ausente && !w.DataRetificacao.HasValue).ToList();
        }

        /// <summary>
        /// Buscar se existem frequências com situação de ’ausência’, com sequencial da mensagem e fora do novo intervalo.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="inicioPeriodo">Inicio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns></returns>
        private List<ApuracaoFrequenciaGradeVO> BuscarAusenciaForaPeriodo(long seqAluno, long seqMensagem, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            //Busca apurações com os novos parametros
            List<ApuracaoFrequenciaGradeVO> apuracoesAtual = BuscarAusenciasPeriodo(seqAluno, inicioPeriodo, fimPeriodo);

            //Busca mensagem com os parametros que estavam no banco
            var mensagemInDB = this.MensagemDomainService.SearchProjectionByKey(seqMensagem, p => new { p.DataInicioVigencia, p.DataFimVigencia });
            List<ApuracaoFrequenciaGradeVO> apuracoesInDb = BuscarAusenciasPeriodo(seqAluno, mensagemInDB.DataInicioVigencia, mensagemInDB.DataFimVigencia);

            //Todas as apuracoes
            List<ApuracaoFrequenciaGradeVO> apuracoesForaPeriodo;
            //Se as apurações do banco que não estiverem nas apurações atuais estarão fora do novo periodo
            var seqApuracacoAtuais = apuracoesAtual.Select(s => s.Seq).ToList();
            apuracoesForaPeriodo = apuracoesInDb.Where(w => !seqApuracacoAtuais.Contains(w.Seq)).ToList();

            return apuracoesForaPeriodo;
        }

        /// <summary>
        /// Buscar se existem aulas existentes no período. 
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="inicioPeriodo">Inicio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns></returns>
        private List<EventoAulaVO> BusacarEventosAulaPeriodo(long seqAluno, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            return EventoAulaDomainService.BuscarEventoAulaAlunoNoPeriodo(seqAluno, inicioPeriodo, fimPeriodo).ToList();
        }

        /// <summary>
        /// Buscar se existem frequências com o sequencial da mensagem e fora do novo intervalo e com situação = 'presença'. 
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="seqMensagem">Sequencial da mensagem</param>
        /// <param name="inicioPeriodo">Inicio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns></returns>
        private List<ApuracaoFrequenciaGradeVO> BuscarPresencaForaPeriodo(long seqAluno, long seqMensagem, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            //Busca apurações com os novos parametros
            List<ApuracaoFrequenciaGradeVO> apuracoesAtual = ApuracaoFrequenciaGradeDomainService
                                                                .BuscarApuracaoFrequenciaGradePorAluno(seqAluno, inicioPeriodo, fimPeriodo);
            //Busca apurações com os parametros que estavam no banco
            var mensagemInDB = this.MensagemDomainService.SearchProjectionByKey(seqMensagem, p => new { p.DataInicioVigencia, p.DataFimVigencia });
            List<ApuracaoFrequenciaGradeVO> apuracoesInDb = ApuracaoFrequenciaGradeDomainService
                                                            .BuscarApuracaoFrequenciaGradePorAluno(seqAluno, mensagemInDB.DataInicioVigencia, mensagemInDB.DataFimVigencia);
            //Todas as apuracoes
            List<ApuracaoFrequenciaGradeVO> apuracoesForaPeriodo;
            var seqApuracacoAtuais = apuracoesAtual.Select(s => s.Seq).ToList();
            apuracoesForaPeriodo = apuracoesInDb.Where(w => !seqApuracacoAtuais.Contains(w.Seq)).ToList();

            return apuracoesForaPeriodo.Where(w => w.Frequencia == Frequencia.Presente).ToList();
        }

        /// <summary>
        /// Montar mensagem de exclusão
        /// </summary>
        /// <param name="lista">Lista de mensagens</param>
        private void MontarMensagemExclusao(List<MensagemListaVO> lista)
        {
            string mensagemHistoricoEscolar = string.Empty;
            foreach (var item in lista)
            {
                item.MensagemExcluir = string.Format(MessagesResource.MSG_EXCLUIR_PADRAO, item.DescricaoTipoMensagem);
                if (item.TokenTipoMensagem == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                    item.TokenTipoMensagem == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                {
                    int apuracoes = ApuracaoFrequenciaGradeDomainService.BuscarApuracaoFrequenciaGradePorAluno(item.SeqPessoaAtuacao,
                                                                                                               item.DataInicioVigencia,
                                                                                                               item.DataFimVigencia)
                                                                         .Where(w => w.Frequencia == Frequencia.Ausente).Count();
                    if (apuracoes > 0)
                    {
                        item.MensagemExcluir += "<br />" + string.Format(MessagesResource.MSG_EXCLUIR_ABONO, apuracoes);
                    }

                    mensagemHistoricoEscolar = ValidarHistoricoEscolcarAlunoMensagemExcluir(item.Transform<MensagemVO>()).mensagemHistorico;
                }
                else if (item.TokenTipoMensagem == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                {
                    int apuracoes = ApuracaoFrequenciaGradeDomainService.BuscarApuracaoFrequenciaGradePorAluno(item.SeqPessoaAtuacao,
                                                                                           item.DataInicioVigencia,
                                                                                           item.DataFimVigencia)
                                                                        .Where(w => w.Frequencia == Frequencia.Presente).Count();
                    if (apuracoes > 0)
                    {
                        item.MensagemExcluir += "<br />" + string.Format(MessagesResource.MSG_EXCLUIR_SANCAO, apuracoes);
                    }

                    mensagemHistoricoEscolar = ValidarHistoricoEscolcarAlunoMensagemExcluir(item.Transform<MensagemVO>()).mensagemHistorico;
                }

                if (!string.IsNullOrEmpty(mensagemHistoricoEscolar))
                {
                    item.MensagemExcluir += !string.IsNullOrEmpty(item.MensagemExcluir) ? $"<br />{mensagemHistoricoEscolar}" : string.Empty;
                }

                if (!string.IsNullOrEmpty(item.MensagemExcluir))
                {
                    item.MensagemExcluir += " Deseja continuar?";
                }
            }
        }

        /// <summary>
        /// Validar mensagens da categoria "Mensagem para ocorrência" não podem ter períodos concorrentes para o mesmo aluno
        /// </summary>
        /// <param name="mensagem">Mensagem a ser salva</param>
        private void ValidarMensagensPeriodoConcorrente(MensagemVO mensagem)
        {
            var spec = new MensagemPessoaAtuacaoFilterSpecification() { SeqPessoaAtuacao = mensagem.SeqPessoaAtuacao, CategoriaMensagem = CategoriaMensagem.Ocorrencia };

            var mensagensAluno = SearchProjectionBySpecification(spec, p => new
            {
                p.SeqMensagem,
                p.Mensagem.DataInicioVigencia,
                p.Mensagem.DataFimVigencia,
                p.Mensagem.TipoMensagem.CategoriaMensagem
            }).ToList();

            foreach (var item in mensagensAluno)
            {
                if (((mensagem.DataInicioVigencia >= item.DataInicioVigencia && mensagem.DataInicioVigencia <= item.DataFimVigencia) ||
                  (mensagem.DataFimVigencia >= item.DataInicioVigencia && mensagem.DataFimVigencia <= item.DataFimVigencia)) &&
                  item.SeqMensagem != mensagem.SeqMensagem)
                {
                    throw new MensagemConcidentesException();
                }
            }
        }

        /// <summary>
        /// Atualizar historico escolar do aluno
        /// </summary>
        /// <param name="mensagem">Mensagem dados</param>
        private void AtualizarHistoricoEscolcarAlunoMensagem(MensagemVO mensagem)
        {
            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);
            //Se token = REGIME_ESPECIAL_ESTUDO, ABONO_POR_PERIODO ou SANSAO_DISCIPLINAR_SUSPENSAO
            //Atualizar historico escolar
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {
                List<long> seqsDvisoesAluno = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia)
                                                                          .Select(s => s.SeqDivisaoTurma).Cast<long>().ToList();

                //Caso não tenha divisão para o periodo do plano de estudo do aluno sai da validação
                if (seqsDvisoesAluno.Count == 0)
                {
                    return;
                }

                var spec = new TurmaFilterSpecification() { SeqsDivisoesTurma = seqsDvisoesAluno };
                var turmasDivisoes = TurmaDomainService.SearchProjectionBySpecification(spec, p => new
                {
                    Seq = p.Seq,
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false
                }).Distinct().Where(w => w.DiarioFechado).ToList();

                //Validar se por ventura o aluno tem historico fechado e desta forma irá mudar a situação do aluno lançando falatas retroativas
                foreach (var turma in turmasDivisoes)
                {
                    var dadosAluno = TurmaDomainService.BuscarDiarioTurmaAluno(turma.Seq, null, mensagem.SeqPessoaAtuacao, null).FirstOrDefault();
                    HistoricoEscolarCompletoVO historicoEscolarAtual = HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(turma.Seq, mensagem.SeqPessoaAtuacao);
                    SituacaoHistoricoEscolar situacaoAlunoAtual = historicoEscolarAtual.SituacaoHistoricoEscolar;
                    //Buscar todas as apurações da turma para contabilizar as faltas
                    var apuracoes = ApuracaoFrequenciaGradeDomainService.BuscarApuracoesFrequenciaTurmaAluno(mensagem.SeqPessoaAtuacao, turma.Seq);
                    //Regra para validar as faltas
                    int faltas = apuracoes.Where(w => (w.Frequencia == Frequencia.Ausente && (w.OcorrenciaFrequencia == null || w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)) ||
                                                      (w.Frequencia == Frequencia.Presente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)).Count();
                    //Prepara para calcular a nova situação do aluno
                    var alunoHistoricoVO = dadosAluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                    alunoHistoricoVO.Faltas = (short)faltas;

                    alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHoraria.GetValueOrDefault();
                    if (dadosAluno.CargaHorariaExecutada > 0 && dadosAluno.CargaHorariaExecutada > dadosAluno.CargaHoraria)
                        alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHorariaExecutada;

                    var situacaoCalculada = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);
                    var percentualFrequenciaCalculada = HistoricoEscolarDomainService.CalcularPercentualFrequencia(alunoHistoricoVO.CargaHoraria, alunoHistoricoVO.Faltas);

                    if (situacaoAlunoAtual != situacaoCalculada || historicoEscolarAtual.Faltas != faltas)
                    {
                        HistoricoEscolar historicoEscolar = new HistoricoEscolar
                        {
                            Seq = dadosAluno.SeqHistoricoEscolar.Value,
                            SituacaoHistoricoEscolar = situacaoCalculada,
                            Faltas = (short)faltas,
                            PercentualFrequencia = percentualFrequenciaCalculada
                        };
                        HistoricoEscolarDomainService.UpdateFields(historicoEscolar, x => x.SituacaoHistoricoEscolar, x => x.Faltas, x => x.PercentualFrequencia);
                    }
                }
            }
        }

        /// <summary>
        /// Validar se houve alteração na situação do historico escolar do aluno
        /// </summary>
        /// <param name="mensagem">Mensagem dados</param>
        private (bool historicoAlterado, string mensagemHistorico) ValidarHistoricoEscolcarAlunoMensagem(MensagemVO mensagem)
        {
            bool historicoAlterado = false;
            string mensagemHistorico = string.Empty;
            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);

            //Se token = REGIME_ESPECIAL_ESTUDO, ABONO_POR_PERIODO ou SANSAO_DISCIPLINAR_SUSPENSAO
            //Atualizar historico escolar
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {


                List<long> seqsDvisoesAluno = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia)
                                                                      .Select(s => s.SeqDivisaoTurma).Cast<long>().ToList();

                //Caso não tenha divisão para o periodo do plano de estudo do aluno sai da validação
                if (seqsDvisoesAluno.Count == 0)
                {
                    return (historicoAlterado, mensagemHistorico);
                }

                //Monta o objeto que trará as turma com suas divisões agrupadas, mas somente as que tem diario fechado
                var spec = new TurmaFilterSpecification() { SeqsDivisoesTurma = seqsDvisoesAluno };
                var turmasDivisoes = TurmaDomainService.SearchProjectionBySpecification(spec, p => new
                {
                    Seq = p.Seq,
                    SeqsDivisoes = p.DivisoesTurma.Select(pd => new
                    {
                        pd.Seq
                    }).ToList(),
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false
                }).Distinct().Where(w => w.DiarioFechado).ToList();

                //Validar se por ventura o aluno tem historico fechado e desta forma irá mudar a situação do aluno lançando falatas retroativas
                foreach (var turma in turmasDivisoes)
                {
                    var specApuracaoPeriodo = new ApuracaoFrequenciaGradeFilterSpecification()
                    {
                        SeqsDivisaoTurma = turma.SeqsDivisoes.Select(s => s.Seq).ToList(),
                        SeqAluno = mensagem.SeqPessoaAtuacao,
                        InicioPeriodo = mensagem.DataInicioVigencia,
                        FimPeriodo = mensagem.DataFimVigencia
                    };

                    List<ApuracaoFrequenciaGradeVO> listaFrequenciaPeriodo = ApuracaoFrequenciaGradeDomainService.SearchProjectionBySpecification(specApuracaoPeriodo, p => new ApuracaoFrequenciaGradeVO
                    {
                        Seq = p.Seq,
                        SeqEventoAula = p.SeqEventoAula,
                        SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                        Frequencia = p.Frequencia,
                        SeqMensagem = p.SeqMensagem,
                        DataRetificacao = p.DataRetificacao,
                        ObservacaoRetificacao = p.ObservacaoRetificacao,
                        OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                        DataInclusao = p.DataInclusao
                    }).ToList();

                    var specApuracaoIntegral = new ApuracaoFrequenciaGradeFilterSpecification()
                    {
                        SeqsDivisaoTurma = turma.SeqsDivisoes.Select(s => s.Seq).ToList(),
                        SeqAluno = mensagem.SeqPessoaAtuacao,
                    };

                    List<ApuracaoFrequenciaGradeVO> listaFrequanciaPeriodoIntegral = ApuracaoFrequenciaGradeDomainService.SearchProjectionBySpecification(specApuracaoIntegral, p => new ApuracaoFrequenciaGradeVO
                    {
                        Seq = p.Seq,
                        SeqEventoAula = p.SeqEventoAula,
                        SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                        Frequencia = p.Frequencia,
                        SeqMensagem = p.SeqMensagem,
                        DataRetificacao = p.DataRetificacao,
                        ObservacaoRetificacao = p.ObservacaoRetificacao,
                        OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                        DataInclusao = p.DataInclusao
                    }).ToList();


                    int faltasIntegral = listaFrequanciaPeriodoIntegral.Where(w => (w.Frequencia == Frequencia.Ausente && (w.OcorrenciaFrequencia == null || w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)) ||
                                                                             (w.Frequencia == Frequencia.Presente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)).Count();

                    int faltasPeriodo = listaFrequenciaPeriodo.Where(w => (w.Frequencia == Frequencia.Ausente && (w.OcorrenciaFrequencia == null || w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)) ||
                                                                    (w.Frequencia == Frequencia.Presente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)).Count();

                    int novasFaltas = 0;

                    if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                       tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                    {
                        novasFaltas = faltasIntegral - faltasPeriodo;
                    }

                    if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                    {
                        if (listaFrequenciaPeriodo.Count() > faltasPeriodo)
                        {
                            novasFaltas = faltasIntegral + (listaFrequenciaPeriodo.Count() - faltasPeriodo);
                        }
                        else if (listaFrequenciaPeriodo.Count() == faltasPeriodo)
                        {
                            novasFaltas = listaFrequanciaPeriodoIntegral.Count();
                        }
                        else
                        {
                            novasFaltas = faltasIntegral + (faltasPeriodo - listaFrequenciaPeriodo.Count());
                        }
                    }

                    var dadosAluno = TurmaDomainService.BuscarDiarioTurmaAluno(turma.Seq, null, mensagem.SeqPessoaAtuacao, null).FirstOrDefault();
                    HistoricoEscolarCompletoVO historicoEscolarAtual = HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(turma.Seq, mensagem.SeqPessoaAtuacao);
                    SituacaoHistoricoEscolar situacaoAlunoAtual = historicoEscolarAtual.SituacaoHistoricoEscolar;
                    //Prepara para calcular a nova situação do aluno
                    var alunoHistoricoVO = dadosAluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                    alunoHistoricoVO.Faltas = (short)novasFaltas;

                    alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHoraria.GetValueOrDefault();
                    if (dadosAluno.CargaHorariaExecutada > 0 && dadosAluno.CargaHorariaExecutada > dadosAluno.CargaHoraria)
                        alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHorariaExecutada;

                    var situacao = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);

                    if (situacaoAlunoAtual != situacao)
                    {
                        historicoAlterado = true;
                        mensagemHistorico = string.Format(MessagesResource.MSG_MENSAGEM_HISTORICO_ESCOLAR, situacaoAlunoAtual, situacao, turma.Seq);
                    }

                }
            }

            return (historicoAlterado, mensagemHistorico);
        }

        /// <summary>
        /// Validar se houve alteração na situação do historico escolar do aluno
        /// </summary>
        /// <param name="mensagem">Mensagem dados</param>
        private (bool historicoAlterado, string mensagemHistorico) ValidarHistoricoEscolcarAlunoMensagemExcluir(MensagemVO mensagem)
        {
            bool historicoAlterado = false;
            string mensagemHistorico = string.Empty;
            var tipoMensagem = TipoMensagemDomainService.BuscarTipoMensagem(mensagem.SeqTipoMensagem);

            //Prepara para calcular a nova situação do aluno
            //Se token = REGIME_ESPECIAL_ESTUDO, ABONO_POR_PERIODO ou SANSAO_DISCIPLINAR_SUSPENSAO
            //Atualizar historico escolar
            if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO ||
                tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
            {

                List<long> seqsDvisoesAluno = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(mensagem.SeqPessoaAtuacao, mensagem.DataInicioVigencia, mensagem.DataFimVigencia)
                                                                      .Select(s => s.SeqDivisaoTurma).Cast<long>().ToList();

                //Caso não tenha divisão para o periodo do plano de estudo do aluno sai da validação
                if (seqsDvisoesAluno.Count == 0)
                {
                    return (historicoAlterado, mensagemHistorico);
                }

                //Monta o objeto que trará as turma com suas divisões agrupadas, mas somente as que tem diario fechado
                var spec = new TurmaFilterSpecification() { SeqsDivisoesTurma = seqsDvisoesAluno };
                var turmasDivisoes = TurmaDomainService.SearchProjectionBySpecification(spec, p => new
                {
                    Seq = p.Seq,
                    SeqsDivisoes = p.DivisoesTurma.Select(pd => new
                    {
                        pd.Seq
                    }).ToList(),
                    DiarioFechado = p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false
                }).Distinct().Where(w => w.DiarioFechado).ToList();

                //Validar se por ventura o aluno tem historico fechado e desta forma irá mudar a situação do aluno lançando falatas retroativas
                foreach (var turma in turmasDivisoes)
                {
                    var specApuracaoPeriodo = new ApuracaoFrequenciaGradeFilterSpecification()
                    {
                        SeqsDivisaoTurma = turma.SeqsDivisoes.Select(s => s.Seq).ToList(),
                        SeqAluno = mensagem.SeqPessoaAtuacao,
                        InicioPeriodo = mensagem.DataInicioVigencia,
                        FimPeriodo = mensagem.DataFimVigencia
                    };

                    List<ApuracaoFrequenciaGradeVO> listaFrequenciaPeriodo = ApuracaoFrequenciaGradeDomainService.SearchProjectionBySpecification(specApuracaoPeriodo, p => new ApuracaoFrequenciaGradeVO
                    {
                        Seq = p.Seq,
                        SeqEventoAula = p.SeqEventoAula,
                        SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                        Frequencia = p.Frequencia,
                        SeqMensagem = p.SeqMensagem,
                        DataRetificacao = p.DataRetificacao,
                        ObservacaoRetificacao = p.ObservacaoRetificacao,
                        OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                        DataInclusao = p.DataInclusao
                    }).ToList();

                    var dadosAluno = TurmaDomainService.BuscarDiarioTurmaAluno(turma.Seq, null, mensagem.SeqPessoaAtuacao, null).FirstOrDefault();
                    HistoricoEscolarCompletoVO historicoEscolarAtual = HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(turma.Seq, mensagem.SeqPessoaAtuacao);
                    SituacaoHistoricoEscolar situacaoAlunoAtual = historicoEscolarAtual.SituacaoHistoricoEscolar;

                    int novasFaltas = 0;

                    if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO ||
                       tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO)
                    {
                        int faltasRemover = listaFrequenciaPeriodo.Where(w => (w.Frequencia == Frequencia.Ausente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.AbonoRetificacao)).Count();

                        novasFaltas = (int)historicoEscolarAtual.Faltas.GetValueOrDefault() + faltasRemover;
                    }

                    if (tipoMensagem.Token == TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO)
                    {
                        int faltasRemover = listaFrequenciaPeriodo.Where(w => (w.Frequencia == Frequencia.Presente && w.OcorrenciaFrequencia == OcorrenciaFrequencia.Sancao)).Count();
                        novasFaltas = (int)historicoEscolarAtual.Faltas.GetValueOrDefault() - faltasRemover;
                    }

                    var alunoHistoricoVO = dadosAluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                    alunoHistoricoVO.Faltas = (short)novasFaltas;

                    alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHoraria.GetValueOrDefault();
                    if (dadosAluno.CargaHorariaExecutada > 0 && dadosAluno.CargaHorariaExecutada > dadosAluno.CargaHoraria)
                        alunoHistoricoVO.CargaHoraria = dadosAluno.CargaHorariaExecutada;

                    var situacao = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);

                    if (situacaoAlunoAtual != situacao)
                    {
                        historicoAlterado = true;
                        mensagemHistorico = string.Format(MessagesResource.MSG_MENSAGEM_HISTORICO_ESCOLAR, situacaoAlunoAtual, situacao, turma.Seq);
                    }
                }
            }

            return (historicoAlterado, mensagemHistorico);
        }

        /// <summary>
        /// Buscar a data limite para inicio da vigencia da mensagem para o aluno aluno
        /// </summary>
        /// <param name="PermitirOcorrenciaCicloLetivoAnterior">Permite ocorrência em coclo letivo anterior</param>
        /// <param name="seqPessoaAtuacao">Sequencial pessoa atuação</param>
        /// <returns>Data do pemitido</returns>
        private DateTime BuscarDataInicioVigenciaPermitida(bool PermitirOcorrenciaCicloLetivoAnterior, long seqPessoaAtuacao)
        {
            DateTime dataLimiteInicioVigencia;

            if (!PermitirOcorrenciaCicloLetivoAnterior)
            {
                var seqCicloLetivo = AlunoDomainService.BuscarCicloLetivoAtual(seqPessoaAtuacao);
                dataLimiteInicioVigencia = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, seqPessoaAtuacao, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO).DataInicio;
            }
            else
            {
                dataLimiteInicioVigencia = AlunoDomainService.SearchProjectionByKey(seqPessoaAtuacao, p => p.Historicos.FirstOrDefault(f => f.Atual).DataAdmissao);
            }

            return dataLimiteInicioVigencia;
        }
    }
}