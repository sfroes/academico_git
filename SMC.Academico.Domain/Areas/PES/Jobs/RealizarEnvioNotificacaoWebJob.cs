using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Domain;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static iTextSharp.text.pdf.AcroFields;

namespace SMC.Academico.Domain.Areas.PES.Jobs
{
    internal class RealizarEnvioNotificacaoWebJob : SMCWebJobBase<RealizarEnvioNotificacaoSATVO, RealizarEnvioNotificacaoVO>
    {
        #region [DomainService]

        private EnvioNotificacaoDomainService EnvioNotificacaoDomainService => this.Create<EnvioNotificacaoDomainService>();
        private EnvioNotificacaoDestinatarioDomainService EnvioNotificacaoDestinatarioDomainService => this.Create<EnvioNotificacaoDestinatarioDomainService>();
        private AlunoHistoricoDomainService AlunoHistoricoDomainService => this.Create<AlunoHistoricoDomainService>();
        private EntidadeDomainService EntidadeDomainService => this.Create<EntidadeDomainService>();
        private ColaboradorDomainService ColaboradorDomainService => this.Create<ColaboradorDomainService>();

        #endregion

        #region Service
        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }

        #endregion Service


        public override ICollection<RealizarEnvioNotificacaoVO> GetItems(RealizarEnvioNotificacaoSATVO filtros)
        {
            var envioNotificacao = EnvioNotificacaoDomainService.SearchByKey(new SMCSeqSpecification<EnvioNotificacao>(filtros.SeqEnvioNotificacao));
            if (envioNotificacao == null)
                Scheduler.LogError("Não foi possível realizar o agendamento. Envio de notificação não existente.");

            Scheduler.LogSucess("Recuperando destinatários");

            var destinatarios = EnvioNotificacaoDomainService.BuscarEnvioNotificacaoDestinatarioJob(envioNotificacao.Seq);

            if (destinatarios.Count == 0)
                Scheduler.LogError("Não foi possível realizar o agendamento. Não existe destinatários para o envio de notificação.");
            else
                Scheduler.LogSucess("Foram recuperados " + destinatarios.Count + " destinatários");

            return destinatarios;
        }

