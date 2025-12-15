using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class GradeHorariaCompartilhadaData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqCicloLetivo { get; set; }
        public long SeqEntidadeResponsavel { get; set; }
        public string Descricao { get; set; }
        public List<GradeHorariaCompartilhadaItemData> Itens { get; set; }
    }
}
