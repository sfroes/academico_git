using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class HierarquiaEntidadeItemNodeData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqItemSuperior")]
        public long? SeqPai { get; set; }
                
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

        [SMCMapProperty("Entidade.Seq")]
        public long SeqEntidade { get; set; }

        public TipoVisao? TipoVisaoHierarquia { get; set; }

        public bool Ativa { get; set; }
    }
}
