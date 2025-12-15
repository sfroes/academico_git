using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.APR.Models
{
    public class EscalaApuracaoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        public bool UtilizadoPorCriterioAprovacao { get; set; }

        [SMCDescription]
        [SMCOrder(0)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid17_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid17_24)]
        [SMCMaxLength(100)]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid7_24)]
        public bool ApuracaoFinal { get; set; }

        [SMCOrder(2)]
        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid9_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid9_24)]
        public bool ApuracaoAvaliacao { get; set; }

        [SMCOrder(3)]
        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoEscalaApuracao TipoEscalaApuracao { get; set; }

        [SMCDetail]
        [SMCHidden(SMCViewMode.List)]
        [SMCOrder(4)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<EscalaApuracaoItemViewModel> Itens { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_APR_001_03_02.MANTER_ESCALA_APURACAO,
                           tokenEdit: UC_APR_001_03_02.MANTER_ESCALA_APURACAO,
                           tokenList: UC_APR_001_03_01.PESQUISAR_ESCALA_APURACAO,
                           tokenRemove: UC_APR_001_03_02.MANTER_ESCALA_APURACAO)
                   .Service<IEscalaApuracaoService>(insert: nameof(IEscalaApuracaoService.BuscarConfiguracaoEscalaApuracao),
                                                    save: nameof(IEscalaApuracaoService.SalvarEscalaApuracao),
                                                    edit: nameof(IEscalaApuracaoService.BuscarEscalaApuracao))
                   .Assert("Mensagem_UsoCriterioAprovacao", model => { return (model as EscalaApuracaoDynamicModel).UtilizadoPorCriterioAprovacao; });
        }
    }
}