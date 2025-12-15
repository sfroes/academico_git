using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Areas.CAM.Views.Campanha.App_LocalResources;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class CampanhaController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private ICampanhaService CampanhaService
        {
            get { return this.Create<ICampanhaService>(); }
        }

        private ITipoProcessoSeletivoService TipoProcessoSeletivoService
        {
            get { return this.Create<ITipoProcessoSeletivoService>(); }
        }

        private IProcessoSeletivoService ProcessoSeletivoService
        {
            get { return this.Create<IProcessoSeletivoService>(); }
        }

        private ICicloLetivoService CicloLetivoService
        {
            get { return this.Create<ICicloLetivoService>(); }
        }

        private IConvocacaoService ConvocacaoService
        {
            get { return this.Create<IConvocacaoService>(); }
        }

        private IChamadaService ChamadaService
        {
            get { return this.Create<IChamadaService>(); }
        }

        #endregion [ Serviços ]

        #region Consultar Candidatos

        [SMCAuthorize(UC_CAM_001_03_01.CONSULTAR_CANDIDATO)]
        public ActionResult ConsultarCandidatos(SMCEncryptedLong seqCampanha, SMCEncryptedLong seqConvocacao)
        {
            var model = new CampanhaConsultarCandidatoFiltroViewModel() { SeqCampanha = seqCampanha };
            if (seqConvocacao != null)
                model.SeqConvocacao = seqConvocacao;

            try
            {
                PrepararModeloCandidatoCampanha(model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Default);
            }

            return View(model);
        }

        [SMCAuthorize(UC_CAM_001_03_01.CONSULTAR_CANDIDATO)]
        public ActionResult ListarCandidatosCampanha(CampanhaConsultarCandidatoFiltroViewModel filtros, int exportAction = 0)
        {
            var filtrosData = filtros.Transform<CampanhaConsultarCandidatoFiltroData>();
            switch (filtros.Exportado)
            {
                case CampanhaConsultarCandidatoFiltroViewModel.FiltroExportado.Sim:
                    filtrosData.Exportado = true;
                    break;

                case CampanhaConsultarCandidatoFiltroViewModel.FiltroExportado.Nao:
                    filtrosData.Exportado = false;
                    break;
            }

            if (exportAction == 1)
            {
                filtrosData.PageSettings.PageSize = int.MaxValue;
                filtrosData.PageSettings.PageIndex = 1;

                var dtoExport = CampanhaService.BuscarCandidatosCampanha(filtrosData).Transform<SMCPagerData<CampanhaConsultarCandidatoListarViewModel>>();
                return File(SMCGridExporter.ExportGridModelToExcel(this, "Candidatos", dtoExport), "application/excel", "candidatos.xlsx");
            }

            var dto = CampanhaService.BuscarCandidatosCampanha(filtrosData);
            var viewModelLista = dto.Transform<SMCPagerData<CampanhaConsultarCandidatoListarViewModel>>();
            var model = new SMCPagerModel<CampanhaConsultarCandidatoListarViewModel>(viewModelLista, filtros.PageSettings, filtros);
            return PartialView("_ListarCandidatosCampanha", model);
        }

        [SMCAllowAnonymous]
        public ActionResult CabecalhoCampanha(SMCEncryptedLong seqCampanha)
        {
            var result = this.CampanhaService.BuscarCabecalhoCampanha(seqCampanha);

            return PartialView("_CabecalhoCampanha", result.Transform<CampanhaConsultarCandidatoCabecalhoViewModel>());
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect(SMCEncryptedLong seqCampanha, long? seqTipoProcessoSeletivo)
        {
            var result = this.ProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect(seqCampanha, seqTipoProcessoSeletivo);

            return Json(result);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect(SMCEncryptedLong seqCampanha, long? seqCicloLetivo, long? seqTipoProcessoSeletivo, long? seqProcessoSeletivo)
        {
            var result = this.ConvocacaoService.BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect(seqCampanha, seqCicloLetivo, seqTipoProcessoSeletivo, seqProcessoSeletivo);

            return Json(result);
        }

        [SMCAllowAnonymous]
        public ActionResult BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect(SMCEncryptedLong seqCampanha, long? seqConvocacao, TipoChamada? tipoChamada)
        {
            var result = this.ChamadaService.BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect(seqCampanha, seqConvocacao, tipoChamada);

            return Json(result);
        }

        private void PrepararModeloCandidatoCampanha(CampanhaConsultarCandidatoFiltroViewModel model)
        {
            model.CiclosLetivos = this.CicloLetivoService.BuscarCiclosLetivosPorCampanhaSelect(model.SeqCampanha);
            model.TiposProcessoSeletivo = this.TipoProcessoSeletivoService.BuscarTiposProcessoSeletivoPorCampanhaSelect(model.SeqCampanha);
            model.ProcessosSeletivos = this.ProcessoSeletivoService.BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect(model.SeqCampanha); // ao entrar na tela carregar todos os processos seletivos pela campanha
            model.Convocacoes = this.ConvocacaoService.BuscarConvocacoesPorCampanhaCicloLetivoProcessoSeletivoSelect(model.SeqCampanha);// ao entrar na tela carregar todas as convocações pela campanha
            model.Chamadas = this.ChamadaService.BuscarChamadasPorCampanhaConvocacaoTipoChamadaSelect(model.SeqCampanha);// ao entrar na tela carregar todas as chamadas pela campanha
        }

        #endregion Consultar Candidatos

        [SMCAllowAnonymous]
        public ActionResult BuscarCampanhas(CampanhaFiltroViewModel filtros)
        {
            var filtrosData = filtros.Transform<CampanhaFiltroData>();
            var campanhasSelectItem = CampanhaService.BuscarCampanhasSelect(filtrosData);
            return Json(campanhasSelectItem);
        }
    }
}