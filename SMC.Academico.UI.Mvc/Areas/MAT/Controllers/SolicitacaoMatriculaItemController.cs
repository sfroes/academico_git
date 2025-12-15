using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Controllers
{
    public class SolicitacaoMatriculaItemController : SMCControllerBase
    {
        #region [ Services ]

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService
        {
            get { return this.Create<ISolicitacaoMatriculaItemService>(); }
        }

        #endregion [ Services ]
        
        [SMCAuthorize(UC_MAT_003_20_01.VISUALIZAR_ITENS_PLANO_ESTUDO)] 
        public ActionResult VisualizarTurmaAtividade(SMCEncryptedLong seqSolicitacaoMatricula, SMCEncryptedLong seqConfiguracaoEtapa, bool erro, bool desconsideraEtapa, bool desativarFiltroDados = false)
        {
            var registro = SolicitacaoMatriculaItemService.BuscarSolicitacaoMatriculaTurmasAtividades(seqSolicitacaoMatricula, seqConfiguracaoEtapa, erro, desconsideraEtapa, desativarFiltroDados);

            registro.Turmas = registro.Turmas.Where(w => !string.IsNullOrEmpty(w.Situacao)).ToList();

            registro.Atividades = registro.Atividades.Where(w => !string.IsNullOrEmpty(w.Situacao)).ToList();

            var view = GetExternalView(AcademicoExternalViews.VISUALIZAR_ITENS_TURMA_ATIVIDADES);
            return PartialView(view, registro.Transform<SolicitacaoMatriculaItemViewModel>());
        }      
    }
}
