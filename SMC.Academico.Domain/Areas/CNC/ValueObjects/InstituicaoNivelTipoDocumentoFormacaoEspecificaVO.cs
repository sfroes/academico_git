using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class InstituicaoNivelTipoDocumentoFormacaoEspecificaVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqInstituicaoNivelTipoDocumentoAcademico { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public string DescricaoFormacaoEspecifica { get; set; }
    }
}
