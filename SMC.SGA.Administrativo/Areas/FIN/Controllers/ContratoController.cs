using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{
    public class ContratoController : SMCDynamicControllerBase
    {
        #region Serviços

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService
        {
            get { return this.Create<ICursoOfertaLocalidadeService>(); }
        }

        private IContratoService ContratoService
        {
            get { return this.Create<IContratoService>(); }
        }

        private ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        private IArquivoAnexadoService ArquivoAnexadoService
        {
            get { return this.Create<IArquivoAnexadoService>(); }
        }

        #endregion

        [SMCAuthorize(UC_FIN_002_02_01.PESQUISAR_CONTRATO)]
        public ActionResult BuscarTurnosPorCursoOfertaLocalidadeSelect(long? SeqCursoOfertaLocalidade)
        {
            var turnos = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(SeqCursoOfertaLocalidade.Value);
            return Json(turnos);
        } 

        [SMCAuthorize(UC_FIN_002_02_01.PESQUISAR_CONTRATO)]
        public ActionResult VisualizarArquivo(long SeqContrato)
        {
            var arquivo = this.ContratoService.BuscarContrato(SeqContrato);

            var file = File(arquivo.ArquivoAnexado.FileData, arquivo.ArquivoAnexado.Type, arquivo.ArquivoAnexado.Name);

            Response.AddHeader("Content-Disposition", "inline; filename= "+ arquivo.ArquivoAnexado.Name);

            return File(file.FileContents, "application/pdf");
        } 
    }
}
