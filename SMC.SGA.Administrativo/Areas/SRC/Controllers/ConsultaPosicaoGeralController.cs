using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.SRC.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Controllers
{
    public class ConsultaPosicaoGeralController : SMCControllerBase
    {
        #region [ Services ]

        public IServicoService ServicoService => Create<IServicoService>();

        public IProcessoUnidadeResponsavelService ProcessoUnidadeResponsavelService => Create<IProcessoUnidadeResponsavelService>();

        public IProcessoService ProcessoService => Create<IProcessoService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_SRC_005_02_01.CONSULTA_POSICAO_CONSOLIDADA_GERAL)]
        public ActionResult Index()
        {
            var model = PreencherModelo();
            return View(model);
        }

        [SMCAuthorize(UC_SRC_005_02_01.CONSULTA_POSICAO_CONSOLIDADA_GERAL)]
        public ActionResult ListarPosicaoConsolidadaGeral(ConsultaPosicaoGeralFiltroViewModel filtros)
        {
            var data = filtros.SeqsServicos.SMCAny() ?
                ProcessoService.BuscarPosicaoConsolidadaGeral(filtros.Transform<ProcessoFiltroData>()) :
                new ConsultaPosicaoGeralData();

            var model = new ConsultaPosicaoGeralViewModel()
            {
                QuantidadeSolicitacoesTotal = data.QuantidadeSolicitacoesTotal,
                Processos = new SMCPagerModel<ConsultaPosicaoGeralProcessoViewModel>(data.Processos?.Transform<SMCPagerData<ConsultaPosicaoGeralProcessoViewModel>>(), filtros.PageSettings, filtros)
            };

            return PartialView("_ListarPosicaoConsolidadaGeral", model);
        }

        [SMCAuthorize(UC_SRC_005_02_01.CONSULTA_POSICAO_CONSOLIDADA_GERAL)]
        public ActionResult BuscarProcessos(long[] seqsServicos, long[] seqsEntidadesResponsaveis = null, long? seqCicloLetivo = null)
        {
            //FIX: Remover ao corrigir dependency de select multiplo
            seqsEntidadesResponsaveis = seqsEntidadesResponsaveis?.Where(w => w != 0).ToArray();

            var pageSettings = new SMCPageSetting() { PageSize = int.MaxValue };
            pageSettings.AddSortField(nameof(ProcessoDynamicModel.DataInicio), SMCSortDirection.Descending);
            return Json(ProcessoService.BuscarProcessosSelect(new ProcessoFiltroData()
            {
                SeqsServicos = seqsServicos,
                SeqsEntidadesResponsaveis = seqsEntidadesResponsaveis,
                SeqCicloLetivo = seqCicloLetivo,
                PageSettings = pageSettings
            }));
        }

        private ConsultaPosicaoGeralFiltroViewModel PreencherModelo()
        {
            var filtro = new ConsultaPosicaoGeralFiltroViewModel();
            filtro.Servicos = ServicoService
                .BuscarServicosSelect()
                .TransformList<SMCSelectListItem>();
            filtro.EntidadesResponsaveis = ProcessoUnidadeResponsavelService
                .BuscarUnidadesResponsaveisVinculadasProcessoSelect()
                .TransformList<SMCSelectListItem>();
            return filtro;
        }
    }
}