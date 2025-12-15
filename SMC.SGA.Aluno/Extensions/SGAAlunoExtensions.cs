using Newtonsoft.Json;
using SMC.Academico.Common.Constants;
using SMC.Framework.Extensions;
using SMC.Framework.Ioc;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Aluno.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SMC.Academico.UI.Mvc.Areas.PES.Controllers;
using SMC.Academico.UI.Mvc.Areas.PES.Models.SuporteTecnico;

namespace SMC.SGA.Aluno.Extensions
{
    public static class SGAAlunoExtensions
    {
        /// <summary>
        /// Buscao aluno do usuário logado
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>Aluno do usuário logado</returns>
        public static AlunoSelecionadoViewModel GetAlunoLogado(this HtmlHelper html)
        {
            return GetAlunoLogado(html.ViewContext.HttpContext);
        }

        /// <summary>
        /// Buscao aluno do usuário logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Aluno do usuário logado</returns>
        public static AlunoSelecionadoViewModel GetAlunoLogado(this SMCControllerBase controller)
        {
            return GetAlunoLogado(controller.HttpContext);
        }

        /// <summary>
        /// Buscao aluno do usuário logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Aluno do usuário logado</returns>
        public static AlunoSelecionadoViewModel GetAlunoLogado(this HttpContextBase httpContext)
        {
            try
            {
                AlunoSelecionadoViewModel alunoSelecionado = new AlunoSelecionadoViewModel
                {
                    Seq = httpContext.GetEntityLogOn(FILTER.ALUNO).Value,
                    Descricao = httpContext.GetEntityLogOn(FILTER.ALUNO).Description,
                    SeqArquivoLogo = httpContext.GetEntityLogOn(FILTER.ALUNO).SeqArquivoLogo
                };
                return alunoSelecionado;
            }
            catch (Exception ex)
            {
                return new AlunoSelecionadoViewModel();
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