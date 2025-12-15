using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DispensaMatrizDynamicModel : SMCDynamicViewModel, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter("Seq")]
        public long SeqDispensa { get; set; }

        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCMapForceFromTo]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<DispensaMatrizExcecaoViewModel> MatrizesExcecao { get; set; }

        #region [ Configurações ]

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Header("CabecalhoDispensa")
                   .RedirectIndexTo("Index", "Dispensa", x => new { })
                   .Service<IDispensaService>(edit: nameof(IDispensaService.BuscarDispensa),
                                                save: nameof(IDispensaService.SalvarDispensaMatriz))
                   .Tokens(tokenList: UC_CUR_003_02_03.ASSOCIAR_MATRIZ_DISPENSA_COMPONENTE,
                           tokenInsert: SMCSecurityConsts.SMC_DENY_AUTHORIZATION,
                           tokenEdit: UC_CUR_003_02_03.ASSOCIAR_MATRIZ_DISPENSA_COMPONENTE,
                           tokenRemove: SMCSecurityConsts.SMC_DENY_AUTHORIZATION);
        }

        #endregion [ Configurações ]
    }
}