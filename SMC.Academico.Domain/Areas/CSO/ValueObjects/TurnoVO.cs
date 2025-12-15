using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CSO.ValueObjects
{
    public class TurnoVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public  string Descricao { get; set; }
    }
}
