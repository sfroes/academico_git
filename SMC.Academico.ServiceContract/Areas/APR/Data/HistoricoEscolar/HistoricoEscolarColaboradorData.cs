using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarColaboradorData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }
    }
}