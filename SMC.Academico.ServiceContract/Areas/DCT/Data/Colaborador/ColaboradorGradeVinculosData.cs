using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorGradeVinculosData : ISMCMappable
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}