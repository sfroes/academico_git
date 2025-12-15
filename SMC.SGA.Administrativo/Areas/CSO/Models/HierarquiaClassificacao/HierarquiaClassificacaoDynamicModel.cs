using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class HierarquiaClassificacaoDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        [SMCSortable(true, true)]
        [SMCStep(1)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.Filter | SMCViewMode.List)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("TiposHierarquiaClassificacao", "Seq", "Descricao")]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCStep(2)]
        public long SeqTipoHierarquiaClassificacao { get; set; }

        [SMCDataSource("TipoHierarquiaClassificacao")]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ICSODynamicService))]
        public List<SMCDatasourceItem> TiposHierarquiaClassificacao { get; set; }

        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit | SMCViewMode.Filter)]
        [SMCInclude("TipoHierarquiaClassificacao")]
        [SMCMapProperty("TipoHierarquiaClassificacao.Descricao")]
        [SMCOrder(2)]
        [SMCSortable(true, true, "TipoHierarquiaClassificacao.Descricao")]
        public string DescricaoTipoHierarquiaClassificacao { get; set; }

        #region Configuracoes

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.Button("Hierarquia", "Index", "Classificacao",
                                UC_CSO_001_04_03.MONTAR_HIERARQUIA_CLASSIFICACAO,
                                i => new { SeqHierarquiaClassificacao = SMCDESCrypto.EncryptNumberForURL(((ISMCSeq)i).Seq) })

                   .Tokens(tokenInsert: UC_CSO_001_04_02.MANTER_HIERARQUIA_CLASSIFICACAO,
                           tokenEdit: UC_CSO_001_04_02.MANTER_HIERARQUIA_CLASSIFICACAO,
                           tokenRemove: UC_CSO_001_04_02.MANTER_HIERARQUIA_CLASSIFICACAO,
                           tokenList: UC_CSO_001_04_01.PESQUISAR_HIERARQUIA_CLASSIFICACAO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new HierarquiaClassificacaoNavigationGroup(this);
        }

        #endregion Configuracoes
    }
}