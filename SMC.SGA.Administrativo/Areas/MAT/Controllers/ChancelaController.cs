using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.MAT.Models;
using SMC.SGA.Administrativo.Areas.PES.Models;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.MAT.Controllers
{
    public class ChancelaController : SolicitacaoServicoFluxoBaseController
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService
        {
            get { return this.Create<IPessoaAtuacaoService>(); }
        }

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService
        {
            get { return this.Create<ISolicitacaoMatriculaService>(); }
        }

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService
        {
            get { return this.Create<ISolicitacaoMatriculaItemService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult CabecalhoPessoaAtuacao(long seqPessoaAtuacao)
        {
            // Recupera o tipo da pessoa atuação
            var modelHeader = ExecuteService<PessoaAtuacaoCabecalhoData, PessoaAtuacaoCabecalhoViewModel>(PessoaAtuacaoService.BuscarPessoaAtuacaoCabecalho, seqPessoaAtuacao);
            return PartialView("_Cabecalho", modelHeader);
        }

        public override ActionResult EntrarEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            try
            {
                return base.EntrarEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa, tokenRet);
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }

        public override ActionResult SelecaoChancelaPlanoEstudo(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            try
            {
                filtro.Orientador = true;

                return base.SelecaoChancelaPlanoEstudo(filtro);
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }

        [SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        public ActionResult Index()
        {
            ChancelaFiltroViewModel filtro = new ChancelaFiltroViewModel();
            return View(filtro);
        }

        [SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        public ActionResult Listar(ChancelaFiltroViewModel filtro)
        {
            var pager = ExecuteService<ChancelaFiltroData,
                               ChancelaItemListaData,
                               ChancelaFiltroViewModel,
                               ChancelaItemListaViewModel>(SolicitacaoMatriculaService.BuscarChancelas, filtro);

            return PartialView("_ChancelaItem", pager);
        }

        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult Editar(SMCEncryptedLong seq, SMCEncryptedLong seqProcesso, SMCEncryptedLong seqProcessoEtapa, bool? orientacao)
        {
            try
            {
                // Recupera os dados a serem chancelados pelo código
                var model = SolicitacaoMatriculaService.BuscarSolicitacaoMatriculaChancela(seq, MatriculaTokens.CHANCELA_PLANO_ESTUDO, orientacao).Transform<ChancelaViewModel>();
                              
                model.Orientacao = orientacao;
                // Retorna
                return View(model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, "Erro", SMCMessagePlaceholders.Centro);

                if (orientacao == true)
                    return RedirectToAction("Index", "Chancela");
                else
                    return RedirectToAction("Index", "SolicitacaoServico", new { @area = "SRC" });
            }
        }
               
        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult Salvar(ChancelaViewModel model)
        {
            try
            {
                SolicitacaoMatriculaService.SalvarChancelaMatricula(model.Transform<ChancelaData>(), MatriculaTokens.CHANCELA_PLANO_ESTUDO);
                SetSuccessMessage("Chancela efetuada com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                return RedirectToAction("Editar", new { @seq = new SMCEncryptedLong(model.Seq), @seqProcesso = new SMCEncryptedLong(model.SeqProcesso), @seqProcessoEtapa = new SMCEncryptedLong(model.SeqProcessoEtapa), @orientacao = model.Orientacao });
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, "Erro", SMCMessagePlaceholders.Centro);
                return RedirectToAction("Editar", new { @seq = new SMCEncryptedLong(model.Seq), @seqProcesso = new SMCEncryptedLong(model.SeqProcesso), @seqProcessoEtapa = new SMCEncryptedLong(model.SeqProcessoEtapa), @orientacao = model.Orientacao });
            }
        }

        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult ReabrirChancela(SMCEncryptedLong seqSolicitacaoServico)
        {
            try
            {
                long seqConfiguracaoEtapa = SolicitacaoMatriculaService.ReabrirChancelaMatricula(seqSolicitacaoServico);

                SetSuccessMessage("Chancela reaberta com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoServico = seqSolicitacaoServico, seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }

        [SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        public ActionResult Voltar(SMCEncryptedLong seqProcesso, SMCEncryptedLong seqProcessoEtapa, bool? orientacao)
        {
            if (orientacao == true)
                return RedirectToAction("Index", "Chancela");
            else
                return RedirectToAction("Index", "SolicitacaoServico", new { @area = "SRC" });
        }

        [SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        public ActionResult LimparProcessosFiltroChancela()
        {
            return SMCRedirectToAction("Index");
        }

        [SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        public ActionResult BuscarProcessosFiltroChancela(bool apenasProcessoVigente)
        {
            return Json(this.SolicitacaoMatriculaService.BuscarProcessosFiltroChancela(apenasProcessoVigente));
        }
    }
}