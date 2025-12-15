using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
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
    public class CampanhaOfertaController : SMCControllerBase
    {
        #region Services

        private ICampanhaService CampanhaService => Create<ICampanhaService>();

        private ICampanhaOfertaService CampanhaOfertaService => Create<ICampanhaOfertaService>();

        private ITipoOfertaService TipoOfertaService => Create<ITipoOfertaService>();

        private IProcessoSeletivoService ProcessoSeletivoService => Create<IProcessoSeletivoService>();

        #endregion Services

        #region Listar / cabeçalho

        [SMCAuthorize(UC_CAM_001_01_03.PESQUISAR_OFERTA_CAMPANHA)]
        public ActionResult Index(CampanhaOfertaFiltroViewModel filtro)
        {
            if (filtro.SeqCampanha == null) { return RedirectToIndex(); }

            filtro.TiposOfertas = TipoOfertaService.BuscarTiposOfertaDaCampanhaSelect(filtro.SeqCampanha.Value)
                                        .TransformList<SMCSelectListItem>();

            return View(filtro);
        }

        [SMCAuthorize(UC_CAM_001_01_03.PESQUISAR_OFERTA_CAMPANHA)]
        public ActionResult ListarCampanhaOfertas(CampanhaOfertaFiltroViewModel filtro)
        {
            var filtroData = filtro.Transform<CampanhaOfertaFiltroTelaData>();
            filtroData.SeqCampanha = filtro.SeqCampanha;
            var data = CampanhaOfertaService.BuscarCampanhaOfertas(filtroData);

            var lista = data.TransformList<CampanhaOfertaListaViewModel>();

            return PartialView("_ListarCampanhaOfertas", lista);
        }

        [SMCAllowAnonymous]
        [ChildActionOnly]
        public ActionResult CabecalhoCampanhaOferta(long seqCampanha)
        {
            if (seqCampanha == 0) { return RedirectToIndex(); }

            var campanha = CampanhaService.BuscarCampanha(seqCampanha);
            var model = new CampanhaOfertaCabecalhoViewModel
            {
                SeqCampanha = seqCampanha,
                Campanha = campanha.Descricao,
                CiclosLetivos = campanha.CiclosLetivos.Select(f => f.Descricao).ToList()
            };
            return PartialView("_CabecalhoCampanhaOferta", model);
        }

        #endregion Listar / cabeçalho

        #region Associar oferta a campanha

        [SMCAuthorize(UC_CAM_001_01_04.ASSOCIAR_OFERTA_CAMPANHA)]
        public ActionResult Incluir(SMCEncryptedLong seqCampanha)
        {
            if (seqCampanha == null) { return RedirectToIndex(); }
            AssociarCampanhaOfertaViewModel model = new AssociarCampanhaOfertaViewModel()
            {
                SeqCampanha = seqCampanha,
                ProcessosSeletivos = ProcessoSeletivoService.BuscarProcessosSeletivosConvocacao(seqCampanha)
                                                            .TransformList<ProcessoSeletivoViewModel>(),
            };

            model.ProcessosSeletivosTree = TransFormFlatTreeview(model.ProcessosSeletivos);

            return View(model);
        }

        [SMCAuthorize(UC_CAM_001_01_04.ASSOCIAR_OFERTA_CAMPANHA)]
        public ActionResult AssociarCampanhaOferta(AssociarCampanhaOfertaViewModel model)
        {
            try
            {
                if (model.SeqCampanha == 0 || !model.OfertasCampanha.SMCAny()) { return RedirectToIndex(); }

                VincularProcessosConvocacoesSelecionados(model);

                var data = model.Transform<CampanhaOfertaAssociacaoData>();

                CampanhaOfertaService.AssociarCampanhaOferta(data);

                return RedirectSuccesAssociarOfertaToIndexCampanha(data.SeqCampanha);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        [SMCAllowAnonymous]
        public ActionResult Excluir(SMCEncryptedLong seq)
        {
            try
            {
                var seqCampanha = CampanhaOfertaService.Excluir(seq);
                return RedirectSuccesExcluirOfertaCampanhaToIndexCampanha(seqCampanha);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Request.UrlReferrer.ToString());
            }
        }

        #endregion Associar oferta a campanha

        #region Configurar Vagas

        [SMCAuthorize(UC_CAM_001_01_05.CONFIGURAR_VAGAS_OFERTAS_CAMPANHA)]
        public ActionResult ConfigurarVagas(ConfigurarVagasOfertaCampanhaViewModel model)
        {
            if (model == null || model.SeqCampanha == 0 || model.gridCampanhaOferta == null) { return null; }

            var filtro = new CampanhaOfertaFiltroTelaData { SeqCampanha = model.SeqCampanha, SeqsCampanhaOfertas = model.gridCampanhaOferta.ToArray() };

            var data = CampanhaOfertaService.BuscarCampanhaOfertas(filtro);

            model.CampanhaOfertas = data.TransformList<ConfigurarVagasOfertaCampanhaListaViewModel>();

            ///Caso exista, os processos seletivos serão listados
            model.ProcessosSeletivos = ProcessoSeletivoService.BuscarProcessosSeletivosConvocacao(model.SeqCampanha)
                                                              .TransformList<ProcessoSeletivoViewModel>();

            model.ProcessosSeletivosTree = TransFormFlatTreeview(model.ProcessosSeletivos);

            return View(model);
        }

        [SMCAuthorize(UC_CAM_001_01_05.CONFIGURAR_VAGAS_OFERTAS_CAMPANHA)]
        public ActionResult SalvarVagasCampanhaOferta(ConfigurarVagasOfertaCampanhaViewModel model, List<ConfigurarVagasOfertaCampanhaListaViewModel> campanhaOfertas)
        {
            model.CampanhaOfertas = campanhaOfertas;

            VincularProcessosConvocacoesSelecionados(model);

            //Salvar as vagas para a campanhaOferta
            CampanhaOfertaService.SalvarVagasCampanhaOferta(model.Transform<VagasCampanhaOfertaData>());

            return RedirectSuccesConfigurarVagasToIndexCampanha(model.SeqCampanha);
        }

        private ActionResult RedirectSuccesExcluirOfertaCampanhaToIndexCampanha(long seqCampanha)
        {
            MenssagemSucessoExcluirOfertacampanha();

            return SMCRedirectToAction("Index", routeValues: new { SeqCampanha = new SMCEncryptedLong(seqCampanha) });
        }

        #endregion Configurar Vagas

        #region Métodos Privados

        /// <summary>
        /// Método que Vincula os Seqs de Processos seletivos
        /// e Convocações selecionados, ao modelo
        /// </summary>
        /// <param name="model"></param>
        private void VincularProcessosConvocacoesSelecionados(AssociarCampanhaOfertaViewModel model)
        {
            if (model.ProcessosSelecionados.SMCAny())
            {
                //Todos Seqs da convocação negativos
                model.SeqsConvocacoes = BuscarSeqsNegativos(model.ProcessosSelecionados);

                //Seqs dos Processos Seletivos positivos
                model.SeqsProcessosSeletivos = BuscarSeqsPositivos(model.ProcessosSelecionados);
            }
        }

        /// <summary>
        /// Método que Vincula os Seqs de Processos seletivos
        /// e Convocações selecionados, ao modelo
        /// </summary>
        /// <param name="model"></param>
        private void VincularProcessosConvocacoesSelecionados(ConfigurarVagasOfertaCampanhaViewModel model)
        {
            if (model.ProcessosSelecionados != null && model.ProcessosSelecionados.Any())
            {
                //Todos Seqs da convocação negativos
                model.SeqsConvocacoes = BuscarSeqsNegativos(model.ProcessosSelecionados);

                //Seqs dos Processos Seletivos positivos
                model.SeqsProcessosSeletivos = BuscarSeqsPositivos(model.ProcessosSelecionados);
            }
        }

        private long[] BuscarSeqsPositivos(SMCSelectedValue<long>[] processosSelecionados)
        {
            return processosSelecionados.Where(c => c.Value > 0).Select(x => Math.Abs(x.Value)).ToArray();
        }

        private long[] BuscarSeqsNegativos(SMCSelectedValue<long>[] processosSelecionados)
        {
            return processosSelecionados.Where(c => c.Value < 0).Select(x => Math.Abs(x.Value)).ToArray();
        }

        private List<SMCTreeViewNode<ProcessoSeletivoListarViewModel>> TransFormFlatTreeview(List<ProcessoSeletivoViewModel> processosSeletivos)
        {
            if (processosSeletivos == null || !processosSeletivos.Any()) { return null; }

            var processos = new List<ProcessoSeletivoListarViewModel>();

            foreach (var processo in processosSeletivos)
            {
                var processoTabular = new ProcessoSeletivoListarViewModel();
                processoTabular.Seq = processo.Seq.Value;
                processoTabular.Descricao = processo.Descricao;
                processos.Add(processoTabular);

                foreach (var convocacao in processo.Convocacoes)
                {
                    processoTabular = new ProcessoSeletivoListarViewModel();
                    //Seqs da convocação terão sinais invertidos, para
                    //diferenciá-los dos seqs dos Processos Seletivos
                    processoTabular.Seq = (-1) * Math.Abs(convocacao.Seq.Value);
                    processoTabular.SeqPai = processo.Seq.Value;
                    processoTabular.Descricao = convocacao.Descricao;

                    processos.Add(processoTabular);
                }
            }

            return SMCTreeView.For(processos);
        }

        private void MenssagemSucesso()
        {
            SetSuccessMessage(Views.CampanhaOferta.App_LocalResources.UIResource.MSG_ConfigurarVagas_Sucesso,
                                  Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
        }

        private ActionResult RedirectSuccesConfigurarVagasToIndexCampanha(long seqCampanha)
        {
            MenssagemSucesso();

            return SMCRedirectToAction("Index", routeValues: new { SeqCampanha = new SMCEncryptedLong(seqCampanha) });
        }

        private ActionResult RedirectSuccesAssociarOfertaToIndexCampanha(long seqCampanha)
        {
            MenssagemSucessoAssociarOferta();

            return SMCRedirectToAction("Index", routeValues: new { SeqCampanha = new SMCEncryptedLong(seqCampanha) });
        }

        private void MenssagemSucessoAssociarOferta()
        {
            SetSuccessMessage(Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Associacao_Sucesso,
                                  Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
        }

        private void MenssagemSucessoExcluirOfertacampanha()
        {
            SetSuccessMessage(Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Excluir_Oferta_Campanha_Sucesso,
                                  Views.CampanhaOferta.App_LocalResources.UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Centro);
        }

        /// <summary>
        /// Redireciona o usuário para a index
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns></returns>
        private ActionResult RedirectToIndex()
        {
            return RedirectToAction("Index", "Campanha");
        }

        #endregion Métodos Privados
    }
}