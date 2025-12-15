using Moq;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.UnitTests.Ioc;
using SMC.Framework.Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SMC.Academico.UnitTests.Areas.GRD.DomainServices
{
    public class EscalaApuracaoDomainServiceTest
    {
        private EscalaApuracaoDomainService _EscalaApuracaoDomainService;
        private TestContainerAcademico _Container;

        public EscalaApuracaoDomainServiceTest()
        {
            _Container = new TestContainerAcademico();
            _EscalaApuracaoDomainService = new EscalaApuracaoDomainService();
        }

        [Fact]
        public void BuscarEscalaApuracao_QuandoInformadoUmSeq_RetornaOItemDeMesmoSeq()
        {
            var data = new List<EscalaApuracao>()
            {
                new EscalaApuracao() { Seq = 2, Descricao = "Item errado" },
                new EscalaApuracao() { Seq = 1, Descricao = "TesteUnitario" }
            };
            _Container.CreateRepository(data);

            var entidade = _EscalaApuracaoDomainService.BuscarEscalaApuracao(1);

            Assert.Equal("TesteUnitario", entidade.Descricao);
        }

        [Fact]
        public void BuscarEscalaApuracao_QuandoInformadoUmSeq_RetornaOItemDeMesmoSeqComIncludes()
        {
            var data = new List<EscalaApuracao>()
            {
                new EscalaApuracao()
                {
                    Seq = 9,
                    Descricao = "TesteUnitario",
                    CriteriosAprovacao = new List<CriterioAprovacao>()
                    {
                        new CriterioAprovacao() { Seq = 11 }
                    },
                    Itens = new List<EscalaApuracaoItem>()
                    {
                        new EscalaApuracaoItem() {Seq = 222},
                        new EscalaApuracaoItem() {Seq = 333}
                    }
                }
            };
            _Container.CreateRepository(data);

            var entidade = _EscalaApuracaoDomainService.BuscarEscalaApuracao(9);

            Assert.Equal("TesteUnitario", entidade.Descricao);
            Assert.Equal(11, entidade.CriteriosAprovacao.First().Seq);
            Assert.Equal(2, entidade.Itens.Count);
            Assert.Equal(333, entidade.Itens.Last().Seq);
        }

        [Fact]
        public void BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect_ConsultaPorSeq_RetornaAsEscalasDaOfertaInformada()
        {
            var dataCurriculo = new List<CurriculoCursoOferta>()
            {
                Mock.Of<CurriculoCursoOferta>(o =>
                    o.Seq == 1 &&
                    o.CursoOferta.Curso.SeqNivelEnsino == 11)
            };
            var dataInstituicao = new List<InstituicaoNivelEscalaApuracao>()
            {
                new InstituicaoNivelEscalaApuracao()
                {
                    InstituicaoNivel = new InstituicaoNivel() { SeqNivelEnsino = 11 },
                    SeqEscalaApuracao = 111
                }
            };
            var data = new List<EscalaApuracao>()
            {
                new EscalaApuracao() 
                {
                    Seq = 111,
                    Descricao = "TesteUnitario",
                    TipoEscalaApuracao = TipoEscalaApuracao.AprovadoReprovado
                }
            };
            _Container.CreateRepository(data);
            _Container.CreateRepository(dataCurriculo);
            _Container.CreateRepository(dataInstituicao);

            var entidade = _EscalaApuracaoDomainService.BuscarEscalasApuracaoNaoConceitoNivelEnsinoSelect(1);

            Assert.NotEmpty(entidade);
            Assert.Equal(111, entidade.First().Seq);
            Assert.Equal("TesteUnitario", entidade.First().Descricao);
        }

        [Fact]
        public void SalvarEscalaApuracao_EscalaValida_NovoSequencial()
        {
            _Container.CreateRepository(new List<EscalaApuracao>());
            _Container.CreateRepository(new List<CriterioAprovacao>());

            var item = new EscalaApuracao()
            {
                ApuracaoFinal = true,
                Itens = new List<EscalaApuracaoItem>()
                {
                    new EscalaApuracaoItem() { Seq = 11, Aprovado = true },
                    new EscalaApuracaoItem() { Seq = 12, Aprovado = false }
                }
            };
            var seqEntidade = _EscalaApuracaoDomainService.SalvarEscalaApuracao(item);

            Assert.Equal(1, seqEntidade);
        }

        [Fact]
        public void SalvarEscalaApuracao_EscalaSemItens_FalhaNoValidador()
        {
            _Container.CreateRepository(new List<EscalaApuracao>());
            _Container.CreateRepository(new List<CriterioAprovacao>());

            var item = new EscalaApuracao()
            {
                ApuracaoFinal = true,
                Itens = new List<EscalaApuracaoItem>()
            };
            var ex = Assert.Throws<SMCInvalidEntityException>(() => _EscalaApuracaoDomainService.SalvarEscalaApuracao(item));
            Assert.Contains("dois", ex.Message);
        }
    }
}
