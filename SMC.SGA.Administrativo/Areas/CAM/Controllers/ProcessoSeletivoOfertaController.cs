using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class ProcessoSeletivoOfertaController : SMCControllerBase
    {
        #region Services
        private IProcessoSeletivoService ProcessoSeletivoService => Create<IProcessoSeletivoService>();

        private IProcessoSeletivoOfertaService ProcessoSeletivoOfertaService => Create<IProcessoSeletivoOfertaService>();

        private ITipoOfertaService TipoOfertaService => Create<ITipoOfertaService>();

        private ICampanhaService CampanhaService => Create<ICampanhaService>();

        private ICampanhaOfertaService CampanhaOfertaService => Create<ICampanhaOfertaService>();

        #endregion

        #region Listar
        [SMCAuthorize(UC_CAM_001_02_03.PESQUISAR_OFERTA_PROCESSO_SELETIVO)]
        public ActionResult Index(ProcessoSeletivoOfertaFiltroViewModel filtro)
        {
            try
            {
                // Se o retorno vier sem a campanha, buscar a campanha.
                filtro.SeqCampanha = filtro.SeqCampanha == 0 ? BuscarSeqCampanha(filtro.SeqProcessoSeletivo) : filtro.SeqCampanha;

                filtro.TiposOferta = TipoOfertaService.BuscarTiposOfertaPorProcessoSeletivoSelect(filtro.SeqProcessoSeletivo)
                                                        .TransformList<SMCSelectListItem>();
                return View(filtro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        [SMCAuthorize(UC_CAM_001_02_03.PESQUISAR_OFERTA_PROCESSO_SELETIVO)]
        public ActionResult ListarOfertasProcessoSeletivo(ProcessoSeletivoOfertaFiltroViewModel filtro)//, int exportAction = 0)
        {
            filtro.PageSettings.PageSize = int.MaxValue;
            filtro.PageSettings.PageIndex = 1;


            //TODO: Removido o código comforme descrito na aba FIX do BUG 36863
            //if (exportAction == 1)
            //{
            //    //filtro.PageSettings.PageSize = int.MaxValue;
            //    //filtro.PageSettings.PageIndex = 1;

            //    var dtoExport = ProcessoSeletivoOfertaService.BuscarProcessosSeletivoOferta(filtro.Transform<ProcessoSeletivoOfertaFiltroData>())
            //                                            .Transform<SMCPagerData<ProcessoSeletivoOfertaListaViewModel>>();
            //    return File(SMCGridExporter.ExportGridModelToExcel(this, "ProcessoSeletivoOfertas", dtoExport), "application/excel", "ProcessoSeletivoOfertas.xlsx");
            //}

            var data = ProcessoSeletivoOfertaService.BuscarProcessosSeletivoOferta(filtro.Transform<ProcessoSeletivoOfertaFiltroData>())
                                                        .Transform<SMCPagerData<ProcessoSeletivoOfertaListaViewModel>>();

            var model = data.TransformList<ProcessoSeletivoOfertaListaViewModel>();
            return PartialView("_ListarOfertasProcessoSeletivo", model);
        }

        [ChildActionOnly]
        public ActionResult CabecalhoProcessoSeletivoOferta(long seqProcessoSeletivo, bool exibirNivelEnsino = false)
        {
            var model = ProcessoSeletivoService.BuscarProcessosSeletivoCabecalho(seqProcessoSeletivo).Transform<CabecalhoProcessoSeletivoOfertaViewModel>();
            model.ExibirNivelEnsino = exibirNivelEnsino;
            return PartialView("_CabecalhoProcessoSeletivoOferta", model);
        }
        #endregion

        #region Inserir / Editar / Excluir
        [SMCAuthorize(UC_CAM_001_02_04.ASSOCIAR_OFERTA_PROCESSO_SELETIVO)]
        public ActionResult Incluir(long seqProcessoSeletivo, long seqCampanha)
        {
            var model = new ProcessoSeletivoOfertaViewModel() { SeqCampanha = seqCampanha, SeqProcessoSeletivo = seqProcessoSeletivo };
            model.SeqEntidadeResponsavel = CampanhaService.BuscarCampanha(seqCampanha).SeqEntidadeResponsavel;

            model.Convocacoes = ProcessoSeletivoOfertaService.BuscarConvocacoesProcessoSeletivo(seqProcessoSeletivo)
                                                             .TransformList<ConvocacaoViewModel>();

            return View(model);
        }

        [SMCAuthorize(UC_CAM_001_02_04.ASSOCIAR_OFERTA_PROCESSO_SELETIVO)]
        [HttpPost]
        public ActionResult Salvar(ProcessoSeletivoOfertaViewModel model, List<long> gridConvocacoes)
        {
            var data = new ProcessoSeletivoOfertaData()
            {
                SeqProcessoSeletivo = model.SeqProcessoSeletivo,
                Ofertas = model.Ofertas.Select(f => f.Seq).ToList(),
                Convocacoes = gridConvocacoes
            };
            ProcessoSeletivoService.SalvarOfertaProcessoSeletivo(data);
            SetSuccessMessage(Views.ProcessoSeletivoOferta.App_LocalResources.UIResource.Mensagem_Associacao, target: SMCMessagePlaceholders.Centro);
            return RedirectToAction(nameof(Index), new { model.SeqCampanha, model.SeqProcessoSeletivo });
        }

        [SMCAuthorize(UC_CAM_001_02_04.ASSOCIAR_OFERTA_PROCESSO_SELETIVO)]
        public ActionResult Excluir(SMCEncryptedLong seq)
        {
            try
            {
                ProcessoSeletivoOfertaService.Excluir(seq);
                SetSuccessMessage(Views.ProcessoSeletivoOferta.App_LocalResources.UIResource.MSG_Excluir_Oferta_Processo_Seletivo_Sucesso,
                                  Views.ProcessoSeletivoOferta.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        #endregion Inserir / Editar / Excluir

        #region Configurar Vagas

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult ConfigurarVagas(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            try
            {
                if (model == null || model.SeqProcessoSeletivo == 0 || !model.selectedValues.SMCAny()) { return null; }

                var filtro = new ProcessoSeletivoOfertaFiltroData { SeqProcessoSeletivo = model.SeqProcessoSeletivo, Seqs = model.selectedValues.ToArray() };

                var data = ProcessoSeletivoOfertaService.BuscarProcessosSeletivoOferta(filtro);

                model.ProcessoSeletivoOfertas = data.TransformList<ConfigurarVagasOfertaProcessoSeletivoListaViewModel>();

                ///Caso exista, as convocações serão listadas
                model.Convocacoes = ProcessoSeletivoOfertaService.BuscarConvocacoesProcessoSeletivo(model.SeqProcessoSeletivo)
                                                             .TransformList<ConvocacaoListarViewModel>();

                model.ConvocacoesTree = SMCTreeView.For(model.Convocacoes);

                return View(model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToIndexOfertaProcessoSeletivo(model.SeqProcessoSeletivo);
            }
        }

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult SalvarVagasProcessoSeletivoOferta(ConfigurarVagasOfertaProcessoSeletivoViewModel model, List<ConfigurarVagasOfertaProcessoSeletivoListaViewModel> processoSeletivoOfertas)
        {
            model.ProcessoSeletivoOfertas = processoSeletivoOfertas;

            SalvarConfigurarVagas(model);

            return RedirectSuccesConfigurarVagasToIndex(model.SeqProcessoSeletivo);
        }

        /// <summary>
        /// Método que salva as configurações de vagas e faz suas respectivas replicações,
        /// conforme seleção do usuário
        /// </summary>
        /// <param name="model"></param>
        private void SalvarConfigurarVagas(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            var data = model.Transform<VagasProcessoSeletivoOfertaData>();

            if (model.ConvocacoesSelecionadas.SMCAny())
            {
                data.SeqsConvocacoes = model.ConvocacoesSelecionadas.Select(z => z.Value).ToArray();
            }

            //Salvar as vagas para o processoSeletivoOferta e Replica as vagas
            ProcessoSeletivoOfertaService.AtualizarVagasOfertasProcessoSeletivo(data);
        }

        #endregion Configurar Vagas

        #region Copiar Vagas da Campanha RN_CAM_068

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult ConfirmarCopiarVagasCampanha(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            if (model == null || model.SeqProcessoSeletivo == 0 || model.gridListaOfertasProcessoSeletivo == null) { return null; }

            BuscarProcessoSeletivoOfertasSelecionados(model);

            var filtroOfertaCampanha = new CampanhaOfertaFiltroTelaData { SeqsCampanhaOfertas = model.ProcessoSeletivoOfertas.Select(x => x.SeqCampanhaOferta).ToArray() };

            var ofertasCampanha = CampanhaOfertaService.BuscarCampanhaOfertas(filtroOfertaCampanha);

            model.CampanhaOfertas = ofertasCampanha.TransformList<ConfigurarVagasOfertaCampanhaListaViewModel>();

            return PartialView("_ConfirmarCopiarVagasCampanha", model);
        }

        private void BuscarProcessoSeletivoOfertasSelecionados(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            var filtro = new ProcessoSeletivoOfertaFiltroData { SeqProcessoSeletivo = model.SeqProcessoSeletivo, Seqs = model.gridListaOfertasProcessoSeletivo.ToArray() };

            var data = ProcessoSeletivoOfertaService.BuscarProcessosSeletivoOferta(filtro);

            model.ProcessoSeletivoOfertas = data.TransformList<ConfigurarVagasOfertaProcessoSeletivoListaViewModel>();
        }

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult CopiarVagasCampanha(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            try
            {
                BuscarProcessoSeletivoOfertasSelecionados(model);

                ProcessoSeletivoOfertaService.CopiarVagasCampanha_RN_CAM_068(model.Transform<VagasProcessoSeletivoOfertaData>());

                return RedirectSuccesConfigurarVagasToIndex(model.SeqProcessoSeletivo);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToIndexOfertaProcessoSeletivo(model.SeqProcessoSeletivo);
            }
        }

        #endregion Copiar Vagas da Campanha RN_CAM_068

        #region Usar vagas disponíveis na campanha RN_CAM_069

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult ConfirmarVagasDisponiveis(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            if (model == null || model.SeqProcessoSeletivo == 0 || model.gridListaOfertasProcessoSeletivo == null) { return null; }

            BuscarProcessoSeletivoOfertasSelecionados(model);

            var filtroOfertaCampanha = new CampanhaOfertaFiltroTelaData { SeqsCampanhaOfertas = model.ProcessoSeletivoOfertas.Select(x => x.SeqCampanhaOferta).ToArray() };

            var ofertasCampanha = CampanhaOfertaService.BuscarCampanhaOfertas(filtroOfertaCampanha);

            model.CampanhaOfertas = ofertasCampanha.TransformList<ConfigurarVagasOfertaCampanhaListaViewModel>();

            return PartialView("_ListarVagasDisponiveis", model);
        }

        [SMCAuthorize(UC_CAM_001_02_05.CONFIGURAR_VAGAS_OFERTAS_PROCESSO_SELETIVO)]
        public ActionResult UsarVagasDisponiveis(ConfigurarVagasOfertaProcessoSeletivoViewModel model)
        {
            try
            {
                BuscarProcessoSeletivoOfertasSelecionados(model);

                ProcessoSeletivoOfertaService.UsarVagasDisponiveis_RN_CAM_069(model.Transform<VagasProcessoSeletivoOfertaData>());

                return RedirectSuccesConfigurarVagasToIndex(model.SeqProcessoSeletivo);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return RedirectToIndexOfertaProcessoSeletivo(model.SeqProcessoSeletivo);
            }
        }

        #endregion Usar vagas disponíveis na campanha RN_CAM_069


        private void MenssagemSucesso()
        {
            SetSuccessMessage(Views.CampanhaOferta.App_LocalResources.UIResource.MSG_ConfigurarVagas_Sucesso,
                                  Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
        }

        private ActionResult RedirectSuccesConfigurarVagasToIndex(long seqProcessoSeletivo)
        {
            MenssagemSucesso();

            return RedirectToIndexOfertaProcessoSeletivo(seqProcessoSeletivo);
        }

        private ActionResult RedirectToIndexOfertaProcessoSeletivo(long seqProcessoSeletivo)
        {
            return SMCRedirectToAction("Index", routeValues: new { SeqProcessoSeletivo = new SMCEncryptedLong(seqProcessoSeletivo) });
        }

        private long BuscarSeqCampanha(long seqProcessoSeletivo)
        {
            var campanha = CampanhaService.BuscarCampanhaProcessoSeletivo(seqProcessoSeletivo);

            return campanha != null ? campanha.Seq : 0;
        }
    }
}