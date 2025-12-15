using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.DCT.ValueObjects
{
    public class ColaboradorGradeVO : ISMCMappable
    {
        public long Seq { get; set; }
        public string Nome { get; set; }
        public string NomeFormatado { get; set; }
        public List<ColaboradorGradeVinculosVO> Vinculos { get; set; }
    }
}