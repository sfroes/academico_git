using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Academico.WebApi.Models;
using SMC.Framework.Service.Security;
using SMC.Framework.UI.Mvc;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;

namespace SMC.Academico.WebApi.Controllers
{
    public class DocumentosAcademicoXMLController : SMCApiControllerBase
    {
        #region Services
        private IDocumentoConclusaoService DocumentoConclusaoService => Create<IDocumentoConclusaoService>();
        private IDocumentoAcademicoService DocumentoAcademicoService => Create<IDocumentoAcademicoService>();
        private IDocumentoAcademicoCurriculoService DocumentoAcademicoCurriculoService => Create<IDocumentoAcademicoCurriculoService>();
        #endregion

        [Route("api/DocumentosAcademicoXML/Atualizar")]
        [HttpPost]
        public void AtualizarDocumentosAcademicoXML(DocumentoAcademicoGADModel model)
        {
            var generic = new GenericIdentity(model.Usuario, "manual");
            generic.AddClaim(new System.Security.Claims.Claim("SeqUsuario", "0"));
            generic.AddClaim(new System.Security.Claims.Claim("Nome", model.Usuario));
            generic.AddClaim(new System.Security.Claims.Claim(SMCServiceContext.ApplicationIdKey, "SGA.Administrativo"));

            Thread.CurrentPrincipal = new GenericPrincipal(generic, null);

            var retorno = DocumentoAcademicoService.BuscarGrupoDocumentoAcademico(model.seqDocumentoAcademicoGAD);

            if (retorno.HasValue && retorno == GrupoDocumentoAcademico.CurriculoEscolar)
            {
                DocumentoAcademicoCurriculoService.AtualizarDocumentoAcademicoCurriculoDigital(model.seqDocumentoAcademicoGAD);
            }
            else if (retorno.HasValue &&
                    (retorno == GrupoDocumentoAcademico.Diploma || retorno == GrupoDocumentoAcademico.HistoricoEscolar))
            {
                DocumentoConclusaoService.AtualizarDocumentoAcademicoDiplomaOuHistoricoDigital(model.seqDocumentoAcademicoGAD);
            }
        }
    }
}