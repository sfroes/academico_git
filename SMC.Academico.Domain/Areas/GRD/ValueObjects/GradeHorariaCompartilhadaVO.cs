using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class GradeHorariaCompartilhadaVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqCicloLetivo { get; set; }
        public long SeqEntidadeResponsavel { get; set; }
        public string Descricao { get; set; }
        public List<GradeHorariaCompartilhadaItemVO> Itens { get; set; }
    }
}
