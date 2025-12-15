using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoHierarquiaClassificacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCFilter(true, true)]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Token { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Button("Hierarquia", "VerificaConfiguracaoTipoClassificacao", "TipoHierarquiaClassificacao",
                                UC_CSO_001_06_03.MONTAR_HIERARQUIA_TIPO_CLASSIFICACAO,
                                i => new { seqTipoHierarquiaClassificacao = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })
                   .Tokens(tokenInsert: UC_CSO_001_06_02.MANTER_TIPO_HIERARQUIA_CLASSIFICACAO,
                           tokenEdit: UC_CSO_001_06_02.MANTER_TIPO_HIERARQUIA_CLASSIFICACAO,
                           tokenRemove: UC_CSO_001_06_02.MANTER_TIPO_HIERARQUIA_CLASSIFICACAO,
                           tokenList: UC_CSO_001_06_01.PESQUISAR_TIPO_HIERARQUIA_CLASSIFICACAO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new TipoHierarquiaClassificacaoNavigationGroup(this);
        }
    }
}