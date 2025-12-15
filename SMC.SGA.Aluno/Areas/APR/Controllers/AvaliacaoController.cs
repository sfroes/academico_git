using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.SGA.Aluno.Areas.APR.Models.Avaliacao;
using SMC.SGA.Aluno.Extensions;
using SMC.SGA.Aluno.Models;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.APR.Controllers
{
    public class AvaliacaoController : SMCControllerBase
    {
        #region Serviços

        private IIntegracaoAcademicoService IntegracaoAcademicoService
        {
            get { return this.Create<IIntegracaoAcademicoService>(); }
        }

        private IAlunoService AlunoService
        {
            get { return this.Create<IAlunoService>(); }
        }

        private IHistoricoEscolarService HistoricoEscolarService => Create<IHistoricoEscolarService>();

        private IAvaliacaoService AvaliacaoService => Create<IAvaliacaoService>();

        #endregion Serviços

        [SMCAuthorize(UC_APR_001_13_01.CONSULTAR_AVALIACAO)]
        public ActionResult Index(long seqTurma)
        {
            AlunoSelecionadoViewModel alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            ConsultaAvaliacoesTurmaViewModel modelo = AvaliacaoService.ConsultaAvaliacoes(seqTurma, alunoLogado.Seq).Transform<ConsultaAvaliacoesTurmaViewModel>();

            return View(modelo);
        }
    }
}