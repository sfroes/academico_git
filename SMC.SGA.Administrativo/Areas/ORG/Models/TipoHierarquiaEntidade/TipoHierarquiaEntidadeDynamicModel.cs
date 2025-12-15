using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoHierarquiaEntidadeDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSortable(true)]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid15_24, Framework.SMCSize.Grid24_24, Framework.SMCSize.Grid12_24, Framework.SMCSize.Grid15_24)]
        public string Descricao { get; set; }

        [SMCOrder(2)]
        [SMCHidden(SMCViewMode.Filter)]
//        [SMCSortable(true)]
        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24)]
        public TipoVisao? TipoVisao { get; set; }

        [SMCOrder(3)]
        [SMCHidden()]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Button("Hierarquia", "VerificaConfiguracaoTipoEntidade", "TipoHierarquiaEntidade",
                                UC_ORG_001_05_03.MONTAR_HIERARQUIA_TIPO_ENTIDADE,
                                i => new { seqTipoHierarquiaEntidade = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Tokens(tokenInsert: UC_ORG_001_05_01.PESQUISAR_TIPO_HIERARQUIA_ENTIDADE,
                           tokenEdit: UC_ORG_001_05_02.MANTER_TIPO_HIERARQUIA_ENTIDADE,
                           tokenRemove: UC_ORG_001_05_02.MANTER_TIPO_HIERARQUIA_ENTIDADE,
                           tokenList: UC_ORG_001_05_02.MANTER_TIPO_HIERARQUIA_ENTIDADE);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new TipoHierarquiaEntidadeNavigationGroup(this);
        }
    }
}