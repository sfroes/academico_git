using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class TipoFormacaoEspecificaTipoCursoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTipoFormacaoEspecifica { get; set; }

        public TipoCurso TipoCurso { get; set; }
    }
}
