using SMC.Academico.Common.Constants;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Professor.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Professor.Extensions
{
    public static class SGAProfessorExtensions
    {
        /// <summary>
        /// Buscao professor logado
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>Professor logado</returns>
        public static ProfessorSelecionadoViewModel GetProfessorLogado(this HtmlHelper html)
        {
            return GetProfessorLogado(html.ViewContext.HttpContext);
        }

        /// <summary>
        /// Buscao professor logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Professor logado</returns>
        public static ProfessorSelecionadoViewModel GetProfessorLogado(this SMCControllerBase controller)
        {
            return GetProfessorLogado(controller.HttpContext);
        }

        /// <summary>
        /// Buscao professor logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Professor logado</returns>
        public static ProfessorSelecionadoViewModel GetProfessorLogado(this HttpContextBase httpContext)
        {
            try
            {
                ProfessorSelecionadoViewModel professorSelecionado = new ProfessorSelecionadoViewModel
                {
                    Seq = httpContext.GetEntityLogOn(FILTER.PROFESSOR).Value,
                    Descricao = httpContext.GetEntityLogOn(FILTER.PROFESSOR).Description,
                    SeqArquivoLogo = httpContext.GetEntityLogOn(FILTER.PROFESSOR).SeqArquivoLogo
                };
                return professorSelecionado;
            }
            catch (Exception ex)
            {
                return new ProfessorSelecionadoViewModel();
            }
        }                       

        /// <summary>
        /// Menu suspenso opção Ajuda
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Professor logado</returns>
        public static MvcHtmlString SGAHelpWindow(this SMCHtmlHelper smcHelper)
        {
            try
            {
                // Cria HtmlHelper fake
                HtmlHelper helperFake = SMCHtmlHelper.Create(smcHelper);
                helperFake.ViewContext.Writer = new StringWriter();
                SMCHtmlHelper smcHelperFake = new SMCHtmlHelper(helperFake);

                // Cria o ID para janela
                var idJanela = Guid.NewGuid().ToString();

                // Cria a janela
                var janelaContexto = smcHelperFake.ContextWindow(idJanela).CssClass("smc-janela-ajuda").HideCloseButton().OpenButtonCss("smc-usuario-help");

                // inicia a janela
                using (janelaContexto.Begin())
                {
                    // Cria o ButtonSet
                    var buttonSet = smcHelperFake.ButtonSet();

                    var botaoCredenciais = smcHelperFake.Button("smc-btn-credenciais").CssClass("smc-btn-credenciais").Action("Index", "CredenciaisAcessoRoute", new { @area = "" }).Type(Framework.SMCButtonType.Link).Text("Credenciais de Acesso").Tooltip("Credenciais de Acesso");
                    buttonSet.AddButton(botaoCredenciais);

                    var botaoSuporte = smcHelperFake.Button("smc-btn-suporte").CssClass("smc-btn-suporte").Action("IntegracaoCSC", "SuporteTecnicoRoute", new { @area = "" }).Type(Framework.SMCButtonType.Link).Text("Suporte Técnico").Tooltip("Suporte Técnico");
                    botaoSuporte.Attribute("target", "_blank");
                    buttonSet.AddButton(botaoSuporte);

                    // Escreve o buttonset
                    helperFake.ViewContext.Writer.Write(buttonSet.ToHtmlString());
                }

                return new MvcHtmlString(helperFake.ViewContext.Writer.ToString());                

            }
            catch (Exception)
            {
                return new MvcHtmlString("");
            }
        }
    }
}