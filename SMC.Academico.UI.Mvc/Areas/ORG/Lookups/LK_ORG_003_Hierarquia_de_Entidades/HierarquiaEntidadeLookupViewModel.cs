using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class HierarquiaEntidadeLookupViewModel : SMCViewModelBase, ISMCLookupData
    {       
        [SMCKey]
        public long? Seq { get; set; }

        [SMCMapProperty("SeqItemSuperior")]
        public long? SeqPai { get; set; }


        [SMCDescription]
        [SMCMapProperty("Entidade.Nome")]
        public string DescricaoHierarquiaEntidade { get; set; }

        /// <summary>
        /// Necessário para o filtro realizado no ato do cadastro
        /// </summary>
        public long? SeqTipoHierarquiaEntidadeItem { get; set; }

        /// <summary>
        /// Indica se um nó pode ser Apenas um nó folha
        /// </summary>
        public bool TipoClassificacaoFolha { get; set; }

        [SMCMapProperty("Entidade.TipoEntidade.EntidadeExternada")]
        public bool TipoEntidadeExternada { get; set; }

        [SMCMapProperty("ItemSuperior.SeqTipoHierarquiaEntidadeItem")]
        public long? SeqTipoHierarquiaEntidadeItemPai { get; set; }

        public long SeqHierarquiaEntidade { get; set; }

        public TipoVisao? TipoVisaoHierarquia { get; set; }
    }
}
