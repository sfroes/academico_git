using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.SGA.Administrativo.Areas.CUR.Views.ConfiguracaoComponenteMatriz.App_LocalResources;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class ConfiguracaoComponenteMatrizController : SMCControllerBase
    {
        #region [ Services ]

        private IDivisaoMatrizCurricularComponenteService DivisaoMatrizCurricularComponenteService => Create<IDivisaoMatrizCurricularComponenteService>();
        private IDivisaoMatrizCurricularService DivisaoMatrizCurricularService => Create<IDivisaoMatrizCurricularService>();
        private IComponenteCurricularService ComponenteCurricularService => Create<IComponenteCurricularService>();
        private IInstituicaoNivelTipoComponenteCurricularService InstituicaoNivelTipoComponenteCurricularService => Create<IInstituicaoNivelTipoComponenteCurricularService>();
        private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();
        private IConfiguracaoComponenteService ConfiguracaoComponenteService => Create<IConfiguracaoComponenteService>();

        #endregion [ Services ]

        [SMCAuthorize(UC_CUR_001_05_06.PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ)]
        public ActionResult ConfiguracaoComponenteMatrizCabecalho(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = MatrizCurricularService.BuscarMatrizCurricularCabecalho(seqMatrizCurricular).Transform<MatrizCurricularCabecalhoViewModel>();


            ConfiguracaoComponeteMatrizCabecalhoViewModel modelo = ConfiguracaoComponenteService.BuscarCabecalhoConfiguracaoComponentePorMatriz(seqMatrizCurricular)
                                                                                                .Transform<ConfiguracaoComponeteMatrizCabecalhoViewModel>();
            return PartialView("_Cabecalho", modelo);
        }

        [SMCAuthorize(UC_CUR_001_05_06.PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ)]
        public ActionResult Index(ConfiguracaoComponeteMatrizFiltroViewModel filtros)
        {
            if (filtros.SeqCurriculoCursoOferta == null || filtros.SeqMatrizCurricular == null) { return RedirectToIndex(); }

            if (TempData["filtroConfiguracaoMatriz"] != null && Request.QueryString["voltar"] != null)
            {
                filtros = TempData["filtroConfiguracaoMatriz"] as ConfiguracaoComponeteMatrizFiltroViewModel;
            }

            filtros.DivisoesMatrizCurricular = DivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularDescricaoSelect(filtros.SeqMatrizCurricular.Value)
                                                                             .TransformList<SMCSelectListItem>();
            filtros.ComponentesCurriculares = ComponenteCurricularService.BuscarComponenteCurricularPorMatrizSelect(filtros.SeqMatrizCurricular.Value)
                                                                         .TransformList<SMCSelectListItem>();
            filtros.TipoComponenteComponenteCurricular = InstituicaoNivelTipoComponenteCurricularService.BuscarInstituicaoNivelTipoComponenteMatrizCurricularSelect(filtros.SeqMatrizCurricular.Value)
                                                                                                        .TransformList<SMCSelectListItem>();

            //NV09 - não exibir o tipo 'Assunto do componente'
            filtros.TipoComponenteComponenteCurricular.RemoveAll(r => r.Text.ToLower() == "assunto do componente");

            SMCDatasourceItem sMCDatasourceItem = new SMCDatasourceItem();
            sMCDatasourceItem.Descricao = "Sim";
            sMCDatasourceItem.Seq = 1;
            filtros.somente = new List<SMCDatasourceItem>() { sMCDatasourceItem };

            return View(filtros);
        }

        [SMCAuthorize(UC_CUR_001_05_07.CONFIGURAR_COMPONENTE_MATRIZ)]
        public ActionResult ExcluirConfiguracao(SMCEncryptedLong seq, SMCEncryptedLong seqMatrizCurricular, SMCEncryptedLong seqCurriculoCursoOferta)
        {
            try
            {
                DivisaoMatrizCurricularComponenteService.ExcluirConfiguracaoComponente(seq);

                SetSuccessMessage(UIResource.Mensagem_Exclusao_Configuracao_Componete,
                                  UIResource.Titulo_Mensagem_Exclusao_Configuracao_Componete,
                                  SMCMessagePlaceholders.Centro);
            }
            catch (Exception e)
            {
                SetErrorMessage(e.Message);
            }
            return RedirectToAction("Index", "ConfiguracaoComponenteMatriz", new { SeqMatrizCurricular = seqMatrizCurricular, SeqCurriculoCursoOferta = seqCurriculoCursoOferta, voltar = true });
        }

        [SMCAuthorize(UC_CUR_001_05_06.PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ)]
        public ActionResult BuscarComponentesCurriculares(ConfiguracaoComponeteMatrizFiltroViewModel filtros)
        {
            var filtroData = filtros.Transform<DivisaoMatrizCurricularComponenteFiltroData>();

            TempData["filtroConfiguracaoMatriz"] = filtros;

            List<ConfiguracaoComponeteMatrizListarViewModel> result = DivisaoMatrizCurricularComponenteService.BuscarDivisaoMatrizCurricularGruposComponentes(filtroData)
                                                                                                                .TransformList<ConfiguracaoComponeteMatrizListarViewModel>();

            return PartialView("_DetailList", result);
        }

        /// <summary>
        /// Redireciona o usuário para a index
        /// </summary>
        /// <returns></returns>
        [SMCAuthorize(UC_CUR_001_05_06.PESQUISAR_CONFIGURACAO_COMPONENTE_MATRIZ)]
        private ActionResult RedirectToIndex()
        {
            return RedirectToAction("Index", "Curriculo");
        }
    }
}