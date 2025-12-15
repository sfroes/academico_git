using SMC.Academico.Common.Areas.DCT.Includes;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.MAT.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.AgendadorTarefa.ServiceContract.Areas.ATS.Interfaces;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class EnvioNotificacaoDomainService : AcademicoContextDomain<EnvioNotificacao>
    {
        #region DomainServices

        private readonly PessoaAtuacaoDomainService PessoaAtuacaoDomainService = new PessoaAtuacaoDomainService();
        private readonly AlunoDomainService AlunoDomainService = new AlunoDomainService();
        private readonly TurmaDomainService TurmaDomainService = new TurmaDomainService();
        private readonly PlanoEstudoItemDomainService PlanoEstudoItemDomainService = new PlanoEstudoItemDomainService();
        private readonly DivisaoTurmaDomainService DivisaoTurmaDomainService = new DivisaoTurmaDomainService();
        private readonly PlanoEstudoDomainService PlanoEstudoDomainService = new PlanoEstudoDomainService();
        private readonly AlunoHistoricoDomainService AlunoHistoricoDomainService = new AlunoHistoricoDomainService();
        private readonly AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService = new AlunoHistoricoCicloLetivoDomainService();
        private readonly ColaboradorDomainService ColaboradorDomainService = new ColaboradorDomainService();
        private readonly ColaboradorVinculoDomainService ColaboradorVinculoDomainService = new ColaboradorVinculoDomainService();
        private readonly TipoVinculoColaboradorDomainService TipoVinculoColaboradorDomainService = new TipoVinculoColaboradorDomainService();
        private readonly TipoVinculoAlunoDomainService TipoVinculoAlunoDomainService = new TipoVinculoAlunoDomainService();
        private readonly EntidadeDomainService EntidadeDomainService = new EntidadeDomainService();
        private readonly NivelEnsinoDomainService NivelEnsinoDomainService = new NivelEnsinoDomainService();
        private readonly CursoOfertaLocalidadeTurnoDomainService CursoOfertaLocalidadeTurnoDomainService = new CursoOfertaLocalidadeTurnoDomainService();
        private readonly CursoOfertaDomainService CursoOfertaDomainService = new CursoOfertaDomainService();
        private readonly CursoOfertaLocalidadeDomainService CursoOfertaLocalidadeDomainService = new CursoOfertaLocalidadeDomainService();
        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();
        private EnvioNotificacaoDestinatarioDomainService EnvioNotificacaoDestinatarioDomainService => Create<EnvioNotificacaoDestinatarioDomainService>();
        private ViewAlunoDomainService ViewAlunoDomainService => Create<ViewAlunoDomainService>();

        #endregion DomainServices

        #region Services

        private INotificacaoService NotificacaoService { get => Create<INotificacaoService>(); }
        private IAgendamentoService AgendamentoService { get => Create<IAgendamentoService>(); }

        #endregion Services

        public SMCPagerData<EnvioNotificacaoListarVO> BuscarEnvioNotificacoes(EnvioNotificacaoFiltroVO filtroVo)
        {
            var spec = new EnvioNotificacaoFilterSpecification()
            {
                Assunto = filtroVo.Assunto,
                Remetente = filtroVo.Remetente,
                UsuarioEnvio = filtroVo.UsuarioEnvio,
                DataEnvio = filtroVo.DataEnvio,
                Seq = filtroVo.Seq,
                SeqPessoaAtuacao = filtroVo.SeqPessoaAtuacao
            };

            int total;

            var tipoAtuacaoConsiderar = new List<TipoAtuacao>() { TipoAtuacao.Aluno, TipoAtuacao.Colaborador };
            if (filtroVo.TipoAtuacao.HasValue && tipoAtuacaoConsiderar.Contains(filtroVo.TipoAtuacao.Value))
                spec.TipoAtuacao = filtroVo.TipoAtuacao;
            else
                spec.TipoAtuacao = null;

            var notificacoesEnviadas = this.SearchProjectionBySpecification(spec, p => new EnvioNotificacaoListarVO()
            {
                Assunto = p.Assunto,
                DataEnvio = p.DataInclusao,
                Remetente = p.NomeOrigem,
                TipoAtuacao = p.TipoAtuacao,
                Seq = p.Seq,
                UsuarioEnvio = p.UsuarioInclusao,
                SeqsDestinatarios = p.Destinatarios.Select(d => d.SeqPessoaAtuacao).ToList(),
                SeqConfiguracaoTipoNotificacao = p.SeqConfiguracaoTipoNotificacao,
                SeqLayoutMensagemEmail = p.SeqLayoutMensagemEmail,
            }, out total).OrderByDescending(x => x.DataEnvio).ToList();

            total = notificacoesEnviadas.Count();
            if (filtroVo.PageSettings != null)
                notificacoesEnviadas = notificacoesEnviadas.Skip((filtroVo.PageSettings.PageIndex - 1) * filtroVo.PageSettings.PageSize).Take(filtroVo.PageSettings.PageSize).ToList();

            var retorno = new SMCPagerData<EnvioNotificacaoListarVO>(notificacoesEnviadas, total);

            return retorno;
        }

        public SMCPagerData<EnvioNotificacaoPessoasListarVO> BuscarPessoasEnvioNotificacoes(EnvioNotificacaoFiltroSelecaoVO filtros)
        {
            int total;
            var retorno = new SMCPagerData<EnvioNotificacaoPessoasListarVO>();

            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var spec = new AlunoFilterSpecification()
                {
                    SeqEntidadesResponsaveis = filtros.SeqsEntidadesResponsaveis,
                    SeqLocalidade = filtros.SeqLocalidade,
                    SeqNivelEnsino = filtros.SeqNivelEnsino,
                    SeqCursoOferta = filtros.SeqCursoOferta,
                    SeqTurno = filtros.SeqTurno,
                    SeqCicloLetivoSituacaoMatricula = filtros.SeqCicloLetivoSituacaoMatricula,
                    SeqsSituacaoMatriculaCicloLetivo = filtros.SeqsSituacaoMatriculaCicloLetivo,
                    SeqFormacaoEspecifica = filtros.SeqFormacaoEspecifica,
                    NumeroRegistroAcademico = filtros.NumeroRegistroAcademico,
                    SeqTipoVinculoAluno = filtros.SeqTipoVinculoAluno
                };

                if (!string.IsNullOrEmpty(filtros.Turma))
                {
                    var specTurma = new TurmaFilterSpecification();
                    var separacaoCodigo = filtros.Turma.Split('.');

                    if (separacaoCodigo.Count() == 1 || string.IsNullOrEmpty(separacaoCodigo[1]))
                        specTurma.Codigo = Convert.ToInt32(separacaoCodigo[0]);
                    else if (separacaoCodigo.Count() == 2)
                    {
                        specTurma.Codigo = !string.IsNullOrEmpty(separacaoCodigo[0]) ? (int?)Convert.ToInt32(separacaoCodigo[0]) : null;
                        specTurma.Numero = Convert.ToInt16(separacaoCodigo[1]);
                    }

                    var turma = TurmaDomainService.SearchByKey(specTurma);
                    var divisoesTurmaSpec = new DivisaoTurmaFilterSpecification() { SeqTurma = turma.Seq };
                    var divisoesTurma = DivisaoTurmaDomainService.SearchBySpecification(divisoesTurmaSpec).Select(x => x.Seq).ToList();
                    spec.SeqsDivisoesTurma = divisoesTurma;
                }

                var alunos = this.AlunoDomainService.SearchProjectionBySpecification(spec, p => new EnvioNotificacaoPessoasListarVO()
                {
                    Seq = p.Seq,
                    NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                    Nome = string.IsNullOrEmpty(p.DadosPessoais.NomeSocial) ? p.DadosPessoais.Nome : p.DadosPessoais.NomeSocial + "(" + p.DadosPessoais.Nome + ")",
                    DadosVinculo = p.Descricao,
                    Vinculo = p.TipoVinculoAluno.Descricao,
                    Turma = filtros.Turma
                }, out total).OrderBy(x => x.Nome).ToList();

                total = alunos.Count();
                alunos = alunos.Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize).Take(filtros.PageSettings.PageSize).ToList();

                foreach (var aluno in alunos)
                {
                    var situacoesMatricula = filtros.SeqCicloLetivoSituacaoMatricula.HasValue ?
                        this.AlunoHistoricoSituacaoDomainService.SearchProjectionBySpecification(new AlunoHistoricoSituacaoFilterSpecification()
                        {
                            SeqPessoaAtuacaoAluno = aluno.Seq,
                            SeqCicloLetivo = filtros.SeqCicloLetivoSituacaoMatricula,
                            SeqsSituacaoMatricula = filtros.SeqsSituacaoMatriculaCicloLetivo,
                            Atual = true
                        }, p => new AlunoListaSituacaoMatriculaVO()
                        {
                            Seq = p.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno,
                            SeqSituacaoMatricula = p.SeqSituacaoMatricula,
                            DescricaoSituacaoMatricula =
                                p.AlunoHistoricoCicloLetivo.CicloLetivo.Numero + "/" +
                                p.AlunoHistoricoCicloLetivo.CicloLetivo.Ano + " - " +
                                p.SituacaoMatricula.Descricao,
                            AnoNumeroCicloLetivo = p.AlunoHistoricoCicloLetivo.CicloLetivo.AnoNumeroCicloLetivo
                        }).ToList() :
                        ViewAlunoDomainService.SearchProjectionBySpecification(new ViewAlunoFilterSpecification() { NumeroRegistroAcademico = aluno.NumeroRegistroAcademico }, p => new AlunoListaSituacaoMatriculaVO()
                        {
                            Seq = p.SeqPessoaAtuacao,
                            SeqSituacaoMatricula = p.SeqSituacaoMatricula,
                            DescricaoSituacaoMatricula = p.DescricaoCicloLetivoSituacaoMatricula,
                            AnoNumeroCicloLetivo = p.AnoNumeroCicloLetivo
                        }).ToList();

                    var situacaoMatricula = situacoesMatricula.First(f => f.Seq == aluno.Seq);
                    aluno.SituacaoMatricula = situacaoMatricula.DescricaoSituacaoMatricula;
                }

                retorno = new SMCPagerData<EnvioNotificacaoPessoasListarVO>(alunos, total);
            }

            if (filtros.TipoAtuacao == TipoAtuacao.Colaborador)
            {
                var colaboradorFiltro = filtros.Transform<ColaboradorFiltroVO>();
                colaboradorFiltro.Seq = filtros.SeqColaborador;

                if (filtros.SeqsEntidadesResponsaveis != null)
                    colaboradorFiltro.SeqsEntidadesResponsaveis = filtros.SeqsEntidadesResponsaveis.ToArray();

                var professores = ColaboradorDomainService.BuscarColaboradoresEnvioNotificacao(colaboradorFiltro);

                foreach (var professor in professores)
                {
                    var vinculosColaborador = ColaboradorVinculoDomainService.SearchBySpecification(new ColaboradorVinculoFilterSpecification() { SeqColaborador = professor.Seq }, IncludesColaboradorVinculo.EntidadeVinculo_TipoEntidade).Select(x => x.SeqEntidadeVinculo).Distinct().ToList();
                    if (vinculosColaborador.Count() > 0)
                    {
                        foreach (var entidade in vinculosColaborador)
                        {
                            var descricaoEntidade = EntidadeDomainService.SearchByKey(new SMCSeqSpecification<Entidade>(entidade)).Nome;
                            if (vinculosColaborador.Count() > 1)
                                professor.Entidade += descricaoEntidade + "; ";
                            else
                                professor.Entidade = descricaoEntidade;
                        }
                    }
                }

                retorno = professores;
            }

            return retorno;
        }

        public SMCPagerData<EnvioNotificacaoPessoasListarVO> BuscarDestinatariosVisualizarNotificacao(VisualizarDestinatariosNotificacaoVO filtros)
        {
            var retorno = new SMCPagerData<EnvioNotificacaoPessoasListarVO>();
            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
            {
                // Monta o specification
                var spec = new AlunoFilterSpecification();
                if (filtros.SeqsDestinatarios?.Any() == true)
                    spec.Seqs = filtros.SeqsDestinatarios.ToArray();
                spec.SetOrderBy(p => p.DadosPessoais.Nome);
                spec.SetPageSetting(filtros.PageSettings);
                var alunos = this.AlunoDomainService.SearchProjectionBySpecification(spec, p => new
                {
                    p.Seq,
                    p.NumeroRegistroAcademico,
                    Nome = p.DadosPessoais.Nome,
                    NomeSocial = p.DadosPessoais.NomeSocial,
                    DadosVinculo = p.Descricao,
                    Vinculo = p.TipoVinculoAluno.Descricao,
                    Emails = p.EnderecosEletronicos.Where(e => e.EnderecoEletronico != null &&
                                                               !string.IsNullOrEmpty(e.EnderecoEletronico.Descricao))
                                                    .Select(e => e.EnderecoEletronico.Descricao)
                }, out int total).ToList();

                var alunoEnvioNotificacaoListarVO = alunos.Select(p => new EnvioNotificacaoPessoasListarVO
                {
                    Seq = p.Seq,
                    NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                    Nome = string.IsNullOrEmpty(p.NomeSocial)
                            ? p.Nome
                            : $"{p.NomeSocial}({p.Nome})",
                    DadosVinculo = p.DadosVinculo,
                    Vinculo = p.Vinculo,
                    Email = string.Join(";", p.Emails)
                });

                var pagerData = new SMCPagerData<EnvioNotificacaoPessoasListarVO>(alunoEnvioNotificacaoListarVO, total);
                retorno = BuscarDetalhesNotificacao(filtros.SeqNotificacao, pagerData);
            }
            if (filtros.TipoAtuacao == TipoAtuacao.Colaborador)
            {
                var colaboradorFiltro = filtros.Transform<ColaboradorFiltroVO>();

                if (filtros.SeqsDestinatarios != null && filtros.SeqsDestinatarios.Count() > 0)
                    colaboradorFiltro.Seqs = filtros.SeqsDestinatarios.ToArray();

                // Aplica o filtro de dados de entidade responsável
                var entidades = EntidadeDomainService.BuscarEntidadesVinculoColaboradorSelect(false);
                colaboradorFiltro.SeqsEntidadesResponsaveis = entidades.Select(e => e.Seq).ToArray();

                var colaboradoresVO = ColaboradorDomainService.BuscarColaboradoresNotificacaoListarVO(colaboradorFiltro, out int total);

                foreach (var professor in colaboradoresVO)
                {
                    var vinculosColaborador = ColaboradorVinculoDomainService.SearchBySpecification(new ColaboradorVinculoFilterSpecification() { SeqColaborador = professor.Seq }, IncludesColaboradorVinculo.EntidadeVinculo_TipoEntidade).ToList();
                    if (vinculosColaborador.Any())
                        professor.Entidade = string.Join(";", vinculosColaborador.Select(v => v.EntidadeVinculo.Nome).Distinct());
                }

                SMCPagerData<EnvioNotificacaoPessoasListarVO> professores = new SMCPagerData<EnvioNotificacaoPessoasListarVO>(colaboradoresVO.ToList(), total);
                retorno = BuscarDetalhesNotificacao(filtros.SeqNotificacao, professores);
            }

            return retorno;
        }

        private SMCPagerData<EnvioNotificacaoPessoasListarVO> BuscarDetalhesNotificacao(long SeqNotificacao,
                                                    SMCPagerData<EnvioNotificacaoPessoasListarVO> destinatarios)
        {
            int total = destinatarios.Total;

            var seqNotificacaoEmailDestinatarioRA = this.SearchProjectionByKey(
                                            new SMCSeqSpecification<EnvioNotificacao>(SeqNotificacao),
                                                x => x.Destinatarios
                                                     .Select(d => new
                                                     {
                                                         d.SeqNotificacaoEmailDestinatario,
                                                         d.SeqPessoaAtuacao
                                                     })
                                                     .ToList()
                                            );

            var dictNotificacaoEmailDestinatarioRA = seqNotificacaoEmailDestinatarioRA
                .ToDictionary(x => x.SeqPessoaAtuacao, x => x.SeqNotificacaoEmailDestinatario);

            foreach (var item in destinatarios)
            {
                item.SeqNotificacaoEmailDestinatario = dictNotificacaoEmailDestinatarioRA[item.Seq];
                item.PermiteVisualizarNotificacao = item.SeqNotificacaoEmailDestinatario != null;
            }

            return new SMCPagerData<EnvioNotificacaoPessoasListarVO>(destinatarios, total);
        }

        public SMCPagerData<EnvioNotificacaoPessoasListarVO> BuscarPessoasEnvioNotificacoesConfirmacao(EnvioNotificacaoVO filtros)
        {
            int total;
            var retorno = new SMCPagerData<EnvioNotificacaoPessoasListarVO>();
            if (filtros.TipoAtuacao == TipoAtuacao.Aluno)
            {
                var spec = new AlunoFilterSpecification() { Seqs = filtros.SelectedValues.ToArray() };

                var alunos = this.AlunoDomainService.SearchProjectionBySpecification(spec, p => new /*EnvioNotificacaoPessoasListarVO()*/
                {
                    Seq = p.Seq,
                    NumeroRegistroAcademico = p.NumeroRegistroAcademico,
                    Nome = string.IsNullOrEmpty(p.DadosPessoais.NomeSocial) ? p.DadosPessoais.Nome : p.DadosPessoais.NomeSocial + "(" + p.DadosPessoais.Nome + ")",
                    Email = p.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(e => e.EnderecoEletronico.Descricao).ToList()
                }, out total).OrderBy(x => x.Nome).ToList();

                total = alunos.Count();
                var retornoAluno = new List<EnvioNotificacaoPessoasListarVO>();
                foreach (var aluno in alunos)
                {
                    retornoAluno.Add(new EnvioNotificacaoPessoasListarVO()
                    {
                        Seq = aluno.Seq,
                        NumeroRegistroAcademico = aluno.NumeroRegistroAcademico,
                        Nome = aluno.Nome,
                        Email = string.Join(";", aluno.Email)
                    });
                }

                retornoAluno = retornoAluno.Skip((filtros.PageSettings.PageIndex - 1) * filtros.PageSettings.PageSize).Take(filtros.PageSettings.PageSize).ToList();

                retorno = new SMCPagerData<EnvioNotificacaoPessoasListarVO>(retornoAluno, total);
            }
            if (filtros.TipoAtuacao == TipoAtuacao.Colaborador)
            {
                var colaboradorFiltro = filtros.Transform<ColaboradorFiltroVO>();

                if (filtros.SelectedValues != null && filtros.SelectedValues.Count() > 0)
                    colaboradorFiltro.Seqs = filtros.SelectedValues.ToArray();

                var professores = ColaboradorDomainService.BuscarColaboradoresEnvioNotificacao(colaboradorFiltro);

                foreach (var professor in professores)
                {
                    var vinculosColaborador = ColaboradorVinculoDomainService.SearchBySpecification(new ColaboradorVinculoFilterSpecification() { SeqColaborador = professor.Seq }, IncludesColaboradorVinculo.EntidadeVinculo_TipoEntidade).ToList();
                    if (vinculosColaborador.Count() > 0)
                    {
                        foreach (var vinculo in vinculosColaborador)
                        {
                            if (vinculosColaborador.Count() > 1)
                                professor.Entidade += vinculo.EntidadeVinculo.Nome + "; ";
                            else
                                professor.Entidade = vinculo.EntidadeVinculo.Nome;
                        }
                    }
                }

                retorno = professores;
            }

            return retorno;
        }

        public void SalvarEEnviarNotificacao(EnvioNotificacaoVO model)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                try
                {
                    var envioNotificacao = SalvarEnvioNotificacao(model);
                    CriarAgendamentoEnvioNotificacao(envioNotificacao);

                    transacao.Commit();
                }
                catch (Exception e)
                {
                    transacao.Rollback();
                    throw e;
                }
            }
        }

        public void ValidaTagsEnvioNotificacao(EnvioNotificacaoVO modelo)
        {
            Regex regex = new Regex(@"{{[_A-Za-z_]+}}");
            if (modelo != null && modelo.ConfiguracaoNotificacao != null && !string.IsNullOrEmpty(modelo.ConfiguracaoNotificacao.Mensagem))
            {
                var tagsMensagem = regex.Matches(modelo.ConfiguracaoNotificacao.Mensagem).Cast<Match>().Select(m => m.Value).ToList();
                string tagsInvalidas = string.Empty;

                var tipoNotificacao = NotificacaoService.BuscarTipoNotificacao(modelo.ConfiguracaoNotificacao.SeqTipoNotificacao);

                if (modelo.ConfiguracaoNotificacao.Tags == null)
                    modelo.ConfiguracaoNotificacao.Tags = tipoNotificacao.Tags;

                foreach (var item in tagsMensagem)
                {
                    if (modelo.ConfiguracaoNotificacao.Tags == null || !modelo.ConfiguracaoNotificacao.Tags.Any(a => a.Nome.ToLower() == item.ToLower()))
                        tagsInvalidas += $"<br />- {item}";
                }

                if (!string.IsNullOrEmpty(tagsInvalidas))
                    throw new EnvioNotificacaoTagsInvalidasException(tagsInvalidas, tipoNotificacao.Descricao);
            }
        }

        public long SalvarEnvioNotificacao(EnvioNotificacaoVO model)
        {

            var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
            var descricaoTipoNotificacao = NotificacaoService.BuscarTipoNotificacao(model.ConfiguracaoNotificacao.SeqTipoNotificacao).Descricao;

            var modelConfiguracao = new ConfiguracaoNotificacaoEmailData()
            {
                Assunto = model.ConfiguracaoNotificacao.Assunto,
                EmailOrigem = model.ConfiguracaoNotificacao.EmailOrigem,
                EmailResposta = model.ConfiguracaoNotificacao.EmailResposta,
                NomeOrigem = model.ConfiguracaoNotificacao.NomeOrigem,
                SeqTipoNotificacao = model.ConfiguracaoNotificacao.SeqTipoNotificacao,
                Mensagem = model.ConfiguracaoNotificacao.Mensagem,
                Descricao = descricaoTipoNotificacao,
                DataInicioValidade = DateTime.Now
            };
            long seqConfiguracaoNotificacao = this.NotificacaoService.SalvarConfiguracaoTipoNotificacao(modelConfiguracao);

            var envioNotificacao = new EnvioNotificacao()
            {
                SeqConfiguracaoTipoNotificacao = seqConfiguracaoNotificacao,
                NomeOrigem = model.ConfiguracaoNotificacao.NomeOrigem,
                Assunto = model.ConfiguracaoNotificacao.Assunto,
                TipoAtuacao = model.TipoAtuacao,
                DataInclusao = DateTime.Now,
                UsuarioInclusao = usuarioInclusao,
                SeqLayoutMensagemEmail = model.SeqLayoutEmail
            };

            this.SaveEntity(envioNotificacao);

            if (model.TipoAtuacao == TipoAtuacao.Aluno)
            {
                foreach (var item in model.SelectedValues)
                {
                    var envioNotificacaoDestinatario = new EnvioNotificacaoDestinatario()
                    {
                        SeqPessoaAtuacao = item,
                        SeqEnvioNotificacao = envioNotificacao.Seq,
                        UsuarioInclusao = usuarioInclusao,
                        DataInclusao = DateTime.Now
                    };

                    EnvioNotificacaoDestinatarioDomainService.SaveEntity(envioNotificacaoDestinatario);
                }
            }
            else
            {
                foreach (var item in model.SelectedValues)
                {

                    var envioNotificacaoDestinatario = new EnvioNotificacaoDestinatario()
                    {
                        SeqPessoaAtuacao = item,
                        SeqEnvioNotificacao = envioNotificacao.Seq,
                        UsuarioInclusao = usuarioInclusao,
                        DataInclusao = DateTime.Now
                    };

                    EnvioNotificacaoDestinatarioDomainService.SaveEntity(envioNotificacaoDestinatario);
                }
            }

            return envioNotificacao.Seq;
        }

        public List<RealizarEnvioNotificacaoVO> BuscarEnvioNotificacaoDestinatarioJob(long seqEnvioNotificacao)
        {
            var envioNotificacaoDestinatario = EnvioNotificacaoDestinatarioDomainService.SearchProjectionBySpecification(new EnvioNotificacaoDestinatarioFilterSpecification
            {
                SeqEnvioNotificacao = seqEnvioNotificacao
            }, x => new RealizarEnvioNotificacaoVO()
            {
                SeqConfiguracaoTipoNotificacao = x.EnvioNotificacao.SeqConfiguracaoTipoNotificacao,
                SeqEnvioNotificacao = x.EnvioNotificacao.Seq,
                SeqLayoutMensagemEmail = x.EnvioNotificacao.SeqLayoutMensagemEmail,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                TipoAtuacao = x.EnvioNotificacao.TipoAtuacao,
                SeqEnvioNotificacaoDestinatario = x.Seq
            });


            return envioNotificacaoDestinatario.TransformList<RealizarEnvioNotificacaoVO>();
        }

        public void RealizarEnvioNotificacaoJob(RealizarEnvioNotificacaoSATVO filtro)
        {
            var servico = Create<Jobs.RealizarEnvioNotificacaoWebJob>();
            servico.Execute(filtro);
        }

        public void CriarAgendamentoEnvioNotificacao(long seqEnvioNotificacao)
        {
            var parametros = AgendamentoService.BuscarParametrosServico(TOKEN_AGENDAMENTO.ENVIO_NOTIFICACAO_PESSOA_ATUACAO);
            foreach (var parametro in parametros)
                parametro.ValorParametro = seqEnvioNotificacao.ToString();

            var descricao = "Envio de notificações (seq = " + seqEnvioNotificacao + ")";
            var agendamento = AgendamentoService.CriarAgendamentoPorTokenServico(TOKEN_AGENDAMENTO.ENVIO_NOTIFICACAO_PESSOA_ATUACAO, descricao, parametros);

            var envioNotificacao = this.SearchByKey(new SMCSeqSpecification<EnvioNotificacao>(seqEnvioNotificacao));
            envioNotificacao.SeqAgendamentoSat = agendamento.SeqAgendamento;

            this.UpdateFields(envioNotificacao, e => e.SeqAgendamentoSat);
        }
    }
}