using Moq;
using Moq.Language.Flow;
using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Academico.UnitTests.Ioc;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Repository;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace SMC.Academico.UnitTests.Areas.GRD.DomainServices
{
    public class EventoAulaDomainServiceTest
    {
        private EventoAulaDomainService _Subject;
        private TestContainerAcademico _Container;

        #region [ Moq ]

        private Mock<ISMCRepository<EventoAula>> _MockEventoAulaRepository;
        private Mock<ISMCRepository<EventoAulaColaborador>> _MockEventoAulaColaboradorRepository;
        private Mock<ISMCUnitOfWork> _MockUnit;
        private Mock<InstituicaoNivelTipoEventoDomainService> _mockInstituicaoNivelTipoEventoDomainService;
        private Mock<DivisaoTurmaDomainService> _mockDivisaoTurmaDomainService;
        private Mock<GradeHorariaCompartilhadaDomainService> _mockGradeHorariaCompartilhadaDomainService;
        private Mock<IEventoService> _mockEventoService;
        private List<EventoAulaColaborador> _eventoAulaColaboradores;
        private List<Colaborador> _colabodores;
        private List<DivisaoTurma> _divisaoTurmas;
        private List<Turma> _turmas;
        private List<EventoAula> _eventoAulas;
        private List<EventoData> _eventosAgd;

        #endregion [ Moq ]

        public EventoAulaDomainServiceTest()
        {
            _Container = new TestContainerAcademico();
            _Subject = new EventoAulaDomainService();

            InicializarServices();
        }

        #region [ Data ]

        private void InicializarRepositorioDivisoesValidacaoHorarios()
        {
            var data = new List<EventoAula>()
            {
                new EventoAula { Seq = 11, SeqDivisaoTurma = 1, SeqHorarioAgd = 110, Data = new DateTime(2021, 05, 10) },
                new EventoAula { Seq = 12, SeqDivisaoTurma = 1, SeqHorarioAgd = 111, Data = new DateTime(2021, 05, 11) },
                new EventoAula { Seq = 21, SeqDivisaoTurma = 2, SeqHorarioAgd = 112, Data = new DateTime(2021, 05, 12) },
                new EventoAula { Seq = 22, SeqDivisaoTurma = 2, SeqHorarioAgd = 114, Data = new DateTime(2021, 05, 14) },
                new EventoAula { Seq = 31, SeqDivisaoTurma = 3, SeqHorarioAgd = 111, Data = new DateTime(2021, 05, 11) },
                new EventoAula { Seq = 32, SeqDivisaoTurma = 3, SeqHorarioAgd = 113, Data = new DateTime(2021, 05, 13) },
                new EventoAula { Seq = 41, SeqDivisaoTurma = 4, SeqHorarioAgd = 110, Data = new DateTime(2021, 05, 10) },
                new EventoAula { Seq = 42, SeqDivisaoTurma = 4, SeqHorarioAgd = 113, Data = new DateTime(2021, 05, 13) },
            };
            _Container.CreateRepository(data);
        }

        private void InicializarRepositorioEventoSimples()
        {
            InicializarRepositorioTurmaSimples();
            InicializarEventosAGDSimples();
            _eventoAulas = new List<EventoAula>()
            {
                Mock.Of<EventoAula>(m => m.Seq == 1
                                      && m.SeqDivisaoTurma == _divisaoTurmas[0].Seq
                                      && m.SeqEventoAgd == "42"
                                      && m.DivisaoTurma == _divisaoTurmas[0]
                                      && m.Data == new DateTime())
            };
            _Container.CreateRepository(_eventoAulas);
        }

        private void InicializarRepositorioTurmaSimples()
        {
            _turmas = new List<Turma>()
            {
                Mock.Of<Turma>(m => m.Seq == 1
                                 && m.SeqAgendaTurma == 12345
                                 && m.Codigo == 13192
                                 && m.Numero == 1)
            };
            _Container.CreateRepository(_turmas);

            _divisaoTurmas = new List<DivisaoTurma>()
            {
                Mock.Of<DivisaoTurma>(m => m.Seq == 11
                                        && m.DivisaoComponente.Numero == 1
                                        && m.NumeroGrupo == 0
                                        && m.Turma == _turmas[0]
                                        && m.SeqTurma == _turmas[0].Seq)
            };
            _Container.CreateRepository(_divisaoTurmas);
            _turmas[0].DivisoesTurma = _divisaoTurmas.ToList();
        }

        private void InicializarRepositorioColaboradores()
        {
            _colabodores = new List<Colaborador>()
            {
                Mock.Of<Colaborador>(m => m.Seq == 27
                                       && m.DadosPessoais.Nome == "Alberto"),
                Mock.Of<Colaborador>(m => m.Seq == 162
                                       && m.DadosPessoais.Nome == "Bernardo"),
            };
            _Container.CreateRepository(_colabodores);
            InicializarRepositorioEventoAulaColaborador();
        }

        private void InicializarRepositorioEventoAulaColaborador()
        {
            _eventoAulaColaboradores = new List<EventoAulaColaborador>()
            {
                Mock.Of<EventoAulaColaborador>(me => me.SeqColaborador == _colabodores[0].Seq
                                                  && me.Colaborador == _colabodores[0]),
                Mock.Of<EventoAulaColaborador>(me => me.SeqColaborador == _colabodores[1].Seq
                                                  && me.Colaborador == _colabodores[1])
            };
            _Container.CreateRepository(_eventoAulaColaboradores);
        }

        private void InicializarRepositorioTurmaCompartilhada(int? codLocalSEFEvento3 = null, string localEvento3 = null, bool compartilhado = false)
        {
            InicializarRepositorioColaboradores();
            InicializarRepositorioTurmas();
            InicializarEventosAGDTurmaCompartilhada();

            int? codLocalSEFEvento1 = null;
            string localEvento1 = null;
            if (compartilhado)
            {
                codLocalSEFEvento1 = codLocalSEFEvento3;
                localEvento1 = localEvento3;
            }
            _eventoAulaColaboradores[0].SeqEventoAula = 1;
            _eventoAulaColaboradores[1].SeqEventoAula = 2;
            _eventoAulas = new List<EventoAula>()
            {
                Mock.Of<EventoAula>(m => m.Seq == 1
                                      && m.SeqDivisaoTurma == _divisaoTurmas[0].Seq
                                      && m.SeqEventoAgd == "12"
                                      && m.DivisaoTurma == _divisaoTurmas[0]
                                      && m.CodigoLocalSEF == codLocalSEFEvento1
                                      && m.Local == localEvento1
                                      && m.Colaboradores == _eventoAulaColaboradores.Where(w => w.SeqEventoAula == 1).ToList()),
                Mock.Of<EventoAula>(m => m.Seq == 2
                                      && m.SeqEventoAgd == "13"
                                      && m.SeqDivisaoTurma == _divisaoTurmas[1].Seq
                                      && m.DivisaoTurma == _divisaoTurmas[1]
                                      && m.CodigoLocalSEF == 101
                                      && m.Local == "Local de conflito"
                                      && m.Colaboradores == _eventoAulaColaboradores.Where(w => w.SeqEventoAula == 2).ToList()),
                Mock.Of<EventoAula>(m => m.Seq == 3
                                      && m.SeqEventoAgd == "14"
                                      && m.SeqDivisaoTurma == _divisaoTurmas[2].Seq
                                      && m.DivisaoTurma == _divisaoTurmas[2]
                                      && m.CodigoLocalSEF == codLocalSEFEvento3
                                      && m.Local == localEvento3),
            };
            _eventoAulaColaboradores[0].EventoAula = _eventoAulas[0];
            _eventoAulaColaboradores[1].EventoAula = _eventoAulas[1];

            _Container.CreateRepository(_eventoAulas);
        }

        private void ConfigurarCompartilhamentoGradeHoraria()
        {
            _mockGradeHorariaCompartilhadaDomainService
                .Setup(s => s.BuscarDivisoesGradeHorariaCompartilhada(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<long>() { _divisaoTurmas[0].Seq, _divisaoTurmas[2].Seq });
        }

        private void InicializarRepositorioTurmas()
        {
            InicializarRepositorioDivisoesTurma();
            _turmas = new List<Turma>()
            {
                Mock.Of<Turma>(m => m.Seq == 20730
                                 && m.SeqAgendaTurma == 11
                                 && m.Codigo == 13192
                                 && m.Numero == 1
                                 && m.DivisoesTurma == _divisaoTurmas.Where(w => w.SeqTurma == 20730).ToList()),
                Mock.Of<Turma>(m => m.Seq == 20732
                                 && m.SeqAgendaTurma == 12
                                 && m.Codigo == 13194
                                 && m.Numero == 1
                                 && m.DivisoesTurma == _divisaoTurmas.Where(w => w.SeqTurma == 20732).ToList()),
                Mock.Of<Turma>(m => m.Seq == 20733
                                 && m.SeqAgendaTurma == 13
                                 && m.Codigo == 13195
                                 && m.Numero == 1
                                 && m.DivisoesTurma == _divisaoTurmas.Where(w => w.SeqTurma == 20733).ToList()),
            };
            _divisaoTurmas[0].Turma = _turmas[0];
            _divisaoTurmas[1].Turma = _turmas[1];
            _divisaoTurmas[2].Turma = _turmas[2];
            _Container.CreateRepository(_turmas);
        }

        private void InicializarRepositorioDivisoesTurma()
        {
            _divisaoTurmas = new List<DivisaoTurma>()
            {
                Mock.Of<DivisaoTurma>(m => m.Seq == 20842
                                        && m.SeqTurma == 20730
                                        && m.DivisaoComponente.Numero == 1
                                        && m.NumeroGrupo == 0),
                Mock.Of<DivisaoTurma>(m => m.Seq == 20844
                                        && m.SeqTurma == 20732
                                        && m.DivisaoComponente.Numero == 1
                                        && m.NumeroGrupo == 0),
                Mock.Of<DivisaoTurma>(m => m.Seq == 20845
                                        && m.SeqTurma == 20733
                                        && m.DivisaoComponente.Numero == 1
                                        && m.NumeroGrupo == 0),
            };
            _Container.CreateRepository(_divisaoTurmas);
        }

        private void InicializarEventosAGDTurmaCompartilhada(int? codigoSef12 = null, int? codigoSef14 = 42)
        {
            _eventosAgd = new List<EventoData>()
            {
                Mock.Of<EventoData>(m => m.Seq == 12 && m.CodigoLocalSEF == codigoSef12),
                Mock.Of<EventoData>(m => m.Seq == 13 && m.CodigoLocalSEF == 101),
                Mock.Of<EventoData>(m => m.Seq == 14 && m.CodigoLocalSEF == codigoSef14),
            };
        }

        private void InicializarEventosAGDSimples()
        {
            _eventosAgd = new List<EventoData>()
            {
                Mock.Of<EventoData>(m => m.Seq == 42 && m.CodigoLocalSEF == null)
            };
        }

        #endregion [ Data ]

        #region [ DomainService ]

        private void InicializarServices()
        {
            _mockInstituicaoNivelTipoEventoDomainService = new Mock<InstituicaoNivelTipoEventoDomainService>();
            _mockInstituicaoNivelTipoEventoDomainService
                .Setup(s => s.BuscarSeqTipoEventoAgdPorTokenDivisao(It.IsAny<string>(), It.IsAny<long>()))
                .Returns(42);
            _Container.Manager.Container.RegisterInstance(_mockInstituicaoNivelTipoEventoDomainService.Object);

            _mockDivisaoTurmaDomainService = new Mock<DivisaoTurmaDomainService>();
            _mockDivisaoTurmaDomainService
                .Setup(s => s.MontarDescricaoDivisaoTurmaRegraGRD020(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns("Mock descricao");
            _mockDivisaoTurmaDomainService
                .Setup(s => s.BuscarAlunosDivisaoTurma(It.IsAny<long>()))
                .Returns(new List<DivisaoTurmaRelatorioAlunoVO>());
            _Container.Manager.Container.RegisterInstance(_mockDivisaoTurmaDomainService.Object);

            _mockGradeHorariaCompartilhadaDomainService = new Mock<GradeHorariaCompartilhadaDomainService>();
            _mockGradeHorariaCompartilhadaDomainService
                .Setup(s => s.BuscarDivisoesGradeHorariaCompartilhada(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<long>());
            _Container.Manager.Container.RegisterInstance(_mockGradeHorariaCompartilhadaDomainService.Object);

            _mockEventoService = new Mock<IEventoService>();
            _mockEventoService
                .Setup(s => s.BuscarEvento(It.IsAny<long>()))
                .Returns<long>(seqEvento => _eventosAgd.First(f => f.Seq == seqEvento));
            _mockEventoService
                .Setup(s => s.SalvarEvento(It.IsAny<EventoData>()))
                .Callback<EventoData>(evento =>
                {
                    _eventosAgd = _eventosAgd.Where(w => w.Seq != evento.Seq).ToList();
                    _eventosAgd.Add(evento);
                })
                .Returns(new RetornoOperacaoEventoData());
            _Container.Manager.Container.RegisterInstance(_mockEventoService.Object);

            _MockUnit = _Container.CreateMock<ISMCUnitOfWork>();
        }

        //TODO: Usar o repository de data
        private void InicializarRepositoryEventoAula()
        {
            _MockEventoAulaRepository = _Container.CreateMock<ISMCRepository<EventoAula>>();
        }

        //TODO: Transformar o SearchBySpecification para virtual no fw
        private void InicializarRepositoryEventoAulaColaborador()
        {
            _MockEventoAulaColaboradorRepository = _Container.CreateMock<ISMCRepository<EventoAulaColaborador>>();
        }

        #endregion [ DomainService ]

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorAUmEvento()
        {
            EventoAula updated = new EventoAula();
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                    }
                });
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(new List<EventoAulaColaborador>());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>() { new EventoAulaColaborador() {
                SeqColaborador = 111
            } };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Seq);
            Assert.Equal(1, updated.Colaboradores.Count);
            Assert.Equal(111, updated.Colaboradores.First().SeqColaborador);
        }

        [Fact]
        public void EditarColaboradoresEventos_RemoverUmProfessorDeUmEvento()
        {
            InicializarRepositoryEventoAula();
            InicializarRepositoryEventoAulaColaborador();
            EventoAula updated = new EventoAula();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    }
                });
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEvento);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>();
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Seq);
            Assert.Equal(0, updated.Colaboradores.Count);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AlterarUmProfessorDeUmEvento()
        {
            EventoAula updated = new EventoAula();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    }
                });
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEvento);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Seq);
            Assert.Equal(1, updated.Colaboradores.Count);
            Assert.Equal(112, updated.Colaboradores.First().SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorNEventos()
        {
            var updated = new List<EventoAula>();
            var colaboradoresEvento = new List<EventoAulaColaborador>();
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = colaboradoresEvento
                    },
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(eventos);
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            var seqEventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 2, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.Equal(3, updated.Count);
            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
                Assert.Equal(111, item.Colaboradores.First().SeqColaborador);
                Assert.Null(item.Colaboradores.First().SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AlterarUmProfessorNEventos()
        {
            var updated = new List<EventoAula>();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = colaboradoresEvento
                    }
                };
            var seqEventos = new List<long>() { 1, 2 };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs == seqEventos), It.IsAny<Enum>()))
                .Returns(eventos);
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 2, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(2), "Quatidade inválida de atualizações");
            Assert.Equal(2, updated.Count);
            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
                Assert.Equal(112, item.Colaboradores.First().SeqColaborador);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorEventoQueJaTenhamProfessoresEspecificos()
        {
            var updated = new List<EventoAula>();
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 1,
                                SeqColaborador = 111
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 112
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 111
                            }
                        }
                    },
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(eventos.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 113
                }
            };
            var seqEventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.Equal(3, updated.Count);

            Assert.Equal(2, updated[0].Colaboradores.Count);
            Assert.Equal(3, updated[1].Colaboradores.Count);
            Assert.Equal(2, updated[2].Colaboradores.Count);

            Assert.Equal(111, updated[0].Colaboradores[0].SeqColaborador);
            Assert.Equal(113, updated[0].Colaboradores[1].SeqColaborador);
            Assert.Equal(111, updated[1].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[1].Colaboradores[1].SeqColaborador);
            Assert.Equal(113, updated[1].Colaboradores[2].SeqColaborador);
            Assert.Equal(111, updated[2].Colaboradores[0].SeqColaborador);
            Assert.Equal(113, updated[2].Colaboradores[1].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverUmProfessorNEventosJaTenhamProfessoresEspecificos()
        {
            var updated = new List<EventoAula>();
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 1,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 1,
                                SeqColaborador = 112
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 113
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 112
                            },
                           new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 113
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 4,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 4,
                                SeqColaborador = 112
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 4,
                                SeqColaborador = 113
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 5,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 4,
                                SeqColaborador = 112
                            }
                        }
                    }
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(3)), It.IsAny<Enum>()))
                .Returns(eventos.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 113
                }
            };
            var seqEventos = new List<long>() { 1, 2, 3, 4, 5 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 3, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(5), "Quatidade inválida de atualizações");
            Assert.Equal(5, updated.Count);

            Assert.Equal(1, updated[0].Colaboradores.Count);
            Assert.Equal(1, updated[1].Colaboradores.Count);
            Assert.Equal(2, updated[2].Colaboradores.Count);
            Assert.Equal(2, updated[3].Colaboradores.Count);
            Assert.Equal(1, updated[4].Colaboradores.Count);

            Assert.Equal(112, updated[0].Colaboradores[0].SeqColaborador);

            Assert.Equal(113, updated[1].Colaboradores[0].SeqColaborador);

            Assert.Equal(112, updated[2].Colaboradores[0].SeqColaborador);
            Assert.Equal(113, updated[2].Colaboradores[1].SeqColaborador);

            Assert.Equal(112, updated[3].Colaboradores[0].SeqColaborador);
            Assert.Equal(113, updated[3].Colaboradores[1].SeqColaborador);

            Assert.Equal(112, updated[4].Colaboradores[0].SeqColaborador);

        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorNEventoQueFiquemMesmoNumeroProfessores()
        {
            var updated = new List<EventoAula>();
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 1,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 1,
                                SeqColaborador = 112
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 112
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 2,
                                SeqColaborador = 113
                            }
                        }
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = new List<EventoAulaColaborador>()
                        {
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 111
                            },
                            new EventoAulaColaborador()
                            {
                                SeqEventoAula = 3,
                                SeqColaborador = 112
                            }
                        }
                    },
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(eventos.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 113
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            var seqEventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.Equal(3, updated.Count);

            Assert.Equal(3, updated[0].Colaboradores.Count);
            Assert.Equal(3, updated[1].Colaboradores.Count);
            Assert.Equal(3, updated[2].Colaboradores.Count);

            Assert.Equal(111, updated[0].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[0].Colaboradores[1].SeqColaborador);
            Assert.Equal(113, updated[0].Colaboradores[2].SeqColaborador);
            Assert.Equal(111, updated[1].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[1].Colaboradores[1].SeqColaborador);
            Assert.Equal(113, updated[1].Colaboradores[2].SeqColaborador);
            Assert.Equal(111, updated[2].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[2].Colaboradores[1].SeqColaborador);
            Assert.Equal(113, updated[2].Colaboradores[2].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorParaNEventoQueJáTenhaUmProfessor()
        {
            var updated = new List<EventoAula>();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = colaboradoresEvento
                    },
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(eventos.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            var seqEventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.Equal(3, updated.Count);

            Assert.Equal(2, updated[0].Colaboradores.Count);
            Assert.Equal(2, updated[1].Colaboradores.Count);
            Assert.Equal(2, updated[2].Colaboradores.Count);

            Assert.Equal(111, updated[0].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[0].Colaboradores[1].SeqColaborador);
            Assert.Equal(111, updated[1].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[1].Colaboradores[1].SeqColaborador);
            Assert.Equal(111, updated[2].Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated[2].Colaboradores[1].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverUmProfessorParaNEventoQueJáTenhaUmProfessor()
        {
            var updated = new List<EventoAula>();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            List<EventoAula> eventos = new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 2,
                        Colaboradores = colaboradoresEvento
                    },
                    new EventoAula() {
                        Seq = 3,
                        Colaboradores = colaboradoresEvento
                    },
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(eventos.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventos.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
            };
            var seqEventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, seqEventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.Equal(3, updated.Count);

            Assert.Equal(1, updated[0].Colaboradores.Count);
            Assert.Equal(1, updated[1].Colaboradores.Count);
            Assert.Equal(1, updated[2].Colaboradores.Count);

            Assert.Equal(111, updated[0].Colaboradores[0].SeqColaborador);
            Assert.Equal(111, updated[1].Colaboradores[0].SeqColaborador);
            Assert.Equal(111, updated[2].Colaboradores[0].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorEventoQueJaTenhaUmProfessor()
        {
            EventoAula updated = new EventoAula();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    }
                });
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEvento);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(2, updated.Colaboradores.Count);

            Assert.Equal(111, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(112, updated.Colaboradores[1].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverProfessorEventoQueJaTenhaMaisDeUmProfessor()
        {
            EventoAula updated = new EventoAula();
            var colaboradoresEvento = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = 112
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaFilterSpecification>(i => i.Seqs.Contains(1)), It.IsAny<Enum>()))
                .Returns(new List<EventoAula>(){
                    new EventoAula() {
                        Seq = 1,
                        Colaboradores = colaboradoresEvento
                    }
                });
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEvento);
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = 111
                },
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Colaboradores.Count);

            Assert.Equal(111, updated.Colaboradores[0].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador() { SeqColaborador = seqProfessor }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>(){
                    new EventoAula()
                    {
                        Seq = 1,
                        Colaboradores = colaboradoresEventoBanco
                    }
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Colaboradores.Count);

            Assert.Equal(seqProfessor, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(seqSubstituto, updated.Colaboradores[0].SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_EditarUmProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            const long seqNovoSubstituto = 113;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqNovoSubstituto
                },
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Colaboradores.Count);

            Assert.Equal(seqProfessor, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(seqNovoSubstituto, updated.Colaboradores[0].SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverUmProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                }
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                },
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Colaboradores.Count);

            Assert.Equal(seqProfessor, updated.Colaboradores[0].SeqColaborador);
            Assert.Null(updated.Colaboradores[0].SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarProfessorPrincipalEventoComProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessorPrincipal = 111;
            const long seqProfessor = 112;
            const long seqSubstituto = 113;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>(){
                    new EventoAula()
                    {
                        Seq = 1,
                        Colaboradores = colaboradoresEventoBanco
                    }
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessorPrincipal
                }
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(2, updated.Colaboradores.Count);

            Assert.Equal(seqProfessor, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(seqSubstituto, updated.Colaboradores[0].SeqColaboradorSubstituto);
            Assert.Equal(seqProfessorPrincipal, updated.Colaboradores[1].SeqColaborador);
            Assert.Null(updated.Colaboradores[1].SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_EditarProfessorPrincipalEventoComProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessorPrincipalAtual = 111;
            const long seqProfessorPrincipalNovo = 112;
            const long seqSubstituto = 113;
            const long seqSubstituido = 114;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqSubstituido,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessorPrincipalAtual
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>(){
                    new EventoAula()
                    {
                        Seq = 1,
                        Colaboradores = colaboradoresEventoBanco
                    }
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessorPrincipalNovo
                }
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(2, updated.Colaboradores.Count);

            Assert.Equal(seqSubstituido, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(seqSubstituto, updated.Colaboradores[0].SeqColaboradorSubstituto);
            Assert.Equal(seqProfessorPrincipalNovo, updated.Colaboradores[1].SeqColaborador);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverProfessorPrincipalEventoComProfessorSubstituto()
        {
            EventoAula updated = new EventoAula();
            const long seqProfessorPrincipal = 111;
            const long seqProfessor = 112;
            const long seqSubstituto = 113;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessorPrincipal
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>(){
                    new EventoAula()
                    {
                        Seq = 1,
                        Colaboradores = colaboradoresEventoBanco
                    }
                };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.Is<EventoAulaColaboradorFilterSpecification>(i => i.SeqEventoAula == 1), It.IsAny<Enum>()))
                .Returns(colaboradoresEventoBanco.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated = entity);
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            var eventos = new List<long>() { 1 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, true);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Once, "Quatidade inválida de atualizações");
            Assert.Equal(1, updated.Colaboradores.Count);

            Assert.Equal(seqProfessor, updated.Colaboradores[0].SeqColaborador);
            Assert.Equal(seqSubstituto, updated.Colaboradores[0].SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstitutoEmNEventos()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
                Assert.Equal(seqProfessor, item.Colaboradores[0].SeqColaborador);
                Assert.Equal(seqSubstituto, item.Colaboradores[0].SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstitutoEmNEventosComUmEventoInalterado()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqProfessor,
                            SeqColaboradorSubstituto = seqSubstituto
                        }
                    }
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
                Assert.Equal(seqProfessor, item.Colaboradores[0].SeqColaborador);
                Assert.Equal(seqSubstituto, item.Colaboradores[0].SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstitutoEmNEventosComUmEventoDiferente()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            const long seqOutroSubstituto = 113;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqProfessor,
                            SeqColaboradorSubstituto = seqOutroSubstituto
                        }
                    }
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
                Assert.Equal(seqProfessor, item.Colaboradores[0].SeqColaborador);
                Assert.Equal(seqSubstituto, item.Colaboradores[0].SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstitutoEmNEventosComDoisProfessores()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            const long seqAuxiliar = 113;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(2, item.Colaboradores.Count);
                Assert.Equal(seqProfessor, item.Colaboradores[0].SeqColaborador);
                Assert.Equal(seqSubstituto, item.Colaboradores[0].SeqColaboradorSubstituto);
                Assert.Equal(seqAuxiliar, item.Colaboradores[1].SeqColaborador);
                Assert.Null(item.Colaboradores[1].SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_RemoverUmProfessorSubstitutoEmNEventosComDoisProfessores()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            const long seqAuxiliar = 113;
            const long seqAuxiliarSubstituto = 114;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar,
                    SeqColaboradorSubstituto = seqAuxiliarSubstituto
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(2, item.Colaboradores.Count);
                Assert.Equal(seqProfessor, item.Colaboradores[0].SeqColaborador);
                Assert.Equal(seqSubstituto, item.Colaboradores[0].SeqColaboradorSubstituto);
                Assert.Equal(seqAuxiliar, item.Colaboradores[1].SeqColaborador);
                Assert.Null(item.Colaboradores[1].SeqColaboradorSubstituto);
            });
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AdicionarUmProfessorSubstitutoEmNEventosComDoisProfessoresEUmSubstitutoEmUmEvento()
        {
            var updated = new List<EventoAula>();
            const long seqProfessor = 111;
            const long seqSubstituto = 112;
            const long seqOutroSubstituto = 113;
            const long seqAuxiliar = 114;
            const long seqAuxiliarSubstituto = 115;
            var colaboradoresEventoBanco = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar,
                    SeqColaboradorSubstituto = seqAuxiliarSubstituto
                }
            };
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = colaboradoresEventoBanco
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqProfessor,
                            SeqColaboradorSubstituto = seqOutroSubstituto
                        },
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqAuxiliar
                        }
                    }
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = colaboradoresEventoBanco
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqProfessor,
                    SeqColaboradorSubstituto = seqSubstituto
                },
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqAuxiliar,
                    SeqColaboradorSubstituto = seqAuxiliarSubstituto
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");
            Assert.All(updated, item =>
            {
                Assert.Equal(2, item.Colaboradores.Count);
                var colaborador = item.Colaboradores.Single(s => s.SeqColaborador == seqProfessor);
                var auxiliar = item.Colaboradores.Single(s => s.SeqColaborador == seqAuxiliar);
                Assert.Equal(seqSubstituto, colaborador.SeqColaboradorSubstituto);
            });
            Assert.Equal(seqAuxiliarSubstituto, updated[0].Colaboradores.Single(s => s.SeqColaborador == seqAuxiliar).SeqColaboradorSubstituto);
            Assert.Null(updated[1].Colaboradores.Single(s => s.SeqColaborador == seqAuxiliar).SeqColaboradorSubstituto);
            Assert.Equal(seqAuxiliarSubstituto, updated[2].Colaboradores.Single(s => s.SeqColaborador == seqAuxiliar).SeqColaboradorSubstituto);
        }

        [Fact(Skip = "Validações professor")]
        public void EditarColaboradoresEventos_AtualizarUmProfessorSubstitutoEmNEventosComDoisProfessoresEDiferentesSubstitutos()
        {
            var updated = new List<EventoAula>();
            const long seqHugo = 111;
            const long seqArabe = 112;
            const long seqJane = 113;
            const long seqDickman = 114;
            List<EventoAula> eventosAulaBanco = new List<EventoAula>()
            {
                new EventoAula()
                {
                    Seq = 1,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqHugo
                        }
                    }
                },
                new EventoAula()
                {
                    Seq = 2,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqHugo,
                            SeqColaboradorSubstituto = seqArabe
                        }
                    }
                },
                new EventoAula()
                {
                    Seq = 3,
                    Colaboradores = new List<EventoAulaColaborador>()
                    {
                        new EventoAulaColaborador()
                        {
                            SeqColaborador = seqJane
                        }
                    }
                },
            };
            _MockEventoAulaRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaFilterSpecification>(), It.IsAny<Enum>()))
                .Returns(eventosAulaBanco.SMCClone());
            _MockEventoAulaColaboradorRepository.Setup(m => m.SearchBySpecification(It.IsAny<EventoAulaColaboradorFilterSpecification>(), It.IsAny<Enum>()))
                .Returns<EventoAulaColaboradorFilterSpecification, Enum>((spec, includes) => eventosAulaBanco.First(f => f.Seq == spec.SeqEventoAula).Colaboradores.SMCClone());
            _MockEventoAulaRepository.Setup(m => m.Update(It.IsAny<EventoAula>(), It.IsAny<string[]>()))
                .Callback<EventoAula, string[]>((entity, ignore) => updated.Add(entity));
            var novoColaborador = new List<EventoAulaColaborador>()
            {
                new EventoAulaColaborador()
                {
                    SeqColaborador = seqHugo,
                    SeqColaboradorSubstituto = seqDickman
                }
            };
            var eventos = new List<long>() { 1, 2, 3 };

            _Subject.EditarColaboradoresEventos(novoColaborador, eventos, 1, false);

            _MockEventoAulaRepository.Verify(m => m.Update(It.IsAny<EventoAula>()), Times.Exactly(3), "Quatidade inválida de atualizações");

            Assert.All(updated, item =>
            {
                Assert.Equal(1, item.Colaboradores.Count);
            });
            Assert.Equal(seqHugo, updated[0].Colaboradores[0].SeqColaborador);
            Assert.Equal(seqDickman, updated[0].Colaboradores[0].SeqColaboradorSubstituto);
            Assert.Equal(seqHugo, updated[1].Colaboradores[0].SeqColaborador);
            Assert.Equal(seqDickman, updated[1].Colaboradores[0].SeqColaboradorSubstituto);
            Assert.Equal(seqJane, updated[2].Colaboradores[0].SeqColaborador);
            Assert.Null(updated[2].Colaboradores[0].SeqColaboradorSubstituto);
        }

        [Fact]
        public void ValidarColisaoHorariosDivisoes_DivisoesSemColisao_ListaVazia()
        {
            InicializarRepositorioDivisoesValidacaoHorarios();

            var colisoes = _Subject.ValidarColisaoHorariosDivisoes(new List<long>() { 1, 2 });

            Assert.Empty(colisoes);
        }

        [Fact]
        public void ValidarColisaoHorariosDivisoes_DivisoesComColisao_ListaDasDivisoesQueColidiram()
        {
            InicializarRepositorioDivisoesValidacaoHorarios();

            var colisoes = _Subject.ValidarColisaoHorariosDivisoes(new List<long>() { 1, 2, 3, 4 });

            Assert.Equal(3, colisoes.Count);
            Assert.Contains(1, colisoes);
            Assert.DoesNotContain(2, colisoes);
            Assert.Contains(3, colisoes);
            Assert.Contains(4, colisoes);
        }

        #region [ Alteração local ]

        [Fact]
        public void EditarLocalEvento_InclusaoDeLocalEmEventoSemLocalSemProfessorSemCompartilhamento_InclusaoComSucessoDoLocal()
        {
            InicializarRepositorioEventoSimples();
            var novoCodSEF = 2;
            var novoLocal = "Novo local";
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            _Container.ConextMock
                .Verify(v =>
                        v.UpdateFields(It.Is<EventoAula>(i => i.Seq == _eventoAulas[0].Seq), It.IsAny<Expression<Func<EventoAula, object>>[]>()));
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_InclusaoDeLocalEmEventoSemLocalSemProfessorComCompartilhamento_InclusaoComSucessoDoLocal()
        {
            InicializarRepositorioTurmaCompartilhada();
            _mockGradeHorariaCompartilhadaDomainService
                .Setup(s => s.BuscarDivisoesGradeHorariaCompartilhada(It.IsAny<long>(), It.IsAny<bool>()))
                .Returns(new List<long>() { _divisaoTurmas[0].Seq, _divisaoTurmas[2].Seq });
            var novoCodSEF = 42;
            var novoLocal = "Novo local compartilhado";
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            _Container.ConextMock
                .Verify(v =>
                        v.UpdateFields(It.Is<EventoAula>(i => i.Seq == _eventoAulas[0].Seq), It.IsAny<Expression<Func<EventoAula, object>>[]>()));
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_InclusaoDeMesmoLocalEmEventoSemLocalSemProfessorComCompartilhamento_InclusaoComSucessoDoLocalSemCodigoSEFNoAGD()
        {
            const int codLocalSEF = 42;
            const string local = "Mesmo local do 3";
            InicializarRepositorioTurmaCompartilhada(codLocalSEF, local);
            ConfigurarCompartilhamentoGradeHoraria();
            var novoCodSEF = codLocalSEF;
            var novoLocal = local;
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            var eventoAgd = _eventosAgd.FirstOrDefault(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd));
            Assert.Null(eventoAgd.CodigoLocalSEF);
            Assert.Equal(local, eventoAgd.Local);

            _Container.ConextMock.Verify(v =>
                v.UpdateFields(It.Is<EventoAula>(i => i.Seq == _eventoAulas[0].Seq), It.IsAny<Expression<Func<EventoAula, object>>[]>()));
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_InclusaoLocalDiferenteEmEventoSemLocalSemProfessorComCompartilhamento_InclusaoComSucessoDoLocalComCodigoSEFNoAGD()
        {
            InicializarRepositorioTurmaCompartilhada(40, "local do 3");
            ConfigurarCompartilhamentoGradeHoraria();
            var novoCodSEF = 42;
            var novoLocal = "Novo local";
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            var eventoAgd = _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd));
            Assert.Equal(42, eventoAgd.CodigoLocalSEF);
            Assert.Equal("Novo local", eventoAgd.Local);
            _Container.ConextMock.Verify(v =>
                v.UpdateFields(It.Is<EventoAula>(i => i.Seq == _eventoAulas[0].Seq), It.IsAny<Expression<Func<EventoAula, object>>[]>()));
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_InclusaoLocalEmConflitoComEventoForaDoCompartilhamentoEmEventoSemLocalSemProfessorComCompartilhamento_AbortarPorConflito()
        {
            InicializarRepositorioTurmaCompartilhada(40, "local do 3");
            ConfigurarCompartilhamentoGradeHoraria();
            var novoCodSEF = 101;
            var novoLocal = "Local de conflito";
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            _mockEventoService
                .Setup(s => s.SalvarEvento(It.IsAny<EventoData>()))
                .Throws(new SMCApplicationException("Teste colisao"));

            Assert.Throws<SMCApplicationException>(() => _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos));

            _Container.ConextMock.Verify(v =>
                v.UpdateFields(It.Is<EventoAula>(i => i.Seq == _eventoAulas[0].Seq), It.IsAny<Expression<Func<EventoAula, object>>[]>()));
            _MockUnit.Verify(v => v.Commit(), Times.Never);
        }

        [Fact]
        public void EditarLocalEvento_AlteracaoLocalRaizCompartilhamento_MudarRaizParaOPrimeiroEventoEAtualizarLocalDoTerceiro()
        {
            InicializarRepositorioTurmaCompartilhada(42, "Local compartilhado", true);
            ConfigurarCompartilhamentoGradeHoraria();
            var novoCodSEF = 40;
            var novoLocal = "Novo local";
            var eventos = new List<long>() { _eventoAulas[2].Seq };

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventoAulas[0].CodigoLocalSEF);
            Assert.Equal("Local compartilhado", _eventoAulas[0].Local);

            Assert.Equal(40, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(40, _eventoAulas[2].CodigoLocalSEF);
            Assert.Equal("Novo local", _eventoAulas[2].Local);

            _Container.ConextMock
                .Verify(v => v.UpdateFields(It.IsAny<EventoAula>(), It.IsAny<Expression<Func<EventoAula, object>>[]>()), Times.Once);
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_RemoverLocalRaizCompartilhamento_MudarRaizParaOPrimeiroEventoELimparLocalDoTerceiro()
        {
            InicializarRepositorioTurmaCompartilhada(42, "Local compartilhado", true);
            ConfigurarCompartilhamentoGradeHoraria();
            var eventos = new List<long>() { _eventoAulas[2].Seq };

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);

            _Subject.EditarLocalEventos(null, "", eventos);

            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventoAulas[0].CodigoLocalSEF);
            Assert.Equal("Local compartilhado", _eventoAulas[0].Local);

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Null(_eventoAulas[2].CodigoLocalSEF);
            Assert.Equal("", _eventoAulas[2].Local);

            _Container.ConextMock
                .Verify(v => v.UpdateFields(It.IsAny<EventoAula>(), It.IsAny<Expression<Func<EventoAula, object>>[]>()), Times.Once);
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_AlteracaoLocalNaoRaizCompartilhamento_GravarNovoLocalSefNoAGD()
        {
            InicializarRepositorioTurmaCompartilhada(42, "Local compartilhado", true);
            ConfigurarCompartilhamentoGradeHoraria();
            var novoCodSEF = 40;
            var novoLocal = "Novo local";
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);

            _Subject.EditarLocalEventos(novoCodSEF, novoLocal, eventos);

            Assert.Equal(40, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(40, _eventoAulas[0].CodigoLocalSEF);
            Assert.Equal("Novo local", _eventoAulas[0].Local);

            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventoAulas[2].CodigoLocalSEF);
            Assert.Equal("Local compartilhado", _eventoAulas[2].Local);

            _Container.ConextMock
                .Verify(v => v.UpdateFields(It.IsAny<EventoAula>(), It.IsAny<Expression<Func<EventoAula, object>>[]>()), Times.Once);
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_RemoverLocalNaoRaizCompartilhamento_LimparApenasOLocalDoEvento()
        {
            InicializarRepositorioTurmaCompartilhada(42, "Local compartilhado", true);
            ConfigurarCompartilhamentoGradeHoraria();
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);

            _Subject.EditarLocalEventos(null, "", eventos);

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Null(_eventoAulas[0].CodigoLocalSEF);
            Assert.Empty(_eventoAulas[0].Local);

            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventoAulas[2].CodigoLocalSEF);
            Assert.Equal("Local compartilhado", _eventoAulas[2].Local);

            _Container.ConextMock
                .Verify(v => v.UpdateFields(It.IsAny<EventoAula>(), It.IsAny<Expression<Func<EventoAula, object>>[]>()), Times.Once);
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        [Fact]
        public void EditarLocalEvento_AlterarLocalNaoRaizComProfessor_AlterarLocal()
        {
            InicializarRepositorioTurmaCompartilhada(42, "Local compartilhado", true);
            ConfigurarCompartilhamentoGradeHoraria();
            var eventos = new List<long>() { _eventoAulas[0].Seq };

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);

            _Subject.EditarLocalEventos(null, "", eventos);

            Assert.Null(_eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[0].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Null(_eventoAulas[0].CodigoLocalSEF);
            Assert.Empty(_eventoAulas[0].Local);

            Assert.Equal(42, _eventosAgd.First(f => f.Seq == int.Parse(_eventoAulas[2].SeqEventoAgd)).CodigoLocalSEF);
            Assert.Equal(42, _eventoAulas[2].CodigoLocalSEF);
            Assert.Equal("Local compartilhado", _eventoAulas[2].Local);

            _Container.ConextMock
                .Verify(v => v.UpdateFields(It.IsAny<EventoAula>(), It.IsAny<Expression<Func<EventoAula, object>>[]>()), Times.Once);
            _MockUnit.Verify(v => v.Commit(), Times.Once);
        }

        #endregion [ Alteração local ]
    }
}
