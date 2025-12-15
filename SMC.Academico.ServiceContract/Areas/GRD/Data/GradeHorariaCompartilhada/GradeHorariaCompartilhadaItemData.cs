using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class GradeHorariaCompartilhadaItemData : ISMCMappable
    {
        public List<SMCDatasourceItem> DivisoesTurma { get; set; }
        public long SeqTurma { get; set; }
        public long SeqDivisaoTurma { get; set; }
    }
}
