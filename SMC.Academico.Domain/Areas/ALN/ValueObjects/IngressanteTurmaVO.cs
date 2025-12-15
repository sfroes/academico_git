using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteTurmaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqProcessoSeletivo { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }
        
        public TipoAtuacao TipoAtuacao { get; set; }
    }
}
