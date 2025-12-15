using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class NivelEnsinoHierarquiaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("SeqPai")]
        public long? SeqNivelEnsinoSuperior { get; set; }

        public bool Folha { get; set; }
    }
}
