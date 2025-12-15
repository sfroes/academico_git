using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class RequisitoController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private IComponenteCurricularService ComponenteCurricularService
        {
            get { return this.Create<IComponenteCurricularService>(); }
        }

        private IDivisaoMatrizCurricularService DivisaoMatrizCurricularService
        {
            get { return this.Create<IDivisaoMatrizCurricularService>(); }
        }

        private IMatrizCurricularService MatrizCurricularService
        {
            get { return this.Create<IMatrizCurricularService>(); }
        }

        private IRequisitoService RequisitoService
        {
            get { return this.Create<IRequisitoService>(); }
        }

        private ICurriculoCursoOfertaGrupoService CurriculoCursoOfertaGrupoService
        {
            get { return this.Create<ICurriculoCursoOfertaGrupoService>(); }
        }

        private IGrupoCurricularService GrupoCurricularService
        {
            get { return this.Create<IGrupoCurricularService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CUR_003_01_01.PESQUISAR_REQUISITO)]
        public ActionResult CabecalhoRequisitoMatrizCurricular(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = ExecuteService<MatrizCurricularCabecalhoData, MatrizCurricularCabecalhoViewModel>(MatrizCurricularService.BuscarMatrizCurricularCabecalho, seqMatrizCurricular);
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_003_01_01.PESQUISAR_REQUISITO)]
        public ActionResult CabecalhoLista(RequisitoDynamicModel model)
        {
            return PartialView("_CabecalhoLista", model);
        }

        [SMCAuthorize(UC_CUR_003_01_01.PESQUISAR_REQUISITO)]
        public JsonResult BuscarComponentesCurricularPorMatrizDivisao(long? seqDivisaoCurricularItem, long seqMatrizCurricular)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            listItens = ComponenteCurricularService.BuscarComponenteCurricularPorMatrizRequisitoSelect(seqMatrizCurricular, seqDivisaoCurricularItem);

            return Json(listItens);
        }


        [SMCAuthorize(UC_CUR_003_01_01.PESQUISAR_REQUISITO)]
        public JsonResult BuscarPreOuCoRequisitoItem(TipoRequisitoItem tipoRequisitoItem, TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqDivisaoCurricularItem, long? seqComponenteCurricular, long? seqDivisaoCurricularItemDetalheAuxiliar, long? seqComponenteCurricularDetalheAuxiliar)
        {
            List<SMCDatasourceItem> listItens = new List<SMCDatasourceItem>();

            if (tipoRequisitoItem == TipoRequisitoItem.DivisaoMatriz)
            {
                listItens = DivisaoMatrizCurricularService.BuscarDivisoesMatrizCurricularTipoPorTipoRequisitoSelect(tipoRequisito, seqMatrizCurricular, seqDivisaoCurricularItem);

                if (seqDivisaoCurricularItemDetalheAuxiliar.HasValue)
                {
                    listItens.FirstOrDefault(a => a.Seq == seqDivisaoCurricularItemDetalheAuxiliar.Value).Selected = true;
                }
            }

            if (tipoRequisitoItem == TipoRequisitoItem.ComponenteCurricular)
            {
                listItens = ComponenteCurricularService.BuscarComponenteCurricularPorComponenteRequisitoSelect(tipoRequisito, seqMatrizCurricular, seqComponenteCurricular);

                if (seqComponenteCurricularDetalheAuxiliar.HasValue)
                {
                    listItens.FirstOrDefault(a => a.Seq == seqComponenteCurricularDetalheAuxiliar.Value).Selected = true;
                }
            }

            return Json(new
            {
                SeqDivisaoCurricularItemDetalhe = listItens,
                SeqComponenteCurricularDetalhe = listItens
            });
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public JsonResult BuscarGruposCurriculares(long seqCurriculoCursoOferta, long? seqComponenteCurricular)
        {
            if (seqComponenteCurricular.HasValue)
                return Json(this.CurriculoCursoOfertaGrupoService.BuscarGruposCurricularesCurriculoCursoOfertaSelect(seqCurriculoCursoOferta, seqComponenteCurricular));
            else
                return Json(this.CurriculoCursoOfertaGrupoService.BuscarGruposCurricularesCurriculoCursoOfertaSelect(seqCurriculoCursoOferta));
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public JsonResult BuscaGrupoCurricularDescricaoFormatada(long? seqGrupoCurricular)
        {
            if (seqGrupoCurricular.HasValue)
                return Json(this.GrupoCurricularService.BuscaGrupoCurricularDescricaoFormatada(seqGrupoCurricular.Value));
            else
                return Json(string.Empty);
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public JsonResult BuscarTiposRequisito(long? seqComponenteCurricular, TipoRequisito tipoRequisitoAuxiliar)
        {
            var result = this.RequisitoService.BuscarTiposRequisitoSelect(seqComponenteCurricular);

            if (tipoRequisitoAuxiliar != TipoRequisito.Nenhum)
            {
                result.FirstOrDefault(a => (TipoRequisito)a.Seq == tipoRequisitoAuxiliar).Selected = true;
            }

            return Json(result);
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public ActionResult AssociarMatrizCurricular(long seq, long seqMatrizCurricular, long seqCurriculoCursoOferta)
        {
            try
            {
                this.RequisitoService.AssociarRequisito(seq, seqMatrizCurricular);

                // Renderiza a lista novamente
                return SMCRedirectToAction("Index", routeValues: new { seqMatrizCurricular = new SMCEncryptedLong(seqMatrizCurricular), seqCurriculoCursoOferta = new SMCEncryptedLong(seqCurriculoCursoOferta) });
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de associar, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public ActionResult DesassociarMatrizCurricular(long seq, long seqMatrizCurricular, bool excluirRequisito, long seqCurriculoCursoOferta)
        {
            try
            {
                this.RequisitoService.DesassociarRequisito(seq, seqMatrizCurricular, excluirRequisito);

                // Renderiza a lista novamente
                return SMCRedirectToAction("Index", routeValues: new { seqMatrizCurricular = new SMCEncryptedLong(seqMatrizCurricular), seqCurriculoCursoOferta = new SMCEncryptedLong(seqCurriculoCursoOferta) });
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de associar, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }

        [SMCAuthorize(UC_CUR_003_01_02.MANTER_REQUISITO)]
        public JsonResult BuscarTiposRequisitosItensPorTipoRequisitoSelec(TipoRequisito tipoRequisito, TipoRequisitoItem tipoRequisitoItemAuxiliar)
        {
            var result = new List<SMCDatasourceItem>();

            if (tipoRequisito == TipoRequisito.CoRequisito)
            {
                result.Add(new SMCDatasourceItem() { Seq = (long)TipoRequisitoItem.ComponenteCurricular, Descricao = SMCEnumHelper.GetDescription(TipoRequisitoItem.ComponenteCurricular) });
            }
            else
            {
                result.AddRange(SMCEnumHelper.GenerateKeyValuePair<TipoRequisitoItem>().TransformSelectItem());
            }

            if (tipoRequisitoItemAuxiliar != TipoRequisitoItem.Nenhum)
            {
                result.FirstOrDefault(a => (TipoRequisitoItem)a.Seq == tipoRequisitoItemAuxiliar).Selected = true;
            }

            return Json(result);
        }
    }
}