using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class PosicaoConsolidadaDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqProcesso { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .Service<IPosicaoConsolidadaService>(
                      index: nameof(IPosicaoConsolidadaService.ListarPosicoesConsolidadas)
                 )
                .HeaderIndex(nameof(PosicaoConsolidadaController.CabecalhoProcesso))
                .Detail<PosicaoConsolidadaListarDynamicModel>("_DetailList", allowSort: false)
                .ButtonBackIndex("Index", "Processo") 
                .Tokens(tokenList: UC_SRC_005_01_01.CONSULTAR_POSICAO_CONSOLIDADA,
                         tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION);
        }
                
        #endregion
    }
}