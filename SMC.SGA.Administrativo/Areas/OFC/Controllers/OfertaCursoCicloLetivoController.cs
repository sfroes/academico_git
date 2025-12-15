using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.App_GlobalResources;
using SMC.SGA.Administrativo.Areas.CSO.Services;
using SMC.SGA.Administrativo.Areas.CUR.Services;
using SMC.SGA.Administrativo.Areas.OFC.Models;
using SMC.SGA.Administrativo.Areas.OFC.Services;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Areas.ORG.Services;
using SMC.SGA.Administrativo.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Controllers
{
    [SMCAllowAnonymous]
    public class OfertaCursoCicloLetivoController : SMCControllerBase
    {
        #region Serviços

        private IOfertaCursoCicloLetivoControllerService OfertaCursoCicloLetivoControllerService
        {
            get
            {
                return this.Create<IOfertaCursoCicloLetivoControllerService>();
            }
        }

        private INivelEnsinoControllerService NivelEnsinoControllerService
        {
            get
            {
                return this.Create<INivelEnsinoControllerService>();
            }
        }

        private IOFCDynamicControllerService OFCDynamicControllerService
        {
            get
            {
                return this.Create<IOFCDynamicControllerService>();
            }
        }

        private IORGDynamicControllerService ORGDynamicControllerService
        {
            get
            {
                return this.Create<IORGDynamicControllerService>();
            }
        }

        public ICSODynamicControllerService CSODynamicControllerService
        {
            get
            {
                return this.Create<ICSODynamicControllerService>();
            }
        }

        private ICurriculoControllerService CurriculoControllerService
        {
            get
            {
                return this.Create<ICurriculoControllerService>();
            }
        }

        private ICicloLetivoControllerService CicloLetivoControllerService
        {
            get
            {
                return this.Create<ICicloLetivoControllerService>();
            }
        }

        private ICursoControllerService CursoControllerService
        {
            get
            {
                return this.Create<ICursoControllerService>();
            }
        }
        
        #endregion

        #region Listar

        public ActionResult Index(OfertaCursoCicloLetivoFiltroViewModel filtros = null)
        {
            filtros.NiveisEnsino = SMCTreeView.For<NivelEnsinoItemArvoreViewModel>(NivelEnsinoControllerService.BuscarNiveisEnsino());

            filtros.FormasIngresso = OFCDynamicControllerService.BuscarFormasIngressoSelect();
            filtros.CiclosLetivos = CicloLetivoControllerService.BuscarCiclosLetivosSelect();

            return View(filtros);
        }

        public ActionResult ListarOfertaCursoCicloLetivo(OfertaCursoCicloLetivoFiltroViewModel filtros)
        {
            SMCPagerData<OfertaCursoCicloLetivoListaViewModel> data = OfertaCursoCicloLetivoControllerService.BuscarOfertasCursosCiclosLetivos(filtros);

            SMCPagerModel<OfertaCursoCicloLetivoListaViewModel> pager = new SMCPagerModel<OfertaCursoCicloLetivoListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarOfertaCursoCicloLetivo", pager);
        }

        #endregion

        #region Preenchimento de modelos

        [NonAction]
        private void PreencherModeloOfertaCursoCicloLetivo(OfertaCursoCicloLetivoViewModel modelo)
        {
            modelo.FormasIngresso = OFCDynamicControllerService.BuscarFormasIngressoSelect();

            if (modelo.SeqFormaIngressoSelecionada != null)
                modelo.TiposVinculo = OFCDynamicControllerService.BuscarTiposVinculoSelect(modelo.SeqFormaIngressoSelecionada.Value);
        }

        #endregion

        #region Selects com dependência

        [HttpPost]
        public JsonResult FormaIngressoSelecionado(string idReq, string idDep, string ValDep)
        {
            return Json(OFCDynamicControllerService.BuscarTiposVinculoSelect(long.Parse(ValDep)));
        }

        #endregion

        #region Incluir/Editar

        [HttpGet]
        public ActionResult Incluir()
        {
            OfertaCursoCicloLetivoViewModel modelo = new OfertaCursoCicloLetivoViewModel();

            PreencherModeloOfertaCursoCicloLetivo(modelo);

            return View(modelo);
        }

        [HttpGet]
        public ActionResult Editar(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {

            OfertaCursoCicloLetivoViewModel modelo = OfertaCursoCicloLetivoControllerService.BuscarOfertaCursoCicloLetivo(seqOfertaCursoCicloLetivo);

            PreencherModeloOfertaCursoCicloLetivo(modelo);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Salvar(OfertaCursoCicloLetivoViewModel modelo)
        {
            try
            {
                long seq;

                if (SalvarOfertaCursoCicloLetivo(modelo, out seq))
                    return RedirectToAction("Editar", new { seqEntidade = (SMCEncryptedLong)seq });
            }
            finally
            {
                PreencherModeloOfertaCursoCicloLetivo(modelo);
            }

            return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
        }

        [HttpPost]
        public ActionResult SalvarNovo(OfertaCursoCicloLetivoViewModel modelo)
        {
            try
            {
                long seq;
                if (SalvarOfertaCursoCicloLetivo(modelo, out seq))
                    return RedirectToAction("Incluir");
            }
            finally
            {
                PreencherModeloOfertaCursoCicloLetivo(modelo);
            }

            return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
        }

        [HttpPost]
        public ActionResult SalvarSair(OfertaCursoCicloLetivoViewModel modelo)
        {
            try
            {
                long seq;
                if (SalvarOfertaCursoCicloLetivo(modelo, out seq))
                    return RedirectToAction("Index");
            }
            finally
            {
                PreencherModeloOfertaCursoCicloLetivo(modelo);
            }

            return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
        }

        [NonAction]
        public bool SalvarOfertaCursoCicloLetivo(OfertaCursoCicloLetivoViewModel modelo, out long seq)
        {
            bool sucesso = this.Save(modelo, OfertaCursoCicloLetivoControllerService.SalvarOfertaCursoCicloLetivo, out seq);

            if (sucesso)
                SetSuccessMessage(string.Format(modelo.Seq == 0 ? MessagesResource.Mensagem_Sucesso_Inclusao_Registro :
                                                                  MessagesResource.Mensagem_Sucesso_Edicao_Registro,
                                                MessagesResource.Entidade_OfertaCursoCicloLetivo),
                                  MessagesResource.Titulo_Sucesso,
                                  SMCMessagePlaceholders.Centro);
            return sucesso;
        }

        #endregion

        #region Excluir

        [HttpPost]
        public ActionResult Excluir(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoControllerService.ExcluirOfertaCursoCicloLetivo(seqOfertaCursoCicloLetivo);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Exclusao_Registro,
                                            MessagesResource.Entidade_OfertaCursoCicloLetivo),
                              MessagesResource.Titulo_Sucesso,
                              SMCMessagePlaceholders.Centro);

            return RenderAction("ListarOfertaCursoCicloLetivo");
        }

        #endregion

        #region Associação de Curso/Unidade/Turno

        public ActionResult AssociacaoCursoUnidadeTurno(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel modelo)
        {
            OfertaCursoCicloLetivoDadosViewModel dadosOfertaCursoCicloLetivo = OfertaCursoCicloLetivoControllerService.BuscarDadosOfertaCursoCicloLetivo(modelo.SeqOfertaCursoCicloLetivo);

            modelo.DadosOfertaCursoCicloLetivo = dadosOfertaCursoCicloLetivo;

            modelo.Turnos = CSODynamicControllerService.BuscarTurnosSelect();
            modelo.Modalidades = CSODynamicControllerService.BuscarModalidadesSelect();;
            modelo.UnidadesLocalidades = ORGDynamicControllerService.BuscarUnidadesLocalidadesSelect();

            return View(modelo);
        }

        public ActionResult ListarAssociacaoCursoUnidadeTurno(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel filtros)
        {
            SMCPagerData<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoListaViewModel> data = OfertaCursoCicloLetivoControllerService.BuscarCursosUnidadesTurnos(filtros);

            SMCPagerModel<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoListaViewModel> pager = new SMCPagerModel<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarAssociacaoCursoUnidadeTurno", pager);
        }

        [HttpGet]
        public ActionResult IncluirAssociacaoCursoUnidadeTurno(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            var modelo = new OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel();

            modelo.Turnos = CSODynamicControllerService.BuscarTurnosSelect();
            modelo.Modalidades = CSODynamicControllerService.BuscarModalidadesSelect();;
            modelo.UnidadesLocalidades = ORGDynamicControllerService.BuscarUnidadesLocalidadesSelect();
            modelo.CurriculosPadrao = CurriculoControllerService.BuscarCurriculosSelect();
            modelo.Formacoes = CursoControllerService.BuscarFormacoesSelect(1);

            return PartialView("_AssociarCursoUnidadeTurno", modelo);
        }

        [HttpPost]
        public ActionResult IncluirAssociacaoCursoUnidadeTurno(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel modelo)
        {
            OfertaCursoCicloLetivoControllerService.SalvarAssociacaoCursoUnidadeTurno(modelo);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Inclusao_Associacao_Generica),
                              MessagesResource.Titulo_Sucesso,
                              SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoCursoUnidadeTurno", new OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel() { SeqOfertaCursoCicloLetivo = modelo.SeqOfertaCursoCicloLetivo });
        }

        [HttpGet]
        public ActionResult EditarAssociacaoCursoUnidadeTurno(SMCEncryptedLong seqAssociacaoCursoUnidadeTurno)
        {
            OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel modelo = OfertaCursoCicloLetivoControllerService.BuscarAssociacaoCursoUnidadeTurno(seqAssociacaoCursoUnidadeTurno);

            modelo.Turnos = CSODynamicControllerService.BuscarTurnosSelect();
            modelo.Modalidades = CSODynamicControllerService.BuscarModalidadesSelect();;
            modelo.UnidadesLocalidades = ORGDynamicControllerService.BuscarUnidadesLocalidadesSelect();
            modelo.CurriculosPadrao = CurriculoControllerService.BuscarCurriculosSelect();

            return PartialView("_AssociarCursoUnidadeTurno", modelo);
        }

        [HttpPost]
        public ActionResult EditarAssociacaoCursoUnidadeTurno(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoViewModel modelo)
        {
            OfertaCursoCicloLetivoControllerService.SalvarAssociacaoCursoUnidadeTurno(modelo);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Inclusao_Associacao_Generica),
                              MessagesResource.Titulo_Sucesso,
                              SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoCursoUnidadeTurno", new OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel() { SeqOfertaCursoCicloLetivo = modelo.SeqOfertaCursoCicloLetivo });
        }

        [HttpPost]
        public ActionResult ExcluirAssociacaoCursoUnidadeTurno(SMCEncryptedLong seqAssociacaoCursoUnidadeTurno, SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoControllerService.ExcluirAssociacaoCursoUnidadeTurno(seqAssociacaoCursoUnidadeTurno);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Exclusao_Associacao_Generica),
                              MessagesResource.Titulo_Sucesso,
                              SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoCursoUnidadeTurno", new OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoFiltroViewModel() { SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        #endregion

        #region Associação de Curso/Unidade/Turno Lote
		 
        public ActionResult AssociacaoCursoUnidadeTurnoLote(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteFiltroViewModel modelo)
        {
            modelo.Turnos = CSODynamicControllerService.BuscarTurnosSelect();
            modelo.Modalidades = CSODynamicControllerService.BuscarModalidadesSelect();;
            modelo.UnidadesLocalidades = ORGDynamicControllerService.BuscarUnidadesLocalidadesSelect();

            return View(modelo);
        }

        public ActionResult ListarAssociacaoCursoUnidadeTurnoLote(OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteFiltroViewModel filtros)
        {
            SMCPagerData<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteListaViewModel> data = OfertaCursoCicloLetivoControllerService.BuscarCursosUnidadesTurnosLote(filtros);

            SMCPagerModel<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteListaViewModel> pager = new SMCPagerModel<OfertaCursoCicloLetivoAssociacaoCursoUnidadeTurnoLoteListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarAssociacaoCursoUnidadeTurnoLote", pager);  
        }

        [HttpPost]
        public ActionResult AssociarCursoUnidadeTurnoLote(List<object> selectedValues, DateTime inicioOferta, DateTime fimOferta, SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoControllerService.SalvarAssociacaoCursoUnidadeTurnoLote(selectedValues, inicioOferta, fimOferta);

            SetSuccessMessage(MessagesResource.Mensagem_Sucesso_Inclusao_Associacao_Generica,
                              MessagesResource.Titulo_Sucesso,
                              SMCMessagePlaceholders.Centro);

            return RedirectToAction("AssociacaoCursoUnidadeTurno", new { seqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        #endregion

        #region Associação de Localidade

        public ActionResult AssociacaoLocalidade(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoDadosViewModel dadosOfertaCursoCicloLetivo = OfertaCursoCicloLetivoControllerService.BuscarDadosOfertaCursoCicloLetivo(seqOfertaCursoCicloLetivo);

            var modelo = new OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel()
            {
                SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo,
                DadosOfertaCursoCicloLetivo = dadosOfertaCursoCicloLetivo
            };

            return View(modelo);
        }

        public ActionResult ListarAssociacaoLocalidade(OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel filtros)
        {
            SMCPagerData<OfertaCursoCicloLetivoAssociacaoLocalidadeListaViewModel> data = OfertaCursoCicloLetivoControllerService.BuscarLocalidadesAssociadas(filtros);

            SMCPagerModel<OfertaCursoCicloLetivoAssociacaoLocalidadeListaViewModel> pager = new SMCPagerModel<OfertaCursoCicloLetivoAssociacaoLocalidadeListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarAssociacaoLocalidade", pager);
        }

        [HttpGet]
        public ActionResult IncluirAssociacaoLocalidade(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoAssociacaoLocalidadeViewModel modelo = new OfertaCursoCicloLetivoAssociacaoLocalidadeViewModel()
            {
                SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo
            };

            List<LocalidadeItemArvoreViewModel> localidades = ORGDynamicControllerService.BuscarLocalidades(); //TODO: Ajustar servico de retorno de itens de arvore de localidades
            modelo.Localidades = SMCTreeView.For<LocalidadeItemArvoreViewModel>(localidades).AllowCheck(prop => prop.ExibirCheckbox);

            return PartialView("_AssociarLocalidade", modelo);
        }

        [HttpPost]
        public ActionResult IncluirAssociacaoLocalidade(SMCEncryptedLong seqOfertaCursoCicloLetivo, SMCTreeViewModel<long> treeviewLocalidades)
        {
            OfertaCursoCicloLetivoControllerService.IncluirAssociacaoLocalidades(seqOfertaCursoCicloLetivo, treeviewLocalidades);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Associacao,
                                            MessagesResource.Entidade_Localidade,
                                            MessagesResource.Entidade_OfertaCursoCicloLetivo),
                                MessagesResource.Titulo_Sucesso,
                                SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoLocalidade", new OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel() { SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        [HttpPost]
        public ActionResult ExcluirAssociacaoLocalidade(SMCEncryptedLong seqOfertaCursoCicloLetivo, SMCEncryptedLong seqAssociacaoLocalidade)
        {
            OfertaCursoCicloLetivoControllerService.ExcluirAssociacaoLocalidade(seqAssociacaoLocalidade);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Exclusao_Associacao,
                                            MessagesResource.Entidade_Localidade,
                                            MessagesResource.Entidade_OfertaCursoCicloLetivo),
                                MessagesResource.Titulo_Sucesso,
                                SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoLocalidade", new OfertaCursoCicloLetivoAssociacaoLocalidadeFiltroViewModel() { SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        #endregion

        #region Associação de Polo

        public ActionResult AssociacaoPolo(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoDadosViewModel dadosOfertaCursoCicloLetivo = OfertaCursoCicloLetivoControllerService.BuscarDadosOfertaCursoCicloLetivo(seqOfertaCursoCicloLetivo);

            var modelo =  new OfertaCursoCicloLetivoAssociacaoPoloFiltroViewModel ( )
            {
                SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo,
                DadosOfertaCursoCicloLetivo = dadosOfertaCursoCicloLetivo
            };

            return View(modelo);
        }

        public ActionResult ListarAssociacaoPolo(OfertaCursoCicloLetivoAssociacaoPoloFiltroViewModel filtros)
        {
            SMCPagerData<OfertaCursoCicloLetivoAssociacaoPoloListaViewModel> data = OfertaCursoCicloLetivoControllerService.BuscarPolosAssociados(filtros);

            SMCPagerModel<OfertaCursoCicloLetivoAssociacaoPoloListaViewModel> pager = new SMCPagerModel<OfertaCursoCicloLetivoAssociacaoPoloListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarAssociacaoPolo", pager);
        }

        [HttpGet]
        public ActionResult IncluirAssociacaoPolo(SMCEncryptedLong seqOfertaCursoCicloLetivo)
        {
            OfertaCursoCicloLetivoAssociacaoPoloViewModel modelo = new OfertaCursoCicloLetivoAssociacaoPoloViewModel()
            {
                SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo
            };

            List<PoloItemArvoreViewModel> polos = ORGDynamicControllerService.BuscarPolos(); //TODO: Ajustar servico de retorno de itens de arvore de polos
            modelo.Polos = SMCTreeView.For<PoloItemArvoreViewModel>(polos).AllowCheck(prop => prop.ExibirCheckbox);

            return PartialView("_AssociarPolo", modelo);
        }

        [HttpPost]
        public ActionResult IncluirAssociacaoPolo(SMCEncryptedLong seqOfertaCursoCicloLetivo, SMCTreeViewModel<long> treeviewPolos)
        {
            OfertaCursoCicloLetivoControllerService.IncluirAssociacaoPolos(seqOfertaCursoCicloLetivo, treeviewPolos);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Associacao,
                                            MessagesResource.Entidade_Polo,
                                            MessagesResource.Entidade_OfertaCursoCicloLetivo),
                                MessagesResource.Titulo_Sucesso,
                                SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoPolo", new OfertaCursoCicloLetivoAssociacaoPoloFiltroViewModel() { SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        [HttpPost]
        public ActionResult ExcluirAssociacaoPolo(SMCEncryptedLong seqOfertaCursoCicloLetivo, SMCEncryptedLong seqAssociacaoPolo)
        {
            OfertaCursoCicloLetivoControllerService.ExcluirAssociacaoPolo(seqAssociacaoPolo);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Exclusao_Associacao,
                                            MessagesResource.Entidade_Polo,
                                            MessagesResource.Entidade_OfertaCursoCicloLetivo),
                                MessagesResource.Titulo_Sucesso,
                                SMCMessagePlaceholders.Centro);

            return RenderAction("ListarAssociacaoPolo", new OfertaCursoCicloLetivoAssociacaoPoloFiltroViewModel() { SeqOfertaCursoCicloLetivo = seqOfertaCursoCicloLetivo });
        }

        #endregion
    }
}