using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoHierarquiaEntidadeItemDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCFilter(true, true)]
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid2_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCConditionalDisplay("SeqItemSuperior", SMCConditionalOperation.GreaterThen, 0)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCInclude("ItemSuperior.TipoEntidade")]
        [SMCMapProperty("ItemSuperior.TipoEntidade.Descricao")]
        [SMCOrder(1)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid20_24, SMCSize.Grid20_24, SMCSize.Grid20_24, SMCSize.Grid20_24)]
        public string DescricaoItemSuperior { get; set; }

        [SMCHidden]
        public long? SeqItemSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqPai
        {
            get { return SeqItemSuperior; }
            set { SeqItemSuperior = value; }
        }

        [SMCHidden]
        [SMCParameter]
        [SMCRequired]
        public long SeqTipoHierarquiaEntidade { get; set; }

        [SMCOrder(2)]
        [SMCRequired]
        [SMCSelect("TiposEntidade")]
        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24, SMCSize.Grid16_24)]
        public long SeqTipoEntidade { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IInstituicaoTipoEntidadeService), "BuscarTipoEntidadesDaInstituicaoSelect")]
        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Insert | SMCViewMode.Edit)]
        [SMCInclude("TipoEntidade")]
        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCOrder(3)]
        [SMCMapProperty("Responsavel")]
        [SMCRequired]
        public bool? Responsavel { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            Func<SMCTreeViewNode<TipoHierarquiaEntidadeItemDynamicModel>, string> retCss = (model) =>
            {
                List<string> ret = new List<string>();

                if (model.Value.Responsavel.GetValueOrDefault())
                    ret.Add("smc-sga-tipo-hierarquia-responsavel");

                return string.Join(" ", ret);
            };

            options.EditInModal()
                 .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "TipoHierarquiaEntidade")
                   .HeaderIndex("CabecalhoTipoHierarquiaEntidade")
                   .TreeView(configureNode: x => (x as SMCTreeViewNode<TipoHierarquiaEntidadeItemDynamicModel>).Options.CssClass = retCss(x as SMCTreeViewNode<TipoHierarquiaEntidadeItemDynamicModel>))
                   .Tokens(tokenEdit: UC_ORG_001_05_04.ASSOCIAR_TIPO_ENTIDADE,
                           tokenInsert: UC_ORG_001_05_04.ASSOCIAR_TIPO_ENTIDADE,
                           tokenRemove: UC_ORG_001_05_04.ASSOCIAR_TIPO_ENTIDADE,
                           tokenList: UC_ORG_001_05_03.MONTAR_HIERARQUIA_TIPO_ENTIDADE);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new TipoHierarquiaEntidadeNavigationGroup(this);
        }
    }
}