using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Helpers;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class AvaliacaoDomainService : AcademicoContextDomain<Avaliacao>
    {
        #region [ Services ]

        public IEventoService EventoService => Create<IEventoService>();

        #endregion [ Services ]

        #region [DomainService]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();
        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();

        private AplicacaoAvaliacaoDomainService AplicacaoAvaliacaoDomainService => Create<AplicacaoAvaliacaoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private InstituicaoNivelTipoEventoDomainService InstituicaoNivelTipoEventoDomainService => Create<InstituicaoNivelTipoEventoDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private EntregaOnlineDomainService EntregaOnlineDomainService => Create<EntregaOnlineDomainService>();

        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService => Create<ApuracaoAvaliacaoDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        #endregion [DomainService]

        /// <summary>
        /// Salvar avaliacao
        /// </summary>
        /// <param name="avaliacao">Dados da avaliacao</param>
        /// <returns>Sequencial da avaliação</returns>
        public long SalvarAvaliacao(AvaliacaoVO avaliacao)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                /*1) Antes de salvar, consistir se a soma de todas as avaliações cadastradas para a origem de avaliação
                é menor ou igual ao valor da nota máxima configurada na origem de avaliação.
                Se ultrapassar o valor máximo, abortar a operação e apresentar a mensagem de erro:
                "Operação não permitida. A soma do valor de todas as avaliações cadastradas para a turma ultrapassa
                < valor máximo >."*/
                OrigemAvaliacao origemAvaliacao = OrigemAvaliacaoDomainService.BuscarOrigemAvaliacao(avaliacao.AplicacoesAvaliacao.FirstOrDefault().SeqOrigemAvaliacao);
                List<AplicacaoAvaliacao> aplicacaoAvaliacaos = AplicacaoAvaliacaoDomainService.BuscarAplicacoesAvaliacoes(new AplicacaoAvaliacaoFilterSpecification() { SeqOrigemAvaliacao = origemAvaliacao.Seq });
                int totalAvalicacoes = 0;

                if (avaliacao.Seq == 0)
                {
                    long? seqPessoaUsuarioSas = SMCContext.User.SMCGetSequencialUsuario();
                    avaliacao.SeqInstituicaoEnsino = PessoaDomainService.SearchBySpecification(new PessoaFilterSpecification() { SeqUsuarioSAS = seqPessoaUsuarioSas }).FirstOrDefault().SeqInstituicaoEnsino;
                    totalAvalicacoes = aplicacaoAvaliacaos.Sum(s => s.Avaliacao.Valor);
                }
                else
                    totalAvalicacoes = aplicacaoAvaliacaos.Where(w => w.Seq != avaliacao.AplicacoesAvaliacao.FirstOrDefault().Seq).Sum(s => s.Avaliacao.Valor);

                totalAvalicacoes += avaliacao.Valor;

                if (totalAvalicacoes > origemAvaliacao.NotaMaxima)
                    throw new AvaliacaoNotaMaximaExecedenteException(origemAvaliacao.NotaMaxima.ToString());

                //Modo Edit
                Avaliacao avaliacaoBD = BuscarAvaliacao(avaliacao.Seq);
                if (avaliacao.Seq > 0)
                {
                    /*2) Se houver nota lançada não permitir alterar o valor da avaliação.
                    Exibir mensagem "Alteração de valor não permitida. Já existe nota lançada"*/
                    if (avaliacao.Valor != avaliacaoBD.Valor)
                    {
                        if (avaliacaoBD.AplicacoesAvaliacao.SelectMany(s => s.ApuracoesAvaliacao).SMCAny())
                            throw new AvaliacaoNotaLancadaException();
                    }

                    //3) Se avaliação com entrega online = sim
                    if (avaliacao.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb != avaliacaoBD.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb)
                    {
                        if (!avaliacao.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb)
                        {
                            /*-se já existir arquivo entregue não permitir mudar para entrega online = não
                            Exibir mensagem "Alteração formato de entrega não permitido. Já existe arquivo entregue."*/
                            if (avaliacaoBD.AplicacoesAvaliacao.FirstOrDefault().EntregasOnline.SMCAny())
                                throw new AvaliacaoAlterarFormatoEntregaException();
                        }
                    }
                }

                // Task 37527: Permitir data anterior caso não seja entrega online.
                if (avaliacao.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb)
                {
                    /*- não permitir alterar data início ou data fim para uma data anterior a data atual
                    Exibir mensagem "Alteração de data não permitida. Não é possível incluir data menor que a atual."*/
                    foreach (var itemAplicacao in avaliacao.AplicacoesAvaliacao)
                    {
                        //Caso seja uma edição valida se houve uma alteração de dados na data de inicio ou fim
                        if (avaliacao.Seq > 0)
                        {
                            AplicacaoAvaliacao aplicacaoAvaliacaoBD = avaliacaoBD.AplicacoesAvaliacao.FirstOrDefault(w => w.Seq == itemAplicacao.Seq);

                            if ((itemAplicacao.DataInicioAplicacaoAvaliacao != aplicacaoAvaliacaoBD.DataInicioAplicacaoAvaliacao
                                && itemAplicacao.DataInicioAplicacaoAvaliacao < DateTime.Now)
                                || (itemAplicacao.DataFimAplicacaoAvaliacao != aplicacaoAvaliacaoBD.DataFimAplicacaoAvaliacao
                                    && itemAplicacao.DataFimAplicacaoAvaliacao < DateTime.Now))
                            {
                                throw new AvaliacaoDataNaoPermitidaException("Alteração");
                            }
                        }
                        else
                        {
                            if (itemAplicacao.DataInicioAplicacaoAvaliacao < DateTime.Now
                                || itemAplicacao.DataFimAplicacaoAvaliacao < DateTime.Now)
                            {
                                throw new AvaliacaoDataNaoPermitidaException("Inclusão");
                            }
                        }
                    }
                }

                var dadosAplicacao = avaliacao.AplicacoesAvaliacao.FirstOrDefault();
                if (dadosAplicacao.SeqAgendaTurma.HasValue && dadosAplicacao.SeqAgendaTurma > 0)
                {
                    var descricaoOrigemAvaliacao = this.OrigemAvaliacaoDomainService.BuscarDescricaoOrigemAvaliacao(origemAvaliacao.Seq);
                    var separarDescricao = descricaoOrigemAvaliacao.Split(new string[] { "<br />" }, StringSplitOptions.None);
                    string nomeAgd = string.Empty;
                    //Foi ajustado para remover a descrição da origem aviação conforme regra de validação
                    if (separarDescricao.Length > 1)
                    {
                        var separarCodigo = separarDescricao[1].Split(' ');
                        nomeAgd = $"{separarCodigo[0].Trim()} - {avaliacao.AplicacoesAvaliacao.FirstOrDefault().Sigla} - {avaliacao.Descricao}";
                    }
                    else
                    {
                        string[] separacaoCodgiTurma = descricaoOrigemAvaliacao.Split('-');
                        nomeAgd = $"{separacaoCodgiTurma[0].Trim()} - {avaliacao.AplicacoesAvaliacao.FirstOrDefault().Sigla} - {avaliacao.Descricao}";
                    }
                    //Truncar o nome do evento para não ocorrer erro no AGD
                    nomeAgd = nomeAgd.SMCTruncate(254);

                    //Integração com AGD
                    var seqTipoEventoAgd = InstituicaoNivelTipoEventoDomainService.BuscarSeqTipoEventoAgdPorTipoAvaliacao(avaliacao.TipoAvaliacao, origemAvaliacao.Seq);

                    var eventoAgd = EventoService.SalvarEvento(new EventoData()
                    {
                        Seq = dadosAplicacao.SeqEventoAgd.GetValueOrDefault(),
                        SeqAgenda = dadosAplicacao.SeqAgendaTurma.GetValueOrDefault(),
                        Nome = nomeAgd,
                        SeqTipoEvento = seqTipoEventoAgd,
                        DataInicio = dadosAplicacao.DataInicioAplicacaoAvaliacao,
                        DataFim = dadosAplicacao.DataFimAplicacaoAvaliacao.HasValue ? dadosAplicacao.DataFimAplicacaoAvaliacao.Value : dadosAplicacao.DataInicioAplicacaoAvaliacao,
                        EventoDiaInteiro = false,
                        Local = dadosAplicacao.Local,
                        EventoParticular = false,
                        CodigoLocalSEF = dadosAplicacao.CodigoLocalSef,
                    });
                    AGDHelper.TratarErroAGD(eventoAgd);
                    avaliacao.AplicacoesAvaliacao[0].SeqEventoAgd = eventoAgd.SeqEvento;
                }

                var avaliacaoSalvar = avaliacao.Transform<Avaliacao>();
                SaveEntity(avaliacaoSalvar);

                transacao.Commit();

                return avaliacaoSalvar.Seq;
            }
        }

        /// <summary>
        /// Buscar avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avaliação</param>
        /// <returns>Modelo da avaliação</returns>
        public Avaliacao BuscarAvaliacao(long seq)
        {
            var include = IncludesAvaliacao.AplicacoesAvaliacao | IncludesAvaliacao.AplicacoesAvaliacao_ApuracoesAvaliacao | IncludesAvaliacao.ArquivoAnexadoInstrucao | IncludesAvaliacao.AplicacoesAvaliacao_EntregasOnline;
            Avaliacao retorno = SearchByKey(new SMCSeqSpecification<Avaliacao>(seq), include);

            return retorno;
        }


        /// <summary>
        /// Inicia o preenchimento dos dados para criação de uma nova avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <returns>Objeto com os dados iniciais preenchidos</returns>
        public AvaliacaoEditarVO PreencherDadosNovaAvaliacao(long seqOrigemAvaliacao)
        {
            // Recupera os dados da origem de avaliação
            var dadosOrigemAvaliacao = OrigemAvaliacaoDomainService.SearchProjectionByKey(seqOrigemAvaliacao, x => new
            {
                TipoOrigemAvaliacao = x.TipoOrigemAvaliacao,
                TipoEscalaApuracao = (TipoEscalaApuracao?)x.EscalaApuracao.TipoEscalaApuracao,
            });

            // Cria o retorno
            var avaliacao = new AvaliacaoEditarVO
            {
                SeqOrigemAvaliacao = seqOrigemAvaliacao,
                TipoOrigemAvaliacao = dadosOrigemAvaliacao.TipoOrigemAvaliacao,
                TipoEscalaApuracao = dadosOrigemAvaliacao.TipoEscalaApuracao
            };

            PreencherDadosAvaliacao(avaliacao);

            return avaliacao;
        }

        public AvaliacaoEditarVO BuscarAvaliacaoEdicao(long seq)
        {
            // Busca os dados para edição
            var avaliacao = this.SearchProjectionByKey(seq, x => new AvaliacaoEditarVO
            {
                ArquivoAnexadoInstrucao = x.ArquivoAnexadoInstrucao,
                DataInicioAplicacaoAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().DataInicioAplicacaoAvaliacao,
                DataFimAplicacaoAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().DataFimAplicacaoAvaliacao,
                Descricao = x.Descricao,
                EntregaWeb = x.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb,
                EntregaWebInBD = x.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb,
                Local = x.AplicacoesAvaliacao.FirstOrDefault().Local,
                QuantidadeMaximaPessoasGrupo = x.AplicacoesAvaliacao.FirstOrDefault().QuantidadeMaximaPessoasGrupo,
                Instrucao = x.Instrucao,
                LocalSEF = x.AplicacoesAvaliacao.FirstOrDefault().CodigoLocalSef,
                Seq = x.Seq,
                //SeqAplicacaoAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().Seq,
                SeqArquivoAnexadoInstrucao = x.SeqArquivoAnexadoInstrucao,
                SeqFimGradeAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().SeqEventoAulaFim,
                SeqInicioGradeAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().SeqEventoAulaInicio,
                //SeqInstituicaoEnsino = x.SeqInstituicaoEnsino.Value,
                SeqOrigemAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().SeqOrigemAvaliacao,
                Sigla = x.AplicacoesAvaliacao.FirstOrDefault().Sigla,
                Valor = x.Valor,
                TipoOrigemAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().OrigemAvaliacao.TipoOrigemAvaliacao,
                TipoEscalaApuracao = x.AplicacoesAvaliacao.FirstOrDefault().OrigemAvaliacao.EscalaApuracao.TipoEscalaApuracao,
                TipoAvaliacao = x.TipoAvaliacao,
                SeqAplicacaoAvaliacao = x.AplicacoesAvaliacao.FirstOrDefault().Seq,
                SeqEventoAgd = x.AplicacoesAvaliacao.FirstOrDefault().SeqEventoAgd
            });

            PreencherDadosAvaliacao(avaliacao);
            return avaliacao;
        }

        void PreencherDadosAvaliacao(AvaliacaoEditarVO avaliacao)
        {
            // Preenche o código de unidade
            avaliacao.CodigoUnidade = EntidadeDomainService.RecuperarCodigoUnidadeSeoPorSeqOrigemAvaliacao(avaliacao.SeqOrigemAvaliacao);

            // Carrega os dados da turma ou divisão
            if (avaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                var dadosTurma = TurmaDomainService.SearchProjectionByKey(new TurmaFilterSpecification { SeqOrigemAvaliacao = avaliacao.SeqOrigemAvaliacao }, x => new
                {
                    x.SeqAgendaTurma,
                    x.SeqCicloLetivoInicio,
                    x.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqCursoOfertaLocalidadeTurno,
                    x.DataInicioPeriodoLetivo,
                    x.DataFimPeriodoLetivo
                });

                var datasLimiteAvaliacao = ValidarDatasLimiteAvaliacao(dadosTurma.SeqCicloLetivoInicio,
                                                                       dadosTurma.SeqCursoOfertaLocalidadeTurno,
                                                                       dadosTurma.DataInicioPeriodoLetivo,
                                                                       dadosTurma.DataFimPeriodoLetivo);

                avaliacao.SeqAgendaTurma = dadosTurma.SeqAgendaTurma;
                avaliacao.TemConfiguracaoGrade = false;
                avaliacao.DataInicioLimiteAvaliacao = datasLimiteAvaliacao.dataInicioLimiteAvalicao;
                avaliacao.DataFimLimiteAvaliacao = datasLimiteAvaliacao.dataFimLimiteAvalicao;
            }
            else
            {
                //2) Se o tipo da origem de avaliação recebido como parâmetro for "Divisão de turma" e a divisão não tem configuração
                // de grade OU
                var dadosDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionByKey(new DivisaoTurmaFilterSpecification { SeqOrigemAvaliacao = avaliacao.SeqOrigemAvaliacao }, x => new
                {
                    x.Turma.SeqAgendaTurma,
                    x.Turma.SeqCicloLetivoInicio,
                    x.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqCursoOfertaLocalidadeTurno,
                    TemConfiguracaoGrade = x.HistoricoConfiguracaoGradeAtual != null,
                    x.Turma.DataInicioPeriodoLetivo,
                    x.Turma.DataFimPeriodoLetivo
                });

                var datasLimiteAvaliacao = ValidarDatasLimiteAvaliacao(dadosDivisaoTurma.SeqCicloLetivoInicio,
                                                                       dadosDivisaoTurma.SeqCursoOfertaLocalidadeTurno,
                                                                       dadosDivisaoTurma.DataInicioPeriodoLetivo,
                                                                       dadosDivisaoTurma.DataFimPeriodoLetivo);

                avaliacao.SeqAgendaTurma = dadosDivisaoTurma.SeqAgendaTurma;
                avaliacao.TemConfiguracaoGrade = dadosDivisaoTurma.TemConfiguracaoGrade;
                avaliacao.DataInicioLimiteAvaliacao = datasLimiteAvaliacao.dataInicioLimiteAvalicao;
                avaliacao.DataFimLimiteAvaliacao = datasLimiteAvaliacao.dataFimLimiteAvalicao;
            }
        }

        /// <summary>
        ///  Validar as datas limites da avaliação se embasando em qual for o maior periodo entre o ciclo letivo e periodo da turma. 
        /// </summary>
        /// <param name="seqCicloLetivoInicio">Sequencial ciclo letivo</param>
        /// <param name="SeqCursoOfertaLocalidadeTurno">Sequencial do curso oferta localidade turno</param>
        /// <param name="dataInicioPeriodoLetivo">Data inicio do periodo letivo da turma</param>
        /// <param name="dataFimPeriodoLetivo">Data fim do periodo letivo da turma</param>
        /// <returns>Data incio e fim limite da avaliação</returns>
        private (DateTime dataInicioLimiteAvalicao, DateTime dataFimLimiteAvalicao) ValidarDatasLimiteAvaliacao(long seqCicloLetivoInicio, long seqCursoOfertaLocalidadeTurno, DateTime dataInicioPeriodoLetivo, DateTime dataFimPeriodoLetivo)
        {
            (DateTime, DateTime)  retorno = (DateTime.Now.Date, DateTime.Now.Date);

            DatasEventoLetivoVO eventoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivoInicio, seqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
            int diasCicloLetivo = eventoLetivo.DataFim.Subtract(eventoLetivo.DataInicio).Days;
            int diasPeriodoLetivo = dataFimPeriodoLetivo.Subtract(dataInicioPeriodoLetivo).Days;

            retorno.Item1 = eventoLetivo.DataInicio;
            retorno.Item2 = eventoLetivo.DataFim;

            //De acordo com o bug 71249, foi incluido para permitir horários de avaliação até as 23:59 da data fim do período
            retorno.Item2 = retorno.Item2.AddDays(1).AddTicks(-1);

            if (diasPeriodoLetivo > diasCicloLetivo) {
                retorno.Item1 = dataInicioPeriodoLetivo;
                retorno.Item2 = dataFimPeriodoLetivo;
            }

            return retorno;
        }

        /// <summary>
        /// Lista de avaliações
        /// </summary>
        /// <param name="filtro">Filtros especificados</param>
        /// <returns>Lista avaliações</returns>
        public SMCPagerData<AvaliacaoVO> BuscarAvaliacoes(AvaliacaoFilterSpecification filtro)
        {
            int total;
            //var include = IncludesAvaliacao.AplicacoesAvaliacao;
            List<AvaliacaoVO> retorno = SearchProjectionBySpecification(filtro, x => new AvaliacaoVO
            {
                Seq = x.Seq,
                AvaliacaoGeral = x.AvaliacaoGeral,
                //Data =,
                AplicacoesAvaliacao = x.AplicacoesAvaliacao.Select(a => new AplicacaoAvaliacaoVO
                {
                    DataCancelamento = a.DataCancelamento,
                    DataInicioAplicacaoAvaliacao = a.DataInicioAplicacaoAvaliacao,
                    DataFimAplicacaoAvaliacao = a.DataFimAplicacaoAvaliacao,
                    EntregaWeb = a.EntregaWeb,
                    Sigla = a.Sigla,
                    Local = a.Local,
                    MotivoCancelamento = a.MotivoCancelamento,
                    Observacao = a.Observacao,
                    QuantidadeMaximaPessoasGrupo = a.QuantidadeMaximaPessoasGrupo,
                    Seq = a.Seq,
                    SeqAvaliacao = a.SeqAvaliacao,
                    SeqEventoAgd = a.SeqEventoAgd,
                    SeqOrigemAvaliacao = a.SeqOrigemAvaliacao,
                    SeqTipoEventoAgd = a.SeqTipoEventoAgd
                }).ToList(),
                Descricao = x.Descricao,
                Instrucao = x.Instrucao,
                Media = x.AplicacoesAvaliacao.SelectMany(a => a.ApuracoesAvaliacao).Where(a => a.Nota.HasValue).Average(a => a.Nota),
                SeqArquivoAnexadoInstrucao = x.SeqArquivoAnexadoInstrucao,
                SeqInstituicaoEnsino = x.SeqInstituicaoEnsino,
                Sigla = x.AplicacoesAvaliacao.FirstOrDefault().Sigla,
                TipoAvaliacao = x.TipoAvaliacao,
                TotalNotasLancadas = x.AplicacoesAvaliacao.SelectMany(a => a.ApuracoesAvaliacao).Count(a => a.Nota.HasValue || !a.Comparecimento),
                Valor = x.Valor
            }, out total).ToList();

            retorno = retorno.OrderBy(o => o.AplicacoesAvaliacao.FirstOrDefault().DataInicioAplicacaoAvaliacao).ToList();

            return new SMCPagerData<AvaliacaoVO>(retorno, total);
        }

        /// <summary>
        /// Deletar Avaliação
        /// </summary>
        /// <param name="seq">Sequencial da avalaiacão</param>
        public void DeleteAvalicao(long seq)
        {
            Avaliacao avaliacao = BuscarAvaliacao(seq);

            /*Se a avaliação que está sendo excluída possuir alguma lançamento de nota(apuração da avaliação)
            abortar a operação e exibir a mensagem de erro:*/
            List<ApuracaoAvaliacao> apuracoes = avaliacao.AplicacoesAvaliacao.SelectMany(s => s.ApuracoesAvaliacao).ToList();
            if (apuracoes.SMCAny())
            {
                throw new AvaliacaoExisteLancamentoNotaException();
            }

            //Se a avaliação que está sendo excluída possuir alguma ou entrega de arquivo já realizada, abortar a
            //operação e exibir a mensagem de erro:
            if (avaliacao.AplicacoesAvaliacao.FirstOrDefault().EntregasOnline.SMCAny())
            {
                throw new AvaliacaoExisteArquivoAvaliacaoException();
            }

            DeleteEntity(avaliacao);
        }

        /// <summary>
        /// Consultar avaliações da turma
        /// </summary>
        /// <param name="seqTurma">Sequencial da turma</param>
        ///<param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        ///<returns>Dados da consulta das avaliações</returns>
        public ConsultaAvaliacoesTurmaVO ConsultaAvaliacoes(long seqTurma, long seqPessoaAtuacao)
        {
            ConsultaAvaliacoesTurmaVO retorno = new ConsultaAvaliacoesTurmaVO() { };

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Coletagem de dados
            var dadosTurma = PlanoEstudoItemDomainService.SearchProjectionBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = seqPessoaAtuacao, SeqTurma = seqTurma, SomenteTurma = true }, p => new
            {
                Codigo = p.DivisaoTurma.Turma.Codigo,
                DescricaoConfiguracaoPrincipal = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).Descricao,
                DescricaoConfiguracao = p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).Descricao,
                Numero = p.DivisaoTurma.Turma.Numero,
                Seq = p.DivisaoTurma.Turma.Seq,
                SeqNivelEnsino = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                SeqNivelEnsinoPrincipal = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.NiveisEnsino.FirstOrDefault(w => w.Responsavel).SeqNivelEnsino,
                SeqOrigemAvaliacao = p.DivisaoTurma.Turma.SeqOrigemAvaliacao,
                SeqTipoComponenteCurricular = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.RestricoesTurmaMatriz.Any(r => r.SeqMatrizCurricularOferta == dadosOrigem.SeqMatrizCurricularOferta)).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqTipoComponenteCurricularPrincipal = (long?)p.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(w => w.Principal).ConfiguracaoComponente.ComponenteCurricular.SeqTipoComponenteCurricular,
                SeqAlunoHistorico = p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico,
                DiarioFechado = p.DivisaoTurma.Turma.HistoricosFechamentoDiario.Count > 0 ? p.DivisaoTurma.Turma.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false,
            }).FirstOrDefault();

            var avaliacoesTurma = ListaAvaliacoesOrigemAvalicao(dadosTurma.SeqOrigemAvaliacao);

            List<AvaliacaoVO> listaAvaliacoesTurma = new List<AvaliacaoVO>();

            foreach (var item in avaliacoesTurma)
            {
                if (item.TipoAvaliacao == TipoAvaliacao.Reavaliacao)
                {
                    listaAvaliacoesTurma.Add(item);
                }
            }

            //Dados da origem avaliação da turma
            var origemAvaliacaoTurma = OrigemAvaliacaoDomainService.BuscarOrigemAvaliacao(dadosTurma.SeqOrigemAvaliacao);

            var dadosDivisaoTurma = PlanoEstudoItemDomainService.SearchProjectionBySpecification(new PlanoEstudoItemFilterSpecification() { SeqAluno = seqPessoaAtuacao, SeqTurma = seqTurma, SomenteTurma = true }, p => new
            {
                Seq = p.SeqDivisaoTurma,
                CargaHoraria = p.DivisaoTurma.DivisaoComponente.CargaHoraria,
                DescricaoDivisaoTurma = p.DivisaoTurma.DivisaoComponente.ConfiguracaoComponente.Descricao,
                NumeroComponente = p.DivisaoTurma.DivisaoComponente.Numero,
                NumeroGrupo = p.DivisaoTurma.NumeroGrupo,
                SeqOriemAvaliacao = p.DivisaoTurma.SeqOrigemAvaliacao,
                TipoDivisaoDescricao = p.DivisaoTurma.DivisaoComponente.TipoDivisaoComponente.Descricao,
                SeqAlunoHistorico = p.PlanoEstudo.AlunoHistoricoCicloLetivo.SeqAlunoHistorico
            }).Distinct().ToList();

            List<AvaliacaoVO> listaAvaliacoesDivisaoTurma = new List<AvaliacaoVO>();

            foreach (var item in dadosDivisaoTurma)
            {
                var lista = ListaAvaliacoesOrigemAvalicao(item.SeqOriemAvaliacao);

                foreach (var avaliacao in lista)
                {
                    listaAvaliacoesDivisaoTurma.Add(avaliacao);
                }
            }

            //Montagem Retorno
            /*Desta forma, no nivel de turma, exibir a descrição da configuração de componente da turma que está na matriz do
            aluno(essa configuração está referenciada no plano de estudo item da matrícula do aluno)
            E no nível de divisão de turma, exibir a descrição conforme a RN_TUR_029 -Exibição Descrição Resumida Divisão
            Turma*/
            retorno.Descricao = !string.IsNullOrEmpty(dadosTurma.DescricaoConfiguracao) ? dadosTurma.DescricaoConfiguracao : dadosTurma.DescricaoConfiguracaoPrincipal;
            retorno.DivisoesTurma = new List<ConsultaAvaliacoesDivisaoTurmaVO>();

            var tiposComponenteNivel = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarInstituicaoNivelTipoComponenteCurricularConfiguracao(dadosTurma.SeqNivelEnsino ?? dadosTurma.SeqNivelEnsinoPrincipal ?? 0,
                                                                                                                                                        dadosTurma.SeqTipoComponenteCurricular ?? dadosTurma.SeqTipoComponenteCurricularPrincipal ?? 0);

            FormatoCargaHoraria? formatoCargaHoraria = null;

            if (tiposComponenteNivel != null)
            {
                formatoCargaHoraria = tiposComponenteNivel.FormatoCargaHoraria;
            }

            foreach (var item in dadosDivisaoTurma)
            {
                //Formatação Nome divisão
                //RN_TUR_023 - Exibição Codificação de turma
                var numeracaoDivisao = $"{dadosTurma.Codigo}.{dadosTurma.Numero}.{item.NumeroComponente}.{item.NumeroGrupo.ToString().PadLeft(3, '0')}";
                string nomeclaturaDivisao = string.Empty;

                if (!string.IsNullOrEmpty(item.TipoDivisaoDescricao))
                {
                    nomeclaturaDivisao += $" - {item.TipoDivisaoDescricao}";
                }

                if (item.CargaHoraria.HasValue)
                {
                    nomeclaturaDivisao += $" - {item.CargaHoraria}";
                }

                if (formatoCargaHoraria.HasValue && formatoCargaHoraria.Value != FormatoCargaHoraria.Nenhum)
                {
                    nomeclaturaDivisao += $" {SMCEnumHelper.GetDescription(formatoCargaHoraria.Value)}";
                }

                //Preencher retorno das avaliações da divisao de turma
                //Listar todas as avaliações da origem de avaliação da turma (ou da divisão de turma) recebida como parametro em
                //ordem de sigla da aplicação de avaliação.
                //Recupera as avaliações desta divisão na lista de avaliações pela origem de avaliação
                var avaliacoesDivisao = listaAvaliacoesDivisaoTurma.Where(w => w.AplicacoesAvaliacao.Select(s => s.SeqOrigemAvaliacao).Contains(item.SeqOriemAvaliacao)).ToList();
                //Cria lista de detalhes das avaliações para o grid de divisão turma
                List<DetalhesAvaliacaoVO> detalhesAvaliacaos = new List<DetalhesAvaliacaoVO>();
                int totalNotaDivisao = 0;
                decimal totalNotaAlunoDivisao = 0;
                int numeroAlaviacaoSemApuraco = 0;
                foreach (var avaliacao in avaliacoesDivisao)
                {
                    detalhesAvaliacaos.Add(MontarDetalhesAvaliacao(avaliacao, dadosTurma.SeqAlunoHistorico));
                    /*Calcular notas divisao turma
                    · Nota - Somatório de notas das apurações de avaliações cadastradas na origens de avaliação da divisão de turma
                    em questão e lançadas para o aluno logado.
                    · Valor - Somatório do valor das avaliações cadastradas na origens de avaliação da divisão de turma em questão.*/
                    totalNotaDivisao += avaliacao.Valor;
                    //verifica se existe nota para o aluno caso contrario contar quantas estão sem notas
                    var apuracaoAvaliacao = avaliacao.AplicacoesAvaliacao.FirstOrDefault().ApuracoesAvaliacao.FirstOrDefault(f => f.SeqAlunoHistorico == dadosTurma.SeqAlunoHistorico);
                    if (apuracaoAvaliacao != null)
                    {
                        totalNotaAlunoDivisao += apuracaoAvaliacao != null ? (decimal)apuracaoAvaliacao?.Nota : 0;
                    }
                    else
                    {
                        numeroAlaviacaoSemApuraco++;
                    }
                }

                retorno.DivisoesTurma.Add(new ConsultaAvaliacoesDivisaoTurmaVO()
                {
                    Descricao = $"{numeracaoDivisao} {nomeclaturaDivisao}",
                    SeqDivisaoTurma = item.Seq.Value,
                    Avaliacoes = detalhesAvaliacaos.OrderBy(o => o.Sigla).ToList(),
                    Nota = $"{ (avaliacoesDivisao.Count != numeroAlaviacaoSemApuraco ? totalNotaAlunoDivisao.ToString() : "-")}/{totalNotaDivisao.ToString()}"
                });
            }

            //Calcular notas - Turma
            var totalNotaConceitoReavaliacao = CalcularTotalNotaConceito(listaAvaliacoesTurma, dadosTurma.SeqAlunoHistorico, origemAvaliacaoTurma);

            //Calcular notas - Divisão turma
            var totalNotaConceito = CalcularTotalNotaConceito(listaAvaliacoesDivisaoTurma, dadosTurma.SeqAlunoHistorico, origemAvaliacaoTurma);

            if (totalNotaConceitoReavaliacao.existeApuracao)
            {
                retorno.NotaTotal = totalNotaConceito.conceito;                
                decimal calculoNota = (totalNotaConceito.notaTotal.GetValueOrDefault() + totalNotaConceitoReavaliacao.notaTotal.GetValueOrDefault()) / 2;
                //Arredondamento
                calculoNota = Math.Round(calculoNota, 0, MidpointRounding.AwayFromZero);
                retorno.NotaTotalReavaliacao = ConceitoNotaFinal(calculoNota, totalNotaConceitoReavaliacao.existeApuracao, origemAvaliacaoTurma);
            }
            else
            {
                retorno.NotaTotalReavaliacao = "";
                retorno.NotaTotal = totalNotaConceito.conceito;
            }

            /*Se existir histórico escolar lançado para a origem de avaliação da turma recebida como parametro para o aluno
            logado, apresentar a situação do histórico escolar.
            Se não existir histórico escolar, apresentar a situação = "Em curso"*/
            var specHistoricoEscolar = new HistoricoEscolarFilterSpecification() { SeqAlunoHistorico = dadosTurma.SeqAlunoHistorico, SeqOrigemAvaliacao = dadosTurma.SeqOrigemAvaliacao };
            var historicoEscolar = HistoricoEscolarDomainService.SearchProjectionBySpecification(specHistoricoEscolar, p => new
            {
                SituacaoHistoricoEscolar = p.SituacaoHistoricoEscolar,
                Faltas = p.Faltas
            }).FirstOrDefault();

            /*Calcular Faltas
            Campo deverá ser exibido somente se o critério de aprovação da origem de avaliação da turma em questão possuir
            apuração por frequência.
            Se existir histórico escolar lançado para a origem de avaliação da turma recebida como parametro para o aluno
            logado, apresentar o número de faltas lançado no histórico.
            Se não existir histórico escolar, Buscar do diario da turma.*/
            if (origemAvaliacaoTurma.CriterioAprovacao.ApuracaoFrequencia)
            {
                if (historicoEscolar != null)
                {
                    retorno.Falta = historicoEscolar.Faltas.ToString();
                }
                else
                {
                    retorno.Falta = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, seqPessoaAtuacao, null).FirstOrDefault().SomaFaltasApuracao.ToString();
                }
            }

            //Preencher retorno das avaliações da turma
            //Listar todas as avaliações da origem de avaliação da turma (ou da divisão de turma) recebida como parametro em
            //ordem de sigla da aplicação de avaliação.
            retorno.Avaliacoes = new List<DetalhesAvaliacaoVO>();
            foreach (var avaliacao in avaliacoesTurma)
            {
                retorno.Avaliacoes.Add(MontarDetalhesAvaliacao(avaliacao, dadosTurma.SeqAlunoHistorico));
            }

            //Refactory do retorno
            retorno.DiarioFechado = dadosTurma.DiarioFechado;
            retorno.Falta = retorno.Falta ?? "0";
            retorno.NotaTotal = string.IsNullOrEmpty(retorno.NotaTotal) ? "-" : retorno.NotaTotal;
            retorno.NotaTotalReavaliacao = string.IsNullOrEmpty(retorno.NotaTotalReavaliacao) ? "-" : retorno.NotaTotalReavaliacao;
            retorno.Situacao = !string.IsNullOrEmpty(historicoEscolar?.SituacaoHistoricoEscolar.SMCGetDescription()) ? historicoEscolar.SituacaoHistoricoEscolar.SMCGetDescription() : "Em curso";
            retorno.PossuiApuracaoFrequencia = origemAvaliacaoTurma.CriterioAprovacao.ApuracaoFrequencia;
            retorno.Avaliacoes.OrderBy(o => o.Sigla).ToList();

            return retorno;
        }

        /// <summary>
        /// A situação só será exibida quando a avaliação está marcada para ter entrega online.
        ///Apresentar a legenda da seguinte forma:
        ///· Caso não exista nenhuma entrega online realizada para a aplicação de avaliação em questão · que o aluno faça
        ///parte dos participantes da entrega, apresentar o valor "Aguardando entrega" na legenda.
        ///· Caso exista uma entrega online realizada para a aplicação de avaliação em questão que o aluno faça parte dos
        ///participantes da entrega, apresentar na legenda a situação atual da entrega, podendo ter os valores: "Entregue",
        ///"Liberado para correção", "Solicitado nova entrega"
        ///· Caso já exista apuração de avaliação para a aplicação de avaliação em questão para o aluno logado, apresentar
        ///o valor "Corrigido" na legenda.
        /// </summary>
        /// <returns></returns>
        private SituacaoEntregaOnline RetornaSituacaoEntrega(long seqAplicacaoAvaliacao, long seqAlunoHistorico)
        {
            SituacaoEntregaOnline retorno = SituacaoEntregaOnline.AguardandoEntrega;

            var entregasOnline = EntregaOnlineDomainService.BuscarEntregasOnline(seqAplicacaoAvaliacao).EntregasOnline;

            var entregaAluno = entregasOnline.Where(w => w.Participantes.Any(a => a.SeqAlunoHistorico == seqAlunoHistorico)).OrderByDescending(o => o.DataEntrega).FirstOrDefault();

            if (entregaAluno != null)
            {
                retorno = entregaAluno.SituacaoEntrega;
            }

            return retorno;
        }

        /// <summary>
        /// Lista de avaliações especificas
        /// </summary>
        /// <param name="seqOrigemAvalicao">Sequencial origem avalição</param>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <returns>Projection com a lista de avalições</returns>
        private List<AvaliacaoVO> ListaAvaliacoesOrigemAvalicao(long seqOrigemAvalicao)
        {
            var spec = new AvaliacaoFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvalicao };
            var retorno = SearchProjectionBySpecification(spec, p => new AvaliacaoVO()
            {
                AplicacoesAvaliacao = p.AplicacoesAvaliacao.Select(s => new AplicacaoAvaliacaoVO()
                {
                    ApuracoesAvaliacao = s.ApuracoesAvaliacao.Select(sa => new ApuracaoAvaliacaoVO()
                    {
                        SeqAlunoHistorico = sa.SeqAlunoHistorico,
                        Nota = sa.Nota,
                        ComentarioApuracao = sa.ComentarioApuracao
                    }).ToList(),
                    DataInicioAplicacaoAvaliacao = s.DataInicioAplicacaoAvaliacao,
                    DataFimAplicacaoAvaliacao = s.DataFimAplicacaoAvaliacao,
                    EntregaWeb = s.EntregaWeb,
                    Local = s.Local,
                    OrigemAvaliacao = new OrigemAvaliacaoVO()
                    {
                        EscalaApuracao = s.OrigemAvaliacao.EscalaApuracao != null ? new EscalaApuracaoVO()
                        {
                            TipoEscalaApuracao = s.OrigemAvaliacao.EscalaApuracao.TipoEscalaApuracao,
                            Itens = s.OrigemAvaliacao.EscalaApuracao.Itens.Select(si => new EscalaApuracaoItemVO()
                            {
                                Descricao = si.Descricao,
                                PercentualMaximo = si.PercentualMaximo,
                                PercentualMinimo = si.PercentualMinimo,
                                Aprovado = si.Aprovado
                            }).ToList()
                        } : null,
                    },
                    Seq = s.Seq,
                    SeqOrigemAvaliacao = s.SeqOrigemAvaliacao,
                    Sigla = s.Sigla
                }).ToList(),
                Descricao = p.Descricao,
                TipoAvaliacao = p.TipoAvaliacao,
                Seq = p.Seq,
                Valor = p.Valor
            }).ToList();

            return retorno.OrderBy(o => o.Sigla).ToList();
        }

        /// <summary>
        /// Monta o Objeto de um detalhe de avaliação
        /// </summary>
        /// <param name="avaliacao">Dados da avaliação</param>
        /// <returns>Detalhe da avaliação</returns>
        private DetalhesAvaliacaoVO MontarDetalhesAvaliacao(AvaliacaoVO avaliacao, long seqAlunoHistorico)
        {
            var apuracaoAvaliacao = avaliacao.AplicacoesAvaliacao.FirstOrDefault().ApuracoesAvaliacao.FirstOrDefault(f => f.SeqAlunoHistorico == seqAlunoHistorico);

            return new DetalhesAvaliacaoVO
            {
                Sigla = avaliacao.AplicacoesAvaliacao.FirstOrDefault().Sigla,
                Descricao = avaliacao.Descricao,
                Data = avaliacao.AplicacoesAvaliacao.FirstOrDefault().DataFimAplicacaoAvaliacao.HasValue ?
                                $"{avaliacao.AplicacoesAvaliacao.FirstOrDefault().DataInicioAplicacaoAvaliacao.ToString()} - {avaliacao.AplicacoesAvaliacao.FirstOrDefault().DataFimAplicacaoAvaliacao.ToString()}" :
                                avaliacao.AplicacoesAvaliacao.FirstOrDefault().DataInicioAplicacaoAvaliacao.ToString(),
                Local = avaliacao.AplicacoesAvaliacao.FirstOrDefault().Local,
                Valor = avaliacao.Valor.ToString(),
                Situacao = RetornaSituacaoEntrega(avaliacao.AplicacoesAvaliacao.FirstOrDefault().Seq, seqAlunoHistorico),
                EntregaWeb = avaliacao.AplicacoesAvaliacao.FirstOrDefault().EntregaWeb,
                Nota = apuracaoAvaliacao != null ? (!string.IsNullOrEmpty(apuracaoAvaliacao?.Nota.ToString()) ? apuracaoAvaliacao.Nota.ToString() : "-") : "-",
                SeqAvaliacao = avaliacao.Seq,
                SeqAplicacaoAvaliacao = avaliacao.AplicacoesAvaliacao.FirstOrDefault().Seq,
                ComentarioApuracao = apuracaoAvaliacao?.ComentarioApuracao
            };
        }

        /// <summary>
        /// Calcular o total das notas com o conceito das avaliacões
        /// </summary>
        /// <returns>Nota formatada</returns>
        private (decimal? notaTotal, string conceito, bool existeApuracao) CalcularTotalNotaConceito(List<AvaliacaoVO> listaAvaliacoes, long seqAlunoHistorico, OrigemAvaliacao origemAvaliacaoTurma)
        {
            /*Nota - Somatório de notas das apurações de avaliações do tipo "Reavaliação" lançadas na origem de avaliação
            da turma recebida como parametro para o aluno logado
            */
            decimal? notaTotalTurma = listaAvaliacoes.SelectMany(sm => sm.AplicacoesAvaliacao.SelectMany(sma => sma.ApuracoesAvaliacao.Where(w => w.SeqAlunoHistorico == seqAlunoHistorico).Select(s => s.Nota))).ToList().Sum();
            string conceitoTurma = string.Empty;
            bool existeApuracaoAvalicao = listaAvaliacoes.SelectMany(sm => sm.AplicacoesAvaliacao.SelectMany(sma => sma.ApuracoesAvaliacao.Where(w => w.SeqAlunoHistorico == seqAlunoHistorico))).SMCAny();
            /*Calcular conceito - Turma
             Conceito - se o critério de aprovação da origem de avaliação da turma recebida como parametro tiver escala de
             apuração calcular o conceito de acordo com a escala.*/
            //Somente irá caucluar conceito se existir apuração de avaliação para alguma avaliacao
            if (existeApuracaoAvalicao)
            {
                if (origemAvaliacaoTurma?.CriterioAprovacao?.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito)
                {
                    foreach (var item in origemAvaliacaoTurma.CriterioAprovacao.EscalaApuracao.Itens)
                    {
                        if (notaTotalTurma >= item.PercentualMinimo && notaTotalTurma <= item.PercentualMaximo)
                        {
                            conceitoTurma = item.Descricao;
                        }
                    }
                }
                conceitoTurma = !string.IsNullOrEmpty(conceitoTurma) ? $"{Math.Round(notaTotalTurma.GetValueOrDefault(), 0, MidpointRounding.AwayFromZero).ToString()} - {conceitoTurma}" : notaTotalTurma.ToString();
            }

            return (notaTotalTurma, conceitoTurma, existeApuracaoAvalicao);
        }

        /// <summary>
        /// Monta o conceito da Nota Final depois do cálculo
        /// </summary>
        /// <returns>Nota formatada</returns>
        private string ConceitoNotaFinal(decimal notaFinal, bool existeApuracao, OrigemAvaliacao origemAvaliacaoTurma)
        {
            string conceitoTurma = string.Empty;

            if (existeApuracao)
            {
                if (origemAvaliacaoTurma?.CriterioAprovacao?.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito)
                {
                    foreach (var item in origemAvaliacaoTurma.CriterioAprovacao.EscalaApuracao.Itens)
                    {
                        if (notaFinal >= item.PercentualMinimo && notaFinal <= item.PercentualMaximo)
                        {
                            conceitoTurma = item.Descricao;
                        }
                    }
                }
                conceitoTurma = !string.IsNullOrEmpty(conceitoTurma) ? $"{notaFinal.ToString()} - {conceitoTurma}" : notaFinal.ToString();
            }
            else
            {
                conceitoTurma = notaFinal.ToString();
            }

            return conceitoTurma;
        }
    }
}