using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaHistoricoSituacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public SituacaoTurma SituacaoTurma { get; set; }

        public string Justificativa { get; set; }

    }
}
