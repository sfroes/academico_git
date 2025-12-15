using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularInformacaoFormacaoVO : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }
        
        public string DescricaoFormacaoEspecifica { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }

        public int Level { get; set; }
    }
}
