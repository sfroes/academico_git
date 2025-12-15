using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CNC.Data
{
    public class InstituicaoNivelTipoDocumentoFormacaoEspecificaData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }
    }
}
