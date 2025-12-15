using SMC.Academico.Common.Constants;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Extensions
{
    public static class SGAAdministrativoExtensions
    {
        /// <summary>
        /// Busca a instituição de ensino do usuário logado
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>Instituição de ensino do usuário logado</returns>
        public static InstituicaoSelecionadaViewModel GetInstituicaoEnsinoLogada(this HtmlHelper html)
        {
            return GetInstituicaoEnsinoLogada(html.ViewContext.HttpContext);
        }

        /// <summary>
        /// Busca a instituição de ensino do usuário logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Instituição de ensino do usuário logado</returns>
        public static InstituicaoSelecionadaViewModel GetInstituicaoEnsinoLogada(this SMCControllerBase controller)
        {
            return GetInstituicaoEnsinoLogada(controller.HttpContext);
        }

        /// <summary>
        /// Busca a instituição de ensino do usuário logado
        /// </summary>
        /// <param name="html">Html Context</param>
        /// <returns>Instituição de ensino do usuário logado</returns>
        public static InstituicaoSelecionadaViewModel GetInstituicaoEnsinoLogada(this HttpContextBase httpContext)
        {
            try
            {
                InstituicaoSelecionadaViewModel instituicaoSelecionada = new InstituicaoSelecionadaViewModel();
                instituicaoSelecionada.Seq = httpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Value;
                instituicaoSelecionada.Sigla = httpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).Description;
                instituicaoSelecionada.SeqArquivoLogo = httpContext.GetEntityLogOn(FILTER.INSTITUICAO_ENSINO).SeqArquivoLogo;
                return instituicaoSelecionada;
            }
            catch (Exception ex)
            {
                return new InstituicaoSelecionadaViewModel();
            }
        }
    }
}