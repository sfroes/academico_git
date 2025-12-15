using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{
    public class InstituicaoNivelBeneficioController : SMCDynamicControllerBase
    {
        #region [ Services ]

        internal IBeneficioService BeneficioService
        {
            get { return this.Create<IBeneficioService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_FIN_003_01_02.MANTER_BENEFICIO_INSTITUICAO_NIVEL)]
        public bool VerificarConfiguracaoBeneficio(long seqBeneficio)
        {
            var beneficio = BeneficioService.BuscarBeneficio(seqBeneficio);

            return beneficio.DeducaoValorParcelaTitular;
        }
    }
}