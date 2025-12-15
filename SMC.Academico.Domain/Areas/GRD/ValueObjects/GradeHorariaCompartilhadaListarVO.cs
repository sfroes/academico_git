using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class GradeHorariaCompartilhadaListarVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public string CicloLetivo { get; set; }
        public string EntidadeResponsavel { get; set; }
        public string Descricao { get; set; }
        public List<string> Divisoes { get; set; }
        public bool PerimiteExclusao { get; set; }
    }
}
