using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class PessoaAtuacaoTermoIntercambioDynamicModel : SMCDynamicViewModel
    {
        public override long Seq { get; set; }
       
        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .DisableInitialListing(true)
                .EditInModal()
                .Service<IPessoaAtuacaoTermoIntercambioService>
                (index: nameof(IPessoaAtuacaoTermoIntercambioService.BuscarPessoaAtuacaoTermoIntercambio))
                .Detail<PessoaAtuacaoTermoIntercambioListarDynamicModel>("_DetailList")
                .Tokens(tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                        tokenEdit: UC_ALN_004_03_02.MANTER_INTERCAMBIO,
                        tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                        tokenList: UC_ALN_004_03_01.PESQUISAR_INTERCAMBIO);
        }
    }
}