using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaColaboradorData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTurma { get; set; }
                
        public List<ColaboradorData> Colaborador { get; set; }
    }
}
