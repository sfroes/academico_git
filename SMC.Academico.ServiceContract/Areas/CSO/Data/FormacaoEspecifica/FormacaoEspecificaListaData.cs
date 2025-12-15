using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data
{
    public class FormacaoEspecificaListaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public string Descricao { get; set; }

        public string DescricaoGrau { get; set; }
    }
}