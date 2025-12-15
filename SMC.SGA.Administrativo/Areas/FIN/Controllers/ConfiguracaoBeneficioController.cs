using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{
    public class ConfiguracaoBeneficioController : SMCDynamicControllerBase
    {
        #region Serviços

        internal IConfiguracaoBeneficioService ConfiguracaoBeneficio
        {
            get { return this.Create<IConfiguracaoBeneficioService>(); }
        }

        #endregion Serviços

        /// <summary>
        /// Validar se Benefico a se configurado tem dedução conforme regra
        /// </summary>
        /// <param name="seqInstituicaoNivelBeneficio">Seq intituicao nível benefício</param>
        /// <param name="seqInstituicaoNivel">Seq intistuicao nivel</param>
        /// <param name="seqBeneficio">Seq Beneficio</param>
        /// <returns>Valida se o beneficio pode ser configurado</returns>
        [SMCAuthorize(UC_FIN_003_01_05.PESQUISAR_CONFIGURACAO_BENEFICIO)]
        public ActionResult VerificarDeducaoBeneficio(SMCEncryptedLong seqInstituicaoNivelBeneficio, SMCEncryptedLong seqInstituicaoNivel, SMCEncryptedLong seqBeneficio)
        {
            try
            {
                if (ConfiguracaoBeneficio.VerificarDeducaoBeneficio(seqInstituicaoNivelBeneficio))
                {
                    return SMCRedirectToAction("Index", "ConfiguracaoBeneficio",
                                                new
                                                {
                                                    seqBeneficio,
                                                    seqInstituicaoNivel,
                                                    seqInstituicaoNivelBeneficio
                                                });

                }
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de excluir, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", "InstituicaoNivelBeneficio");
                throw ex;
            }
            return View();
        }

        /// <summary>
        /// Verificar se existe alguma associação a pessoa atuação beneficio
        /// </summary>
        /// <param name="seq">seq da configuração de beneficio</param>
        /// <returns>Return false ou true</returns>
        public bool VerificarAssociacaoPessoaBeneficio(SMCEncryptedLong seq)
        {
            return ConfiguracaoBeneficio.VerificarAssociacaoPessoaBeneficio(seq);            
        }

        [SMCAuthorize(UC_FIN_003_01_06.MANTER_CONFIGURACAO_BENEFICIO)]
        public ActionResult BuscarBeneficiosGRASelect(long SeqTipoBeneficio)
        {
            var beneficiosGra = Create<IBeneficioService>().BuscarBeneficiosGRASelect(SeqTipoBeneficio);
            return Json(beneficiosGra);
        }
    }
}