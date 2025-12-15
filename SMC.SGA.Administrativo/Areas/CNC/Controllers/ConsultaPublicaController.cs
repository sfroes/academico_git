using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CNC.Models;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    [SMCAllowAnonymous, AllowAnonymous]
    public class ConsultaPublicaController : SMCControllerBase
    {
        #region [ Services ]

        private IDocumentoConclusaoService DocumentoConclusaoService => Create<IDocumentoConclusaoService>();

        #endregion [ Services ]

        public ActionResult Index(ConsultaPublicaFiltrarViewModel filtro)
        {
            return View(filtro);
        }

        public ActionResult Visualizar(ConsultaPublicaFiltrarViewModel filtro)
        {
            if (!SMCCaptcha.IsValidCaptcha(HttpContext) && !filtro.ExibirApenasConsulta)
            {
                SetErrorMessage("Captcha inválido!", target: SMCMessagePlaceholders.Centro);
                return SMCRedirectToAction("Index", routeValues: new { codigoVerificacao = filtro.CodigoVerificacao, nomeCompletoDiplomado = filtro.NomeCompletoDiplomado });
            }

            var retorno = new ConsultaPublicaViewModel();
            try
            {
                retorno = DocumentoConclusaoService.ValidarCodigoVerificacaoDiploma(filtro.CodigoVerificacao, filtro.NomeCompletoDiplomado).Transform<ConsultaPublicaViewModel>();
                retorno.Titulacao = string.IsNullOrEmpty(retorno.DescricaoTitulacaoXSD) ? retorno.DescricaoTitulacao : retorno.DescricaoTitulacaoXSD;

                retorno.CodigoVerificacao = filtro.CodigoVerificacao;
                retorno.DataConsulta = DateTime.Now;

                retorno.ExibirApenasConsulta = filtro.ExibirApenasConsulta;

                retorno.DataRegistroDOUFormatado = retorno.DataRegistroDOU.HasValue ? retorno.DataRegistroDOU.Value.ToString("dd/MM/yyyy") : "Aguardando publicação";

                return PartialView("_Consulta", retorno);
            }
            catch (Exception ex)
            {
                retorno.CodigoVerificacao = filtro.CodigoVerificacao;
                retorno.DataConsulta = DateTime.Now;
                retorno.Valido = false;
                retorno.SituacaoDiploma = "Diploma digital não localizado";
                retorno.Mensagem = ex.Message;

                return PartialView("_Consulta", retorno);
            }
        }

    }
}