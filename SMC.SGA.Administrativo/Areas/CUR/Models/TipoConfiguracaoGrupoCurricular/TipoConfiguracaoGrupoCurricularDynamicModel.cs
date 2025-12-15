using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoConfiguracaoGrupoCurricularDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid13_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid14_24)]
        public string Descricao { get; set; }

        [SMCDescription]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid6_24)]
        [SMCHidden(SMCViewMode.Filter)]
        [SMCMinLength(3)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCRadioButtonList]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid4_24)]
        public bool Raiz { get; set; }

        [SMCRadioButtonList]
        [SMCOrder(4)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid5_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        public bool ExigeFormato { get; set; }

        /// <summary>
        /// Indica se o registro será subgrupo dele mesmo
        /// </summary>
        [SMCRadioButtonList]
        [SMCOrder(5)]
        [SMCRequired]
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List | SMCViewMode.Edit)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid10_24)]
        public bool SelfSubgrupo { get; set; }

        /// <summary>
        /// Subgrupos
        /// </summary>
        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCOrder(6)]
        [SMCDetail]
        public SMCMasterDetailList<TipoConfiguracaoGrupoCurricularFilhoViewModel> TiposConfiguracoesGrupoCurricularFilhos { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Tokens(tokenInsert: UC_CUR_001_08_01.MANTER_TIPO_CONFIGURACAO_GRUPO_CURRICULAR,
                           tokenEdit: UC_CUR_001_08_01.MANTER_TIPO_CONFIGURACAO_GRUPO_CURRICULAR,
                           tokenList: UC_CUR_001_08_01.MANTER_TIPO_CONFIGURACAO_GRUPO_CURRICULAR,
                           tokenRemove: UC_CUR_001_08_01.MANTER_TIPO_CONFIGURACAO_GRUPO_CURRICULAR)
                   .Service<ITipoConfiguracaoGrupoCurricularService>(save: "SalvarTipoConfiguracaoGrupoCurricular", delete: "ExcluirTipoConfiguracaoGrupoCurricular");
        }
    }
}