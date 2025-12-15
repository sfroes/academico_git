using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaColaboradorVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }

        public List<ColaboradorVO> Colaborador { get; set; }
    }
}
