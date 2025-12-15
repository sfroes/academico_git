using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class CursoFormacaoEspecificaTitulacaoVO : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqCursoFormacaoEspecifica { get; set; }

        public long SeqTitulacao { get; set; }

        public long SeqFormacaoEspecifica { get; set; }
    }
}
