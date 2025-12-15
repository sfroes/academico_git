using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class PeriodicoDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCRequired]
        public long SeqClassificacaoPeriodico { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCMapProperty("ClassificacaoPeriodico.Descricao")]
        [SMCInclude("ClassificacaoPeriodico")]
        [SMCIgnoreProp]
        public string DescricaoClassificacaoPeriodico { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCIgnoreProp(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(min: 1)]
        [SMCOrder(2)]
        public SMCMasterDetailList<QualisPeriodicoDetalheViewModel> QualisPeriodico { get; set; }

        #region Configurações

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                .EditInModal(refreshIndexPageOnSubmit: true)
                .Header("CabecalhoPeriodico")
                .Detail<PeriodicoListarDynamicModel>("_DetailList")
                .ModalSize(SMCModalWindowSize.Large)
                .IgnoreFilterGeneration(true)
                .IgnoreInsert()
                .HeaderIndex("Periodico")
                .ConfigureButton((button, Models, action) =>
                {
                    if (action == SMCDynamicButtonAction.Edit)
                        button.Hide(true);

                    if (action == SMCDynamicButtonAction.Remove)
                        button.Hide(true);
                })
                .Tokens(tokenInsert: UC_CUR_002_06_03.MANTER_PERIODICO_CAPES,
                           tokenEdit: UC_CUR_002_06_03.MANTER_PERIODICO_CAPES,
                           tokenRemove: UC_CUR_002_06_03.MANTER_PERIODICO_CAPES,
                           tokenList: UC_CUR_002_06_01.PESQUISAR_PERIODICO_CAPES)
                .Service<IPeriodicoService>(index: nameof(IPeriodicoService.BuscarPeriodicosCapes),
                                            insert: nameof(IPeriodicoService.PrepararModeloPeriodico));
        }

        #endregion Configurações
    }
}