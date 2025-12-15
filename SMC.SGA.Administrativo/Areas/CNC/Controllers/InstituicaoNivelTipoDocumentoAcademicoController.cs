using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CNC.Controllers
{
    public class InstituicaoNivelTipoDocumentoAcademicoController : SMCDynamicControllerBase
    {
        #region Services

        private IInstituicaoNivelTipoFormacaoEspecificaService InstituicaoNivelTipoFormacaoEspecificaService => Create<IInstituicaoNivelTipoFormacaoEspecificaService>();

        private IArquivoAnexadoService ArquivoAnexadoService => Create<IArquivoAnexadoService>();

        private ITipoDocumentoAcademicoService TipoDocumentoAcademicoService => Create<ITipoDocumentoAcademicoService>();

        private IInstituicaoNivelSistemaOrigemService InstituicaoNivelSistemaOrigemService => Create<IInstituicaoNivelSistemaOrigemService>();

        #endregion

        [SMCAuthorize(UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL)]
        public ActionResult BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(long seqInstituicaoNivel)
        {
            return Json(InstituicaoNivelTipoFormacaoEspecificaService.BuscarTiposFormacaoEspecificaPorInstituicaoNivelSelect(seqInstituicaoNivel));
        }

        [SMCAuthorize(UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL)]
        public JsonResult BuscarConfiguracaoGADPorNivelEnsino(UsoSistemaOrigem usoSistemaOrigem, long seqInstituicaoNivel)
        {
            if (usoSistemaOrigem != UsoSistemaOrigem.Nenhum)
            {
                var configuracaoSistemaGAD = InstituicaoNivelSistemaOrigemService.BuscarConfiguracaoDiplomaPorNivelEnsinoGADSelect(seqInstituicaoNivel, usoSistemaOrigem);
                return Json(configuracaoSistemaGAD);
            }
            return null;
        }

        [SMCAuthorize(UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL)]
        public JsonResult BuscarTipoUsoSistemaOrigem(long seqInstituicaoNivel)
        {
            var configuracaoSistemaGAD = InstituicaoNivelSistemaOrigemService.BuscarTipoUsoNivelEnsino(seqInstituicaoNivel);
            return Json(configuracaoSistemaGAD);

        }

        [SMCAuthorize(UC_CNC_004_04_01.MANTER_TIPO_DOCUMENTO_ACADEMICO_INSTITUICAO_NIVEL)]
        public ActionResult ExibeTiposFormacao(long seqTipoDocumentoAcademico)
        {
            var tipoDocumentoAcademico = TipoDocumentoAcademicoService.BuscarTipoDocumentoAcademico(seqTipoDocumentoAcademico);

            var exibeTiposFormacao = tipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.DeclaracoesGenericasAluno &&
                                     tipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.DeclaracoesGenericasProfessor &&
                                     tipoDocumentoAcademico.GrupoDocumentoAcademico != GrupoDocumentoAcademico.CurriculoEscolar;


            return Json(exibeTiposFormacao);
        }
    }
}