using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Resources;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Specifications;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.Domain.Areas.CSO.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class AlunoFormacaoDomainService : AcademicoContextDomain<AlunoFormacao>
    {
        #region [ DomainServices ]

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private FormacaoEspecificaDomainService FormacaoEspecificaDomainService => Create<FormacaoEspecificaDomainService>();

        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService => Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private DocumentoConclusaoFormacaoDomainService DocumentoConclusaoFormacaoDomainService => Create<DocumentoConclusaoFormacaoDomainService>();

        private DocumentoConclusaoDomainService DocumentoConclusaoDomainService => Create<DocumentoConclusaoDomainService>();

        private ApuracaoFormacaoDomainService ApuracaoFormacaoDomainService => Create<ApuracaoFormacaoDomainService>();

        private CursoFormacaoEspecificaDomainService CursoFormacaoEspecificaDomainService => Create<CursoFormacaoEspecificaDomainService>();

        private CursoOfertaDomainService CursoOfertaDomainService => Create<CursoOfertaDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private InstituicaoNivelTipoDocumentoAcademicoDomainService InstituicaoNivelTipoDocumentoAcademicoDomainService => Create<InstituicaoNivelTipoDocumentoAcademicoDomainService>();

        private TipoDocumentoAcademicoDomainService TipoDocumentoAcademicoDomainService => Create<TipoDocumentoAcademicoDomainService>();

        #endregion [ DomainServices ]

        #region [ Services ]

        private IIntegracaoAcademicoService IntegracaoAcademicoService => Create<IIntegracaoAcademicoService>();

        #endregion

        /// <summary>
        /// Busca as associações de formação específica do aluno
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <returns>Dados das assoociações de formação específica do aluno</returns>
        public AssociacaoFormacaoEspecificaAlunoVO BuscarAssociacaoFormacaoEspecifica(long seqAluno)
        {
            var spec = new SMCSeqSpecification<Aluno>(seqAluno);
            var formacoesAluno = AlunoDomainService.SearchProjectionByKey(spec, p => new
            {
                p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqCurso,
                p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.CursoOfertaLocalidade.CursoOferta.SeqFormacaoEspecifica,
                p.Historicos.FirstOrDefault(f => f.Atual).CursoOfertaLocalidadeTurno.SeqCursoOfertaLocalidade,
                FormacoesEspecificasAluno = p.Historicos.FirstOrDefault(f => f.Atual).Formacoes.Where(w => w.DataFim == null).Select(s => (long)s.SeqFormacaoEspecifica)
            });

            return new AssociacaoFormacaoEspecificaAlunoVO()
            {
                SeqAluno = seqAluno,
                SeqCurso = formacoesAluno.SeqCurso,
                SeqCursoOfertaLocalidade = formacoesAluno.SeqCursoOfertaLocalidade,
                SeqFormacaoEspecifica = formacoesAluno.SeqFormacaoEspecifica,
                FormacoesEspecificas = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(formacoesAluno.FormacoesEspecificasAluno.ToArray())
            };
        }

        /// <summary>
        /// Grava as associações com foramação específica do aluno
        /// </summary>
        /// <param name="associacaoFormacaoEspecificaAlunoData">Dados das associações de formações específicas do aluno</param>
        /// <exception cref="AssociacaoFormacaoEspecificaIngressanteException">Caso não sejam informadas as quantidades de formações configuradas por tipo</exception>
        public void SalvarAssociacaoFormacaoEspecifica(AssociacaoFormacaoEspecificaAlunoVO associacao)
        {
            // RN_ALN_019.1 Validação de formações obrigatórias
            if (associacao.FormacoesEspecificas == null)
            {
                associacao.FormacoesEspecificas = new List<FormacaoEspecificaHierarquiaVO>();
            }
            var tiposFormacaoInvalidos = InstituicaoTipoEntidadeFormacaoEspecificaDomainService
                .BuscarObrigatoriedadeFormacoesNaoAtendidasPorCurso(associacao.SeqCurso, associacao.FormacoesEspecificas.Select(s => s.Seq), true)
                .Select(s => string.Format(MessagesResource.AssociacaoFormacaoEspecificaIngressanteQtdTipoFormacaoEspecifica,
                                           s.QuantidadePermitidaAssociacaoAluno,
                                           s.TipoFormacaoEspecifica.Descricao));
            if (tiposFormacaoInvalidos.Any())
                throw new AssociacaoFormacaoEspecificaIngressanteException(string.Join("", tiposFormacaoInvalidos));

            // Recuperar os dados atuais do aluno
            var dadosAluno = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(associacao.SeqAluno), p => new
            {
                p.CodigoAlunoMigracao,
                SeqAlunoHistoricoAtual = p.Historicos.FirstOrDefault(f => f.Atual).Seq,
                p.Historicos.FirstOrDefault(f => f.Atual).SeqCursoOfertaLocalidadeTurno,
                Bloqueios = p.Bloqueios.Where(w => w.MotivoBloqueio.Token == TOKEN_MOTIVO_BLOQUEIO.FORMACAO_ESPECIFICA_PENDENTE &&
                                                   w.SituacaoBloqueio == SituacaoBloqueio.Bloqueado),
                FormacoesEspecificas = p.Historicos.FirstOrDefault(f => f.Atual).Formacoes.Where(w => !w.DataFim.HasValue).Select(s => new
                {
                    AlunoFormacao = s,
                    s.ApuracoesFormacao
                })
            });

            // RN_ALN_052 Desbloqueio formação específica
            var bloqueios = dadosAluno.Bloqueios.ToList();
            foreach (var bloqueio in bloqueios)
            {
                bloqueio.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                bloqueio.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                bloqueio.UsuarioDesbloqueioEfetivo = SMCContext.User.Identity.Name;
                bloqueio.DataDesbloqueioEfetivo = DateTime.Now;
                bloqueio.JustificativaDesbloqueio = MessagesResource.MSG_DesbloqueioFormacaoAluno;
            }

            // Recuperação das formações atuais
            var seqsFormacoesAtuais = dadosAluno.FormacoesEspecificas.Where(w => w.AlunoFormacao.DataFim == null).Select(s => s.AlunoFormacao.SeqFormacaoEspecifica).ToList();
            var seqsFormacoesRemovidas = seqsFormacoesAtuais.Except(associacao.FormacoesEspecificas.Select(s => s.Seq)).ToList();
            var seqsFormacoesAdicionadas = associacao.FormacoesEspecificas.Select(s => s.Seq).Except(seqsFormacoesAtuais).ToList();

            // UC_ALN_001_04_01.NV01
            var formacoesAtualizar = new List<AlunoFormacao>();
            // Inclui na lista de atualização as formações a serem canceladas
            foreach (var seqFormacaoRemovida in seqsFormacoesRemovidas)
            {
                var dadosFormacaoFormacao = dadosAluno.FormacoesEspecificas.First(f => f.AlunoFormacao.SeqFormacaoEspecifica == seqFormacaoRemovida);
                var formacao = dadosFormacaoFormacao.AlunoFormacao;
                DateTime dataAtual;
                formacao.ApuracoesFormacao = dadosFormacaoFormacao.ApuracoesFormacao ?? new List<ApuracaoFormacao>();

                ///Verifica para não colocar data fim menor que data inicio
                if (formacao.DataInicio > DateTime.Now.AddDays(-1))
                {
                    dataAtual = DateTime.Now;
                }
                else
                {
                    dataAtual = DateTime.Now.AddDays(-1);
                }

                formacao.DataFim = dataAtual;
                formacao.ApuracoesFormacao.Add(new ApuracaoFormacao()
                {
                    SituacaoAlunoFormacao = SituacaoAlunoFormacao.Cancelado,
                    DataInicio = dataAtual
                });
                formacoesAtualizar.Add(formacao);
            }

            var cursoFormacaoEspecifica = new List<CursoFormacaoEspecificaVO>();
            string descricaoDocumentoConclusao = string.Empty;
            if (seqsFormacoesAdicionadas.Any())
            {
                cursoFormacaoEspecifica = CursoFormacaoEspecificaDomainService.BuscarCursoFormacaoEspecifica(new CursoFormacaoEspecificaFilterSpecification { SeqCurso = associacao.SeqCurso });
                descricaoDocumentoConclusao = CursoOfertaDomainService.BuscarDescricaoDocumentoConclusao(associacao.SeqCurso);
            }

            // Inclui na lista de atualização as formações a serem criadas
            foreach (var seqFormacaoAdicionada in seqsFormacoesAdicionadas)
            {

                long? seqTitulacao = 0;
                var titulacoes = cursoFormacaoEspecifica.Where(w => w.SeqFormacaoEspecifica == seqFormacaoAdicionada && w.Titulacoes.Any(a => a.SeqCursoFormacaoEspecifica == w.Seq && a.Ativo))
                                                        .Select(s => s.Titulacoes).FirstOrDefault();
                if (titulacoes != null)
                    seqTitulacao = titulacoes.Select(s => s.SeqTitulacao).FirstOrDefault();

                formacoesAtualizar.Add(new AlunoFormacao()
                {
                    SeqAlunoHistorico = dadosAluno.SeqAlunoHistoricoAtual,
                    SeqFormacaoEspecifica = seqFormacaoAdicionada,
                    DataInicio = DateTime.Now,
                    ApuracoesFormacao = new List<ApuracaoFormacao>()
                    {
                        new ApuracaoFormacao()
                        {
                            SituacaoAlunoFormacao = SituacaoAlunoFormacao.AConcluir,
                            DataInicio = DateTime.Now
                        }
                    },
                    SeqTitulacao = seqTitulacao > 0 ? seqTitulacao : null,
                    DescricaoDocumentoConclusao = descricaoDocumentoConclusao
                });
            }

            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                foreach (var alunoFormacao in formacoesAtualizar)
                {
                    this.SaveEntity(alunoFormacao);
                }

                foreach (var bloqueio in bloqueios)
                {
                    PessoaAtuacaoBloqueioDomainService.SaveEntity(bloqueio);
                }

                if (formacoesAtualizar.SMCAny())
                {
                    AtualizarFormacaoEspecificaAlunoSGP(associacao.SeqAluno, dadosAluno.CodigoAlunoMigracao, dadosAluno.SeqCursoOfertaLocalidadeTurno);
                }

                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Busca as formações específicas do aluno
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno histórico</param>
        /// <returns>Dados das assoociações de formação específica do aluno</returns>
        public List<AlunoFormacaoVO> BuscarSequenciaisFormacoesAlunoHistorico(long seqAlunoHistorico)
        {
            var spec = new AlunoFormacaoFilterSpecification() { SeqAlunoHistorico = seqAlunoHistorico, Ativo = true };

            var seqsFormacoes = SearchProjectionBySpecification(spec, p => new AlunoFormacaoVO()
            {
                SeqFormacaoEspecifica = p.SeqFormacaoEspecifica,
                TokenTipoFormacaoEspecifica = p.FormacaoEspecifica.TipoFormacaoEspecifica.Token
            }).ToList();

            return seqsFormacoes;
        }

        public List<SMCDatasourceItem> BuscarFormacoesAlunoTipoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqGrauAcademico)
        {
            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();

            var dadosDocumentoConclusao = this.DocumentoConclusaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<DocumentoConclusao>(seqDocumentoConclusao), x => new
            {
                x.SeqTipoDocumentoAcademico,
                x.SeqAtuacaoAluno
            });
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dadosDocumentoConclusao.SeqAtuacaoAluno);

            //Recupera o sequencial da instituição nível
            var seqInstituicaoNivel = InstituicaoNivelDomainService.SearchProjectionByKey(new InstituicaoNivelFilterSpecification
            {
                SeqInstituicaoEnsino = dadosOrigem.SeqInstituicaoEnsino,
                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
            }, x => x.Seq);

            var documentosConclusaoFormacao = this.DocumentoConclusaoFormacaoDomainService.BuscarDocumentosConclusaoFormacaoPorDocumentoConclusao(seqDocumentoConclusao);

            var spec = new AlunoFormacaoFilterSpecification()
            {
                AlunoHistoricoAtual = true,
                SeqPessoa = seqPessoa,
                SeqCurso = seqCurso,
                SeqGrauAcademico = seqGrauAcademico,
                SeqsAlunoFormacaoDiferente = documentosConclusaoFormacao.Select(a => a.SeqAlunoFormacao).ToList()
            };

            var dadosAlunoFormacao = this.SearchProjectionBySpecification(spec, x => new
            {
                SeqPessoaAtuacao = x.AlunoHistorico.Aluno.Seq,
                SeqAlunoFormacao = x.Seq,
                SeqFormacaoEspecifica = x.SeqFormacaoEspecifica,
                SeqTipoFormacaoEspecifica = x.FormacaoEspecifica.SeqTipoFormacaoEspecifica,
                GeraCarimbo = x.FormacaoEspecifica.TipoFormacaoEspecifica.GeraCarimbo,
                PermiteEmitirDocumentoConclusao = x.FormacaoEspecifica.TipoFormacaoEspecifica.PermiteEmitirDocumentoConclusao
            }).ToList();

            foreach (var dadoAlunoFormacao in dadosAlunoFormacao)
            {
                var specInstituicaoNivelTipoDocumentoAcademico = new InstituicaoNivelTipoDocumentoAcademicoFilterSpecification()
                {
                    SeqInstituicaoNivel = seqInstituicaoNivel,
                    SeqsTipoFormacaoEspecifica = new List<long>() { dadoAlunoFormacao.SeqTipoFormacaoEspecifica }
                };

                var instituicoesNivelTipoDocumentoAcademico = InstituicaoNivelTipoDocumentoAcademicoDomainService.SearchBySpecification(specInstituicaoNivelTipoDocumentoAcademico).ToList();
                var tipoFormacaoPermiteTipoDocumentoSolicitado = false;

                // Validando se o tipo de formação permite o tipo de documento de conclusão solicitado 
                foreach (var instituicaoNivelTipoDocumentoAcademico in instituicoesNivelTipoDocumentoAcademico)
                {
                    var tipoDocumentoAcademicoParametrizado = TipoDocumentoAcademicoDomainService.SearchByKey(new SMCSeqSpecification<TipoDocumentoAcademico>(instituicaoNivelTipoDocumentoAcademico.SeqTipoDocumentoAcademico));

                    if (dadosDocumentoConclusao.SeqTipoDocumentoAcademico == tipoDocumentoAcademicoParametrizado.Seq)
                    {
                        tipoFormacaoPermiteTipoDocumentoSolicitado = true;
                        break;
                    }
                }

                /*TASK 41274: O tipo da formação também esteja parametrizado para gerar carimbo no verso do documento e também 
                permite emitir o mesmo tipo do documento em questão.*/
                if (dadoAlunoFormacao.GeraCarimbo.GetValueOrDefault() && dadoAlunoFormacao.PermiteEmitirDocumentoConclusao && tipoFormacaoPermiteTipoDocumentoSolicitado)
                {
                    var apuracoesFormacao = this.ApuracaoFormacaoDomainService.BuscarApuracoesFormacaoPorAlunoFormacao(dadoAlunoFormacao.SeqAlunoFormacao);

                    if (apuracoesFormacao.Any())
                    {
                        var apuracaoFormacaoAtual = apuracoesFormacao.OrderByDescending(o => o.DataInclusao).ThenByDescending(o => o.Seq).FirstOrDefault();

                        if (apuracaoFormacaoAtual.SituacaoAlunoFormacao == SituacaoAlunoFormacao.Formado)
                        {
                            SMCDatasourceItem item = new SMCDatasourceItem()
                            {
                                Seq = dadoAlunoFormacao.SeqAlunoFormacao
                            };

                            var descricoesFormacoesEspecificas = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(dadoAlunoFormacao.SeqPessoaAtuacao).Select(x => x.DescricaoFormacaoEspecifica).ToList();

                            if (descricoesFormacoesEspecificas != null && descricoesFormacoesEspecificas.Any())
                            {
                                item.Descricao = string.Join(Environment.NewLine, descricoesFormacoesEspecificas);
                            }
                            else
                            {
                                var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { dadoAlunoFormacao.SeqFormacaoEspecifica });
                                var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                                item.Descricao = string.Join(Environment.NewLine, hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList());
                            }

                            result.Add(item);
                        }
                    }
                }
            }

            return result.OrderBy(o => o.Descricao).ToList();
        }

        public List<SMCDatasourceItem> BuscarFormacoesAlunoTipoNaoExigeGrauAcademico(long seqDocumentoConclusao, long? seqPessoa, long? seqCurso, long? seqTipoFormacaoEspecifica)
        {
            List<SMCDatasourceItem> result = new List<SMCDatasourceItem>();

            var documentosConclusaoFormacao = this.DocumentoConclusaoFormacaoDomainService.BuscarDocumentosConclusaoFormacaoPorDocumentoConclusao(seqDocumentoConclusao);

            var spec = new AlunoFormacaoFilterSpecification()
            {
                AlunoHistoricoAtual = true,
                SeqPessoa = seqPessoa,
                SeqCurso = seqCurso,
                SeqTipoFormacaoEspecifica = seqTipoFormacaoEspecifica,
                SeqsAlunoFormacaoDiferente = documentosConclusaoFormacao.Select(a => a.SeqAlunoFormacao).ToList()
            };

            var dadosAlunoFormacao = this.SearchProjectionBySpecification(spec, x => new
            {
                SeqPessoaAtuacao = x.AlunoHistorico.Aluno.Seq,
                SeqAlunoFormacao = x.Seq,
                SeqFormacaoEspecifica = x.SeqFormacaoEspecifica

            }).ToList();

            foreach (var dadoAlunoFormacao in dadosAlunoFormacao)
            {
                var apuracoesFormacao = this.ApuracaoFormacaoDomainService.BuscarApuracoesFormacaoPorAlunoFormacao(dadoAlunoFormacao.SeqAlunoFormacao);

                if (apuracoesFormacao.Any())
                {
                    var apuracaoFormacaoAtual = apuracoesFormacao.OrderByDescending(o => o.DataInclusao).ThenByDescending(o => o.Seq).FirstOrDefault();

                    if (apuracaoFormacaoAtual.SituacaoAlunoFormacao == SituacaoAlunoFormacao.Formado)
                    {
                        SMCDatasourceItem item = new SMCDatasourceItem()
                        {
                            Seq = dadoAlunoFormacao.SeqAlunoFormacao
                        };

                        var descricoesFormacoesEspecificas = FormacaoEspecificaDomainService.BuscarDescricaoFormacaoEspecifica(dadoAlunoFormacao.SeqPessoaAtuacao).Select(x => x.DescricaoFormacaoEspecifica).ToList();

                        if (descricoesFormacoesEspecificas != null && descricoesFormacoesEspecificas.Any())
                        {
                            item.Descricao = string.Join(Environment.NewLine, descricoesFormacoesEspecificas);
                        }
                        else
                        {
                            var descricaoFormacaoEspecifica = FormacaoEspecificaDomainService.BuscarFormacoesEspecificasHierarquia(new long[] { dadoAlunoFormacao.SeqFormacaoEspecifica });
                            var hierarquiasFormacao = descricaoFormacaoEspecifica.SelectMany(a => a.Hierarquia).ToList();
                            item.Descricao = string.Join(Environment.NewLine, hierarquiasFormacao.Select(a => $"[{a.DescricaoTipoFormacaoEspecifica}] {a.Descricao}").ToList());
                        }

                        result.Add(item);
                    }
                }
            }

            return result.OrderBy(o => o.Descricao).ToList();
        }

        private void AtualizarFormacaoEspecificaAlunoSGP(long seqAluno, int? codigoAlunoMigracao, long? seqCursoOfertaLocalidadeTurno)
        {
            if (codigoAlunoMigracao == null)
            {
                throw new ArgumentNullException(nameof(codigoAlunoMigracao));
            }
            if (seqCursoOfertaLocalidadeTurno == null)
            {
                throw new ArgumentNullException(nameof(seqCursoOfertaLocalidadeTurno));
            }

            var formacoesAtualizar = AlunoDomainService.SearchProjectionByKey(seqAluno, p =>
                p.Historicos.FirstOrDefault(f => f.Atual)
                    .Formacoes.Where(w => w.FormacaoEspecifica.TipoFormacaoEspecifica.Token == TOKEN_TIPO_FORMACAO_ESPECIFICA.LINHA_PESQUISA)
                    .Select(s => new
                    {
                        s.SeqFormacaoEspecifica,
                        s.DataInicio,
                        s.DataFim
                    })
            );

            var atualizacaoSGP = new AlunoFormacaoEspecificaSGPData()
            {
                CodigoAluno = codigoAlunoMigracao.Value,
                FormacaoEspecificas = formacoesAtualizar
                    .Select(s => new FormacaoEspecificaSGPData()
                    {
                        SeqFormacaoEspecifica = s.SeqFormacaoEspecifica,
                        SeqCursoOfertaLocalidadeTurno = seqCursoOfertaLocalidadeTurno.Value,
                        DataInicio = s.DataInicio,
                        DataFim = s.DataFim
                    }).ToList()
            };

            IntegracaoAcademicoService.AtualizarFormacaoEspecificaAlunoSGP(atualizacaoSGP);
        }
    }
}