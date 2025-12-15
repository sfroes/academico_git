using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class FormacaoEspecificaListaVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.Descricao")]
        public string DescricaoTipoFormacaoEspecifica { get; set; }

        [SMCMapProperty("TipoFormacaoEspecifica.Token")]
        public string TokenTipoFormacaoEspecifica { get; set; }

        public string Descricao { get; set; }

        public string DescricaoGrau { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public bool Vigente { get; set; }
    }
}