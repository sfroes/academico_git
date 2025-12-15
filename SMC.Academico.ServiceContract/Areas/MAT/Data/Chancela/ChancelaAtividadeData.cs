using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class ChancelaAtividadeData : ISMCMappable
    {
        public long SeqItem { get; set; }

        public string Descricao { get; set; }

        public long SeqSituacaoItemMatricula { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public MatriculaPertencePlanoEstudo SituacaoPlanoEstudo { get; set; }

        public bool? LegendaPertencePlanoEstudo { get; set; }
    }
}
