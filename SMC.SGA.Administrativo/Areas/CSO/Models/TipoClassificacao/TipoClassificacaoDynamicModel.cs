using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoClassificacaoDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCKey]
        [SMCOrder(1)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCHidden(SMCViewMode.Filter)]
        [SMCRegularExpression(REGEX.TOKEN)]
        [SMCOrder(3)]
        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid10_24)]
        public string Token { get; set; }

        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCInclude("TipoClassificacaoSuperior")]
        [SMCMapProperty("TipoClassificacaoSuperior.Descricao")]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCOrder(0)]
        [SMCConditionalDisplay("SeqTipoClassificacaoSuperior", SMCConditionalOperation.GreaterThen, 0)]
        public string DescricaoItemSuperior { get; set; }

        [SMCHidden]
        public long? SeqTipoClassificacaoSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCParameter]
        [SMCHidden]
        public long? SeqPai
        {
            get { return SeqTipoClassificacaoSuperior; }
            set { SeqTipoClassificacaoSuperior = value; }
        }

        [SMCHidden]
        [SMCRequired]
        [SMCParameter]
        public long SeqTipoHierarquiaClassificacao { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "TipoHierarquiaClassificacao")
                   .HeaderIndex("CabecalhoTipoHierarquiaClassificacao")
                   .Tokens(tokenEdit: UC_CSO_001_06_04.MANTER_TIPO_CLASSIFICACAO,
                           tokenInsert: UC_CSO_001_06_04.MANTER_TIPO_CLASSIFICACAO,
                           tokenRemove: UC_CSO_001_06_04.MANTER_TIPO_CLASSIFICACAO,
                           tokenList: UC_CSO_001_06_03.MONTAR_HIERARQUIA_TIPO_CLASSIFICACAO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new TipoHierarquiaClassificacaoNavigationGroup(this);
        }
    }
}