using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeItemListarDynamicModel : SMCDynamicViewModel, ISMCTreeNode
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCHidden]
        public override long Seq { get; set; }

        [SMCMapProperty("SeqItemSuperior")]
        [SMCHidden]
        public long? SeqPai { get; set; }

        #region Descricao do node

        [SMCDescription]
        [SMCInclude("Entidade")]
        [SMCMapProperty("Entidade.Nome")]
        public string DescricaoHierarquiaEntidade { get; set; }

        #endregion Descricao do node

        /// <summary>
        /// Necessário para o filtro realizado no ato do cadastro
        /// </summary>
        [SMCHidden]
        [SMCParameter]
        public long? SeqTipoHierarquiaEntidadeItem { get; set; }

        /// <summary>
        /// Indica se um nó pode ser Apenas um nó folha
        /// </summary>
        [SMCHidden]
        public bool TipoClassificacaoFolha { get; set; }

        /// <summary>
        /// Indica se o tipo da entidade refenciada pelo nó é externada
        /// </summary>
        [SMCHidden]
        public bool TipoEntidadeExternada { get; set; }

        /// <summary>
        /// Tipo de visão da árvore de hierarquia
        /// </summary>
        [SMCHidden]
        public TipoVisao? TipoVisaoHierarquia { get; set; }

        [SMCHidden]
        [SMCParameter]
        [SMCInclude("ItemSuperior")]
        [SMCMapProperty("ItemSuperior.SeqTipoHierarquiaEntidadeItem")]
        public long? SeqTipoHierarquiaEntidadeItemPai { get; set; }

        [SMCHidden]
        public long SeqHierarquiaEntidade { get; set; }

        [SMCHidden]
        public bool Ativa { get; set; }
    }
}