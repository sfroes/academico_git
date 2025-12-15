using SMC.Academico.Common.Constants;
using SMC.Framework.DataFilters;
using SMC.Framework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMC.SGA.BDP
{
    public static class SGABDPExtensions
    {
        public static long GetSeqInstituicaoLogada(this ControllerContext context)
        {
            var cookie = GetInstituicaoLogada(context);
            if (cookie != null)
                return cookie.Value;
            return 0;
        }

        public static SMCEntityFilterGlobalModel GetInstituicaoLogada(this ControllerContext context)
        {
            var filter = context.RouteData.Values["instituicao"]?.ToString();
            var entity = context.HttpContext.GetEntityLogOn(filter);
            context.HttpContext.Items["entity"] = entity;
            return entity;
        }

        public static string GetDescricaoInstituicaoLogada(this HttpContextBase context)
        {
            return (context.Items["entity"] as SMCEntityFilterGlobalModel).Description;
        }

        public static long? GetSeqArquivoInstituicaoLogada(this HttpContextBase context)
        {
            return (context.Items["entity"] as SMCEntityFilterGlobalModel).SeqArquivoLogo;
        }
    }
}