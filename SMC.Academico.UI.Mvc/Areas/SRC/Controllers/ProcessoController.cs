using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Controllers
{
    public class ProcessoController : SMCControllerBase
    {
        #region [ Services ]

        private IProcessoService ProcessoService
        {
            get { return this.Create<IProcessoService>(); }
        }

        private IServicoService ServicoService
        {
            get { return this.Create<IServicoService>(); }
        }

        #endregion [ Services ]

        [SMCAllowAnonymous]
        public ActionResult PreencherDataInicio(SMCEncryptedLong seqCicloLetivo)
        {
            var result = this.ProcessoService.BuscarProcesso(new ProcessoFiltroData() { SeqCicloLetivo = seqCicloLetivo });

            return Json(result != null ? result.DataInicio.ToString("MM/dd/yyyy") : string.Empty);
        }

        [SMCAllowAnonymous]
        public ActionResult PreencherDataFim(SMCEncryptedLong seqCicloLetivo)
        {
            var result = this.ProcessoService.BuscarProcesso(new ProcessoFiltroData() { SeqCicloLetivo = seqCicloLetivo });

            return Json(result != null && result.DataFim.HasValue ? result.DataFim.Value.ToString("MM/dd/yyyy") : string.Empty);
        }

        /// <summary>
        /// Listar os serviços de acordo com o tipo atuação selecionado.
        /// </summary>
        /// <param name="TipoAtuacao"></param>
        /// <returns></returns>
        [SMCAllowAnonymous]
        public ActionResult BuscarServicos(TipoAtuacao? tipoAtuacao)
        {
            var result = ServicoService.BuscarServicosSelect(new ServicoFiltroData { TipoAtuacao = tipoAtuacao });

            return Json(result);
        }
    }
}