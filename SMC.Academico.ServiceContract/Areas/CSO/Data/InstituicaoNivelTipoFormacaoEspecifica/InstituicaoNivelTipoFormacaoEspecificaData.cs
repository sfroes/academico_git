using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoNivelTipoFormacaoEspecifica
{
    public class InstituicaoNivelTipoFormacaoEspecificaData : ISMCMappable
    {
        public long Seq { get; set; }


        public long SeqInstituicaoNivel { get; set; }


        public long SeqTipoFormacaoEspecifica { get; set; }
    }
}
