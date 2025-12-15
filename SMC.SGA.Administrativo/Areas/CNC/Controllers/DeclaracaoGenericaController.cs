using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.CNC.Data.DeclaracaoGenerica;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CNC.Models;
using SMC.SGA.Administrativo.Extensions;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class DeclaracaoGenericaController : SMCControllerBase
    {

        #region Services

        private ITipoDocumentoAcademicoService TipoDocumentoAcademicoService => Create<ITipoDocumentoAcademicoService>();

        private ISituacaoDocumentoAcademicoService SituacaoDocumentoAcademicoService => Create<ISituacaoDocumentoAcademicoService>();

        private IEntidadeService EntidadeService => Create<IEntidadeService>();

        private IDeclaracaoGenericaService DeclaracaoGenericaService => Create<IDeclaracaoGenericaService>();

        #endregion

        [SMCAuthorize(UC_CNC_005_01_01.PESQUISAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO)]
        public ActionResult Index(DeclaracaoGenericaFiltroDynamicModel filtro)
        {
            var grupos = new List<GrupoDocumentoAcademico> { GrupoDocumentoAcademico.DeclaracoesGenericasAluno };            

            filtro.EntidadesResponsaveis = this.EntidadeService.BuscarEntidadesResponsaveisVisaoOrganizacionalSelect(false, true);            
            filtro.TiposDocumento = this.TipoDocumentoAcademicoService.BuscarTiposDocumentoAcademicoPorTipoGrupoDocAcadSelect(GrupoDocumentoAcademico.DeclaracoesGenericasAluno);
            filtro.Situacoes = this.SituacaoDocumentoAcademicoService.BuscarSituacoesDocumentoAcademicoPorGrupoSelect(grupos);

            return View(filtro);
        }


        [SMCAuthorize(UC_CNC_005_01_01.PESQUISAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO)]
        public ActionResult ListarDeclaracoesGenericas(DeclaracaoGenericaFiltroDynamicModel filtro)
        {
            SMCPagerModel<DeclaracaoGenericaListarViewModel> model = ExecuteService<DeclaracaoGenericaFiltroData, DeclaracaoGenericaListarData,
                                                                        DeclaracaoGenericaFiltroDynamicModel, DeclaracaoGenericaListarViewModel>
                                                                       (DeclaracaoGenericaService.BuscarDeclaracoesGenericas, filtro);
            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_CNC_005_01_02.CONSULTAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO)]
        public ActionResult Consultar(SMCEncryptedLong seqDocumento) 
        {            
            var model = DeclaracaoGenericaService.BuscarDeclaracaoGenerica(seqDocumento).Transform<DeclaracaoGenericaDadosGeraisViewModel>();

            if (model.SeqDocumentoGAD.HasValue && model.SeqDocumentoGAD != 0)
            {
                var usuarioInclusao = UsuarioLogado.RetornaUsuarioLogado();
                model.UrlDocumentoGAD = $"{ConfigurationManager.AppSettings["UrlDeclaracaoGenericaGAD"]}?Seq={new SMCEncryptedLong(model.SeqDocumentoGAD.Value)}";                    
            }

            return View("Consultar", model);
        }
    }
}