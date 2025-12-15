using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class InstituicaoLocalidadeAgendaData : ISMCMappable, ISMCSeq​​
    {
        public long Seq { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqAgendaAgd { get; set; }
    }
}