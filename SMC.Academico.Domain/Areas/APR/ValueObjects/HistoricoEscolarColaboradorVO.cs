using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarColaboradorVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }

        public string NomeOrdenacao { get; set; }
    }
}