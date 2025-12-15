using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.AssinaturaDigital.Common.Areas.DOC.Enums;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class DocumentosAcademicoController : SMCApiControllerBase
    {
        #region Services
        private IDeclaracaoGenericaService DeclaracaoGenericaService => Create<IDeclaracaoGenericaService>();
        #endregion

        const string Usuario = "Integração GAD";
        public static string TokenAutorizacaoGAD => "VJvYKe84xUWRy9k7fk1QJSmb3ZXBpGsYDvTk59M9y4D7aOrAFmK673c7e6840c41";

        [Route("api/DocumentosAcademico/Atualizar")]
        [HttpPost]
        public ActionResult AtualizarDocumentoAcademico(CallbackDocumentoGadModel model)
        {
            HttpStatusCodeResult httpStatus = null;

            if (this.ValidarToken(model.Token, out httpStatus))
            {
                var generic = new GenericIdentity(Usuario, "manual");
                generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
                generic.AddClaim(new System.Security.Claims.Claim("Nome", Usuario));
                generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

                Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

                if (model.SeqDocumento.HasValue && model.StatusDocumento == StatusDocumento.Concluido ||
                    model.SeqDocumento.HasValue && model.StatusDocumento == StatusDocumento.Cancelado ||
                    model.SeqDocumento.HasValue && model.StatusDocumento == StatusDocumento.Recusado)
                    DeclaracaoGenericaService.AtualizarSituacaoDocumento(model.SeqDocumento.Value, model.StatusDocumento.Value);
            }

            return new HttpStatusCodeResult(httpStatus.StatusCode, httpStatus.StatusDescription);
        }

        private bool ValidarToken(string token, out HttpStatusCodeResult httpStatus)
        {
            if (string.IsNullOrEmpty(token))
            {
                httpStatus = new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                return false;
            }
            else
            {
                if (ValidarTokenCallback(token))
                {
                    httpStatus = new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized, "Token Inválido");
                    return false;
                }
                else
                {
                    httpStatus = new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
                    return true;
                }
            }
        }

        public static bool ValidarTokenCallback(string token)
        {
            return TokenAutorizacaoGAD != token;
        }
    }
}