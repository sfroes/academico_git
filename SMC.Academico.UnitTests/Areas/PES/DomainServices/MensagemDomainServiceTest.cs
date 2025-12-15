using Moq;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.GRD.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.UnitTests.Ioc;
using SMC.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SMC.Academico.UnitTests.Areas.PES.DomainServices
{
    public class MensagemDomainServiceTest
    {
        private MensagemPessoaAtuacaoDomainService _MensagemPessoaAtuacaoDomainService;
        private TestContainerAcademico _Container;

        public MensagemDomainServiceTest()
        {
            _Container = new TestContainerAcademico();
            _MensagemPessoaAtuacaoDomainService = new MensagemPessoaAtuacaoDomainService();
        }

        public Mensagem CriarMensagem(long seq, long seqTipoMensagem, DateTime dataInicioVigencia, DateTime dataFimVigencia)
        {
            var pessoas = new List<MensagemPessoaAtuacao>();
            //pessoas.Add(PesFactory.GetMensagemPessoaAtuacao(1, seq, 12345678));
            //return PesFactory.GetMensagem(seq, seqTipoMensagem, dataInicioVigencia, dataFimVigencia, pessoas, PesFactory.GetTipoMensagem(null, CategoriaMensagem.Ocorrencia));

            pessoas.Add(new MensagemPessoaAtuacao() { Seq = 1, SeqMensagem = seq, SeqPessoaAtuacao = 12345678 });

            return new Mensagem()
            {
                Seq = seq,
                SeqTipoMensagem = seqTipoMensagem,
                DataInicioVigencia = dataInicioVigencia,
                DataFimVigencia = dataFimVigencia,
                TipoMensagem = new TipoMensagem()
                {
                    CategoriaMensagem = CategoriaMensagem.Ocorrencia
                },
                Pessoas = pessoas,
            };
        }

        public MensagemVO CriarMensagemVO(long seq, long seqTipoMensagem, DateTime dataInicioVigencia, DateTime dataFimVigencia)
        {
            return new MensagemVO()
            {
                Seq = seq,
                SeqTipoMensagem = seqTipoMensagem,
                SeqPessoaAtuacao = 12345678,
                DataInicioVigencia = dataInicioVigencia,
                DataFimVigencia = dataFimVigencia,
            };
        }

        public List<MensagemPessoaAtuacao> CriarMensagemPessoaAtuacao(long seqMensagem = 11)
        {
            var retorno = new List<MensagemPessoaAtuacao>();
            //retorno.Add(PesFactory.GetMensagemPessoaAtuacao(1, seqMensagem, 12345678, CriarMensagem(seqMensagem, 111, new DateTime(2021, 4, 26), new DateTime(2021, 4, 30))));
            //return retorno;

            //var retorno = new List<MensagemPessoaAtuacao>();
            retorno.Add(new MensagemPessoaAtuacao()
            {
                Seq = 1,
                SeqMensagem = seqMensagem,
                SeqPessoaAtuacao = 12345678,
                Mensagem = CriarMensagem(seqMensagem, 111, new DateTime(2021, 4, 26), new DateTime(2021, 4, 30))
            });
            return retorno;
        }

        public List<EventoAula> CriarEventosAula(DateTime[] datas)
        {
            List<EventoAula> retorno = new List<EventoAula>();
            int index = 1;
            foreach (var data in datas)
            {
                retorno.Add(new EventoAula() { Seq = index * 11, Data = data });
                index++;
            }
            return retorno;
        }

        public List<ApuracaoFrequenciaGrade> CriarFrequencias(long? seqMensagem = null,
                                                              Frequencia frequencia = Frequencia.Nenhum,
                                                              OcorrenciaFrequencia? ocorrenciaFrequencia = null)
        {
            var retorno = new List<ApuracaoFrequenciaGrade>()
            {
                new ApuracaoFrequenciaGrade(){Seq = 10, SeqAlunoHistoricoCicloLetivo = 12345, SeqEventoAula = 11, SeqMensagem = seqMensagem, Frequencia = frequencia, OcorrenciaFrequencia = ocorrenciaFrequencia },
                new ApuracaoFrequenciaGrade(){Seq = 20, SeqAlunoHistoricoCicloLetivo = 12345, SeqEventoAula = 22, SeqMensagem = seqMensagem, Frequencia = frequencia, OcorrenciaFrequencia = ocorrenciaFrequencia },
                new ApuracaoFrequenciaGrade(){Seq = 30, SeqAlunoHistoricoCicloLetivo = 12345, SeqEventoAula = 33, SeqMensagem = seqMensagem, Frequencia = frequencia, OcorrenciaFrequencia = ocorrenciaFrequencia }
            };

            _Container.CreateRepository(new List<AlunoHistoricoCicloLetivo>());
            _Container.CreateRepository(new List<SolicitacaoServico>());
            _Container.CreateRepository(new List<Mensagem>());
            _Container.CreateRepository(new List<EventoAula>());
            _Container.CreateRepository(new List<Turma>());

            var mockApuracaoFrequencia = _Container.CreateMock<ApuracaoFrequenciaGradeDomainService>();
            mockApuracaoFrequencia.Setup(s => s.BuscarApuracaoFrequenciaGradePorAluno(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                                  .Returns(retorno.TransformList<ApuracaoFrequenciaGradeVO>());
            mockApuracaoFrequencia.Setup(s => s.UpdateFields(It.IsAny<ApuracaoFrequenciaGrade>(), It.IsAny<Expression<Func<ApuracaoFrequenciaGrade, object>>[]>()))
                                  .Callback<ApuracaoFrequenciaGrade, Expression<Func<ApuracaoFrequenciaGrade, object>>[]>((model, expression) =>
                                  {
                                      var data = retorno.First(f => f.Seq == model.Seq);
                                      data.SeqMensagem = model.SeqMensagem;
                                      data.OcorrenciaFrequencia = model.OcorrenciaFrequencia;
                                  });
            var mockPlanoEstudoItemDomainService = _Container.CreateMock<PlanoEstudoItemDomainService>();
            mockPlanoEstudoItemDomainService.Setup(s => s.BuscarTurmasAlunoNoPeriodo(It.IsAny<long>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                                            .Returns(new List<PlanoEstudoItemTurmasAlunoVO>());

            return retorno;
        }

        public List<TipoMensagem> CriarTipoMensagem()
        {
            var retorno = new List<TipoMensagem>() {
            new TipoMensagem() { Seq = 111, CategoriaMensagem = CategoriaMensagem.Ocorrencia, Token = TOKEN_TIPO_MENSAGEM.ABONO_POR_PERIODO },
            new TipoMensagem() { Seq = 222, CategoriaMensagem = CategoriaMensagem.Ocorrencia, Token = TOKEN_TIPO_MENSAGEM.REGIME_ESPECIAL_ESTUDO },
            new TipoMensagem() { Seq = 333, CategoriaMensagem = CategoriaMensagem.Ocorrencia, Token = TOKEN_TIPO_MENSAGEM.SANSAO_DISCIPLINAR_SUSPENSAO }
        };

            return retorno;
        }

        public void InicializarRepositorioSalvarMensagm(long seqMensagem = 11)
        {
            List<MensagemPessoaAtuacao> dataMensagemPessoaAtuacao = CriarMensagemPessoaAtuacao(seqMensagem);
            List<TipoMensagem> dataTipoMensagem = CriarTipoMensagem();
            var listaEvento = CriarEventosAula(new[] { new DateTime(2021, 5, 3), new DateTime(2021, 5, 5), new DateTime(2021, 5, 7) }).TransformList<EventoAulaVO>();

            _Container.CreateRepository(dataMensagemPessoaAtuacao);
            _Container.CreateRepository(dataTipoMensagem);
            _Container.CreateRepository(new List<Mensagem>());
            _Container.CreateRepository(new List<ApuracaoFrequenciaGrade>());

            var mockEventoAula = _Container.CreateMock<EventoAulaDomainService>();
            mockEventoAula.Setup(s => s.BuscarEventoAulaAlunoNoPeriodo(It.IsAny<long>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                          .Returns(listaEvento);

            _Container.UnitOfWorkMock.Setup(v => v.Commit()).Verifiable("Não fez commit!");
            _Container.UnitOfWorkMock.Setup(v => v.Rollback()).Throws(new Exception("Não deveria fazer rollback!"));
        }

        [Fact]
        public void SalvarMensagem_QuandoCoincideHorario_RetornaException()
        {
            InicializarRepositorioSalvarMensagm();

            MensagemVO dataMensagemVO = CriarMensagemVO(0, 111, new DateTime(2021, 4, 26), new DateTime(2021, 4, 28));

            Assert.Throws<MensagemConcidentesException>(() => _MensagemPessoaAtuacaoDomainService.SalvarMensagem(dataMensagemVO));
        }

        [Fact]
        public void SalvarMensagem_QuandoCriaAbono_RetornaApuracoesComFalatasAbonadas()
        {
            InicializarRepositorioSalvarMensagm();
            var frequencias = CriarFrequencias(frequencia: Frequencia.Ausente);

            var dataMensagemVO = CriarMensagemVO(0, 111, new DateTime(2021, 5, 3), new DateTime(2021, 5, 7));
            var entidade = _MensagemPessoaAtuacaoDomainService.SalvarMensagem(dataMensagemVO);

            Assert.Equal(2, entidade);
            Assert.All(frequencias, frequencia =>
            {
                Assert.NotNull(frequencia.SeqMensagem);
                Assert.Equal(OcorrenciaFrequencia.AbonoRetificacao, frequencia.OcorrenciaFrequencia);
            });
            _Container.UnitOfWorkMock.Verify();
        }
    }
}
