using Moq;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.UnitTests.Ioc;
using SMC.Framework.Jobs.Service;
using SMC.Framework.Model.Jobs;
using SMC.Framework.Repository;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using SMC.Inscricoes.ServiceContract.Areas.INS.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace SMC.Academico.UnitTests.API.CAM.Jobs
{
    public class CaragaIngressantesJobTest
    {
        private CargaIngressantesJob _Subject;
        private TestContainerAcademico _Container;

        private CargaIngressanteSATVO _Filter;

        #region [ Moq ]

        private Mock<ISMCRepository<Chamada>> _MockChamadaRepository;
        private Mock<ISMCReportProgressJobService> _MockJobs;
        private Mock<IIntegracaoService> _MockIntegracaoService;

        #endregion [ Moq ]

        public CaragaIngressantesJobTest()
        {
            _Container = new TestContainerAcademico();
            _Subject = new CargaIngressantesJob();

            _Filter = new CargaIngressanteSATVO()
            {
                SeqChamada = 1,
                SeqEntidadeInstituicao = 2,
                SeqHistoricoAgendamento = 3,
                SeqSolicitante = "4",
                NomeSolicitante = "Teste"
            };

            _MockChamadaRepository = _Container.CreateMock<ISMCRepository<Chamada>>();
            _MockJobs = _Container.CreateMock<ISMCReportProgressJobService>();
            _MockIntegracaoService = _Container.CreateMock<IIntegracaoService>();
        }

        [Fact]
        public void ExecucaoSemNenhumAlunoFinalizadaComSucesso()
        {
            _MockIntegracaoService.Setup(m => m.BuscarDadosInscricoes(It.IsAny<List<long>>()))
                .Returns(new List<PessoaIntegracaoData>());

            _Subject.Execute(_Filter);

            _MockJobs.Verify(m => m.ReportProgress(It.Is<SMCSchedulerHistoryModel>(r => r.Progress == 100)), Times.Once);
            _MockJobs.Verify(m => m.LogSucess(It.IsAny<SMCSchedulerHistoryModel>()), Times.AtLeastOnce);
        }
    }
}
