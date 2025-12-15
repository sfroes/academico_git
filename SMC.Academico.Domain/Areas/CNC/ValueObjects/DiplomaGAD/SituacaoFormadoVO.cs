using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.CNC.ValueObjects
{
    public class SituacaoFormadoVO : ISMCMappable
    {
        public DateTimeOffset? DataConclusaoCurso { get; set; }
        public DateTimeOffset? DataColacaoGrau { get; set; }
        public DateTimeOffset? DataExpedicaoDiploma { get; set; }
    }
}
