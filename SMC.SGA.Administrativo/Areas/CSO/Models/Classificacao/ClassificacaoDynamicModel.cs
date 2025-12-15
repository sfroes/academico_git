using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.CSO.Views.Curso.App_LocalResources;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ClassificacaoDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCReadOnly(SMCViewMode.Edit | SMCViewMode.Insert)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCHidden(SMCViewMode.Edit | SMCViewMode.Insert)]
        public string DescricaoCompleta
        {
            get { return string.IsNullOrEmpty(CodigoExterno)? Descricao: string.Format("{0} - {1}", CodigoExterno, Descricao); }
        }

        [SMCHidden(SMCViewMode.List)]
        [SMCMaxLength(100)]
        [SMCOrder(2)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(3)]
        [SMCSize(SMCSize.Grid8_24)]
        public string CodigoExterno { get; set; }

        [SMCConditionalDisplay("SeqClassificacaoSuperior", SMCConditionalOperation.GreaterThen, 0)]
        [SMCHidden(SMCViewMode.List | SMCViewMode.Filter)]
        [SMCInclude("ClassificacaoSuperior")]
        [SMCMapProperty("ClassificacaoSuperior.Descricao")]
        [SMCOrder(1)]
        [SMCParameter(Name = "Description")] //Para passar a descrição do pai para o no filho (quando for novo)
        [SMCReadOnly]
        [SMCSize(SMCSize.Grid20_24)]
        public string DescricaoItemSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCHidden]
        public long? SeqPai { get { return SeqClassificacaoSuperior; } set { SeqClassificacaoSuperior = value; } }

        [SMCHidden]
        public long? SeqClassificacaoSuperior { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCRequired]
        public long SeqHierarquiaClassificacao { get; set; }

        [SMCOrder(4)]
        [SMCRequired]
        [SMCSelect("TiposClassificacao", autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid16_24)]
        public long? SeqTipoClassificacao { get; set; }

        [SMCDataSource()]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoClassificacaoService),
            "BuscarTiposClassificacaoPorHierarquiaSelect",
            values: new string[] { nameof(SeqHierarquiaClassificacao), nameof(Exclusivo), nameof(SeqClassificacaoSuperior) })]
        public List<SMCDatasourceItem> TiposClassificacao { get; set; }

        [SMCHidden]
        [SMCIgnoreMetadata]
        public bool Exclusivo { get { return true; } }

        [SMCHidden]
        public bool TipoClassificacaoFolha { get; set; }

        public override void ConfigureDynamic(ref SMCDynamicOptions options)
        {
            options.EditInModal()
                   .Service<IClassificacaoService>(index: "BuscarClassificacaoPorHierarquiaClassificacao")
                   .ConfigureContextMenuItem((button, model, action) =>
                      {
                          bool tipoClassificacaoFolha = ((SMCTreeViewNode<ClassificacaoDynamicModel>)model).Value.TipoClassificacaoFolha;
                          if (action == SMCDynamicButtonAction.Insert && tipoClassificacaoFolha)
                              button.Hide();
                      })
                   .IgnoreFilterGeneration()
                   .ButtonBackIndex("Index", "HierarquiaClassificacao")
                   .HeaderIndex("CabecalhoHierarquiaClassificacao")
                   .Messages(x => ((SMCTreeViewNode<ClassificacaoDynamicModel>)x).Nodes.Count > 0 ? string.Format(UIResource.Excluir_Classificacao_Com_Filhos, ((SMCTreeViewNode<ClassificacaoDynamicModel>)x).Value.DescricaoCompleta) : string.Format(UIResource.Excluir_Classificacao_Sem_Filhos, ((SMCTreeViewNode<ClassificacaoDynamicModel>)x).Value.DescricaoCompleta))
                   .Tokens(tokenEdit: UC_CSO_001_04_04.MANTER_CLASSIFICACAO,
                           tokenInsert: UC_CSO_001_04_04.MANTER_CLASSIFICACAO,
                           tokenRemove: UC_CSO_001_04_04.MANTER_CLASSIFICACAO,
                           tokenList: UC_CSO_001_04_03.MONTAR_HIERARQUIA_CLASSIFICACAO);
        }

        public override void ConfigureNavigation(ref SMCNavigationGroup navigationGroup)
        {
            navigationGroup = new HierarquiaClassificacaoNavigationGroup(this);
        }
    }
}