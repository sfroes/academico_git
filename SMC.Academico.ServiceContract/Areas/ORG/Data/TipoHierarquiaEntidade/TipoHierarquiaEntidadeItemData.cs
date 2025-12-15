using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoHierarquiaEntidadeItemData : ISMCMappable
    {
        public long Seq { get; set; }

        [SMCMapProperty("SeqItemSuperior")]
        public long? SeqPai { get; set; }

        [SMCMapProperty("SeqTipoHierarquia")]
        public long SeqTipoHierarquiaEntidade { get; set; }

        public long SeqTipoEntidade { get; set; }

        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCMapProperty("Responsavel")]
        public bool Responsavel { get; set; }
    }
}
