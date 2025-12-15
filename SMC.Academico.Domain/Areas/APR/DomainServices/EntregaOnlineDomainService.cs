using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class EntregaOnlineDomainService : AcademicoContextDomain<EntregaOnline>
    {
        #region DomainServices

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private EntregaOnlineHistoricoSituacaoDomainService EntregaOnlineHistoricoSituacaoDomainService => Create<EntregaOnlineHistoricoSituacaoDomainService>();
        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService => Create<ApuracaoAvaliacaoDomainService>();
        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService => Create<AplicacaoAvaliacaoDomainService>();
        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();
        private MensagemDomainService MensagemDomainService => Create<MensagemDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        #endregion DomainServices

        public AplicacaoAvaliacaoEntregaOnlineVO BuscarEntregasOnline(long seqAplicacaoAvaliacao)
        {
            return AplicacaoAvaliacaoDomainService.SearchProjectionByKey(seqAplicacaoAvaliacao, x => new AplicacaoAvaliacaoEntregaOnlineVO
            {
                SeqAplicacaoAvaliacao = x.Seq,
                DescricaoOrigemAvaliacao = x.OrigemAvaliacao.Descricao,
                Descricao = x.Avaliacao.Descricao,
                DataFim = x.DataFimAplicacaoAvaliacao,
                DataInicio = x.DataInicioAplicacaoAvaliacao,
                QuantidadeMaximaPessoasGrupo = x.QuantidadeMaximaPessoasGrupo,
                Sigla = x.Sigla,
                Valor = x.Avaliacao.Valor,
                Instrucao = x.Avaliacao.Instrucao,
                SeqArquivoAnexadoInstrucao = x.Avaliacao.SeqArquivoAnexadoInstrucao,
                UidArquivoAnexadoInstrucao = x.Avaliacao.ArquivoAnexadoInstrucao.UidArquivo,
                SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                EntregasOnline = x.EntregasOnline.Select(e => new EntregaOnlineVO
                {
                    DataEntrega = e.DataEntrega,
                    Seq = e.Seq,
                    Observacao = e.Observacao,
                    SeqArquivoAnexado = e.SeqArquivoAnexado,
                    UidArquivoAnexado = e.ArquivoAnexado.UidArquivo,
                    Conteudo = e.ArquivoAnexado.Conteudo,
                    Participantes = e.Participantes.Select(p => new EntregaOnlineParticipanteVO
                    {
                        Seq = p.Seq,
                        SeqAlunoHistorico = p.SeqAlunoHistorico,
                        Comentario = x.ApuracoesAvaliacao.FirstOrDefault(a => a.SeqAlunoHistorico == p.SeqAlunoHistorico).ComentarioApuracao,
                        Nota = x.ApuracoesAvaliacao.FirstOrDefault(a => a.SeqAlunoHistorico == p.SeqAlunoHistorico).Nota,
                        NomeAluno = (p.AlunoHistorico.Aluno.DadosPessoais.NomeSocial ?? p.AlunoHistorico.Aluno.DadosPessoais.Nome).ToUpper(),
                        NumeroRA = p.AlunoHistorico.Aluno.NumeroRegistroAcademico,
                        ResponsavelEntrega = p.ResponsavelEntrega,
                        ResponsavelEntregaLegenda = p.ResponsavelEntrega ? ResponsavelEntregaOnline.Sim : ResponsavelEntregaOnline.Nao
                    }).ToList(),
                    SituacaoEntrega = e.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoEntregaOnline,
                    CodigoProtocolo = e.CodigoProtocolo,
                }).ToList()
            });
        }

        /// <summary>
        /// Salvar a liberação da entrega para correção
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial Entrega Online</param>
        public void SalvarLiberarCorrecao(long seqEntregaOnline)
        {
            EntregaOnlineHistoricoSituacao entregaOnlineHistoricoSituacao = new EntregaOnlineHistoricoSituacao()
            {
                SeqEntregaOnline = seqEntregaOnline,
                SituacaoEntregaOnline = SituacaoEntregaOnline.LiberadoParaCorrecao
            };

            EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistoricoSituacao);
        }

        /// <summary>
        /// Salvar solicitacao de liberação para nova entrega
        /// </summary>
        /// <param name="seqEntregaOnline">Sequencial de entrega online</param>
        /// <param name="observacao">Observação</param>
        public void SalvarSolicitarLiberarNovaEntrega(long seqEntregaOnline, string observacao)
        {
            EntregaOnlineHistoricoSituacao entregaOnlineHistoricoSituacao = new EntregaOnlineHistoricoSituacao()
            {
                SeqEntregaOnline = seqEntregaOnline,
                SituacaoEntregaOnline = SituacaoEntregaOnline.SolicitadoNovaEntrega,
                Observacao = observacao
            };

            EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistoricoSituacao);
        }

        public (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarLiberacaoNovaEntrega(long seqEntregaOnline, string observacao)
        {
            // Verifica se está nas condições corretas para permitir criar novo histórico
            var dadosEntrega = this.SearchProjectionByKey(seqEntregaOnline, x => new
            {
                SituacaoAtual = x.SituacaoAtual.SituacaoEntregaOnline,
                DataInicio = x.AplicacaoAvaliacao.DataInicioAplicacaoAvaliacao,
                DataFim = x.AplicacaoAvaliacao.DataFimAplicacaoAvaliacao,
                SeqOrigemAvaliacao = x.AplicacaoAvaliacao.SeqOrigemAvaliacao,
                SeqAplicacaoAvaliacao = x.SeqAplicacaoAvaliacao
            });

            if (dadosEntrega != null)
            {
                if (dadosEntrega.DataInicio <= DateTime.Now && dadosEntrega.DataFim >= DateTime.Now)
                {
                    if (dadosEntrega.SituacaoAtual == SituacaoEntregaOnline.LiberadoParaCorrecao || dadosEntrega.SituacaoAtual == SituacaoEntregaOnline.SolicitadoNovaEntrega)
                    {
                        // Tudo ok, cria o novo histórico
                        var entregaOnlineHistorico = new EntregaOnlineHistoricoSituacao
                        {
                            SeqEntregaOnline = seqEntregaOnline,
                            SituacaoEntregaOnline = SituacaoEntregaOnline.Entregue,
                            Observacao = observacao
                        };
                        EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistorico);

                        return (dadosEntrega.SeqAplicacaoAvaliacao, dadosEntrega.SeqOrigemAvaliacao);
                    }
                    else
                        throw new SMCApplicationException("A situação atual da avaliação não permite que seja liberada para uma nova entrega.");
                }
                else
                    throw new SMCApplicationException("Não é possível liberar uma nova entrega pois a data atual não está no período de aplicação desta avaliação.");
            }
            else
                throw new SMCApplicationException("Entrega online não encontrada");
        }

        public (long SeqAplicacaoAvaliacao, long SeqOrigemAvaliacao) SalvarNegacaoNovaEntrega(long seqEntregaOnline, string observacao)
        {
            // Verifica se está nas condições corretas para permitir criar novo histórico
            var dadosEntrega = this.SearchProjectionByKey(seqEntregaOnline, x => new
            {
                SituacaoAtual = x.SituacaoAtual.SituacaoEntregaOnline,
                SeqOrigemAvaliacao = x.AplicacaoAvaliacao.SeqOrigemAvaliacao,
                SeqAplicacaoAvaliacao = x.SeqAplicacaoAvaliacao
            });

            if (dadosEntrega != null)
            {
                if (dadosEntrega.SituacaoAtual == SituacaoEntregaOnline.SolicitadoNovaEntrega)
                {
                    // Tudo ok, cria o novo histórico
                    var entregaOnlineHistorico = new EntregaOnlineHistoricoSituacao
                    {
                        SeqEntregaOnline = seqEntregaOnline,
                        SituacaoEntregaOnline = SituacaoEntregaOnline.LiberadoParaCorrecao,
                        Observacao = observacao
                    };
                    EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistorico);

                    return (dadosEntrega.SeqAplicacaoAvaliacao, dadosEntrega.SeqOrigemAvaliacao);
                }
                else
                    throw new SMCApplicationException("A situação atual da avaliação não permite que seja negada a liberação para uma nova entrega.");
            }
            else
                throw new SMCApplicationException("Entrega online não encontrada");
        }

        public FileInfo BuscarArquivosEntregasOnline(List<long> seqsEntregasOnline)
        {
            var arquivos = this.SearchProjectionBySpecification(new SMCContainsSpecification<EntregaOnline, long>(x => x.Seq, seqsEntregasOnline.ToArray()), x => new
            {
                SituacaoEntregaOnline = x.SituacaoAtual.SituacaoEntregaOnline,
                DataFimAplicacaoAvaliacao = (DateTime?)x.AplicacaoAvaliacao.DataFimAplicacaoAvaliacao,
                SeqArquivoAnexado = (long?)x.SeqArquivoAnexado
            });

            List<long> seqsArquivosBaixar = new List<long>();
            foreach (var arquivo in arquivos)
            {
                if (arquivo != null && arquivo.SeqArquivoAnexado.HasValue)
                {
                    if (arquivo.SituacaoEntregaOnline == Common.Areas.APR.Enums.SituacaoEntregaOnline.LiberadoParaCorrecao ||
                        arquivo.SituacaoEntregaOnline == Common.Areas.APR.Enums.SituacaoEntregaOnline.Corrigido ||
                        (arquivo.DataFimAplicacaoAvaliacao < DateTime.Now))
                        seqsArquivosBaixar.Add(arquivo.SeqArquivoAnexado.Value);
                }
            }

            if (!seqsArquivosBaixar.Any())
                throw new SMCApplicationException("Nenhum arquivo está disponível para download na seleção informada.");

            return ArquivoAnexadoDomainService.BuscarECompactarArquivos(seqsArquivosBaixar);
        }

        public void SalvarAvaliacao(AplicacaoAvaliacaoEntregaOnlineVO entregaOnlineVO)
        {
            using (var transaction = SMCUnitOfWork.Begin())
            {
                // Percorre as entregas online para verificar se possui ou não notas lançadas
                if (entregaOnlineVO != null && entregaOnlineVO.EntregasOnline != null && entregaOnlineVO.EntregasOnline.Any())
                {
                    foreach (var entrega in entregaOnlineVO.EntregasOnline)
                    {
                        // Caso esteja em situação de liberado para correção ou caso já tenha ultrapassado a data final para entrega, permite alteração da avaliação
                        if (entrega.SituacaoEntrega == SituacaoEntregaOnline.LiberadoParaCorrecao || entrega.SituacaoEntrega == SituacaoEntregaOnline.Corrigido || entregaOnlineVO.DataFim <= DateTime.Now)
                        {
                            int totalAvaliacoes = 0;
                            if (entrega.Participantes != null && entrega.Participantes.Any())
                            {
                                // Verifica cada participante
                                foreach (var participante in entrega.Participantes)
                                {
                                    // Recupera a apuracaoAvaliacao caso exista
                                    var apuracaoAvaliacao = ApuracaoAvaliacaoDomainService.SearchByKey(new ApuracaoAvaliacaoFilterSpecification
                                    {
                                        SeqAlunoHistorico = participante.SeqAlunoHistorico,
                                        SeqAplicacaoAvaliacao = entregaOnlineVO.SeqAplicacaoAvaliacao,
                                    });

                                    if (apuracaoAvaliacao != null || participante.Nota.HasValue)
                                    {
                                        // Busca os dados do aluno
                                        var seqAluno = AlunoHistoricoDomainService.SearchProjectionByKey(participante.SeqAlunoHistorico, x => x.SeqAluno);
                                        var dadosAluno = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, true);

                                        // descrição da turma
                                        var dadosOrigemAvaliacao = AplicacaoAvaliacaoDomainService.SearchProjectionByKey(entregaOnlineVO.SeqAplicacaoAvaliacao, x => new
                                        {
                                            SeqOrigemAvaliacao = x.SeqOrigemAvaliacao,
                                            TipoOrigemAvaliacao = x.OrigemAvaliacao.TipoOrigemAvaliacao
                                        });

                                        // Busca o SeqTurma da divisão ou da turma
                                        long? seqTurma = null;
                                        if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
                                            seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(new DivisaoTurmaFilterSpecification { SeqOrigemAvaliacao = dadosOrigemAvaliacao.SeqOrigemAvaliacao }, x => x.SeqTurma);
                                        else
                                            seqTurma = TurmaDomainService.SearchProjectionByKey(new TurmaFilterSpecification { SeqOrigemAvaliacao = dadosOrigemAvaliacao.SeqOrigemAvaliacao }, x => x.Seq);

                                        var descricaoTurma = TurmaDomainService.BuscarDescricaoTurmaConcatenado(seqTurma.GetValueOrDefault(), dadosAluno.SeqMatrizCurricularOferta);

                                        if (participante.Nota.HasValue)
                                        {
                                            // Tem nota. Verifica se já existe a avaliação online do aluno. Caso positivo, altera. Caso negativo, cria uma nova.
                                            if (apuracaoAvaliacao == null)
                                                apuracaoAvaliacao = new ApuracaoAvaliacao
                                                {
                                                    SeqAlunoHistorico = participante.SeqAlunoHistorico,
                                                    SeqAplicacaoAvaliacao = entregaOnlineVO.SeqAplicacaoAvaliacao
                                                };

                                            bool salvarLinhaTempo = apuracaoAvaliacao.Nota != participante.Nota;

                                            apuracaoAvaliacao.Nota = participante.Nota;
                                            apuracaoAvaliacao.ComentarioApuracao = participante.Comentario;
                                            apuracaoAvaliacao.Comparecimento = true;

                                            ApuracaoAvaliacaoDomainService.SaveEntity(apuracaoAvaliacao);

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
                                            if (salvarLinhaTempo)
                                            {
                                                Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
                                                dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.NOTA_AVALIACAO, participante.Nota.ToString());
                                                dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.TURMA, descricaoTurma);
                                                dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.AVALIACAO, $"{entregaOnlineVO.Sigla} - {entregaOnlineVO.Descricao}");

                                                MensagemDomainService.EnviarMensagemPessoaAtuacao(seqAluno,
                                                                                                    dadosAluno.SeqInstituicaoEnsino,
                                                                                                    dadosAluno.SeqNivelEnsino,
                                                                                                    TOKEN_TIPO_MENSAGEM.NOTA_AVALIACAO,
                                                                                                    CategoriaMensagem.LinhaDoTempo,
                                                                                                    dicTagsMsg);
                                            }

                                            totalAvaliacoes++;
                                        }
                                        else if (apuracaoAvaliacao != null)
                                        {
                                            // Professor removeu a nota do aluno, envia outra notificação
                                            Dictionary<string, string> dicTagsMsg = new Dictionary<string, string>();
                                            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.TURMA, descricaoTurma);
                                            dicTagsMsg.Add(TOKEN_TAG_MENSAGEM.AVALIACAO, $"{entregaOnlineVO.Sigla} - {entregaOnlineVO.Descricao}");

                                            MensagemDomainService.EnviarMensagemPessoaAtuacao(seqAluno,
                                                                                                dadosAluno.SeqInstituicaoEnsino,
                                                                                                dadosAluno.SeqNivelEnsino,
                                                                                                TOKEN_TIPO_MENSAGEM.NOTA_AVALIACAO_EXCLUIDA,
                                                                                                CategoriaMensagem.LinhaDoTempo,
                                                                                                dicTagsMsg);

                                            // Não tem nota. Apaga a avaliação online para este aluno
                                            ApuracaoAvaliacaoDomainService.DeleteEntity(apuracaoAvaliacao);
                                        }
                                    }
                                }

                                // Altera a situação para corrigido caso tenha sido definido nota para todos os alunos ou volta o histórico anterior caso o atual seja corrigido
                                if (totalAvaliacoes > 0 && totalAvaliacoes == entrega.Participantes.Count)
                                {
                                    var entregaOnlineHistorico = new EntregaOnlineHistoricoSituacao
                                    {
                                        SeqEntregaOnline = entrega.Seq,
                                        SituacaoEntregaOnline = SituacaoEntregaOnline.Corrigido,
                                    };
                                    EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistorico);
                                }
                                else
                                {
                                    if (entrega.SituacaoEntrega == Common.Areas.APR.Enums.SituacaoEntregaOnline.Corrigido)
                                    {
                                        // Recupera o histórico anterior ao corrigido
                                        var historicoAnterior = this.SearchProjectionByKey(entrega.Seq, x => x.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault(h => h.SituacaoEntregaOnline != SituacaoEntregaOnline.Corrigido));

                                        // Copia o histórico
                                        var entregaOnlineHistorico = new EntregaOnlineHistoricoSituacao
                                        {
                                            SeqEntregaOnline = entrega.Seq,
                                            SituacaoEntregaOnline = historicoAnterior.SituacaoEntregaOnline,
                                        };
                                        EntregaOnlineHistoricoSituacaoDomainService.SaveEntity(entregaOnlineHistorico);
                                    }
                                }
                            }
                        }
                    }
                }

                transaction.Commit();
            }
        }

        public HistoricoSituacaoEntregaOnlineVO BuscarHistoricoSituacaoEntrega(long seqEntregaOnline)
        {
            return this.SearchProjectionByKey(seqEntregaOnline, x => new HistoricoSituacaoEntregaOnlineVO
            {
                SeqEntregaOnline = x.Seq,
                Alunos = x.Participantes.OrderBy(p => p.AlunoHistorico.Aluno.DadosPessoais.NomeSocial ?? p.AlunoHistorico.Aluno.DadosPessoais.Nome).Select(p => p.AlunoHistorico.Aluno.NumeroRegistroAcademico + " - " + (p.AlunoHistorico.Aluno.DadosPessoais.NomeSocial ?? p.AlunoHistorico.Aluno.DadosPessoais.Nome).ToUpper()).ToList(),
                DataInicio = x.AplicacaoAvaliacao.DataInicioAplicacaoAvaliacao,
                DataFim = x.AplicacaoAvaliacao.DataFimAplicacaoAvaliacao,
                Descricao = x.AplicacaoAvaliacao.Avaliacao.Descricao,
                Sigla = x.AplicacaoAvaliacao.Sigla,
                Situacoes = x.HistoricosSituacao.OrderByDescending(h => h.DataInclusao).Select(h => new HistoricoSituacaoEntregaOnlineItemVO
                {
                    Data = h.DataInclusao,
                    NomeResponsavel = h.UsuarioInclusao,
                    Seq = h.Seq,
                    Observacao = h.Observacao,
                    SituacaoEntregaOnline = h.SituacaoEntregaOnline
                }).ToList()
            });
        }

        /// <summary>
        /// Salvar entrega online aluno
        /// </summary>
        /// <param name="entregaOnline">Dados da entrega</param>
        /// <returns>Sequencial da entrega</returns>
        public long SalvarEntregaOnline(EntregaOnlineVO entregaOnlineVO)
        {
            EntregaOnline entregaOnline = new EntregaOnline();
            DateTime dataAtual = DateTime.Now;

            var entregasEfetuadas = BuscarEntregasOnline(entregaOnlineVO.SeqAplicacaoAvaliacao);

            /*Verificar se algum dos alunos informados na entrega da avaliação já é participante de outra entrega
            da mesma aplicação de avaliação.Se ocorrer, abortar a operação e apresentar mensagem de erro:
            "O aluno <RA - nome aluno> já realizou a entrega desta avaliação em outro grupo."
            Desde que não seja a entrega que ele pertence*/
            foreach (var item in entregasEfetuadas.EntregasOnline.Where(w => w.Seq != entregaOnlineVO.Seq).ToList())
            {
                foreach (var participante in entregaOnlineVO.Participantes)
                {
                    if (item.Participantes.Any(a => a.SeqAlunoHistorico == participante.SeqAlunoHistorico))
                    {
                        var aluno = item.Participantes.FirstOrDefault(f => f.SeqAlunoHistorico == participante.SeqAlunoHistorico);
                        throw new EntregaOnlineParticipaEmOutraEntregaException($"{aluno.NumeroRA} - {aluno.NomeAluno}");
                    }
                }
            }

            /*Caso a data corrente do sistema NÃO esteja entre a data de inicio e fim definido na aplicação da
            avaliação, abortar a operação e apresentar mensagem de erro:*/
            if (!(dataAtual >= entregasEfetuadas.DataInicio && dataAtual <= entregasEfetuadas.DataFim.Value))
            {
                throw new EntregaOnlineEncerradaException();
            }

            /*Caso seja uma alteração da entrega e a situação da entrega seja diferente de "Entregue", abortar a
            operação e apresentar mensagem de erro:
            "Entrega não é permitida. Avaliação já liberada para correção."*/
            if (entregaOnlineVO.Seq != 0)
            {
                if (entregasEfetuadas.EntregasOnline.FirstOrDefault(f => f.Seq == entregaOnlineVO.Seq).SituacaoEntrega != SituacaoEntregaOnline.Entregue)
                {
                    throw new EntregaOnlineSituacaoEntregaException();
                }
            }

            /*Salvar nome do arquivo como: < sequencial da origem avaliação > +"_" + < sigla aplicacao
            avaliação > +"_" + < nome do aluno responsavel entrega retirando espaços>.Manter a extensão do
            arquivo que foi postado.
            ex. 11781_P1_AndersonBarbosaCoutinho.doc*/
            long seqAlunoHistoricoResonsavel = entregaOnlineVO.Participantes.FirstOrDefault(f => f.ResponsavelEntrega).SeqAlunoHistorico;
            string extensaoArquivo = Path.GetExtension(entregaOnlineVO.ArquivoAnexado.Name);
            string nomeAluno = AlunoHistoricoDomainService.SearchProjectionByKey(new SMCSeqSpecification<AlunoHistorico>(seqAlunoHistoricoResonsavel), p => p.Aluno.DadosPessoais.NomeSocial ?? p.Aluno.DadosPessoais.Nome);
            string nomeArquivo = $"{entregaOnlineVO.SeqAplicacaoAvaliacao.ToString()}_{entregasEfetuadas.Sigla}_{nomeAluno.Replace(" ", "")}{extensaoArquivo}";
            
            entregaOnlineVO.ArquivoAnexado.Name = nomeArquivo;
            entregaOnlineVO.DataEntrega = DateTime.Now;
            entregaOnlineVO.SituacaoEntrega = SituacaoEntregaOnline.Entregue;
            entregaOnlineVO.CodigoProtocolo = Guid.NewGuid();

            entregaOnline = entregaOnlineVO.Transform<EntregaOnline>();
            entregaOnline.ArquivoAnexado.Conteudo = entregaOnlineVO.ArquivoAnexado.FileData;

            //Tratamento para inserir um novo historico de situação
            if (entregaOnline.Seq == 0)
            {
                entregaOnline.HistoricosSituacao = new List<EntregaOnlineHistoricoSituacao>();
            }
            else
            {
                entregaOnline.HistoricosSituacao = EntregaOnlineHistoricoSituacaoDomainService.SearchBySpecification(new EntregaOnlineHistoricoSituacaoFilterSpecification()
                { SeqEntregaOnline = entregaOnlineVO.Seq }).ToList();

                // Quando o arquivo não foi alterado, o Conteudo fica nulo. Tratamento para preencher este campo
                if (entregaOnline.ArquivoAnexado.Conteudo == null)
                {
                    var entregaOnlineAlterada = entregasEfetuadas.EntregasOnline.FirstOrDefault(c => c.Seq == entregaOnlineVO.Seq);
                    if (entregaOnlineAlterada != null)
                    {
                        entregaOnline.ArquivoAnexado.Conteudo = entregaOnlineAlterada.Conteudo;
                    }
                }
            }

            entregaOnline.HistoricosSituacao.Add(new EntregaOnlineHistoricoSituacao
            {
                SituacaoEntregaOnline = SituacaoEntregaOnline.Entregue
            });

            SaveEntity(entregaOnline);

            return entregaOnline.Seq;
        }
    }
}