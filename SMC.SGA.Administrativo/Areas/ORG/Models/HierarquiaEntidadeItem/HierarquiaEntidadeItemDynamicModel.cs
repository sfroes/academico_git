using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.UI.Mvc.Areas.ORG.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeItemDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long SeqHierarquiaEntidade { get; set; }

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

        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCConditionalDisplay("SeqItemSuperior", SMCConditionalOperation.GreaterThen, 0)]
        [SMCInclude("ItemSuperior.Entidade")]
        [SMCMapProperty("ItemSuperior.Entidade.Nome")]
        [SMCOrder(0)]
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        public string NomeEntidadeSuperior { get; set; }

        [SMCOrder(1)]
        [SMCRequired]
        [SMCSelect("TiposHierarquiaEntidadeItem")]
        [SMCSize(SMCSize.Grid24_24)]
        public long? SeqTipoHierarquiaEntidadeItem { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(IHierarquiaEntidadeItemService), nameof(IHierarquiaEntidadeItemService.BuscarTipoHierarquiaEntidadeItemSelect),
            values: new string[3] { nameof(SeqHierarquiaEntidade), nameof(SeqTipoHierarquiaEntidadeItem), nameof(SeqTipoHierarquiaEntidadeItemPai) })]
        public List<SMCDatasourceItem> TiposHierarquiaEntidadeItem { get; set; }

        [SMCHidden]
        [SMCIgnoreMetadata]
        public long SeqEntidade { get { return Entidade == null ? 0 : Entidade.Seq; } }

        [EntidadeLookup]
        [SMCConditionalDisplay("SeqTipoHierarquiaEntidadeItem", SMCConditionalOperation.GreaterThen, new object[1] { 0 })]
        [SMCDependency("SeqTipoHierarquiaEntidadeItem")]
        [SMCDependency("ApenasAtivos")]
        [SMCDescription]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public EntidadeLookupViewModel Entidade { get; set; }

        [SMCHidden]
        public bool? ApenasAtivos { get; set; } = true;

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options
                   .IgnoreFilterGeneration()
                   .EditInModal()
                   .ConfigureContextMenuButton((button, model) =>
                   {
                       var listarModel = ((SMCTreeViewNode<HierarquiaEntidadeItemListarDynamicModel>)model).Value;
                       bool tipoEntidadeExternada = listarModel.TipoEntidadeExternada;
                       bool contemVisao = listarModel.TipoVisaoHierarquia.HasValue && listarModel.TipoVisaoHierarquia != TipoVisao.Nenhum;
                       bool isTipoClassificacaoFolha = listarModel.TipoClassificacaoFolha;

                       button.Hide(contemVisao && tipoEntidadeExternada && isTipoClassificacaoFolha).DisplayMode(SMCButtonDisplayMode.Icon);
                   })
                   .ConfigureContextMenuItem((button, model, action) =>
                     {
                         HierarquiaEntidadeItemListarDynamicModel listarModel = ((SMCTreeViewNode<HierarquiaEntidadeItemListarDynamicModel>)model).Value;
                         bool tipoEntidadeExternada = listarModel.TipoEntidadeExternada;
                         bool contemVisao = listarModel.TipoVisaoHierarquia.HasValue && listarModel.TipoVisaoHierarquia != TipoVisao.Nenhum;

                         if ((action == SMCDynamicButtonAction.Edit || action == SMCDynamicButtonAction.Remove) && contemVisao && tipoEntidadeExternada)
                         {
                             button.Hide();
                         }
                         else if (action == SMCDynamicButtonAction.Insert)
                         {
                             bool isTipoClassificacaoFolha = listarModel.TipoClassificacaoFolha;
                             if (isTipoClassificacaoFolha)
                                 button.Hide();
                         }
                     })
                   .ButtonBackIndex("Index", "HierarquiaEntidade")
                   .HeaderIndex("CabecalhoHierarquiaEntidadeItem")
                   .HeaderIndexList("MensagemHierarquiaEntidadeItem")
                   .TreeView(configureNode: x => !(x as SMCTreeViewNode<HierarquiaEntidadeItemListarDynamicModel>).Value.Ativa ?
                                                   (x as SMCTreeViewNode<HierarquiaEntidadeItemListarDynamicModel>).Options.CssClass = "smc-sga-item-inativo" :
                                                   null)
                   .Service<IHierarquiaEntidadeItemService>(index: nameof(IHierarquiaEntidadeItemService.BuscarHierarquiaEntidadeItens),
                                                            save: nameof(IHierarquiaEntidadeItemService.SalvarHierarquiaEntidadeItem),
                                                            delete: nameof(IHierarquiaEntidadeItemService.ExcluirHierarquiaEntidadeItem))
                   .Tokens(tokenInsert: UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE,
                           tokenEdit: UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE,
                           tokenRemove: UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE,
                           tokenList: UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE);
        }

        /// <summary>
        /// Utilizado apenas como parâmetro e para o modo edição de um nó
        /// </summary>
        [SMCHidden]
        [SMCInclude("ItemSuperior")]
        [SMCMapProperty("ItemSuperior.SeqTipoHierarquiaEntidadeItem")]
        [SMCParameter]
        public long? SeqTipoHierarquiaEntidadeItemPai { get; set; }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new HierarquiaEntidadeItemNavigationGroup(this);
        }
    }
}