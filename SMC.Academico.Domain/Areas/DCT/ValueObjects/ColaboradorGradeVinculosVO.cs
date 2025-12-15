using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorGradeVinculosVO : ISMCMappable
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}