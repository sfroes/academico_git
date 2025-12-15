using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorGradeData : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public string NomeFormatado { get; set; }
        public List<ColaboradorGradeVinculosData> Vinculos { get; set; }
    }
}