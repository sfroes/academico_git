using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaHistoricoSituacaoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
               
        public long SeqTurma { get; set; }
               
        public SituacaoTurma SituacaoTurma { get; set; }
               
        public string Justificativa { get; set; }

    }
}
