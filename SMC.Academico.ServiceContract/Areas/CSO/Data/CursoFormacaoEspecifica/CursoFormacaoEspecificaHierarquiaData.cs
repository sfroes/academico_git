using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class CursoFormacaoEspecificaHierarquiaData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        [SMCMapProperty("SeqFormacaoEspecificaSuperior")]
        public long? SeqPai { get; set; }
    }
}