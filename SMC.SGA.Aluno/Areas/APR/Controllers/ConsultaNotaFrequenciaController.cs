using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.SGA.Aluno.Areas.APR.Models.ConsultaNotaFrequencia;
using SMC.SGA.Aluno.Extensions;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.APR.Controllers
{
    public class ConsultaNotaFrequenciaController : SMCControllerBase
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

        #endregion Serviços

        [SMCAuthorize(UC_APR_001_12_01.CONSULTA_NOTA_FREQUENCIA)]
        public ActionResult Index()
        {
            var alunoLogado = this.GetAlunoLogado();
            if (alunoLogado == null || alunoLogado.Seq == 0)
                throw new AlunoLogadoSemVinculoException();

            // Busca os dados do histórico do aluno
            var dados = HistoricoEscolarService.ConsultarNotasFrequencias(alunoLogado.Seq, true, true, true, true);
            HistoricoViewModel model = new HistoricoViewModel();

            if (dados.Count() == 0)
            {
                return View(model);
            }

            // Prepara o modelo
            model.PermitirCredito = dados.FirstOrDefault().PermitirCredito;
            model.AproveitamentoCredito = new SMCPagerModel<ComponeteCreditoViewModel>(dados.Where(w => w.TipoConclusao == TipoConclusao.AproveitamentoCredito).TransformList<ComponeteCreditoViewModel>());
            model.ComponentesConcluidos = new SMCPagerModel<ComponeteCreditoViewModel>(dados.Where(w => w.TipoConclusao == TipoConclusao.ComponenteConcluido).TransformList<ComponeteCreditoViewModel>());
            model.ComponentesSemApuracao = new SMCPagerModel<ComponeteCreditoViewModel>(dados.Where(w => w.TipoConclusao == TipoConclusao.CursadoSemHistoricoEscolar).TransformList<ComponeteCreditoViewModel>());
            model.ComponentesExame = new SMCPagerModel<ComponeteCreditoViewModel>(dados.Where(w => w.TipoConclusao == TipoConclusao.Exame).TransformList<ComponeteCreditoViewModel>());

            model.ComponentesConcluidos.SMCForEach(c =>
            {
                //Quando a situação for dispensado, não exibir nota
                if (c.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Dispensado)
                    c.Nota = string.Empty;

                //Quando a situação for reprovado, não exibir conceito
                if (c.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado)
                    c.DescricaoEscalaApuracaoItem = string.Empty;

                //Só exibir conceito se existir nota cadastrada
                if (string.IsNullOrEmpty(c.Nota))
                    c.DescricaoEscalaApuracaoItem = string.Empty;
            });

            model.ComponentesExame.SMCForEach(c =>
            {
                //Quando a situação for reprovado, não exibir conceito
                if (c.SituacaoHistoricoEscolar == SituacaoHistoricoEscolar.Reprovado)
                    c.DescricaoEscalaApuracaoItem = string.Empty;

                //Só exibir conceito se existir nota cadastrada
                if (string.IsNullOrEmpty(c.Nota))
                    c.DescricaoEscalaApuracaoItem = string.Empty;
            });
            VerificarPossuiDadosHistoricoEscolar(model);
            return View(model);
        }

        private void VerificarPossuiDadosHistoricoEscolar(HistoricoViewModel model)
        {
            if (model.AproveitamentoCredito.SMCAny() ||
               model.ComponentesConcluidos.SMCAny() ||
               model.ComponentesExame.SMCAny() ||
               model.ComponentesSemApuracao.SMCAny())
            {
                model.PossuiDadosHistoricoEscolar = true;
            }
        }
    }
}