        public override bool ProcessItem(RealizarEnvioNotificacaoVO item)
        {
            if (item.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var alunos = AlunoHistoricoDomainService.SearchProjectionBySpecification(new AlunoHistoricoFilterSpecification() { SeqAluno = item.SeqPessoaAtuacao, Atual = true },
                                            p => new
                                            {
                                                ///Dados Aluno
                                                SeqAluno = p.SeqAluno,
                                                Nome = string.IsNullOrEmpty(p.Aluno.DadosPessoais.NomeSocial) ? p.Aluno.DadosPessoais.Nome : p.Aluno.DadosPessoais.NomeSocial + " (" + p.Aluno.DadosPessoais.Nome + ")",
                                                DadosVinculo = p.NivelEnsino.Descricao + " - " + p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao + " - " + p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome + " - " + p.CursoOfertaLocalidadeTurno.Turno.Descricao,
                                                Vinculo = p.Aluno.TipoVinculoAluno.Descricao,
                                                OfertaCurso = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.Descricao,
                                                Localidade = p.CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.HierarquiasEntidades.FirstOrDefault().ItemSuperior.Entidade.Nome,
                                                FormacaoEspecifica = p.Formacoes.Where(x => !x.DataFim.HasValue).Select(x => x.FormacaoEspecifica).FirstOrDefault().Descricao,
                                                NivelEnsino = p.NivelEnsino.Descricao,
                                                EnderecosEletronicos = p.Aluno.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList(),
                                                Entidade = p.SeqEntidadeVinculo
                                            });

                foreach (var aluno in alunos)
                {
                    // Busca os dados da entidade de vinculo para formatação do e-mail
                    var specEntidade = new SMCSeqSpecification<Entidade>(aluno.Entidade);

                    var dadosEntidade = EntidadeDomainService.SearchProjectionByKey(specEntidade, e => new
                    {
                        Nome = e.Nome,
                        Endereco = e.Enderecos.FirstOrDefault(d => d.TipoEndereco == TipoEndereco.Comercial),
                        Telefones = e.Telefones,
                        Emails = e.EnderecosEletronicos.Where(m => m.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                    });

                    string enderecoFormatado = string.Empty;
                    if (dadosEntidade.Endereco != null)
                    {
                        // Formata o endereço da entidade
                        enderecoFormatado = string.Format("{0}, {1}{2} - {3}, {4} - {5}, CEP: {6}",
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Logradouro) ? string.Empty : dadosEntidade.Endereco.Logradouro.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Numero) ? string.Empty : dadosEntidade.Endereco.Numero.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Complemento) ? string.Empty : string.Format(" / {0}", dadosEntidade.Endereco.Complemento.Trim()),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Bairro) ? string.Empty : dadosEntidade.Endereco.Bairro.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.NomeCidade) ? string.Empty : dadosEntidade.Endereco.NomeCidade.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.SiglaUf) ? string.Empty : dadosEntidade.Endereco.SiglaUf.Trim(),
                        string.IsNullOrEmpty(dadosEntidade.Endereco.Cep) ? string.Empty : dadosEntidade.Endereco.Cep.Trim());
                    }

                    // Formata o telefone da entidade
                    string telefoneFormatado = string.Empty;
                    if (dadosEntidade.Telefones != null)
                    {
                        // Buscar telefone comercial sem categoria associada,
                        var telComercialSemCategoria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && !t.CategoriaTelefone.HasValue);
                        if (telComercialSemCategoria != null)
                        {
                            telefoneFormatado = string.Format("({0}){1}",
                                                                telComercialSemCategoria.CodigoArea.ToString().Trim(),
                                                                telComercialSemCategoria.Numero.ToString().Trim());
                        }
                        // Se não existir, buscar comercial e categoria 'secretaria'
                        else
                        {
                            var telComercialSecretaria = dadosEntidade.Telefones.FirstOrDefault(t => t.TipoTelefone == TipoTelefone.Comercial && t.CategoriaTelefone == CategoriaTelefone.Secretaria);
                            if (telComercialSecretaria != null)
                            {
                                telefoneFormatado = string.Format("({0}){1}",
                                                                    telComercialSecretaria.CodigoArea.ToString().Trim(),
                                                                    telComercialSecretaria.Numero.ToString().Trim());
                            }
                        }
                    }

                    // Formata o endereço eletrônico da entidade
                    string emailFormatado = string.Empty;
                    if (dadosEntidade.Emails != null)
                    {
                        // Buscar e-mails sem categoria associada
                        var emailSemCategoria = dadosEntidade.Emails.FirstOrDefault(e => !e.CategoriaEnderecoEletronico.HasValue);
                        if (emailSemCategoria != null)
                        {
                            emailFormatado = emailSemCategoria.Descricao;
                        }
                        // Se não existir, buscar categoria 'secretaria'
                        else
                        {
                            var emailSecretaria = dadosEntidade.Emails.FirstOrDefault(e => e.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria);
                            if (emailSecretaria != null)
                            {
                                emailFormatado = emailSecretaria.Descricao;
                            }
                        }
                    }
                    if (aluno.EnderecosEletronicos == null || !aluno.EnderecosEletronicos.Any())
                        Scheduler.LogError("Notificação não enviada." + aluno.Nome + " não possui e-mail cadastrado.");
                    else
                    {
                        var dadosMerge = new Dictionary<string, string>
                          {
                              { "{{NOM_PESSOA}}", aluno.Nome},
                              { "{{NOM_ENTIDADE}}", dadosEntidade.Nome},
                              { "{{END_ENTIDADE}}", enderecoFormatado },
                              { "{{TEL_ENTIDADE}}", telefoneFormatado },
                              { "{{END_ELETRONICO_ENTIDADE}}", emailFormatado },
                              { "{{DSC_CURSO_OFERTA}}", aluno.OfertaCurso },
                              { "{{DSC_OFERTA_CURSO_LOCALIDADE}}", aluno.Localidade},
                              { "{{DESCRICAO_FORMACAO_ESPECIFICA}}", aluno.FormacaoEspecifica},
                              { "{{DSC_NIVEL_ENSINO}}", aluno.NivelEnsino},
                              { "{{DSC_VINCULO}}", aluno.Vinculo}
                          };

                        var data = new NotificacaoEmailData()
                        {
                            SeqConfiguracaoNotificacao = item.SeqConfiguracaoTipoNotificacao,
                            DadosMerge = dadosMerge,
                            DataPrevistaEnvio = DateTime.Now,
                            PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                            SeqLayouMensagemEmail = item.SeqLayoutMensagemEmail,
                            Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                          {
                              new NotificacaoEmailDestinatarioData()
                              {
                                  EmailDestinatario = string.Join(";", aluno.EnderecosEletronicos)
                              }
                          }
                        };
                        using (var unitOfWork = SMCUnitOfWork.Begin())
                        {
                            long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                            var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                            var envioNotificacaoDestinatario = EnvioNotificacaoDestinatarioDomainService.SearchByKey(item.SeqEnvioNotificacaoDestinatario);

                            foreach (var envio in envioDestinatario)
                                //Salva a referencia da notificação enviada
                                envioNotificacaoDestinatario.SeqNotificacaoEmailDestinatario = envio.Seq;
                            EnvioNotificacaoDestinatarioDomainService.SaveEntity(envioNotificacaoDestinatario);

                            unitOfWork.Commit();
                        }
                    }
                }
            }
            else
            {
                //Substituir as TAG'S, de acordo com as regras:
                //{ { NOM_PESSOA} }: buscar o nome social do professor / pesquisador em questão, caso exista. Se não existir,
                //buscar o nome do professor / pesquisador;
                var professor = this.ColaboradorDomainService.SearchProjectionByKey(new SMCSeqSpecification<Colaborador>(item.SeqPessoaAtuacao), x => new
                {
                    Seq = x.Seq,
                    Nome = !string.IsNullOrEmpty(x.DadosPessoais.NomeSocial) ? x.DadosPessoais.NomeSocial : x.DadosPessoais.Nome,
                    EnderecosEletronicos = x.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList()
                });

                var dadosMerge = new Dictionary<string, string> { { "{{NOM_PESSOA}}", professor.Nome } };

                if (professor.EnderecosEletronicos == null || !professor.EnderecosEletronicos.Any())
                    Scheduler.LogError("Notificação não enviada." + professor.Nome + " não possui e-mail cadastrado.");
                else
                {

                    var data = new NotificacaoEmailData()
                    {
                        SeqConfiguracaoNotificacao = item.SeqConfiguracaoTipoNotificacao,
                        DadosMerge = dadosMerge,
                        DataPrevistaEnvio = DateTime.Now,
                        PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                        SeqLayouMensagemEmail = item.SeqLayoutMensagemEmail,
                        Destinatarios = new List<NotificacaoEmailDestinatarioData>()
                          {
                              new NotificacaoEmailDestinatarioData()
                              {
                                  EmailDestinatario = string.Join(";", professor.EnderecosEletronicos)
                              }
                          }
                    };

                    using (var unitOfWork = SMCUnitOfWork.Begin())
                    {
                        long seqNotificacaoEnviada = this.NotificacaoService.SalvarNotificacao(data);

                        var envioDestinatario = this.NotificacaoService.BuscaNotificacaoEmailDestinatario(seqNotificacaoEnviada);
                        var envioNotificacaoDestinatario = EnvioNotificacaoDestinatarioDomainService.SearchByKey(item.SeqEnvioNotificacaoDestinatario);

                        foreach (var envio in envioDestinatario)
                            //Salva a referencia da notificação enviada
                            envioNotificacaoDestinatario.SeqNotificacaoEmailDestinatario = envio.Seq;


                        EnvioNotificacaoDestinatarioDomainService.SaveEntity(envioNotificacaoDestinatario);
                        unitOfWork.Commit();
                    }
                }
            }

            return true;
        }
    }
}