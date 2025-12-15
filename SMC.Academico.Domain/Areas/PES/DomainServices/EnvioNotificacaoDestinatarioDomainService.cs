using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Model;
using SMC.Framework.Util;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class EnvioNotificacaoDestinatarioDomainService : AcademicoContextDomain<EnvioNotificacaoDestinatario>
    {
        #region [DomainServices]

        private AlunoDomainService AlunoDomainService = new AlunoDomainService();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => Create<AlunoHistoricoSituacaoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        #endregion

        public EnvioNotificacaoDestinatarioAlunoCabecalhoVO BuscaDadosEnvioNotificacaoDestinatarioAlunoCabecalho(long seqPessoaAtuacao)
        {

         
            var dadosMatricula = AlunoDomainService.BuscarDadosMatriculaAluno(seqPessoaAtuacao);

            var dadosAluno = AlunoHistoricoDomainService.SearchProjectionBySpecification(new AlunoHistoricoFilterSpecification() { SeqAluno = dadosMatricula.SeqAluno, Atual = true },
                p => new
                {
                    SeqAluno = p.Aluno.Seq,
                    SeqNivelEnsino = p.Aluno.Historicos.FirstOrDefault(h => h.Atual).NivelEnsino.Seq,
                    SeqTipoVinculo = p.Aluno.TipoVinculoAluno.Seq,
                    p.Aluno.SeqTipoVinculoAluno,
                    DescricaoTipoVinculo = p.Aluno.TipoVinculoAluno.Descricao,
                    p.Aluno.TermosIntercambio,
                    DescricaoNivelEnsino = p.Aluno.Historicos.FirstOrDefault(a => a.Atual).NivelEnsino.Descricao,
                    p.Aluno.Pessoa.DadosPessoais.Select(d => d).FirstOrDefault().Nome,
                    p.Aluno.Pessoa.DadosPessoais.Select(d => d).FirstOrDefault().NomeSocial,
                    DescricaoSituacaoMatricula = p.Aluno.Historicos
                                                        .FirstOrDefault(a => a.Atual).HistoricosCicloLetivo
                                                        .OrderByDescending(hcl => hcl.CicloLetivo.Ano)
                                                        .ThenByDescending(hcl => hcl.CicloLetivo.Numero)
                                                        .FirstOrDefault(hcl => !hcl.DataExclusao.HasValue).AlunoHistoricoSituacao
                                                        .OrderByDescending(hs => hs.DataInicioSituacao)
                                                        .FirstOrDefault(hs => hs.DataInicioSituacao <= DateTime.Today && !hs.DataExclusao.HasValue).SituacaoMatricula.Descricao,
                    NomeEntidadeResponsavel = p.Aluno.Historicos.FirstOrDefault(a => a.Atual).EntidadeVinculo.Nome,
                    ParceriaTipoTermoIntercambio = p.Aluno.TermosIntercambio.OrderByDescending(t => t.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo,
                    TipoTermoIntercambio = p.Aluno.TermosIntercambio.OrderByDescending(t => t.Seq).FirstOrDefault().TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio,
                }).FirstOrDefault();



            ///RN_ALN_057 - Exibição do ciclo letivo da situação de matrícula válida
            var dadosSituacaoMatricula = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAtualAluno(dadosAluno.SeqAluno);
            var DescricaoCicloLetivo = CicloLetivoDomainService
                                       .SearchProjectionByKey(new CicloLetivoFilterSpecification() { Seq = dadosSituacaoMatricula.SeqCiclo },
                                                              cl => cl.Descricao);

            // RN_ALN_029 - Descrição Vínculo Tipo Termo Intercâmbio
            var configuracaoVinculo = InstituicaoNivelTipoVinculoAlunoDomainService
                                      .BuscarConfiguracaoVinculo(dadosAluno.SeqNivelEnsino, dadosAluno.SeqTipoVinculoAluno);

            var destinatario = new EnvioNotificacaoDestinatarioAlunoCabecalhoVO()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                NumeroRegistroAcademico = dadosMatricula.RA,

                //RN_PES_023 - Quando a pessoa-atuação possuir nome social, exibi-lo seguido do nome da pessoa-atuação entre parênteses: "Nome social" + "(" + "Nome" + ")"
                Nome = PessoaDadosPessoaisDomainService.FormatarNomeSocial(dadosAluno.Nome, dadosAluno.NomeSocial),
                DescricaoSituacaoMatricula = $"{DescricaoCicloLetivo} - {dadosAluno.DescricaoSituacaoMatricula}",
                DescricaoVinculo = AlunoDomainService.RecuperarVinculoAluno(configuracaoVinculo, dadosAluno.ParceriaTipoTermoIntercambio?.SeqTipoTermoIntercambio,
                                                                            dadosAluno.DescricaoTipoVinculo, dadosAluno.TipoTermoIntercambio?.Descricao),
                DescricaoNivelEnsino = dadosAluno.DescricaoNivelEnsino,
                NomeEntidadeResponsavel = dadosAluno.NomeEntidadeResponsavel
            };

            return destinatario;
        }

        public EnvioNotificacaoDestinatarioColaboradorCabecalhoVO BuscaDadosEnvioNotificacaoDestinatarioColaboradorCabecalho(long seqPessoaAtuacao)
        {
            PessoaAtuacaoDomainService.DisableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            var colaborador = PessoaAtuacaoDomainService.SearchProjectionByKey(new PessoaAtuacaoFilterSpecification() { Seq = seqPessoaAtuacao },
            p => new
            {
                p.Seq,
                p.Pessoa.DadosPessoais.Select(c => c).FirstOrDefault().Nome,
                p.Pessoa.DadosPessoais.Select(c => c).FirstOrDefault().NomeSocial,
                p.Pessoa.Cpf,
                p.Pessoa.NumeroPassaporte
            });

            var destinatario = new EnvioNotificacaoDestinatarioColaboradorCabecalhoVO()
            {
                Seq = colaborador.Seq,
                //RN_PES_023 - Quando a pessoa-atuação possuir nome social, exibi-lo seguido do nome da pessoa-atuação entre parênteses: "Nome social" + "(" + "Nome" + ")"
                Nome = PessoaDadosPessoaisDomainService.FormatarNomeSocial(colaborador.Nome, colaborador.NomeSocial),
                CpfOuPassaporte = !string.IsNullOrEmpty(colaborador.Cpf) ? SMCMask.ApplyMaskCPF(colaborador.Cpf) : colaborador.NumeroPassaporte
            };

            PessoaAtuacaoDomainService.EnableFilter(FILTER.HIERARQUIA_ENTIDADE_ORGANIZACIONAL);

            return destinatario;
        }

        public SMCPagerData<EnvioNotificacaoDestinatarioListarVO> BuscarNotificacoesDestinatario(EnvioNotificacaoDestinatarioFilterSpecification spec)
        {
            spec.SetOrderByDescending(x => x.DataInclusao);

            int total = 0;
            var notificacoesEnviadas = this.SearchProjectionBySpecification(spec, p => new EnvioNotificacaoDestinatarioListarVO()
            {
                Seq = p.Seq,
                SeqPessoaAtuacao = p.SeqPessoaAtuacao,
                SeqNotificacaoEmail = p.SeqEnvioNotificacao,
                Assunto = p.EnvioNotificacao.Assunto,
                Remetente = p.EnvioNotificacao.NomeOrigem,
                DataEnvio = p.EnvioNotificacao.DataInclusao,
                UsuarioEnvio = p.UsuarioInclusao,
                SeqNotificacaoEmailDestinatario = p.SeqNotificacaoEmailDestinatario
            }, out total).ToList();

            var retorno = new SMCPagerData<EnvioNotificacaoDestinatarioListarVO>(notificacoesEnviadas, total);

            return retorno;
        }
    }
}