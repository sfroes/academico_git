using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class HierarquiaEntidadeItemNodeVO : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqItemSuperior")]
        public long? SeqPai { get; set; }

        [SMCMapProperty("Entidade.Nome")]
        public string DescricaoHierarquiaEntidade { get; set; }

        public long? SeqTipoHierarquiaEntidadeItem { get; set; }

        public bool TipoClassificacaoFolha { get; set; }

        [SMCMapProperty("Entidade.TipoEntidade.EntidadeExternada")]
        public bool TipoEntidadeExternada { get; set; }

        [SMCMapProperty("ItemSuperior.SeqTipoHierarquiaEntidadeItem")]
        public long? SeqTipoHierarquiaEntidadeItemPai { get; set; }

        public long SeqHierarquiaEntidade { get; set; }

        public long SeqEntidade { get; set; }

        public TipoVisao? TipoVisaoHierarquia { get; set; }

        [SMCMapMethod("Entidade.VerificarSituacaoAtualAtiva")]
        public bool Ativa { get; set; }
    }
}