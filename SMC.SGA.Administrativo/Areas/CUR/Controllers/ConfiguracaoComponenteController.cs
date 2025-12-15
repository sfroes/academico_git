using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class ConfiguracaoComponenteController : SMCDynamicControllerBase
    {
        #region [ Services ]

        internal IComponenteCurricularService ComponenteCurricularService
        {
            get { return Create<IComponenteCurricularService>(); }
        }

        internal ITipoDivisaoComponenteService TipoDivisaoComponenteService
        {
            get { return Create<ITipoDivisaoComponenteService>(); }
        }

        internal IInstituicaoNivelTipoDivisaoComponenteCurricularService InstituicaoNivelTipoDivisaoComponenteCurricularService
        {
            get { return Create<IInstituicaoNivelTipoDivisaoComponenteCurricularService>(); }
        }


        #endregion [ Services ]

        public ActionResult CabecalhoComponenteCurricular(SMCEncryptedLong seqComponenteCurricular)
        {
            // Busca as informações do Componente curricular para o cabeçalho
            ComponenteCurricularCabecalhoViewModel model = ExecuteService<ComponenteCurricularCabecalhoData, ComponenteCurricularCabecalhoViewModel>(ComponenteCurricularService.BuscarComponenteCurricularCabecalho, seqComponenteCurricular);
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR)]
        public bool VerificarTipoDivisaoGestao(long seqTipoDivisaoComponente)
        {
            var tipos = TipoDivisaoComponenteService.BuscarTipoDivisaoComponente(seqTipoDivisaoComponente);

            return tipos.TipoGestaoDivisaoComponente != TipoGestaoDivisaoComponente.Turma;
        }

        [SMCAuthorize(UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR)]
        public bool VerificarTipoDivisaoArtigo(long seqTipoDivisaoComponente)
        {
            var tipos = TipoDivisaoComponenteService.BuscarTipoDivisaoComponente(seqTipoDivisaoComponente);

            return tipos.Artigo.GetValueOrDefault();
        }

        [SMCAuthorize(UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR)]
        public bool VerificarPermissaoCargaHorariaGrade(long seqTipoDivisaoComponente, long seqComponenteCurricular)
        {
            var configuracao = InstituicaoNivelTipoDivisaoComponenteCurricularService.VerificarPermissaoCargaHorariaGrade(seqTipoDivisaoComponente, seqComponenteCurricular);

            return configuracao.PermiteCargaHorariaGrade;
        }


        [SMCAuthorize(UC_CUR_002_01_04.MANTER_CONFIGURACAO_COMPONENTE_CURRICULAR)]
        public bool VerificarQuantidadeSemanasComponentePreenchida(long seqComponenteCurricular)
        {
            var quantidadeSemanasComponentePreenchida = ComponenteCurricularService.VerificarQuantidadeSemanasComponentePreenchida(seqComponenteCurricular);

            return quantidadeSemanasComponentePreenchida;
        }
    }
}