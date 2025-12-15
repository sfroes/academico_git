using SMC.Academico.Common.Areas.ORT.Enums;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using SMC.SGA.BDP.Models;
using SMC.SGA.BDP.Views.Home.App_LocalResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.BDP.Controllers
{
    public class HomeController : SMCControllerBase
    {
        #region [ Services ]

        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        private IClassificacaoService ClassificacaoService => Create<IClassificacaoService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IInstituicaoEnsinoService InstituicaoEnsinoService => Create<IInstituicaoEnsinoService>();

        private INotificacaoService NotificacaoService => Create<INotificacaoService>();

        private ITipoTrabalhoService TipoTrabalhoService => Create<ITipoTrabalhoService>();

        private ITrabalhoAcademicoService TrabalhoAcademicoService => Create<ITrabalhoAcademicoService>();

        #endregion

        #region [ Overrides ]

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var instituicaoLogada = ControllerContext.GetInstituicaoLogada();

            if (instituicaoLogada == null || instituicaoLogada.Value == 0)
            {
                var filterKey = ControllerContext.RouteData.Values["instituicao"]?.ToString();
                var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsinoPorSigla(filterKey);
                if (instituicao == null)
                    throw new HttpException(404, "Página não encontrada");

                this.SetCookie(new SMCEntityFilterGlobalModel()
                {
                    FilterKey = filterKey,
                    Description = instituicao.Nome,
                    Value = instituicao.Seq,
                    SeqArquivoLogo = instituicao.SeqArquivoLogotipo,
                });
            }
        }

        #endregion

        [SMCAllowAnonymous]
        public FileResult Logo(SMCEncryptedLong seq)
        {
            var arquivo = ArquivoAnexadoService.BuscarArquivoAnexado(seq);
            return File(arquivo.FileData, arquivo.Type);
        }

        /// <summary>
        /// Página Home
        /// </summary> 
        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            var seqInstituicaoLogada = ControllerContext.GetSeqInstituicaoLogada();

             //Repetindo código do OnActionExcuting - existia um bug que o cookie setado no método acima não estava buscando nos métodos do SGABDPExtensions
             //Dessa forma, buscando pela instituição, não teremos mais o problema do seqInstituicaoLogada vir zerado.
            if (seqInstituicaoLogada == 0)
            {
                var filterKey = ControllerContext.RouteData.Values["instituicao"]?.ToString();
                var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsinoPorSigla(filterKey);
                if (instituicao == null)
                    throw new HttpException(404, "Página não encontrada");

                seqInstituicaoLogada = instituicao.Seq;
            }

            var model = new PesquisaViewModel()
            {
                TiposTrabalho = TipoTrabalhoService.BuscarTiposTrabalhoSelectBDP(seqInstituicaoLogada).TransformList<SMCDatasourceItem>(),
                AreasConhecimento = ClassificacaoService.BuscarClassificacoesBDP().TransformList<SMCDatasourceItem>(),
                Programas = EntidadeService.BuscarEntidadesSelect(TOKEN_TIPO_ENTIDADE.GRUPO_PROGRAMA, seqInstituicaoLogada),
                TiposPesquisaTrabalho = TipoTrabalhoService.BuscarTipoPesquisaTrabalhoAcademicoOrder()
            };

            return View(model);
        }

        [SMCAllowAnonymous]
        public ActionResult Pesquisar(PesquisaViewModel filtro)
        {
            filtro.SeqInstituicaoLogada = ControllerContext.GetSeqInstituicaoLogada();
            filtro.FiltrosDescricao = new List<string>();
            SMCPagerData<TrabalhoAcademicoListaData> data = new SMCPagerData<TrabalhoAcademicoListaData>();
            SMCPagerModel<ResultadoViewModel> model;

            if (filtro.EmPublicacao == true)
            {
                ViewBag.Title = $" {UIResource.Pesquisa_Filtro_EmPublicacao}";

                data = TrabalhoAcademicoService.BuscarTrabalhoAcademicosBDP(filtro.Transform<TrabalhoAcademicoFiltroData>());
            }

            if (filtro.EmFuturasDefesas == true)
            {
                ViewBag.Title = $" {UIResource.Pesquisa_Filtro_FuturasDefesas}";

                data = TrabalhoAcademicoService.BuscarTrabalhoFuturasDefesasAcademicosBDP(filtro.Transform<TrabalhoAcademicoFiltroData>());
            }
            
            model = new SMCPagerModel<ResultadoViewModel>(data.Transform<SMCPagerData<ResultadoViewModel>>(), filtro.PageSettings, filtro);
            
            return View("Pesquisar", model);
        }

        [SMCAllowAnonymous]
        public ActionResult PesquisaDetalhada(PesquisaViewModel filtro)
        {
            filtro.SeqInstituicaoLogada = ControllerContext.GetSeqInstituicaoLogada();

            //Quando o filtro for pesquisa detalhada obrigatório informar um dos campos de pesquisa
            if (!filtro.SeqAreaConhecimento.HasValue && 
                !filtro.SeqPrograma.HasValue && 
                !filtro.SeqsTipoTrabalho.SMCAny() && 
                !filtro.DataInicio.HasValue && 
                !filtro.DataFim.HasValue &&
                !filtro.TipoPesquisaTrabalho.SMCAny()&&
                string.IsNullOrEmpty(filtro.Nome) &&
                string.IsNullOrEmpty(filtro.TituloResumo))
            {
                SetErrorMessage(UIResource.Pesquisar_Consulta_Erro, target: SMCMessagePlaceholders.Centro);
                return null;
            }

            var data = TrabalhoAcademicoService.BuscarTrabalhoAcademicosBDP(filtro.Transform<TrabalhoAcademicoFiltroData>());
            var model = new SMCPagerModel<ResultadoViewModel>(data.Transform<SMCPagerData<ResultadoViewModel>>(), filtro.PageSettings, filtro);

            return PartialView("_ListarResultados", model);
        }

        [SMCAllowAnonymous]
        public ActionResult InstrucoesPublicacao()
        {
            var model = new PesquisaViewModel();
            model.DescricaoInstituicaoLogada = ControllerContext.GetInstituicaoLogada().Description;
            return View(model);
        }

        public ActionResult ListarResultados(PesquisaViewModel filtro)
        {
            try
            {
                //O cabeçalho ja foi montado no inicio não sendo necessário
                //Remover o filtro descrição porque quando contém html <label> na descrição barra o post da propriedade por segurança
                filtro.FiltrosDescricao = null;
                SMCPagerData<TrabalhoAcademicoListaData> data = new SMCPagerData<TrabalhoAcademicoListaData>();

                if (filtro.EmFuturasDefesas.GetValueOrDefault())
                {
                    data = TrabalhoAcademicoService.BuscarTrabalhoFuturasDefesasAcademicosBDP(filtro.Transform<TrabalhoAcademicoFiltroData>());
                }
                else
                {
                    data = TrabalhoAcademicoService.BuscarTrabalhoAcademicosBDP(filtro.Transform<TrabalhoAcademicoFiltroData>());
                }
                
                var model = new SMCPagerModel<ResultadoViewModel>(data.Transform<SMCPagerData<ResultadoViewModel>>(), filtro.PageSettings, filtro);

                //Só exibe o nome do coorientador na pesquisa quando a mesma é filtrada por ele
                if (filtro.ExibirCoorientador)
                    model.SMCForEach(f => f.ExibirCoorientador = true);

                return PartialView("_ListarResultados", model);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex, target: SMCMessagePlaceholders.Centro);
                return null;
            }
        }

        [SMCAllowAnonymous]
        public ActionResult Visualizar(SMCEncryptedLong seq)
        {
            var data = TrabalhoAcademicoService.VisualizarTrabalhoAcademico(seq);
            if (data == null)
            {
                SetErrorMessage("Trabalho não encontrado!");
                return null;
            }
            var model = data.Transform<TrabalhoAcademicoViewModel>();
            ViewBag.Title = model.ViewTitle;
            return PartialView("_Visualizar", model);
        }


        public ActionResult Contato(ContatoViewModel model)
        {
            try
            {
                var identificacao = @"SGA\Contato BDP";
                var generic = new GenericIdentity(identificacao, "manual");
                generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
                generic.AddClaim(new System.Security.Claims.Claim("Nome", identificacao));
                generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA"));

                Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

                var instituicao = InstituicaoEnsinoService.BuscarInstituicaoEnsino(ControllerContext.GetSeqInstituicaoLogada());
                NotificacaoService.EnviarEmail(new EmailData()
                {
                    Assunto = model.Assunto,
                    Mensagem = model.Message,
                    NomeRemetente = model.Nome,
                    EmailRemetente = model.Email,
                    EmailResposta = model.Email,
                    Destinatarios = new List<EmailDestinatarioData>()
                    {
                        new EmailDestinatarioData() { Email = instituicao.EmailContatoBdp }
                    }
                });

                SetSuccessMessage(UIResource.Mensagem_Email_Sucesso);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message);
            }

            return RedirectToAction("Index");
        }
    }
}