using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class ProcessoSeletivoController : SMCDynamicControllerBase
    {
        #region Services
        private ICampanhaService CampanhaService => Create<ICampanhaService>();

        private ITipoVinculoAlunoService TipoVinculoAlunoService => Create<ITipoVinculoAlunoService>();

        private IFormaIngressoService FormaIngressoService => Create<IFormaIngressoService>();

        private IProcessoService ProcessoService => Create<IProcessoService>();
        #endregion

        [ChildActionOnly]
        public ActionResult CabecalhoProcessoSeletivo(long seqCampanha)
        {
            var model = new CabecalhoProcessoSeletivoViewModel()
            {
                SeqCampanha = seqCampanha
            };
            var campanha = CampanhaService.BuscarCampanha(seqCampanha);
            model.Campanha = campanha.Descricao;
            model.CiclosLetivos = campanha.CiclosLetivos.Select(f => f.Descricao).ToList();

            return PartialView("_CabecalhoProcessoSeletivo", model);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarTipoVinculo(long seqTipoProcessoSeletivo)
        {
            var lista = TipoVinculoAlunoService.BuscarTipoVinculoAlunoPorTipoProcessoSeletivo(seqTipoProcessoSeletivo);
            return Json(lista);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarFormaIngresso(List<long> seqsNivelEnsino, long? seqTipoProcessoSeletivo, long? seqTipoVinculoAluno)
        {
            var lista = FormaIngressoService.BuscarFormasIngressoInstituicaoNivelVinculoSelect(new FormaIngressoFiltroData()
            {
                SeqsNivelEnsino = seqsNivelEnsino,
                SeqTipoVinculoAluno = seqTipoVinculoAluno,
                SeqTipoProcessoSeletivo = seqTipoProcessoSeletivo
            });
            return Json(lista);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessoMatricula(long seqCicloLetivo, long seqCampanha)
        {
            var lista = ProcessoService.BuscarProcessosMatriculaPorCicloLetivoSelect(seqCicloLetivo, seqCampanha);
            return Json(lista);
        }
    }
}