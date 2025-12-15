using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class GradeHorariaCompartilhadaListarData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public string CicloLetivo { get; set; }
        public string EntidadeResponsavel { get; set; }
        public string Descricao { get; set; }
        public List<string> Divisoes { get; set; }
        public bool PerimiteExclusao { get; set; }

    }
}
