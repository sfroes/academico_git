using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Ioc;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Aluno.Areas.SRC.Filters
{
    public class SGAVerificarUsuarioSolicitacaoServicoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Verifica se existe o sequencial da solicitação nos parâmetros da request
            long? seqSolicitacao = RecuperarSeqSolicitacaoServicoRequest(filterContext);
            bool logado = false;

            if (seqSolicitacao.HasValue)
            {
                var seqUsuarioLogado = filterContext.RequestContext.HttpContext.User.SMCGetSequencialUsuario();

                // Verifica se o usuário da requisição é o mesmo usuário dono da solicitação
                var verificado = filterContext.RequestContext.HttpContext.Session["__SMC_KEY_SESSION_USUARIO_VERIFICADO"] as Dictionary<long, long> ?? new Dictionary<long, long>();

                if (verificado.ContainsKey(seqSolicitacao.Value) && verificado[seqSolicitacao.Value] == seqUsuarioLogado)
                    logado = true;
                else
                {
                    var seqUsuarioSAS = RecuperarUsuarioSolicitacao(seqSolicitacao.GetValueOrDefault());
                    if (seqUsuarioLogado == seqUsuarioSAS)
                    {
                        verificado.Add(seqSolicitacao.GetValueOrDefault(), seqUsuarioSAS);
                        filterContext.RequestContext.HttpContext.Session["__SMC_KEY_SESSION_USUARIO_VERIFICADO"] = verificado;
                        logado = true;
                    }
                }

                if (!logado)
                    throw new UnauthorizedAccessException();
            }
        }

        private long? RecuperarSeqSolicitacaoServicoRequest(ActionExecutingContext filterContext)
        {
            foreach (var key in filterContext.RequestContext.HttpContext.Request.Params.AllKeys)
            {
                if (key.ToLower() == "seqsolicitacaoservico")
                {
                    long ret = 0;
                    if (long.TryParse(filterContext.RequestContext.HttpContext.Request.Params[key], out ret))
                        return ret;

                    return new SMCEncryptedLong(filterContext.RequestContext.HttpContext.Request.Params[key]);
                }

                if (key.ToLower() == "seqsolicitacaomatricula")
                {
                    long ret = 0;
                    if (long.TryParse(filterContext.RequestContext.HttpContext.Request.Params[key], out ret))
                        return ret;

                    return new SMCEncryptedLong(filterContext.RequestContext.HttpContext.Request.Params[key]);
                }
            }

            return null;
        }

        private long RecuperarUsuarioSolicitacao(long seqSolicitacao)
        {
            long seqUsuarioSolicitacao = 0;
            using (var manager = new SMCContainerManager())
            {
                using (var solicitacaoMatriculaService = manager.Create<ISolicitacaoServicoService>())
                {
                    seqUsuarioSolicitacao = solicitacaoMatriculaService.BuscarSeqUsuarioSASSolicitacaoServico(seqSolicitacao);
                }
            }

            return seqUsuarioSolicitacao;
        }
    }
}