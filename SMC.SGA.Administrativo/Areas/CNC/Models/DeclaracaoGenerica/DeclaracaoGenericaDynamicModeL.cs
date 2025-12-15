using SMC.Academico.Common.Areas.CNC.Constants;
using SMC.Academico.Service.Areas.CNC.Services;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models.DeclaracaoGenerica
{
    public class DeclaracaoGenericaDynamicModeL : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }


        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.DisableInitialListing()
                   .Tokens(tokenList: UC_CNC_005_01_01.PESQUISAR_DOCUMENTO_ACADEMICO_PESSOA_ATUACAO)
                   .Detail<SMCPagerModel<DeclaracaoGenericaListarViewModel>>("_DetailList")
                   .Service<IDeclaracaoGenericaService>(index: nameof(IDeclaracaoGenericaService.BuscarDeclaracoesGenericas));
        }
    }
}