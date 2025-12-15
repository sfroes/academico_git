using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularInformacaoFormacaoData : ISMCMappable
    {
        public long SeqFormacaoEspecifica { get; set; }

        public long? SeqFormacaoEspecificaSuperior { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public string DescricaoTipoFormacaoEspecifica { get; set; }
    }
}
