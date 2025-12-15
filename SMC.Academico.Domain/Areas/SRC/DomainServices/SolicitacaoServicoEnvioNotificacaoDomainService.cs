using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Specification;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoServicoEnvioNotificacaoDomainService : AcademicoContextDomain<SolicitacaoServicoEnvioNotificacao>
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService => Create<InstituicaoNivelTipoOrientacaoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();

        #endregion [ DomainService ]

        #region [ Service ]

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion [Service]

        /// <summary>
        /// Envia uma notificação configurada para uma solicitação de serviço.
        /// Os destinatários do envio podem ser:
        /// 1) O solicitante da solicitação, caso o flag EnvioSolicitante = TRUE
        /// 2) Uma lista de destinatários, caso o parâmetro Destinatarios seja informado
        /// 3) Os destinatários configurados na configuração da notificação, sempre que configurados
        /// <param name="parametros">Parametros para o envio da notificação</param>
        public void EnviarNotificacaoSolicitacaoServico(EnvioNotificacaoSolicitacaoServicoVO parametros)
        {
            // Busca os dados da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(parametros.SeqSolicitacaoServico);

            var dados = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
            {
                SeqConfiguracaoTipoNotificacao = (long?)x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel).SeqConfiguracaoTipoNotificacao,
                SeqProcessoEtapaConfiguracaoNotificacao = (long?)x.SituacaoAtual.SolicitacaoServicoEtapa.ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel && cn.EnvioAutomatico).Seq,
                Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                             .Select(e => new { Email = e.EnderecoEletronico.Descricao }).ToList()
            });

            // Caso não encontre os seqs do jeito certo, busca do jeito antigo (para manter compatibilidade com o que já funcionava antes)
            if (dados == null || !dados.SeqProcessoEtapaConfiguracaoNotificacao.HasValue)
            {
                dados = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
                {
                    SeqConfiguracaoTipoNotificacao = (long?)x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault().ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel).SeqConfiguracaoTipoNotificacao,
                    SeqProcessoEtapaConfiguracaoNotificacao = (long?)x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault().ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel && cn.EnvioAutomatico).Seq,
                    Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                                 .Select(e => new { Email = e.EnderecoEletronico.Descricao }).ToList()
                });
            }

            if (dados == null)
                throw new SMCApplicationException("Erro método EnviarNotificacaoSolicitacaoServico. Não encontrou a solicitação.");

            // Caso tenha sido parametrizado para recuperar a configuração de email da primeira etapa ele reconfigura os parametros que eram da etapa atual
            if (parametros.ConfiguracaoPrimeiraEtapa)
            {
                dados = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
                {
                    SeqConfiguracaoTipoNotificacao = (long?)x.Etapas.OrderBy(e => e.Seq).FirstOrDefault().ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel).SeqConfiguracaoTipoNotificacao,
                    SeqProcessoEtapaConfiguracaoNotificacao = (long?)x.Etapas.OrderBy(e => e.Seq).FirstOrDefault().ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == parametros.TokenNotificacao && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeResponsavel && cn.EnvioAutomatico).Seq,
                    Emails = x.PessoaAtuacao.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                             .Select(e => new { Email = e.EnderecoEletronico.Descricao }).ToList()
                });
            }

            // Se encontrou a configuração para envio, prossegue com o envio
            if (dados.SeqProcessoEtapaConfiguracaoNotificacao.GetValueOrDefault() > 0)
            {
                // Monta o Data para o serviço de notificação
                NotificacaoEmailData data = new NotificacaoEmailData()
                {
                    SeqConfiguracaoNotificacao = dados.SeqConfiguracaoTipoNotificacao.GetValueOrDefault(),
                    DadosMerge = parametros.DadosMerge,
                    DataPrevistaEnvio = DateTime.Now,
                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                };

                // Se é para enviar notificação para o solicitante e existem e-mails configurados
                if (parametros.EnvioSolicitante && dados.Emails.SMCCount() > 0)
                {
                    data.Destinatarios = new List<NotificacaoEmailDestinatarioData>() {
                            new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", dados.Emails.Select(s => s.Email)) }
                        };
                }

                // Se recebeu uma lista de destinatários
                if (parametros.Destinatarios.SMCCount() > 0)
                {
                    data.Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                    {
                        new NotificacaoEmailDestinatarioData { EmailDestinatario = string.Join(";", parametros.Destinatarios) }
                    };
                }

                // Chama o serviço de envio de notificação
                long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                // Busca o sequencial da notificação-email-destinatário enviada
                var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                if (envioDestinatario.Count == 0)
                    throw new SolicitacaoServicoEnvioNotificacaoException(parametros.TokenNotificacao);

                foreach (var item in envioDestinatario)
                {
                    // Salva a referencia da notificação enviada para a solicitação de serviço
                    SolicitacaoServicoEnvioNotificacao envio = new SolicitacaoServicoEnvioNotificacao()
                    {
                        SeqSolicitacaoServico = parametros.SeqSolicitacaoServico,
                        SeqProcessoEtapaConfiguracaoNotificacao = dados.SeqProcessoEtapaConfiguracaoNotificacao.GetValueOrDefault(),
                        SeqNotificacaoEmailDestinatario = item.Seq
                    };
                    this.InsertEntity(envio);
                }
            }
        }

        /// <summary>
        /// Reenvia uma notificação configurada para uma solicitação de serviço, copiando os dados da notificação enviada anteriormente.
        /// Os destinatários do envio podem ser:
        /// 1) O solicitante da solicitação, caso o flag EnvioSolicitante = TRUE
        /// 2) Uma lista de destinatários, caso o parâmetro Destinatarios seja informado
        /// 3) Os destinatários configurados na configuração da notificação, sempre que configurados
        /// <param name="parametros">Parametros para o envio da notificação</param>
        public void ReenviarNotificacaoSolicitacaoServico(long seqSolicitacaoServico, long seqNotificacaoEmailDestinatario, EnvioNotificacaoSolicitacaoServicoVO parametros)
        {
            //Busca os dados da solicitação envio notificação de origem
            var specSolicitacaoServicoEnvioNotificacao = new SolicitacaoServicoEnvioNotificacaoFilterSpecification()
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                SeqNotificacaoEmailDestinatario = seqNotificacaoEmailDestinatario
            };

            var solicitacaoServicoEnvioNotificacao = this.SearchByKey(specSolicitacaoServicoEnvioNotificacao, x => x.ProcessoEtapaConfiguracaoNotificacao);

            //Busca os dados da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(parametros.SeqSolicitacaoServico);

            var emails = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => x.PessoaAtuacao.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                                                        .Select(e => new { Email = e.EnderecoEletronico.Descricao }).ToList());

            var notificacao = this.NotificacaoService.ConsultaNotificacao(seqNotificacaoEmailDestinatario);

            // Monta o Data para o serviço de notificação
            NotificacaoEmailData data = new NotificacaoEmailData()
            {
                SeqConfiguracaoNotificacao = solicitacaoServicoEnvioNotificacao.ProcessoEtapaConfiguracaoNotificacao.SeqConfiguracaoTipoNotificacao,
                DadosMerge = parametros.DadosMerge,
                MensagemReenvio = notificacao.Mensagem,
                DataPrevistaEnvio = DateTime.Now,
                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
            };

            // Se recebeu uma lista de destinatários
            if (parametros.Destinatarios.SMCCount() > 0)
            {
                data.Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                {
                    new NotificacaoEmailDestinatarioData { EmailDestinatario = string.Join(";", parametros.Destinatarios) }
                };
            }

            // Chama o serviço de envio de notificação
            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

            // Busca o sequencial da notificação-email-destinatário enviada
            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
            if (envioDestinatario.Count == 0)
                throw new SolicitacaoServicoEnvioNotificacaoException(notificacao.TokenTipoNotificacao);

            foreach (var item in envioDestinatario)
            {
                // Salva a referencia da notificação enviada para a solicitação de serviço
                SolicitacaoServicoEnvioNotificacao envio = new SolicitacaoServicoEnvioNotificacao()
                {
                    SeqSolicitacaoServico = parametros.SeqSolicitacaoServico,
                    SeqProcessoEtapaConfiguracaoNotificacao = solicitacaoServicoEnvioNotificacao.SeqProcessoEtapaConfiguracaoNotificacao,
                    SeqNotificacaoEmailDestinatario = item.Seq
                };
                this.InsertEntity(envio);
            }
        }

        /// <summary>
        /// Envia uma notificação configurada para uma solicitação de serviço de token CHANCELA_PROGRAMA_DESTINO_SECRETARIA  utilizada somente na CHANCELA_DISCIPLINA_ELETIVA_ORIGEM 
        /// Recuperando na consulta todos os parâmetros e utilizando fixo a validação com a unidade compartilhada
        /// Os destinatérios são configurados de acordo com a unidade compartilhada da solicitação de serviço no método Salvar da Notificação.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <param name="seqConfiguracaoEtapa">Sequencial da configuração de etapa</param>
        public void EnviarNotivicacaoSolicitacaoServicoUnidadeCompartilhada(long seqSolicitacaoServico, long seqConfiguracaoEtapa)
        {
            // Busca os dados da solicitação
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(seqSolicitacaoServico);
            var dados = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
            {
                SeqConfiguracaoTipoNotificacao = (long?)x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault(f => f.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == TOKEN_TIPO_NOTIFICACAO.CHANCELA_PROGRAMA_DESTINO_SECRETARIA && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeCompartilhada).SeqConfiguracaoTipoNotificacao,
                SeqProcessoEtapaConfiguracaoNotificacao = (long?)x.Etapas.OrderByDescending(e => e.Seq).FirstOrDefault(f => f.SeqConfiguracaoEtapa == seqConfiguracaoEtapa).ConfiguracaoEtapa.ProcessoEtapa.ConfiguracoesNotificacao.FirstOrDefault(cn => cn.TipoNotificacao.Token == TOKEN_TIPO_NOTIFICACAO.CHANCELA_PROGRAMA_DESTINO_SECRETARIA && cn.ProcessoUnidadeResponsavel.SeqEntidadeResponsavel == x.SeqEntidadeCompartilhada && cn.EnvioAutomatico).Seq,
                NomeSolicitante = x.PessoaAtuacao.DadosPessoais.NomeSocial ?? x.PessoaAtuacao.DadosPessoais.Nome,
                DescricaoCursoOfertaLocalidade = x.AlunoHistorico.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.Nome,
                NumeroProtocolo = x.NumeroProtocolo,
            });

            // Se encontrou a configuração para envio, prossegue com o envio
            if (dados.SeqProcessoEtapaConfiguracaoNotificacao.GetValueOrDefault() > 0)
            {
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, dados.NomeSolicitante);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_OFERTA_CURSO_LOCALIDADE, dados.DescricaoCursoOfertaLocalidade);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NUM_PROTOCOLO, dados.NumeroProtocolo);

                // Monta o Data para o serviço de notificação
                NotificacaoEmailData data = new NotificacaoEmailData()
                {
                    SeqConfiguracaoNotificacao = dados.SeqConfiguracaoTipoNotificacao.GetValueOrDefault(),
                    DadosMerge = dadosMerge,
                    DataPrevistaEnvio = DateTime.Now,
                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                };

                // Chama o serviço de envio de notificação
                long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                // Busca o sequencial da notificação-email-destinatário enviada
                var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                if (envioDestinatario.Count == 0)
                    throw new SolicitacaoServicoEnvioNotificacaoException(TOKEN_TIPO_NOTIFICACAO.CHANCELA_PROGRAMA_DESTINO_SECRETARIA);

                foreach (var item in envioDestinatario)
                {
                    // Salva a referencia da notificação enviada para a solicitação de serviço
                    SolicitacaoServicoEnvioNotificacao envio = new SolicitacaoServicoEnvioNotificacao()
                    {
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        SeqProcessoEtapaConfiguracaoNotificacao = dados.SeqProcessoEtapaConfiguracaoNotificacao.GetValueOrDefault(),
                        SeqNotificacaoEmailDestinatario = item.Seq
                    };
                    this.InsertEntity(envio);
                }
            }
        }

        /// <summary>
        /// Envia uma notificação configurada para uma solicitação de serviço incluíndo como destinatário 
        /// os orientadores do solicitante da solicitação
        /// </summary>
        /// <param name="parametros">Parametros para o envio da notificação</param>
        public void EnviarNotivicacaoSolicitacaoServicoOrientador(EnvioNotificacaoSolicitacaoServicoVO parametros)
        {
            // Busca as orientações do solicitante
            var specSolicitacao = new SMCSeqSpecification<SolicitacaoServico>(parametros.SeqSolicitacaoServico);
            var dados = SolicitacaoServicoDomainService.SearchProjectionByKey(specSolicitacao, x => new
            {
                TipoAtuacao = x.PessoaAtuacao.TipoAtuacao,
                SeqTipoTermoIntercambio = (long?)x.PessoaAtuacao.TermosIntercambio.FirstOrDefault(t => t.Periodos.Any(p => p.DataInicio <= DateTime.Today && p.DataFim >= DateTime.Today)).TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                Orientadores = x.PessoaAtuacao.OrientacoesPessoaAtuacao
                                              .SelectMany(o => o.Orientacao.OrientacoesColaborador)
                                              .Where(o => o.DataInicioOrientacao <= DateTime.Today && (!o.DataFimOrientacao.HasValue || o.DataFimOrientacao >= DateTime.Today))
                                              .Select(s => new
                                              {
                                                  s.Orientacao.SeqEntidadeInstituicao,
                                                  s.Orientacao.SeqNivelEnsino,
                                                  s.Orientacao.SeqTipoVinculoAluno,
                                                  s.Orientacao.SeqTipoOrientacao,
                                                  Emails = s.Colaborador.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList()
                                              }).ToList()
            });

            // Monta a lista de destinatários (orientadores)
            var destinatarios = new List<string>();
            foreach (var orientador in dados.Orientadores)
            {
                // Busca a parametrização do tipo de orientação por instituição x nível x vinculo
                var specINTO = new InstituicaoNivelTipoOrientacaoFilterSpecification()
                {
                    SeqInstituicaoEnsino = orientador.SeqEntidadeInstituicao,
                    SeqNivelEnsino = orientador.SeqNivelEnsino,
                    SeqTipoVinculoAluno = orientador.SeqTipoVinculoAluno,
                    SeqTipoOrientacao = orientador.SeqTipoOrientacao,
                    SeqTipoIntercambio = dados.SeqTipoTermoIntercambio
                };
                var InsNivTipOrientacao = InstituicaoNivelTipoOrientacaoDomainService.SearchProjectionByKey(specINTO, x => new
                {
                    CadastroOrientacaoIngressante = x.CadastroOrientacaoIngressante,
                    CadastroOrientacaoAluno = x.CadastroOrientacaoAluno
                });

                // Se achou a parametrização, verifica se ela é obrigatória para o tipo de atuação
                if (InsNivTipOrientacao != null)
                {
                    if (dados.TipoAtuacao == TipoAtuacao.Ingressante && InsNivTipOrientacao.CadastroOrientacaoIngressante == CadastroOrientacao.Exige)
                        destinatarios.AddRange(orientador.Emails);

                    if (dados.TipoAtuacao == TipoAtuacao.Aluno && InsNivTipOrientacao.CadastroOrientacaoAluno == CadastroOrientacao.Exige)
                        destinatarios.AddRange(orientador.Emails);
                }
            }

            // Se o solicitante possui orientadores para envio
            if (destinatarios.SMCCount() > 0)
            {
                parametros.Destinatarios = destinatarios;
                EnviarNotificacaoSolicitacaoServico(parametros);
            }
        }

        public void EnviarNotificacaoSolicitacaoDiplomaDigitalFinalizado(string usuarioInclusao, string codigoMigracao, string tokenTipoDocumentoAcademico)
        {
            var mensagemNotificacao = "Atenção: foi concluído o processo de emissão do diploma digital referente a conclusão do seu curso. Consulte o seu e-mail para mais informações.";

            if (tokenTipoDocumentoAcademico != TOKEN_TIPO_DOCUMENTO_ACADEMICO.DIPLOMA_DIGITAL)
                mensagemNotificacao = "Atenção: foi concluído o processo de emissão do histórico escolar. Consulte o seu e-mail para mais informações.";

            var data = new NotificacaoMobileData()
            {
                UsuarioEnvio = usuarioInclusao,
                PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                TokenAplicativo = "PUC_MOBILE",
                TokenNotificacao = "NOTIFICACAO_SGA_ALUNO",
                TokenTipoNotificacao = "MOBILE_ALUNO_NOTICIA",
                MensagemNotificacao = mensagemNotificacao,
                ValorParametro = codigoMigracao,
                ValorParametroPush = string.Format("login#{0}", codigoMigracao),
                BroadCast = false,
                DataPrevistaEnvio = DateTime.Now,
                Titulo = "Processo de emissão do diploma digital",
                OrigemMensagem = ""
            };
            var seq = this.NotificacaoService.SalvarNotificacao(data);
        }
    }
}