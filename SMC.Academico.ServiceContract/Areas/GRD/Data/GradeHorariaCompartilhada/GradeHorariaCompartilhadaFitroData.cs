using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class GradeHorariaCompartilhadaFitroData : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public long? SeqCicloLetivo { get; set; }
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        public long? SeqTurma { get; set; }
        public string Descricao { get; set; }
    }
}
