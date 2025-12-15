using SMC.Academico.Common.Areas.MAT.Exceptions;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.TUR.Models;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Controllers
{
    public class DetalhesDivisaoTurmaController : SMCControllerBase
    {
        #region [ Services ]

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return this.Create<IDivisaoTurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        [HttpGet]
        public ActionResult VisualizarDivisaoTurmaDetalhes(SMCEncryptedLong seqDivisaoTurma, long? seqPessoaAtuacao)
        {
            var registro = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(seqDivisaoTurma, seqPessoaAtuacao, telaDetalhes: true);

            var view = GetExternalView(AcademicoExternalViews.DETALHES_DIVISAO_TURMA);
            return PartialView(view, registro.TransformList<DetalhesDivisaoTurmaViewModel>());
        }

        public ActionResult VisualizarDivisaoTurmaDependency(SMCEncryptedLong seqDivisaoTurmaTemp, long? seqPessoaAtuacao)
        {
            var model = new SelecaoTurmaOfertaDivisaoViewModel();
            model.SeqDivisaoTurmaTemp = seqDivisaoTurmaTemp;
            model.SeqPessoaAtuacao = seqPessoaAtuacao.GetValueOrDefault();
            var view = GetExternalView(AcademicoExternalViews.DETALHES_DIVISAO_TURMA_DEPENDENCY);
            return PartialView(view, model);
        }
    }
}
